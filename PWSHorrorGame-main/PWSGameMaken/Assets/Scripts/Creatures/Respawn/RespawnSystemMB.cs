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

	public void RespawnFromMonsterCollision(Transform CreatureToRespawn)
	{	
		Transform[] spawnPoints = CreatureToRespawn.GetComponent<CreatureMB>().respawnPoints;

		Transform preferredSpawnPoint = GetClosestRespawnPoint(CreatureToRespawn, spawnPoints);
		Respawn(CreatureToRespawn, preferredSpawnPoint);
	}

	public void RespawnFromWeightPuzzle(Transform CreatureToRespawn)
	{
		Transform respawnPoint = CreatureToRespawn.GetComponent<PlayerMB>().weightPuzzleRespawnPos;
		Respawn(CreatureToRespawn, respawnPoint);
	}

	private void Respawn(Transform CreatureToRespawn, Transform spawnPos)
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
