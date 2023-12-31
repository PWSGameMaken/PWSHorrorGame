/*
* Grobros
* https://github.com/GroBro-s/MorbidMarshmallow
*/
using UnityEngine;
using UnityEngine.UI;

public static class TempItem
{
	#region variables
	public static GameObject item;
	#endregion

	#region Unity Methods
	public static void SetSprite(IBaseItem item)
	{
		if(item.Id >= 0)
		{
			TempItem.item = GameObject.Find("TempItemPrefab");
			TempItem.item.GetComponent<Image>().sprite = item.ItemSO.sprite;
			TempItem.item.GetComponent<RectTransform>().sizeDelta = new Vector2(50, 50);
		}
		else
		{
			ResetSprite();
		}
	}

	public static void ResetSprite()
	{
		item.GetComponent<Image>().sprite = null;
		item.GetComponent<RectTransform>().sizeDelta = new Vector2 (0, 0);
	}

	public static void Move()
	{
		var mousePosition = MouseObject.GetPosition();
		item.GetComponent<RectTransform>().position = mousePosition;
	}
	#endregion
}
