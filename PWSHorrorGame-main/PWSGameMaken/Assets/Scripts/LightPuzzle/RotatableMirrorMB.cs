using StarterAssets;
using UnityEngine;

public class RotatableMirrorMB : InteractableObjectMB, IInteractWithoutSlot
{
	private bool _isRotating = false;
	[SerializeField] private int _rotateSpeed;
	[SerializeField] private FirstPersonController _firstPersonController;

	private void FixedUpdate()
	{
		if (_isRotating)
		{
			RotateObject();
		}
	}

	public void Interact()
	{
		SetActive(!_isRotating);
	}

	private void SetActive(bool status)
	{
		_isRotating = status;
		_firstPersonController.enabled = !status;
	}

	private void RotateObject()
	{
		var input = Input.GetAxis("Horizontal");

		transform.Rotate(0, Time.deltaTime * _rotateSpeed * input, 0);
	}
}
