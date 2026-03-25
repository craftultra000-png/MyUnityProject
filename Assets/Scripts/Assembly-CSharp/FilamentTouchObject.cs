using System.Collections.Generic;
using UnityEngine;

public class FilamentTouchObject : MonoBehaviour
{
	public FeelerNoisePosition _feelerNoisePosition;

	public List<Transform> baseBones;

	[Header("Target")]
	public Transform targetObject;

	public Transform rootObject;

	public Transform bendObject;

	[Header("Calc")]
	public float calcBend;

	public AnimationCurve bendCurve;

	public List<Vector3> baseBonesPosition;

	[Header("Status")]
	public bool isSearch;

	public bool isAim;

	public bool isAimEnd;

	[Header("Aim Data")]
	public float aimMin = 0.2f;

	public float aimMax = 0.95f;

	[Space]
	public float aimCurrent;

	public float aimSpeed = 3f;

	[Header("Aim Position")]
	public float distance;

	public float calcDistance;

	[Header("Se")]
	public AudioClip aimSe;

	[Header("Rotation")]
	public Vector3 defaultRotation = new Vector3(0f, -90f, -90f);

	private void Start()
	{
		aimCurrent = aimMin;
		baseBonesPosition.Clear();
		for (int i = 0; i < baseBones.Count; i++)
		{
			baseBonesPosition.Add(Vector3.zero);
		}
	}

	private void LateUpdate()
	{
		if (isAim & isSearch)
		{
			if (!isAimEnd && aimCurrent < aimMax && aimCurrent < aimMax)
			{
				aimCurrent += Time.deltaTime * aimSpeed;
				if (aimCurrent > aimMax)
				{
					isAimEnd = true;
					aimCurrent = aimMax;
				}
			}
		}
		else if (aimCurrent > aimMin)
		{
			aimCurrent -= Time.deltaTime * aimSpeed;
			if (aimCurrent < aimMin)
			{
				isAimEnd = false;
				aimCurrent = aimMin;
			}
		}
		_feelerNoisePosition.aimCurrent = aimCurrent;
		base.transform.position = Vector3.Lerp(rootObject.position, targetObject.position, aimCurrent);
		Vector3 normalized = (targetObject.position - bendObject.position).normalized;
		Quaternion quaternion = Quaternion.FromToRotation(bendObject.up, normalized);
		baseBones[0].rotation = quaternion * baseBones[0].rotation;
		for (int i = 0; i < baseBones.Count; i++)
		{
			float time = (float)i / (float)(baseBones.Count - 1);
			calcBend = bendCurve.Evaluate(time);
			Vector3 a = Vector3.Lerp(targetObject.position, bendObject.position, calcBend);
			Vector3 b = Vector3.Lerp(bendObject.position, rootObject.position, calcBend);
			Vector3 value = Vector3.Lerp(a, b, calcBend);
			baseBonesPosition[i] = value;
		}
		for (int j = 0; j < baseBones.Count; j++)
		{
			normalized = ((j >= baseBones.Count - 1) ? (baseBonesPosition[j] - baseBonesPosition[j - 1]).normalized : (baseBonesPosition[j + 1] - baseBonesPosition[j]).normalized);
			Quaternion identity = Quaternion.identity;
			identity = ((j != 0) ? Quaternion.LookRotation(normalized, -baseBones[j - 1].forward) : Quaternion.LookRotation(normalized, -baseBones[j].forward));
			identity *= Quaternion.Euler(90f, 0f, 0f);
			baseBones[j].rotation = identity;
			baseBones[j].position = baseBonesPosition[j];
		}
	}
}
