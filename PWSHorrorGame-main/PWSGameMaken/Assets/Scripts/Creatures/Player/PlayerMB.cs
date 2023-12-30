using UnityEngine;

public class PlayerMB : CreatureMB
{
	#region Singleton

	public static PlayerMB instance;

	private void Awake()
	{
		instance = this;
	}
	#endregion

	public Transform playerCameraRoot;
	public GameObject playerCapsuleGO;
	public Transform monsterFocusPoint;
	public GameObject playerBody;
	public GameObject killLantern;
	public Transform weightPuzzleRespawnPos;
}
