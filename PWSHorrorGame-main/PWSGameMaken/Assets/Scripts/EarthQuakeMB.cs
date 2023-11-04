using System.Collections;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class EarthQuakeMB : MonoBehaviour
{
    [SerializeField] private float shakeIntensity = 25f;
	[SerializeField] private float shakeFrequency = 10f;
	[SerializeField] private float shakeTime = 1f;
	[SerializeField] private GameObject[] sounds;

	private void OnDestroy()
	{
		EarthQuake();
	}

	public void EarthQuake()
	{
		CinemachineShake.Instance.ShakeCamera(shakeIntensity, shakeFrequency, shakeTime);
		for (int i = 0; i < sounds.Length; i++)
		{
			sounds[i].SetActive(true);
		}
	}
}
