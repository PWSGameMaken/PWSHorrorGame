using UnityEngine;

public class ItemCollectionPoint : CollectionPointMB, IInteractWithSlot
{
	private HiddenSlotsMB _hiddenSlotsMB;
	[SerializeField] private ItemSO[] _acceptedItemSO;
	private bool _isFull = false;

	private void Start()
	{
		_hiddenSlotsMB = GetComponent<HiddenSlotsMB>();
	}

	public void CheckForCompletion()
	{
		if (!_hiddenSlotsMB.HasEmptySlots())
		{
			ObjectiveCompleted();
			_isFull = true;
		}
	}

	public void Interact(VisibleSlotsMB visibleSlotsMB)
	{
		if (!_isFull) { CollectItems(visibleSlotsMB); }
		else if (_isFull) { GiveItems(visibleSlotsMB); }
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

	private void GiveItems(VisibleSlotsMB visibleSlotsMB)
	{
		foreach (var slot in _hiddenSlotsMB.slots)
		{
			_isFull = false;

			visibleSlotsMB.AddItem(slot.ItemObject, slot.amount);
			ObjectiveCompleted();
			slot.ClearSlot();
		}
	}

	private void MoveItemToCollectionPoint(VisibleSlotsMB visibleSlotsMB)
	{
		var selectedSlot = visibleSlotsMB.selectedSlot;

		var collectionPointSlots = GetComponent<HiddenSlotsMB>();
		var isMoved = collectionPointSlots.AddItem(selectedSlot.ItemObject, selectedSlot.amount);

		if (isMoved)
		{
			visibleSlotsMB.ClearSelectedSlot();
		}
	}

	public bool CanAddItem(ItemSO itemSO)
	{
		foreach (var acceptedItemSO in _acceptedItemSO)
		{
			if(acceptedItemSO == itemSO)
			{
				return true;
			}
		}
		return false;
	}
}
