using System.Collections.Generic;
using UnityEngine;

public class OnomatopoeiaBalloonManager : MonoBehaviour
{
	public static OnomatopoeiaBalloonManager instance;

	public Transform shotStocker;

	[Header("Onomatopoeia")]
	public GameObject onomatopoeia;

	[Header("Status")]
	public bool useOtomanopoeia = true;

	[Header("Onomatopoeia Emotion")]
	public Color defaultColor;

	public Color heartColor;

	[Space]
	public Color sukllColor;

	[Header("Onomatopoeia Sprite")]
	public Sprite heart;

	public Sprite heart2;

	public Sprite heart3;

	public Sprite threeDot;

	public List<Sprite> normal;

	public List<Sprite> hard;

	public List<Sprite> veryHard;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
	}

	public void SpawnOnomatopoeia(Vector3 position, Transform parent, string type, Transform lookTarget)
	{
		if (useOtomanopoeia)
		{
			GameObject gameObject = Object.Instantiate(onomatopoeia, position, Quaternion.identity, shotStocker.transform);
			OnomatopoeiaBalloonObject component = gameObject.GetComponent<OnomatopoeiaBalloonObject>();
			component.lookTarget = lookTarget;
			if (parent == null)
			{
				gameObject.transform.parent = shotStocker;
			}
			else
			{
				gameObject.transform.parent = parent;
			}
			component.floatSpeed = Random.Range(0.025f, 0.05f);
			switch (type)
			{
			case "Heart":
				component.currentTime = 1.5f;
				component.isBomb = true;
				component.iconScript._sprite = heart;
				component.iconScript._tex = heart.texture;
				component.icon.color = heartColor;
				break;
			case "Heart2":
				component.currentTime = 2f;
				component.isFloat = true;
				component.iconScript._sprite = heart;
				component.iconScript._tex = heart2.texture;
				component.icon.color = heartColor;
				break;
			case "Heart3":
				component.currentTime = 2f;
				component.isFloat = true;
				component.iconScript._sprite = heart;
				component.iconScript._tex = heart3.texture;
				component.icon.color = heartColor;
				break;
			case "Breath":
				component.currentTime = 1.5f;
				component.isSway = true;
				component.iconScript._sprite = threeDot;
				component.iconScript._tex = threeDot.texture;
				component.icon.color = defaultColor;
				break;
			case "ThreeDot":
			{
				component.currentTime = 1.5f;
				component.isSway = true;
				Sprite sprite = normal[Random.Range(0, normal.Count)];
				component.iconScript._sprite = sprite;
				component.iconScript._tex = sprite.texture;
				component.icon.color = defaultColor;
				break;
			}
			default:
				Debug.LogError("Onomatopoeia Type Error: " + type);
				break;
			}
		}
	}
}
