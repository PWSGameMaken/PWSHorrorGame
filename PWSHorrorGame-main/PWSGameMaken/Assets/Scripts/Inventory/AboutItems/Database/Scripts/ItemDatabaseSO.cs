/*
* Grobros
* https://github.com/GroBro-s
*/

using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database", menuName = "Inventory System/Items/Database")]
public class ItemDatabaseSO : ScriptableObject, ISerializationCallbackReceiver
{
	public ItemSO[] itemSOlist;

	//checkt of de Id's nogsteeds overeenkomen
	[ContextMenu("Update ID's")]
	private void UpdateID()
	{
		for (int i = 0; i < itemSOlist.Length; i++) 
		{
			if (itemSOlist[i].id != i)
			{
				itemSOlist[i].id = i;
			}
		}
	}

	public void OnAfterDeserialize()
	{
		UpdateID();
	}

	public void OnBeforeSerialize()
	{

	}
}