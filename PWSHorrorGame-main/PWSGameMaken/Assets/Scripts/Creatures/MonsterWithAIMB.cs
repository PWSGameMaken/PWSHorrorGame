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
	public float rotationSpeed = 5f;
	public float huntRadius = 10f;
	public float killRadius = 2f;
	public int walkingSpeed = 4;
	public int runningSpeed = 6;
	protected PlayerMB playerMB;

	private MonsterState currentState;

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

	public abstract void CollideWithPlayer();

	public abstract void HuntPlayer();

	public abstract void FollowPlayer();
}
