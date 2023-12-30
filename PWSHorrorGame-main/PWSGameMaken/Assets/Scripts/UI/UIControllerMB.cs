using StarterAssets;
using UnityEngine;

public class UIControllerMB : MonoBehaviour
{
	#region Singleton
	public static UIControllerMB instance;
	private void Awake()
	{
		instance = this;
	}
	#endregion
	[SerializeField] private GameObject _gameOverMenu;
	[SerializeField] private GameObject _pauseMenu;
	[SerializeField] private StarterAssetsInputs input;

	private bool isActivated = false;

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.I))
		{
			SetActiveMenu(_pauseMenu, isActivated);
		}
	}
	
	public void SetActiveGameOverMenu(bool activeState)
	{
		SetActiveMenu(_gameOverMenu, activeState);
	}

	public void SetActivePauseMenu(bool activeState)
	{
		SetActiveMenu(_pauseMenu, activeState);
	}

	public void SetActiveMenu(GameObject menuToActivate, bool activeState)
	{
		SetActiveMenuSettings(activeState);
		menuToActivate.SetActive(activeState);
	}

	private void SetActiveMenuSettings(bool state)
	{
		isActivated = !isActivated;

		Cursor.lockState = state == true ? CursorLockMode.Confined : CursorLockMode.Locked;
		Cursor.visible = state;

		Time.timeScale = state == true ? 0 : 1;

		input.cursorInputForLook = !state;
		input.cursorLocked = !state;
	}
}
