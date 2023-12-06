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
	[SerializeField] private Animator _Anim;
	
	[SerializeField] private MovementAnimationMB _movementAnimation;

	protected Dictionary<GameObject, InventorySlot> slots_dict = new();
	#endregion

	#region Unity Methods
	protected void Start()
	{
		CreateSlots();
		FillSlots();

		SetSelectedSlot();
	}

	private void SetSelectedSlot()
	{
		selectedSlot = slots[slotIndex];
		selectedSlot.slotGO.GetComponent<Image>().color = _selectedSlotColor;
	}

	public void DropItems()
	{
		CreateGroundItems(selectedSlot);

		_movementAnimation.ChangeHandAnimationState(selectedSlot.ItemObject.Item.ItemSO.animTag, false);
		_objectInHandMB.Despawn();

		slots[slotIndex].ClearSlot();	
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

		if ((slots_dict[draggedSlot].ItemObject?.Item.Id ?? -1) >= 0)
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
				CreateGroundItems(slot);

				slot.ClearSlot();
				return;
			}

			if (MouseObject.slotHoveredOver)
			{
				SwapItems(slot);
			}
		}
	}

	private void CreateGroundItems(InventorySlot slot)
	{
		for (int i = 0; i < slot.amount; i++)
		{
			var itemSO = slot.ItemObject.Item.ItemSO;
			GroundItemMB.Create(itemSO);
		}
	}

	private void OnDrag(GameObject draggedSlot)
	{
		_dragging = true;
		var tempItemBeingDragged = TempItem.item;

		if (tempItemBeingDragged != null)
		{
			var mousePosition = MouseObject.GetPosition();

			TempItem.Move(mousePosition);
		}
	}

	protected GameObject AddEvents(GameObject slot)
	{
		AddEvent(slot, EventTriggerType.PointerEnter, delegate { OnEnter(slot); });
		AddEvent(slot, EventTriggerType.PointerExit, delegate { OnExit(slot); });
		AddEvent(slot, EventTriggerType.BeginDrag, delegate { OnDragStart(slot); });
		AddEvent(slot, EventTriggerType.EndDrag, delegate { OnDragEnd(slot); });
		AddEvent(slot, EventTriggerType.Drag, delegate { OnDrag(slot); });

		return slot;
	}

	public override InventorySlot FillEmptySlot(ItemObject itemObject, int amount)
	{
		for (int i = 0; i < slots.Length; i++)
		{
			if (slots[i].ItemObject == null)
			{
				slots[i].UpdateSlot(itemObject, amount);
				if (slots[i] == selectedSlot)
				{
					_objectInHandMB.Spawn(slots[i].ItemObject.Item.ItemSO);
					_movementAnimation.ChangeHandAnimationState(slots[i].ItemObject.Item.ItemSO.animTag, true);
				}
				return slots[i];
			}
		}
		//negeer item als de inventory vol is.
		return null;
	}

	protected void OnSlotUpdate(InventorySlot slot)
	{
		slot.UpdateSlotDisplay();
	}

	private void SwapSlots(InventorySlot slot1, InventorySlot slot2)
	{
		var item1 = slot1.ItemObject.Item;
		var item2 = slot2.ItemObject?.Item;

		if (item1.Id == item2?.Id && slot1 != slot2)
		{
			var isStackable = item1.Stackable;

			if (isStackable)
			{
				MergeStackableSlots(slot1, slot2);
			}
			else
			{
				SwapUnstackableSlots(slot1, slot2);
			}
		}
		else
		{
			SwapUnstackableSlots(slot1, slot2);
		}
	}

	private void SwapItems(InventorySlot slot1)
	{
		InventorySlot slot2 = MouseObject.visibleSlotsMB.slots_dict[MouseObject.slotHoveredOver];

		SwapSlots(slot1, slot2);
	}

	private void MergeStackableSlots(InventorySlot slot1, InventorySlot slot2)
	{
		slot1.AddAmount(slot2.amount);
		slot2.ClearSlot();
	}

	private void SwapUnstackableSlots(InventorySlot slot1, InventorySlot slot2)
	{
		InventorySlot temp = new InventorySlot(slot2.ItemObject, slot2.amount);
		slot2.UpdateSlot(slot1.ItemObject, slot1.amount);
		slot1.UpdateSlot(temp.ItemObject, temp.amount);
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

		if (selectedSlot.ItemObject != null)
		{
			_movementAnimation.ChangeHandAnimationState(selectedSlot.ItemObject.Item.ItemSO.animTag, false);
		}

		_objectInHandMB.Despawn();

		selectedSlot.slotGO.GetComponent<Image>().color = _slotColor;
		selectedSlot = slots[slotIndex];
		selectedSlot.slotGO.GetComponent<Image>().color = _selectedSlotColor;

		if (selectedSlot.ItemObject != null)
		{
			_objectInHandMB.Spawn(selectedSlot.ItemObject.Item.ItemSO);

			_movementAnimation.ChangeHandAnimationState(selectedSlot.ItemObject.Item.ItemSO.animTag, true);
		}
	}

	public void ClearSelectedSlot()
	{
		slots[slotIndex].ClearSlot();
		_objectInHandMB.Despawn();
	}
	#endregion
}

public static class ExtensionMethods
{
	public static void UpdateSlotsDisplay(this Dictionary<GameObject, InventorySlot> _slotsOnInterface)
	{
		foreach (KeyValuePair<GameObject, InventorySlot> _slot in _slotsOnInterface)
		{
			var slot = _slot.Value;
			slot.UpdateSlotDisplay();
		}
	}

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