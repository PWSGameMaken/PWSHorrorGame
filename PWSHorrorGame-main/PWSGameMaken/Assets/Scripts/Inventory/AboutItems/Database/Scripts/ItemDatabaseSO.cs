/*
* Grobros
* https://github.com/GroBro-s
*/

using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database", menuName = "Inventory System/Items/Database")]
public class ItemDatabaseSO : ScriptableObject, ISerializationCallbackReceiver
{
	public ItemSO[] ItemSOlist;

	//checkt of de Id's nogsteeds overeenkomen
	[ContextMenu("Update ID's")]
	private void UpdateID()
	{
		for (int i = 0; i < ItemSOlist.Length; i++)
		{
			if (ItemSOlist[i].id != i)
			{
				ItemSOlist[i].id = i;
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