using System.Collections.Generic;
using UnityEngine;

public class FeelerMobShotObject : MonoBehaviour
{
	public List<Transform> baseBones;

	[Header("Target")]
	public Transform targetObject;

	public Transform rootObject;

	public Transform bendObject;

	[Header("Shot")]
	public Transform shotTarget;

	public Transform mobShotObject;

	[Header("Calc")]
	public float calcBend;

	public AnimationCurve bendCurve;

	public List<Vector3> baseBonesPosition;

	[Header("Noise Position")]
	public Vector3 targetCurrent;

	public Vector3 targetCalc;

	public Vector3 targetNoise = new Vector3(0.25f, 0.25f, 0.25f);

	public Vector3 noiseCalc;

	public float noiseSpeed = 0.5f;

	private void Start()
	{
		baseBonesPosition.Clear();
		for (int i = 0; i < baseBones.Count; i++)
		{
			baseBonesPosition.Add(Vector3.zero);
		}
	}

	private void LateUpdate()
	{
		targetCurrent = shotTarget.position;
		float num = Time.time * noiseSpeed;
		noiseCalc = new Vector3(Mathf.PerlinNoise(num, 0f) * 2f - 1f, Mathf.PerlinNoise(0f, num) * 2f - 1f, Mathf.PerlinNoise(num, num) * 2f - 1f);
		noiseCalc = Vector3.Scale(noiseCalc, targetNoise);
		targetCalc = targetCurrent + noiseCalc;
		Vector3 normalized = (targetCalc - mobShotObject.position).normalized;
		Quaternion quaternion = Quaternion.FromToRotation(mobShotObject.up, normalized);
		mobShotObject.rotation = quaternion * mobShotObject.rotation;
		for (int i = 0; i < baseBones.Count; i++)
		{
			float time = (float)i / (float)(baseBones.Count - 1);
			calcBend = bendCurve.Evaluate(time);
			Vector3 a = Vector3.Lerp(mobShotObject.position, bendObject.position, calcBend);
			Vector3 b = Vector3.Lerp(bendObject.position, targetObject.position, calcBend);
			Vector3 value = Vector3.Lerp(a, b, calcBend);
			baseBonesPosition[i] = value;
		}
		for (int j = 0; j < baseBones.Count; j++)
		{
			normalized = Vector3.zero;
			normalized = ((j >= baseBones.Count - 1) ? (baseBonesPosition[j] - baseBonesPosition[j - 1]).normalized : (baseBonesPosition[j + 1] - baseBonesPosition[j]).normalized);
			Quaternion identity = Quaternion.identity;
			_ = Quaternion.identity;
			if (j == 0)
			{
				identity = Quaternion.LookRotation(normalized, -baseBones[j].forward);
				Quaternion.FromToRotation(normalized, -baseBones[j].up);
			}
			else
			{
				identity = Quaternion.LookRotation(normalized, -baseBones[j - 1].forward);
				Quaternion.FromToRotation(normalized, -baseBones[j - 1].up);
			}
			identity *= Quaternion.Euler(90f, 0f, 0f);
			baseBones[j].rotation = identity;
			baseBones[j].position = baseBonesPosition[j];
		}
	}
}
