using UnityEngine;

public class EarthQuakeMB : MonoBehaviour
{
    [SerializeField] private float shakeIntensity = 25f;
	[SerializeField] private float shakeFrequency = 10f;
	public float shakeTime = 1f;

	public void EarthQuake()
	{
		ShakeCamera();
		Sounds();
	}

	private void ShakeCamera()
	{
		CinemachineShakeMB.Instance.ShakeCamera(shakeIntensity, shakeFrequency, shakeTime);	
	}

	private void Sounds()
	{
		if (TryGetComponent<PlaySounds>(out var playSoundsMB))
		{
			playSoundsMB.ActivateSounds(true);
		}
	}
}
