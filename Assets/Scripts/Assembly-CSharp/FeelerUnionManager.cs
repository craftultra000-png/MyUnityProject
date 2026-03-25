using System.Collections.Generic;
using UnityEngine;

public class FeelerUnionManager : MonoBehaviour
{
	[Header("Self Bone")]
	public List<Transform> baseBone;

	public List<Transform> bendBone;

	public List<Transform> targetBone;

	[Header("Other")]
	public List<Transform> targetObject;

	public List<FeelerNoisePosition> _feelerNoisePosition;

	private void Start()
	{
	}

	private void LateUpdate()
	{
		for (int i = 0; i < baseBone.Count; i++)
		{
			Quaternion quaternion = Quaternion.LookRotation((targetObject[i].position - baseBone[i].position).normalized, Vector3.up);
			Quaternion quaternion2 = Quaternion.Euler(-90f, 0f, 0f);
			baseBone[i].localRotation = quaternion * quaternion2;
			float y = Vector3.Distance(baseBone[i].position, targetObject[i].position);
			Vector3 localPosition = targetBone[i].localPosition;
			localPosition.y = y;
			targetBone[i].localPosition = localPosition;
			Vector3 localPosition2 = targetBone[i].localPosition;
			_feelerNoisePosition[i].defaultPosition = localPosition2;
		}
	}
}
