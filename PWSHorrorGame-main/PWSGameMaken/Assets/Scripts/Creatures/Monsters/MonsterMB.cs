using UnityEngine;
using UnityEngine.AI;

public class MonsterMB : CreatureMB
{
	#region Singleton
	public static MonsterMB instance;

	private void Awake()
	{
		instance = this;
	}
	#endregion

	[HideInInspector] public PlaySounds playSounds;
	[HideInInspector] public NavMeshAgent navMeshAgent;
	[HideInInspector] public Animator anim;
	public Transform focusPoint;
	public AnimationClip killAnimation;
	public float rotationSpeed = 5f;
	public float huntRadius = 10f;
	public float killRadius = 2f;
	public int walkingSpeed = 4;
	public int runningSpeed = 6;

	private void Start()
	{
		playSounds = GetComponent<PlaySounds>();
		anim = GetComponent<Animator>();
		navMeshAgent = GetComponent<NavMeshAgent>();
	}

	public void ActivateKillScene(Vector3 monsterTarget, bool state)
	{
		navMeshAgent.SetDestination(monsterTarget);
		ActivateSlashAnimation(state);
	}

	private void ActivateSlashAnimation(bool state)
	{
		anim.SetBool("Slash", state);
		anim.SetBool("Run", !state);
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
