using System;
using UnityEngine;

public class RibbonFilamentTest : MonoBehaviour
{
	public Transform targetTransform;

	[Min(2f)]
	public int segmentCount = 20;

	public LineRenderer lineRenderer;

	public Vector3[] positions;

	public float amplitude = 0.5f;

	public float frequency = 1f;

	private void Start()
	{
		positions = new Vector3[segmentCount];
		lineRenderer.positionCount = segmentCount;
	}

	private void LateUpdate()
	{
		Vector3 position = base.transform.position;
		Vector3 vector = targetTransform.position - position;
		float magnitude = vector.magnitude;
		Vector3 normalized = vector.normalized;
		Vector3 rhs = Vector3.up;
		if (Mathf.Abs(Vector3.Dot(normalized, rhs)) > 0.99f)
		{
			rhs = Vector3.right;
		}
		Vector3 normalized2 = Vector3.Cross(normalized, rhs).normalized;
		for (int i = 0; i < segmentCount; i++)
		{
			float num = (float)i / (float)(segmentCount - 1);
			Vector3 vector2 = position + normalized * (magnitude * num);
			float num2 = Mathf.Sin(num * frequency * 2f * MathF.PI + Time.time * frequency) * amplitude;
			positions[i] = vector2 + normalized2 * num2;
		}
		lineRenderer.SetPositions(positions);
	}
}
