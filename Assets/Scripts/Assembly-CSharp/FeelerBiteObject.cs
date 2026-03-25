using System.Collections.Generic;
using UnityEngine;

public class FeelerBiteObject : MonoBehaviour
{
	public FeelerBiteMultiObject _feelerBiteMultiObject;

	public CharacterLifeManager _characterLifeManager;

	public Animator _animator;

	public List<Transform> baseBones;

	public List<FeelerBiteEdge> biteEdge;

	[Header("Paint Object")]
	public GameObject bitePaintBody;

	public GameObject bitePaintCostume;

	public GameObject biteTouchPoint;

	[Header("Single Bite")]
	public bool single;

	[Header("LinkStatus")]
	public FeelerBiteObject _feelerBiteObject;

	public bool titsL;

	public bool titsR;

	public bool master;

	[Space]
	public bool vagina;

	public bool point;

	[Header("Attack Type")]
	public int skillID;

	public string attackType = "Bite";

	public CharacterColliderObject damageCollider;

	public int biteType;

	[Header("Status")]
	public bool isBite;

	public bool isBiteStart;

	public bool isBiteWait;

	public bool isBiteEnd;

	[Header("Time")]
	public float waitTime;

	public float waitTimeMax = 1.5f;

	[Header("LinkScript")]
	public int animNum;

	public int animNumMax = 4;

	[Header("Target")]
	public Transform targetObject;

	public Transform rootObject;

	public Transform pairObject;

	public Transform bendObject;

	[Header("Bite")]
	public Transform biteTargetObject;

	public Transform biteBone;

	[Header("Calc")]
	public float calcBend;

	public AnimationCurve bendCurve;

	public List<Vector3> baseBonesPosition;

	public Vector3 oldPositionP0;

	public Vector3 oldPositionP1;

	public Vector3 oldPositionP2;

	public Vector3 oldPositionP3;

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

	public Quaternion bone0DefaultRotation;

	[Header("Transform")]
	[Space]
	public Vector3 adjustTarget = new Vector3(0f, 0.06f, 0f);

	public Vector3 localCalcTargetPosition;

	public Vector3 worldCalcTargetPosition;

	public Vector3 defaultRotation = new Vector3(0f, -90f, -90f);

	public float rotationSpeed = 5f;

	[Header("Se")]
	public int seCount;

	public int seCountMin = 2;

	public int seCountMax = 6;

	public AudioClip aimSe;

	public List<AudioClip> biteSe;

	public List<AudioClip> lickSe;

	public AudioClip titsSe;

	public AudioClip touchSe;

