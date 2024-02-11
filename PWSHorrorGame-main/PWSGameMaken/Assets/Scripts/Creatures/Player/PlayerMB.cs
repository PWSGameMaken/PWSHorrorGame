using StarterAssets;
using System.Collections;
using UnityEngine;

public enum PlayerAnimations
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

public interface ITargetMB
{
	bool IsCaught { get; set; }
	Transform RespawnPoint { get; }

	void Kill();
	void Respawn();
}

public class PlayerMB : CreatureMB, ITargetMB
{
	#region Singleton

	public static PlayerMB instance;

	private void Awake()
	{
		instance = this;
	}
	#endregion

	[SerializeField] private Transform _playerCameraRoot;

	[SerializeField] private AnimationClip _killAnimation;
	[SerializeField] private GameObject _playerBody;
	[SerializeField] private GameObject _killLantern;

	[SerializeField] private int _killCamRotationSpeed = 5;

	[SerializeField] private Transform _weightPuzzleRespawnPos;
	[SerializeField] private Transform _respawnPoint;
	[SerializeField] private Transform _monsterFocusPoint;

	[SerializeField] private GameOverMenu _gameOverMenu;

	private CharacterController _charController;
	private FirstPersonController _fpsController;
	private StarterAssetsInputs _playerInput;

	private bool _walking = false;
	public bool isCaught = false;

	public Transform WeightPuzzleRespawnPos { get => _weightPuzzleRespawnPos; }
	public Transform RespawnPoint { get => _respawnPoint; }
	public bool IsCaught { get => isCaught; set => isCaught = value; }

	private new void Start()
	{
		base.Start();
		_charController = GetComponent<CharacterController>();
		_fpsController = GetComponent<FirstPersonController>();
		_playerInput = GetComponent<StarterAssetsInputs>();
	}

	private void Update()
	{
		if (IsCaught)
		{
			Face(_monsterFocusPoint.position);
			return;
		}

		SetMovementAnimation();
	}

	public void Kill()
	{
		BlockPlayerMovement(true);

		_playerBody.SetActive(false);
		_killLantern.SetActive(true);
		IsCaught = true;
		StartCoroutine(KillPart2());
	}

	private IEnumerator KillPart2()
	{
		yield return new WaitForSeconds(_killAnimation.length);
		AudioListener.pause = true;
		_gameOverMenu.SetActiveMenu();
	}

	public void Respawn()
	{
		RespawnSystemMB.Respawn(gameObject.transform, RespawnPoint);
		AudioListener.pause = false;
		BlockPlayerMovement(false);

		_gameOverMenu.SetActiveMenu();
		_playerBody.SetActive(true);
		_killLantern.SetActive(false);
		IsCaught = false;
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

	private void SetMovementAnimation()
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
				AnimMB.SetAnimation(PlayerAnimations.IsWalking.ToString(), true);
				AudioManager.Play(PlayerAudio.PlayerFootsteps.ToString(), gameObject);
			}
			else
			{
				AnimMB.SetAnimation(PlayerAnimations.IsWalking.ToString(), false);
				AudioManager.Stop(PlayerAudio.PlayerFootsteps.ToString());
			}
		}
	}

	private IEnumerator ActivateCharacterController(bool activeState)
	{
		yield return new WaitForSecondsRealtime(0.5f);
		_charController.enabled = activeState;
	}

	public void Face(Vector3 faceDirection)
	{
		FaceObject(_playerCameraRoot, faceDirection, _killCamRotationSpeed);
	}
}
