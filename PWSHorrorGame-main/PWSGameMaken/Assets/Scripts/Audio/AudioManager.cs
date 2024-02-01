using System;
using System.Collections;
using UnityEngine;

public enum OtherAudio
{
	Ambiance,
	EarthQuake,
}

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

	public static AudioManager instance;

	private void Awake()
	{
		if(instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}

	private static void SetAudioStats(Sound sound, GameObject emitter)
	{
		if(emitter.TryGetComponent<AudioSource>(out var source) && !source.isPlaying)
		{
			sound.source = source;
		}
		else
		{
			sound.source = emitter.AddComponent<AudioSource>();
		}

		sound.source.clip = sound.clip;
		sound.source.loop = sound.loop;
		sound.source.volume = sound.volume;
		sound.source.pitch = sound.pitch;

		if (sound.spatialize)
		{
			sound.source.spatialize = true;
			sound.source.rolloffMode = AudioRolloffMode.Linear;
			sound.source.spatialBlend = 1f;
			sound.source.minDistance = sound.minDistance;
			sound.source.maxDistance = sound.maxDistance;
		}

		#region Alleen nodig bij Mixer
		//s.source.outputAudioMixedGrouop = mixerGroup;
		//s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		//s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));
		#endregion
	}

	public void Play(string name, GameObject emitter)
	{
		if(TryFindSound(name, out var sound))
		{
			SetAudioStats(sound, emitter);
			sound.source.Play();
		}
	}

	public void PlayOneShot(string name, GameObject emitter) //Voor Sound Effects
	{
		if (TryFindSound(name, out var sound))
		{
			SetAudioStats(sound, emitter);
			sound.source.PlayOneShot(sound.clip);

			Destroy(sound.source, sound.clip.length);
		}
	}

	public void Stop(string name)
	{
		if(TryFindSound(name, out var sound))
		{
			sound.source.Stop();
		}
	}

	private bool TryFindSound(string name, out Sound sound)
	{
		sound = Array.Find(sounds, item => item.name == name);

		//Print error wanneer er geen Sound is gevonden
		if (sound == null)
		{
			print("Sound: " + sound.name + " not found! Did you spell it correctly?");
			return false;
		}

		return true;
	}
}

#region als we willen dat geluid niet afgekapt wordt tussen scenes
//public static AudioManager instance;

//Awake()
//if (instance == null)
//	instance = this;
//else
//{
//	Destroy(gameObject);
//	return;
//}
//DontDestroyOnLoad(gameObject);
#endregion