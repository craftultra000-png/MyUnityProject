using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
	[Header("Scene Change")]
	public string NextSceneName;

	public string LoadingScene = "Loading";

	public void Start()
	{
		ES3.Save("NextScene", "Title");
	}

	public void NextScene(string value)
	{
		NextSceneName = value;
		Debug.Log("Next Scene Name: " + NextSceneName);
		ES3.Save("NextScene", NextSceneName);
		SceneManager.LoadScene(NextSceneName);
	}

	public void NextSceneUseString(string value)
	{
		Debug.Log("Next Scene Name: " + value);
		ES3.Save("NextScene", value);
		SceneManager.LoadScene(LoadingScene);
	}

	public void NextSceneNoneLoad()
	{
		SceneManager.LoadScene(NextSceneName);
	}

	public void RestartScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
