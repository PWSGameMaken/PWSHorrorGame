using StarterAssets;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController2 : Creature
{
	[Header("Monster")]
	[SerializeField] private GameObject monster;
	[SerializeField] private float _rotationSpeed = 5f;
	[SerializeField] private float _huntRadius = 10f;
	[SerializeField] private float _killRadius = 5f;
	[SerializeField] private Animator _anim;
	[SerializeField] private NavMeshAgent _agent;

	[Header("Player")]
	[SerializeField] private Transform _target;
	[SerializeField] private GameObject _playerCapsule;
	[SerializeField] private Transform _monsterFocusPoint;
	[SerializeField] private GameObject _playerBody;
	[SerializeField] private GameObject _KillLantern;

	[Header("Rest")]
	[SerializeField] private AnimationClip _killAnimation;
	[SerializeField] private AudioSource _AudioSource;

	private bool _isCaught = false;
	private FirstPersonController _fPSController;
	private StarterAssetsInputs _inputScript;

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, _huntRadius);

		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(transform.position, _killRadius);
	}

	private void Start()
	{
		_agent = GetComponent<NavMeshAgent>();
		_fPSController = _playerCapsule.GetComponent<FirstPersonController>();
		_inputScript = _playerCapsule.GetComponent<StarterAssetsInputs>();
	}

	private void Update()
	{
		if (_isCaught)
		{
			FaceTarget(_target, _monsterFocusPoint, 20f);
			return;
		}

		_agent.SetDestination(_target.transform.position);

		float distance = Vector3.Distance(_target.position, transform.position);
		if (distance < _killRadius)
		{
			StartCoroutine(GameOver());
		}
		else if (distance < _huntRadius)
		{
			HuntPlayer();
		}
		else
		{
			_anim.SetBool("Run", false);
			_anim.SetBool("Walk", true);
		}
	}

	private void HuntPlayer()
	{
		//Er is geen audio geselecteerd, dus uitgezet anders error
		//AudioSource.Play();
		FaceTarget(_agent.transform, _target, _rotationSpeed);
		_anim.SetBool("Walk", false);
		_anim.SetBool("Run", true);
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

		_agent.SetDestination(transform.position);

		yield return new WaitForSeconds(_killAnimation.length);

		ContinuePlaying();
	}

	private void BlockPlayerMovement(bool state)
	{
		_inputScript.cursorInputForLook = !state;
		_fPSController.MoveSpeed = state == true ? 0 : 4;
	}

	private void ContinuePlaying()
	{
		IsDead(false);
		BlockPlayerMovement(false);

		_agent.SetDestination(_target.transform.position);

		RespawnSystem.instance.RespawnFromMonsterCollision(_playerCapsule);
		RespawnSystem.instance.RespawnFromMonsterCollision(_agent.gameObject);
	}

	private void IsDead(bool state)
	{
		_isCaught = state;

		_anim.SetBool("Slash", state);
		_anim.SetBool("Run", !state);

		_playerBody.SetActive(state);
		_KillLantern.SetActive(!state);
	}

}
