using System.Collections.Generic;
using UnityEngine;

public class FeelerCoilObject : MonoBehaviour
{
	public CoilFeelerAnimancer _coilFeelerAnimancer;

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

	[Header("Coil Position")]
	public bool isNeck;

	public bool isHeadEyes;

	public bool isExtend;

	public bool isDamage;

	[Header("Status")]
	public bool isInitialize;

	public bool isSqueeze;

	public bool isCoil;

	public bool isAim;

	public bool isAimEnd;

	public bool isFirstCoil;

	[Header("Aim Data")]
	public float aimMin = 0.2f;

	public float aimMax = 0.95f;

	[Space]
	public float aimCurrent;

	public float aimSpeed = 3f;

	[Header("Coil Data")]
	public float coilCurrent;

	[Header("Hide Eyes")]
	public CharacterLifeManager _characterLifeManager;

	[Header("Bend Object")]
	public FeelerNoisePosition _feelerNoisePosition;

	public Vector3 bendDefaultPositon;

	public Vector3 bendCalcPositon;

	[Header("Onomatopoeia")]
	public float onomatopoeiaTime;

	public float onomatopoeiaTimeMin = 10f;

	public float onomatopoeiaTimeMax = 30f;

	[Header("Se")]
	public AudioClip aimSe;

	public List<AudioClip> coilSe;

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
		if (!isHeadEyes && !isExtend)
		{
			isSqueeze = false;
			isCoil = true;
			isAim = true;
			isAimEnd = true;
			_coilFeelerAnimancer.isCoil = true;
			isFirstCoil = true;
			aimCurrent = aimMax;
			coilCurrent = 1f;
		}
		_coilFeelerAnimancer.coilParam = coilCurrent;
		if (!isNeck && !isHeadEyes)
		{
			onomatopoeiaTime = Random.Range(onomatopoeiaTimeMin, onomatopoeiaTimeMax);
		}
		if (isHeadEyes)
		{
			onomatopoeiaTime = 1f;
		}
	}

	private void LateUpdate()
	{
		if (isAim)
		{
			if (aimCurrent < aimMax)
			{
				aimCurrent += Time.deltaTime * aimSpeed;
				if (aimCurrent > aimMax)
				{
					isAimEnd = true;
					aimCurrent = aimMax;
					isCoil = true;
					CoilSe(value: true);
					if (isFirstCoil && !isNeck && !isHeadEyes)
					{
						onomatopoeiaTime = Random.Range(0.25f, 0.5f);
					}
					isFirstCoil = true;
				}
			}
			if (aimCurrent == aimMax)
			{
				if (OnomatopoeiaManager.instance.useOtomanopoeia && !isNeck && !isHeadEyes)
				{
					onomatopoeiaTime -= Time.deltaTime;
					if (onomatopoeiaTime < 0f)
					{
						onomatopoeiaTime = Random.Range(onomatopoeiaTimeMin, onomatopoeiaTimeMax);
						OnomatopoeiaManager.instance.SpawnOnomatopoeia(base.transform.position, targetObject, "Bind", Camera.main.transform);
						EffectSeManager.instance.PlaySe(coilSe[Random.Range(0, coilSe.Count)]);
					}
				}
				else if (isHeadEyes && !isDamage)
				{
					onomatopoeiaTime -= Time.deltaTime;
					if (onomatopoeiaTime < 0f)
					{
						isDamage = true;
						if (OnomatopoeiaManager.instance.useOtomanopoeia)
						{
							OnomatopoeiaManager.instance.SpawnOnomatopoeia(base.transform.position, targetObject, "Bind", Camera.main.transform);
						}
						EffectSeManager.instance.PlaySe(coilSe[Random.Range(0, coilSe.Count)]);
						if (_characterLifeManager != null)
						{
							_characterLifeManager.HitData("Eyes", "Hide");
						}
					}
				}
			}
		}
		else if (!isAim && !isCoil && aimCurrent > aimMin)
		{
			aimCurrent -= Time.deltaTime * aimSpeed;
			if (aimCurrent < aimMin)
			{
				isAimEnd = false;
				aimCurrent = aimMin;
			}
		}
		if (isCoil && coilCurrent < 1f)
		{
			coilCurrent += Time.deltaTime;
			if (coilCurrent > 1f)
			{
				coilCurrent = 1f;
			}
			_coilFeelerAnimancer.coilParam = coilCurrent;
		}
		else if (!isCoil && coilCurrent > 0f)
		{
			coilCurrent -= Time.deltaTime;
			if (coilCurrent < 0f)
			{
				coilCurrent = 0f;
				isAim = false;
				if (isHeadEyes)
				{
					isDamage = false;
					onomatopoeiaTime = 1f;
				}
			}
			_coilFeelerAnimancer.coilParam = coilCurrent;
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

	public void SetCoil(bool value)
	{
		if (value)
		{
			isAim = true;
			_coilFeelerAnimancer.isCoil = value;
		}
		else
		{
			isCoil = false;
			_coilFeelerAnimancer.isCoil = value;
		}
	}

	public void SetSqueeze(bool value)
	{
		isSqueeze = value;
		_coilFeelerAnimancer.isSqueeze = isSqueeze;
		if (!isSqueeze)
		{
			_coilFeelerAnimancer.isSqueezeEnd = false;
		}
	}

	public void CoilSe(bool value)
	{
		if (value)
		{
			EffectSeManager.instance.PlaySe(coilSe[Random.Range(0, coilSe.Count)]);
		}
		else
		{
			EffectSeManager.instance.PlaySe(uncoilSe);
		}
	}

	public void OnomatopoeiaBind()
	{
		if (OnomatopoeiaManager.instance.useOtomanopoeia)
		{
			onomatopoeiaTime -= onomatopoeiaTimeMin;
		}
	}
}
