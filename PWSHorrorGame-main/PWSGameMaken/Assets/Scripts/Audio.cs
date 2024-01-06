using UnityEngine;

public class Audio : MonoBehaviour
{
	private AudioSource _soundSource;

	private void Start()
	{
		_soundSource = GetComponent<AudioSource>();
	}

	public void PlaySound(bool playSound)
	{
		if (playSound)
		{
			_soundSource.Play();
		}
		else
		{
			_soundSource.Stop();
		}
	}
}

