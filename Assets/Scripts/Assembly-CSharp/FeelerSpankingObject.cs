using System.Collections.Generic;
using UnityEngine;

public class FeelerSpankingObject : MonoBehaviour
{
	public List<Transform> baseBones;

	public CameraRaycastManager _cameraRaycastManager;

	[Header("Spanking Object")]
	public SpankingObject _spankingObject;

	public GameObject feelerSpankingObject;

	public GameObject spankingPaintBody;

	public GameObject spankingPaintCostume;

	public GameObject spankingTouchPoint;

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

	public bool isSlide;

	public bool isReturn;

	public bool isTouch;

	[Header("CoolTime")]
	public bool isCoolTime;

	public float coolTime;

	public float coolTimeMax = 1.5f;

	[Header("Whip")]
	public float timeCurrent;

	public float timeMax = 1f;

	public float whipTime;

	public float returnSpeed = 3f;

	[Header("Slide")]
	public float slideTime;

	public float slideTimeMax = 0.25f;

	public float slideRange = 0.05f;

	[Header("Bend")]
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
		feelerSpankingObject.SetActive(value: false);
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
			if (!isSlide)
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
			if (!isSet && !isReturn && timeCurrent > timeMax / 2.5f)
			{
				isSet = true;
				isTouch = true;
				isSlide = true;
				spankingPaintBody.SetActive(value: true);
				spankingPaintCostume.SetActive(value: true);
				spankingTouchPoint.SetActive(value: true);
				slideTime = 0f;
			}
			else if (isSet && isSlide)
			{
				slideTime += Time.deltaTime;
				if (slideTime > slideTimeMax)
				{
					slideTime = slideTimeMax;
					isSlide = false;
				}
				Vector3 a = targetObject.position + Camera.main.transform.right * slideRange;
				Vector3 vector = targetObject.position - Camera.main.transform.right * slideRange;
				if (isSlide)
				{
					whipEdgeObject.position = Vector3.Lerp(a, vector, slideTime / slideTimeMax);
				}
				else
				{
					targetObject.position = vector;
					whipEdgeObject.position = targetObject.position;
				}
			}
			else if (!isReturn && timeCurrent > timeMax / 2f && !isSlide)
			{
				isReturn = true;
				timeCurrent = timeMax / 2f;
			}
			else if (isReturn && isTouch && timeCurrent > timeMax / 1.5f)
			{
				isTouch = false;
				spankingPaintBody.SetActive(value: false);
				spankingPaintCostume.SetActive(value: false);
				spankingTouchPoint.SetActive(value: false);
			}
			else if (timeCurrent >= timeMax)
			{
				isWhip = false;
				timeCurrent = 0f;
				feelerSpankingObject.SetActive(value: false);
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
			Vector3 right = Camera.main.transform.right;
			Vector3 normalized = (up + right).normalized;
			if (!isSlide)
			{
				Vector3 vector2 = Vector3.Lerp(rootObject.position, targetObject.position, whipTime);
				whipEdgeObject.position = vector2 + normalized * calcTarget;
			}
		}
		for (int i = 0; i < baseBones.Count; i++)
		{
			float time = (float)i / (float)(baseBones.Count - 1);
			calcBend = bendCurve.Evaluate(time);
			Vector3 a2 = Vector3.Lerp(rootObject.position, bendObject.position, calcBend);
			Vector3 b = Vector3.Lerp(bendObject.position, whipEdgeObject.position, calcBend);
			Vector3 value = Vector3.Lerp(a2, b, calcBend);
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

	public void Spanking(int skillID)
	{
		Debug.LogError("Spanking");
		if (isWhip || isCoolTime)
		{
			return;
		}
		if (_cameraRaycastManager.isActiveSpanking)
		{
			_cameraRaycastManager.RecheckRaycast();
			if (_cameraRaycastManager.hitTarget != null)
			{
				EffectSeManager.instance.PlaySe(whipSe[Random.Range(0, whipSe.Count)]);
				isWhip = true;
				isSet = false;
				isReturn = false;
				isTouch = false;
				feelerSpankingObject.SetActive(value: true);
				_spankingObject.gameObject.SetActive(value: true);
				_spankingObject.isHit = false;
				timeCurrent = 0f;
				isCoolTime = true;
				coolTime = coolTimeMax;
				_cameraRaycastManager.coolTimeSpanking = coolTimeMax;
				SkillGUIDataBase.instance.SetCoolTime(skillID, coolTimeMax);
			}
			else
			{
				EffectSeManager.instance.PlaySe(missSe);
			}
		}
		else
		{
			EffectSeManager.instance.PlaySe(missSe);
		}
	}

	public void SpankingAim(bool value)
	{
		_cameraRaycastManager.SetMakerSpanking(value);
	}
}
