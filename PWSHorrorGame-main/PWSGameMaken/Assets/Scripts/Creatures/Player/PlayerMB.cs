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
	//bool IsCaught { get; set; }
	//Transform RespawnPoint { get; }

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
	[SerializeField] private int _killCamRotationSpeed = 5;

	[SerializeField] private AnimationClip _killAnimation;
	[SerializeField] private GameObject _playerBody;
	[SerializeField] private GameObject _killLantern;

	[SerializeField] private Transform _weightPuzzleRespawnPos;
	[SerializeField] private Transform _respawnPoint;

	[SerializeField] private EindMonsterMB _eindMonsterMB;

	private CharacterController _characterController;
	private StarterAssetsInputs _playerInput;

	private bool _walking = false;
	private bool _isCaught = false;

	public Transform WeightPuzzleRespawnPos { get => _weightPuzzleRespawnPos; }

	public delegate void OnKill();
	public OnKill onKill;

	public delegate void OnRespawn();
	public OnRespawn onRespawn;

	private new void Start()
	{
		base.Start();
		_characterController = GetComponent<CharacterController>();
		_playerInput = GetComponent<StarterAssetsInputs>();
	}

	private void Update()
	{
		if (_isCaught)
		{
			Face(_eindMonsterMB.focusPoint.position);
			return;
		}

		SetMovementAnimation();
	}

	public void Kill()
	{
		BlockPlayerMovement();

		_playerBody.SetActive(false);
		_killLantern.SetActive(true);
		_isCaught = true;
		StartCoroutine(KillPart2());
	}

	private IEnumerator KillPart2()
	{
		yield return new WaitForSeconds(_killAnimation.length);
		AudioListener.pause = true;
		onKill();
	}

	public void Respawn()
	{
		RespawnSystemMB.Respawn(gameObject.transform, _respawnPoint.position);
		AudioListener.pause = false;
		UnblockPlayerMovement();

		onRespawn();
		_playerBody.SetActive(true);
		_killLantern.SetActive(false);
		_isCaught = false;
	}

	private void BlockPlayerMovement()
	{
		_playerInput.cursorInputForLook = false;

		StartCoroutine(ActivateCharacterController(false));
	}

	private void UnblockPlayerMovement()
	{
		_playerInput.cursorInputForLook = true;

		StartCoroutine(ActivateCharacterController(true));
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

	//Wait is necessary, otherwise the controller will block the respawn(position)
	private IEnumerator ActivateCharacterController(bool activeState)
	{
		yield return new WaitForSecondsRealtime(0.5f);
		_characterController.enabled = activeState;
	}

	public void Face(Vector3 faceDirection)
	{
		FaceObject(_playerCameraRoot, faceDirection, _killCamRotationSpeed);
	}
}
