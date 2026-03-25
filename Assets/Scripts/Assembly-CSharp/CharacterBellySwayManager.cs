using UnityEngine;

public class CharacterBellySwayManager : MonoBehaviour
{
	[Header("Bone")]
	public Transform parentBone;

	public Transform bellyBone1;

	public Transform bellyBone2;

	[Header("Sway")]
	public float moveThreshold = 0.01f;

	public float swayForce = 5f;

	public float swayForceSmooth = 4f;

	public float swaySpeed = 20f;

	public float swayMaxAngle = 30f;

	public float damping = 0.025f;

	[Header("Gravity")]
	[Range(0f, 1f)]
	public float currentGravity;

	public float gravityForce = 5f;

	[Header("Collision")]
	public float collisionForce = 10f;

	[Header("Calc")]
	public Vector2 currentSway;

	public Vector2 calcSway;

	private Vector2 swayAcceleration;

	private Quaternion lastParentRotation;

	private Vector3 lastParentEuler;

	private Quaternion baseRot1;

	private Quaternion baseRot2;

	[Header("Weight")]
	public float bone1Weight = 1f;

	public float bone2Weight = 0.5f;

	public float xWeight = 1f;

	public float yWeight = 0.5f;

	public float zWeight = 0.1f;

	private void Start()
	{
		lastParentEuler = parentBone.eulerAngles;
		baseRot1 = bellyBone1.localRotation;
		baseRot2 = bellyBone2.localRotation;
		currentSway = Vector2.zero;
		calcSway = Vector2.zero;
		swayAcceleration = Vector2.zero;
	}

	private void LateUpdate()
	{
		Quaternion rotation = parentBone.rotation;
		Quaternion quaternion = rotation * Quaternion.Inverse(lastParentRotation);
		lastParentRotation = rotation;
		quaternion.ToAngleAxis(out var angle, out var axis);
		Vector3 vector = parentBone.InverseTransformDirection(axis) * angle;
		Vector2 vector2 = new Vector2(y: 0f - vector.x, x: 0f - vector.y) * swayForce;
		if (vector2.magnitude > moveThreshold)
		{
			swayAcceleration = Vector2.Lerp(swayAcceleration, vector2 * swaySpeed, Time.deltaTime * swayForceSmooth);
		}
		Vector3 vector3 = parentBone.rotation * Vector3.down;
		Vector2 vector4 = new Vector2(vector3.x, vector3.y) * (currentGravity * gravityForce);
		swayAcceleration += vector4;
		float deltaTime = Time.deltaTime;
		Vector2 vector5 = 2f * currentSway - calcSway + swayAcceleration * deltaTime;
		vector5 *= 1f - damping;
		swayAcceleration *= damping;
		vector5 += vector4 * deltaTime;
		if (vector5.magnitude > swayMaxAngle)
		{
			vector5 = vector5.normalized * swayMaxAngle;
			swayAcceleration = Vector2.Lerp(swayAcceleration, -swayAcceleration, Time.deltaTime * damping);
		}
		calcSway = currentSway;
		currentSway = vector5;
		Quaternion quaternion2 = Quaternion.Euler(currentSway.y * yWeight * bone1Weight, currentSway.x * xWeight * bone1Weight, 0f);
		Quaternion quaternion3 = Quaternion.Euler(currentSway.y * yWeight * bone2Weight, currentSway.x * xWeight * bone2Weight, 0f);
		bellyBone1.localRotation = baseRot1 * quaternion2;
		bellyBone2.localRotation = baseRot2 * quaternion3;
	}

	public void HitSway(Vector3 collisionPoint)
	{
		Vector3 position = bellyBone1.position;
		Vector3 normalized = (collisionPoint - position).normalized;
		Vector2 vector = new Vector2(normalized.x, normalized.y) * collisionForce;
		calcSway += vector;
		if (calcSway.magnitude > swayMaxAngle)
		{
			calcSway = calcSway.normalized * swayMaxAngle;
		}
	}
}
