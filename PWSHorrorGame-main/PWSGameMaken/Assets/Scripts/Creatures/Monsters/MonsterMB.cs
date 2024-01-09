using UnityEngine;
using UnityEngine.AI;

public abstract class MonsterMB : CreatureMB
{
	#region Singleton
	public static MonsterMB instance;

	private void Awake()
	{
		instance = this;
	}
	#endregion

	[HideInInspector] public PlaySounds playSounds;
	[HideInInspector] public Animator anim;

	protected void Start()
	{
		playSounds = GetComponent<PlaySounds>();
		anim = GetComponent<Animator>();
		typeOfCreature = TypeOfCreature.Monster;
	}
}
