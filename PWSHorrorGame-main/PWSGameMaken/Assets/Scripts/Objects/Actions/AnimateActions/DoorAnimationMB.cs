using UnityEngine;

public class DoorAnimationMB : ParentAnimateActionsMB
{
	public override void Animate()
	{
		animator.SetBool("OpenL", true);
		animator.SetBool("OpenR", true);
	}
}
