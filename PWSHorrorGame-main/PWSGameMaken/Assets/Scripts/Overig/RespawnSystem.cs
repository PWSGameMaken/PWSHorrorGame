using UnityEngine;

public class RespawnSystem : MonoBehaviour
{
	#region Singleton

	public static RespawnSystem instance;

	private void Awake()
	{
		instance = this;
	}
	#endregion

	public GameObject player;
	public Transform weightPuzzleRespawnPos;
	public Transform[] PlayerRespawnPos;
	public Transform[] MonsterRespawnPos;

	private float minSpawnDistance = 10000f;
	private Transform preferredSpawnPoint;

	public void RespawnFromMonsterCollision(GameObject objectToSpawn)
	{
		GetClosestSpawnPoint(objectToSpawn);
		Respawn(objectToSpawn, preferredSpawnPoint);
	}

	public void RespawnFromWeightPuzzle()
	{
		Respawn(player, weightPuzzleRespawnPos);
	}

	private void Respawn(GameObject objectToSpawn, Transform spawnPos)
	{
		objectToSpawn.transform.position = spawnPos.position;
	}

	private void GetClosestSpawnPoint(GameObject objectToSpawn, Transform[] spawnPositions = null)
	{
		var typeOfCreature = objectToSpawn.GetComponent<Creature>().TypeOfCreature;
        if (typeOfCreature == TypeOfCreature.Player)
        {
			GetPointFromArray(objectToSpawn, PlayerRespawnPos);
        }
		else if (typeOfCreature == TypeOfCreature.Monster)
		{
			GetPointFromArray(objectToSpawn, MonsterRespawnPos);
		}
	}

	private void GetPointFromArray(GameObject objectToSpawn, Transform[] arrayToCheck)
	{
		foreach (Transform spawnPos in arrayToCheck)
		{
			var distanceToSpawn = Vector3.Distance(objectToSpawn.transform.position, spawnPos.position);

			if (distanceToSpawn < minSpawnDistance)
			{
				minSpawnDistance = distanceToSpawn;
				preferredSpawnPoint = spawnPos;
			}
		}
	}
}
