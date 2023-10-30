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
		public GameObject InventoryUI;
		public Transform firePoint;
		public float interactionDistanceLimit;
		#endregion

		private void Update()
		{
			ScrollSlots();
			if(Input.GetKeyDown(KeyCode.E)) PickUpItem();
		}

		private void PickUpItem()
		{
			RaycastHit hit;

			if(Physics.Raycast(firePoint.position , firePoint.transform.forward , out hit, 3))
			{
				//Deze lijn zie je nu niet aangezien deze functie maar 1 frame wordt aangeroepen.
				Debug.DrawRay(firePoint.position, firePoint.transform.forward * hit.distance, Color.yellow);
			}

			var collidedGO = hit.transform?.gameObject;

			if (collidedGO.TryGetComponent<GroundItemMB>(out var groundItem))
			{
				MoveGroundItemToInventorySlot(collidedGO);
			}
		}

		private void ScrollSlots()
		{
			var scrollDelta = (int)Input.mouseScrollDelta.y;
			var slotsMB = InventoryUI.GetComponent<ParentSlotsMB>();

			if (scrollDelta != 0)
			{
				slotsMB.ChangeSelectedSlot(scrollDelta);
			}
		}

		private void MoveGroundItemToInventorySlot(GameObject groundItem)
		{
			//voor nu is amount altijd hetzelfde want ieder in-game item komt overeen met 1 inventory-item.
			//als dit niet meer het geval is moet dit systeem aangepast worden.
			var itemSO = groundItem.GetComponent<GroundItemMB>().itemSO;
			var itemObject = new ItemObject(itemSO);

			var isMoved = InventoryUI.GetComponent<DynamicSlotsMB>().AddItem(itemObject);

			if (isMoved) Destroy(groundItem);
		}
	}
}