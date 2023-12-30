using UnityEngine;

public class ItemSoundsMB : MonoBehaviour
{
	private Rigidbody rb;
	private AudioSource audioSource;
	private AudioClip audioClip;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();
		audioClip = audioSource.clip;
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (rb.velocity.magnitude > 0 && Time.time > 10)
		{
			audioSource.PlayOneShot(audioClip);
		}
	}
}
