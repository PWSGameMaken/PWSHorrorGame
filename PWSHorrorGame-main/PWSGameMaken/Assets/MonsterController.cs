using UnityEngine.AI;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
using StarterAssets;

public class MonsterController : MonoBehaviour
{
	[SerializeField] private float _killRadius = 2f;
	[SerializeField] private float _huntRadius = 10f;
	[SerializeField] private float _rotationSpeed = 5f;

	[SerializeField] private Transform _target;
	[SerializeField] private NavMeshAgent _agent;

	[SerializeField] private AnimationClip killAnimation;

	[SerializeField] private Transform monsterHead;

	[SerializeField] private GameObject playerBody;
	[SerializeField] private GameObject KillLantern;
	[SerializeField] private FirstPersonController FPSController;
	[SerializeField] private StarterAssetsInputs inputScript;
    [SerializeField] private AudioSource AudioSource;
    private bool isCaught = false;


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
	}

	private void Update()
	{
		if(isCaught)
		{
			FaceTarget(_target, monsterHead, 100f);
			return;
		}

		_agent.SetDestination(_target.transform.position);

		
		float distance = Vector3.Distance(_target.position, transform.position);
		if(distance < _huntRadius)
		{
			HuntPlayer();
            AudioSource.Play();
        }

		if(distance < _killRadius)
		{
			KillPlayer();
		}
	}

	private void HuntPlayer()
	{
		FaceTarget(_agent.transform, _target, _rotationSpeed);
	}

	private void KillPlayer()
	{
		isCaught = true;
		_agent.SetDestination(transform.position);
		
		playerBody.SetActive(false);
		KillLantern.SetActive(true);
		BlockPlayerMovement();
		//playerBody.transform.rotation = Quaternion.LookRotation(new Vector3(0, _target.transform.rotation.y, 0));

		StartCoroutine(GameOver());
	}

	private void FaceTarget(Transform ObjectToRotate, Transform ObjectToFace, float rotationSpeed)
	{
		Vector3 direction = (ObjectToFace.position - ObjectToRotate.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, direction.y, direction.z));
		ObjectToRotate.rotation = Quaternion.Slerp(ObjectToRotate.rotation, lookRotation, Time.deltaTime * rotationSpeed);
	}

	private IEnumerator GameOver()
	{
		//Voor wanneer we een animatie hebben.
		//yield return new WaitForSeconds(killAnimation.length);

		yield return new WaitForSeconds(3);
		UnblockPlayerMovement();
		SceneManager.LoadScene(1);
	}

	private void BlockPlayerMovement()
	{
		inputScript.cursorInputForLook = false;
		FPSController.MoveSpeed = 0;
	}

	private void UnblockPlayerMovement()
	{
		inputScript.cursorInputForLook = true;
		FPSController.MoveSpeed = 4;
	}
}
