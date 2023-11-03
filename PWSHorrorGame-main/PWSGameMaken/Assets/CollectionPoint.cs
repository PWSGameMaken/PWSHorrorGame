using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionPoint : MonoBehaviour
{
    [SerializeField] private ItemSO itemSOToCollect;
    private bool isFull = false;

    public void AddItemToCollectionPoint(ItemSO itemSO)
    {
        if(itemSOToCollect == itemSO)
        {
            isFull = true;
        }
    }
}
