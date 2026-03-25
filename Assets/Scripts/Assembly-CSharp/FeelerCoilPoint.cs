using UnityEngine;

public class FeelerCoilPoint : MonoBehaviour
{
	public float radius;

	public Transform targetTransform;

	[Header("Look")]
	public Transform baseTransform;

	public Transform lookTransform;

	public Vector3 rotationY;

	private void FixedUpdate()
	{
		Vector3 forward = baseTransform.position - lookTransform.position;
		forward.y = 0f;
		Quaternion quaternion = Quaternion.LookRotation(forward, Vector3.up);
		float y = base.transform.rotation.eulerAngles.y;
		Quaternion localRotation = Quaternion.Euler(0f, quaternion.eulerAngles.y - y, 0f);
		lookTransform.localRotation = localRotation;
	}
}
