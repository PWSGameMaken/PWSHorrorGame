using UnityEngine;

public enum TypeOfCreature
{
	Player,
	Monster
}

public class CreatureMB : MonoBehaviour
{
	public Transform[] respawnPoints;
	public TypeOfCreature TypeOfCreature;
}
