using Animancer;
using UnityEngine;

public class AnalAnimancer : MonoBehaviour
{
	public AnimancerComponent _animancer;

	public AnalChildFeelerManager _analChildFeelerManager;

	public AnalController _analController;

	public AnalPoolManager _analPoolManager;

	[Header("SpawnPoint")]
	public Transform movePoint;

	public AnimationCurve movePointPositionY;

	public Vector3 calcPosition;

	[Header("Layer")]
	public AnimancerLayer animancerLayer1;

	[Header("Status")]
	public bool isBirth;

	public int birthCount;

	[Space]
	public float scaleUpSpeed = 0.25f;

	public float scaleDownSpeed = 0.25f;

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
			conceiveCurrentParameter += Time.deltaTime * scaleUpSpeed;
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
			conceiveCurrentParameter -= Time.deltaTime * scaleDownSpeed;
			if (conceiveCurrentParameter < conceiveTargetParameter)
			{
				conceiveCurrentParameter = conceiveTargetParameter;
				SyncUterusPool();
			}
			_conceiveMixer.Parameter = conceiveCurrentParameter;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Character"))
		{
			if (birthCount == 0)
			{
				_analController.Birth(value: true);
			}
			birthCount++;
			isBirth = true;
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
		_analChildFeelerManager.conceiveParameter = conceiveTargetParameter;
		_analPoolManager.conceiveParameter = conceiveTargetParameter;
		_analChildFeelerManager.ResetMovePoint();
	}

	public void SyncUterusPool()
	{
		_analPoolManager.CheckFill();
	}
}
