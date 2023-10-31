/*
* Grobros
* https://github.com/GroBro-s
*/
using UnityEngine;

public static class MouseObject
{
	public static UserInterfaceMB interfaceMouseIsOver;
	public static ParentSlotsMB parentSlotsMB;
	public static GameObject slotHoveredOver;

	public static Vector2 GetPosition()
	{
		return new Vector2(Input.mousePosition.x, Input.mousePosition.y);
	}

	public static void OnEnterInterface(GameObject userInterface)
	{
		interfaceMouseIsOver = userInterface.GetComponent<UserInterfaceMB>();
	}

	public static void OnEnterSlot(GameObject slotGO, ParentSlotsMB parentSlotsMBInput)
	{
		slotHoveredOver = slotGO;
		parentSlotsMB = parentSlotsMBInput;
	}

	public static void OnExitSlot()
	{
		slotHoveredOver = null;
		parentSlotsMB = null;
	}

	public static void OnExitInterface()
	{
		interfaceMouseIsOver = null;
	}
}