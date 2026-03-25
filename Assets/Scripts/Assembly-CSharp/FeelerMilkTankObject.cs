using System.Collections.Generic;
using UnityEngine;

public class FeelerMilkTankObject : MonoBehaviour
{
	public CharacterEffectManager _characterEffectManager;

	[Header("Milk Mesh")]
	public MeshRenderer _mesh;

	public Material milkTankMaterial;

	[Header("Status")]
	public bool isGag;

	public bool isTitsMilk;

	[Header("Tank Data")]
	public float fillCurrent;

	public float fillTarget;

	public float fillMax = 0.8f;

	public float fillSpeed = 0.2f;

	public float fillDripSpeed = 0.002f;

	public AnimationCurve fillCurve;

	[Space]
	public float rotateCurrent;

	public float rotateMax = 0.75f;

	public float rotatePower = 5f;

	public float rotateDamp = 2f;

	public float rotateSpeed = 5f;

	[Header("MIlk Tank Data")]
	public Transform milkTankCurface;

	public float milkTankCurrent;

	public float milkTankMin = -0.01f;

	public float milkTankMax = 0.29f;

	public float milkTankSpeed = 0.1f;

	public Vector3 milkTankPosition;

	[Header("Calc")]
	public Vector3 previousPosition;

	public Vector3 calcPosition;

	[Header("Effect Splash")]
	public Transform milkStocker;

	public GameObject milkTankSplash;

	public float dripMax = 30f;

	[Header("Effect Drip")]
	public ParticleSystem tankDripParticle;

	private ParticleSystem.EmissionModule _emissionTank;

	private ParticleSystem.MinMaxCurve _rateTank;

	[Header("Onomatopoeia")]
	public float onomatopoeiaTime;

	public float onomatopoeiaTime2;

	public float onomatopoeiaTimeMin = 3f;

	public float onomatopoeiaTimeMax = 8f;

	[Header("Se")]
	public List<AudioClip> milkSe;

	private void Start()
	{
		milkTankMaterial = _mesh.material;
		previousPosition = base.transform.position;
		fillCurrent = 0f;
		fillTarget = 0f;
		milkTankMaterial.SetFloat("_Fill", fillCurve.Evaluate(fillCurrent));
		rotateCurrent = 0f;
		milkTankMaterial.SetFloat("_Rotate", rotateCurrent);
		_emissionTank = tankDripParticle.emission;
		_rateTank = _emissionTank.rateOverTime;
		_rateTank.constantMax = 0f;
		_emissionTank.rateOverTime = _rateTank;
		onomatopoeiaTime = Random.Range(onomatopoeiaTimeMin, onomatopoeiaTimeMax);
		onomatopoeiaTime2 = Random.Range(onomatopoeiaTimeMin, onomatopoeiaTimeMax);
		milkTankCurrent = milkTankMin;
		milkTankPosition = milkTankCurface.localPosition;
		milkTankPosition.y = milkTankMin;
		milkTankCurface.localPosition = milkTankPosition;
	}

	private void LateUpdate()
	{
		if (!isTitsMilk)
		{
			return;
		}
		calcPosition = base.transform.position - previousPosition;
		float magnitude = calcPosition.magnitude;
		if (magnitude > 0.001f)
		{
			rotateCurrent += magnitude * rotatePower;
			rotateCurrent = Mathf.Min(rotateCurrent, rotateMax);
		}
		rotateCurrent *= Mathf.Exp((0f - rotateDamp) * Time.deltaTime);
		float value = Mathf.Sin(Time.time * rotateSpeed) * rotateCurrent;
		if (fillCurrent > fillTarget)
		{
			fillCurrent -= Time.deltaTime * fillSpeed;
			if (fillCurrent < fillTarget)
			{
				fillCurrent = fillTarget;
			}
		}
		else if (fillCurrent < fillTarget)
		{
			fillCurrent += Time.deltaTime * fillSpeed;
			if (fillCurrent > fillTarget)
			{
				fillCurrent = fillTarget;
			}
		}
		milkTankMaterial.SetFloat("_Fill", fillCurve.Evaluate(fillCurrent));
		milkTankMaterial.SetFloat("_Rotate", value);
		previousPosition = base.transform.position;
		if (fillTarget > 0f)
		{
			if (!isGag)
			{
				fillTarget -= Time.deltaTime * fillDripSpeed;
				if (OnomatopoeiaManager.instance.useOtomanopoeia)
				{
					onomatopoeiaTime -= Time.deltaTime;
					if (onomatopoeiaTime < 0f)
					{
						onomatopoeiaTime = Random.Range(onomatopoeiaTimeMin, onomatopoeiaTimeMax);
						OnomatopoeiaManager.instance.SpawnOnomatopoeia(tankDripParticle.transform.position, null, "MilkDrip", Camera.main.transform);
					}
				}
			}
			if (fillTarget < 0f)
			{
				_rateTank.constantMax = 0f;
				_emissionTank.rateOverTime = _rateTank;
			}
		}
		if (isGag || !(fillTarget > 0f) || !(milkTankCurrent < milkTankMax))
		{
			return;
		}
		milkTankCurrent += Time.deltaTime * milkTankSpeed;
		if (milkTankCurrent > milkTankMax)
		{
			milkTankCurrent = milkTankMax;
		}
		milkTankPosition = milkTankCurface.localPosition;
		milkTankPosition.y = milkTankCurrent;
		milkTankCurface.localPosition = milkTankPosition;
		if (OnomatopoeiaManager.instance.useOtomanopoeia)
		{
			onomatopoeiaTime2 -= Time.deltaTime;
			if (onomatopoeiaTime2 < 0f)
			{
				onomatopoeiaTime2 = Random.Range(onomatopoeiaTimeMin, onomatopoeiaTimeMax);
				OnomatopoeiaManager.instance.SpawnOnomatopoeia(milkTankCurface.transform.position, null, "MilkTank", Camera.main.transform);
			}
		}
	}

	public void AddMilk(float value)
	{
		if (isGag)
		{
			_characterEffectManager.TitsSuck(3f);
			fillTarget += value;
			if (fillTarget > fillMax)
			{
				fillTarget = fillMax;
			}
			GameObject obj = Object.Instantiate(milkTankSplash, milkStocker.position, milkStocker.rotation, milkStocker);
			obj.transform.localPosition = Vector3.zero;
			obj.transform.localRotation = Quaternion.identity;
			EffectSeManager.instance.PlaySe(milkSe[Random.Range(0, milkSe.Count)]);
		}
	}

	public void SetGag(bool value)
	{
		isGag = value;
		_characterEffectManager.isTitsGag = isGag;
		if (isGag)
		{
			_rateTank.constantMax = 0f;
			_emissionTank.rateOverTime = _rateTank;
		}
		else if (fillTarget > 0f)
		{
			_rateTank.constantMax = dripMax;
			_emissionTank.rateOverTime = _rateTank;
		}
	}

	public void MilkOn()
	{
		if (isGag)
		{
			_rateTank.constantMax = 0f;
			_emissionTank.rateOverTime = _rateTank;
		}
		else if (fillTarget > 0f)
		{
			_rateTank.constantMax = dripMax;
			_emissionTank.rateOverTime = _rateTank;
		}
	}
}
