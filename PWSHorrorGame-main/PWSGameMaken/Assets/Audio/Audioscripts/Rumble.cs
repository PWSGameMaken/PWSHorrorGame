using UnityEngine;

public class RumbleSound : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip clip; 

    private void Update()
    {
        if(!source.isPlaying)
        {
			source.PlayOneShot(clip);
		}
    }
}
