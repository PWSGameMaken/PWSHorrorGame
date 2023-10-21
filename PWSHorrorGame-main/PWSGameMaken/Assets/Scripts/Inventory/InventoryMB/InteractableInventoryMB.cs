/*
* Grobros
* https://github.com/GroBro-s
*/

using System;
using UnityEngine;

namespace Inventory
{
	public class InteractableInventoryMB : ParentInventoryMB
	{
		#region variables
		public GameObject InventoryUI;
		#endregion

		private void Update()
		{
			var scrollDelta = (int)Input.mouseScrollDelta.y;
			var slotsMB = InventoryUI.GetComponent<ParentSlotsMB>();
			print(scrollDelta);
			if(scrollDelta != 0 )
			{
				slotsMB.ChangeSelectedSlot(scrollDelta);
			}
		}

		protected override void OnTriggerEnter(Collider collision)
		{
			if (collision.TryGetComponent<GroundItemMB>(out var groundItem))
			{
				SetGroundItemToInventorySlot(collision, groundItem);
			}
		}

		private void SetGroundItemToInventorySlot(Collider collision, GroundItemMB groundItem) //TransferGroundItemToInventory
		{
			//voor nu is amount altijd hetzelfde want ieder in-game item komt overeen met 1 inventory-item.
			//als dit niet meer het geval is moet dit systeem aangepast worden.
			var itemObject = new ItemObject(groundItem.itemSO);
			if (InventoryUI.GetComponent<DynamicSlotsMB>().AddItem(itemObject))
			{
				Destroy(collision.gameObject);
			}
		}
	}
}