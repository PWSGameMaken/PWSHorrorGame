using UnityEngine;

[CreateAssetMenu(fileName = "StoneItem", menuName = "Inventory System/Items/StoneItem")]
[System.Serializable]
public class StoneSO : ItemSO
{
	public StoneSO()
	{
		animTag = AnimTag.HasVase;
		stackable = false;
	}
}
