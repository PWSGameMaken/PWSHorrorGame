using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullScreenInventory : MonoBehaviour
{
    [NonSerialized] public bool inventoryIsActivated = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if(inventoryIsActivated == false)
            {
                ActivateMouseCursor();
                return;
            }

            if (inventoryIsActivated == true)
		    {
                DeactivateMouseCursor();
                return;
            }
        }
	}

    private void ActivateMouseCursor()
    {
		inventoryIsActivated = true;

		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
	}

    private void DeactivateMouseCursor()
    {
		inventoryIsActivated = false;

		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.Locked;
	}
}
