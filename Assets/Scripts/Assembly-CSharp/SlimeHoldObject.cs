using System.Collections.Generic;
using UnityEngine;

public class SlimeHoldObject : MonoBehaviour
{
	public List<Transform> baseBones;

	[Header("Target")]
	public Transform targetObject;

	public Transform rootObject;

	public Transform pairObject;

	public Transform bendObject;

	[Header("Calc")]
	public float calcBend;

	public AnimationCurve bendCurve;

	public List<Vector3> baseBonesPosition;

	[Header("Status")]
	public bool isInitialize;

	public bool isAim;

	public bool isAimEnd;

	[Header("Aim Data")]
	public float aimMin = 0.2f;

	public float aimMax = 0.95f;

	[Space]
	public float aimCurrent;

	public float aimSpeed = 3f;

	[Header("Bend Object")]
	public FeelerNoisePosition _feelerNoisePosition;

	public Vector3 bendDefaultPositon;

	public Vector3 bendCalcPositon;

	[Header("Se")]
	public AudioClip aimSe;

	public AudioClip coilSe;

	public AudioClip uncoilSe;

	[Header("Rotation")]
	public Vector3 defaultRotation = new Vector3(0f, -90f, -90f);

	private void Start()
	{
		base.transform.localRotation = Quaternion.Euler(defaultRotation);
		baseBonesPosition.Clear();
		for (int i = 0; i < baseBones.Count; i++)
		{
			baseBonesPosition.Add(Vector3.zero);
		}
		bendDefaultPositon = _feelerNoisePosition.transform.localPosition;
		aimCurrent = 0f;
	}

	private void LateUpdate()
	{
		if (isAim && !isAimEnd)
		{
			if (aimCurrent < aimMax)
			{
				aimCurrent += Time.deltaTime * aimSpeed;
				if (aimCurrent > aimMax)
				{
					isAimEnd = true;
					aimCurrent = aimMax;
					SeHold(value: true);
				}
			}
		}
		else if (!isAim && isAimEnd && aimCurrent > aimMin)
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
		bendCalcPositon = bendDefaultPositon * aimCurrent;
		_feelerNoisePosition.defaultPosition = bendCalcPositon;
		for (int i = 0; i < baseBones.Count; i++)
		{
			float time = (float)i / (float)(baseBones.Count - 1);
			calcBend = bendCurve.Evaluate(time);
			Vector3 a = Vector3.Lerp(base.transform.position, bendObject.position, calcBend);
			Vector3 b = Vector3.Lerp(bendObject.position, rootObject.position, calcBend);
			Vector3 value = Vector3.Lerp(a, b, calcBend);
			baseBonesPosition[i] = value;
		}
		for (int j = 0; j < baseBones.Count; j++)
		{
			Vector3 zero = Vector3.zero;
			zero = ((j >= baseBones.Count - 1) ? (baseBonesPosition[j] - baseBonesPosition[j - 1]).normalized : (baseBonesPosition[j + 1] - baseBonesPosition[j]).normalized);
			if (zero != Vector3.zero)
			{
				Quaternion identity = Quaternion.identity;
				_ = Quaternion.identity;
				if (j == 0)
				{
					identity = Quaternion.LookRotation(zero, pairObject.forward);
					Quaternion.FromToRotation(zero, -pairObject.up);
				}
				else
				{
					identity = Quaternion.LookRotation(zero, -baseBones[j - 1].forward);
					Quaternion.FromToRotation(zero, -baseBones[j - 1].up);
				}
				identity *= Quaternion.Euler(90f, 0f, 0f);
				baseBones[j].rotation = identity;
			}
			baseBones[j].position = baseBonesPosition[j];
		}
	}

	public void SetAim()
	{
		EffectSeManager.instance.PlaySe(aimSe);
	}

	public void SeHold(bool value)
	{
		if (value)
		{
			EffectSeManager.instance.PlaySe(coilSe);
		}
		else
		{
			EffectSeManager.instance.PlaySe(uncoilSe);
		}
	}
}
