using UnityEngine;

public class ActivatePuzzleMB : MonoBehaviour
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
