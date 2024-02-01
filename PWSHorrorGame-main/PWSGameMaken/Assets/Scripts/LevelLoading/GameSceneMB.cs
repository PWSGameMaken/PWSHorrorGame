using UnityEngine;

public class GameSceneMB : MonoBehaviour
{
	void Start()
	{
		//Cursor.lockState = CursorLockMode.None;
		Cursor.visible = false;
		AudioManager.instance.Play(OtherAudio.Ambiance.ToString(), gameObject);
	}
}
