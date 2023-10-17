/*
* Grobros
* https://github.com/GroBro-s
*/

using UnityEngine;

namespace Inventory
{
	//De parentinventory moet worden opgedeelt in 2 groepen van inventories die grounditems op kunnen pakken en
	//inventories die dat niet kunnen.
	public class InteractableInventoryMB : ParentInventoryMB
	{
		#region variables
		public GameObject InventoryUI;
		#endregion

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