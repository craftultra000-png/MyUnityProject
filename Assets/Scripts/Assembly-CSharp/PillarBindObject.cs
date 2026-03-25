using System.Collections.Generic;
using UnityEngine;

public class PillarBindObject : MonoBehaviour
{
	public Animator _caharacterAnimator;

	public CharacterPositionManager _characterPositionManager;

	public CharacterLifeManager _characterLifeManager;

	public CharacterLimbIK _characterLimbIK;

	public Transform baseObject;

	[Header("Hold Object")]
	public Transform holdLeftHand;

	public Transform holdRightHand;

	public Transform holdLeftFoot;

	public Transform holdRightFoot;

	[Header("Status")]
	public bool isHold;

	public bool isSet;

	public bool isRotation;

	public bool isRelease;

	public bool isReleaseBody;

	public bool isIK;

	[Header("Position")]
	public Vector3 currentPosition;

	public Vector3 defaultPosition = new Vector3(0f, -2f, -0.2f);

	public Vector3 setPosition;

	public float currentTime;

	public float setTime = 1f;

	public float setSpeed = 2f;

	public float releaseSpeed = 2f;

	[Header("Release")]
	public float releaseTime;

	public float releaseTimeBody = 0.5f;

	[Header("Other")]
	public float setWaitTime = 1f;

	public float releaseIKTime = 0.8f;

	[Header("Se")]
	public List<AudioClip> holdSe;

	public List<AudioClip> releaseSe;

	private void Start()
	{
		baseObject.localPosition = defaultPosition;
	}

	private void LateUpdate()
	{
		if (isSet && !isRelease)
		{
			if (currentTime < 1f)
			{
				currentTime += Time.deltaTime * setSpeed;
				if (currentTime > 1f)
				{
					currentTime = 1f;
					Hold();
					isIK = false;
					SetIK(value: true);
				}
				if (currentTime > 0f)
				{
					baseObject.localPosition = Vector3.Lerp(defaultPosition, setPosition, currentTime);
				}
			}
		}
		else
		{
			if (!isSet || !isRelease)
			{
				return;
			}
			if (isRotation)
			{
				releaseTime += Time.deltaTime;
				if (releaseTime > releaseTimeBody && !isReleaseBody)
				{
					isReleaseBody = true;
					_characterPositionManager.ReleasePillarBindBody();
				}
				if (releaseTime > 1f)
				{
					releaseTime = 1f;
					isRotation = false;
				}
			}
			if (currentTime > 0f && !isRotation)
			{
				currentTime -= Time.deltaTime * releaseSpeed;
				if (currentTime < releaseIKTime && isIK)
				{
					isIK = false;
					SetIK(value: false);
				}
				if (currentTime < 0f)
				{
					currentTime = 0f;
					Release();
				}
				baseObject.localPosition = Vector3.Lerp(defaultPosition, setPosition, currentTime);
			}
		}
	}

	public void HoldMove()
	{
		UpdateHoldObject(holdLeftHand, _caharacterAnimator.GetBoneTransform(HumanBodyBones.LeftUpperArm), _characterLimbIK.weightLeftHand, right: false);
		UpdateHoldObject(holdRightHand, _caharacterAnimator.GetBoneTransform(HumanBodyBones.RightUpperArm), _characterLimbIK.weightRightHand, right: true);
		UpdateHoldObject(holdLeftFoot, _caharacterAnimator.GetBoneTransform(HumanBodyBones.LeftUpperLeg), _characterLimbIK.weightLeftFoot, right: false);
		UpdateHoldObject(holdRightFoot, _caharacterAnimator.GetBoneTransform(HumanBodyBones.RightUpperLeg), _characterLimbIK.weightRightFoot, right: true);
	}

	private void UpdateHoldObject(Transform hold, Transform targetBone, float weight, bool right)
	{
	}

	public void SetHold(bool value)
	{
		if (isSet != value)
		{
			if (value)
			{
				currentTime = 0f - setWaitTime;
				isSet = true;
				isHold = true;
				isIK = true;
			}
			else
			{
				currentTime = 1f;
				releaseTime = 0f;
				isHold = false;
				isRelease = true;
				isReleaseBody = false;
				isIK = true;
			}
		}
	}

	public void SetIK(bool value)
	{
		if (value)
		{
			_characterLimbIK.leftHandTarget = holdLeftHand;
			_characterLimbIK.rightHandTarget = holdRightHand;
			_characterLimbIK.leftFootTarget = holdLeftFoot;
			_characterLimbIK.rightFootTarget = holdRightFoot;
			_characterLimbIK.isLeftHand = true;
			_characterLimbIK.isRightHand = true;
			_characterLimbIK.isLeftFoot = true;
			_characterLimbIK.isRightFoot = true;
		}
		else
		{
			_characterLimbIK.leftHandTarget = null;
			_characterLimbIK.rightHandTarget = null;
			_characterLimbIK.leftFootTarget = null;
			_characterLimbIK.rightFootTarget = null;
			_characterLimbIK.isLeftHand = false;
			_characterLimbIK.isRightHand = false;
			_characterLimbIK.isLeftFoot = false;
			_characterLimbIK.isRightFoot = false;
		}
	}

	public void Hold()
	{
		isRotation = true;
		_characterPositionManager.SetPillarBindBody();
		EffectSeManager.instance.PlaySe(holdSe[Random.Range(0, holdSe.Count)]);
	}

	public void Release()
	{
		isSet = false;
		isRelease = false;
	}

	public void ReleaseEnd()
	{
		_characterPositionManager.SetDefaultEnd();
	}
}
