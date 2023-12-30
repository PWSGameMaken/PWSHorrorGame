using StarterAssets;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MonsterControllerMB : MonoBehaviour
{
	private bool _isCaught = false;

	private PlayerMB _playerMB;
	private MonsterMB _monsterMB;

	private FirstPersonController _fPSController;
	private StarterAssetsInputs _inputScript;
	private NavMeshAgent _navMeshAgent;
	[SerializeField] private UIControllerMB _uiControllerMB;


	private void Start()
	{
		_monsterMB = MonsterMB.instance;
		_playerMB = PlayerMB.instance;

		_navMeshAgent = _monsterMB.GetComponent<NavMeshAgent>();
		_fPSController = _playerMB.GetComponent<FirstPersonController>();
		_inputScript = _playerMB.GetComponent<StarterAssetsInputs>();
	}

	private void Update()
	{
		if (_isCaught)
		{
			FaceTarget(_playerMB.playerCameraRoot, _playerMB.monsterFocusPoint, 20f);
			return;
		}

		_monsterMB.navMeshAgent.SetDestination(_playerMB.playerCameraRoot.transform.position);

		float distance = Vector3.Distance(_playerMB.playerCameraRoot.position, transform.position);
		if (distance < _monsterMB.killRadius)
		{
			StartCoroutine(GameOver());
		}
		else if (distance < _monsterMB.huntRadius)
		{
			HuntPlayer();
		}
		else
		{
			SetMovementAnimState(false);
		}
	}

	private void HuntPlayer()
	{
		//Er is geen audio geselecteerd, dus uitgezet anders error
		if(_monsterMB.audioSource.isPlaying == false)
		{
			_monsterMB.audioSource.PlayOneShot(_monsterMB.audioSource.clip);
		}

		FaceTarget(_monsterMB.navMeshAgent.transform, _playerMB.playerCameraRoot, _monsterMB.rotationSpeed);
		SetMovementAnimState(true);
	}

	private void SetMovementAnimState(bool state)
	{
		_navMeshAgent.speed = state == true ? _monsterMB.runningSpeed : _monsterMB.walkingSpeed;
		_monsterMB.anim.SetBool("Run", state);
		_monsterMB.anim.SetBool("Walk", !state);
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
		BlockPlayerMovement(true);

		_monsterMB.navMeshAgent.SetDestination(transform.position);

		yield return new WaitForSeconds(_monsterMB.killAnimation.length);

		_uiControllerMB.SetActiveGameOverMenu(true);
	}

	private void BlockPlayerMovement(bool state)
	{
		_inputScript.cursorInputForLook = !state;
		_fPSController.MoveSpeed = state == true ? 0 : 4;
	}

	public void ContinuePlaying()
	{
		IsDead(false);
		BlockPlayerMovement(false);

		RespawnSystemMB.instance.RespawnFromMonsterCollision(_playerMB.playerCapsuleGO.transform);
		RespawnSystemMB.instance.RespawnFromMonsterCollision(_monsterMB.monsterGO.transform);

		_monsterMB.navMeshAgent.SetDestination(_playerMB.playerCameraRoot.transform.position);
	}

	private void IsDead(bool state)
	{
		_isCaught = state;

		_monsterMB.anim.SetBool("Slash", state);
		_monsterMB.anim.SetBool("Run", !state);

		_playerMB.playerBody.SetActive(!state);
		_playerMB.killLantern.SetActive(state);
	}
}
