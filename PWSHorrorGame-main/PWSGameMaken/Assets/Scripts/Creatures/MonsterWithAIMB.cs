using UnityEngine;
using UnityEngine.AI;
public enum MonsterState
{
	isColliding,
	isHunting,
	isWalking
}

public abstract class MonsterWithAIMB : MonsterMB
{
	[HideInInspector] public NavMeshAgent navMeshAgent;
	[Header("NavMesh Settings")]
	public float rotationSpeed = 15f;
	public float huntRadius = 10f;
	public float killRadius = 3f;
	public int walkingSpeed = 4;
	public int runningSpeed = 6;

	protected PlayerMB playerMB;
	protected bool playerIsCaught = false;

	private MonsterState currentState;

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, huntRadius);

		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(transform.position, killRadius);
	}

	protected void StartV2()
	{
		playerMB = PlayerMB.instance;
		navMeshAgent = GetComponent<NavMeshAgent>();
		currentState = MonsterState.isWalking;
		base.Start();
	}

	protected void Move()
	{
		navMeshAgent.SetDestination(playerMB.playerCameraRoot.position);
		CheckDistanceToPlayer();
	}

	protected void CheckDistanceToPlayer()
	{
		float distance = Vector3.Distance(playerMB.playerCameraRoot.position, transform.position);
		if (distance < killRadius && currentState != MonsterState.isColliding)
		{
			currentState = MonsterState.isColliding;
			CollideWithPlayer();
		}
		else if (distance < huntRadius && currentState != MonsterState.isHunting)
		{
			currentState = MonsterState.isHunting;
			HuntPlayer();
		}
		else if (distance > huntRadius && currentState != MonsterState.isWalking)
		{
			currentState = MonsterState.isWalking;
			FollowPlayer();
		}
	}

	protected void FaceTarget(Transform ObjectToRotate, Transform ObjectToFace, float rotationSpeed)
	{
		Vector3 direction = (ObjectToFace.position - ObjectToRotate.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, direction.y, direction.z));
		ObjectToRotate.rotation = Quaternion.Slerp(ObjectToRotate.rotation, lookRotation, Time.deltaTime * rotationSpeed);
	}

	public abstract void CollideWithPlayer();

	public abstract void HuntPlayer();

	public abstract void FollowPlayer();
}
