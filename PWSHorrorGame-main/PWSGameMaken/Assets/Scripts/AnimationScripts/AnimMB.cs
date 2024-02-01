using UnityEngine;

public class AnimMB : MonoBehaviour
{
	private Animator _anim;

	private void Start()
	{
		_anim = GetComponent<Animator>();
	}

	public void SetAnimation(string animName, bool state)
	{
		_anim.SetBool(animName, state);
	}
}
