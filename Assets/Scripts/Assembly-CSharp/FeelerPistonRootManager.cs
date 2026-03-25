using System.Collections.Generic;
using UnityEngine;

public class FeelerPistonRootManager : MonoBehaviour
{
	public List<Transform> pointRoot;

	public List<Transform> pointTarget;

	public float horizontalAdjust = 0.5f;

	public float horizontalMax = 1f;

	public float horizontalRange = 0.075f;

	public List<float> rangeCalc;

	public List<Vector3> calcPosition;

	private void Start()
	{
		for (int i = 0; i < calcPosition.Count; i++)
		{
			calcPosition[i] = pointRoot[i].localPosition;
		}
	}

	private void LateUpdate()
	{
		for (int i = 0; i < pointRoot.Count; i++)
		{
			Vector3 localPosition = pointRoot[i].localPosition;
			float num = Vector3.Distance(pointRoot[i].position, pointTarget[i].position);
			float value = Mathf.Max(0f, num - horizontalAdjust);
			float num2 = Mathf.InverseLerp(0f, horizontalMax - horizontalAdjust, value);
			float num3 = horizontalRange * num2;
			float num4 = 0f;
			num4 = ((i != 0 && i != 1) ? num3 : (0f - num3));
			calcPosition[i] = new Vector3(num4, localPosition.y, localPosition.z);
			pointRoot[i].localPosition = calcPosition[i];
		}
	}
}
