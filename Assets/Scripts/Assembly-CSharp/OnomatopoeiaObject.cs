using System;
using TMPro;
using UnityEngine;
using Utage;

public class OnomatopoeiaObject : MonoBehaviour
{
	public Transform lookTarget;

	[Header("Text")]
	public TextMeshPro text;

	public Material material;

	public Color color;

	public float fontSize = 0.5f;

	[Header("Language")]
	public string lang;

	public string langBase;

	private LanguageManagerBase langManager;

	[Header("Hear")]
	public int heartCount;

	public int heartType;

	public float heartCalc;

	[Header("Type")]
	public bool isFloat;

	public bool isBomb;

	public bool isBounce;

	public bool isSway;

	[Header("Move")]
	public float currentTime = 1f;

	public float floatSpeed = 0.1f;

	public float tiltAngle = 15f;

	public Vector3 defaultPosition;

	[Header("Scale")]
	public float currentScale = 1f;

	public Vector3 defaultScale;

	[Header("Rotation")]
	public float swatyRotation;

	[Header("Calc")]
	private float spawnTime;

	private float calcTime;

	public float bombSpeed = 2f;

	public float swaySpeed = 2f;

	public float bombPower = 0.5f;

	public float bouncePower = 0.01f;

	public float swayPwer = 15f;

	[Header("Onomatopoeia")]
	public string key;

	private void Start()
	{
		color = text.color;
		langManager = LanguageManagerBase.Instance;
		langBase = langManager.CurrentLanguage;
		if (lang == "English")
		{
			langManager.CurrentLanguage = "English";
			text.text = langManager.LocalizeText(key);
		}
		else if (lang == "Japanese")
		{
			langManager.CurrentLanguage = "Japanese";
			text.text = langManager.LocalizeText(key);
		}
		langManager.CurrentLanguage = langBase;
		if (heartCount > 0)
		{
			heartType = UnityEngine.Random.Range(0, 2);
			for (int i = 1; i < heartCount; i++)
			{
				heartCalc = UnityEngine.Random.value;
				if (UnityEngine.Random.value < 0.3f && heartType == 0)
				{
					text.text += "♥";
				}
				else if (UnityEngine.Random.value < 0.3f && heartType == 1)
				{
					text.text += "♡";
				}
			}
		}
		Vector3 vector = new Vector3(UnityEngine.Random.Range(-0.02f, 0.02f), UnityEngine.Random.Range(0.005f, 0.015f), UnityEngine.Random.Range(-0.02f, 0.02f));
		base.transform.position += vector;
		currentTime *= UnityEngine.Random.Range(0.8f, 1.2f);
		floatSpeed *= UnityEngine.Random.Range(0.8f, 1.2f);
		tiltAngle *= UnityEngine.Random.Range(-1f, 1f);
		spawnTime = Time.time;
		defaultPosition = base.transform.position;
		defaultScale = base.transform.localScale;
	}

	private void LateUpdate()
	{
		Vector3 vector = base.transform.position - lookTarget.transform.position;
		if (vector != Vector3.zero)
		{
			Quaternion quaternion = Quaternion.LookRotation(vector.normalized, lookTarget.up);
			Quaternion quaternion2 = Quaternion.AngleAxis(tiltAngle + swatyRotation, lookTarget.transform.forward);
			base.transform.rotation = quaternion * quaternion2;
		}
		if (isFloat)
		{
			base.transform.position += base.transform.up * floatSpeed * Time.deltaTime;
		}
		if (isBomb)
		{
			calcTime = Time.time - spawnTime;
			float num = 1f + Mathf.Sin(Mathf.Clamp01(calcTime * bombSpeed) * MathF.PI) * bombPower;
			base.transform.localScale = defaultScale * num;
		}
		if (isBounce)
		{
			calcTime = Time.time - spawnTime;
			float y = Mathf.Sin(calcTime * bombSpeed) * bouncePower;
			base.transform.position = defaultPosition + new Vector3(0f, y, 0f);
		}
		if (isSway)
		{
			calcTime = Time.time - spawnTime;
			swatyRotation = Mathf.Sin(swaySpeed * calcTime) * swayPwer;
		}
		currentTime -= Time.deltaTime;
		if (currentTime < 0f)
		{
			currentTime = 0f;
		}
		if (currentTime < 1f)
		{
			color.a = currentTime;
		}
		else
		{
			color.a = 1f;
		}
		text.color = color;
		if (currentTime <= 0f)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}
}
