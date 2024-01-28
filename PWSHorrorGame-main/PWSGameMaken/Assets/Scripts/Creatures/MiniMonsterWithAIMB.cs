using UnityEngine;

public class MiniMonsterWithAIMB : MonsterWithAiMB
{
	[SerializeField] private Transform _runAwayPosition;
	private AudioManager _audioManager;

	private new void Start()
	{
		base.Start();
		_audioManager = AudioManager.instance;
		_audioManager.Play("MiniMonsterFootsteps", gameObject);
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
		_audioManager.PlayOneShot("MiniMonsterScream", gameObject);
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
