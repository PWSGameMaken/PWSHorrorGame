using UnityEngine;

public class PlayerManagerMB : MonoBehaviour
{
	#region Singleton

	public static PlayerManagerMB instance;

	private void Awake()
	{
		instance = this;
	}
	#endregion

	public GameObject player;
}
