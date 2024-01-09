using StarterAssets;
using System.Collections;
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
	
	private CharacterController _charController;
	private FirstPersonController _fpsController;
	private StarterAssetsInputs _playerInput;

	private void Start()
	{
		_charController = GetComponent<CharacterController>();
		_fpsController = GetComponent<FirstPersonController>();
		_playerInput = GetComponent<StarterAssetsInputs>();
		typeOfCreature = TypeOfCreature.Player;
	}

	public void ActivateKillScene(bool state)
	{
		StartCoroutine(ActivateCharacterController(state));
		playerBody.SetActive(!state);
		killLantern.SetActive(state);
		BlockPlayerMovement(state);
	}

	private void BlockPlayerMovement(bool state)
	{
		_playerInput.cursorInputForLook = !state;
		_fpsController.MoveSpeed = state == true ? 0 : 4;
	}
		
	private IEnumerator ActivateCharacterController(bool state)
	{
		yield return new WaitForSecondsRealtime(0.5f);
		_charController.enabled = !state;
	}
}
