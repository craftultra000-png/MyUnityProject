using System.Collections.Generic;
using UnityEngine;

public class ComponentRotatePlacement : MonoBehaviour
{
	public List<Transform> targetList;

	[Header("Data")]
	public float startRotate;

	[ContextMenu("Rotate Placement")]
	public void RotatePlacement()
	{
		float num = 360f / (float)targetList.Count;
		for (int i = 0; i < targetList.Count; i++)
		{
			float num2 = num * (float)i;
			Vector3 euler = new Vector3(0f, num2 + startRotate, 0f);
			targetList[i].localRotation = Quaternion.Euler(euler);
		}
	}
}
