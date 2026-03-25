using UnityEngine;

public class UterusPoolManager : MonoBehaviour
{
	public Material poolMaterial;

	public ParticleSystem uterusDripParticle;

	public ParticleSystem vaginaDripParticle;

	private ParticleSystem.EmissionModule _emissionUterus;

	private ParticleSystem.MinMaxCurve _rateUterus;

	private ParticleSystem.EmissionModule _emissionVagina;

	private ParticleSystem.MinMaxCurve _rateVagina;

	public Transform onomatopoeiaLookTarget;

	[Header("Status")]
	public bool isGag;

	[Header("Paramater")]
	public float currentFill;

	public float targetFill;

	[Header("Anim Param")]
	public float conceiveParameter;

	[Header("Data")]
	public float fillMin = -0.05f;

	public float fillMaxCurrent = 0.35f;

	public float fillMaxDefault = 0.25f;

	public float fillMaxConceive = 2f;

	public float fillOver = 0.1f;

	public float fillSpeed = 0.2f;

	public float fillDripSpeed = 0.005f;

	public float dripMax = 10f;

	[Header("Onomatopoeia")]
	public float onomatopoeiaTime;

	public float onomatopoeiaTimeMin = 5f;

	public float onomatopoeiaTimeMax = 15f;

	private void Start()
	{
		currentFill = fillMin;
		targetFill = currentFill;
		poolMaterial.SetFloat("_Fill", currentFill);
		_emissionUterus = uterusDripParticle.emission;
		_rateUterus = _emissionUterus.rateOverTime;
		_rateUterus.constantMax = 0f;
		_emissionUterus.rateOverTime = _rateUterus;
		_emissionVagina = vaginaDripParticle.emission;
		_rateVagina = _emissionVagina.rateOverTime;
		_rateVagina.constantMax = 0f;
		_emissionVagina.rateOverTime = _rateVagina;
		onomatopoeiaTime = Random.Range(onomatopoeiaTimeMin, onomatopoeiaTimeMax);
	}

	private void LateUpdate()
	{
		if (targetFill > fillMin)
		{
			if (!isGag)
			{
				targetFill -= Time.deltaTime * fillDripSpeed;
			}
			if (targetFill < fillMin)
			{
				_rateUterus.constantMax = 0f;
				_emissionUterus.rateOverTime = _rateUterus;
				_rateVagina.constantMax = 0f;
				_emissionVagina.rateOverTime = _rateVagina;
				if (OnomatopoeiaManager.instance.useOtomanopoeia && onomatopoeiaTime > 2f)
				{
					onomatopoeiaTime = Random.Range(onomatopoeiaTimeMin, onomatopoeiaTimeMax);
					if (!isGag)
					{
						OnomatopoeiaManager.instance.SpawnOnomatopoeia(uterusDripParticle.transform.position, null, "SelectionCreamPie", onomatopoeiaLookTarget);
						OnomatopoeiaManager.instance.SpawnOnomatopoeia(vaginaDripParticle.transform.position, null, "CreamPie", Camera.main.transform);
					}
					else if (isGag)
					{
						OnomatopoeiaManager.instance.SpawnOnomatopoeia(uterusDripParticle.transform.position, null, "SelectionDrip", onomatopoeiaLookTarget);
						OnomatopoeiaManager.instance.SpawnOnomatopoeia(vaginaDripParticle.transform.position, null, "Drip", Camera.main.transform);
					}
				}
			}
			if (OnomatopoeiaManager.instance.useOtomanopoeia)
			{
				onomatopoeiaTime -= Time.deltaTime;
				if (onomatopoeiaTime < 0f)
				{
					onomatopoeiaTime = Random.Range(onomatopoeiaTimeMin, onomatopoeiaTimeMax);
					if (!isGag)
					{
						OnomatopoeiaManager.instance.SpawnOnomatopoeia(uterusDripParticle.transform.position, null, "SelectionCreamPie", onomatopoeiaLookTarget);
						OnomatopoeiaManager.instance.SpawnOnomatopoeia(vaginaDripParticle.transform.position, null, "CreamPie", Camera.main.transform);
					}
					else if (isGag)
					{
						OnomatopoeiaManager.instance.SpawnOnomatopoeia(uterusDripParticle.transform.position, null, "SelectionDrip", onomatopoeiaLookTarget);
						OnomatopoeiaManager.instance.SpawnOnomatopoeia(vaginaDripParticle.transform.position, null, "Drip", Camera.main.transform);
					}
				}
			}
		}
		if (currentFill == targetFill)
		{
			return;
		}
		if (currentFill < targetFill)
		{
			currentFill += Time.deltaTime * fillSpeed;
			if (currentFill > targetFill)
			{
				currentFill = targetFill;
			}
		}
		else if (currentFill > targetFill)
		{
			currentFill -= Time.deltaTime * fillSpeed;
			if (currentFill < targetFill)
			{
				currentFill = targetFill;
			}
		}
		poolMaterial.SetFloat("_Fill", currentFill);
	}

	public void AddFill(float value)
	{
		CheckFill();
		if (targetFill < fillMaxCurrent)
		{
			targetFill += value;
			if (targetFill > fillMaxCurrent + fillOver)
			{
				targetFill = fillMaxCurrent + fillOver;
			}
		}
		if (targetFill > fillMin && !isGag)
		{
			_rateUterus.constantMax = dripMax;
			_emissionUterus.rateOverTime = _rateUterus;
			_rateVagina.constantMax = dripMax;
			_emissionVagina.rateOverTime = _rateVagina;
		}
	}

	public void CheckFill()
	{
		float num = Mathf.Lerp(fillMaxDefault, fillMaxConceive, conceiveParameter);
		if (fillMaxCurrent > num)
		{
			fillMaxCurrent = num + fillOver;
		}
		else
		{
			fillMaxCurrent = num;
		}
		if (targetFill > fillMaxCurrent)
		{
			targetFill = fillMaxCurrent;
			currentFill = targetFill;
		}
	}

	public void SetGag(bool value)
	{
		isGag = value;
		if (isGag)
		{
			_rateUterus.constantMax = 0f;
			_emissionUterus.rateOverTime = _rateUterus;
			_rateVagina.constantMax = 0f;
			_emissionVagina.rateOverTime = _rateVagina;
		}
		else if (targetFill > fillMin)
		{
			_rateUterus.constantMax = dripMax;
			_emissionUterus.rateOverTime = _rateUterus;
			_rateVagina.constantMax = dripMax;
			_emissionVagina.rateOverTime = _rateVagina;
		}
	}
}
