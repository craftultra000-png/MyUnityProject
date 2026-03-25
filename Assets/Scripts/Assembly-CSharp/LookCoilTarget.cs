using UnityEngine;

public class LookCoilTarget : MonoBehaviour
{
	[Space]
	public Transform lookTarget;

	public Transform parentObject;

	public Transform setPoint;

	[Header("Satus")]
	public bool unRoll;

	public Vector3 unRollRotation = new Vector3(0f, 0f, 90f);

	[Header("Calc")]
	public float targetAngle;

	public Vector3 targetDirection;

	public Vector3 rotateDirection;

	public Vector3 calcRotation;

	public Vector3 adjustRotation = new Vector3(0f, 180f, 0f);

	public float radius = 0.6f;

	private void Start()
	{
	}

	public void LinkOnAnimatorIK(int layerIndex)
	{
		if (!unRoll)
		{
			targetDirection = lookTarget.position - parentObject.position;
			Vector3 vector = parentObject.InverseTransformDirection(targetDirection);
			vector.y = 0f;
			Vector3 position = vector.normalized * radius;
			Vector3 position2 = parentObject.TransformPoint(position);
			setPoint.transform.position = position2;
			rotateDirection = parentObject.position - setPoint.position;
			calcRotation = Quaternion.LookRotation(rotateDirection, -parentObject.up).eulerAngles;
			setPoint.rotation = Quaternion.Euler(calcRotation + adjustRotation);
		}
	}
}
