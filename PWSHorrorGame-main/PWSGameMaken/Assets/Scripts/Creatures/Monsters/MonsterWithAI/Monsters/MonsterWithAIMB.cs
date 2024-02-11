using System;
using UnityEngine;
using UnityEngine.AI;

public abstract class MonsterWithAiMB : CreatureMB
{
	protected bool hasCaughtTarget = false;

	[Header("NavMesh Settings")]
	private NavMeshAgent _navMeshAgent;
	[SerializeField] private float _rotationSpeed = 15f;

	[SerializeField] private Transform _targetTransform;
	private ITargetMB _target;

	protected ITargetMB Target { get => _target;}
	protected float RotationSpeed { get => _rotationSpeed;}
	protected NavMeshAgent NavMeshAgent { get => _navMeshAgent; private set => _navMeshAgent = value; }

	protected new void Start()
	{
		base.Start();
		_target = _targetTransform.GetComponentInChildren<ITargetMB>();
		NavMeshAgent = GetComponent<NavMeshAgent>();
	}

	protected void SetDestination(ITargetMB targetMB)
	{
		var _targetMB = targetMB as MonoBehaviour ?? throw new ArgumentNullException(nameof(targetMB));
		SetDestination(_targetMB.gameObject.transform);
	}

	protected void SetDestination(Transform target)
	{
		NavMeshAgent.SetDestination(target.position);
	}

	public abstract void KillTarget(ITargetMB targetMB);

	public abstract void HuntTarget(ITargetMB targetMB);

	public abstract void FollowTarget(ITargetMB targetMB);
}

//public enum CurrentState
//{
//	isColliding,
//	isHunting,
//	isWalking
//}

//Variabelen
//private CurrentState currentState;

//In Start
//currentState = CurrentState.isWalking;

//In Move
//CheckDistanceToPlayer();

//protected void CheckDistanceToPlayer()
//{
//	float distance = Vector3.Distance(PlayerMB.PlayerCameraRoot.position, transform.position);
//	var killMode = distance < killRadius && currentState != CurrentState.isColliding;
//	var huntMode = distance < huntRadius && currentState != CurrentState.isHunting;
//	var lookForMode = distance > huntRadius && currentState != CurrentState.isWalking;
//	if (killMode)
//	{
//		currentState = CurrentState.isColliding;
//		//CollideWithPlayer();
//	}
//	else if (huntMode)
//	{
//		currentState = CurrentState.isHunting;
//		//HuntPlayer();
//	}
//	else if (lookForMode)
//	{
//		currentState = CurrentState.isWalking;
//		//FollowPlayer();
//	}
//}