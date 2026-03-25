using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoading : MonoBehaviour
{
	private AsyncOperation async;

	public Slider LoadGauge;

	public string NextScene;

	public bool LoadStart;

	public bool LoadEnd;

	public float waitTime = 1f;

	private void Start()
	{
		NextScene = ES3.Load<string>("NextScene");
		LoadStart = false;
		LoadEnd = false;
	}

	public void Update()
	{
		if (!LoadStart)
		{
			LoadStart = true;
			LoadGauge.value = 0f;
			async = SceneManager.LoadSceneAsync(NextScene);
			async.allowSceneActivation = false;
		}
		if (LoadStart && !LoadEnd)
		{
			LoadGauge.value = async.progress;
			if (async.progress >= 0.9f)
			{
				LoadGauge.value = 1f;
				LoadEnd = true;
				async.allowSceneActivation = true;
			}
		}
	}

	private IEnumerator ChangeWait()
	{
		yield return new WaitForSeconds(waitTime);
		async.allowSceneActivation = true;
	}
}
