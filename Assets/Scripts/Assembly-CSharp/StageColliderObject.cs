using UnityEngine;

public class StageColliderObject : MonoBehaviour
{
	public enum BodyType
	{
		Bounce = 0,
		Cannon = 1,
		Other = 2
	}

	public BodyType StageType;

	[Header("Target")]
	public Transform baseTransform;

	public Transform targetTransform;
}
