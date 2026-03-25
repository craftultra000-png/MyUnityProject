using System.Collections.Generic;
using UnityEngine;

public class WartBedObject : MonoBehaviour
{
	public Animator _caharacterAnimator;

	public CharacterPositionManager _characterPositionManager;

	public CharacterLifeManager _characterLifeManager;

	public CharacterLimbIK _characterLimbIK;

	public Transform baseObject;

	public List<Transform> onomatopoeiaPoint;

	public Transform onomatopoeiaVaginaPoint;

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

	public bool isReleaseBody;

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

	public Transform armLBone;

	public Transform armRBone;

	public Quaternion armLQuaterinion;

	public Quaternion armRQuaterinion;

	public float currentRotation;

	[Range(0f, 1f)]
	public float targetRotation;

	public float rotationRangeMin = 180f;

	public float rotationRangeMax = 100f;

	public float rotationSpeed = 2f;

	public float rotationAdjustPosition = 0.1f;

	[Header("Release")]
	public float releaseTime;

	public float releaseTimeBody = 0.5f;

	[Header("Other")]
	public float setWaitTime = 1f;

	public float releaseIKTime = 0.8f;

	[Header("Open SE Wait")]
	public float openWait;

	public float openWaitMin = 1f;

	[Header("Onomatopoeia")]
	public float onomatopoeiaTime;

	public float onomatopoeiaTimeMin = 3f;

	public float onomatopoeiaTimeMax = 8f;

	[Header("Se")]
	public List<AudioClip> holdSe;

	public List<AudioClip> releaseSe;

	private void Start()
	{
		baseObject.position = defaultPosition;
		armLQuaterinion = armLBone.localRotation;
		armRQuaterinion = armRBone.localRotation;
		RotationMove();
		onomatopoeiaTime = Random.Range(onomatopoeiaTimeMin, onomatopoeiaTimeMax);
		openWait = 0f;
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
					baseObject.position = Vector3.Lerp(defaultPosition, setPosition, currentTime);
				}
			}
		}
		else if (isSet && isRelease)
		{
			if (isRotation)
			{
				releaseTime += Time.deltaTime;
				if (releaseTime > releaseTimeBody && !isReleaseBody)
				{
					isReleaseBody = true;
					_characterPositionManager.ReleaseWartBedBody();
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
				baseObject.position = Vector3.Lerp(defaultPosition, setPosition, currentTime);
			}
		}
		if (!isSet)
		{
			return;
		}
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
				OnomatopoeiaManager.instance.SpawnOnomatopoeia(onomatopoeiaPoint[Random.Range(0, onomatopoeiaPoint.Count)].position, null, "WartBed", Camera.main.transform);
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
	}

	public void RotationMove()
	{
		if (isHold)
		{
			if (openWait > 0f)
			{
				openWait -= Time.deltaTime;
			}
			if (currentRotation < targetRotation)
			{
				currentRotation += Time.deltaTime * rotationSpeed;
				if (currentRotation > targetRotation)
				{
					currentRotation = targetRotation;
					if (openWait <= 0f)
					{
						openWait = openWaitMin;
						if (OnomatopoeiaManager.instance.useOtomanopoeia)
						{
							OnomatopoeiaManager.instance.SpawnOnomatopoeia(onomatopoeiaVaginaPoint.position, null, "WartBedOpen", Camera.main.transform);
						}
						EffectSeManager.instance.PlaySe(holdSe[Random.Range(0, holdSe.Count)]);
					}
				}
			}
			else if (currentRotation > targetRotation)
			{
				currentRotation -= Time.deltaTime * rotationSpeed;
				if (currentRotation < targetRotation)
				{
					currentRotation = targetRotation;
					if (openWait <= 0f)
					{
						openWait = openWaitMin;
						EffectSeManager.instance.PlaySe(holdSe[Random.Range(0, holdSe.Count)]);
					}
				}
			}
		}
		else
		{
			currentRotation = Mathf.Lerp(currentRotation, 0f, releaseTime);
		}
		float num = Mathf.LerpAngle(rotationRangeMin, rotationRangeMax, currentRotation);
		Quaternion quaternion = Quaternion.Euler(0f, 0f, num);
		armLBone.localRotation = armLQuaterinion * Quaternion.Euler(0f, 0f, 0f - num);
		armRBone.localRotation = armRQuaterinion * quaternion;
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
		_characterPositionManager.SetWartBedBody();
		EffectSeManager.instance.PlaySe(holdSe[Random.Range(0, holdSe.Count)]);
		if (OnomatopoeiaManager.instance.useOtomanopoeia)
		{
			OnomatopoeiaManager.instance.SpawnOnomatopoeia(onomatopoeiaPoint[Random.Range(0, onomatopoeiaPoint.Count)].position, null, "WartBed", Camera.main.transform);
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
