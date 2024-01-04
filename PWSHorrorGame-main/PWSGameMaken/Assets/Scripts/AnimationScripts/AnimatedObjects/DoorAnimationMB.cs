using UnityEngine;

public class DoorAnimationMB : AnimateObjectsMB
{
	public override void Animate()
	{
		animator.SetBool("OpenL", true);
		animator.SetBool("OpenR", true);
	}
}
