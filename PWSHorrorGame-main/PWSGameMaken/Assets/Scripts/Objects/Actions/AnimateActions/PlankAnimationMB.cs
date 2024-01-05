using UnityEngine;

public class PlankAnimationMB : ParentAnimateActionsMB
{
	public override void Animate()
	{
		animator.SetBool("PlankStijg", true);
	}
}
