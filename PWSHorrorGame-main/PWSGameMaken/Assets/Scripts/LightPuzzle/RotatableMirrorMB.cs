using StarterAssets;
using UnityEngine;

public class RotatableMirrorMB : MonoBehaviour
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

	public void SetActive(bool status)
	{
		_isRotating = status;
		_firstPersonController.enabled = !status;
	}

	private void RotateObject()
	{
		if(Input.GetKey(KeyCode.A))
		{
			RotateLeft();
		}
		else if(Input.GetKey(KeyCode.D))
		{
			RotateRight();
		}
	}

	private void RotateLeft()
	{
		transform.Rotate(0, Time.deltaTime * _rotateSpeed, 0);
	}

	private void RotateRight()
	{
		transform.Rotate(0, Time.deltaTime * -_rotateSpeed, 0);
	}
}
