using UnityEngine;

public enum TypeOfCreature
{
	Player,
	Monster
}

public class Creature : MonoBehaviour
{
	public TypeOfCreature TypeOfCreature;
}
