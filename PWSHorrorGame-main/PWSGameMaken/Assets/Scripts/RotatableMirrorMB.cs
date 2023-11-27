using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatableMirrorMB : MonoBehaviour
{
	private bool _isRotating = false;

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
	}

	public void DeActivateMirror()
	{
		_isRotating = false;
	}

	private void RotateObject()
	{
		if(Input.GetKeyDown(KeyCode.A))
		{
			RotateLeft();
		}
		else if(Input.GetKeyDown(KeyCode.D))
		{
			RotateRight();
		}
	}

	private void RotateLeft()
	{
		transform.Rotate(Time.deltaTime * 10, 0, 0);
	}

	private void RotateRight()
	{
		transform.Rotate(Time.deltaTime * 10, 0, 0);
	}
}
