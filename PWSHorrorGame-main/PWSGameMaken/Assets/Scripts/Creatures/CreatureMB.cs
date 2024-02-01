using UnityEngine;

public class CreatureMB : MonoBehaviour
{
	[SerializeField] private Transform[] respawnPoints;

	public Transform[] RespawnPoints { get => respawnPoints; }
	protected AnimMB AnimMB { get; private set; }
	protected AudioManager AudioManager { get; private set; }

	protected void Start()
	{
		AnimMB = GetComponentInChildren<AnimMB>();
		AudioManager = AudioManager.instance;
	}
}
