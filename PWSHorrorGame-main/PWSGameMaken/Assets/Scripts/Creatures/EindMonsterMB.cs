using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EindMonsterMB : MonsterWithAIMB
{
	public Transform focusPoint;
	public AnimationClip killAnimation;

	public void ActivateKillScene(Vector3 monsterTarget, bool state)
	{
		navMeshAgent.SetDestination(monsterTarget);
		ActivateSlashAnimation(state);
	}

	private void ActivateSlashAnimation(bool state)
	{
		anim.SetBool("Slash", state);
		anim.SetBool("Run", !state);
	}
}
