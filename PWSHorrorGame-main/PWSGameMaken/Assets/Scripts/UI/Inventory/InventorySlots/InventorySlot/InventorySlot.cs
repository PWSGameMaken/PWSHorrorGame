/*
* Grobros
* https://github.com/GroBro-s
*/

using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
	[System.Serializable]
	public class InventorySlot
	{
		public GameObject slotGO;
		public SlotUpdated OnAfterUpdate;
		public SlotUpdated OnBeforeUpdate;
		public ItemObject ItemObject { get; set; }
		public int amount;

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

		public void UpdateSlotDisplay()
		{
			var slotImage = slotGO.transform.GetChild(0).GetComponentInChildren<Image>();
			var slotText = slotGO.GetComponentInChildren<TextMeshProUGUI>();

			if ((ItemObject?.Item.Id ?? -1) >= 0)
			{
				slotImage.sprite = ItemObject.Item.Sprite;
				slotImage.color = new Color(1, 1, 1, 1);
				slotText.text = amount == 1 ? "" : amount.ToString("n0");
			}
			else
			{
				slotImage.sprite = null;
				slotImage.color = new Color(0, 0, 0, 0);
				slotText.text = "";
			}
		}
	}
}
