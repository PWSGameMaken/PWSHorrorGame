using UnityEngine;

public class EarthQuakeMB : MonoBehaviour
{
    [SerializeField] private float _shakeIntensity = 25f;
	[SerializeField] private float _shakeFrequency = 10f;
	[SerializeField] private float _shakeTime = 1f;

	[SerializeField] private AudioSource _audioSource;
	[SerializeField] private AudioClip earthQuakeSound;
    public float ShakeTime { get => _shakeTime; }

	private void Start()
	{
		_audioSource = GetComponent<AudioSource>();
	}

	public void EarthQuake()
	{
		ShakeCamera();
		PlaySounds();
	}

	private void ShakeCamera()
	{
		CinemachineShakeMB.Instance.ShakeCamera(_shakeIntensity, _shakeFrequency, _shakeTime);	
	}

	private void PlaySounds()
	{
		_audioSource.PlayOneShot(earthQuakeSound);
	}
}
