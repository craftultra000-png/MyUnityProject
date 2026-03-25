using Animancer;
using UnityEngine;

public class CharacterLookAtHead : MonoBehaviour
{
	public Animator _animator;

	public AnimancerComponent _animancer;

	[Header("Facial Status")]
	public bool isLookAwayHead;

	public float lookAwayHead;

	[Header("Status")]
	public bool useLookHead;

	public bool useLookBody;

	public bool isLook;

	public bool isSleep;

	public GameObject lookTarget;

	public GameObject lookTargetCurrent;

	[Header("LookTarget Calc")]
	public Transform calcObjectHead;

	public Transform calcObjectHeadTarget;

	public Transform calcObjectBody;

	public Transform calcObjectBodyTarget;

	[Header("Character Eyes")]
	public Transform eyesObject;

	public Transform rootObject;

	[Header("Player Look")]
	public bool usePlayerLook;

	public bool isPlayerLook;

	public float playerLookTime;

	public float playerLookTimeMax = 3f;

	public float playerAngle;

	public float playerAngleMax = 0.5f;

	public float playerDistance;

	public float playerDistanceMax = 1f;

	[Header("Look Calc")]
	public Vector3 currentPosition;

	public Vector3 targetPosition;

	public Vector3 calcHeadPosition;

	public Vector3 calcBodyPosition;

	public Vector3 calcPosition;

	[Header("Look Weight")]
	public Vector2 verticalRange = new Vector2(-70f, 60f);

	public Vector2 horizontalRange = new Vector2(-60f, 60f);

	public Vector2 verticalLimit = new Vector2(-70f, 70f);

	public Vector2 horizontalLimit = new Vector2(-120f, 120f);

	public float verticalAngle;

	public float horizontalAngle;

	public float verticalLimitWeight;

	public float horizontalLimitWeight;

	[Range(0f, 1f)]
	public float verticalWeight;

	[Range(0f, 1f)]
	public float horizontalWeight;

	[Header("Look Weight")]
	public float lookDistance;

	public float lookDistanceWeight;

	public float lookFeedDistance = 1f;

	public float lookLimitDistance = 2f;

	[Header("Feed Weight")]
	public float feedSpeed = 0.5f;

	public float baseWeightSpeed = 0.05f;

	[Range(0f, 1f)]
	public float weightLimit = 0.8f;

	[Range(0f, 1f)]
	public float headBodyRatio = 0.3f;

	[Range(0f, 1f)]
	public float headBodyTargetRatio = 0.5f;

	[Space]
	public float baseCurrentWeight = 0.5f;

	public float bodyCurrentWeight = 0.2f;

	public float headCurrentWeight = 0.8f;

	public float clampCurrentWeight;

	[Space]
	public float baseTargetWeight;

	public float bodyTargetWeight;

	public float headTargetWeight;

	public float clampTargetWeight;

	[Header("Weight Param")]
	[SerializeField]
	[Range(0f, 1f)]
	private float baseWeight = 1f;

	[SerializeField]
	[Range(0f, 1f)]
	private float bodyWeight = 0.4f;

	public float bodyWeightCalc;

	[SerializeField]
	[Range(0f, 1f)]
	private float headWeight = 0.8f;

	public float headWeightCalc;

	[SerializeField]
	[Range(0f, 1f)]
	private float eyesWeight;

	[SerializeField]
	[Range(0f, 1f)]
	private float clampWeight = 0.5f;

	private void Start()
	{
		_animator = _animancer.GetComponent<AnimancerComponent>().Animator;
		UseLook(head: true, body: true);
		_animancer.Layers[0].ApplyAnimatorIK = true;
		_animancer.Layers[1].ApplyAnimatorIK = true;
	}

