using Animancer;
using UnityEngine;

public class CharacterAnimancerIK : MonoBehaviour
{
	public Animator _animator;

	public CharacterSoundManager _characterSoundManager;

	[Header("Type")]
	public int handNum;

	public bool isTitsSe;

	public bool isOutTits;

	public bool isInTits;

	public bool isSideTits;

	public bool isPalm;

	[Header("Status")]
	public bool useHandL;

	public bool useHandR;

	[Header("Weight Calc")]
	[Range(0f, 1f)]
	public float handWeightL;

	[Range(0f, 1f)]
	public float handWeightR;

	public float weightSpeed = 2f;

	[Header("Hand Adjust")]
	public Vector3 rotationAdjusthandL = new Vector3(-90f, 180f, 0f);

	public Vector3 rotationAdjusthandR = new Vector3(-90f, 180f, 0f);

	[Header("Hand IK Calc")]
	public Transform leftHandTarget;

	public Transform rightHandTarget;

	[Header("Hand Position")]
	public Vector3 handRPosition;

	[Header("Hand")]
	public HandIKObject handL;

	public HandIKObject handR;

	[Header("Hand Target")]
	public HandIKObject targetHandL;

	public HandIKObject targetHandR;

	[Header("Target Script")]
	public CharacterAnimancerManager _characterAnimancerManager;

	private void Start()
	{
		_animator = GetComponent<AnimancerComponent>().Animator;
		targetHandL = null;
		targetHandR = null;
	}

	private void LateUpdate()
	{
		if (useHandL && handWeightL < 1f)
		{
			handWeightL += Time.deltaTime * weightSpeed;
			if (handWeightL > 1f)
			{
				handWeightL = 1f;
				if (isOutTits || isInTits || isSideTits)
				{
					if (isTitsSe)
					{
						isTitsSe = false;
						_characterSoundManager.TitsSe();
					}
					if (useHandR)
					{
						_characterAnimancerManager.TitsSet(handNum);
					}
					else if (isOutTits)
					{
						_characterAnimancerManager.TitsRSet(handNum);
					}
					else if (isInTits || isSideTits)
					{
						_characterAnimancerManager.TitsLSet(handNum);
					}
					targetHandL.SetHandAnim(handNum);
				}
			}
		}
		else if (!useHandL && handWeightL > 0f)
		{
			handWeightL -= Time.deltaTime * weightSpeed;
			if (handWeightL < 0f)
			{
				handWeightL = 0f;
			}
		}
		if (useHandR && handWeightR < 1f)
		{
			handWeightR += Time.deltaTime * weightSpeed;
			if (handWeightR > 1f)
			{
				handWeightR = 1f;
				if (isOutTits || isInTits || isSideTits)
				{
					if (!useHandL)
					{
						if (isTitsSe)
						{
							isTitsSe = false;
							_characterSoundManager.TitsSe();
						}
						if (isOutTits)
						{
							_characterAnimancerManager.TitsLSet(handNum);
						}
						else if (isInTits || isSideTits)
						{
							_characterAnimancerManager.TitsRSet(handNum);
						}
					}
					targetHandR.SetHandAnim(handNum);
				}
			}
		}
		else if (!useHandR && handWeightR > 0f)
		{
			handWeightR -= Time.deltaTime * weightSpeed;
			if (handWeightR < 0f)
			{
				handWeightR = 0f;
			}
		}
		if (handWeightL > 0f && !isPalm)
		{
			UpdateFingerL();
		}
		if (handWeightR > 0f && !isPalm)
		{
			UpdateFingerR();
		}
	}

	public void ClearHand()
	{
		useHandL = false;
		useHandR = false;
		isTitsSe = false;
		isOutTits = false;
		isInTits = false;
		isSideTits = false;
		isPalm = false;
		if (targetHandL != null)
		{
			targetHandL.gameObject.SetActive(value: false);
			targetHandL.SetHandAnim(0);
		}
		if (targetHandR != null)
		{
			targetHandR.gameObject.SetActive(value: false);
			targetHandR.SetHandAnim(0);
		}
		if (_characterAnimancerManager != null)
		{
			_characterAnimancerManager.TitsEnd();
		}
		_characterAnimancerManager = null;
	}

	public void SetHandL(string type, bool value, int num)
	{
		Debug.LogWarning("Set HandL type:" + type + " :" + value, base.gameObject);
		if (type == "OutTits" && value)
		{
			if (useHandL && isOutTits)
			{
				Debug.LogError("OutHand End?", base.gameObject);
				targetHandL.SetHandAnim(0);
				if (_characterAnimancerManager != null)
				{
					_characterAnimancerManager.TitsEnd();
				}
				handWeightL = 0f;
			}
			useHandL = true;
			isOutTits = true;
			isInTits = false;
			isSideTits = false;
			handNum = num;
			targetHandL.gameObject.SetActive(value: true);
			_characterAnimancerManager = targetHandL._characterAnimancerManager;
			return;
		}
		if (type == "InTits" && value)
		{
			useHandL = true;
			isOutTits = false;
			isInTits = true;
			isSideTits = false;
			handNum = num;
			targetHandL.gameObject.SetActive(value: true);
			_characterAnimancerManager = targetHandL._characterAnimancerManager;
			return;
		}
		if (type == "SideTits" && value)
		{
			useHandL = true;
			isOutTits = false;
			isInTits = false;
			isSideTits = true;
			handNum = num;
			targetHandL.gameObject.SetActive(value: true);
			_characterAnimancerManager = targetHandL._characterAnimancerManager;
			return;
		}
		switch (type)
		{
		case "OutTits":
		case "InTits":
		case "SideTits":
			useHandL = false;
			isOutTits = false;
			isInTits = false;
			isSideTits = false;
			handNum = 0;
			targetHandL.SetHandAnim(0);
			if (_characterAnimancerManager != null)
			{
				_characterAnimancerManager.TitsEnd();
			}
			break;
		case "Palm":
			useHandL = value;
			isPalm = value;
			break;
		}
	}

