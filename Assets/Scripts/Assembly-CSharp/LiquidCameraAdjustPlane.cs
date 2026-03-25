using System;
using UnityEngine;

public class LiquidCameraAdjustPlane : MonoBehaviour
{
	public Camera liquidCamera;

	public float distanceFromCamera = 0.1f;

	[ContextMenu("Adjust Plane")]
	public void AdjustPlane()
	{
		if (!(liquidCamera == null))
		{
			float fieldOfView = liquidCamera.fieldOfView;
			float aspect = liquidCamera.aspect;
			float num = 2f * Mathf.Tan(fieldOfView * 0.5f * (MathF.PI / 180f)) * distanceFromCamera;
			float x = num * aspect;
			base.transform.localScale = new Vector3(x, num, 1f);
			base.transform.position = liquidCamera.transform.position + liquidCamera.transform.forward * distanceFromCamera;
		}
	}
}
