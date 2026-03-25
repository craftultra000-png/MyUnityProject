using System.Collections.Generic;
using UnityEngine;

public class FeelerAimObject : MonoBehaviour
{
	public List<Transform> baseBones;

	[Header("Target")]
	public Transform targetObject;

	public Transform rootObject;

	[Header("Self")]
	public Transform origin;

	public Transform endBone;

	[Header("Status")]
	public bool isAim;

	public bool isAimEnd;

	[Header("Aim Data")]
	public float aimCurrent;

	public float aimMax = 0.3f;

	public float aimMin = 0.05f;

	public float aimSpeed = 5f;

	[Header("Aim Position")]
	public int boneEndcount;

	public float distance;

	public float calcDistance;

	[Header("Aim Rotation")]
	public Vector3 defaultRotation;

	public Vector3 calcRotation;

	private void Start()
	{
		boneEndcount = baseBones.Count - 1;
		aimCurrent = aimMin;
		isAim = true;
	}

	private void FixedUpdate()
	{
		if (isAim)
		{
			if (!isAimEnd && aimCurrent < aimMax && aimCurrent < aimMax)
			{
				aimCurrent += Time.deltaTime * aimSpeed;
				if (aimCurrent > aimMax)
				{
					isAimEnd = true;
					aimCurrent = aimMax;
				}
			}
		}
		else
		{
			isAimEnd = false;
			if (aimCurrent > aimMin)
			{
				aimCurrent -= Time.deltaTime * aimSpeed;
				if (aimCurrent < aimMin)
				{
					aimCurrent = aimMin;
				}
			}
		}
		base.transform.position = Vector3.Lerp(rootObject.position, targetObject.position, aimCurrent);
		Vector3 eulerAngles = Quaternion.LookRotation((targetObject.position - rootObject.position).normalized).eulerAngles;
		origin.rotation = Quaternion.Euler(eulerAngles);
		for (int i = 0; i < boneEndcount; i++)
		{
			float t = (float)i / (float)boneEndcount;
			baseBones[i].position = Vector3.Lerp(origin.position, endBone.position, t);
		}
		endBone.position = rootObject.position;
	}
}
