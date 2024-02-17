using UnityEngine;

public class PauseMenu : UIControllerMB
{
	private new void Start()
	{
		base.Start();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.I))
		{
			SetActiveMenu();
		}
	}
}
