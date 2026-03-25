using UnityEngine;
using UnityEngine.UI;

public class FeedOutMask : MonoBehaviour
{
	public Image mask;

	public Color color;

	public float currentTime;

	private void Start()
	{
		currentTime = 0f;
		color.a = 0f;
		mask.color = color;
	}

	private void OnEnable()
	{
		currentTime = 0f;
		color.a = 0f;
		mask.color = color;
	}

	private void LateUpdate()
	{
		if (currentTime < 1f)
		{
			currentTime += Time.deltaTime;
			if (currentTime > 1f)
			{
				currentTime = 1f;
			}
			color.a = currentTime;
			mask.color = color;
		}
	}
}
