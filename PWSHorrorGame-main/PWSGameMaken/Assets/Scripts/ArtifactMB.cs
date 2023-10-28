using System.Collections;
using UnityEngine;

public class ArtifactMB : MonoBehaviour
{
	[SerializeField] private GameObject rocksToSpawn;
	[SerializeField] private GameObject wallToCollapse;

	[SerializeField] private float shakeIntensity = 25f;
	[SerializeField] private float shakeFrequency = 10f;
	[SerializeField] private float shakeTime = 1f;

	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
			EarthQuake();
		}	
	}

	private void EarthQuake()
	{
		CinemachineShake.Instance.ShakeCamera(shakeIntensity, shakeFrequency, shakeTime);
	}
}
