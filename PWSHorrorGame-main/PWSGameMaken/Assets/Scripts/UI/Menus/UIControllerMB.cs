using UnityEngine;

public abstract class UIControllerMB : MonoBehaviour
{
	protected GameObject menuToActivate;

	protected bool isActivated = false;

	protected void Start()
	{
		menuToActivate = transform.Find("UI").gameObject;
	}

	public void SetActiveMenu()
	{
		SetActiveMenu(menuToActivate, !isActivated);
	}

	public void SetActiveMenu(GameObject menuToActivate, bool activeState)
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
	}
}
