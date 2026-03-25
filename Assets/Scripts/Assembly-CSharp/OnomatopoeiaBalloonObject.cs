using System;
using UnityEngine;

public class OnomatopoeiaBalloonObject : MonoBehaviour
{
	public Transform lookTarget;

	[Header("Image")]
	public SpriteRenderer icon;

	public SpriteRenderer balloon;

	public Material iconMaterial;

	public Material baloonMaterial;

	public Color iconColor;

	public Color balloonColor;

	public StageGUIMaterialSync iconScript;

	public StageGUIMaterialSync balloonScript;

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
		iconColor = icon.color;
		balloonColor = balloon.color;
		iconScript._color = iconColor;
		balloonScript._color = balloonColor;
		Vector3 vector = new Vector3(UnityEngine.Random.Range(-0.1f, 0.1f), UnityEngine.Random.Range(0.15f, 0.2f), UnityEngine.Random.Range(-0.1f, 0.1f));
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
			iconColor.a = currentTime;
		}
		else
		{
			iconColor.a = 1f;
		}
		if (currentTime < 1f)
		{
			balloonColor.a = currentTime;
		}
		else
		{
			balloonColor.a = 1f;
		}
		icon.color = iconColor;
		balloon.color = balloonColor;
		iconScript._color = iconColor;
		balloonScript._color = balloonColor;
		if (currentTime <= 0f)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}
}
