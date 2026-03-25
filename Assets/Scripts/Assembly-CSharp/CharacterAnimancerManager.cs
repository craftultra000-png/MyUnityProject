using System.Collections.Generic;
using Animancer;
using UnityEngine;

public class CharacterAnimancerManager : MonoBehaviour
{
	public AnimancerComponent _animancer;

	public CharacterFaceManager _characterFaceManager;

	public CharacterAnimancerIK _characterAnimancerIK;

	[Header("Status")]
	public string StateName = "Idle";

	public bool isMove;

	[Header("Anim FeedTime")]
	public float feedTime = 1f;

	[Header("Animation Clip")]
	public float currentSpeed;

	public AnimationClip tPose;

	public List<AnimationClip> moveClip;

	private LinearMixerState _walkMixer;

	[Header("Hand Target IK")]
	public HandIKObject handTargetL;

	public HandIKObject handTargetR;

	[Header("HandIK Tits")]
	public HandIKObject handOutTitsL;

	public HandIKObject handOutTitsR;

	public HandIKObject handInTitsL;

	public HandIKObject handInTitsR;

	public HandIKObject handSideTitsL;

	public HandIKObject handSideTitsR;

	[Header("HandIK Palm")]
	public HandIKObject handPalmL;

	public HandIKObject handPalmR;

	[Header("Animation Clip")]
	public AnimationClip actionClip;

	public AnimationClip idleClip;

	public AnimationClip cardClip;

	public AnimationClip cardRClip;

	public AnimationClip cardIdleClip;

	public AnimationClip cardIdleRClip;

	[Header("Flinch Clip")]
	public AvatarMask flinchMask;

	public AnimancerLayer flinchLayer;

	public AnimationClip flinchIdleClip;

	public List<AnimationClip> flinchClip;

	[Header("Tits Clip")]
	public AvatarMask titsMask;

	public AnimancerLayer titsLayer;

	public int titsNum;

	public AnimationClip titsIdleClip;

	public List<AnimationClip> titsClip;

	public List<AnimationClip> titsLClip;

	public List<AnimationClip> titsRClip;

	private void Start()
	{
		_animancer.Play(tPose);
		_walkMixer = new LinearMixerState();
		_walkMixer.Add(moveClip[0], 0f);
		_walkMixer.Add(moveClip[1], 0.5f);
		StateSet("isMove", 0f, playWait: false);
		titsLayer = _animancer.Layers[3];
		titsLayer.IsAdditive = true;
		titsLayer.Play(titsIdleClip, 0.1f);
	}

	private void FixedUpdate()
	{
		if (StateName == "isMove" && currentSpeed > 0f)
		{
			isMove = true;
			if (currentSpeed > 0.5f)
			{
				currentSpeed = 0.5f;
			}
			_walkMixer.Parameter = currentSpeed;
		}
		else if (StateName == "isMove" && isMove && currentSpeed == 0f)
		{
			isMove = false;
			currentSpeed = 0f;
			_animancer.Play(moveClip[0], 0.25f);
		}
	}

	public void IdleMotion()
	{
		StateSet("isIdle", 0.25f, playWait: false);
	}

	public void ClickStateSet(string value)
	{
		StateSet(value, feedTime, playWait: false);
	}

	public void StateSet(string value, float feed, bool playWait)
	{
		Debug.Log(base.gameObject.name + " StateSet:" + value, base.gameObject);
		feedTime = feed;
		StateName = value;
		if (StateName == "isMove")
		{
			_animancer.Play(_walkMixer, 0.25f);
		}
		else if (StateName == "isIdle")
		{
			_animancer.Play(idleClip, 0.25f);
		}
		else
		{
			if (StateName == "isSkip")
			{
				return;
			}
			if (StateName == "isAction")
			{
				_animancer.Play(actionClip, feedTime);
			}
			else
			{
				if (StateName == "isActionToIdle")
				{
					return;
				}
				if (StateName == "isCard")
				{
					if (cardIdleClip == null)
					{
						_animancer.Play(cardClip, feedTime);
					}
					_characterFaceManager.SetEmote("");
				}
				else if (StateName == "isCardR" && cardRClip != null && cardIdleRClip == null)
				{
					_animancer.Play(cardRClip, feedTime);
				}
			}
		}
	}

	public void FlinchSet()
	{
		Debug.Log("Flinch Unuse", base.gameObject);
	}

	public void TitsSet(int value)
	{
		Debug.LogWarning("Tits Set:" + value, base.gameObject);
		TitsNumCheck(value);
		titsLayer.Play(titsClip[titsNum], 0.1f);
	}

	public void TitsLSet(int value)
	{
		Debug.LogWarning("Tits L Set:" + value, base.gameObject);
		TitsNumCheck(value);
		titsLayer.Play(titsLClip[titsNum], 0.1f);
	}

	public void TitsRSet(int value)
	{
		Debug.LogWarning("Tits R Set:" + value, base.gameObject);
		TitsNumCheck(value);
		titsLayer.Play(titsRClip[titsNum], 0.1f);
	}

	public void TitsNumCheck(int value)
	{
		Debug.LogWarning("Tits Num Check: " + value, base.gameObject);
		titsNum = value;
	}

	public void TitsEnd()
	{
		Debug.LogWarning("Tits End: " + 0, base.gameObject);
		titsNum = 0;
		titsLayer.Play(titsIdleClip, 0.1f);
	}

	public void SetTitsSe()
	{
		_characterAnimancerIK.isTitsSe = true;
	}

	public void ClearHand()
	{
		_characterAnimancerIK.ClearHand();
	}

	public void SetHandL(HandIKObject obj, bool value, string type, int num)
	{
		if (obj != null)
		{
			handTargetL = obj;
			_characterAnimancerIK.targetHandL = obj;
			switch (type)
			{
			case "OutTits":
				_characterAnimancerIK.isOutTits = true;
				break;
			case "InTits":
				_characterAnimancerIK.isInTits = true;
				break;
			case "SideTits":
				_characterAnimancerIK.isSideTits = true;
				break;
			case "Palm":
				_characterAnimancerIK.isPalm = true;
				break;
			default:
				Debug.LogError("Set HandL Error: " + type);
				break;
			}
			Debug.LogWarning("Set HandL: " + type, base.gameObject);
		}
		_characterAnimancerIK.SetHandL(type, value, num);
	}

	public void SetHandR(HandIKObject obj, bool value, string type, int num)
	{
		if (obj != null)
		{
			handTargetR = obj;
			_characterAnimancerIK.targetHandR = obj;
			switch (type)
			{
			case "OutTits":
				_characterAnimancerIK.isOutTits = true;
				break;
			case "InTits":
				_characterAnimancerIK.isInTits = true;
				break;
			case "SideTits":
				_characterAnimancerIK.isSideTits = true;
				break;
			case "Palm":
				_characterAnimancerIK.isPalm = true;
				break;
			default:
				Debug.LogError("Set HandR Error: " + type);
				break;
			}
			Debug.LogWarning("Set HandR: " + type, base.gameObject);
		}
		_characterAnimancerIK.SetHandR(type, value, num);
	}
}
