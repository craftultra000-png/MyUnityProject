using System.Collections.Generic;
using UnityEngine;

public class FeelerAim : MonoBehaviour
{
	public FeelerCoil _feelerCoil;

	public FeelerCoilPoint _feelerCoilPoint;

	public List<Transform> bones;

	public Transform parentBase;

	public Transform boneBase;

	public Transform aimTarget;

	public Transform endBone;

	[Header("Status")]
	public bool isAim;

	public bool isCoil;

	[Header("Aim Data")]
	public float aimCurrent;

	public float aimMin = 0.3f;

	public float aimSpeed = 5f;

	[Header("Aim Position")]
	public int boneEndcount;

	public float distance;

	public float calcDistance;

	[Header("Aim Rotation")]
	public Vector3 defaultRotation;

	public Vector3 spinRotation;

	public Vector3 calcRotation;

	public Vector3 boneEndRange;

	private void Start()
	{
		boneEndcount = bones.Count - 1;
		aimCurrent = aimMin;
	}

	private void FixedUpdate()
	{
		Vector3 normalized = (aimTarget.position - parentBase.position).normalized;
		calcRotation = Quaternion.LookRotation(normalized, Vector3.forward).eulerAngles;
		parentBase.rotation = Quaternion.Euler(calcRotation);
		endBone.position = Vector3.Lerp(bones[0].position, aimTarget.position, aimCurrent);
		distance = Vector3.Distance(bones[0].position, endBone.position);
		calcDistance = (distance - boneEndRange.y) / distance;
		bones[boneEndcount].position = Vector3.Lerp(bones[0].position, endBone.position, calcDistance);
		for (int i = 1; i < boneEndcount; i++)
		{
			float t = (float)i / (float)boneEndcount;
			bones[i].position = Vector3.Lerp(bones[0].position, bones[boneEndcount].position, t);
		}
		spinRotation = defaultRotation;
		boneBase.localRotation = Quaternion.Euler(spinRotation);
		Quaternion rotation = Quaternion.Slerp(bones[boneEndcount - 1].rotation, endBone.rotation, 0.5f);
		bones[boneEndcount].rotation = rotation;
		if (isAim && aimCurrent < 1f)
		{
			if (aimCurrent < 1f)
			{
				aimCurrent += Time.deltaTime * aimSpeed;
				if (aimCurrent > 1f)
				{
					aimCurrent = 1f;
				}
			}
			return;
		}
		if (isAim && aimCurrent >= 1f)
		{
			_feelerCoil.isAim = true;
			return;
		}
		_feelerCoil.isAim = false;
		if (!_feelerCoil.isCoil && aimCurrent > aimMin)
		{
			aimCurrent -= Time.deltaTime * aimSpeed;
			if (aimCurrent < aimMin)
			{
				aimCurrent = aimMin;
			}
		}
	}
}
