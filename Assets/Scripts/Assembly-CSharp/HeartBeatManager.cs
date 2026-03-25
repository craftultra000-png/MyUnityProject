using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UtageExtensions;

public class HeartBeatManager : MonoBehaviour
{
	public GameObject heartBeatObject;

	public Image heartBeatImage;

	public Material heartBeatMaterial;

	[Header("Data")]
	[Range(0f, 1f)]
	public float currentSpeed;

	[Range(0f, 1f)]
	public float currentBeat;

	[Range(0f, 1f)]
	public float currentSize;

	[Range(0f, 1f)]
	public float currentColor;

	[Header("Damping")]
	public float speedDamping = 0.1f;

	public float beatDamping = 0.1f;

	[Range(0f, 5f)]
	public float beatSizeDamping = 2f;

	public float colorDamping = 0.1f;

	[Header("Wave")]
	public float waveTime;

	[Range(0f, 1f)]
	public float waveSpeed = 0.1f;

	public float waveCount = 8f;

	[Header("Beat")]
	public float beatTime;

	[Range(0f, 15f)]
	public float beatSpeed = 2f;

	[Range(0f, 0.4f)]
	public float beatSize;

	public float beatSizeMin = 0.05f;

	public float beatSizeMax = 0.4f;

	public float beatSizeCalc;

	public float beatSizeVelocity;

	[Header("Color")]
	public Gradient gradientColor;

	public Gradient gradientColorTMP;

	public Color calcColor;

	[Header("RectTransform")]
	public int heartBeatType;

	public RectTransform graphPosition;

	public RectTransform graphSize;

	public List<Vector3> setPosition;

	public List<Vector2> setSize;

	private void FixedUpdate()
	{
		if (heartBeatMaterial == null)
		{
			heartBeatMaterial = heartBeatImage.material;
			heartBeatImage.material = heartBeatMaterial;
			heartBeatMaterial.SetFloat("_WaveSpeed", 0.1f);
			heartBeatMaterial.SetFloat("_WaveTime", waveTime);
			heartBeatMaterial.SetFloat("_BeatSize", beatSize);
		}
		currentBeat -= Time.deltaTime * beatDamping;
		if (currentBeat < 0f)
		{
			currentBeat = 0f;
		}
		currentColor -= Time.deltaTime * colorDamping;
		if (currentColor < 0f)
		{
			currentColor = 0f;
		}
		waveTime += Time.deltaTime;
		if (waveTime > 100f)
		{
			waveTime -= 100f;
		}
		heartBeatMaterial.SetFloat("_WaveTime", waveTime);
		beatSpeed = Mathf.Lerp(1f, 10f, currentBeat);
		beatTime += Time.deltaTime * beatSpeed;
		beatTime = Mathf.Repeat(beatTime, MathF.PI * 2f);
		float f = Mathf.Sin(beatTime);
		f = Mathf.Abs(f);
		currentSize = Mathf.Lerp(currentSize, 0f, beatSizeDamping * Time.deltaTime);
		currentSize = Mathf.Clamp01(currentSize);
		beatSize = Mathf.Lerp(beatSizeMin, beatSizeMax, currentSize);
		beatSizeCalc = f * beatSize;
		heartBeatMaterial.SetFloat("_BeatSize", beatSizeCalc);
		calcColor = gradientColor.Evaluate(currentColor);
		heartBeatMaterial.SetColor("_BeatColor", calcColor);
	}

	public void Beat(float damage, float speed, float beat, float color)
	{
		currentSize += damage;
		if (currentSize > 1f)
		{
			currentSize = 1f;
		}
		currentSpeed += speed;
		if (currentSpeed > 1f)
		{
			currentSpeed = 1f;
		}
		currentBeat += beat;
		if (currentBeat > 1f)
		{
			currentBeat = 1f;
		}
		currentColor += color;
		if (currentColor > 1f)
		{
			currentColor = 1f;
		}
	}

	public void SetHeartBeat(bool value, int type)
	{
		heartBeatType = type;
		heartBeatObject.SetActive(value);
		if (heartBeatType == 1)
		{
			graphPosition.localPosition = setPosition[0];
			graphSize.SetWidth(setSize[0].x);
			graphSize.SetHeight(setSize[0].y);
		}
		else if (heartBeatType == 2)
		{
			graphPosition.localPosition = setPosition[1];
			graphSize.SetWidth(setSize[1].x);
			graphSize.SetHeight(setSize[1].y);
		}
	}

	public void DisableHeartBeat()
	{
		if (heartBeatType != 1)
		{
			heartBeatObject.SetActive(value: false);
		}
		else if (heartBeatType == 1)
		{
			heartBeatObject.SetActive(value: true);
		}
	}
}
