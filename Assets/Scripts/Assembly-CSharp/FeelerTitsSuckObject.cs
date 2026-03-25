using System.Collections.Generic;
using UnityEngine;

public class FeelerTitsSuckObject : MonoBehaviour
{
	public CharacterLifeManager _characterLifeManager;

	public Animator _animator;

	public List<Transform> baseBones;

	public FeelerMilkTankObject _feelerMilkTankObject;

	[Header("LinkStatus")]
	public FeelerTitsSuckObject _feelerTitsSuckObject;

	public bool titsL;

	public bool titsR;

	public bool master;

	[Header("LinkScript")]
	public int animNum;

	public int animNumMax = 3;

	[Header("Target")]
	public Transform targetObject;

	public Transform rootObject;

	public Transform pairObject;

	public Transform bendObject;

	public Transform hangObject;

	public Transform suckEndObject;

	[Header("Calc")]
	public float calcBend;

	public AnimationCurve bendCurve;

	public List<Vector3> baseBonesPosition;

	[Header("Status")]
	public bool isAim;

	public bool isAimEnd;

	public bool isGag;

	public bool isTitsMilk;

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
	[Space]
	public Vector3 adjustTarget = new Vector3(0f, 0.06f, 0f);

	public Vector3 localCalcTargetPosition;

	public Vector3 worldCalcTargetPosition;

	public Vector3 lastWorldCalcTargetPosition;

	public Vector3 defaultRotation = new Vector3(0f, -90f, -90f);

	public float rotationSpeed = 5f;

	[Header("Se")]
	public int seCount;

	public int seCountMin = 2;

	public int seCountMax = 6;

	public AudioClip aimSe;

	public List<AudioClip> suckSe;

	public AudioClip fitSe;

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
			if (isAimEnd || !(aimCurrent < aimMax) || !(aimCurrent < aimMax))
			{
				return;
			}
			aimCurrent += Time.deltaTime * aimSpeed;
			if (aimCurrent > aimMax)
			{
				isAimEnd = true;
				aimCurrent = aimMax;
				SetGag(value: true);
				_animator.SetBool("isSuck", value: true);
				animNum = Random.Range(0, animNumMax);
				if (master && _feelerTitsSuckObject.isAim)
				{
					_animator.SetInteger("AnimType", animNum);
					_feelerTitsSuckObject._animator.SetInteger("AnimType", animNum);
					TitsFitSe();
				}
				else if (master && !_feelerTitsSuckObject.isAim)
				{
					_animator.SetInteger("AnimType", animNum);
					TitsFitSe();
				}
				else if (!master && !_feelerTitsSuckObject.isAim)
				{
					_animator.SetInteger("AnimType", animNum);
					TitsFitSe();
				}
			}
			return;
		}
		isAimEnd = false;
		if (aimCurrent > aimMin)
		{
			aimCurrent -= Time.deltaTime * aimSpeed;
			if (aimCurrent < aimMin)
			{
				aimCurrent = aimMin;
				_animator.SetBool("isSuck", value: false);
			}
		}
	}

	public void MagicaAnimator()
	{
		_feelerNoisePosition.aimCurrent = aimCurrent;
		localCalcTargetPosition = targetObject.localPosition + adjustTarget;
		worldCalcTargetPosition = targetObject.parent.TransformPoint(localCalcTargetPosition);
		Vector3 b = (lastWorldCalcTargetPosition = Vector3.Lerp(worldCalcTargetPosition, lastWorldCalcTargetPosition, 0.5f));
		base.transform.position = Vector3.Lerp(hangObject.position, b, aimCurrent);
		bendCalcPositon = bendDefaultPositon;
		_feelerNoisePosition.defaultPosition = bendCalcPositon;
		Quaternion rotation = hangObject.rotation;
		Quaternion b2 = Quaternion.LookRotation((targetObject.position - base.transform.position).normalized, Vector3.up) * Quaternion.Euler(defaultRotation);
		Quaternion b3 = Quaternion.Slerp(rotation, b2, aimCurrent);
		base.transform.rotation = Quaternion.Slerp(base.transform.rotation, b3, Time.deltaTime * rotationSpeed);
		for (int i = 0; i < baseBones.Count; i++)
		{
			float time = (float)i / (float)(baseBones.Count - 1);
			calcBend = bendCurve.Evaluate(time);
			Vector3 a = Vector3.Lerp(base.transform.position, bendObject.position, calcBend);
			Vector3 b4 = Vector3.Lerp(bendObject.position, rootObject.position, calcBend);
			Vector3 value = Vector3.Lerp(a, b4, calcBend);
			baseBonesPosition[i] = value;
		}
		for (int j = 0; j < baseBones.Count; j++)
		{
			Vector3 zero = Vector3.zero;
			zero = ((j >= baseBones.Count - 1) ? (baseBonesPosition[j] - baseBonesPosition[j - 1]).normalized : (baseBonesPosition[j + 1] - baseBonesPosition[j]).normalized);
			Quaternion rotation2 = Quaternion.identity;
			_ = Quaternion.identity;
			if (zero != Vector3.zero)
			{
				if (j == 0)
				{
					rotation2 = Quaternion.LookRotation(zero, pairObject.forward);
					Quaternion.FromToRotation(zero, -pairObject.up);
				}
				else
				{
					rotation2 = Quaternion.LookRotation(zero, -baseBones[j - 1].forward);
					Quaternion.FromToRotation(zero, -baseBones[j - 1].up);
				}
			}
			rotation2 *= Quaternion.Euler(90f, 0f, 0f);
			baseBones[j].rotation = rotation2;
			baseBones[j].position = baseBonesPosition[j];
		}
	}

	public void SetGag(bool value)
	{
		isGag = value;
		_feelerMilkTankObject.SetGag(isGag);
	}

	public void TitsSuck()
	{
		if (titsL)
		{
			_characterLifeManager.HitData("TitsL", "SuckTits");
			OnomatopoeiaManager.instance.SpawnOnomatopoeia(suckEndObject.position, null, "SuckTits", Camera.main.transform);
		}
		if (titsR)
		{
			_characterLifeManager.HitData("TitsR", "SuckTits");
			OnomatopoeiaManager.instance.SpawnOnomatopoeia(suckEndObject.position, null, "SuckTits", Camera.main.transform);
		}
	}

	public void TitsFitSe()
	{
		EffectSeManager.instance.PlaySe(fitSe);
		OnomatopoeiaManager.instance.SpawnOnomatopoeia(suckEndObject.position, null, "SuckFit", Camera.main.transform);
	}

	public void TitsSuckSeL()
	{
		if (titsL)
		{
			EffectSeManager.instance.PlaySe(suckSe[Random.Range(0, suckSe.Count)]);
			if (isTitsMilk)
			{
				_feelerMilkTankObject.AddMilk(0.02f);
				OnomatopoeiaManager.instance.SpawnOnomatopoeia(_feelerMilkTankObject.transform.position, null, "SuckMilk", Camera.main.transform);
			}
		}
	}

	public void TitsSuckSeR()
	{
		if (titsR)
		{
			EffectSeManager.instance.PlaySe(suckSe[Random.Range(0, suckSe.Count)]);
			if (isTitsMilk)
			{
				_feelerMilkTankObject.AddMilk(0.02f);
				OnomatopoeiaManager.instance.SpawnOnomatopoeia(_feelerMilkTankObject.transform.position, null, "SuckMilk", Camera.main.transform);
			}
		}
	}
}