	private void LateUpdate()
	{
		if (isLookAwayHead)
		{
			lookAwayHead += Time.deltaTime * 5f;
			if (lookAwayHead > 1f)
			{
				lookAwayHead = 1f;
			}
		}
		else if (!isLookAwayHead)
		{
			lookAwayHead -= Time.deltaTime * 5f;
			if (lookAwayHead < 0f)
			{
				lookAwayHead = 0f;
			}
		}
		if (lookTargetCurrent != null)
		{
			Vector3 vector = lookTargetCurrent.transform.position - eyesObject.position;
			playerAngle = Vector3.Dot(eyesObject.forward, vector.normalized);
			playerDistance = vector.magnitude;
			if (playerAngle > playerAngleMax && playerDistance < playerDistanceMax)
			{
				playerLookTime += Time.deltaTime;
			}
			else
			{
				playerLookTime = 0f;
			}
			if (playerLookTime >= playerLookTimeMax)
			{
				isPlayerLook = true;
			}
		}
		if (lookTarget != null && isLook)
		{
			if (lookTargetCurrent == null)
			{
				lookTargetCurrent = lookTarget;
			}
			else if (lookTargetCurrent != lookTarget)
			{
				lookTargetCurrent = lookTarget;
			}
			LookTarget();
			LookPosition();
		}
		if (lookTarget == null || !isLook)
		{
			if (lookTargetCurrent != null)
			{
				lookTargetCurrent = null;
			}
			targetPosition = Vector3.zero;
			LookPosition();
		}
		if (isLook)
		{
			if (baseCurrentWeight < baseTargetWeight)
			{
				baseCurrentWeight += Time.deltaTime * baseWeightSpeed;
				if (baseCurrentWeight > baseTargetWeight)
				{
					baseCurrentWeight = baseTargetWeight;
				}
			}
			else if (baseCurrentWeight > baseTargetWeight)
			{
				baseCurrentWeight -= Time.deltaTime * baseWeightSpeed;
				if (baseCurrentWeight < baseTargetWeight)
				{
					baseCurrentWeight = baseTargetWeight;
				}
			}
		}
		else if (baseCurrentWeight > 0f)
		{
			baseCurrentWeight -= Time.deltaTime * 2f;
			if (baseCurrentWeight < 0f)
			{
				baseCurrentWeight = 0f;
			}
		}
	}

	public void LookTarget()
	{
		calcObjectHead = rootObject.transform;
		calcObjectHeadTarget = lookTargetCurrent.transform;
		calcObjectBody = rootObject.transform;
		Vector3 vector = calcObjectHeadTarget.position - calcObjectHead.position;
		vector.Normalize();
		Vector3 vector2 = Quaternion.Inverse(rootObject.transform.rotation) * vector;
		verticalAngle = Mathf.Asin(vector2.y) * 57.29578f;
		horizontalAngle = Mathf.Atan2(vector2.x, vector2.z) * 57.29578f;
		lookDistanceWeight = 1f;
		if (lookTargetCurrent != null)
		{
			lookDistance = Vector3.Distance(calcObjectHead.position, lookTargetCurrent.transform.position);
			float num = 1f;
			if (lookDistance > lookFeedDistance)
			{
				num = Mathf.Clamp01(1f - (lookDistance - lookFeedDistance) / (lookLimitDistance - lookFeedDistance));
			}
			lookDistanceWeight *= num;
		}
		float num2 = Mathf.Clamp(verticalAngle, verticalRange.x, verticalRange.y);
		float num3 = Mathf.Abs(verticalAngle - num2);
		verticalWeight = 1f - Mathf.Clamp01(num3 / (verticalRange.y - verticalRange.x));
		float num4 = Mathf.Clamp(horizontalAngle, horizontalRange.x, horizontalRange.y);
		float num5 = Mathf.Abs(horizontalAngle - num4);
		horizontalWeight = 1f - Mathf.Clamp01(num5 / (horizontalRange.y - horizontalRange.x));
		if (verticalAngle >= verticalRange.x && verticalAngle <= verticalRange.y)
		{
			verticalLimitWeight = 1f;
		}
		else if (verticalAngle > verticalRange.y && verticalAngle <= verticalLimit.y)
		{
			float num6 = Mathf.Abs(verticalAngle - verticalRange.y);
			verticalLimitWeight = Mathf.Clamp01(1f - num6 / (verticalLimit.y - verticalRange.y));
		}
		else
		{
			verticalLimitWeight = 0f;
		}
		if (horizontalAngle >= horizontalRange.x && horizontalAngle <= horizontalRange.y)
		{
			horizontalLimitWeight = 1f;
		}
		else if (horizontalAngle > horizontalRange.y && horizontalAngle <= horizontalLimit.y)
		{
			float num7 = Mathf.Abs(horizontalAngle - horizontalRange.y);
			horizontalLimitWeight = Mathf.Clamp01(1f - num7 / (horizontalLimit.y - horizontalRange.y));
		}
		else
		{
			horizontalLimitWeight = 0f;
		}
		baseTargetWeight = Mathf.Min(verticalWeight, horizontalWeight) * horizontalLimitWeight * verticalLimitWeight;
		headBodyTargetRatio = Mathf.Lerp(headBodyRatio, 1f, 1f - baseTargetWeight);
		targetPosition = Vector3.Lerp(calcObjectHeadTarget.position, calcObjectBodyTarget.position, headBodyTargetRatio);
		baseTargetWeight = Mathf.Lerp(1f, headBodyRatio, headBodyTargetRatio);
	}

