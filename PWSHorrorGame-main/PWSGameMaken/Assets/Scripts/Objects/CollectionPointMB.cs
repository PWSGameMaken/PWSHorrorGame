using UnityEngine;

public class CollectionPointMB : InteractableObjectMB
{
    [SerializeField] private GameObject[] itemsToSpawn;
    [SerializeField] private GameObject[] itemsToDespawn;
    [SerializeField] private GameObject[] itemsToAnimate;

    private HiddenSlotsMB _hiddenSlotsMB;
	public string hintText;
	public ItemSO[] acceptedItemSO;

    [SerializeField] private bool canCollectItems;


	private void Start()
	{
        if(canCollectItems)
        {
		    _hiddenSlotsMB = GetComponent<HiddenSlotsMB>();
        }
	}

	public override string GetHintUIText()
	{
        return hintText;
	}

	public override void Interact(GameObject itemToInteract, VisibleSlotsMB visibleSlotsMB)
    {
		InteractWithCollectionPoint(itemToInteract.GetComponent<HiddenSlotsMB>(), itemToInteract.GetComponent<CollectionPointMB>(), visibleSlotsMB);
	}

	private void InteractWithCollectionPoint(HiddenSlotsMB hiddenSlotsMB, CollectionPointMB collectionPoint, VisibleSlotsMB playerSlots)
	{
		var selectedItemSO = playerSlots.selectedSlot.ItemObject?.Item.ItemSO;

		if (selectedItemSO == null) return;

		if (collectionPoint.CanAddItem(selectedItemSO))
		{
			MoveItemToCollectionPoint(hiddenSlotsMB, playerSlots);
			collectionPoint.CheckForCompletion();
		}
	}

	private void MoveItemToCollectionPoint(HiddenSlotsMB slotsToBeMoved, VisibleSlotsMB playerSlots)
	{
		var selectedSlot = playerSlots.selectedSlot;

		var isMoved = slotsToBeMoved.AddItem(selectedSlot.ItemObject);

		if (isMoved)
		{
			playerSlots.ClearSelectedSlot();
		}
	}

	public bool CanAddItem(ItemSO itemSO)
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

    public void CheckForCompletion()
    {
		if (_hiddenSlotsMB.CountEmptySlots() == 0)
		{
			ObjectiveCompleted();
		}
	}

    public void ObjectiveCompleted()
    {
        for (int i = 0; i < itemsToAnimate.Length; i++)
        {
			if (itemsToAnimate[i].CompareTag("Door"))
            {
                var Anim = itemsToAnimate[i].GetComponent<Animator>();
                Anim.SetBool("OpenL", true);
                Anim.SetBool("OpenR", true);
            }
            if (itemsToAnimate[i].CompareTag("Plank"))
            {
                var Anim = itemsToAnimate[i].GetComponent<Animator>();
                Anim.SetBool("PlankStijg", true);
            }
        }
        
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
