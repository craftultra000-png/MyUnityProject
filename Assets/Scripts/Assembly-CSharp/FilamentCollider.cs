using UnityEngine;

public class FilamentCollider : MonoBehaviour
{
	public Rigidbody _rigidBody;

	public Vector3 defaultPosition;

	[Header("Rotation")]
	public Vector3 currentRotation;

	public Vector3 targetRotation;

	public Vector3 calcRotation;

	[Header("Setting")]
	[Range(0f, 90f)]
	public float rotationLimit = 30f;

	[Range(0.01f, 1f)]
	public float returnSpeed = 0.1f;

	[Range(0.01f, 1f)]
	public float lerpSpeed = 0.2f;

	private void Start()
	{
		defaultPosition = base.transform.localPosition;
	}

	private void LateUpdate()
	{
		currentRotation = base.transform.localRotation.eulerAngles;
		currentRotation = Vector3.Lerp(currentRotation, Vector3.zero, Time.deltaTime * returnSpeed);
		base.transform.localRotation = Quaternion.Euler(currentRotation);
		base.transform.localPosition = defaultPosition;
	}

	private Vector3 NormalizeAngles(Vector3 angles)
	{
		angles.x = NormalizeAngle(angles.x);
		angles.y = NormalizeAngle(angles.y);
		angles.z = NormalizeAngle(angles.z);
		return angles;
	}

	private float NormalizeAngle(float angle)
	{
		if (angle > 180f)
		{
			angle -= 360f;
		}
		if (angle < -180f)
		{
			angle += 360f;
		}
		return angle;
	}
}
