using System.Collections.Generic;
using UnityEngine;

public class SlimeAdjustPosition : MonoBehaviour
{
	public List<Transform> baseBones;

	[Header("Target")]
	public Transform targetObject;

	public Transform rootObject;

	public Transform bendObject;

	[Header("Adjust")]
	public Vector3 adjustPositon;

	public float heightMin = 0.3f;

	public float heightMax = 1.6f;

	[Header("Calc")]
	public float calcBend;

	public AnimationCurve bendCurve;

	public List<Vector3> baseBonesPosition;

	[Header("Status")]
	public bool isAim;

	public bool isAimEnd;

	[Header("Aim Data")]
	public float aimMin;

	public float aimMax = 1f;

	[Space]
	public float aimCurrent;

	public float aimSpeed = 3f;

	[Header("Bend Object")]
	public FeelerNoisePosition _feelerNoisePosition;

	public Vector3 bendDefaultPositon;

	public Vector3 bendCalcPositon;

	[Header("Transform")]
	public Vector3 adjustTarget = new Vector3(0f, 0.06f, 0f);

	public Vector3 localCalcTargetPosition;

	public Vector3 worldCalcTargetPosition;

	public Vector3 defaultRotation = new Vector3(0f, -90f, -90f);

	public float rotationSpeed = 5f;

	private void Awake()
	{
		aimCurrent = aimMin;
		base.transform.localRotation = Quaternion.Euler(defaultRotation);
		baseBonesPosition.Clear();
		for (int i = 0; i < baseBones.Count; i++)
		{
			baseBonesPosition.Add(Vector3.zero);
		}
		bendDefaultPositon = _feelerNoisePosition.transform.localPosition;
	}

	private void LateUpdate()
	{
		if (isAim)
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
		else
		{
			isAimEnd = false;
			if (aimCurrent > aimMin)
			{
				aimCurrent -= Time.deltaTime * aimSpeed;
				if (aimCurrent < aimMin)
				{
					aimCurrent = aimMin;
				}
			}
		}
		_feelerNoisePosition.aimCurrent = aimCurrent;
		localCalcTargetPosition = targetObject.localPosition + adjustTarget;
		worldCalcTargetPosition = targetObject.parent.TransformPoint(localCalcTargetPosition);
		base.transform.position = Vector3.Lerp(rootObject.position, worldCalcTargetPosition, aimCurrent);
		bendCalcPositon = bendDefaultPositon * aimCurrent;
		_feelerNoisePosition.defaultPosition = bendCalcPositon;
		Vector3 normalized = (targetObject.position - base.transform.position).normalized;
		Vector3 up = Vector3.up;
		Quaternion quaternion = Quaternion.LookRotation(normalized, up);
		Quaternion quaternion2 = Quaternion.Euler(defaultRotation);
		Quaternion b = quaternion * quaternion2;
		base.transform.rotation = Quaternion.Slerp(base.transform.rotation, b, Time.deltaTime * rotationSpeed);
		Vector3 b2 = base.transform.position + adjustPositon;
		b2.y = Mathf.Clamp(b2.y, heightMin, heightMax);
		for (int i = 0; i < baseBones.Count; i++)
		{
			float time = (float)i / (float)(baseBones.Count - 1);
			calcBend = bendCurve.Evaluate(time);
			Vector3 a = Vector3.Lerp(rootObject.position, bendObject.position, calcBend);
			Vector3 b3 = Vector3.Lerp(bendObject.position, b2, calcBend);
			Vector3 value = Vector3.Lerp(a, b3, calcBend);
			baseBonesPosition[i] = value;
		}
		for (int j = 0; j < baseBones.Count; j++)
		{
			Vector3 zero = Vector3.zero;
			zero = ((j >= baseBones.Count - 1) ? (baseBonesPosition[j] - baseBonesPosition[j - 1]).normalized : (baseBonesPosition[j + 1] - baseBonesPosition[j]).normalized);
			Quaternion rotation = Quaternion.identity;
			_ = Quaternion.identity;
			if (zero != Vector3.zero)
			{
				if (j == 0)
				{
					rotation = Quaternion.LookRotation(zero, rootObject.forward);
					Quaternion.FromToRotation(zero, -rootObject.up);
				}
				else
				{
					rotation = Quaternion.LookRotation(zero, -baseBones[j - 1].forward);
					Quaternion.FromToRotation(zero, -baseBones[j - 1].up);
				}
			}
			rotation *= Quaternion.Euler(90f, 0f, 0f);
			baseBones[j].rotation = rotation;
			baseBones[j].position = baseBonesPosition[j];
		}
	}
}
