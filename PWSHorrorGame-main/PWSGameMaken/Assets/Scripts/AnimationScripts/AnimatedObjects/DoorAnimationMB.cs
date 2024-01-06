using UnityEngine;

public class DoorAnimationMB : AnimateObjectsMB
{
	public override void Animate()
	{
		animator.SetBool("Open", true);
	}
}
