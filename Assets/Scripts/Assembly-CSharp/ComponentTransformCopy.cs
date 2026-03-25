using UnityEngine;

public class ComponentTransformCopy : MonoBehaviour
{
	public Transform origin;

	public Transform target;

	[ContextMenu("Set Up")]
	public void Setup()
	{
		CopyTransformRecursively(origin, target);
	}

	private void CopyTransformRecursively(Transform originParent, Transform targetParent)
	{
		foreach (Transform item in originParent)
		{
			Transform transform2 = targetParent.Find(item.name);
			if (transform2 != null)
			{
				transform2.position = item.position;
				transform2.rotation = item.rotation;
				transform2.localScale = item.localScale;
				Debug.Log(item.name + " の Transform をコピーしました。");
				CopyTransformRecursively(item, transform2);
			}
		}
	}
}
