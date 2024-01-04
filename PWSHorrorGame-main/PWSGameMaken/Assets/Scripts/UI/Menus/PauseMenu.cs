using UnityEngine;

public class PauseMenu : UIControllerMB
{
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.I))
		{
			SetActive();
		}
	}

	public override void SetActive()
	{
		SetActiveMenu(menuToActivate, !isActivated);
	}
}
