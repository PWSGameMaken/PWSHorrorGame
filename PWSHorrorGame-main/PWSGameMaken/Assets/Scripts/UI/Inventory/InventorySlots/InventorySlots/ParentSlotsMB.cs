/*
* Grobros
* https://github.com/GroBro-s/MorbidMarshmallow
*/

using Inventory;
using UnityEngine;

public abstract class ParentSlotsMB : UserInterfaceMB
{
	#region Variables
	[SerializeField] protected int _slotAmount = 1;
	protected ItemDatabaseSO itemDatabaseSO;
	
	[System.NonSerialized] public InventorySlot[] slots;
	#endregion

	#region Unity Methods
	protected void Start()
	{
		itemDatabaseSO = GameStatsMB.instance.itemDatabaseSO;
	}

	public abstract InventorySlot FillEmptySlot(ItemObject itemObject, int amount);

	public bool AddItem(ItemObject itemObject, int amount = 1)
	{
		var itemId_object = itemObject.Item.Id;
		InventorySlot slot = FindItemOnInventory(itemId_object);
		var stackableItem = itemDatabaseSO.ItemSOlist[itemId_object].stackable;

		if ((!stackableItem || (stackableItem && slot == null)) && HasEmptySlots())
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

	public bool HasEmptySlots()
	{
		foreach (var slot in slots)
		{
			if(slot.ItemObject == null)
			{
				return true;
			}
		}
		return false;
	}

	protected void CreateSlots()
	{
		slots = new InventorySlot[_slotAmount];
	}

	private InventorySlot FindItemOnInventory(int itemId_object)
	{
		foreach (var slot in slots)
		{
			var itemId_slot = slot.ItemObject?.Item.Id;
			if (itemId_slot == itemId_object) // slot moet null zijn maar de editor laat 0 zien. alsnog wordt hij hier als ongelijk weergegeven, wat klopt. Wij hebben geen verklaring kan later errors opleveren.
			{
				return slot;
			}
		}
		return null;
	}

	protected void CreateGroundItems(InventorySlot slot)
	{
		for (int i = 0; i < slot.amount; i++)
		{
			var itemSO = slot.ItemObject.Item.ItemSO;
			GroundItemMB.Create(itemSO);
		}
	}

	public void ClearSlots()
	{
		foreach (var slot in slots)
		{
			slot.ClearSlot();
		}
	}
	protected abstract void FillSlots();
	#endregion
}

public delegate void SlotUpdated(InventorySlot _slot);