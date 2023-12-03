/*
* Grobros
* https://github.com/GroBro-s/MorbidMarshmallow
*/

using Inventory;
using UnityEngine;

public abstract class ParentSlotsMB : UserInterfaceMB
{
	#region Variables
	[SerializeField] protected int _slotAmount = 20;
	[SerializeField] protected ItemDatabaseSO itemDatabaseSO;
	
	public InventorySlot[] slots;
	#endregion

	#region Unity Methods
	public abstract InventorySlot FillEmptySlot(ItemObject itemObject, int amount);

	public bool AddItem(ItemObject itemObject, int amount = 1)
	{
		var itemId_object = itemObject.Item.Id;
		InventorySlot slot = FindItemOnInventory(itemId_object);
		var stackableItem = itemDatabaseSO.ItemSOlist[itemId_object].stackable;

		if ((!stackableItem || (stackableItem && slot == null)) && CountEmptySlots() > 0)
		{
			FillEmptySlot(itemObject, amount);
			return true;
		}
		else if (stackableItem && slot != null)  //als er ooit een maximum op het aantal stackable objecten komt moet hier de voorwaarde aangepast worden.
		{
			slot.AddAmount(amount);
			return true;
		}
		return false;
	}

	public int CountEmptySlots()
	{
		var emptySlots = 0;
		for (int i = 0; i < slots.Length; i++)
		{
			if (slots[i].ItemObject == null)
			{
				emptySlots++;
			}
		}
		return emptySlots;
	}

	protected void CreateSlots()
	{
		slots = new InventorySlot[_slotAmount];
	}

	private InventorySlot FindItemOnInventory(int itemId_object)
	{
		for (int i = 0; i < slots.Length; i++)
		{
			var itemId_slot = slots[i].ItemObject?.Item.Id;
			if (itemId_slot == itemId_object) // slot moet null zijn maar de editor laat 0 zien. alsnog wordt hij hier als ongelijk weergegeven, wat klopt. Wij hebben geen verklaring kan later errors opleveren.
			{
				return slots[i];
			}
		}
		return null;
	}

	public void RemoveItem(ItemObject itemObject)
	{
		for (int i = 0; i < slots.Length; i++)
		{
			if (slots[i].ItemObject == itemObject)
			{
				slots[i].ClearSlot();
			}
		}
	}

	public void ClearSlots()
	{
		for (int i = 0; i < slots.Length; i++)
		{
			slots[i].ClearSlot();
		}
	}

	protected abstract void FillSlots();
	#endregion
}

public delegate void SlotUpdated(InventorySlot _slot);