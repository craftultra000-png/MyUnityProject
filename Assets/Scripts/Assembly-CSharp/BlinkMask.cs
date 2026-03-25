using UnityEngine;
using UnityEngine.UI;

public class BlinkMask : MonoBehaviour
{
	public Image mask;

	public Color color;

	[Header("Blink")]
	public bool isBlink;

	public bool isBlinkEnd;

	public float fadeTime = 2f;

	public float currentTime;

	private void Start()
	{
		currentTime = 0f;
		isBlinkEnd = true;
		color.a = 0f;
		mask.color = color;
	}

	private void LateUpdate()
	{
		if (isBlinkEnd)
		{
			return;
		}
		if (isBlink)
		{
			if (currentTime < fadeTime)
			{
				currentTime += Time.deltaTime;
				if (currentTime > fadeTime)
				{
					currentTime = fadeTime;
				}
				color.a = currentTime / fadeTime;
				mask.color = color;
			}
		}
		else if (currentTime < fadeTime)
		{
			currentTime += Time.deltaTime;
			if (currentTime > fadeTime)
			{
				currentTime = fadeTime;
			}
			color.a = 1f - currentTime / fadeTime;
			mask.color = color;
		}
	}

	public void FeedIn()
	{
		currentTime = 0f;
		isBlink = true;
		isBlinkEnd = false;
	}

	public void FeedOut()
	{
		currentTime = 0f;
		isBlink = false;
		isBlinkEnd = false;
	}
}
