/*
* Grobros
* https://github.com/GroBro-s
*/
using UnityEngine;

public static class MouseObject
{
	public static UserInterfaceMB interfaceMouseOver;
	public static VisibleSlotsMB visibleSlotsMB;
	public static GameObject slotHoveredOver;

	public static Vector2 GetPosition()
	{
		return new Vector2(Input.mousePosition.x, Input.mousePosition.y);
	}

	public static void OnEnterInterface(UserInterfaceMB userInterface)
	{
		interfaceMouseOver = userInterface;
	}

	public static void OnExitInterface()
	{
		interfaceMouseOver = null;
	}

	public static void OnEnterSlot(GameObject slotGO, VisibleSlotsMB visibleSlotsMBInput)
	{
		slotHoveredOver = slotGO;
		visibleSlotsMB = visibleSlotsMBInput;
	}

	public static void OnExitSlot()
	{
		slotHoveredOver = null;
		visibleSlotsMB = null;
	}
}