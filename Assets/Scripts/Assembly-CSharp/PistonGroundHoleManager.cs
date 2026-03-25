using UnityEngine;

public class PistonGroundHoleManager : MonoBehaviour
{
	public Transform rootBone;

	public Transform holeBone;

	[Header("Status")]
	public bool isAway;

	public Vector3 awayPosition = new Vector3(0.35f, 0f, 0f);

	[Header("Position")]
	public Vector3 defaultPosition;

	public Vector3 calcPosition;

	public float maxDistance = 0.35f;

	public float lerpSpeed = 2f;

	public float threshold = 0.1f;

	public float raycastDistance = 10f;

	public LayerMask groundLayer;

	[Header("Scale")]
	public Vector3 currentScale;

	public Vector3 calcScale;

	public float scaleMin = 0.9f;

	public float scaleMax = 1.1f;

	public float scaleSpeed = 1f;

	private void Start()
	{
		currentScale = holeBone.localScale;
	}

	private void LateUpdate()
	{
		if (isAway)
		{
			holeBone.position = Vector3.Lerp(holeBone.position, awayPosition, lerpSpeed * Time.deltaTime);
			return;
		}
		Vector3 vector = -rootBone.up;
		Vector3 vector2 = rootBone.position + rootBone.up * 0.1f;
		Debug.DrawRay(vector2, vector * raycastDistance, Color.red);
		if (Physics.Raycast(vector2, vector, out var hitInfo, raycastDistance, groundLayer))
		{
			Debug.DrawRay(hitInfo.point, Vector3.up * 0.2f, Color.green);
			calcPosition = new Vector3(hitInfo.point.x, 0f, hitInfo.point.z);
		}
		Vector3 vector3 = calcPosition - defaultPosition;
		if (vector3.magnitude > maxDistance)
		{
			calcPosition = defaultPosition + vector3.normalized * maxDistance;
		}
		if (Vector2.Distance(new Vector2(holeBone.position.x, holeBone.position.z), new Vector2(calcPosition.x, calcPosition.z)) > threshold)
		{
			holeBone.position = Vector3.Lerp(holeBone.position, calcPosition, lerpSpeed * Time.deltaTime);
		}
		float t = Mathf.PerlinNoise(Time.time * scaleSpeed, 0f);
		float t2 = Mathf.PerlinNoise(0f, Time.time * scaleSpeed);
		calcScale.x = Mathf.Lerp(scaleMin, scaleMax, t);
		calcScale.z = Mathf.Lerp(scaleMin, scaleMax, t2);
		calcScale.y = currentScale.y;
		holeBone.localScale = calcScale;
	}
}
