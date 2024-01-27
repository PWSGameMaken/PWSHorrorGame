using UnityEngine;

public class MiniMonsterWithAIMB : MonsterWithAIMB
{
	[SerializeField] private Transform _runAwayPosition;

	private new void Start()
	{
		base.Start();
	}

	private void Update()
	{
		if (playerIsCaught)
		{
			RunAway();
			return;
		}

		Move();
	}

	public override void CollideWithPlayer()
	{
		playerIsCaught = true;
		//ActivateSounds(true);
	}

	public override void FollowPlayer()
	{
		NavMeshAgent.SetDestination(PlayerMB.transform.position);
	}

	public override void HuntPlayer()
	{
		FaceTarget(transform, PlayerMB.PlayerCameraRoot, RotationSpeed);
	}

	private void RunAway()
	{
		NavMeshAgent.SetDestination(_runAwayPosition.position);
	}
}
