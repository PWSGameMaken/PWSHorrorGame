/*
* Grobros
* https://github.com/GroBro-s
*/

using System.Collections;
using UnityEngine;

public class GroundItemMB : InteractableObjectMB
{
	private static Transform _collectables;
	public ItemSO itemSO;
	private static readonly int _power = 100;
	private bool isMoving = false;

	private void Start()
	{
		var gameStatsMB = GameObject.Find("GameController").GetComponent<GameStatsMB>();
		_collectables = gameStatsMB.collectables;
	}

	public override string GetHintUIText()
	{
		return itemSO.hintText;
	}

	public override void Interact(GameObject itemToInteract, VisibleSlotsMB visibleSlotsMB)
	{
		MoveGroundItemToInventorySlot(itemToInteract, visibleSlotsMB);
	}

	private void MoveGroundItemToInventorySlot(GameObject groundItem, VisibleSlotsMB visibleSlotsMB)
	{
		var groundItemMB = groundItem.GetComponent<GroundItemMB>();

		if (groundItemMB.isMoving == true) { return; }

		var itemObject = new ItemObject(groundItemMB.itemSO);
		var isAdded = visibleSlotsMB.AddItem(itemObject);

		if (isAdded)
		{
			groundItemMB.isMoving = true;
			if (groundItem.TryGetComponent(out EarthQuakeMB earthQuakeMB))
			{
				earthQuakeMB.EarthQuake();
			}

			var destroyDelay = earthQuakeMB != null ? earthQuakeMB.shakeTime / 2 : 0;
			groundItemMB.DestroyGroundItem(groundItem, destroyDelay);
		}
	}

	public static GameObject Create(ItemSO itemSO)
	{
		var player = GameObject.FindGameObjectWithTag("Player");
		Vector3 forward = player.transform.TransformDirection(Vector3.forward);

		var newGroundItem = Instantiate(itemSO.groundItemPrefab, GetSpawnPosition() + forward * 0.75f, player.transform.rotation * itemSO.groundItemPrefab.transform.rotation, _collectables); //itemSO.groundItemPrefab.transform.rotation
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

	public void DestroyGroundItem(GameObject groundItem, float destroyDelay)
	{
		if(destroyDelay > 0) { groundItem.GetComponent<MeshRenderer>().enabled = false; }

		StartCoroutine(DestroyExec(groundItem, destroyDelay));
	}

	private IEnumerator DestroyExec(GameObject groundItem, float delayTime)
	{
		yield return new WaitForSeconds(delayTime);

		if (groundItem.TryGetComponent<SpawnObjectsInSceneMB>(out var spawnObjectsInSceneMB))
		{
			spawnObjectsInSceneMB.SpawnObjects();
		}
		if (groundItem.TryGetComponent<DespawnObjectsInSceneMB>(out var despawnObjectsInSceneMB))
		{
			despawnObjectsInSceneMB.DespawnObjects();
		}

		Destroy(groundItem);
	}
}