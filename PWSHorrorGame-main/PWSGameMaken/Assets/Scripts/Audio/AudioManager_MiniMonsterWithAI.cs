using UnityEngine;

public class AudioManager_MiniMonsterWithAI : MonoBehaviour
{
    private AudioSource _ASFootsteps;
    private AudioSource _ASScream;

    public AudioClip[] footsteps;
    public AudioClip[] screams;

    void Start()
    {
        _ASFootsteps = gameObject.AddComponent<AudioSource>();
        _ASFootsteps.loop = true;
        Footstep();
        _ASScream = gameObject.AddComponent<AudioSource>();
        Scream();
    }

    private void Footstep()
    {
        if (footsteps.Length == 0) { print("Should Play footstep, but no clip"); return; }
        int i = Random.Range(0, footsteps.Length);
        _ASFootsteps.clip = footsteps[i];
        _ASFootsteps.Play();
    }

	private void Scream()
	{
		if (screams.Length == 0) { print("Should Play ScreamTag, but no clip"); return; }
		int i = Random.Range(0, screams.Length);
		_ASScream.clip = screams[i];
		_ASScream.Play();
	}
}
