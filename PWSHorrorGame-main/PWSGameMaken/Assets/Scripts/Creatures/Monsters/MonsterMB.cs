using UnityEngine;

public abstract class MonsterMB : CreatureMB
{
	protected AnimMB AnimMB { get; private set; }

	protected void Start()
	{
		AnimMB = GetComponentInChildren<AnimMB>();
		TypeOfCreature = TypeOfCreature.Monster;
	}
}
