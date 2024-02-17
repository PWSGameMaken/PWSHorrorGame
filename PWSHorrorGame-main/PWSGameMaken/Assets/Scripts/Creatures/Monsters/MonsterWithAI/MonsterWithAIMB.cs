using UnityEngine;
using UnityEngine.AI;

public abstract class MonsterWithAiMB : MonsterMB
{
	[SerializeField] private float _rotationSpeed = 15f;

	private NavMeshAgent _navMeshAgent;
	protected bool hasCaughtTarget = false;

	protected float RotationSpeed { get => _rotationSpeed; }
	protected NavMeshAgent NavMeshAgent { get => _navMeshAgent; private set => _navMeshAgent = value; }

	protected new void Start()
	{
		base.Start();
		NavMeshAgent = GetComponent<NavMeshAgent>();
	}

	protected void SetNavMeshDestination(Transform hasCaughtTargetDestination)
	{
		if (hasCaughtTarget)
		{
			SetDestination(hasCaughtTargetDestination);
		}
		else
		{
			SetDestination(target.transform);
		}
	}

	private void SetDestination(Transform target)
	{
		if (target == null) return;
		
		NavMeshAgent.SetDestination(target.position);
	}

	public abstract void KillTarget(ITargetMB targetMB);

	public abstract void HuntTarget(ITargetMB targetMB);

	public abstract void FollowTarget(ITargetMB targetMB);
}

//private void SetDestination(ITargetMB targetMB)
//{
//	var _targetMB = targetMB as MonoBehaviour; //?? throw new ArgumentNullException(nameof(targetMB));
//	SetDestination(_targetMB?.gameObject.transform);
//}