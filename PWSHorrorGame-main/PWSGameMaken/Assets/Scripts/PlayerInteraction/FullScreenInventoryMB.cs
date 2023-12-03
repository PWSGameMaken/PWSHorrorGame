using System;
using UnityEngine;

public class FullScreenInventoryMB : MonoBehaviour
{
    [NonSerialized] public bool inventoryIsActivated = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            EnableInventory(!inventoryIsActivated);
        }
	}

    private void EnableInventory(bool value)
    {
		inventoryIsActivated = value;

		Cursor.visible = value;
		Cursor.lockState = value == true ? CursorLockMode.None : CursorLockMode.Locked;
	}
}
