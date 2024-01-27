using System.Collections;
using UnityEngine;

public class EindMonsterMB : MonsterWithAIMB
{
	[SerializeField] private Transform _focusPoint;
	[SerializeField] private AnimationClip _killAnimation;
	[SerializeField] private Transform[] _creaturesToRespawn;
	[SerializeField] private GameOverMenu _gameOverMenu;
	[SerializeField] private int walkingSpeed = 4;
	[SerializeField] private int runningSpeed = 6;
	private AudioManager_EindMonster _audioManager;

	private new void Start()
	{
		base.Start();
		Anim.SetBool("Walk", true);
		_audioManager = GetComponentInChildren<AudioManager_EindMonster>();
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

	private void ActivateKillScene(Vector3 monsterTarget, bool state)
	{
		NavMeshAgent.SetDestination(monsterTarget);
		ActivateSlashAnimation(state);
	}

	private void ActivateSlashAnimation(bool state)
	{
		Anim.SetBool("Slash", state);
		Anim.SetBool("Run", !state);
	}

	private void Run(bool state)
	{
		NavMeshAgent.speed = state == true ? runningSpeed : walkingSpeed;
		_audioManager.Footsteps(state);
		_audioManager.Scream();

		Anim.SetBool("Run", state);
		Anim.SetBool("Walk", !state);
	}

	private IEnumerator GameOver()
	{
		Run(false);
		IsDead(true);
		yield return new WaitForSeconds(_killAnimation.length);
		AudioListener.pause = true;
		_gameOverMenu.SetActiveMenu();
	}

	public void ContinuePlaying()
	{
		AudioListener.pause = false;
		RespawnSystemMB.Respawn(_creaturesToRespawn);
		_gameOverMenu.SetActiveMenu();
		IsDead(false);
	}

	private void IsDead(bool state)
	{
		//Deze werkt niet aangezien het monster en de speler in eerste instantie nog naast elkaar staan. De update van MonsterWithAIMB fixt dit momenteel wel.
		//currentState = state ? MonsterState.isColliding : MonsterState.isWalking;

		playerIsCaught = state;

		var monsterTarget = state ? transform.position : PlayerMB.PlayerCameraRoot.position;
		ActivateKillScene(monsterTarget, state);
		PlayerMB.ActivateKillScene(state);
	}
}
