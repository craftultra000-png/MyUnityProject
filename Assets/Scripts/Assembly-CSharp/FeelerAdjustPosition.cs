using System;
using UnityEngine;

public class FeelerAdjustPosition : MonoBehaviour
{
	public Transform targetPosition;

	[Header("Stauts")]
	public bool isRotation;

	public bool isNoise = true;

	[Header("Position")]
	public Vector3 defaultPosition;

	public float lerpSpeed = 2f;

	public Vector3 calcPosition;

	[Header("Adjust")]
	public Vector3 adjustPositon;

	public bool isOtherAdjust;

	public Vector3 otherAdjustPositon;

	public Vector3 calcAdjustPositon;

	public float adjustLerpSpeed = 2f;

	[Header("Rotation Adjust")]
	public Vector3 rotationOffsetUp;

	public Vector3 rotationOffsetDown;

	public Vector3 adjustRotation;

	public Vector3 calcAdjustRotation;

	public float rotationAngle;

	[Header("Noise")]
	public Vector3 noiseData = new Vector3(0.1f, 0.05f, 0.1f);

	public float noiseSpeed = 2f;

	public Vector3 currentNoise;

	public Vector3 targetNoise;

	[Header("Height Limit")]
	public float heightMin = 0.3f;

	public float heightMax = 1.6f;

	private void Start()
	{
		defaultPosition = targetPosition.position;
		base.transform.position = defaultPosition + adjustPositon;
		calcPosition = base.transform.position;
		calcAdjustPositon = adjustPositon;
		currentNoise = Vector3.zero;
		targetNoise = GenerateRandomNoise();
	}

	private void LateUpdate()
	{
		if (isNoise)
		{
			currentNoise = Vector3.Lerp(currentNoise, targetNoise, Time.deltaTime * noiseSpeed);
			if (Vector3.Distance(currentNoise, targetNoise) < 0.01f)
			{
				targetNoise = GenerateRandomNoise();
			}
		}
		calcPosition = Vector3.Lerp(calcPosition, targetPosition.position, Time.deltaTime * lerpSpeed);
		adjustRotation = Vector3.zero;
		if (isRotation)
		{
			float num = Mathf.Cos(Vector3.Angle(targetPosition.forward, Vector3.up) * (MathF.PI / 180f)) * 1f;
			Vector3 vector = ((!(num >= 0f)) ? rotationOffsetDown : rotationOffsetUp);
			rotationAngle = Mathf.Abs(num);
			Vector3 position = vector * rotationAngle * Mathf.Sign(num);
			Vector3 vector2 = targetPosition.TransformPoint(position);
			adjustRotation = vector2 - targetPosition.position;
		}
		Vector3 b = ((!isOtherAdjust) ? adjustPositon : otherAdjustPositon);
		b += adjustRotation;
		calcAdjustPositon = Vector3.Lerp(calcAdjustPositon, b, Time.deltaTime * adjustLerpSpeed);
		Vector3 localPosition = calcPosition + calcAdjustPositon + currentNoise + adjustRotation;
		localPosition.y = Mathf.Clamp(localPosition.y, heightMin, heightMax);
		base.transform.localPosition = localPosition;
	}

	private Vector3 GenerateRandomNoise()
	{
		return new Vector3(UnityEngine.Random.Range(0f - noiseData.x, noiseData.x), UnityEngine.Random.Range(0f - noiseData.y, noiseData.y), UnityEngine.Random.Range(0f - noiseData.z, noiseData.z));
	}
}
