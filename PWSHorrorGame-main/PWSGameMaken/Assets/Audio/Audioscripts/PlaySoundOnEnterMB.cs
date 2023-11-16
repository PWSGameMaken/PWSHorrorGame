using UnityEngine;

public class PlaySoundOnEnterMB : MonoBehaviour
{
    public AudioSource playSound;
   void OnTriggerEnter(Collider other)
    {
        playSound.Play();
    }

}
