using Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionPoint : MonoBehaviour
{
    public ItemSO[] acceptedItemSO;
    public bool isFull = false;
    private ParentSlotsMB parentSlotsMB;

	private void Start()
	{
		parentSlotsMB = GetComponent<UninteractableInventoryMB>().inventoryUI.GetComponent<ParentSlotsMB>();
	}

	private void Update()
	{
        if(parentSlotsMB.CountEmptySlots() ==  0) isFull = true;
		if(isFull)
        {
            ObjectiveCompleted();
        }
	}

	public bool CanAddItemToCollectionPoint(ItemSO itemSO)
    {
        for (int i = 0; i < acceptedItemSO.Length; i++)
        {
            if (acceptedItemSO[i] == itemSO)
            {
                return true;
            }
        }
        return false;
    }

    private void ObjectiveCompleted()
    {
        print("Now the door needs to Despawn");
    }
}
