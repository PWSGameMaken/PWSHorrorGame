using Inventory;

public class HiddenSlotsMB : ParentSlotsMB
{
	private void Start()
	{
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
		for (int i = 0; i < slots.Length; i++)
		{
			if (slots[i].ItemObject == null)
			{
				slots[i].UpdateSlot(itemObject, amount);
				return slots[i];
			}
		}
		//negeer item als de inventory vol is.
		return null;
	}
}
