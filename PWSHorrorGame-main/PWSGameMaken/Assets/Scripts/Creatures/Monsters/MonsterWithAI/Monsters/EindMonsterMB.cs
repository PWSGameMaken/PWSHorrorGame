using System;
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
	//Staat nu tijdelijk bij PlayerMB
	//[SerializeField] private Transform _focusPoint;
	[SerializeField] private Transform _respawnPos;

	[SerializeField] private int walkingSpeed = 4;
	[SerializeField] private int runningSpeed = 6;

	private new void Start()
	{
		base.Start();
	}

	private void Update()
	{
		SetNavMeshDestination();
	}

	public override void KillTarget(ITargetMB targetMB)
	{
		hasCaughtTarget = true;

		StartSlashing();
		Walking();
		targetMB.Kill();
	}

	public override void HuntTarget(ITargetMB targetMB)
	{
		var _targetMB = targetMB as MonoBehaviour ?? throw new ArgumentNullException(nameof(targetMB));

		FaceObject(transform, _targetMB.transform.position, RotationSpeed);
		Running();
	}

	public override void FollowTarget(ITargetMB targetMB)
	{
		Walking();
	}

	private void Running()
	{
		NavMeshAgent.speed = runningSpeed;

		AudioManager.PlayOneShot(MonsterAudio.MonsterScream.ToString(), gameObject);
		AudioManager.Play(MonsterAudio.MonsterRun.ToString(), gameObject);

		AnimMB.SetAnimation(MonsterAnimation.Run.ToString(), true);
		AnimMB.SetAnimation(MonsterAnimation.Walk.ToString(), false);
	}

	private void Walking()
	{
		NavMeshAgent.speed = walkingSpeed;

		AudioManager.Stop(MonsterAudio.MonsterRun.ToString());

		AnimMB.SetAnimation(MonsterAnimation.Run.ToString(), false);
		AnimMB.SetAnimation(MonsterAnimation.Walk.ToString(), true);
	}

	public void ContinuePlaying()
	{
		hasCaughtTarget = false;

		RespawnSystemMB.Respawn(transform, _respawnPos);
		
		//Dit is nog een los eindje, Kun je de target niet beter met de ContinuePlaying meegeven?
		//Wat als het monster niet meer op de Player gefocust is? Maar je alsnog op de UI klikt
		Target.Respawn();

		StopSlashing();
	}

	private void StartSlashing()
	{
		AnimMB.SetAnimation(MonsterAnimation.Slash.ToString(), true);
		AnimMB.SetAnimation(MonsterAnimation.Run.ToString(), false);
		AudioManager.PlayOneShot(MonsterAudio.MonsterSlash.ToString(), gameObject);
	}

	private void StopSlashing()
	{
		AnimMB.SetAnimation(MonsterAnimation.Slash.ToString(), false);
	}

	private void SetNavMeshDestination()
	{
		if (hasCaughtTarget)
		{
			SetDestination(transform);
		}
		else
		{
			SetDestination(Target);
		}
	}
}
