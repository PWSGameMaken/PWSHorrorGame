using UnityEngine;
using UnityEngine.AI;

public class Monster : Creature
{
	#region Singleton

	public static Monster instance;

	private void Awake()
	{
		instance = this;
	}
	#endregion

	public GameObject monster;
	public Animator anim;
	public NavMeshAgent navMeshAgent;
	public float rotationSpeed = 5f;
	public float huntRadius = 10f;
	public float killRadius = 2f;
}
