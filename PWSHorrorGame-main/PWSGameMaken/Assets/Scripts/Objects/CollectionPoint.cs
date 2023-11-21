using Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionPoint : MonoBehaviour
{
    [SerializeField] private GameObject[] itemsToSpawn;
    [SerializeField] private GameObject[] itemsToDespawn;

    private ParentSlotsMB _parentSlotsMB;

    public ItemSO[] acceptedItemSO;

    [HideInInspector] public bool isFull = false;
    [SerializeField] private bool canCollectItems;


	private void Start()
	{
        if(canCollectItems)
        {
		    _parentSlotsMB = GetComponent<UninteractableInventoryMB>().inventoryUI.GetComponent<ParentSlotsMB>();
        }

	}

	private void Update()
	{
        if (isFull == false && canCollectItems)
        {
            if (_parentSlotsMB.CountEmptySlots() == 0) isFull = true;
            if (isFull) ObjectiveCompleted();
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

    public void ObjectiveCompleted()
    {
        for (int i = 0; i < itemsToSpawn.Length; i++)
        {
            itemsToSpawn[i].SetActive(true);
        }

        for (int i = 0; i < itemsToDespawn.Length; i++)
        {
            itemsToDespawn[i].SetActive(false);
        }
    }
}