	public void LookPosition()
	{
		currentPosition = Vector3.Lerp(currentPosition, targetPosition, Time.deltaTime * feedSpeed);
	}

	public void LinkOnAnimatorIK(int layerIndex)
	{
		baseWeight = baseCurrentWeight * weightLimit;
		bodyWeight = bodyCurrentWeight * lookDistanceWeight;
		if (!useLookBody || isSleep)
		{
			if (bodyWeightCalc > 0f)
			{
				bodyWeightCalc -= Time.deltaTime;
				if (bodyWeightCalc < 0f)
				{
					bodyWeightCalc = 0f;
				}
			}
		}
		else if (bodyWeightCalc < bodyWeight)
		{
			bodyWeightCalc += Time.deltaTime;
			if (bodyWeightCalc > bodyWeight)
			{
				bodyWeightCalc = bodyWeight;
			}
		}
		else if (bodyWeightCalc > bodyWeight)
		{
			bodyWeightCalc -= Time.deltaTime;
			if (bodyWeightCalc < bodyWeight)
			{
				bodyWeightCalc = bodyWeight;
			}
		}
		headWeight = headCurrentWeight;
		if (!useLookBody || isSleep)
		{
			headWeight /= 4f;
		}
		if (!useLookHead || isSleep)
		{
			if (headWeightCalc > 0f)
			{
				headWeightCalc -= Time.deltaTime;
				if (headWeightCalc < 0f)
				{
					headWeightCalc = 0f;
				}
			}
		}
		else if (headWeightCalc < headWeight)
		{
			headWeightCalc += Time.deltaTime;
			if (headWeightCalc > headWeight)
			{
				headWeightCalc = headWeight;
			}
		}
		else if (headWeightCalc > headWeight)
		{
			headWeightCalc -= Time.deltaTime;
			if (headWeightCalc < headWeight)
			{
				headWeightCalc = headWeight;
			}
		}
		eyesWeight = 0f;
		clampWeight = clampCurrentWeight;
		_animator.SetLookAtWeight(baseWeight, bodyWeightCalc, headWeightCalc, eyesWeight, clampWeight);
		_animator.SetLookAtPosition(currentPosition);
	}

	public void UseLook(bool head, bool body)
	{
		if (head || body)
		{
			isLook = true;
		}
		else
		{
			isLook = false;
		}
		useLookHead = head;
		useLookBody = body;
	}
}
