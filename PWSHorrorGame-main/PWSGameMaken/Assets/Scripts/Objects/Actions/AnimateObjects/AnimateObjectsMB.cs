using UnityEngine;

public abstract class AnimateObjectsMB : ActionObjectMB
{
	protected Animator animator;

	public override void Action()
	{
		foreach (var _object in objects)
		{
			animator = _object.GetComponent<Animator>();
			Animate();
		}
	}


	public abstract void Animate();
}
