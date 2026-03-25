using System.Collections.Generic;
using UnityEngine;

public class FeelerSyringeObject1 : MonoBehaviour
{
	public List<Transform> baseBones;

	public CameraRaycastManager _cameraRaycastManager;

	[Header("Target")]
	public Transform targetObject;

	public Transform rootObject;

	public Transform bendObject;

	public Transform whipEdgeObject;

	[Header("Calc")]
	public float calcBend;

	public AnimationCurve bendCurve;

	public List<Vector3> baseBonesPosition;

	[Header("Status")]
	public bool isWhip;

	public bool isSet;

	public bool isReturn;

	[Header("CoolTime")]
	public bool isCoolTime;

	public float coolTime;

	public float coolTimeMax = 1.5f;

	[Header("Syringe")]
	public float syringeTimeCurrent;

	public float syringeTimeMax = 1f;

	[Header("Whip")]
	public float timeCurrent;

	public float timeMax = 1f;

	public float whipTime;

	public float returnSpeed = 3f;

	[Space]
	public float calcWhip;

	public AnimationCurve speedCurveStart;

	public AnimationCurve speedCurveEnd;

	[Space]
	public float calcTarget;

	public AnimationCurve targetCurveStart;

	public AnimationCurve targetCurveEnd;

	[Space]
	public float calcBendWhip;

	public AnimationCurve whipCurveStart;

	public AnimationCurve whipCurveEnd;

	public Vector3 bendPosition;

	[Header("Se")]
	public List<AudioClip> whipSe;

	public AudioClip missSe;

	[Header("Rotation")]
	public Vector3 defaultRotation = new Vector3(0f, -90f, -90f);

	private void Start()
	{
		baseBonesPosition.Clear();
		for (int i = 0; i < baseBones.Count; i++)
		{
			baseBonesPosition.Add(Vector3.zero);
		}
		bendPosition = bendObject.localPosition;
		syringeTimeCurrent = 0f;
	}

	private void FixedUpdate()
	{
		if (isCoolTime)
		{
			coolTime -= Time.deltaTime;
			if (coolTime < 0f)
			{
				isCoolTime = false;
			}
		}
		if (isWhip)
		{
			if (!isSet)
			{
				if (timeCurrent < timeMax / 2f)
				{
					timeCurrent += Time.deltaTime;
				}
				else
				{
					timeCurrent += Time.deltaTime / returnSpeed;
				}
			}
			if (!isSet && !isReturn && timeCurrent > timeMax / 2f)
			{
				isSet = true;
				syringeTimeCurrent = 0f;
			}
			else if (isSet && !isReturn)
			{
				syringeTimeCurrent += Time.deltaTime;
				if (syringeTimeCurrent > syringeTimeMax)
				{
					isSet = false;
					isReturn = true;
					timeCurrent = timeMax / 2f;
				}
			}
			else if (timeCurrent >= timeMax)
			{
				isWhip = false;
				timeCurrent = 0f;
			}
			if (timeCurrent <= timeMax / 2f)
			{
				whipTime = Mathf.Lerp(0f, 1f, timeCurrent / (timeMax / 2f));
				calcBendWhip = whipCurveStart.Evaluate(whipTime);
				calcWhip = speedCurveStart.Evaluate(whipTime);
				calcTarget = targetCurveStart.Evaluate(whipTime);
			}
			else
			{
				whipTime = Mathf.Lerp(1f, 0f, (timeCurrent - timeMax / 2f) / (timeMax / 2f));
				calcBendWhip = whipCurveEnd.Evaluate(whipTime);
				calcWhip = speedCurveEnd.Evaluate(whipTime);
				calcTarget = targetCurveEnd.Evaluate(whipTime);
			}
			bendPosition.z = calcBendWhip;
			bendPosition.x = 0f - calcBendWhip;
			bendObject.localPosition = bendPosition;
			Vector3 up = Camera.main.transform.up;
			Vector3 vector = -Camera.main.transform.right;
			Vector3 normalized = (up + vector).normalized;
			Vector3 vector2 = Vector3.Lerp(rootObject.position, targetObject.position, whipTime);
			whipEdgeObject.position = vector2 + normalized * calcTarget;
		}
		for (int i = 0; i < baseBones.Count; i++)
		{
			float time = (float)i / (float)(baseBones.Count - 1);
			calcBend = bendCurve.Evaluate(time);
			Vector3 a = Vector3.Lerp(rootObject.position, bendObject.position, calcBend);
			Vector3 b = Vector3.Lerp(bendObject.position, whipEdgeObject.position, calcBend);
			Vector3 value = Vector3.Lerp(a, b, calcBend);
			baseBonesPosition[i] = value;
		}
		for (int j = 0; j < baseBones.Count; j++)
		{
			if (j < baseBones.Count - 1)
			{
				Vector3 forward = baseBonesPosition[j + 1] - baseBonesPosition[j];
				Quaternion quaternion = Quaternion.Euler(90f, 0f, 0f);
				baseBones[j].rotation = Quaternion.LookRotation(forward, Vector3.up) * quaternion;
			}
			baseBones[j].position = baseBonesPosition[j];
		}
	}

	public void Syringe()
	{
		if (!isWhip && !isCoolTime)
		{
			if (_cameraRaycastManager.isActiveSyringe)
			{
				EffectSeManager.instance.PlaySe(whipSe[Random.Range(0, whipSe.Count)]);
				isWhip = true;
				isSet = false;
				isReturn = false;
				timeCurrent = 0f;
				syringeTimeCurrent = 0f;
				isCoolTime = true;
				coolTime = coolTimeMax;
				_cameraRaycastManager.coolTimeSyringe = coolTimeMax;
			}
			else
			{
				EffectSeManager.instance.PlaySe(missSe);
			}
		}
	}

	public void SyringeAim(bool value)
	{
		_cameraRaycastManager.SetMakerSyringe(value);
	}
}
