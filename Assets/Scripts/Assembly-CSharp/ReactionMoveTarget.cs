using UnityEngine;

public class ReactionMoveTarget : MonoBehaviour
{
	[Header("Target")]
	public Transform baseObject;

	[Header("Setting")]
	[Range(0f, 5f)]
	public float rangeX = 1f;

	[Range(0f, 5f)]
	public float rangeY = 1f;

	[Range(0f, 5f)]
	public float rangeZ = 0.25f;

	[Range(0f, 10f)]
	public float speedX = 1f;

	[Range(0f, 10f)]
	public float speedY = 1f;

	[Range(0f, 10f)]
	public float speedZ = 0.5f;

	[Header("Wave")]
	[Range(0f, 10f)]
	public float waveX = 1f;

	[Range(0f, 10f)]
	public float waveY = 1f;

	[Range(0f, 10f)]
	public float waveZ = 1f;

	[Header("Calc")]
	public float calcSpeedX;

	public float calcSpeedY;

	public float calcSpeedZ;

	[Header("Position")]
	public float moveSpeed = 10f;

	public Vector3 calcNoise;

	public Vector3 defaultPosition;

	[Space]
	public Vector3 currentPosition;

	public Vector3 targetPosition;

	private void Start()
	{
		defaultPosition = baseObject.localPosition;
		currentPosition = defaultPosition;
	}

	private void LateUpdate()
	{
		calcSpeedX = Mathf.Sin(Time.time * speedX * waveX) * rangeX;
		calcSpeedY = Mathf.Cos(Time.time * speedY * waveY) * rangeY;
		calcSpeedZ = Mathf.Sin(Time.time * speedZ * waveZ) * rangeZ;
		calcNoise = new Vector3(calcSpeedX, calcSpeedY, calcSpeedZ);
		targetPosition = defaultPosition + calcNoise;
		currentPosition = Vector3.Lerp(currentPosition, targetPosition, Time.deltaTime * moveSpeed);
		baseObject.localPosition = currentPosition;
	}
}
