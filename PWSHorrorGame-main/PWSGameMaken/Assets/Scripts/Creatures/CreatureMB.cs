using UnityEngine;

public enum TypeOfCreature
{
	Player,
	Monster
}

public class CreatureMB : MonoBehaviour
{
	[SerializeField] private Transform[] respawnPoints;
	private TypeOfCreature typeOfCreature;

	public Transform[] RespawnPoints { get => respawnPoints; }
	protected TypeOfCreature TypeOfCreature { set => typeOfCreature = value; }

	//[HideInInspector] public GameObject creatureGO;
	//private void Start()
	//{
	//	creatureGO = gameObject;
	//}
}
