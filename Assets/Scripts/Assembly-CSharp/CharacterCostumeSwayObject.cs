using UnityEngine;

public class CharacterCostumeSwayObject : MonoBehaviour
{
	[Header("Target Bone Settings")]
	public Transform bone;

	public Quaternion baseRotation = Quaternion.identity;

	[Header("Rotation Limit (Local X & Z)")]
	public Vector3 rotationLimitMin = new Vector3(-20f, 0f, -20f);

	public Vector3 rotationLimitMax = new Vector3(30f, 0f, 30f);

	[Header("Sway Settings")]
	public float gravityForce = 1f;

	public float damping = 0.1f;

	public float swayMaxAngle = 45f;

	private Vector2 currentSway;

	private Vector2 calcSway;

	private Vector2 swayAcceleration;

	private void Start()
	{
		baseRotation = bone.localRotation;
	}

	private void LateUpdate()
	{
		Vector3 down = Vector3.down;
		Vector2 vector = new Vector2(down.x, down.z) * gravityForce;
		swayAcceleration += vector * Time.deltaTime;
		float deltaTime = Time.deltaTime;
		Vector2 vector2 = 2f * currentSway - calcSway + swayAcceleration * deltaTime * deltaTime;
		vector2 *= 1f - damping;
		swayAcceleration *= damping;
		vector2 += vector * deltaTime;
		if (vector2.magnitude > swayMaxAngle)
		{
			vector2 = vector2.normalized * swayMaxAngle;
			swayAcceleration = Vector2.Lerp(swayAcceleration, -swayAcceleration, deltaTime * damping);
		}
		calcSway = currentSway;
		currentSway = vector2;
		Quaternion quaternion = Quaternion.Euler(currentSway.y, 0f, currentSway.x);
		Vector3 euler = ClampEulerAngles((baseRotation * quaternion).eulerAngles, rotationLimitMin, rotationLimitMax);
		bone.localRotation = Quaternion.Euler(euler);
	}

	private Vector3 ClampEulerAngles(Vector3 euler, Vector3 min, Vector3 max)
	{
		euler.x = ClampAngle(euler.x, min.x, max.x);
		euler.z = ClampAngle(euler.z, min.z, max.z);
		return euler;
	}

	private float ClampAngle(float angle, float min, float max)
	{
		angle = NormalizeAngle(angle);
		min = NormalizeAngle(min);
		max = NormalizeAngle(max);
		return Mathf.Clamp(angle, min, max);
	}

	private float NormalizeAngle(float angle)
	{
		while (angle > 180f)
		{
			angle -= 360f;
		}
		while (angle < -180f)
		{
			angle += 360f;
		}
		return angle;
	}
}
