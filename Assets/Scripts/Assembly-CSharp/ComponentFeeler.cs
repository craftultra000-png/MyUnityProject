using UnityEngine;

public class ComponentFeeler : MonoBehaviour
{
	public FeelerAdjustPosition unionRight;

	public FeelerAdjustPosition unionLeft;

	public FeelerAdjustPosition unionNeck;

	public FeelerAdjustPosition unionHip;

	public FeelerAdjustPosition unionTitsR;

	public FeelerAdjustPosition unionTitsL;

	public FeelerAdjustPosition unionHead;

	public FeelerAdjustPosition unionBody;

	[ContextMenu("Set Up")]
	public void Setup(Transform target)
	{
		AddBoneRecursive(target);
	}

	private void AddBoneRecursive(Transform target)
	{
		foreach (Transform item in target)
		{
			if (item.name == "forearm_stretch.r")
			{
				unionRight.targetPosition = item.transform;
			}
			if (item.name == "forearm_stretch.l")
			{
				unionLeft.targetPosition = item.transform;
			}
			if (item.name == "neck.x")
			{
				unionNeck.targetPosition = item.transform;
			}
			if (item.name == "root.x")
			{
				unionHip.targetPosition = item.transform;
			}
			if (item.name == "spine_03.x")
			{
				unionBody.targetPosition = item.transform;
			}
			AddBoneRecursive(item);
		}
	}
}
