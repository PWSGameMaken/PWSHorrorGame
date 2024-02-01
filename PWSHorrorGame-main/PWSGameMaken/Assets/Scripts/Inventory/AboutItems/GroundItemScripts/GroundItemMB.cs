/*
* Grobros
* https://github.com/GroBro-s
*/

using System.Collections;
using UnityEngine;

public class GroundItemMB : InteractableObjectMB, IInteractWithSlot
{
	[SerializeField] private ItemSO _itemSO;

	[SerializeField] private AudioClip _dropSound;
	private AudioSource _audioSource;

	private static Transform _player;
	private Rigidbody _rb;

	private static readonly int _power = 100;
	private bool isMoving = false;

	public ItemSO ItemSO { get => _itemSO; set => _itemSO = value; }

	private void Start()
	{
		_player = GameObject.FindGameObjectWithTag("Player").transform;
		_audioSource = GetComponent<AudioSource>();
		_rb = GetComponent<Rigidbody>();
	}

	private void OnCollisionEnter(Collision collision)
	{
		//Alle geluiden spelen in het begin nog tegelijkertijd af tot één grote boem!
		if (_rb.velocity.magnitude > 1)
		{
			_audioSource.PlayOneShot(_dropSound);
		}
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