using System;
using System.Collections.Generic;
using UnityEngine;

public class FeelerCoil : MonoBehaviour
{
	public List<Transform> bones;

	public Transform coilBase;

	public Transform coilTarget;

	[Header("Status")]
	public bool isAim;

	public bool isCoil;

	[Header("Set")]
	public float coilRadius = 0.2f;

	public float boneLength = 0.15f;

	public float coilSpeed = 20f;

	public float coilSlideSpeed = 20f;

	[Header("Aim Data")]
	public float endCoilSpeed = 20f;

	public float endCoilLerp;

	[Header("Calc")]
	public float radiusRange;

	public float boneSplitCount;

	public float slideAngle;

	public float currentSlideAngle;

	public float boneAngle;

	public float currentBoneAngle;

	[Header("End Feeler")]
	public Vector3 boneEndRotation;

	public Vector3 boneEndRotationFix = new Vector3(0f, 0f, -90f);

	public Vector3 calcBoneEndRotation;

	public float calcBoneDistribute;

	public Vector3 calcBoneDistributeRotation;

	private void Start()
	{
		SetCoil();
	}

	private void FixedUpdate()
	{
		if (isAim)
		{
			isCoil = true;
			currentBoneAngle += Time.deltaTime * coilSpeed;
			if (currentBoneAngle > boneAngle)
			{
				currentBoneAngle = boneAngle;
			}
			currentSlideAngle += Time.deltaTime * coilSlideSpeed;
			if (currentSlideAngle > slideAngle)
			{
				currentSlideAngle = slideAngle;
			}
			boneEndRotation = coilTarget.rotation.eulerAngles + boneEndRotationFix;
			if (endCoilLerp < 1f)
			{
				endCoilLerp += Time.deltaTime * endCoilSpeed;
				if (endCoilLerp > 1f)
				{
					endCoilLerp = 1f;
				}
			}
			calcBoneEndRotation = Vector3.Lerp(Vector3.zero, boneEndRotation, endCoilLerp);
			coilBase.rotation = Quaternion.Euler(calcBoneEndRotation);
		}
		else
		{
			currentBoneAngle -= Time.deltaTime * coilSpeed;
			if (currentBoneAngle < 0f)
			{
				currentBoneAngle = 0f;
			}
			currentSlideAngle -= Time.deltaTime * coilSlideSpeed * 2f;
			if (currentSlideAngle < 0f)
			{
				currentSlideAngle = 0f;
			}
			if (currentBoneAngle <= 0f && currentSlideAngle <= 0f)
			{
				isCoil = false;
			}
			boneEndRotation = coilTarget.rotation.eulerAngles + boneEndRotationFix;
			if (endCoilLerp > 0f)
			{
				endCoilLerp -= Time.deltaTime * endCoilSpeed;
				if (endCoilLerp < 0f)
				{
					endCoilLerp = 0f;
				}
			}
			calcBoneEndRotation = Vector3.Lerp(Vector3.zero, boneEndRotation, endCoilLerp);
			coilBase.rotation = Quaternion.Euler(calcBoneEndRotation);
		}
		for (int i = 0; i < bones.Count; i++)
		{
			bones[i].localRotation = Quaternion.Euler(currentBoneAngle, 0f, 0f - currentSlideAngle);
		}
	}

	private void SetCoil()
	{
		radiusRange = coilRadius * MathF.PI;
		boneSplitCount = radiusRange / boneLength;
		boneAngle = 360f / boneSplitCount;
		slideAngle = 60f / boneSplitCount;
	}
}
