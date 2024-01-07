using UnityEngine;

public class RespawnSystemMB : MonoBehaviour
{
	#region Singleton

	public static RespawnSystemMB instance;

	private void Awake()
	{
		instance = this;
	}
	#endregion

	public void RespawnFromMonsterCollision(Transform[] creaturesToRespawn)
	{
		foreach (var creature in creaturesToRespawn)
		{
			RespawnFromMonsterCollision(creature);
		}
	}

	public void RespawnFromMonsterCollision(Transform creatureToRespawn)
	{	
		Transform[] spawnPoints = creatureToRespawn.GetComponent<CreatureMB>().respawnPoints;

		Transform spawnPoint = GetClosestRespawnPoint(creatureToRespawn, spawnPoints);
		Respawn(creatureToRespawn, spawnPoint);
	}

	public void RespawnFromWeightPuzzle(Transform CreatureToRespawn)
	{
		Transform respawnPoint = CreatureToRespawn.GetComponent<PlayerMB>().weightPuzzleRespawnPos;
		Respawn(CreatureToRespawn, respawnPoint);
	}

	public void Respawn(Transform CreatureToRespawn, Transform spawnPos)
	{
		CreatureToRespawn.position = spawnPos.position;
	}

	private Transform GetClosestRespawnPoint(Transform CreatureToRespawn, Transform[] arrayToCheck)
	{
		float minSpawnDistance = 10000f;
		Transform preferredSpawnPoint = null;

		foreach (Transform spawnPos in arrayToCheck)
		{
			var distanceToSpawnPoint = Vector3.Distance(CreatureToRespawn.position, spawnPos.position);

			if (distanceToSpawnPoint < minSpawnDistance)
			{
				minSpawnDistance = distanceToSpawnPoint;
				preferredSpawnPoint = spawnPos;
			}
		}
		return preferredSpawnPoint;
	}
}
