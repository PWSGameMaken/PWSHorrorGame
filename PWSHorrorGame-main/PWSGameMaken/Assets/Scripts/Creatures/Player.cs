using UnityEngine;

public class Player : Creature
{
	#region Singleton

	public static Player instance;

	private void Awake()
	{
		instance = this;
	}
	#endregion

	public Transform playerCamera;
	public GameObject playerCapsule;
	public Transform monsterFocusPoint;
	public GameObject playerBody;
	public GameObject KillLantern;
}
