/*
* Grobros
* https://github.com/GroBro-s
*/

using Inventory;
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
	}
}
public static class ExtensionMethods
{
	//public static void UpdateSlotsDisplay(this Dictionary<GameObject, InventorySlot> _slotsOnInterface)
	//{
	//	foreach (KeyValuePair<GameObject, InventorySlot> _slot in _slotsOnInterface)
	//	{
	//		var slot = _slot.Value;
	//		slot.UpdateSlotDisplay();
	//	}
	//}

	public static void UpdateSlotDisplay(this InventorySlot slot)
	{
		var slotImage = slot.slotGO.transform.GetChild(0).GetComponentInChildren<Image>();
		var slotText = slot.slotGO.GetComponentInChildren<TextMeshProUGUI>();

		if ((slot.ItemObject?.Item.Id ?? -1) >= 0)
		{
			slotImage.sprite = slot.ItemObject.Item.Sprite;
			slotImage.color = new Color(1, 1, 1, 1);
			slotText.text = slot.amount == 1 ? "" : slot.amount.ToString("n0");
		}
		else
		{
			slotImage.sprite = null;
			slotImage.color = new Color(0, 0, 0, 0);
			slotText.text = "";
		}
	}
}
