using UnityEngine;

public class CharacterAnimationSpeedManager : MonoBehaviour
{
	public QuickSkillGUI _quickSkillGUI;

	[Header("Character Animancer")]
	public MotionAnimancer _motionAnimancer;

	public MotionFuckAnimancer _motionFuckAnimancer;

	[Header("Piston Vagina")]
	public PistonAnimancer _feelerVaginaAnimancer;

	public UterusController _uterusController;

	[Header("Piston Anal")]
	public PistonAnimancer _feelerAnalAnimancer;

	public AnalController _analController;

	[Header("Piston Mouth")]
	public PistonAnimancer _feelerMouthAnimancer;

	[Header("Piston Tits")]
	public PistonAnimancer _feelerTitsAnimancer;

	[Header("Open Vagina")]
	public FilamentVaginaOpenObject _filamentVaginaOpenObject;

	[Header("Horse Ride")]
	public HorseRideObject _horseRideObject;

	[Header("Limb Hold")]
	public LimbEntombedObject _limbEntombedObject;

	[Header("Bed Open")]
	public WartBedObject _wartBedObject;

	[Header("Status")]
	public bool isVaginaPiston;

	public bool isAnalPiston;

	[Header("Volume Setting")]
	public float vaginaPistonSpeed;

	public float analPistonSpeed;

	public float mouthPistonSpeed;

	public float titsPistonSpeed;

	public float vaginaOpenSpeed;

	public float horseRideSpeed;

	public float horseRidePower;

	public float limbHoldAngle;

	public float bedOpenAngle;

	public AnimationCurve vaginaPiston;

	public AnimationCurve analPiston;

	public AnimationCurve mouthPiston;

	public AnimationCurve titsPiston;

	private void Start()
	{
	}

	public void CheckPiston()
	{
		if (isVaginaPiston && !isAnalPiston)
		{
			_motionAnimancer.pistonMixParameter = vaginaPistonSpeed;
			_motionFuckAnimancer.pistonMixParameter = vaginaPistonSpeed;
			float num = 0f;
			num = vaginaPiston.Evaluate(vaginaPistonSpeed);
			_motionAnimancer.ChangeAnimationSpeed(num);
			_motionFuckAnimancer.ChangeAnimationSpeed(num);
		}
		if (!isVaginaPiston && isAnalPiston)
		{
			_motionAnimancer.pistonMixParameter = analPistonSpeed;
			_motionFuckAnimancer.pistonMixParameter = analPistonSpeed;
			float num2 = 0f;
			num2 = analPiston.Evaluate(analPistonSpeed);
			_motionAnimancer.ChangeAnimationSpeed(num2);
			_motionFuckAnimancer.ChangeAnimationSpeed(num2);
		}
	}

	public void ChangeVaginaPiston(float value)
	{
		vaginaPistonSpeed = value;
		float num = vaginaPiston.Evaluate(vaginaPistonSpeed);
		float value2 = 0f;
		if (isAnalPiston)
		{
			if (vaginaPistonSpeed >= analPistonSpeed)
			{
				value2 = num;
				_motionAnimancer.pistonMixParameter = vaginaPistonSpeed;
				_motionFuckAnimancer.pistonMixParameter = vaginaPistonSpeed;
			}
			else if (vaginaPistonSpeed < analPistonSpeed)
			{
				value2 = analPiston.Evaluate(analPistonSpeed);
				_motionAnimancer.pistonMixParameter = analPistonSpeed;
				_motionFuckAnimancer.pistonMixParameter = analPistonSpeed;
			}
		}
		else
		{
			value2 = num;
			_motionAnimancer.pistonMixParameter = vaginaPistonSpeed;
			_motionFuckAnimancer.pistonMixParameter = vaginaPistonSpeed;
		}
		_motionAnimancer.ChangeAnimationSpeed(value2);
		_motionFuckAnimancer.ChangeAnimationSpeed(value2);
		_uterusController.ChangeAnimationSpeed(num);
		_feelerVaginaAnimancer.ChangeAnimationSpeed(num);
	}

	public void ChangeAnalPiston(float value)
	{
		analPistonSpeed = value;
		float num = analPiston.Evaluate(analPistonSpeed);
		float value2 = 0f;
		if (isVaginaPiston)
		{
			if (vaginaPistonSpeed >= analPistonSpeed)
			{
				value2 = vaginaPiston.Evaluate(vaginaPistonSpeed);
				_motionAnimancer.pistonMixParameter = vaginaPistonSpeed;
				_motionFuckAnimancer.pistonMixParameter = vaginaPistonSpeed;
			}
			else if (vaginaPistonSpeed < analPistonSpeed)
			{
				value2 = num;
				_motionAnimancer.pistonMixParameter = analPistonSpeed;
				_motionFuckAnimancer.pistonMixParameter = analPistonSpeed;
			}
		}
		else
		{
			value2 = num;
			_motionAnimancer.pistonMixParameter = analPistonSpeed;
			_motionFuckAnimancer.pistonMixParameter = analPistonSpeed;
		}
		_motionAnimancer.ChangeAnimationSpeed(value2);
		_motionFuckAnimancer.ChangeAnimationSpeed(value2);
		_analController.ChangeAnimationSpeed(num);
		_feelerAnalAnimancer.ChangeAnimationSpeed(num);
	}

	public void ChangeMouthPiston(float value)
	{
		mouthPistonSpeed = value;
		float value2 = mouthPiston.Evaluate(mouthPistonSpeed);
		_feelerMouthAnimancer.ChangeAnimationSpeed(value2);
	}

	public void ChangeTitsPiston(float value)
	{
		titsPistonSpeed = value;
		float value2 = titsPiston.Evaluate(titsPistonSpeed);
		_feelerTitsAnimancer.ChangeAnimationSpeed(value2);
	}

	public void ChangeVaginaOpen(float value)
	{
		vaginaOpenSpeed = value;
		_filamentVaginaOpenObject.targetOpen = vaginaOpenSpeed;
	}

	public void ChangeHorseRideSpeed(float value)
	{
		horseRideSpeed = value;
		_horseRideObject.shakeSpeedCurrent = horseRideSpeed;
	}

	public void ChangeHorseRidePower(float value)
	{
		horseRidePower = value;
		_horseRideObject.shakePowerCurrnet = horseRidePower;
	}

	public void ChangeLimbHoldAngle(float value)
	{
		limbHoldAngle = value;
		_limbEntombedObject.targetRotation = limbHoldAngle;
	}

	public void ChangeBedOpenAngle(float value)
	{
		bedOpenAngle = value;
		_wartBedObject.targetRotation = bedOpenAngle;
	}
}
