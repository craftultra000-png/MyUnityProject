using UnityEngine;

public class CharacterCostumeMove : MonoBehaviour
{
	[Header("Target")]
	public Transform targetBone;

	[Header("Move")]
	public Vector3 adjustPosition;

	private void Awake()
	{
		base.transform.SetParent(targetBone.transform);
		base.transform.localPosition = adjustPosition;
	}
}
