using System.Collections.Generic;
using UnityEngine;

public class BindManager : MonoBehaviour
{
	public SqueezeManager _squeezeManager;

	[Header("Feeler")]
	public FeelerCoilObject feelerNeck;

	public FeelerCoilObject feelerLeftForearm;

	public FeelerCoilObject feelerRightForearm;

	public FeelerCoilObject feelerBody;

	public FeelerCoilObject feelerLeftLeg;

	public FeelerCoilObject feelerRightLeg;

	[Header("Extend Feeler")]
	public FeelerCoilObject feelerLeftArm;

	public FeelerCoilObject feelerRightArm;

	public FeelerCoilObject feelerLeftThigh;

	public FeelerCoilObject feelerRightThigh;

	[Header("Script")]
	public List<FeelerCoilObject> feelerScript;

	[Header("Feeler Head")]
	public FeelerCoilObject feelerHeadEyes;

	[Header("Hold")]
	public bool feeler;

	[Header("Type")]
	public bool neck;

	public bool leftForearm;

	public bool rightForearm;

	public bool body;

	public bool leftLeg;

	public bool rightLeg;

	[Header("Extend Type")]
	public bool isExtendUpper;

	public bool isExtendLower;

	public bool leftArm;

	public bool rightArm;

	public bool leftThigh;

	public bool rightThigh;

	[Header("Damage")]
	public int bindBodyCount;

	public int bindHeadCount;

	[Header("Skill Status")]
	public bool isHeadEyes;

	public bool isSqueeze;

	[Header("Gimmick Fuck Status")]
	public bool isEat;

	public bool isRide;

	public bool isLimbHold;

	public bool isWartBed;

	public bool isPillarBind;

	public bool isFuck;

	[Header("Se")]
	public List<AudioClip> coilSe;

	private void Start()
	{
		neck = true;
		leftForearm = true;
		rightForearm = true;
		body = true;
		leftLeg = true;
		rightLeg = true;
		isExtendUpper = false;
		isExtendLower = false;
		leftArm = false;
		rightArm = false;
		leftThigh = false;
		rightThigh = false;
		feeler = true;
		isHeadEyes = false;
		SetHeadEyes(isHeadEyes);
	}

	public void ChangePose()
	{
		EffectSeManager.instance.PlaySe(coilSe[Random.Range(0, coilSe.Count)]);
		if (OnomatopoeiaManager.instance.useOtomanopoeia)
		{
			feelerLeftForearm.OnomatopoeiaBind();
			feelerRightForearm.OnomatopoeiaBind();
			feelerBody.OnomatopoeiaBind();
			feelerLeftLeg.OnomatopoeiaBind();
			feelerRightLeg.OnomatopoeiaBind();
			if (isExtendUpper)
			{
				feelerLeftArm.OnomatopoeiaBind();
				feelerRightArm.OnomatopoeiaBind();
			}
			if (isExtendLower)
			{
				feelerLeftThigh.OnomatopoeiaBind();
				feelerRightThigh.OnomatopoeiaBind();
			}
		}
	}

	public void SetFeelerAll(bool value)
	{
		if (value)
		{
			SetNeck(value);
			SetLeftForearm(value);
			SetRightForearm(value);
			SetBody(value);
			SetLeftLeg(value);
			SetRightLeg(value);
			if (isExtendUpper)
			{
				SetLeftArm(value);
				SetRightArm(value);
			}
			if (isExtendLower)
			{
				SetLeftThigh(value);
				SetRightThigh(value);
			}
		}
		else
		{
			SetNeck(value);
			SetLeftForearm(value);
			SetRightForearm(value);
			SetBody(value);
			SetLeftLeg(value);
			SetRightLeg(value);
			SetLeftArm(value);
			SetRightArm(value);
			SetLeftThigh(value);
			SetRightThigh(value);
		}
	}

	public void SetNeck(bool value)
	{
		if (isEat)
		{
			return;
		}
		if (value)
		{
			neck = value;
			if (feeler)
			{
				feelerNeck.SetCoil(value: true);
			}
		}
		else if (!value)
		{
			neck = value;
			if (feeler)
			{
				feelerNeck.SetCoil(value: false);
			}
		}
	}

	public void SetHeadEyes(bool value)
	{
		if (!isEat)
		{
			if (value)
			{
				isHeadEyes = value;
				if (feeler)
				{
					feelerHeadEyes.SetCoil(value: true);
					if (feelerHeadEyes.aimCurrent == feelerHeadEyes.aimMax)
					{
						feelerHeadEyes.isCoil = true;
					}
				}
			}
			else if (!value)
			{
				isHeadEyes = value;
				if (feeler)
				{
					feelerHeadEyes.SetCoil(value: false);
				}
			}
		}
		BindCount();
	}

