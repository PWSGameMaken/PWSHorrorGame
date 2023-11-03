using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RumbleSound : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clip; 
    void Start()
    {
    }

    void Update()
    {
    source.PlayOneShot(clip);
    }
}
