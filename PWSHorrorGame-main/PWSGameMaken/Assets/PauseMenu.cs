using StarterAssets;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
	#region Singleton

	public static PauseMenu instance;

	private void Awake()
	{
		instance = this;
	}
	#endregion

	[SerializeField] private StarterAssetsInputs input;
	[SerializeField] private GameObject UI;
	private bool isActivated = false;

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			SetActivePauseMenu(isActivated);
		}
	}

	public void SetActivePauseMenu(bool state)
	{
		isActivated = !isActivated;
		Time.timeScale = state == true ? 0 : 1;
		UI.SetActive(state);
		input.cursorInputForLook = !state;
		Cursor.visible = state;
	}
}
