using UnityEngine;
using UnityEngine.UI;

public class BlinkText : MonoBehaviour
{
	public float blinkSpeed = 0.5f;

	private Text text;

	public float time;

	private void Start()
	{
		text = GetComponent<Text>();
	}

	private void Update()
	{
		text.color = GetAlphaColor(text.color);
	}

	private void OnEnable()
	{
		time = 0f;
	}

	private void OnDisable()
	{
		time = 0f;
		text.color = new Color(1f, 1f, 1f, 0f);
	}

	private Color GetAlphaColor(Color color)
	{
		time += Time.deltaTime * blinkSpeed;
		color.a = Mathf.PingPong(time, 1f);
		return color;
	}
}
