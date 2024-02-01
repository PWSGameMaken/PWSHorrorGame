using StarterAssets;
using System.Collections;
using UnityEngine;

public enum AnimationsPlayer
{
	NoAnimation,
	IsWalking,
	HasVase,
	HasStone,
}

public enum PlayerAudio
{
	PlayerFootsteps
}

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

	private bool _walking = false;

	public Transform PlayerCameraRoot { get => _playerCameraRoot; }
	public GameObject PlayerBody { get => _playerBody; }
	public Transform WeightPuzzleRespawnPos { get => _weightPuzzleRespawnPos; }
	public int KillCamRotationSpeed { get => _killCamRotationSpeed; }

	private new void Start()
	{
		base.Start();
		_charController = GetComponent<CharacterController>();
		_fpsController = GetComponent<FirstPersonController>();
		_playerInput = GetComponent<StarterAssetsInputs>();
	}

	private void Update()
	{
		var goingToWalk = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);
		var toggleWalking =
			!_walking && goingToWalk
			|| _walking && !goingToWalk
			//Deze lijn moet weg worden gehaald wanneer we geen sprint meer hebben! (Onder)
			|| _walking && Input.GetKey(KeyCode.LeftShift);

		if (toggleWalking)
		{
			_walking = !_walking;
			if (_walking)
			{
				AnimMB.SetAnimation(AnimationsPlayer.IsWalking.ToString(), true);
				AudioManager.Play(PlayerAudio.PlayerFootsteps.ToString(), gameObject);
			}
			else
			{
				AnimMB.SetAnimation(AnimationsPlayer.IsWalking.ToString(), false);
				AudioManager.Stop(PlayerAudio.PlayerFootsteps.ToString());
			}
		}
	}

	public void ActivateKillScene(bool activate)
	{
		if(activate)
		{
			BlockPlayerMovement(true);

			PlayerBody.SetActive(false);
			_killLantern.SetActive(true);
		}
		else
		{
			BlockPlayerMovement(false);

			PlayerBody.SetActive(true);
			_killLantern.SetActive(false);
		}
	}

	private void BlockPlayerMovement(bool blockMovement)
	{
		if(blockMovement)
		{
			_playerInput.cursorInputForLook = false;
			_fpsController.MoveSpeed = 0;

			StartCoroutine(ActivateCharacterController(false));
		}
		else
		{
			_playerInput.cursorInputForLook = true;
			_fpsController.MoveSpeed = 4;

			StartCoroutine(ActivateCharacterController(true));
		}
	}
		
	private IEnumerator ActivateCharacterController(bool activeState)
	{
		yield return new WaitForSecondsRealtime(0.5f);
		_charController.enabled = activeState;
	}
}
