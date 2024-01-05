using Inventory;

public class HiddenSlotsMB : ParentSlotsMB
{
	private new void Start()
	{
		base.Start();
		CreateSlots();
		FillSlots();
	}

	protected override void FillSlots()
	{
		for (int i = 0; i < slots.Length; i++)
		{
			slots[i] = new InventorySlot { };
		}
	}

	public override InventorySlot FillEmptySlot(ItemObject itemObject, int amount)
	{
		foreach (var slot in slots)
		{
			if(slot.ItemObject == null)
			{
				slot.UpdateSlot(itemObject, amount);
				return slot;
			}
		}
		//negeer item als de inventory vol is.
		return null;
	}
}
