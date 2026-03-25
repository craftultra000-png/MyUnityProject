using UnityEngine;

public class CharacterLookAt1 : MonoBehaviour
{
	public Animator _animator;

	[Header("Behavior")]
	[Header("Status")]
	public bool useLookHead;

	public bool useLookEyes;

	public bool player;

	public bool playerBlend;

	public bool isLook;

	public GameObject lookTarget;

	[Header("LookTarget Calc")]
	public Transform calcObjectEyes;

	public Transform calcObjectTarget;

	[Header("Player Trace Eyes")]
	public SkinnedMeshRenderer eyesRenderer;

	private Mesh eyesSkinnedMesh;

	public GameObject eyePosition;

	public Transform headBone;

	[Header("Look Param")]
	public float eyeSpeed = 50f;

	public float adjustRangeX = 15f;

	public float adjustRangeY = 0.15f;

	public float cameraAdjustY = 0.05f;

	[Header("Look Calc")]
	public Vector3 targetPosition;

	public Vector3 diffPosition;

	public Vector3 calcPosition;

	public Vector3 eyeCalc;

	public Vector3 lerpEyePosition;

	public Vector3 headPosition;

	public float targetAngle;

	[Header("Head Blend Calc")]
	public GameObject blendWaitTarget;

	public bool resetBlendWeight;

	public float blendChange;

	public float blendChangeSpeed = 5f;

	public float blendCurrentWeight;

	public float blendTargetWeight = 0.5f;

	public float blendAngleWeight;

	public float blendCalcWeight;

	public float blendSpeed = 5f;

	public AnimationCurve blendAngleCurve;

	[SerializeField]
	[Range(0f, 1f)]
	private float blendMaxWeight = 0.6f;

	[Header("Look Target")]
	public GameObject currentLookTarget;

	public GameObject targetObject;

	public Vector3 currentLookPos;

	public Vector3 targetLookPos;

	public Vector3 calcLookPos;

	public Vector3 playerAdjust = new Vector3(0f, 0f, 0f);

	public float headSpeed = 2.5f;

	public float targetChangeTime = 0.25f;

	public float targetChangeWait = 0.25f;

	[Header("LookAt Param")]
	[SerializeField]
	[Range(0f, 1f)]
	private float lookAtWeight = 1f;

	[SerializeField]
	[Range(0f, 1f)]
	private float bodyWeight;

	[SerializeField]
	[Range(0f, 1f)]
	private float headWeight = 0.4f;

	[SerializeField]
	[Range(0f, 1f)]
	private float eyesWeight;

	[SerializeField]
	[Range(0f, 1f)]
	private float clampWeight = 0.5f;

	private void Start()
	{
		useLookHead = true;
		useLookEyes = true;
		currentLookPos = Vector3.zero;
		targetLookPos = Vector3.zero;
		eyesSkinnedMesh = eyesRenderer.sharedMesh;
	}

	private void LateUpdate()
	{
		if (lookTarget != null)
		{
			playerAdjust.y = lookTarget.transform.position.y;
		}
		calcObjectEyes.position = eyePosition.transform.position;
		calcObjectEyes.rotation = eyePosition.transform.rotation;
		if (lookTarget != null && (useLookHead || useLookEyes))
		{
			if (useLookHead)
			{
				if (currentLookTarget == null)
				{
					currentLookTarget = lookTarget;
				}
				if (currentLookTarget != lookTarget)
				{
					resetBlendWeight = true;
					if (blendChange == 0f)
					{
						currentLookTarget = lookTarget;
						blendWaitTarget = currentLookTarget;
					}
				}
				else if (currentLookTarget == lookTarget)
				{
					if (currentLookTarget == blendWaitTarget)
					{
						resetBlendWeight = false;
					}
					else
					{
						resetBlendWeight = true;
						if (blendChange == 0f)
						{
							blendWaitTarget = currentLookTarget;
						}
					}
				}
				if (currentLookTarget.tag == "Player")
				{
					playerBlend = true;
				}
				else
				{
					playerBlend = false;
				}
				targetLookPos = currentLookTarget.transform.position;
				if (blendCurrentWeight == 0f)
				{
					currentLookPos = targetLookPos;
				}
				else if (currentLookPos != targetLookPos)
				{
					currentLookPos = Vector3.Lerp(currentLookPos, targetLookPos, Time.deltaTime * headSpeed);
				}
				calcLookPos = currentLookPos;
				if (blendCurrentWeight < blendMaxWeight)
				{
					blendCurrentWeight += Time.deltaTime * headSpeed;
					if (blendCurrentWeight > blendMaxWeight)
					{
						blendCurrentWeight = blendMaxWeight;
					}
				}
				if (useLookHead)
				{
					HeadWeight();
					SetHeadBelnd();
				}
			}
			LookEyes();
			SetEyesShapeKey();
		}
		if (lookTarget == null || !useLookHead)
		{
			targetChangeTime = targetChangeWait;
			if (currentLookTarget != null)
			{
				currentLookTarget = null;
			}
			else if (currentLookPos != Vector3.zero)
			{
				currentLookPos = Vector3.Lerp(currentLookPos, Vector3.zero, Time.deltaTime);
			}
			if (blendCurrentWeight > 0f)
			{
				blendCurrentWeight -= Time.deltaTime * headSpeed;
				if (blendCurrentWeight < 0f)
				{
					blendCurrentWeight = 0f;
					playerBlend = false;
					isLook = false;
				}
			}
			if (blendCalcWeight > 0f)
			{
				resetBlendWeight = true;
			}
			blendAngleWeight = 0f;
			SetHeadBelnd();
		}
		if (lookTarget == null || !useLookEyes)
		{
			calcPosition = Vector3.zero;
			SetEyesShapeKey();
		}
	}

