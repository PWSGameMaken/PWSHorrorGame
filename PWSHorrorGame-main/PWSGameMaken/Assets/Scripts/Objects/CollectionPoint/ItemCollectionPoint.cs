using UnityEngine;

public class ItemCollectionPoint : CollectionPointMB, IInteractWithSlot
{
	private HiddenSlotsMB _hiddenSlotsMB;
	[SerializeField] private ItemSO[] _acceptedItemSO;

	private void Start()
	{
		_hiddenSlotsMB = GetComponent<HiddenSlotsMB>();
	}

	public void CheckForCompletion()
	{
		if (_hiddenSlotsMB.CountEmptySlots() == 0)
		{
			ObjectiveCompleted();
		}
	}

	public void Interact(VisibleSlotsMB visibleSlotsMB)
	{
		var selectedItemSO = visibleSlotsMB.selectedSlot.ItemObject?.Item.ItemSO;

		if (selectedItemSO == null) return;

		if (CanAddItem(selectedItemSO))
		{
			MoveItemToCollectionPoint(visibleSlotsMB);
			CheckForCompletion();
		}
	}

	private void MoveItemToCollectionPoint(VisibleSlotsMB visibleSlotsMB)
	{
		var selectedSlot = visibleSlotsMB.selectedSlot;

		var collectionPointSlots = GetComponent<HiddenSlotsMB>();
		var isMoved = collectionPointSlots.AddItem(selectedSlot.ItemObject);

		if (isMoved)
		{
			visibleSlotsMB.ClearSelectedSlot();
		}
	}

	public bool CanAddItem(ItemSO itemSO)
	{
		for (int i = 0; i < _acceptedItemSO.Length; i++)
		{
			if (_acceptedItemSO[i] == itemSO)
			{
				return true;
			}
		}
		return false;
	}
}
