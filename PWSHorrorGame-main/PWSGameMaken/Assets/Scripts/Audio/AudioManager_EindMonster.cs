using UnityEngine;

public class AudioManager_EindMonster : MonoBehaviour
{
	private AudioSource _ASFootsteps;
	private AudioSource _ASScream;
	private AudioSource _ASSlash;

	public AudioClip[] footsteps;
	public AudioClip[] screams;
	public AudioClip[] slashes;

	void Start()
	{
		_ASFootsteps = gameObject.AddComponent<AudioSource>();
		_ASFootsteps.loop = true;
		_ASScream = gameObject.AddComponent<AudioSource>();
		_ASSlash = gameObject.AddComponent<AudioSource>();
	}

	public void Footsteps(bool state)
	{
		if (state == false) { _ASFootsteps.Stop(); return; }

		if (footsteps.Length == 0) { print("Should Play footstep, but no clip"); return; }
		int i = Random.Range(0, footsteps.Length);
		_ASFootsteps.clip = footsteps[i];
		_ASFootsteps.Play();
	}

	public void Scream()
	{
		if (screams.Length == 0) { print("Should Play scream, but no clip"); return; }
		int i = Random.Range(0, screams.Length);
		_ASScream.clip = screams[i];
		_ASScream.Play();
	}

	public void Slash()
	{
		if (slashes.Length == 0) { print("Should Play slash, but no clip"); return; }
		int i = Random.Range(0, slashes.Length);
		_ASSlash.clip = slashes[i];
		_ASSlash.Play();
	}
}
