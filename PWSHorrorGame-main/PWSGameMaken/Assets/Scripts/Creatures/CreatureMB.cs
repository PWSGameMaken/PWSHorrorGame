using UnityEngine;

public enum TypeOfCreature
{
	Player,
	Monster
}

public class CreatureMB : MonoBehaviour
{
	public GameObject creatureGO;
	public Transform[] respawnPoints;
	public TypeOfCreature TypeOfCreature;

	private void Start()
	{
		creatureGO = gameObject;
	}
}
