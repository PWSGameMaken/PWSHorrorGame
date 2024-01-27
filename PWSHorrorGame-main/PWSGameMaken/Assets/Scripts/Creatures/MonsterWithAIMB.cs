using UnityEngine;
using UnityEngine.AI;
public enum CurrentState
{
	isColliding,
	isHunting,
	isWalking
}

public abstract class MonsterWithAIMB : MonsterMB
{
	protected bool playerIsCaught = false;

	[Header("NavMesh Settings")]
	private NavMeshAgent navMeshAgent;
	[SerializeField] private float rotationSpeed = 15f;
	[SerializeField] private float huntRadius = 10f;
	[SerializeField] private float killRadius = 3f;

	private PlayerMB playerMB;
	private CurrentState currentState;

	protected float RotationSpeed { get => rotationSpeed;}
	protected PlayerMB PlayerMB { get => playerMB; private set => playerMB = value; }
	protected NavMeshAgent NavMeshAgent { get => navMeshAgent; private set => navMeshAgent = value; }


	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, huntRadius);

		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(transform.position, killRadius);
	}

	protected new void Start()
	{
		PlayerMB = PlayerMB.instance;
		NavMeshAgent = GetComponent<NavMeshAgent>();
		currentState = CurrentState.isWalking;
		base.Start();
	}

	protected void Move()
	{
		NavMeshAgent.SetDestination(PlayerMB.PlayerCameraRoot.position);
		CheckDistanceToPlayer();
	}

	protected void CheckDistanceToPlayer()
	{
		float distance = Vector3.Distance(PlayerMB.PlayerCameraRoot.position, transform.position);
		if (distance < killRadius && currentState != CurrentState.isColliding)
		{
			currentState = CurrentState.isColliding;
			CollideWithPlayer();
		}
		else if (distance < huntRadius && currentState != CurrentState.isHunting)
		{
			currentState = CurrentState.isHunting;
			HuntPlayer();
		}
		else if (distance > huntRadius && currentState != CurrentState.isWalking)
		{
			currentState = CurrentState.isWalking;
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
