using UnityEngine;

public class CharacterLookAtEyes : MonoBehaviour
{
	[Header("Facial Status")]
	public bool isLookAwayEyes;

	public bool isAhe;

	public bool isSqueeze;

	public bool isBottom;

	public float lookAwayEyes;

	public float aheEyes;

	public float bottomEyes;

	[Header("Status")]
	public bool isLook;

	public GameObject lookTarget;

	public GameObject lookTargetCurrent;

	[Header("LookTarget Calc")]
	public Transform calcObjectEyes;

	public Transform calcObjectEyesTarget;

	[Header("Character Eyes")]
	public SkinnedMeshRenderer eyesMesh;

	private Mesh eyesSkinnedMesh;

	public Transform eyesObject;

	[Header("Look Param")]
	public float eyeSpeed = 200f;

	public float eyeFaicalSpeed = 500f;

	public float adjustRangeX = 15f;

	public float adjustRangeY = 0.15f;

	public float cameraAdjustY = 0.05f;

	[Header("Look Calc")]
	public Vector3 calcPosition;

	public Vector3 eyeCalc;

	public Vector3 lerpEyePosition;

	public float horizontalAngle;

	private void Start()
	{
		isLook = true;
		eyesSkinnedMesh = eyesMesh.sharedMesh;
	}

	private void LateUpdate()
	{
		if (isLookAwayEyes)
		{
			lookAwayEyes += Time.deltaTime * 5f;
			if (lookAwayEyes > 0.5f)
			{
				lookAwayEyes = 0.5f;
			}
		}
		else if (!isLookAwayEyes)
		{
			lookAwayEyes -= Time.deltaTime * 5f;
			if (lookAwayEyes < 0f)
			{
				lookAwayEyes = 0f;
			}
		}
		if (isAhe || isSqueeze)
		{
			aheEyes += Time.deltaTime * 5f;
			if (aheEyes > 1f)
			{
				aheEyes = 1f;
			}
		}
		else if (!isAhe && !isSqueeze)
		{
			aheEyes -= Time.deltaTime * 5f;
			if (aheEyes < 0f)
			{
				aheEyes = 0f;
			}
		}
		if (isBottom)
		{
			bottomEyes += Time.deltaTime * 5f;
			if (bottomEyes > 1f)
			{
				bottomEyes = 1f;
			}
		}
		else if (!isBottom)
		{
			bottomEyes -= Time.deltaTime * 5f;
			if (bottomEyes < 0f)
			{
				bottomEyes = 0f;
			}
		}
		calcObjectEyes.position = eyesObject.position;
		calcObjectEyes.rotation = eyesObject.rotation;
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
			LookEyes();
			SetEyesShapeKey();
		}
		if (lookTarget == null || !isLook)
		{
			if (lookTargetCurrent != null)
			{
				lookTargetCurrent = null;
			}
			calcPosition = Vector3.zero;
			SetEyesShapeKey();
		}
	}

	public void LookEyes()
	{
		calcObjectEyesTarget.LookAt(lookTarget.transform.position);
		calcObjectEyesTarget.position = lookTarget.transform.position;
		calcObjectEyesTarget.rotation = lookTarget.transform.rotation;
		Vector3 to = calcObjectEyesTarget.position - calcObjectEyes.position;
		horizontalAngle = Vector3.SignedAngle(calcObjectEyes.forward, to, calcObjectEyes.up);
		calcPosition.x = horizontalAngle;
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
		calcPosition.x *= 1.5f;
		calcObjectEyesTarget.position = lookTarget.transform.position;
		eyeCalc = calcObjectEyesTarget.position;
		eyeCalc.y += cameraAdjustY;
		calcObjectEyesTarget.position = eyeCalc;
		float y = calcObjectEyesTarget.localPosition.y * 500f;
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
		if (calcPosition.x > 100f)
		{
			calcPosition.x = 100f;
		}
		if (calcPosition.x < -100f)
		{
			calcPosition.x = -100f;
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
		if (lookAwayEyes > 0f)
		{
			if (horizontalAngle < 0f)
			{
				lerpEyePosition.x += Time.deltaTime * eyeFaicalSpeed;
				if (lerpEyePosition.x > 100f)
				{
					lerpEyePosition.x = 100f;
				}
			}
			else if (horizontalAngle > 0f)
			{
				lerpEyePosition.x -= Time.deltaTime * eyeFaicalSpeed;
				if (lerpEyePosition.x < -100f)
				{
					lerpEyePosition.x = -100f;
				}
			}
		}
		else if (lerpEyePosition.x > calcPosition.x)
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
		eyesMesh.SetBlendShapeWeight(2, 0f - lerpEyePosition.x);
		if (aheEyes > 0f)
		{
			lerpEyePosition.y += Time.deltaTime * eyeFaicalSpeed;
			if (lerpEyePosition.y > 100f)
			{
				lerpEyePosition.y = 100f;
			}
		}
		else if (bottomEyes > 0f)
		{
			lerpEyePosition.y -= Time.deltaTime * eyeFaicalSpeed;
			if (lerpEyePosition.y < -100f)
			{
				lerpEyePosition.y = -100f;
			}
		}
		else if (lerpEyePosition.y > calcPosition.y)
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
			eyesMesh.SetBlendShapeWeight(0, lerpEyePosition.y);
			eyesMesh.SetBlendShapeWeight(1, 0f);
		}
		else if (lerpEyePosition.y < 0f)
		{
			eyesMesh.SetBlendShapeWeight(0, 0f);
			eyesMesh.SetBlendShapeWeight(1, 0f - lerpEyePosition.y);
		}
	}
}
