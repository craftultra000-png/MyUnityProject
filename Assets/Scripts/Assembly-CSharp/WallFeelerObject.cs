using System.Collections.Generic;
using UnityEngine;

public class WallFeelerObject : MonoBehaviour
{
	[Header("Target")]
	public List<Transform> wallFeeler;

	[Header("Data")]
	public List<Vector3> defaultRotation;

	public List<float> rotationZ;

	public List<float> scaleY;

	public List<float> targetRotationZ;

	public List<float> targetScaleY;

	[Header("Rotation")]
	public float rotationLimitMin = -20f;

	public float rotationLimitMax = 20f;

	public float rotationSpeed = 20f;

	[Header("Scale")]
	public float scaleLimitMin = 0.9f;

	public float scaleLimitMax = 1.1f;

	public float scaleSpeed = 0.5f;

	private void Start()
	{
		defaultRotation.Clear();
		rotationZ.Clear();
		scaleY.Clear();
		targetRotationZ.Clear();
		targetScaleY.Clear();
		for (int i = 0; i < wallFeeler.Count; i++)
		{
			defaultRotation.Add(wallFeeler[i].localRotation.eulerAngles);
			float item = Random.Range(rotationLimitMin, rotationLimitMax);
			float item2 = Random.Range(scaleLimitMin, scaleLimitMax);
			rotationZ.Add(item);
			scaleY.Add(item2);
			targetRotationZ.Add(item);
			targetScaleY.Add(item2);
			ApplyTransform(i);
		}
	}

	private void LateUpdate()
	{
		for (int i = 0; i < wallFeeler.Count; i++)
		{
			rotationZ[i] = Mathf.Lerp(rotationZ[i], targetRotationZ[i], Time.deltaTime * rotationSpeed);
			scaleY[i] = Mathf.Lerp(scaleY[i], targetScaleY[i], Time.deltaTime * scaleSpeed);
			ApplyTransform(i);
			if (Mathf.Abs(rotationZ[i] - targetRotationZ[i]) < 0.1f)
			{
				targetRotationZ[i] = Random.Range(rotationLimitMin, rotationLimitMax);
			}
			if (Mathf.Abs(scaleY[i] - targetScaleY[i]) < 0.01f)
			{
				targetScaleY[i] = Random.Range(scaleLimitMin, scaleLimitMax);
			}
		}
	}

	private void ApplyTransform(int value)
	{
		defaultRotation[value] = new Vector3(0f, defaultRotation[value].y, rotationZ[value]);
		wallFeeler[value].localRotation = Quaternion.Euler(defaultRotation[value]);
		Vector3 one = Vector3.one;
		one.y = scaleY[value];
		wallFeeler[value].localScale = one;
	}
}
