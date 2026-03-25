using UnityEngine;
using UnityEngine.UI;

public class FeedMask : MonoBehaviour
{
	public Image mask;

	public Color color;

	public float waitTime = 1f;

	public float fadeTime = 2f;

	public float currentTime;

	private void Start()
	{
		waitTime = 1f;
		currentTime = 0f;
		color.a = 1f;
		mask.color = color;
	}

	private void LateUpdate()
	{
		if (waitTime > 0f)
		{
			waitTime -= Time.deltaTime;
		}
		else if (currentTime < fadeTime)
		{
			currentTime += Time.deltaTime;
			if (currentTime > fadeTime)
			{
				base.gameObject.SetActive(value: false);
				return;
			}
			color.a = 1f - currentTime / fadeTime;
			mask.color = color;
		}
	}
}