	public void SetLeftForearm(bool value)
	{
		if (value)
		{
			leftForearm = value;
			if (feeler)
			{
				feelerLeftForearm.SetCoil(value: true);
			}
		}
		else if (!value)
		{
			leftForearm = value;
			if (feeler)
			{
				feelerLeftForearm.SetCoil(value: false);
			}
		}
	}

	public void SetRightForearm(bool value)
	{
		if (value)
		{
			rightForearm = value;
			if (feeler)
			{
				feelerRightForearm.SetCoil(value: true);
			}
		}
		else if (!value)
		{
			rightForearm = value;
			if (feeler)
			{
				feelerRightForearm.SetCoil(value: false);
			}
		}
	}

	public void SetBody(bool value)
	{
		if (isRide)
		{
			return;
		}
		if (value)
		{
			body = value;
			if (feeler)
			{
				feelerBody.SetCoil(value: true);
			}
		}
		else if (!value)
		{
			body = value;
			if (feeler)
			{
				feelerBody.SetCoil(value: false);
			}
		}
	}

	public void SetLeftLeg(bool value)
	{
		if (isRide)
		{
			return;
		}
		if (value)
		{
			leftLeg = value;
			if (feeler)
			{
				feelerLeftLeg.SetCoil(value: true);
			}
		}
		else if (!value)
		{
			leftLeg = value;
			if (feeler)
			{
				feelerLeftLeg.SetCoil(value: false);
			}
		}
	}

	public void SetRightLeg(bool value)
	{
		if (isRide)
		{
			return;
		}
		if (value)
		{
			rightLeg = value;
			if (feeler)
			{
				feelerRightLeg.SetCoil(value: true);
			}
		}
		else if (!value)
		{
			rightLeg = value;
			if (feeler)
			{
				feelerRightLeg.SetCoil(value: false);
			}
		}
	}

	public void SetLeftArm(bool value)
	{
		Debug.LogError("Set Bind L Arm:" + value);
		if (value)
		{
			leftArm = value;
			if (feeler)
			{
				feelerLeftArm.SetCoil(value: true);
			}
		}
		else if (!value)
		{
			leftArm = value;
			if (feeler)
			{
				feelerLeftArm.SetCoil(value: false);
			}
		}
	}

	public void SetRightArm(bool value)
	{
		Debug.LogError("Set Bind R Arm:" + value);
		if (value)
		{
			rightArm = value;
			if (feeler)
			{
				feelerRightArm.SetCoil(value: true);
			}
		}
		else if (!value)
		{
			rightArm = value;
			if (feeler)
			{
				feelerRightArm.SetCoil(value: false);
			}
		}
	}

	public void SetLeftThigh(bool value)
	{
		if (isRide)
		{
			return;
		}
		if (value)
		{
			leftThigh = value;
			if (feeler)
			{
				feelerLeftThigh.SetCoil(value: true);
			}
		}
		else if (!value)
		{
			leftThigh = value;
			if (feeler)
			{
				feelerLeftThigh.SetCoil(value: false);
			}
		}
	}

	public void SetRightThigh(bool value)
	{
		if (isRide)
		{
			return;
		}
		if (value)
		{
			rightThigh = value;
			if (feeler)
			{
				feelerRightThigh.SetCoil(value: true);
			}
		}
		else if (!value)
		{
			rightThigh = value;
			if (feeler)
			{
				feelerRightThigh.SetCoil(value: false);
			}
		}
	}

	public void SetUpperBind(bool value)
	{
		EffectSeManager.instance.PlaySe(coilSe[Random.Range(0, coilSe.Count)]);
		isExtendUpper = value;
		if (isExtendUpper)
		{
			SetLeftArm(leftForearm);
			SetRightArm(rightForearm);
		}
		else if (!isExtendUpper)
		{
			SetLeftArm(value: false);
			SetRightArm(value: false);
		}
		feelerRightArm.SetSqueeze(isSqueeze);
		feelerLeftArm.SetSqueeze(isSqueeze);
		BindCount();
	}

	public void SetLowerBind(bool value)
	{
		EffectSeManager.instance.PlaySe(coilSe[Random.Range(0, coilSe.Count)]);
		isExtendLower = value;
		if (isExtendLower)
		{
			SetLeftThigh(leftLeg);
			SetRightThigh(rightLeg);
		}
		else if (!isExtendLower)
		{
			SetLeftThigh(value: false);
			SetRightThigh(value: false);
		}
		feelerRightThigh.SetSqueeze(isSqueeze);
		feelerLeftThigh.SetSqueeze(isSqueeze);
		BindCount();
	}

