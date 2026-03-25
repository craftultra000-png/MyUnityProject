using UnityEngine;
using UnityEngine.Events;

public class GuildFeedLogo : MonoBehaviour
{
	public SpriteRenderer logo;

	public Color color;

	[Header("Satus")]
	public bool isFeedIn;

	public bool isFeedWait;

	public bool isFeedOut;

	[Header("Time")]
	private float currentTime;

	public float waitTime = 3f;

	[Header("On Enable Event")]
	[SerializeField]
	private UnityEvent m_FeedEnd = new UnityEvent();

	private void Start()
	{
	}

	private void OnEnable()
	{
		currentTime = 0f;
		color.a = 0f;
		logo.color = color;
		isFeedIn = true;
	}

	private void LateUpdate()
	{
		if (isFeedIn)
		{
			currentTime += Time.deltaTime;
			color.a = currentTime;
			if (currentTime > 1f)
			{
				color.a = 1f;
				currentTime = 0f;
				isFeedIn = false;
				isFeedWait = true;
			}
			logo.color = color;
		}
		else if (isFeedWait)
		{
			currentTime += Time.deltaTime;
			if (currentTime > waitTime)
			{
				currentTime = 0f;
				isFeedWait = false;
				isFeedOut = true;
			}
		}
		else if (isFeedOut)
		{
			currentTime += Time.deltaTime;
			color.a = 1f - currentTime;
			logo.color = color;
			if (currentTime >= 1f)
			{
				color.a = 0f;
				logo.color = color;
				isFeedOut = false;
				m_FeedEnd.Invoke();
			}
		}
	}
}