	public void HeadWeight()
	{
		targetPosition = lookTarget.transform.position;
		headPosition = eyePosition.transform.position;
		diffPosition = targetPosition - headPosition;
		diffPosition.y = 0f;
		headPosition = eyePosition.transform.forward;
		headPosition.y = 0f;
		targetAngle = Vector3.Angle(headPosition, diffPosition);
		blendAngleWeight = targetAngle / 180f;
		if (blendAngleWeight < 0f)
		{
			blendAngleWeight = 0f;
		}
	}

	private void SetHeadBelnd()
	{
		if (blendCalcWeight < blendAngleWeight)
		{
			blendCalcWeight += Time.deltaTime * blendSpeed;
			if (blendCalcWeight > blendAngleWeight)
			{
				blendCalcWeight = blendAngleWeight;
			}
		}
		else if (blendCalcWeight > blendAngleWeight)
		{
			blendCalcWeight -= Time.deltaTime * blendSpeed;
			if (blendCalcWeight < blendAngleWeight)
			{
				blendCalcWeight = blendAngleWeight;
			}
		}
		if (resetBlendWeight)
		{
			blendChange -= Time.deltaTime * blendChangeSpeed;
			if (blendChange < 0f)
			{
				blendChange = 0f;
			}
		}
		else
		{
			blendChange += Time.deltaTime * blendChangeSpeed;
			if (blendChange > 1f)
			{
				blendChange = 1f;
			}
		}
		lookAtWeight = blendCurrentWeight * blendAngleCurve.Evaluate(blendCalcWeight) * blendChange;
	}

	public void LookEyes()
	{
		calcObjectTarget.LookAt(lookTarget.transform.position);
		calcObjectTarget.position = lookTarget.transform.position;
		calcObjectTarget.rotation = lookTarget.transform.rotation;
		Vector3 to = calcObjectTarget.position - calcObjectEyes.position;
		float x = Vector3.SignedAngle(calcObjectEyes.forward, to, calcObjectEyes.up);
		calcPosition.x = x;
		if (Mathf.Abs(calcPosition.x) < adjustRangeX)
		{
			calcPosition.x = 0f;
		}
		else if (calcPosition.x > adjustRangeX)
		{
			calcPosition.x -= adjustRangeX;
		}
		else if (calcPosition.x < 0f - adjustRangeX)
		{
			calcPosition.x += adjustRangeX;
		}
		calcPosition.x /= 2f;
		if (calcPosition.x > 100f)
		{
			calcPosition.x = 100f;
		}
		if (calcPosition.x < -100f)
		{
			calcPosition.x = -100f;
		}
		calcObjectTarget.position = lookTarget.transform.position;
		eyeCalc = calcObjectTarget.position;
		eyeCalc.y += cameraAdjustY;
		calcObjectTarget.position = eyeCalc;
		float y = calcObjectTarget.localPosition.y * 500f;
		calcPosition.y = y;
		if (Mathf.Abs(calcPosition.y) < adjustRangeY)
		{
			calcPosition.y = 0f;
		}
		else if (calcPosition.y > adjustRangeY)
		{
			calcPosition.y -= adjustRangeY;
		}
		else if (calcPosition.y < 0f - adjustRangeY)
		{
			calcPosition.y += adjustRangeY;
		}
		if (calcPosition.y > 100f)
		{
			calcPosition.y = 100f;
		}
		if (calcPosition.y < -100f)
		{
			calcPosition.y = -100f;
		}
	}

	private void SetEyesShapeKey()
	{
		if (lerpEyePosition.x > calcPosition.x)
		{
			lerpEyePosition.x -= Time.deltaTime * eyeSpeed;
			if (lerpEyePosition.x < calcPosition.x)
			{
				lerpEyePosition.x = calcPosition.x;
			}
		}
		else if (lerpEyePosition.x < calcPosition.x)
		{
			lerpEyePosition.x += Time.deltaTime * eyeSpeed;
			if (lerpEyePosition.x > calcPosition.x)
			{
				lerpEyePosition.x = calcPosition.x;
			}
		}
		eyesRenderer.SetBlendShapeWeight(2, 0f - lerpEyePosition.x);
		if (lerpEyePosition.y > calcPosition.y)
		{
			lerpEyePosition.y -= Time.deltaTime * eyeSpeed;
			if (lerpEyePosition.y < calcPosition.y)
			{
				lerpEyePosition.y = calcPosition.y;
			}
		}
		else if (lerpEyePosition.y < calcPosition.y)
		{
			lerpEyePosition.y += Time.deltaTime * eyeSpeed;
			if (lerpEyePosition.y > calcPosition.y)
			{
				lerpEyePosition.y = calcPosition.y;
			}
		}
		if (lerpEyePosition.y > 0f)
		{
			eyesRenderer.SetBlendShapeWeight(0, lerpEyePosition.y);
			eyesRenderer.SetBlendShapeWeight(1, 0f);
		}
		else if (lerpEyePosition.y < 0f)
		{
			eyesRenderer.SetBlendShapeWeight(0, 0f);
			eyesRenderer.SetBlendShapeWeight(1, 0f - lerpEyePosition.y);
		}
	}

	private void OnAnimatorIK(int layerIndex)
	{
		_animator.SetLookAtWeight(lookAtWeight, bodyWeight, headWeight, eyesWeight, clampWeight);
		_animator.SetLookAtPosition(calcLookPos);
	}
}
