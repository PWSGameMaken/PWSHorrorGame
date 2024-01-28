using System;
using UnityEngine;

[Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;
    [Range(0.1f, 3f)]
    public float pitch;

    public bool loop;
    public bool spatialize = true;

    [Header("Spatialization Parameters")]
    public float minDistance = 1.5f;
    public float maxDistance = 3.0f;
    [Range(0f, 360f)]
    public float spread = 0f;

    [HideInInspector] public AudioSource source;
}
