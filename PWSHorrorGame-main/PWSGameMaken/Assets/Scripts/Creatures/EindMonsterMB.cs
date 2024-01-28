using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EindMonsterMB : MonsterWithAiMB
{
	[SerializeField] private Transform _focusPoint;
	[SerializeField] private AnimationClip _killAnimation;
	[SerializeField] private Transform[] _creaturesToRespawn;
	[SerializeField] private GameOverMenu _gameOverMenu;

	[SerializeField] private int walkingSpeed = 4;
	[SerializeField] private int runningSpeed = 6;

	private AudioManager _audioManager;

	private new void Start()
	{
		base.Start();
		AnimMB.SetAnimation(MonsterAnimations.Walk.ToString(), false);
		_audioManager = FindObjectOfType<AudioManager>();
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

	private void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.CompareTag("Player"))
		{
			var contactPoint = collision.GetContact(0);

			var name = contactPoint.thisCollider.gameObject.name;
			if (name == "KillArea")
			{
				print("KillArea is hit");
			}
			else if (name == "HuntArea")
			{
				print("HuntArea is hit");
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		
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
			AnimMB.SetAnimation(MonsterAnimations.Slash.ToString(), true);
			AnimMB.SetAnimation(MonsterAnimations.Run.ToString(), false);
			_audioManager.PlayOneShot("MonsterSlash", gameObject);
		}
		else
		{
			AnimMB.SetAnimation(MonsterAnimations.Slash.ToString(), false);
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

		_audioManager.PlayOneShot("MonsterScream", gameObject);
		_audioManager.Play("MonsterRun", gameObject);

		AnimMB.SetAnimation(MonsterAnimations.Run.ToString(), true);
		AnimMB.SetAnimation(MonsterAnimations.Walk.ToString(), false);
	}

	private void StopRunning()
	{
		NavMeshAgent.speed = walkingSpeed;

		_audioManager.Stop("MonsterRun");

		AnimMB.SetAnimation(MonsterAnimations.Run.ToString(), false);
		AnimMB.SetAnimation(MonsterAnimations.Walk.ToString(), true);
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
