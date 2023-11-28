using StarterAssets;
using System.Collections;
using System.Collections.Generic;
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

	public void ActivateMirror()
	{
		_isRotating = true;
		_firstPersonController.enabled = false;
	}

	public void DeActivateMirror()
	{
		_isRotating = false;
		_firstPersonController.enabled = true;
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
