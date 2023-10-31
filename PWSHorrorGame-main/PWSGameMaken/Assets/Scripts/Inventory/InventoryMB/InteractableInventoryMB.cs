/*
* Grobros
* https://github.com/GroBro-s
*/

using UnityEngine;

namespace Inventory
{
	public class InteractableInventoryMB : ParentInventoryMB
	{
		#region variables
		[Header("Interaction With Items")]
		[SerializeField] private Transform _firePoint;
		[SerializeField] private float _interactionDistanceLimit;
		#endregion

		private void Update()
		{
			ScrollSlots();
			if(Input.GetKeyDown(KeyCode.E)) PickUpItem();
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

		private void PickUpItem()
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
			}
		}

		private void MoveGroundItemToInventorySlot(GameObject groundItem)
		{
			//voor nu is amount altijd hetzelfde want ieder in-game item komt overeen met 1 inventory-item.
			//als dit niet meer het geval is moet dit systeem aangepast worden.
			var itemSO = groundItem.GetComponent<GroundItemMB>().itemSO;
			var itemObject = new ItemObject(itemSO);

			var isMoved = inventoryUI.GetComponent<DynamicSlotsMB>().AddItem(itemObject);

			if (isMoved) Destroy(groundItem);
		}
	}
}