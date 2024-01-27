using UnityEngine;

public abstract class MonsterMB : CreatureMB
{
	protected Animator Anim { get; private set; }

	protected void Start()
	{
		Anim = GetComponent<Animator>();
		TypeOfCreature = TypeOfCreature.Monster;
	}
}