	private void Awake()
	{
		aimCurrent = aimMin;
		seCount = Random.Range(seCountMin, seCountMax);
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
					isBite = true;
					if (!single)
					{
						_animator.SetBool("isLick", value: true);
						animNum = Random.Range(0, animNumMax);
						_animator.SetInteger("AnimType", animNum);
					}
					if (bitePaintBody != null)
					{
						bitePaintBody.SetActive(value: true);
						bitePaintCostume.SetActive(value: true);
						biteTouchPoint.SetActive(value: true);
					}
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
					if (!single)
					{
						_animator.SetBool("isLick", value: false);
					}
					isBite = false;
				}
			}
		}
		_feelerNoisePosition.aimCurrent = aimCurrent;
		if (isBiteStart && !isBiteWait)
		{
			isBiteWait = true;
			waitTime = waitTimeMax;
			biteType = Random.Range(0, 4);
			for (int i = 0; i < biteEdge.Count; i++)
			{
				biteEdge[i].SetBite(biteType);
			}
		}
		else if (isBiteWait && !isBiteEnd)
		{
			isBiteEnd = true;
			for (int j = 0; j < biteEdge.Count; j++)
			{
				if (!biteEdge[j].isBiteEnd)
				{
					isBiteEnd = false;
				}
			}
		}
		else if (isBiteEnd && waitTime > 0f)
		{
			waitTime -= Time.deltaTime;
			if (waitTime < 0f)
			{
				for (int k = 0; k < biteEdge.Count; k++)
				{
					biteEdge[k].ResetBite();
				}
				isBiteStart = false;
				isBiteWait = false;
				isBiteEnd = false;
				if (single)
				{
					isBite = false;
					_feelerBiteMultiObject.BiteSet(skillID, value: false);
				}
			}
		}
		else
		{
			if (!isBite)
			{
				return;
			}
			isBiteStart = true;
			for (int l = 0; l < biteEdge.Count; l++)
			{
				if (biteEdge[l].isOpen)
				{
					isBiteStart = false;
				}
			}
		}
	}

	public void SetBite(bool value)
	{
		for (int i = 0; i < biteEdge.Count; i++)
		{
			biteEdge[i].ResetBite();
		}
		if (!value && bitePaintBody != null)
		{
			bitePaintBody.SetActive(value: false);
			bitePaintCostume.SetActive(value: false);
			biteTouchPoint.SetActive(value: false);
		}
	}

	public void MagicaAnimator()
	{
		localCalcTargetPosition = targetObject.localPosition + adjustTarget;
		worldCalcTargetPosition = targetObject.parent.TransformPoint(localCalcTargetPosition);
		base.transform.position = Vector3.Lerp(rootObject.position, worldCalcTargetPosition, aimCurrent);
		bendCalcPositon = bendDefaultPositon * aimCurrent;
		_feelerNoisePosition.defaultPosition = bendCalcPositon;
		Quaternion quaternion = Quaternion.Euler(defaultRotation);
		Quaternion b = targetObject.rotation * quaternion;
		base.transform.rotation = Quaternion.Slerp(base.transform.rotation, b, Time.deltaTime * rotationSpeed);
		Vector3 normalized = (biteTargetObject.position - biteBone.position).normalized;
		Quaternion quaternion2 = Quaternion.FromToRotation(biteBone.up, normalized);
		biteBone.rotation = quaternion2 * biteBone.rotation;
		float num = Vector3.Distance(biteBone.position, rootObject.position);
		float num2 = Vector3.Distance(bendObject.position, rootObject.position);
		float num3 = Mathf.Clamp01((num - 0.1f) / num2);
		Vector3 normalized2 = Vector3.Cross(-Camera.main.transform.up, normalized).normalized;
		Vector3 normalized3 = Vector3.Cross(normalized, normalized2).normalized;
		float num4 = num * 0.5f * num3;
		if (isAim)
		{
			oldPositionP0 = Vector3.Lerp(oldPositionP0, biteBone.position, 0.2f);
			oldPositionP1 = Vector3.Lerp(oldPositionP1, bendObject.position, 0.2f);
		}
		else
		{
			oldPositionP0 = Vector3.Lerp(oldPositionP0, biteBone.position, 0.5f);
			oldPositionP1 = Vector3.Lerp(oldPositionP1, bendObject.position, 0.5f);
		}
		Vector3 vector = oldPositionP0;
		Vector3 vector2 = oldPositionP1;
		Vector3 vector3 = (vector2 + rootObject.position) * 0.5f;
		if (isAim)
		{
			oldPositionP2 = Vector3.Lerp(oldPositionP2, vector3 + normalized3 * num4, 0.2f);
		}
		else
		{
			oldPositionP2 = Vector3.Lerp(oldPositionP2, vector3 + normalized3 * num4, 0.5f);
		}
		Vector3 p = oldPositionP2;
		Vector3 position = rootObject.position;
		for (int i = 0; i < baseBones.Count; i++)
		{
			float time = (float)i / (float)(baseBones.Count - 1);
			float t = bendCurve.Evaluate(time);
			Vector3 value = BezierCubic(vector, vector2, p, position, t);
			if (value.y < 0f)
			{
				value.y = Mathf.Lerp(value.y, 0f, 0.2f);
			}
			if (i == 0)
			{
				baseBonesPosition[i] = vector;
			}
			else
			{
				baseBonesPosition[i] = value;
			}
		}
		for (int j = 0; j < baseBones.Count; j++)
		{
			normalized = Vector3.zero;
			normalized = ((j >= baseBones.Count - 1) ? (baseBonesPosition[j] - baseBonesPosition[j - 1]).normalized : (baseBonesPosition[j + 1] - baseBonesPosition[j]).normalized);
			Quaternion rotation = Quaternion.identity;
			if (normalized != Vector3.zero)
			{
				rotation = ((j != 0) ? Quaternion.LookRotation(normalized, -baseBones[j - 1].forward) : Quaternion.LookRotation(normalized, pairObject.forward));
			}
			rotation *= Quaternion.Euler(90f, 0f, 0f);
			baseBones[j].rotation = rotation;
			baseBones[j].position = baseBonesPosition[j];
		}
	}

	private Vector3 BezierCubic(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
	{
		float num = 1f - t;
		float num2 = t * t;
		float num3 = num * num;
		float num4 = num3 * num;
		float num5 = num2 * t;
		return num4 * p0 + 3f * num3 * t * p1 + 3f * num * num2 * p2 + num5 * p3;
	}

	public void TitsBite()
	{
		if (titsL)
		{
			_characterLifeManager.HitData("TitsL", attackType);
		}
		if (titsR)
		{
			_characterLifeManager.HitData("TitsR", attackType);
		}
	}

	public void MultiBite()
	{
		if (damageCollider != null)
		{
			damageCollider.HitData(attackType);
			EffectSeManager.instance.PlaySe(biteSe[Random.Range(0, biteSe.Count)]);
			OnomatopoeiaManager.instance.SpawnOnomatopoeia(targetObject.transform.position, null, "Bite", Camera.main.transform);
		}
		else
		{
			Debug.LogError("Multi Bite Target is not found.");
		}
	}

	public void BiteSe()
	{
		EffectSeManager.instance.PlaySe(biteSe[Random.Range(0, biteSe.Count)]);
	}

	public void MultiLick()
	{
		if (damageCollider != null)
		{
			damageCollider.HitData(attackType);
			EffectSeManager.instance.PlaySe(lickSe[Random.Range(0, lickSe.Count)]);
			OnomatopoeiaManager.instance.SpawnOnomatopoeia(targetObject.transform.position, null, "Lick", Camera.main.transform);
		}
		else
		{
			Debug.LogError("Multi Lick Target is not found.");
		}
	}

	public void LickSe()
	{
		EffectSeManager.instance.PlaySe(lickSe[Random.Range(0, lickSe.Count)]);
	}

	public void VaginaBite()
	{
		_characterLifeManager.HitData("Vagia", attackType);
		EffectSeManager.instance.PlaySe(biteSe[Random.Range(0, biteSe.Count)]);
		OnomatopoeiaManager.instance.SpawnOnomatopoeia(targetObject.transform.position, null, "Bite", Camera.main.transform);
	}

	public void TitsBiteSeL()
	{
		if (titsL)
		{
			EffectSeManager.instance.PlaySe(biteSe[Random.Range(0, biteSe.Count)]);
			OnomatopoeiaManager.instance.SpawnOnomatopoeia(targetObject.transform.position, null, "Bite", Camera.main.transform);
			OnomatopoeiaManager.instance.SpawnOnomatopoeia(_feelerBiteObject.targetObject.transform.position, null, "Bite", Camera.main.transform);
		}
	}

	public void TitsBiteSeR()
	{
		if (titsR)
		{
			EffectSeManager.instance.PlaySe(biteSe[Random.Range(0, biteSe.Count)]);
			OnomatopoeiaManager.instance.SpawnOnomatopoeia(targetObject.transform.position, null, "Bite", Camera.main.transform);
			OnomatopoeiaManager.instance.SpawnOnomatopoeia(_feelerBiteObject.targetObject.transform.position, null, "Bite", Camera.main.transform);
		}
	}
}
