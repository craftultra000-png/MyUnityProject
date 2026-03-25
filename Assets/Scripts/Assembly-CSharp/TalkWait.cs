using UnityEngine;
using Utage;

public class TalkWait : MonoBehaviour
{
	public EllipsisObject _ellipsisObject;

	[Header("Utage")]
	public AdvUguiManager uguiManager;

	[Header("Status")]
	public bool isStartText;

	public bool isEndText;

	[Header("WaitTime")]
	public bool isSkip;

	public bool isWait;

	public float waitTime;

	public float waitTimeMax = 3f;

	[Header("Audio")]
	public AudioSource _audioSource;

	public AudioClip talkSe;

	private void Awake()
	{
		InputUtil.EnableInput = false;
		isEndText = true;
	}

	private void Update()
	{
		if (waitTime > 0f || isSkip || isEndText)
		{
			waitTime -= Time.deltaTime;
			if (waitTime < 0f)
			{
				isSkip = false;
				waitTime = 0f;
				isEndText = true;
				uguiManager.OnInput();
			}
		}
	}

	public void ReleaseUpdateText()
	{
		if (isEndText)
		{
			isStartText = false;
			isEndText = false;
			_audioSource.PlayOneShot(talkSe);
			_ellipsisObject.gameObject.SetActive(value: false);
		}
	}

	public void TextWait()
	{
		if (!isStartText)
		{
			waitTime = waitTimeMax;
			_ellipsisObject.waitTimeMax = waitTimeMax;
			_ellipsisObject.gameObject.SetActive(value: true);
		}
		isStartText = true;
	}

	public void SetTextWaitTime()
	{
		if (!isStartText)
		{
			Debug.LogError("Text SetTextWaitTime");
			isWait = false;
			waitTime = waitTimeMax;
			_ellipsisObject.waitTimeMax = waitTimeMax;
			_ellipsisObject.gameObject.SetActive(value: true);
		}
		isStartText = true;
	}

	public void SetTextWait()
	{
		if (!isStartText)
		{
			Debug.LogError("Text SetTextWait");
			isWait = true;
			waitTime = waitTimeMax;
			_ellipsisObject.waitTimeMax = waitTimeMax;
			_ellipsisObject.gameObject.SetActive(value: true);
		}
		isStartText = true;
	}
}
