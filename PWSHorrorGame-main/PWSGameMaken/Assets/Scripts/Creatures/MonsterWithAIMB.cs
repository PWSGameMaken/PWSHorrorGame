using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class MonsterWithAIMB : MonsterMB
{
	[HideInInspector] public NavMeshAgent navMeshAgent;
	public float rotationSpeed = 5f;
	public float huntRadius = 10f;
	public float killRadius = 2f;
	public int walkingSpeed = 4;
	public int runningSpeed = 6;

	private new void Start()
	{
		navMeshAgent = GetComponent<NavMeshAgent>();
		base.Start();
	}

	public void Run(bool state)
	{
		navMeshAgent.speed = state == true ? runningSpeed : walkingSpeed;
		playSounds.ActivateSounds(state);

		anim.SetBool("Run", state);
		anim.SetBool("Walk", !state);
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, huntRadius);

		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(transform.position, killRadius);
	}
}
