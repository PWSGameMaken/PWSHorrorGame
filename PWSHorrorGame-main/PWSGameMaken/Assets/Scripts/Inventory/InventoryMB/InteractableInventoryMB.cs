/*
* Grobros
* https://github.com/GroBro-s
*/

using System.Collections;
using UnityEngine;

namespace Inventory
{
	public class InteractableInventoryMB : ParentInventoryMB
	{
		#region variables
		[Header("Interaction With Items")]
		[SerializeField] private Transform _firePoint;
		[SerializeField] private float _interactionDistanceLimit;
		private bool _isMoving = false;
		#endregion

		private void Update()
		{
			ScrollSlots();
			if (Input.GetKeyDown(KeyCode.E)) Interact();
			if (Input.GetKeyDown(KeyCode.Q)) inventoryUI.GetComponent<ParentSlotsMB>().DropItems();
		}

		private void ScrollSlots()
		{
			var scrollDelta = (int)Input.mouseScrollDelta.y;
			var slotsMB = inventoryUI.GetComponent<ParentSlotsMB>();

			if (scrollDelta != 0)
			{
				slotsMB.ChangeSelectedSlot(scrollDelta);
			}
		}

		private void Interact()
		{
			RaycastHit hit;

			if(Physics.Raycast(_firePoint.position , _firePoint.transform.forward , out hit, _interactionDistanceLimit))
			{
				//Deze lijn zie je nu niet aangezien deze functie maar 1 frame wordt aangeroepen.
				Debug.DrawRay(_firePoint.position, _firePoint.transform.forward * hit.distance, Color.yellow);
			}

			var collidedGO = hit.transform?.gameObject;

			//Hier is een 'dubbele' check, want als je de != null weghaalt zul je een error
			//krijgen als je 'de leegte' probeert op te pakken.
			if(collidedGO != null)
			{
				if (collidedGO.TryGetComponent<GroundItemMB>(out var groundItem))
				{
					MoveGroundItemToInventorySlot(collidedGO);
				}

				else if (collidedGO.TryGetComponent<CollectionPoint>(out var collectionPoint))
				{
					var collidedparentSlotsMB = collidedGO.GetComponent<UninteractableInventoryMB>().inventoryUI.GetComponent<ParentSlotsMB>();
					
					var parentSlotsMB = inventoryUI.GetComponent<ParentSlotsMB>();
					var selectedItemSO = parentSlotsMB.slots[parentSlotsMB.slotIndex].ItemObject.Item.ItemSO;

					if(collectionPoint.CanAddItemToCollectionPoint(selectedItemSO))
					{
						MoveItem(collidedparentSlotsMB);
					}
				}
			}
		}

		private void MoveGroundItemToInventorySlot(GameObject groundItem)
		{
			//voor nu is amount altijd hetzelfde want ieder in-game item komt overeen met 1 inventory-item.
			//als dit niet meer het geval is moet dit systeem aangepast worden.
			if (_isMoving) return;

			var itemSO = groundItem.GetComponent<GroundItemMB>().itemSO;

			var itemObject = new ItemObject(itemSO);

			var isMoved = inventoryUI.GetComponent<ParentSlotsMB>().AddItem(itemObject);

			if (isMoved)
			{
				groundItem.TryGetComponent<EarthQuakeMB>(out var earthQuake);
				DestroyGroundItem(groundItem, earthQuake);
				_isMoving = true;
			}
		}

		private void DestroyGroundItem(GameObject groundItem, EarthQuakeMB earthQuakeMB)
		{
			earthQuakeMB.EarthQuake();
			DestroyGroundItem(groundItem, earthQuakeMB.shakeTime/2);
		}
		private void DestroyGroundItem(GameObject groundItem, float delayTime = 0f)
		{
			StartCoroutine(DestroyExec(groundItem, delayTime));
		}

		private IEnumerator DestroyExec(GameObject groundItem, float delayTime)
		{
			yield return new WaitForSeconds(delayTime);

			if(groundItem.TryGetComponent<SpawnObjectsInScene>(out var spawnObjectsInSceneMB))
			{
				spawnObjectsInSceneMB.SpawnObjects();
			}
			if (groundItem.TryGetComponent<DespawnObjectsInScene>(out var despawnObjectsInSceneMB))
			{	
				despawnObjectsInSceneMB.DespawnObjects();
			}

			Destroy(groundItem);
			_isMoving = false;
		}

		private void MoveItem(ParentSlotsMB slotsToBeMoved)
		{	
			var parentSlotsMB = inventoryUI.GetComponent<ParentSlotsMB>();
			var selectedSlot = parentSlotsMB.selectedSlot;

			var isMoved = slotsToBeMoved.AddItem(selectedSlot.ItemObject);

			if(isMoved) parentSlotsMB.ClearSelectedSlot();
		}
	}
}