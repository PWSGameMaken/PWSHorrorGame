using UnityEngine;

public class PauseMenu : UIControllerMB
{
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.I))
		{
			SetActiveMenu();
		}
	}
}
