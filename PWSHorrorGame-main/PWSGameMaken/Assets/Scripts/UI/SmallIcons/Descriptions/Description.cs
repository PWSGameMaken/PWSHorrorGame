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
	public static GameObject descriptionGO;

	private static readonly int _xOffset = 110;
	private static readonly int _yOffset = 0;
	private static readonly Color transparent = new(0.4156863f, 0.3490196f, 0.1137255f, 0f);
	private static readonly Color opaque = new(0.4156863f, 0.3490196f, 0.1137255f, 1f);
	#endregion

	#region unity functions
	public static void SetDescription(string descriptionText, Vector3 slotPos)
	{
		descriptionGO = GameObject.FindGameObjectWithTag("DescriptionPrefab");
 		descriptionGO.GetComponentInChildren<TextMeshProUGUI>().text = descriptionText;

		descriptionGO.GetComponentInChildren<Image>().color = opaque;
		descriptionGO.GetComponentInChildren<RectTransform>().position = Move(slotPos);
	}

	public static void DeleteDescription()
	{
		descriptionGO.GetComponentInChildren<Image>().color = transparent;
		descriptionGO.GetComponentInChildren<TextMeshProUGUI>().text = null;
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
