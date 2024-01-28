using UnityEngine;

public enum MonsterAnimations
{
	Walk,
	Run,
	Slash
}

public enum PlayerAnimations
{
	NoAnimation,
	IsWalking,
	HasVase,
	HasStone,
}

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
