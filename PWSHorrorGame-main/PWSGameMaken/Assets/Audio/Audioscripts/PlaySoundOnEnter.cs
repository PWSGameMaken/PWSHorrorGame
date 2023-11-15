using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class PlaySoundOnEnter : MonoBehaviour
{
    public AudioSource playSound;
   void OnTriggerEnter(Collider other)
    {
        playSound.Play();
    }

}
