using UnityEngine;

public enum MiniMonsterAudio
{
	MiniMonsterFootsteps,
	MiniMonsterScream
}

public class MiniMonsterWithAIMB : MonsterWithAiMB
{
	[SerializeField] private Transform _runAwayPosition;

	private new void Start()
	{
		base.Start();
		AudioManager.Play(MiniMonsterAudio.MiniMonsterFootsteps.ToString(), gameObject);
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
		AudioManager.Play(MiniMonsterAudio.MiniMonsterScream.ToString(), gameObject);
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
