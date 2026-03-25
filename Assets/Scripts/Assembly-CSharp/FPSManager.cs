using UnityEngine;
using UnityEngine.UI;

public class FPSManager : MonoBehaviour
{
	public Text fpsText;

	public float fps;

	public float waitTime;

	public float waitTimeMax = 1f;

	private void Awake()
	{
		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = 60;
	}

	private void Start()
	{
		waitTime = waitTimeMax;
	}

	private void Update()
	{
		waitTime -= Time.deltaTime;
		if (waitTime < 0f)
		{
			waitTime = waitTimeMax;
			fps = 1f / Time.deltaTime;
			fpsText.text = "FPS: " + Mathf.Ceil(fps);
		}
	}
}
