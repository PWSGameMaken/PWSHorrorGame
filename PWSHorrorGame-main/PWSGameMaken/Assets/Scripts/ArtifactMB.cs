using System.Collections;
using UnityEngine;

public class ArtifactMB : MonoBehaviour
{
	[SerializeField] private GameObject rocksToSpawn;
	[SerializeField] private GameObject wallToCollapse;

	[SerializeField] private float shakeIntensity = 25f;
	[SerializeField] private float shakeFrequency = 10f;
	[SerializeField] private float shakeTime = 1f;
	public CameraShake cameraShake;
	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
			CinemachineShake.Instance.ShakeCamera(shakeIntensity, shakeFrequency, shakeTime);
		}	
	}

	public void CollapseMap()
	{
		rocksToSpawn.SetActive(true);
		wallToCollapse.SetActive(false);
	}
}
