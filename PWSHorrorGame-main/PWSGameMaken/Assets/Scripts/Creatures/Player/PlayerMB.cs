using StarterAssets;
using UnityEngine;

public class PlayerMB : CreatureMB
{
	#region Singleton

	public static PlayerMB instance;

	private void Awake()
	{
		instance = this;
	}
	#endregion

	public Transform playerCameraRoot;
	public GameObject playerBody;
	public GameObject killLantern;
	public Transform weightPuzzleRespawnPos;

	private FirstPersonController fpsController;
	private StarterAssetsInputs playerInput;

	private void Start()
	{
		playerBody = gameObject;
		fpsController = GetComponent<FirstPersonController>();
		playerInput = GetComponent<StarterAssetsInputs>();
	}

	public void ActivateKillScene(bool state)
	{
		playerBody.SetActive(!state);
		killLantern.SetActive(state);
		BlockPlayerMovement(state);
	}

	private void BlockPlayerMovement(bool state)
	{
		playerInput.cursorInputForLook = !state;
		fpsController.MoveSpeed = state == true ? 0 : 4;
	}
}
