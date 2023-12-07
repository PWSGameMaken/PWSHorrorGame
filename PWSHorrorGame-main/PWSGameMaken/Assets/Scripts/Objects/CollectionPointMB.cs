using UnityEngine;

public class CollectionPointMB : MonoBehaviour
{
    public string hintText;
    [SerializeField] private GameObject[] itemsToSpawn;
    [SerializeField] private GameObject[] itemsToDespawn;

    private HiddenSlotsMB _hiddenSlotsMB;

    public ItemSO[] acceptedItemSO;

    [SerializeField] private bool canCollectItems;


	private void Start()
	{
        if(canCollectItems)
        {
		    _hiddenSlotsMB = GetComponent<HiddenSlotsMB>();
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
