using System.Collections.Generic;
using UnityEngine;

public class WallHipObject : MonoBehaviour
{
	public Animator _caharacterAnimator;

	public CharacterPositionManager _characterPositionManager;

	public CharacterLifeManager _characterLifeManager;

	public CharacterLimbIK _characterLimbIK;

	public Transform baseObject;

	public Transform holeBone;

	public Transform onomatopoeiaPoint;

	[Header("Hold Object")]
	public Transform holdLeftHand;

	public Transform holdRightHand;

	public Transform holdLeftFoot;

	public Transform holdRightFoot;

	[Header("Status")]
	public bool isSet;

	public bool isRelease;

	public bool isReleaseBody;

	public bool isIK;

	[Header("Position")]
	public Vector3 currentPosition;

	public Vector3 currentHolePosition;

	public Vector3 defaultPosition = new Vector3(0f, -2f, 0f);

	public Vector3 setPosition;

	public float currentTime;

	public float setTime = 1f;

	public float setSpeed = 2f;

	public float releaseSpeed = 2f;

	public AnimationCurve positionYCurve;

	public AnimationCurve positionZCurve;

	public AnimationCurve positionHoleYCurve;

	[Header("Rotation")]
	public float currentRotation;

	public Vector3 calcRotation;

	public AnimationCurve bodyCurve;

	[Header("Scale")]
	public Vector3 currentScale = Vector3.one;

	public float calcScale;

	public Vector3 currentHoleScale = Vector3.one;

	public Vector3 minScale = new Vector3(0.5f, 1f, 0.5f);

	public Vector3 maxScale = new Vector3(2f, 1f, 3f);

	public AnimationCurve scaleCurve;

	public AnimationCurve holeScaleXCurve;

	public AnimationCurve holeScaleZCurve;

	[Header("Other")]
	public float setWaitTime = 1f;

	public float releaseIKTime = 0.8f;

	[Header("Onomatopoeia")]
	public float onomatopoeiaTime;

	public float onomatopoeiaTimeMin = 5f;

	public float onomatopoeiaTimeMax = 15f;

	[Header("Se")]
	public List<AudioClip> holdSe;

	public List<AudioClip> releaseSe;

	private void Start()
	{
		baseObject.position = defaultPosition;
		WallSet();
		onomatopoeiaTime = Random.Range(onomatopoeiaTimeMin, onomatopoeiaTimeMax);
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
					WallSet();
				}
				return;
			}
			onomatopoeiaTime -= Time.deltaTime;
			if (onomatopoeiaTime < 0f)
			{
				onomatopoeiaTime = Random.Range(onomatopoeiaTimeMin, onomatopoeiaTimeMax);
				if (OnomatopoeiaManager.instance.useOtomanopoeia)
				{
					OnomatopoeiaManager.instance.SpawnOnomatopoeia(onomatopoeiaPoint.position, null, "Bind", Camera.main.transform);
				}
				EffectSeManager.instance.PlaySe(holdSe[Random.Range(0, holdSe.Count)]);
			}
		}
		else if (isSet && isRelease && currentTime > 0f)
		{
			currentTime -= Time.deltaTime * releaseSpeed;
			if (currentTime < releaseIKTime && isIK)
			{
				isIK = false;
				SetIK(value: false);
				_characterPositionManager.ReleaseWallHipBody();
			}
			if (currentTime < 0f)
			{
				currentTime = 0f;
				Release();
			}
			baseObject.position = Vector3.Lerp(defaultPosition, setPosition, currentTime);
			WallSet();
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

	public void WallSet()
	{
		currentPosition.y = positionYCurve.Evaluate(currentTime);
		currentPosition.z = positionZCurve.Evaluate(currentTime);
		baseObject.localPosition = currentPosition;
		currentHolePosition.y = positionHoleYCurve.Evaluate(currentTime);
		holeBone.localPosition = currentHolePosition;
		currentRotation = bodyCurve.Evaluate(currentTime);
		calcRotation.x = currentRotation;
		baseObject.localRotation = Quaternion.Euler(calcRotation);
		calcScale = scaleCurve.Evaluate(currentTime);
		baseObject.localScale = Vector3.one * calcScale;
		currentHoleScale.x = holeScaleXCurve.Evaluate(currentTime);
		currentHoleScale.z = holeScaleZCurve.Evaluate(currentTime);
		holeBone.localScale = currentHoleScale;
	}

	public void SetHold(bool value)
	{
		if (isSet != value)
		{
			if (value)
			{
				currentTime = 0f;
				isSet = true;
				isIK = true;
			}
			else
			{
				currentTime = 1f;
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
			_characterLimbIK.isLeftHand = true;
			_characterLimbIK.isRightHand = true;
			return;
		}
		_characterLimbIK.leftHandTarget = null;
		_characterLimbIK.rightHandTarget = null;
		_characterLimbIK.leftFootTarget = null;
		_characterLimbIK.rightFootTarget = null;
		_characterLimbIK.isLeftHand = false;
		_characterLimbIK.isRightHand = false;
		_characterLimbIK.isLeftFoot = false;
		_characterLimbIK.isRightFoot = false;
	}

	public void Hold()
	{
		_characterPositionManager.SetWallHipBody();
		EffectSeManager.instance.PlaySe(holdSe[Random.Range(0, holdSe.Count)]);
		if (OnomatopoeiaManager.instance.useOtomanopoeia)
		{
			OnomatopoeiaManager.instance.SpawnOnomatopoeia(onomatopoeiaPoint.position, null, "Bind", Camera.main.transform);
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
