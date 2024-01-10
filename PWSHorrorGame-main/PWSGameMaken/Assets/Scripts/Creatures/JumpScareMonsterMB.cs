using UnityEngine;

public class JumpScareMonsterMB : MonsterWithAIMB
{
	[Header("Other")]
	[SerializeField] private Transform _runAwayDirection;

	private new void Start()
	{
		base.StartV2();
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
		playSounds.ActivateSounds(true);
	}

	public override void FollowPlayer()
	{
		navMeshAgent.SetDestination(playerMB.transform.position);
	}

	public override void HuntPlayer()
	{
		FaceTarget(transform, playerMB.playerCameraRoot, rotationSpeed);
	}

	private void RunAway()
	{
		navMeshAgent.SetDestination(_runAwayDirection.position);
	}
}
