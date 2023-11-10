using System.Collections;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class EarthQuakeMB : MonoBehaviour
{
    [SerializeField] private float shakeIntensity = 25f;
	[SerializeField] private float shakeFrequency = 10f;
	public float shakeTime = 1f;
	[SerializeField] private GameObject[] sounds;

	//private void OnDestroy()
	//{
	//	EarthQuake();
	//}

	public void EarthQuake()
	{
		ShakeCamera();

		ActivateSounds();
	}

	private void ShakeCamera()
	{
		CinemachineShake.Instance.ShakeCamera(shakeIntensity, shakeFrequency, shakeTime);
	}

	public void ActivateSounds()
	{
		for (int i = 0; i < sounds.Length; i++)
		{
			sounds[i].SetActive(true);
		}
	}
}
