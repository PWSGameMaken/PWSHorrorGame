using UnityEngine;

public abstract class AnimateObjectsMB : ActionObjectMB
{
	protected Animator animator;

	private void Start()
	{
		animator = GetComponent<Animator>();
	}

	public override void Action()
	{
		for (int i = 0; i < objects.Length; i++)
		{
			Animate();
		}
	}

	public abstract void Animate();
}
