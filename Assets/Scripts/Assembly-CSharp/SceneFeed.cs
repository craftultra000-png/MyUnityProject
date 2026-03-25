using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SceneFeed : MonoBehaviour
{
	[Header("Scene Change")]
	public SceneChange _sceneChange;

	public string NextSceneName;

	public bool restart;

	[Header("Feed Calc")]
	public Image feedMask;

	public GameObject clickMask;

	public bool feedIn;

	public bool feedOut;

	public bool isFeedOut;

	public bool feedStart;

	[Header("Feed Calc")]
	private Color _color;

	public float speed = 1f;

	public float time;

	[Header("On FeedIn Event")]
	[SerializeField]
	private UnityEvent m_FeedInEnd = new UnityEvent();

	[Header("On FeedOut Event")]
	[SerializeField]
	private UnityEvent m_FeedOutEnd = new UnityEvent();

	private void Start()
	{
		_color = feedMask.color;
		FeedIn();
	}

	private void FixedUpdate()
	{
		if (feedIn && feedStart)
		{
			time -= Time.deltaTime * speed;
			_color.a = time;
			feedMask.color = _color;
			if (time <= 0f)
			{
				m_FeedInEnd.Invoke();
				feedIn = false;
				feedStart = false;
				clickMask.SetActive(value: false);
			}
		}
		if (!feedOut || !feedStart)
		{
			return;
		}
		time += Time.unscaledDeltaTime * speed;
		_color.a = time;
		feedMask.color = _color;
		if (time >= 1f)
		{
			m_FeedOutEnd.Invoke();
			feedOut = false;
			feedStart = false;
			if (restart)
			{
				_sceneChange.RestartScene();
			}
			else if (!restart)
			{
				_sceneChange.NextScene(NextSceneName);
			}
		}
	}

	public void FeedIn()
	{
		Time.timeScale = 1f;
		feedIn = true;
		feedOut = false;
		feedStart = true;
		_color.a = 1f;
		time = 1f;
		feedMask.color = _color;
		clickMask.SetActive(value: true);
	}

	public void FeedInWait()
	{
		_color.a = 1f;
		feedMask.color = _color;
		clickMask.SetActive(value: true);
	}

	public void FeedOut(string value)
	{
		if (!isFeedOut)
		{
			restart = false;
			NextSceneName = value;
			Time.timeScale = 1f;
			feedIn = false;
			feedOut = true;
			feedStart = true;
			_color.a = 0f;
			time = 0f;
			isFeedOut = true;
			clickMask.SetActive(value: true);
		}
	}

	public void FeedOutRestart()
	{
		if (!isFeedOut)
		{
			restart = true;
			Time.timeScale = 1f;
			feedIn = false;
			feedOut = true;
			feedStart = true;
			_color.a = 0f;
			time = 0f;
			isFeedOut = true;
			clickMask.SetActive(value: true);
		}
	}
}
