using Unity.Mathematics;
using UnityEngine;

public class CharacterLimbIK : MonoBehaviour
{
	public Animator _animator;

	[Header("Targets")]
	public Transform leftHandTarget;

	public Transform rightHandTarget;

	public Transform leftFootTarget;

	public Transform rightFootTarget;

	[Header("Status")]
	public bool isLeftHand;

	public bool isRightHand;

	public bool isLeftFoot;

	public bool isRightFoot;

	[Header("Weight & Speed")]
	[Range(0f, 1f)]
	public float weightLeftHand;

	[Range(0f, 1f)]
	public float weightRightHand;

	[Range(0f, 1f)]
	public float weightLeftFoot;

	[Range(0f, 1f)]
	public float weightRightFoot;

	public float weightStartSpeed = 0.5f;

	public float weightEndSpeed = 0.25f;

	[Header("Position")]
	public Vector3 currentLeftHandPosition;

	public Vector3 currentRightHandPosition;

	public Vector3 currentLeftFootPosition;

	public Vector3 currentRightFootPosition;

	[Space]
	public quaternion currentLeftHandRotation;

	public quaternion currentRightHandRotation;

	public quaternion currentLeftFootRotation;

	public quaternion currentRightFootRotation;

	[Header("Rotation Adjust")]
	public Vector3 rotationAdjustLeftHand = new Vector3(-90f, 180f, 0f);

	public Vector3 rotationAdjustRightHand = new Vector3(-90f, 180f, 0f);

	public Vector3 rotationAdjustLeftFoot = new Vector3(-90f, 180f, 0f);

	public Vector3 rotationAdjustRightFoot = new Vector3(-90f, 180f, 0f);

	private void LateUpdate()
	{
		SetWeight(ref weightLeftHand, isLeftHand);
		SetWeight(ref weightRightHand, isRightHand);
		SetWeight(ref weightLeftFoot, isLeftFoot);
		SetWeight(ref weightRightFoot, isRightFoot);
	}

	public void SetWeight(ref float weight, bool use)
	{
		if (use && weight < 1f)
		{
			weight += Time.deltaTime * weightStartSpeed;
			if (weight > 1f)
			{
				weight = 1f;
			}
		}
		else if (!use && weight > 0f)
		{
			weight -= Time.deltaTime * weightEndSpeed;
			if (weight < 0f)
			{
				weight = 0f;
			}
		}
	}

	private void OnAnimatorIK(int layerIndex)
	{
		if (layerIndex == 0)
		{
			ApplyIK(AvatarIKGoal.LeftHand, HumanBodyBones.LeftHand, leftHandTarget, weightLeftHand, rotationAdjustLeftHand);
			ApplyIK(AvatarIKGoal.RightHand, HumanBodyBones.RightHand, rightHandTarget, weightRightHand, rotationAdjustRightHand);
			ApplyIK(AvatarIKGoal.LeftFoot, HumanBodyBones.LeftFoot, leftFootTarget, weightLeftFoot, rotationAdjustLeftFoot);
			ApplyIK(AvatarIKGoal.RightFoot, HumanBodyBones.RightFoot, rightFootTarget, weightRightFoot, rotationAdjustRightFoot);
		}
	}

	private void ApplyIK(AvatarIKGoal goal, HumanBodyBones bone, Transform target, float weight, Vector3 rotationAdjust)
	{
		Transform boneTransform = _animator.GetBoneTransform(bone);
		Vector3 goalPosition = boneTransform.position;
		Quaternion rotation = boneTransform.rotation;
		if (target != null)
		{
			switch (goal)
			{
			case AvatarIKGoal.LeftHand:
				goalPosition = Vector3.Lerp(currentLeftHandPosition, target.position, weight);
				rotation = Quaternion.Slerp(currentLeftHandRotation, target.rotation, weight);
				currentLeftHandPosition = goalPosition;
				currentLeftHandRotation = rotation;
				break;
			case AvatarIKGoal.RightHand:
				goalPosition = Vector3.Lerp(currentRightHandPosition, target.position, weight);
				rotation = Quaternion.Slerp(currentRightHandRotation, target.rotation, weight);
				currentRightHandPosition = goalPosition;
				currentRightHandRotation = rotation;
				break;
			case AvatarIKGoal.LeftFoot:
				goalPosition = Vector3.Lerp(currentLeftFootPosition, target.position, weight);
				rotation = Quaternion.Slerp(currentLeftFootRotation, target.rotation, weight);
				currentLeftFootPosition = goalPosition;
				currentLeftFootRotation = rotation;
				break;
			case AvatarIKGoal.RightFoot:
				goalPosition = Vector3.Lerp(currentRightFootPosition, target.position, weight);
				rotation = Quaternion.Slerp(currentRightFootRotation, target.rotation, weight);
				currentRightFootPosition = goalPosition;
				currentRightFootRotation = rotation;
				break;
			}
		}
		else
		{
			switch (goal)
			{
			case AvatarIKGoal.LeftHand:
				goalPosition = Vector3.Lerp(boneTransform.position, currentLeftHandPosition, weight);
				rotation = Quaternion.Slerp(boneTransform.rotation, currentLeftHandRotation, weight);
				currentLeftHandPosition = goalPosition;
				currentLeftHandRotation = rotation;
				break;
			case AvatarIKGoal.RightHand:
				goalPosition = Vector3.Lerp(boneTransform.position, currentRightHandPosition, weight);
				rotation = Quaternion.Slerp(boneTransform.rotation, currentRightHandRotation, weight);
				currentRightHandPosition = goalPosition;
				currentRightHandRotation = rotation;
				break;
			case AvatarIKGoal.LeftFoot:
				goalPosition = Vector3.Lerp(boneTransform.position, currentLeftFootPosition, weight);
				rotation = Quaternion.Slerp(boneTransform.rotation, currentLeftFootRotation, weight);
				currentLeftFootPosition = goalPosition;
				currentLeftFootRotation = rotation;
				break;
			case AvatarIKGoal.RightFoot:
				goalPosition = Vector3.Lerp(boneTransform.position, currentRightFootPosition, weight);
				rotation = Quaternion.Slerp(boneTransform.rotation, currentRightFootRotation, weight);
				currentRightFootPosition = goalPosition;
				currentRightFootRotation = rotation;
				break;
			}
		}
		_animator.SetIKPositionWeight(goal, weight);
		_animator.SetIKPosition(goal, goalPosition);
	}
}
