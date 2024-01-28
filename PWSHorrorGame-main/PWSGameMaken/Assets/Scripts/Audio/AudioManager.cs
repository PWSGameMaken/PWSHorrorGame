using System;
using System.Collections;
using UnityEngine;

//Mogelijk inplaats van syntax gevoelige strings te gebruiken
public enum AudioType
{
	MonsterRun,
	MonsterScream,
	MonsterSlah,

	Ambiance,

	PlayerFootsteps,

	EarthQuake,

	MiniMonsterFootsteps,
	MiniMonsterScream
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

	private void Start()
	{
		Play("Ambiance", gameObject);
	}

	private void SetAudioStats(Sound sound, GameObject emitter)
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

		//s.source.outputAudioMixedGrouop = mixerGroup;
		//s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		//s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));
	}

	public void Play(string name, GameObject emitter)
	{
		Sound s = Array.Find(sounds, sound => sound.name == name);
		if (s == null) 
		{ 
			print("Sound: " + name + " not found!"); 
			return; 
		}

		SetAudioStats(s, emitter);
		s.source.Play();
	}

	public void Stop(string name)
	{
		Sound s = Array.Find(sounds, sound => sound.name == name);
		if (s == null)
		{
			print("Sound: " + name + " not found!");
			return;
		}
		s.source.Stop();
	}

	public void PlayOneShot(string name, GameObject emitter) //For Sound Effects
	{
		Sound sound = Array.Find(sounds, item => item.name == name);

		//if there there is no sound specified by the string, throw an error
		if (sound == null)
		{
			print("Sound: " + sound.name + " not found! Did you spell it correctly?");
			return;
		}

		SetAudioStats(sound, emitter);
		sound.source.PlayOneShot(sound.clip);

		StartCoroutine(DestroyAudioSource(sound.source));
	}

	private IEnumerator DestroyAudioSource(AudioSource source)
	{
		yield return new WaitForSeconds(source.clip.length);
		Destroy(source);
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