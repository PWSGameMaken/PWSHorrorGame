using System.Collections;
using UnityEngine;

public enum MonsterAnimation
{
	Walk,
	Run,
	Slash
}

public enum MonsterAudio
{
	MonsterRun,
	MonsterScream,
	MonsterSlash
}

public class EindMonsterMB : MonsterWithAiMB
{
	[SerializeField] private Transform _focusPoint;
	[SerializeField] private AnimationClip _killAnimation;
	[SerializeField] private Transform[] _creaturesToRespawn;
	[SerializeField] private GameOverMenu _gameOverMenu;

	[SerializeField] private int walkingSpeed = 4;
	[SerializeField] private int runningSpeed = 6;

	private new void Start()
	{
		base.Start();
	}

	private void Update()
	{
		if (playerIsCaught)
		{
			FaceTarget(PlayerMB.PlayerCameraRoot, _focusPoint, PlayerMB.KillCamRotationSpeed);
			return;
		}
			
		Move();
	}

	public override void CollideWithPlayer()
	{
		StartCoroutine(GameOver());
	}

	public override void HuntPlayer()
	{
		FaceTarget(transform, PlayerMB.PlayerCameraRoot, RotationSpeed);
		Run(true);
	}

	public override void FollowPlayer()
	{
		Run(false);
	}

	private void ActivateKillScene(bool activate)
	{
		if (activate)
		{
			AnimMB.SetAnimation(MonsterAnimation.Slash.ToString(), true);
			AnimMB.SetAnimation(MonsterAnimation.Run.ToString(), false);
			AudioManager.PlayOneShot(MonsterAudio.MonsterSlash.ToString(), gameObject);
		}
		else
		{
			AnimMB.SetAnimation(MonsterAnimation.Slash.ToString(), false);
		}
	}

	private void Run(bool running)
	{
		if (running)
		{
			StartRunning();
		}
		else
		{
			StopRunning();
		}
	}

	private void StartRunning()
	{
		NavMeshAgent.speed = runningSpeed;

		AudioManager.PlayOneShot(MonsterAudio.MonsterScream.ToString(), gameObject);
		AudioManager.Play(MonsterAudio.MonsterRun.ToString(), gameObject);

		AnimMB.SetAnimation(MonsterAnimation.Run.ToString(), true);
		AnimMB.SetAnimation(MonsterAnimation.Walk.ToString(), false);
	}

	private void StopRunning()
	{
		NavMeshAgent.speed = walkingSpeed;

		AudioManager.Stop(MonsterAudio.MonsterRun.ToString());

		AnimMB.SetAnimation(MonsterAnimation.Run.ToString(), false);
		AnimMB.SetAnimation(MonsterAnimation.Walk.ToString(), true);
	}

	private IEnumerator GameOver()
	{
		Run(false);
		PlayerIsDead(true);
		yield return new WaitForSeconds(_killAnimation.length);
		AudioListener.pause = true;
		_gameOverMenu.SetActiveMenu();
	}

	public void ContinuePlaying()
	{
		AudioListener.pause = false;
		RespawnSystemMB.Respawn(_creaturesToRespawn);
		_gameOverMenu.SetActiveMenu();
		PlayerIsDead(false);
	}

	private void PlayerIsDead(bool isDying)
	{
		if (isDying)
		{
			ActivateGameOver();
		}
		else
		{
			DeActivateGameOver();
		}
	}

	private void ActivateGameOver()
	{
		playerIsCaught = true;

		var monsterPos = transform.position;
		NavMeshAgent.SetDestination(monsterPos);

		ActivateKillScene(true);
		PlayerMB.ActivateKillScene(true);
	}

	private void DeActivateGameOver()
	{
		playerIsCaught = false;

		var playerPos = PlayerMB.PlayerCameraRoot.position;
		NavMeshAgent.SetDestination(playerPos);

		ActivateKillScene(false);
		PlayerMB.ActivateKillScene(false);
	}
}
