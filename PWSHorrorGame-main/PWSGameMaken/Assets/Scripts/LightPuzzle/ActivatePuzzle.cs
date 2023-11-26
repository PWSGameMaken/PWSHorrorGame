using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePuzzle : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		RenderSettings.fog = false;
	}

	private void OnTriggerExit(Collider other)
	{
		RenderSettings.fog = true;
	}
}