	public void SetSqueeze(bool value)
	{
		isSqueeze = value;
		_squeezeManager.SetSqueeze(isSqueeze);
		feelerNeck.SetSqueeze(isSqueeze);
		feelerLeftForearm.SetSqueeze(isSqueeze);
		feelerRightForearm.SetSqueeze(isSqueeze);
		feelerBody.SetSqueeze(isSqueeze);
		feelerLeftLeg.SetSqueeze(isSqueeze);
		feelerRightLeg.SetSqueeze(isSqueeze);
		if (isExtendUpper)
		{
			feelerRightArm.SetSqueeze(isSqueeze);
			feelerLeftArm.SetSqueeze(isSqueeze);
		}
		if (isExtendLower)
		{
			feelerRightThigh.SetSqueeze(isSqueeze);
			feelerLeftThigh.SetSqueeze(isSqueeze);
		}
		BindCount();
	}

	public void SqueezeOnomatopoeia()
	{
		feelerLeftForearm.OnomatopoeiaBind();
		feelerRightForearm.OnomatopoeiaBind();
		feelerBody.OnomatopoeiaBind();
		feelerLeftLeg.OnomatopoeiaBind();
		feelerRightLeg.OnomatopoeiaBind();
		if (isExtendUpper)
		{
			feelerRightArm.OnomatopoeiaBind();
			feelerLeftArm.OnomatopoeiaBind();
		}
		if (isExtendLower)
		{
			feelerRightThigh.OnomatopoeiaBind();
			feelerLeftThigh.OnomatopoeiaBind();
		}
	}

	public void BindCount()
	{
		bindBodyCount = 0;
		bindHeadCount = 0;
		if (neck)
		{
			bindBodyCount++;
		}
		if (leftForearm)
		{
			bindBodyCount++;
		}
		if (rightForearm)
		{
			bindBodyCount++;
		}
		if (body)
		{
			bindBodyCount++;
		}
		if (leftLeg)
		{
			bindBodyCount++;
		}
		if (rightLeg)
		{
			bindBodyCount++;
		}
		if (leftArm)
		{
			bindBodyCount++;
		}
		if (rightArm)
		{
			bindBodyCount++;
		}
		if (leftThigh)
		{
			bindBodyCount++;
		}
		if (rightThigh)
		{
			bindBodyCount++;
		}
		if (isHeadEyes)
		{
			bindHeadCount++;
		}
	}

	public void EatMode()
	{
		isEat = true;
		feelerNeck.SetCoil(value: false);
		feelerHeadEyes.SetCoil(value: false);
		BindCount();
	}

	public void RideMode()
	{
		isRide = true;
		feelerBody.SetCoil(value: false);
		feelerLeftLeg.SetCoil(value: false);
		feelerRightLeg.SetCoil(value: false);
		if (isExtendLower)
		{
			feelerRightThigh.SetCoil(value: false);
			feelerLeftThigh.SetCoil(value: false);
		}
		BindCount();
	}

	public void LimbHoldMode()
	{
		isLimbHold = true;
		SetFeelerAll(value: false);
		BindCount();
	}

	public void WartBedMode()
	{
		isWartBed = true;
		SetFeelerAll(value: false);
		BindCount();
	}

	public void PillarBindMode()
	{
		isPillarBind = true;
		feelerNeck.SetCoil(value: false);
		feelerBody.SetCoil(value: false);
		BindCount();
	}

	public void FuckMode()
	{
		isFuck = true;
		SetFeelerAll(value: false);
		BindCount();
	}

	public void ResetMode()
	{
		if (isEat && feeler)
		{
			feelerNeck.SetCoil(neck);
			feelerHeadEyes.SetCoil(isHeadEyes);
		}
		if (isRide && feeler)
		{
			feelerBody.SetCoil(body);
			feelerLeftLeg.SetCoil(leftLeg);
			feelerRightLeg.SetCoil(rightLeg);
			if (isExtendLower)
			{
				feelerRightThigh.SetCoil(leftThigh);
				feelerLeftThigh.SetCoil(rightThigh);
			}
		}
		if (isLimbHold && feeler)
		{
			SetFeelerAll(value: true);
		}
		if (isWartBed && feeler)
		{
			SetFeelerAll(value: true);
		}
		if (isPillarBind && feeler)
		{
			feelerNeck.SetCoil(neck);
			feelerBody.SetCoil(body);
		}
		if (isFuck && feeler)
		{
			SetFeelerAll(value: true);
		}
		isEat = false;
		isRide = false;
		isLimbHold = false;
		isWartBed = false;
		isPillarBind = false;
		isFuck = false;
		BindCount();
	}
}
