using UnityEngine;

public class DirectionalLightRotate : MonoBehaviour
{
	public float speed = 10f;

	public float angle = 30f;

	[Space]
	public float currentTime;

	public float currentAngle;

	[Header("Calc")]
	public Vector3 defaultRotation;

	public Vector3 calcRotation;

	private void Start()
	{
		defaultRotation = base.transform.rotation.eulerAngles;
	}

	private void LateUpdate()
	{
		currentTime += Time.deltaTime * speed;
		currentTime %= 360f;
		currentAngle = Mathf.Sin(currentTime) * angle;
		calcRotation = new Vector3(0f, currentAngle, 0f);
		base.transform.rotation = Quaternion.Euler(calcRotation + defaultRotation);
	}
}
