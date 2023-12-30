using StarterAssets;
using System.Collections;
using UnityEngine;

public class MonsterControllerMB : MonoBehaviour
{
	private MonsterMB _monster;

	private bool _isCaught = false;

	private PlayerMB _player;
	private FirstPersonController _fPSController;
	private StarterAssetsInputs _inputScript;
	[SerializeField] private UIControllerMB _uiControllerMB;


	private void Start()
	{
		_monster = MonsterMB.instance;
		_player = PlayerMB.instance;

		_fPSController = _player.GetComponent<FirstPersonController>();
		_inputScript = _player.GetComponent<StarterAssetsInputs>();
	}

	private void Update()
	{
		if (_isCaught)
		{
			FaceTarget(_player.playerCameraRoot, _player.monsterFocusPoint, 20f);
			return;
		}

		_monster.navMeshAgent.SetDestination(_player.playerCameraRoot.transform.position);

		float distance = Vector3.Distance(_player.playerCameraRoot.position, transform.position);
		if (distance < _monster.killRadius)
		{
			StartCoroutine(GameOver());
		}
		else if (distance < _monster.huntRadius)
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
		if(_monster.audioSource.isPlaying == false)
		{
			_monster.audioSource.PlayOneShot(_monster.audioSource.clip);
		}

		FaceTarget(_monster.navMeshAgent.transform, _player.playerCameraRoot, _monster.rotationSpeed);
		SetMovementAnimState(true);
	}

	private void SetMovementAnimState(bool state)
	{
		_monster.anim.SetBool("Run", state);
		_monster.anim.SetBool("Walk", !state);
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

		_monster.navMeshAgent.SetDestination(transform.position);

		yield return new WaitForSeconds(_monster.killAnimation.length);

		_uiControllerMB.SetActiveGameOverUI(true);
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

		RespawnSystemMB.instance.RespawnFromMonsterCollision(_player.playerCapsuleGO);
		RespawnSystemMB.instance.RespawnFromMonsterCollision(_monster.monsterGO);

		_monster.navMeshAgent.SetDestination(_player.playerCameraRoot.transform.position);
	}

	private void IsDead(bool state)
	{
		_isCaught = state;

		_monster.anim.SetBool("Slash", state);
		_monster.anim.SetBool("Run", !state);

		_player.playerBody.SetActive(!state);
		_player.killLantern.SetActive(state);
	}
}
