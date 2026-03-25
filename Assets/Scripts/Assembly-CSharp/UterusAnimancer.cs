using Animancer;
using UnityEngine;

public class UterusAnimancer : MonoBehaviour
{
	public AnimancerComponent _animancer;

	public UterusChildFeelerManager _uterusChildFeelerManager;

	public UterusController _uterusController;

	public UterusPoolManager _uterusPoolManager;

	[Header("SpawnPoint")]
	public Transform movePoint;

	public AnimationCurve movePointPositionY;

	public Vector3 calcPosition;

	[Header("Layer")]
	public AnimancerLayer animancerLayer1;

	[Header("Status")]
	public bool isBirth;

	public int birthCount;

	public Transform cervixBone;

	public float cervixScaleCurrent = 1f;

	public float cervixScaleCalc = 1f;

	public float cervixScaleMax = 4f;

	public float cervixScaleUpSpeed = 0.25f;

	public float cervixScaleDownSpeed = 0.25f;

	[Space]
	public float cervixScaleFeelerSize = 0.2f;

	public Vector3 cervixScale = Vector3.one;

	public float cervixOpenSpeed = 10f;

	public float cervixCloseSpeed = 2f;

	[Header("Anim Param")]
	[Range(0f, 1f)]
	public float conceiveTargetParameter;

	public float conceiveCurrentParameter;

	public float conceiveBeforeParameter;

	public bool sizeSetBefore;

	public float feedTime = 0.25f;

	[Header("Animation Clip")]
	public AnimationClip idleClip;

	public AnimationClip conceiveClip;

	private AnimancerState _state;

	private StringAsset _ParameterName;

	private Parameter<float> _parameter;

	private LinearMixerState _conceiveMixer = new LinearMixerState();

	private void Start()
	{
		animancerLayer1 = _animancer.Layers[1];
		animancerLayer1.IsAdditive = false;
		animancerLayer1.Weight = 0f;
		_conceiveMixer.Add(idleClip, 0f);
		_conceiveMixer.Add(conceiveClip, 1f);
		_conceiveMixer.Parameter = conceiveTargetParameter;
		conceiveCurrentParameter = conceiveTargetParameter;
		animancerLayer1.Play(_conceiveMixer, feedTime);
		SetUterusSize();
	}

	private void LateUpdate()
	{
		if (conceiveBeforeParameter != conceiveTargetParameter)
		{
			conceiveBeforeParameter = conceiveTargetParameter;
			sizeSetBefore = false;
		}
		if (conceiveCurrentParameter < conceiveTargetParameter)
		{
			conceiveCurrentParameter += Time.deltaTime * cervixScaleUpSpeed;
			if (conceiveCurrentParameter > conceiveTargetParameter)
			{
				conceiveCurrentParameter = conceiveTargetParameter;
				SetUterusSize();
				SyncUterusPool();
			}
			_conceiveMixer.Parameter = conceiveCurrentParameter;
		}
		else if (conceiveCurrentParameter > conceiveTargetParameter)
		{
			if (!sizeSetBefore)
			{
				sizeSetBefore = true;
				SetUterusSize();
			}
			conceiveCurrentParameter -= Time.deltaTime * cervixScaleDownSpeed;
			if (conceiveCurrentParameter < conceiveTargetParameter)
			{
				conceiveCurrentParameter = conceiveTargetParameter;
				SyncUterusPool();
			}
			_conceiveMixer.Parameter = conceiveCurrentParameter;
		}
		if (isBirth)
		{
			if (cervixScaleCurrent < cervixScaleCalc)
			{
				cervixScaleCurrent += Time.deltaTime * cervixOpenSpeed;
				if (cervixScaleCurrent > cervixScaleCalc)
				{
					cervixScaleCurrent = cervixScaleCalc;
				}
				cervixScale.x = cervixScaleCurrent;
				cervixBone.localScale = cervixScale;
			}
		}
		else if (cervixScaleCurrent > 1f)
		{
			cervixScaleCurrent -= Time.deltaTime * cervixCloseSpeed;
			if (cervixScaleCurrent < 1f)
			{
				cervixScaleCurrent = 1f;
			}
			cervixScale.x = cervixScaleCurrent;
			cervixBone.localScale = cervixScale;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Character"))
		{
			if (birthCount == 0)
			{
				_uterusController.Birth(value: true);
			}
			birthCount++;
			isBirth = true;
			float currentSize = other.GetComponent<UterusChildFeelerObject>().currentSize;
			if (cervixScaleFeelerSize < currentSize)
			{
				cervixScaleFeelerSize = currentSize;
			}
			float t = (cervixScaleFeelerSize - 0.2f) / 0.2f;
			cervixScaleCalc = Mathf.Lerp(1f, cervixScaleMax, t);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Character"))
		{
			birthCount--;
			if (birthCount == 0)
			{
				isBirth = false;
				cervixScaleFeelerSize = 0.2f;
			}
		}
	}

	public void SetConceive(float value)
	{
		conceiveTargetParameter += value;
		if (conceiveTargetParameter < 0f)
		{
			conceiveTargetParameter = 0f;
		}
		else if (conceiveTargetParameter > 1f)
		{
			conceiveTargetParameter = 1f;
		}
	}

	public void SetUterusSize()
	{
		calcPosition = movePoint.localPosition;
		calcPosition.y = movePointPositionY.Evaluate(conceiveTargetParameter);
		movePoint.localPosition = calcPosition;
		_uterusChildFeelerManager.conceiveParameter = conceiveTargetParameter;
		_uterusPoolManager.conceiveParameter = conceiveTargetParameter;
		_uterusChildFeelerManager.ResetMovePoint();
	}

	public void SyncUterusPool()
	{
		_uterusPoolManager.CheckFill();
	}
}
