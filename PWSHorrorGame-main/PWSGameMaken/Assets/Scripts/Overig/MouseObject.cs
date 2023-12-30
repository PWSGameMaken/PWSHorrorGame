/*
* Grobros
* https://github.com/GroBro-s
*/
using UnityEngine;

public static class MouseObject
{
	public static UserInterfaceMB interfaceMouseIsOver;
	public static VisibleSlotsMB visibleSlotsMB;
	public static GameObject slotHoveredOver;

	public static Vector2 GetPosition()
	{
		return new Vector2(Input.mousePosition.x, Input.mousePosition.y);
	}

	public static void OnEnterInterface(GameObject userInterface)
	{
		interfaceMouseIsOver = userInterface.GetComponent<UserInterfaceMB>();
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

	public static void OnExitInterface()
	{
		interfaceMouseIsOver = null;
	}
}