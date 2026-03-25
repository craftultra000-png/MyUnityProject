using System.Collections.Generic;
using UnityEngine;

public class FeelerWave : MonoBehaviour
{
	public List<Transform> bones;

	[Header("Status")]
	public bool isWave;

	[Header("Rotation")]
	public float rotationSpeed = 10f;

	public float rotationEdgeSpeed = 0.5f;

	public float maxRotationAngle = 30f;

	public AnimationCurve rotationCurve;

	public List<Vector3> boneTargetRotation;

	public List<Vector3> boneCurrentRotation;

	public Vector3 calcRotation;

	[Header("Strech")]
	public float stretchSpeed = 1f;

	public float minStretch = 0.9f;

	public float maxStretch = 1.1f;

	public AnimationCurve stretchCurve;

	public List<Vector3> stretchDefault;

	public List<Vector3> stretchTarget;

	public List<Vector3> stretchCurrent;

	public Vector3 calcStretch;

	private void Start()
	{
		boneTargetRotation.Clear();
		boneCurrentRotation.Clear();
		stretchDefault.Clear();
		stretchTarget.Clear();
		stretchCurrent.Clear();
		for (int i = 0; i < bones.Count; i++)
		{
			boneTargetRotation.Add(bones[i].localRotation.eulerAngles);
			boneCurrentRotation.Add(bones[i].localRotation.eulerAngles);
			stretchDefault.Add(bones[i].localPosition);
			stretchTarget.Add(bones[i].localPosition);
			stretchCurrent.Add(bones[i].localPosition);
		}
	}

	private void FixedUpdate()
	{
		for (int i = 0; i < bones.Count; i++)
		{
			Transform obj = bones[i];
			calcRotation = boneTargetRotation[i];
			if (Mathf.Abs(boneCurrentRotation[i].x - boneTargetRotation[i].x) < 1f)
			{
				float num = maxRotationAngle * rotationCurve.Evaluate((bones.Count - i) / bones.Count);
				calcRotation.x = Random.Range(0f - num, num);
				if ((calcRotation.x > 0f && boneCurrentRotation[i].x > 0f) || (calcRotation.x < 0f && boneCurrentRotation[i].x < 0f))
				{
					calcRotation.x = 0f - calcRotation.x;
				}
			}
			if (Mathf.Abs(boneCurrentRotation[i].z - boneTargetRotation[i].z) < 1f)
			{
				float num2 = maxRotationAngle * rotationCurve.Evaluate((bones.Count - i) / bones.Count);
				calcRotation.z = Random.Range(0f - num2, num2);
				if ((calcRotation.z > 0f && boneCurrentRotation[i].z > 0f) || (calcRotation.z < 0f && boneCurrentRotation[i].z < 0f))
				{
					calcRotation.z = 0f - calcRotation.z;
				}
			}
			boneTargetRotation[i] = calcRotation;
			float num3 = (float)(bones.Count - (bones.Count - i)) * rotationEdgeSpeed;
			boneCurrentRotation[i] = Vector3.Lerp(boneCurrentRotation[i], boneTargetRotation[i], Time.deltaTime * rotationSpeed * num3);
			obj.localRotation = Quaternion.Euler(boneCurrentRotation[i]);
			calcStretch = stretchTarget[i];
			if (Mathf.Abs(stretchCurrent[i].y - stretchTarget[i].y) < 0.01f)
			{
				float y = Random.Range(minStretch, maxStretch) * stretchDefault[i].y;
				stretchTarget[i] = new Vector3(stretchCurrent[i].x, y, stretchCurrent[i].z);
			}
			stretchCurrent[i] = Vector3.Lerp(stretchCurrent[i], stretchTarget[i], Time.deltaTime * stretchSpeed);
			obj.localPosition = stretchCurrent[i];
		}
	}
}
