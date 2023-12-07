/*
* Grobros
* https://github.com/GroBro-s
*/

using System.Collections;
using UnityEngine;

public class GroundItemMB : MonoBehaviour
{
	private static Transform _collectables;
	public ItemSO itemSO;
	private static readonly int _power = 100;
	public bool isMoving;

	private void Start()
	{
		var gameStatsMB = GameObject.Find("GameController").GetComponent<GameStatsMB>();
		_collectables = gameStatsMB.collectables;
	}

	public static GameObject Create(ItemSO itemSO)
	{
		var player = GameObject.FindGameObjectWithTag("Player");
		Vector3 forward = player.transform.TransformDirection(Vector3.forward);

		var newGroundItem = Instantiate(itemSO.groundItemPrefab, GetSpawnPosition() + forward * 0.75f, player.transform.rotation, _collectables); //itemSO.groundItemPrefab.transform.rotation
		newGroundItem.GetComponent<GroundItemMB>().itemSO = itemSO;


		newGroundItem.GetComponent<Rigidbody>().AddForce(forward * _power);

		return newGroundItem;
	}

	private static Vector3 GetSpawnPosition()
	{
		var player = GameObject.FindGameObjectWithTag("Player");
		var playerPos = player.transform.position;

		return new Vector3(playerPos.x, playerPos.y + 1.8f, playerPos.z);
	}

	public void DestroyGroundItem(GameObject groundItem, float destoryDelay)
	{
		groundItem.GetComponent<MeshRenderer>().enabled = false;
		StartCoroutine(DestroyExec(groundItem, destoryDelay));
	}

	private IEnumerator DestroyExec(GameObject groundItem, float delayTime)
	{
		yield return new WaitForSeconds(delayTime);

		if (groundItem.TryGetComponent<SpawnObjectsInScene>(out var spawnObjectsInSceneMB))
		{
			spawnObjectsInSceneMB.SpawnObjects();
		}
		if (groundItem.TryGetComponent<DespawnObjectsInScene>(out var despawnObjectsInSceneMB))
		{
			despawnObjectsInSceneMB.DespawnObjects();
		}

		Destroy(groundItem);
	}
}