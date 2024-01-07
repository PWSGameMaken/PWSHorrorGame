using UnityEngine;

public class PlaySounds : MonoBehaviour
{
	[SerializeField] private Audio[] sounds;
	public void ActivateSounds(bool activeState)
	{
		foreach (var sound in sounds)
		{
			sound.PlaySound(activeState);
		}
	}
}
