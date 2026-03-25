using UnityEngine;

public class FeelerNoisePosition : MonoBehaviour
{
	[Header("Position")]
	public Vector3 defaultPosition;

	public Vector3 currentNoise;

	public Vector3 calcNoise;

	public Vector3 targetNoise;

	[Header("Noise")]
	public Vector3 noiseData = new Vector3(0.1f, 0.05f, 0.1f);

	public float noiseSpeed = 2f;

	[Header("Aim Adjust")]
	[Range(0f, 1f)]
	public float aimCurrent = 1f;

	private void Start()
	{
		defaultPosition = base.transform.localPosition;
		currentNoise = Vector3.zero;
		targetNoise = GenerateRandomNoise();
	}

	private void LateUpdate()
	{
		currentNoise = Vector3.Lerp(currentNoise, targetNoise, Time.deltaTime * noiseSpeed);
		calcNoise = currentNoise * aimCurrent;
		base.transform.localPosition = defaultPosition + calcNoise;
		if (Vector3.Distance(currentNoise, targetNoise) < 0.01f)
		{
			targetNoise = GenerateRandomNoise();
		}
	}

	private Vector3 GenerateRandomNoise()
	{
		return new Vector3(Random.Range(0f - noiseData.x, noiseData.x), Random.Range(0f - noiseData.y, noiseData.y), Random.Range(0f - noiseData.z, noiseData.z));
	}
}
