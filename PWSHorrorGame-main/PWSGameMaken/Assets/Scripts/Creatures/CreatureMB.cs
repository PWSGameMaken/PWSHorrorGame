using UnityEngine;

public enum TypeOfCreature
{
	Player,
	Monster
}

public class CreatureMB : MonoBehaviour
{
	[HideInInspector] public GameObject creatureGO;
	public Transform[] respawnPoints;
	public TypeOfCreature typeOfCreature;

	private void Start()
	{
		creatureGO = gameObject;
	}
}
