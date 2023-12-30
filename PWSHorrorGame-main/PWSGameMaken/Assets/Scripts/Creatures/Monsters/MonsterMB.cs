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

	public GameObject monsterGO;
	public Animator anim;
	public NavMeshAgent navMeshAgent;
	public float rotationSpeed = 5f;
	public float huntRadius = 10f;
	public float killRadius = 2f;
	public int walkingSpeed = 4;
	public int runningSpeed = 6;
	public AnimationClip killAnimation;
	public AudioSource audioSource;

	private void Start()
	{
		navMeshAgent = GetComponent<NavMeshAgent>();
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, huntRadius);

		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(transform.position, killRadius);
	}
}
