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
	private MonsterMB _monsterMB;

	[SerializeField] private Transform[] _creaturesToRespawn;
	[SerializeField] private GameOverMenu gameOverMenu;
	private RespawnSystemMB _respawnSystemMB;


	private void Start()
	{
		_monsterMB = MonsterMB.instance;
		_playerMB = PlayerMB.instance;
		_respawnSystemMB = RespawnSystemMB.instance;
	}

	private void Update()
	{
		if (_isCaught)
		{
			FaceTarget(_playerMB.playerCameraRoot, _monsterMB.focusPoint, 20f);
			return;
		}

		_monsterMB.navMeshAgent.SetDestination(_playerMB.playerCameraRoot.position);

		CheckDistanceToPlayer();
	}

	private void CheckDistanceToPlayer()
	{
		float distance = Vector3.Distance(_playerMB.playerCameraRoot.position, transform.position);
		if (distance < _monsterMB.killRadius && _currentState != MonsterState.killing)
		{
			StartCoroutine(GameOver());
		}
		else if (distance < _monsterMB.huntRadius && _currentState != MonsterState.hunting)
		{
			HuntPlayer();
		}
		else if (distance > _monsterMB.huntRadius && _currentState != MonsterState.walking)
		{
			_currentState = MonsterState.walking;
			_monsterMB.Run(false);
		}
	}

	private void HuntPlayer()
	{
		_currentState = MonsterState.hunting;
		FaceTarget(_monsterMB.navMeshAgent.transform, _playerMB.playerCameraRoot, _monsterMB.rotationSpeed);
		_monsterMB.Run(true);
	}

	private void FaceTarget(Transform ObjectToRotate, Transform ObjectToFace, float rotationSpeed)
	{
		Vector3 direction = (ObjectToFace.position - ObjectToRotate.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, direction.y, direction.z));
		ObjectToRotate.rotation = Quaternion.Slerp(ObjectToRotate.rotation, lookRotation, Time.deltaTime * rotationSpeed);
	}

	private IEnumerator GameOver()
	{
		_monsterMB.Run(false);
		IsDead(true);

		yield return new WaitForSeconds(_monsterMB.killAnimation.length);

		gameOverMenu.SetActiveMenu();
	}

	public void ContinuePlaying()
	{
		IsDead(false);

		_respawnSystemMB.RespawnFromMonsterCollision(_creaturesToRespawn);
	}

	private void IsDead(bool state)
	{
		_currentState = state ? MonsterState.killing : MonsterState.walking;
		_isCaught = state;
		var monsterTarget = state ? transform.position : _playerMB.playerCameraRoot.position;

		_monsterMB.ActivateKillScene(monsterTarget, state);
		_playerMB.ActivateKillScene(state);
	}
}
