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
	[SerializeField] private GameObject _gameOverUI;
	[SerializeField] private GameObject _pauseMenuUI;
	[SerializeField] private StarterAssetsInputs input;

	private bool isActivated = false;

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.I))
		{
			SetActivePauseUI(isActivated);
		}
	}
	
	public void SetActiveGameOverUI(bool state)
	{
		SetActiveMenuSettings(state);
		SetActiveMenu(_gameOverUI, state);
	}

	public void SetActivePauseUI(bool state)
	{
		SetActiveMenuSettings(state);
		SetActiveMenu(_pauseMenuUI, state);
	}

	public void SetActiveMenuSettings(bool state)
	{
		Cursor.lockState = state == true ? CursorLockMode.Confined : CursorLockMode.Locked;
		isActivated = !isActivated;
		Time.timeScale = state == true ? 0 : 1;
		input.cursorInputForLook = !state;
		input.cursorLocked = !state;
		Cursor.visible = state;
	}

	public void SetActiveMenu(GameObject menuToSetActive, bool activeState)
	{
		menuToSetActive.SetActive(activeState);
	}
}
