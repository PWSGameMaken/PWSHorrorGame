using System.Collections;
using System.Collections.Generic;
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
	public Transform[] monsterRespawnPos;

	private float minSpawnDistance;
	private Transform preferredSpawnPoint;

	public void RespawnFromMonster()
	{
		GetClosestSpawnPoint();
		Respawn(preferredSpawnPoint);
	}

	private void RespawnFromWeightPuzzle()
	{
		Respawn(weightPuzzleRespawnPos);
	}

	private void Respawn(Transform spawnPos)
	{
		player.transform.position = spawnPos.position;
	}

	private void GetClosestSpawnPoint()
	{
		foreach (Transform spawnPos in monsterRespawnPos)
		{
			var distanceToSpawn = Vector3.Distance(player.transform.position, spawnPos.position);

			if (distanceToSpawn < minSpawnDistance)
			{
				minSpawnDistance = distanceToSpawn;
				preferredSpawnPoint = spawnPos;
			}
		}
	}
}
