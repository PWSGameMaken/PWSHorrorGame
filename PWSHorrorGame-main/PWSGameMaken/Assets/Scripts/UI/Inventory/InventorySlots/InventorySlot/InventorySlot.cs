/*
* Grobros
* https://github.com/GroBro-s
*/

using UnityEngine;

namespace Inventory
{
	[System.Serializable]
	public class InventorySlot
	{
		[System.NonSerialized] public GameObject slotGO;
		[System.NonSerialized] public SlotUpdated OnAfterUpdate;
		[System.NonSerialized] public SlotUpdated OnBeforeUpdate;
		[System.NonSerialized] public int amount;
		public ItemObject ItemObject { get; set; }

		public InventorySlot() { }

		public InventorySlot(ItemObject itemObject, int amount)
		{
			UpdateSlot(itemObject, amount);
		}

		public void UpdateSlot(ItemObject itemObject, int amount)
		{
			OnBeforeUpdate?.Invoke(this);

			ItemObject = itemObject;
			this.amount = amount;

			OnAfterUpdate?.Invoke(this);
			//kan beter?
		}

		public void ClearSlot()
		{
			UpdateSlot(null, 0);
		}

		public void AddAmount(int value)
		{
			UpdateSlot(ItemObject, amount += value);
		}
	}
}