using Inventory;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class VisibleSlotsMB : ParentSlotsMB
{
	#region Variables
	private bool _dragging = false;

	[HideInInspector] public InventorySlot selectedSlot;
	[HideInInspector] public int slotIndex = 0;

	[SerializeField] private Color _selectedSlotColor;
	[SerializeField] private Color _slotColor;

	[SerializeField] private ObjectInHandMB _objectInHandMB;
	[SerializeField] private PlayerMovementAnimationMB _movementAnimation;
	protected Dictionary<GameObject, InventorySlot> slots_dict = new();
	#endregion

	#region Unity Methods
	protected new void Start()
	{
		base.Start();
		CreateSlots();
		FillSlots();

		SetSelectedSlot();
	}

	public void ChangeSelectedSlot(int scrollDelta)
	{
		switch (scrollDelta)
		{
			case -1:
				slotIndex = (slotIndex < (_slotAmount - 1)) ? slotIndex + 1 : 0;
				break;
			case 1:
				slotIndex = (slotIndex > 0) ? slotIndex - 1 : _slotAmount - 1;
				break;
			default:
				return;
		}

		if (selectedSlot.ItemObject != null) { SetActiveObjectInHand(false); }

		selectedSlot.slotGO.GetComponent<Image>().color = _slotColor;
		selectedSlot = slots[slotIndex];
		selectedSlot.slotGO.GetComponent<Image>().color = _selectedSlotColor;

		if (selectedSlot.ItemObject != null) { SetActiveObjectInHand(true); }
	}

	public void DropItems(InventorySlot slot)
	{
		if (slot == selectedSlot) { SetActiveObjectInHand(false); }

		CreateGroundItems(slot);
		slot.ClearSlot();
	}

	public override InventorySlot FillEmptySlot(ItemObject itemObject, int amount)
	{
		foreach (var slot in slots)
		{
			if (slot.ItemObject == null)
			{
				slot.UpdateSlot(itemObject, amount);

				if (slot == selectedSlot) { SetActiveObjectInHand(true); }

				return slot;
			}
		}
		//negeer item als de inventory vol is.
		return null;
	}

	public void SetActiveObjectInHand(bool activeState)
	{
		if (selectedSlot.ItemObject == null) { return; }

		var selectedItemSO = selectedSlot.ItemObject.Item.ItemSO;

		_movementAnimation.ChangeHandAnimationState(selectedItemSO.animTag, activeState);
		_objectInHandMB.SetActive(selectedItemSO, activeState);
	}

	private void SetSelectedSlot()
	{
		selectedSlot = slots[slotIndex];
		selectedSlot.slotGO.GetComponent<Image>().color = _selectedSlotColor;
	}

	private void OnEnter(GameObject slotGO)
	{
		MouseObject.OnEnterSlot(slotGO, this);

		if (!_dragging)
		{
			var slot = slots_dict[slotGO];
			var itemObject = slot?.ItemObject;

			if ((itemObject?.Item.Id ?? -1) >= 0)
			{
				var slotPos = slotGO.transform.position;

				Description.SetDescription(itemObject.Item, slotPos);
			}
		}
	}

	private void OnExit(GameObject slotGO)
	{
		MouseObject.OnExitSlot();

		if ((slots_dict[slotGO].ItemObject?.Item.Id ?? -1) >= 0)
		{
			Description.DeleteDescription();
		}
	}

	private void OnDragStart(GameObject draggedSlot)
	{
		var itemObject = slots_dict[draggedSlot].ItemObject;

		if ((itemObject?.Item.Id ?? -1) >= 0)
		{
			TempItem.SetSprite(itemObject.Item);
		}
	}

	private void OnDragEnd(GameObject draggedSlot)
	{
		var slot = slots_dict[draggedSlot];
		var itemObject = slot.ItemObject;
		_dragging = false;

		if (itemObject != null)
		{
			TempItem.ResetSprite();

			if (MouseObject.interfaceMouseIsOver == null && itemObject.Item.Id >= 0)
			{
				DropItems(slot);
				return;
			}

			if (MouseObject.slotHoveredOver)
			{
				MergeOrSwapSlots(slot);
			}
		}
	}

	private void OnDrag()
	{
		_dragging = true;
		var tempItemBeingDragged = TempItem.item;

		if (tempItemBeingDragged != null)
		{
			TempItem.Move();
		}
	}

	protected GameObject AddEvents(GameObject slot)
	{
		AddEvent(slot, EventTriggerType.PointerEnter, delegate { OnEnter(slot); });
		AddEvent(slot, EventTriggerType.PointerExit, delegate { OnExit(slot); });
		AddEvent(slot, EventTriggerType.BeginDrag, delegate { OnDragStart(slot); });
		AddEvent(slot, EventTriggerType.EndDrag, delegate { OnDragEnd(slot); });
		AddEvent(slot, EventTriggerType.Drag, delegate { OnDrag(); });

		return slot;
	}

	protected void OnSlotUpdate(InventorySlot slot)
	{
		slot.UpdateSlotDisplay();
	}

	private void MergeOrSwapSlots(InventorySlot slot1)
	{
		InventorySlot slot2 = MouseObject.visibleSlotsMB.slots_dict[MouseObject.slotHoveredOver];

		MergeOrSwapSlots(slot1, slot2);
	}

	private void MergeOrSwapSlots(InventorySlot slot1, InventorySlot slot2)
	{
		if (slot1 == slot2) return;

		var item1 = slot1.ItemObject.Item;
		var item2 = slot2.ItemObject?.Item;
		var stackable = item1.Stackable;
		var identicalItemId = item1.Id == item2?.Id;

		if (identicalItemId && stackable)
		{
			MergeSlots(slot1, slot2);

			return;
		}
		
		SwapSlots(slot1, slot2);
	}

	private void MergeSlots(InventorySlot slot1, InventorySlot slot2)
	{
		slot1.AddAmount(slot2.amount);
		slot2.ClearSlot();
	}

	private void SwapSlots(InventorySlot slot1, InventorySlot slot2)
	{
		InventorySlot temp = new InventorySlot(slot2.ItemObject, slot2.amount);
		slot2.UpdateSlot(slot1.ItemObject, slot1.amount);
		slot1.UpdateSlot(temp.ItemObject, temp.amount);
	}
	#endregion
}