using System.Collections;
using UnityEngine;

public class EindMonsterMB : MonsterWithAIMB
{
	[Header("Other")]
	public Transform focusPoint;
	public AnimationClip killAnimation;
	[SerializeField] private Transform[] _creaturesToRespawn;
	[SerializeField] private GameOverMenu gameOverMenu;

	private new void Start()
	{
		base.StartV2();
	}

	private void Update()
	{
		if (playerIsCaught)
		{
			FaceTarget(playerMB.playerCameraRoot, focusPoint, 20f);
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
		FaceTarget(transform, playerMB.playerCameraRoot, rotationSpeed);
		Run(true);
	}

	public override void FollowPlayer()
	{
		Run(false);
	}

	private void ActivateKillScene(Vector3 monsterTarget, bool state)
	{
		navMeshAgent.SetDestination(monsterTarget);
		ActivateSlashAnimation(state);
	}

	private void ActivateSlashAnimation(bool state)
	{
		anim.SetBool("Slash", state);
		anim.SetBool("Run", !state);
	}

	private void Run(bool state)
	{
		navMeshAgent.speed = state == true ? runningSpeed : walkingSpeed;
		playSounds.ActivateSounds(state);

		anim.SetBool("Run", state);
		anim.SetBool("Walk", !state);
	}

	private IEnumerator GameOver()
	{
		IsDead(true);
		yield return new WaitForSeconds(killAnimation.length);

		gameOverMenu.SetActiveMenu();
	}

	public void ContinuePlaying()
	{
		RespawnSystemMB.Respawn(_creaturesToRespawn);
		IsDead(false);
	}

	private void IsDead(bool state)
	{
		playerIsCaught = state;
		Run(false);
		//Deze werkt niet aangezien het monster en de speler in eerste instantie nog naast elkaar staan. De update van MonsterWithAIMB fixt dit momenteel wel.
		//currentState = state ? MonsterState.isColliding : MonsterState.isWalking;
		var monsterTarget = state ? transform.position : playerMB.playerCameraRoot.position;

		ActivateKillScene(monsterTarget, state);
		playerMB.ActivateKillScene(state);
	}
}
