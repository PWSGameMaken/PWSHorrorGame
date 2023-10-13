/*
* Grobros
* https://github.com/GroBro-s
*/

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public static class Description
{
	#region variables
	public static GameObject item;

	private static readonly int _xOffset = 110;
	private static readonly int _yOffset = 0;
	private static readonly Color transparent = new(0.4156863f, 0.3490196f, 0.1137255f, 0f);
	private static readonly Color opaque = new(0.4156863f, 0.3490196f, 0.1137255f, 1f);
	#endregion

	#region unity functions
	public static void SetDescription(IBaseItem item, Vector3 slotPos)
	{
		Description.item = GameObject.FindGameObjectWithTag("DescriptionPrefab");
 		Description.item.GetComponentInChildren<TextMeshProUGUI>().text = item.ItemSO.description;

		Description.item.GetComponentInChildren<Image>().color = opaque;
		Description.item.GetComponentInChildren<RectTransform>().position = Move(slotPos);
	}

	public static void DeleteDescription()
	{
		item.GetComponentInChildren<Image>().color = transparent;
		item.GetComponentInChildren<TextMeshProUGUI>().text = null;
	}

	private static Vector2 Move(Vector2 slotPos)
	{
		float outerScreenBorder = Screen.width - 200;

		var descriptionPos = slotPos;

		descriptionPos.x = descriptionPos.x > outerScreenBorder
			? descriptionPos.x -= _xOffset
			: descriptionPos.x += _xOffset;
		descriptionPos.y = descriptionPos.y -= _yOffset;

		return descriptionPos;
	}
	#endregion
}
