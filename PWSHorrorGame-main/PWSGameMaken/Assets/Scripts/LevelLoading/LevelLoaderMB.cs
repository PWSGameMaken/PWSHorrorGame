using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum LevelName
{
	Playground,
	StartMenu,
	OptionsMenu
}

public class LevelLoaderMB : MonoBehaviour
{
	public GameObject loadingScreen;
	public Slider slider;
	public TextMeshProUGUI progressText;

	public void LoadPlayground()
	{
		StartCoroutine(LoadAsynchronously(LevelName.Playground));
	}

	public void ExitGame()
	{
		//Kan zijn dat deze code nog niet in browserapplicatie werkt, moet je even checken.
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#endif
		Application.Quit();
	}

	public void LoadStartMenu()
	{
		StartCoroutine(LoadAsynchronously(LevelName.StartMenu));
	}

	public void LoadOptions()
	{
		StartCoroutine(LoadAsynchronously(LevelName.OptionsMenu));
	}

	IEnumerator LoadAsynchronously (LevelName sceneToLoad)
	{
		Time.timeScale = 1;

		AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad.ToString());

		loadingScreen.SetActive(true);

		while (!operation.isDone)
		{
			float progress = Mathf.Clamp01(operation.progress / 0.9f);
			slider.value = progress;
			progressText.text = (progress * 100).ToString("f1") + "%";

			yield return null;
		}
	}
}
