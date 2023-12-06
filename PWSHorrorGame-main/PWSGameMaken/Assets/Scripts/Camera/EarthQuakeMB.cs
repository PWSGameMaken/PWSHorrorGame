using UnityEngine;

public class EarthQuakeMB : MonoBehaviour
{
    [SerializeField] private float shakeIntensity = 25f;
	[SerializeField] private float shakeFrequency = 10f;
	[SerializeField] private GameObject[] sounds;

	public float shakeTime = 1f;

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
