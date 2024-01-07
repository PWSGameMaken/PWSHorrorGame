using UnityEngine;

public class PlankAnimationMB : AnimateObjectsMB
{
	public override void Animate()
	{
		animator.SetBool("PlankStijg", true);
	}
}
