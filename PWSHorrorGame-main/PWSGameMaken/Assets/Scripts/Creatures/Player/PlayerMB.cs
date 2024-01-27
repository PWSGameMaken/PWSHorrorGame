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

	[SerializeField] private Transform _playerCameraRoot;
	[SerializeField] private GameObject _playerBody;
	[SerializeField] private GameObject _killLantern;
	[SerializeField] private Transform _weightPuzzleRespawnPos;
	[SerializeField] private int _killCamRotationSpeed = 5;

	private CharacterController _charController;
	private FirstPersonController _fpsController;
	private StarterAssetsInputs _playerInput;

	public Transform PlayerCameraRoot { get => _playerCameraRoot; }
	public GameObject PlayerBody { get => _playerBody; }
	public Transform WeightPuzzleRespawnPos { get => _weightPuzzleRespawnPos; }
	public int KillCamRotationSpeed { get => _killCamRotationSpeed; }

	private void Start()
	{
		_charController = GetComponent<CharacterController>();
		_fpsController = GetComponent<FirstPersonController>();
		_playerInput = GetComponent<StarterAssetsInputs>();
		TypeOfCreature = TypeOfCreature.Player;
	}

	public void ActivateKillScene(bool state)
	{
		BlockPlayerMovement(state);

		PlayerBody.SetActive(!state);
		_killLantern.SetActive(state);
	}

	private void BlockPlayerMovement(bool state)
	{
		_playerInput.cursorInputForLook = !state;
		_fpsController.MoveSpeed = state == true ? 0 : 4;

		StartCoroutine(ActivateCharacterController(state));
	}
		
	private IEnumerator ActivateCharacterController(bool state)
	{
		yield return new WaitForSecondsRealtime(0.5f);
		_charController.enabled = !state;
	}
}