	public void SetHandR(string type, bool value, int num)
	{
		Debug.LogWarning("Set HandR type:" + type + " :" + value, base.gameObject);
		if (type == "OutTits" && value)
		{
			if (useHandR && isOutTits)
			{
				Debug.LogError("OutHand End?", base.gameObject);
				targetHandR.SetHandAnim(0);
				if (_characterAnimancerManager != null)
				{
					_characterAnimancerManager.TitsEnd();
				}
				handWeightR = 0f;
			}
			useHandR = true;
			isOutTits = true;
			isInTits = false;
			isSideTits = false;
			handNum = num;
			targetHandR.gameObject.SetActive(value: true);
			_characterAnimancerManager = targetHandR._characterAnimancerManager;
			return;
		}
		if (type == "InTits" && value)
		{
			useHandR = true;
			isOutTits = false;
			isInTits = true;
			isSideTits = false;
			handNum = num;
			targetHandR.gameObject.SetActive(value: true);
			_characterAnimancerManager = targetHandR._characterAnimancerManager;
			return;
		}
		if (type == "SideTits" && value)
		{
			useHandR = true;
			isOutTits = false;
			isInTits = false;
			isSideTits = true;
			handNum = num;
			targetHandR.gameObject.SetActive(value: true);
			_characterAnimancerManager = targetHandR._characterAnimancerManager;
			return;
		}
		switch (type)
		{
		case "OutTits":
		case "InTits":
		case "SideTits":
			useHandR = false;
			isOutTits = false;
			isInTits = false;
			isSideTits = false;
			handNum = 0;
			targetHandR.SetHandAnim(0);
			if (_characterAnimancerManager != null)
			{
				_characterAnimancerManager.TitsEnd();
			}
			break;
		case "Palm":
			useHandR = value;
			isPalm = value;
			break;
		}
	}

	public void UpdateFingerL()
	{
		if (!(targetHandL != null))
		{
			return;
		}
		for (int i = 0; i < handL.fingerBones.Count; i++)
		{
			if (handL.fingerBones[i].name == "hand.l")
			{
				Quaternion.Slerp(handL.fingerBones[i].rotation, targetHandL.hand.rotation, handWeightL);
			}
			else
			{
				handL.fingerBones[i].localRotation = Quaternion.Slerp(handL.fingerBones[i].localRotation, targetHandL.fingerBones[i].localRotation, handWeightL);
			}
		}
	}

	public void UpdateFingerR()
	{
		if (!(targetHandR != null))
		{
			return;
		}
		for (int i = 0; i < handL.fingerBones.Count; i++)
		{
			if (handR.fingerBones[i].name == "hand.r")
			{
				Quaternion.Slerp(handR.fingerBones[i].rotation, targetHandR.hand.rotation, handWeightR);
			}
			else
			{
				handR.fingerBones[i].localRotation = Quaternion.Slerp(handR.fingerBones[i].localRotation, targetHandR.fingerBones[i].localRotation, handWeightR);
			}
		}
	}

	private void OnAnimatorIK(int layerIndex)
	{
		if (layerIndex == 0)
		{
			Transform boneTransform = _animator.GetBoneTransform(HumanBodyBones.LeftHand);
			if (targetHandL == null)
			{
				leftHandTarget.position = boneTransform.position;
				leftHandTarget.rotation = boneTransform.rotation;
			}
			else
			{
				leftHandTarget.position = Vector3.Lerp(boneTransform.position, targetHandL.hand.position, handWeightL);
				leftHandTarget.rotation = Quaternion.Slerp(boneTransform.rotation, targetHandL.hand.rotation, handWeightL);
			}
			_animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, handWeightL);
			_animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, handWeightL);
			_animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandTarget.position);
			_animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandTarget.rotation * Quaternion.Euler(rotationAdjusthandL));
			Transform boneTransform2 = _animator.GetBoneTransform(HumanBodyBones.RightHand);
			handRPosition = _animator.GetBoneTransform(HumanBodyBones.RightHand).position;
			if (targetHandR == null)
			{
				rightHandTarget.position = boneTransform2.position;
				rightHandTarget.rotation = boneTransform2.rotation;
			}
			else
			{
				rightHandTarget.position = Vector3.Lerp(boneTransform2.position, targetHandR.hand.position, handWeightR);
				rightHandTarget.rotation = Quaternion.Slerp(boneTransform2.rotation, targetHandR.hand.rotation, handWeightR);
			}
			_animator.SetIKPositionWeight(AvatarIKGoal.RightHand, handWeightR);
			_animator.SetIKRotationWeight(AvatarIKGoal.RightHand, handWeightR);
			_animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandTarget.position);
			_animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandTarget.rotation * Quaternion.Euler(rotationAdjusthandR));
		}
		if (layerIndex == 3)
		{
			_animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, handWeightL);
			_animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, handWeightL);
			_animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandTarget.position);
			_animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandTarget.rotation * Quaternion.Euler(rotationAdjusthandL));
			_animator.SetIKPositionWeight(AvatarIKGoal.RightHand, handWeightR);
			_animator.SetIKRotationWeight(AvatarIKGoal.RightHand, handWeightR);
			_animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandTarget.position);
			_animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandTarget.rotation * Quaternion.Euler(rotationAdjusthandR));
		}
	}
}
