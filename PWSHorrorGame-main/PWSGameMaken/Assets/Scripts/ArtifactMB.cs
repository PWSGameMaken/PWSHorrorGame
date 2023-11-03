using System.Collections;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class ArtifactMB : MonoBehaviour

{

    [SerializeField] private float shakeIntensity = 25f;
	[SerializeField] private float shakeFrequency = 10f;
	[SerializeField] private float shakeTime = 1f;

	private void OnDestroy()
	{
		EarthQuake();
	}

	private void EarthQuake()
	{
		CinemachineShake.Instance.ShakeCamera(shakeIntensity, shakeFrequency, shakeTime);
	}
}
