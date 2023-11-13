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

	public static void LoadScene(string sceneName, LoadSceneMode mode = LoadSceneMode.Single) {
		UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName, mode);
	}

}
