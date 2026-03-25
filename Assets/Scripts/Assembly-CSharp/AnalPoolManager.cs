using UnityEngine;

public class AnalPoolManager : MonoBehaviour
{
	public Material poolMaterial;

	public ParticleSystem rectumDripParticle;

	public ParticleSystem analDripParticle;

	private ParticleSystem.EmissionModule _emissionRectum;

	private ParticleSystem.MinMaxCurve _rateRectum;

	private ParticleSystem.EmissionModule _emissionAnal;

	private ParticleSystem.MinMaxCurve _rateAnal;

	public Transform onomatopoeiaLookTarget;

	[Header("Status")]
	public bool isGag;

	[Header("Paramater")]
	public float currentFill;

	public float targetFill;

	[Header("Anim Param")]
	public float conceiveParameter;

	[Header("Data")]
	public float fillMin = -0.12f;

	public float fillMaxCurrent = 0.32f;

	public float fillMaxDefault = 0.25f;

	public float fillMaxConceive = 2f;

	public float fillOver = 0.1f;

	public float fillSpeed = 0.2f;

	public float fillDripSpeed = 0.005f;

	public float dripMax = 10f;

	[Header("Rotate Data")]
	public float rotateDefault = -6.25f;

	public float rotateFinish = -6.75f;

	public float rotateLimit = 0.25f;

	public float rotateCalc;

	[Header("Onomatopoeia")]
	public float onomatopoeiaTime;

	public float onomatopoeiaTimeMin = 5f;

	public float onomatopoeiaTimeMax = 15f;

	private void Start()
	{
		currentFill = fillMin;
		targetFill = currentFill;
		poolMaterial.SetFloat("_Fill", currentFill);
		_emissionRectum = rectumDripParticle.emission;
		_rateRectum = _emissionRectum.rateOverTime;
		_rateRectum.constantMax = 0f;
		_emissionRectum.rateOverTime = _rateRectum;
		_emissionAnal = analDripParticle.emission;
		_rateAnal = _emissionAnal.rateOverTime;
		_rateAnal.constantMax = 0f;
		_emissionAnal.rateOverTime = _rateAnal;
		onomatopoeiaTime = Random.Range(onomatopoeiaTimeMin, onomatopoeiaTimeMax);
	}

	private void LateUpdate()
	{
		if (targetFill > fillMin)
		{
			if (!isGag)
			{
				targetFill -= Time.deltaTime * fillDripSpeed;
				PoolAngle();
			}
			if (targetFill < fillMin)
			{
				_rateRectum.constantMax = 0f;
				_emissionRectum.rateOverTime = _rateRectum;
				_rateAnal.constantMax = 0f;
				_emissionAnal.rateOverTime = _rateAnal;
				if (OnomatopoeiaManager.instance.useOtomanopoeia && onomatopoeiaTime > 2f)
				{
					onomatopoeiaTime = Random.Range(onomatopoeiaTimeMin, onomatopoeiaTimeMax);
					if (!isGag)
					{
						OnomatopoeiaManager.instance.SpawnOnomatopoeia(rectumDripParticle.transform.position, null, "SelectionCreamPie", onomatopoeiaLookTarget);
						OnomatopoeiaManager.instance.SpawnOnomatopoeia(analDripParticle.transform.position, null, "CreamPie", Camera.main.transform);
					}
					else if (isGag)
					{
						OnomatopoeiaManager.instance.SpawnOnomatopoeia(rectumDripParticle.transform.position, null, "SelectionDrip", onomatopoeiaLookTarget);
						OnomatopoeiaManager.instance.SpawnOnomatopoeia(analDripParticle.transform.position, null, "Drip", Camera.main.transform);
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
						OnomatopoeiaManager.instance.SpawnOnomatopoeia(rectumDripParticle.transform.position, null, "SelectionCreamPie", onomatopoeiaLookTarget);
						OnomatopoeiaManager.instance.SpawnOnomatopoeia(analDripParticle.transform.position, null, "CreamPie", Camera.main.transform);
					}
					else if (isGag)
					{
						OnomatopoeiaManager.instance.SpawnOnomatopoeia(rectumDripParticle.transform.position, null, "SelectionDrip", onomatopoeiaLookTarget);
						OnomatopoeiaManager.instance.SpawnOnomatopoeia(analDripParticle.transform.position, null, "Drip", Camera.main.transform);
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
			PoolAngle();
		}
		else if (currentFill > targetFill)
		{
			currentFill -= Time.deltaTime * fillSpeed;
			if (currentFill < targetFill)
			{
				currentFill = targetFill;
			}
			PoolAngle();
		}
		poolMaterial.SetFloat("_Fill", currentFill);
	}

	public void PoolAngle()
	{
		if (currentFill >= rotateLimit)
		{
			rotateCalc = rotateDefault;
		}
		else
		{
			float t = Mathf.Clamp01((currentFill - rotateLimit) / (rotateDefault - rotateLimit));
			rotateCalc = Mathf.Lerp(0f, rotateFinish, t);
		}
		poolMaterial.SetFloat("_Rotate", rotateCalc);
	}

	public void AddFill(float value)
	{
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
			_rateRectum.constantMax = dripMax;
			_emissionRectum.rateOverTime = _rateRectum;
			_rateAnal.constantMax = dripMax;
			_emissionAnal.rateOverTime = _rateAnal;
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
			_rateRectum.constantMax = 0f;
			_emissionRectum.rateOverTime = _rateRectum;
			_rateAnal.constantMax = 0f;
			_emissionAnal.rateOverTime = _rateAnal;
		}
		else if (targetFill > fillMin)
		{
			_rateRectum.constantMax = dripMax;
			_emissionRectum.rateOverTime = _rateRectum;
			_rateAnal.constantMax = dripMax;
			_emissionAnal.rateOverTime = _rateAnal;
		}
	}
}
