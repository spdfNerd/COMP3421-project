using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour {

	public void LoadLevel(string sceneName) {
		LoadScene(sceneName);
	}

	public void LoadLevelSelect() {
		LoadScene("LevelSelect");
	}

	public void LoadCredits() {
		LoadScene("CreditScene");
	}

	public void QuitGame() {
		Application.Quit();
	}

	/// <summary>
	/// Utility wrapper function to make it easier to call the internal engine LoadScene() function while using this class
	/// </summary>
	/// <param name="sceneName">Name or path of the Scene to load</param>
	/// <param name="mode">Allows you to specify whether or not to load the Scene additively.
	/// See SceneManagement.LoadSceneMode for more information about the options
	/// </param>
	public static void LoadScene(string sceneName, LoadSceneMode mode = LoadSceneMode.Single) {
		UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName, mode);
	}

}
