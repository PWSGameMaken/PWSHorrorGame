using UnityEngine;

public static class RespawnSystemMB
{
	public static void Respawn(Transform[] creaturesToRespawn)
	{
		foreach (var creature in creaturesToRespawn)
		{
			Respawn(creature);
		}
	}

	public static void Respawn(Transform creatureToRespawn)
	{	
		Transform[] spawnPoints = creatureToRespawn.GetComponent<CreatureMB>().respawnPoints;

		Transform spawnPoint = GetClosestRespawnPoint(creatureToRespawn, spawnPoints);
		Respawn(creatureToRespawn, spawnPoint);
	}

	public static void Respawn(Transform CreatureToRespawn, Transform spawnPos)
	{
		CreatureToRespawn.position = spawnPos.position;
	}

	private static Transform GetClosestRespawnPoint(Transform CreatureToRespawn, Transform[] arrayToCheck)
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
