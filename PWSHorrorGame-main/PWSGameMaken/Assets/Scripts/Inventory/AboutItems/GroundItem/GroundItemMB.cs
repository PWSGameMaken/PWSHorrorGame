/*
* Grobros
* https://github.com/GroBro-s
*/

using System.Collections;
using UnityEngine;

public enum GroundItemSounds
{
	DropSound
}

public class GroundItemMB : InteractableObjectMB, IInteractWithSlot
{
	[SerializeField] private ItemSO _itemSO;

	private static Transform _player;

	private static readonly int _throwPower = 100;
	private bool isMoving = false;

	public ItemSO ItemSO { get => _itemSO; set => _itemSO = value; }

	private void Start()
	{
		_player = GameObject.FindGameObjectWithTag("Player").transform;
	}

	private void OnCollisionEnter(Collision collision)
	{
		AudioManager.instance.PlayOneShot(GroundItemSounds.DropSound.ToString(), gameObject);
	}

	public void Interact(VisibleSlotsMB visibleSlotsMB)
	{
		if (!isMoving && AddToSlot(visibleSlotsMB)) { Destroy(); }
	}

	private bool AddToSlot(VisibleSlotsMB visibleSlotsMB)
	{
		var itemObject = new ItemObject(ItemSO);
		
		return visibleSlotsMB.AddItem(itemObject);
	}

	private void Destroy()
	{
		isMoving = true;
		
		if (TryGetComponent(out EarthQuakeMB earthQuakeMB))
		{
			earthQuakeMB.EarthQuake();
		}

		var destroyDelay = earthQuakeMB != null ? earthQuakeMB.ShakeTime / 2 : 0;
		StartCoroutine(DestroyExec(destroyDelay));
	}

	public static GameObject Create(ItemSO itemSO)
	{
		Vector3 forward = _player.TransformDirection(Vector3.forward);

		var newGroundItem = Instantiate(itemSO.groundItemPrefab, GetSpawnPosition() + forward * 0.75f, _player.rotation * itemSO.groundItemPrefab.transform.rotation);
		newGroundItem.GetComponent<GroundItemMB>().ItemSO = itemSO;

		newGroundItem.GetComponent<Rigidbody>().AddForce(forward * _throwPower);

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

#region Geluiden niet onnodig afsplen (werkt niet in begin) (Zit in OnCollisionEnter)
//private Rigidbody _rb;
//_rb = GetComponent<Rigidbody>();
//Alle geluiden spelen in het begin nog tegelijkertijd af tot één grote boem!
//if (_rb.velocity.magnitude > 0.1f)
//{
//}
#endregion