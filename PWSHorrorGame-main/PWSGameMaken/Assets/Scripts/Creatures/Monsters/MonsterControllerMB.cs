using System.Collections;
using UnityEngine;

public enum MonsterState
{
	killing,
	hunting,
	walking
}

public class MonsterControllerMB : MonoBehaviour
{
	private bool _isCaught = false;
	private MonsterState _currentState;

	private PlayerMB _playerMB;
	private EindMonsterMB _eindMonsterMB;

	[SerializeField] private Transform[] _creaturesToRespawn;
	[SerializeField] private GameOverMenu gameOverMenu;


	private void Start()
	{
		_eindMonsterMB = GetComponent<EindMonsterMB>();
		_playerMB = PlayerMB.instance;
	}

	private void Update()
	{
		if (_isCaught)
		{
			FaceTarget(_playerMB.playerCameraRoot, _eindMonsterMB.focusPoint, 20f);
			return;
		}

		_eindMonsterMB.navMeshAgent.SetDestination(_playerMB.playerCameraRoot.position);

		CheckDistanceToPlayer();
	}

	private void CheckDistanceToPlayer()
	{
		float distance = Vector3.Distance(_playerMB.playerCameraRoot.position, transform.position);
		if (distance < _eindMonsterMB.killRadius && _currentState != MonsterState.killing)
		{
			StartCoroutine(GameOver());
		}
		else if (distance < _eindMonsterMB.huntRadius && _currentState != MonsterState.hunting)
		{
			HuntPlayer();
		}
		else if (distance > _eindMonsterMB.huntRadius && _currentState != MonsterState.walking)
		{
			_currentState = MonsterState.walking;
			_eindMonsterMB.Run(false);
		}
	}

	private void HuntPlayer()
	{
		_currentState = MonsterState.hunting;
		FaceTarget(_eindMonsterMB.navMeshAgent.transform, _playerMB.playerCameraRoot, _eindMonsterMB.rotationSpeed);
		_eindMonsterMB.Run(true);
	}

	private void FaceTarget(Transform ObjectToRotate, Transform ObjectToFace, float rotationSpeed)
	{
		Vector3 direction = (ObjectToFace.position - ObjectToRotate.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, direction.y, direction.z));
		ObjectToRotate.rotation = Quaternion.Slerp(ObjectToRotate.rotation, lookRotation, Time.deltaTime * rotationSpeed);
	}

	private IEnumerator GameOver()
	{
		IsDead(true);

		yield return new WaitForSeconds(_eindMonsterMB.killAnimation.length);

		gameOverMenu.SetActiveMenu();
	}

	public void ContinuePlaying()
	{
		IsDead(false);

		RespawnSystemMB.Respawn(_creaturesToRespawn);
	}

	private void IsDead(bool state)
	{
		_eindMonsterMB.Run(false);
		_currentState = state ? MonsterState.killing : MonsterState.walking;
		_isCaught = state;
		var monsterTarget = state ? transform.position : _playerMB.playerCameraRoot.position;

		_eindMonsterMB.ActivateKillScene(monsterTarget, state);
		_playerMB.ActivateKillScene(state);
	}
}
