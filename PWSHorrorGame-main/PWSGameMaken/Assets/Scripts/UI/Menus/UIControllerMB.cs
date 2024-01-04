using StarterAssets;
using UnityEngine;

public abstract class UIControllerMB : MonoBehaviour
{
	[SerializeField] protected GameObject menuToActivate;
	[SerializeField] private StarterAssetsInputs input;

	protected bool isActivated = false;

	public abstract void SetActive();

	protected void SetActiveMenu(GameObject menuToActivate, bool activeState)
	{
		SetActiveMenuSettings(activeState);
		menuToActivate.SetActive(activeState);
	}

	private void SetActiveMenuSettings(bool activeState)
	{
		isActivated = activeState;

		Cursor.lockState = activeState == true ? CursorLockMode.Confined : CursorLockMode.Locked;
		Cursor.visible = activeState;

		Time.timeScale = activeState == true ? 0 : 1;

		input.cursorInputForLook = !activeState;
		input.cursorLocked = !activeState;
	}
}
