using UnityEngine;

public class ItemCollectionPoint : CollectionPointMB, IInteractWithSlot
{
	private HiddenSlotsMB _hiddenSlotsMB;
	[SerializeField] private ItemSO[] _acceptedItemSO;

	private void Start()
	{
		_hiddenSlotsMB = GetComponent<HiddenSlotsMB>();
	}

	public void Interact(VisibleSlotsMB visibleSlotsMB)
	{
		if (!isFull) { CollectItems(visibleSlotsMB); }
		else if (isFull) { DropItems(visibleSlotsMB); }
	}

	private void CollectItems(VisibleSlotsMB visibleSlotsMB)
	{
		var selectedItemSO = visibleSlotsMB.selectedSlot.ItemObject?.Item.ItemSO;

		if (selectedItemSO == null) return;

		if (CanAddItem(selectedItemSO))
		{
			MoveItemToCollectionPoint(visibleSlotsMB);
			CheckForCompletion();
		}
	}

	private void DropItems(VisibleSlotsMB visibleSlotsMB)
	{
		isFull = false;
		var isMoved = visibleSlotsMB.AddItem(_hiddenSlotsMB.slots[0].ItemObject);

		if(isMoved)
		{
			_hiddenSlotsMB.ClearSlots();
			ObjectiveCompleted();
			ChangeHintText();
		}
	}

	private void MoveItemToCollectionPoint(VisibleSlotsMB visibleSlotsMB)
	{
		var selectedSlot = visibleSlotsMB.selectedSlot;

		var collectionPointSlots = GetComponent<HiddenSlotsMB>();
		var isMoved = collectionPointSlots.AddItem(selectedSlot.ItemObject);

		if (isMoved)
		{
			visibleSlotsMB.SetActiveObjectInHand(false);
			selectedSlot.ClearSlot();
		}
	}

	private bool CanAddItem(ItemSO itemSO)
	{
		foreach (var acceptedItemSO in _acceptedItemSO)
		{
			if (itemSO == acceptedItemSO)
			{
				return true;
			}
		}
		return false;
	}

	private void CheckForCompletion()
	{
		if (!_hiddenSlotsMB.HasEmptySlots())
		{
			isFull = true;
			ChangeHintText();
			ObjectiveCompleted();
		}
	}

}
