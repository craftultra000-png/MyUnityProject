using UnityEngine;

public class HangPositionObject : MonoBehaviour
{
	public Transform parentObject;

	public Vector3 setPosition = new Vector3(0f, -0.25f, 0f);

	[Header("Sway")]
	public float followSpeed = 5f;

	public float swayAmount = 10f;

	public float swaySpeed = 0.5f;

	public float moveAmount = 0.1f;

	private Vector3 velocity;

	private float noiseOffsetX;

	private float noiseOffsetY;

	private float noiseOffsetZ;

	private void Start()
	{
		noiseOffsetX = Random.Range(0f, 100f);
		noiseOffsetY = Random.Range(0f, 100f);
		noiseOffsetZ = Random.Range(0f, 100f);
	}

	private void LateUpdate()
	{
		if (Time.timeScale > 0f)
		{
			Vector3 target = parentObject.position + setPosition;
			base.transform.position = Vector3.SmoothDamp(base.transform.position, target, ref velocity, 1f / followSpeed);
			float x = (Mathf.PerlinNoise(Time.time * swaySpeed, noiseOffsetX) - 0.5f) * 2f * moveAmount;
			float y = (Mathf.PerlinNoise(Time.time * swaySpeed, noiseOffsetY) - 0.5f) * 2f * (moveAmount * 0.5f);
			float z = (Mathf.PerlinNoise(Time.time * swaySpeed, noiseOffsetZ) - 0.5f) * 2f * moveAmount;
			base.transform.position += new Vector3(x, y, z);
			float x2 = (Mathf.PerlinNoise(Time.time * swaySpeed, noiseOffsetX) - 0.5f) * 2f * swayAmount;
			float z2 = (Mathf.PerlinNoise(Time.time * swaySpeed, noiseOffsetZ) - 0.5f) * 2f * swayAmount;
			Quaternion b = Quaternion.Euler(x2, 0f, z2) * Quaternion.LookRotation(Vector3.forward, -Vector3.up);
			base.transform.rotation = Quaternion.Slerp(base.transform.rotation, b, Time.deltaTime * followSpeed);
		}
	}
}
