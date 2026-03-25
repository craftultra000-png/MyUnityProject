using System.Collections.Generic;
using UnityEngine;

public class LimbEntombedObject : MonoBehaviour
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

	public Vector3 defaultRotationL = new Vector3(180f, 90f, 90f);

	public Vector3 defaultRotationR = new Vector3(0f, 90f, 90f);

	[Header("Status")]
	public bool isHold;

	public bool isSet;

	public bool isRotation;

	public bool isRelease;

	public bool isIK;

	[Header("Position")]
	public Vector3 currentPosition;

	public Vector3 defaultPosition = new Vector3(0f, -2f, 0f);

	public Vector3 setPosition;

	public float currentTime;

	public float setTime = 1f;

	public float setSpeed = 2f;

	public float releaseSpeed = 2f;

	[Header("Rotation")]
	public Transform rootBone;

	public float currentRotation;

	[Range(-1f, 1f)]
	public float targetRotation;

	public float rotationRange = 75f;

	public float rotationSpeed = 2f;

	public float rotationAdjustPosition = 0.1f;

	[Header("Release")]
	public float releaseTime;

	[Header("Onomatopoeia")]
	public float onomatopoeiaTime;

	public float onomatopoeiaTimeMin = 5f;

	public float onomatopoeiaTimeMax = 15f;

	public Transform onomatopoeiaLeftHand;

	public Transform onomatopoeiaRightHand;

	public Transform onomatopoeiaLeftFoot;

	public Transform onomatopoeiaRightFoot;

	[Header("Other")]
	public float scaleMin = 0.3f;

	public float setWaitTime = 1f;

	public float releaseIKTime = 0.8f;

	[Header("Se")]
	public List<AudioClip> holdSe;

	public List<AudioClip> releaseSe;

	private void Start()
	{
		baseObject.position = defaultPosition;
		onomatopoeiaTime = Random.Range(onomatopoeiaTimeMin, onomatopoeiaTimeMax);
	}

	private void LateUpdate()
	{
		if (isSet && !isRelease)
		{
			if (currentTime < 1f)
			{
				currentTime += Time.deltaTime * setSpeed;
				if (currentTime > 0f && isIK)
				{
					isIK = false;
					SetIK(value: true);
				}
				if (currentTime > 1f)
				{
					currentTime = 1f;
					Hold();
				}
				if (currentTime > 0f)
				{
					baseObject.position = Vector3.Lerp(defaultPosition, setPosition, currentTime);
				}
			}
		}
		else if (isSet && isRelease)
		{
			if (isRotation)
			{
				releaseTime += Time.deltaTime;
				if (releaseTime > 1f)
				{
					releaseTime = 1f;
					isRotation = false;
					_characterPositionManager.ReleaseLimbHoldBody();
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
				baseObject.position = Vector3.Lerp(defaultPosition, setPosition, currentTime);
			}
		}
		if (!isSet)
		{
			return;
		}
		HoldMove();
		if (isRotation)
		{
			RotationMove();
		}
		onomatopoeiaTime -= Time.deltaTime;
		if (onomatopoeiaTime < 0f)
		{
			onomatopoeiaTime = Random.Range(onomatopoeiaTimeMin, onomatopoeiaTimeMax);
			if (OnomatopoeiaManager.instance.useOtomanopoeia)
			{
				OnomatopoeiaManager.instance.SpawnOnomatopoeia(onomatopoeiaLeftHand.position, onomatopoeiaLeftHand, "Bind", Camera.main.transform);
				OnomatopoeiaManager.instance.SpawnOnomatopoeia(onomatopoeiaRightHand.position, onomatopoeiaRightHand, "Bind", Camera.main.transform);
				OnomatopoeiaManager.instance.SpawnOnomatopoeia(onomatopoeiaLeftFoot.position, onomatopoeiaLeftFoot, "Bind", Camera.main.transform);
				OnomatopoeiaManager.instance.SpawnOnomatopoeia(onomatopoeiaRightFoot.position, onomatopoeiaRightFoot, "Bind", Camera.main.transform);
			}
			EffectSeManager.instance.PlaySe(holdSe[Random.Range(0, holdSe.Count)]);
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
		hold.LookAt(targetBone.position, Vector3.forward);
		if (!right)
		{
			hold.rotation *= Quaternion.Euler(defaultRotationL);
		}
		else
		{
			hold.rotation *= Quaternion.Euler(defaultRotationR);
		}
		Vector3 localScale = hold.localScale;
		localScale.y = Mathf.Lerp(scaleMin, 1f, currentTime);
		hold.localScale = localScale;
	}

	public void RotationMove()
	{
		if (isHold)
		{
			currentRotation = Mathf.Lerp(currentRotation, targetRotation, Time.deltaTime * rotationSpeed);
		}
		else
		{
			currentRotation = Mathf.Lerp(currentRotation, 0f, releaseTime);
		}
		float x = currentRotation * (0f - rotationRange);
		Vector3 localEulerAngles = rootBone.localEulerAngles;
		rootBone.localEulerAngles = new Vector3(x, localEulerAngles.y, localEulerAngles.z);
		Vector3 localPosition = rootBone.localPosition;
		localPosition.z = currentRotation * rotationAdjustPosition;
		rootBone.localPosition = localPosition;
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
		_characterPositionManager.SetLimbHoldBody();
		EffectSeManager.instance.PlaySe(holdSe[Random.Range(0, holdSe.Count)]);
		if (OnomatopoeiaManager.instance.useOtomanopoeia)
		{
			OnomatopoeiaManager.instance.SpawnOnomatopoeia(onomatopoeiaLeftHand.position, onomatopoeiaLeftHand, "Bind", Camera.main.transform);
			OnomatopoeiaManager.instance.SpawnOnomatopoeia(onomatopoeiaRightHand.position, onomatopoeiaRightHand, "Bind", Camera.main.transform);
			OnomatopoeiaManager.instance.SpawnOnomatopoeia(onomatopoeiaLeftFoot.position, onomatopoeiaLeftFoot, "Bind", Camera.main.transform);
			OnomatopoeiaManager.instance.SpawnOnomatopoeia(onomatopoeiaRightFoot.position, onomatopoeiaRightFoot, "Bind", Camera.main.transform);
		}
	}

	public void Release()
	{
		isSet = false;
		isRelease = false;
		EffectSeManager.instance.PlaySe(releaseSe[Random.Range(0, releaseSe.Count)]);
	}

	public void ReleaseEnd()
	{
		_characterPositionManager.SetDefaultEnd();
	}
}
