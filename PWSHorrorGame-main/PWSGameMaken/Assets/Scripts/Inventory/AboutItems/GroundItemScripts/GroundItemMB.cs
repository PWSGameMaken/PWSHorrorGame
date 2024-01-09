/*
* Grobros
* https://github.com/GroBro-s
*/

using System.Collections;
using UnityEngine;

public class GroundItemMB : InteractableObjectMB, IInteractWithSlot
{
	//private static Transform _collectables;
	public ItemSO itemSO;
	private static readonly int _power = 100;
	private bool isMoving = false;
	private static Transform _player;

	private void Start()
	{
		_player = GameObject.FindGameObjectWithTag("Player").transform;
		//_collectables = GameStatsMB.instance.collectables;
	}

	public void Interact(VisibleSlotsMB visibleSlotsMB)
	{
		if (!isMoving && AddToSlot(visibleSlotsMB)) { Destroy(); }
	}

	private bool AddToSlot(VisibleSlotsMB visibleSlotsMB)
	{
		var itemObject = new ItemObject(itemSO);
		
		return visibleSlotsMB.AddItem(itemObject);
	}

	private void Destroy()
	{
		isMoving = true;
		
		if (TryGetComponent(out EarthQuakeMB earthQuakeMB))
		{
			earthQuakeMB.EarthQuake();
		}

		var destroyDelay = earthQuakeMB != null ? earthQuakeMB.shakeTime / 2 : 0;
		StartCoroutine(DestroyExec(destroyDelay));
	}

	public static GameObject Create(ItemSO itemSO)
	{
		Vector3 forward = _player.TransformDirection(Vector3.forward);

		var newGroundItem = Instantiate(itemSO.groundItemPrefab, GetSpawnPosition() + forward * 0.75f, _player.rotation * itemSO.groundItemPrefab.transform.rotation);
		newGroundItem.GetComponent<GroundItemMB>().itemSO = itemSO;

		newGroundItem.GetComponent<Rigidbody>().AddForce(forward * _power);

		return newGroundItem;
	}

	private static Vector3 GetSpawnPosition()
	{
		var playerPos = _player.position;

		return new Vector3(playerPos.x, playerPos.y + 1.8f, playerPos.z);
	}

	private IEnumerator DestroyExec(float destroyDelay)
	{
		if (destroyDelay > 0) { GetComponent<MeshRenderer>().enabled = false; }

		yield return new WaitForSeconds(destroyDelay);

		ObjectiveCompleted();
		Destroy(gameObject);
	}
}