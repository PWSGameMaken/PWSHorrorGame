using System;
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

	private void FixedUpdate()
	{
		SetNavMeshDestination(_runAwayPosition);
	}

	public override void KillTarget(ITargetMB iTargetMB)
	{
		hasCaughtTarget = true;
		AudioManager.Play(MiniMonsterAudio.MiniMonsterScream.ToString(), gameObject);
	}

	public override void FollowTarget(ITargetMB iTargetMB)
	{
		//Heeft geen functie. Override eruit gooien?

		//Zou niet nodig moeten zijn:
		//var _iTargetMB = iTargetMB as MonoBehaviour ?? throw new ArgumentNullException(nameof(iTargetMB));

		//NavMeshAgent.SetDestination(_iTargetMB.transform.position);
	}

	//Werkt deze wel iedere frame? Moet hij dan niet naar Update()?
	public override void HuntTarget(ITargetMB iTargetMB)
	{
		var _iTargetMB = iTargetMB as MonoBehaviour ?? throw new ArgumentNullException(nameof(iTargetMB));

		FaceObject(transform, _iTargetMB.transform.position, RotationSpeed);
	}
}
