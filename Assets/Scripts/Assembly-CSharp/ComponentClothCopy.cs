using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ComponentClothCopy : MonoBehaviour
{
	public Transform baseModel;

	public Transform targetModel;

	private List<Transform> movedObjects = new List<Transform>();

	[Header("Calc")]
	public Vector3 calcPosition;

	public Vector3 calcRotation;

	public Vector3 calcScale;

	[ContextMenu("Set Up")]
	public void Setup()
	{
		CopyMissingComponents(baseModel, targetModel);
		TransferHierarchy(baseModel, targetModel);
	}

	private void TransferHierarchy(Transform baseBone, Transform targetBone)
	{
		List<Transform> list = new List<Transform>();
		string[] source = new string[3] { "cc_bulge.l", "cc_bulge.r", "cc_bulge.x" };
		foreach (Transform item in baseBone)
		{
			if (!source.Contains(item.name))
			{
				list.Add(item);
			}
		}
		int num = 0;
		while (list.Count > 0)
		{
			num++;
			if (num > 1000)
			{
				Debug.LogError("Infinite loop detected in TransferHierarchy! ");
				break;
			}
			List<Transform> list2 = new List<Transform>();
			foreach (Transform item2 in list)
			{
				Transform transform2 = FindChildByName(targetBone, item2.name);
				if (transform2 != null)
				{
					Debug.Log("Error target:" + transform2.name);
				}
				if (transform2 != null)
				{
					CopyMissingComponents(item2, transform2);
					TransferHierarchy(item2, transform2);
					continue;
				}
				calcPosition = item2.localPosition;
				calcScale = item2.localScale;
				item2.SetParent(targetBone, worldPositionStays: true);
				item2.localPosition = calcPosition;
				item2.localScale = calcScale;
			}
			foreach (Transform item3 in list)
			{
				if (!item3.IsChildOf(targetBone) && FindChildByName(targetBone, item3.name) == null)
				{
					list2.Add(item3);
				}
			}
			list = list2;
		}
	}

	private Transform FindChildByName(Transform parent, string name)
	{
		foreach (Transform item in parent)
		{
			if (item.name == name)
			{
				return item;
			}
		}
		return null;
	}

	private void RestoreTransform(Transform original, Transform target)
	{
		target.localPosition = original.localPosition;
		target.localRotation = original.localRotation;
		target.localScale = original.localScale;
	}

	private void CopyMissingComponents(Transform baseTransform, Transform targetTransform)
	{
		SkinnedMeshRenderer component = baseTransform.GetComponent<SkinnedMeshRenderer>();
		if (component != null)
		{
			SkinnedMeshRenderer component2 = targetTransform.GetComponent<SkinnedMeshRenderer>();
			if (component2 != null)
			{
				component2.sharedMaterials = component.sharedMaterials;
				targetTransform.gameObject.layer = baseTransform.gameObject.layer;
				Bounds localBounds = component2.localBounds;
				Vector3 center = localBounds.center;
				Vector3 size = new Vector3(1f, 1f, 1f) * 2f;
				localBounds.size = size;
				localBounds.center = center;
				component2.localBounds = localBounds;
			}
		}
		_ = baseTransform.GetComponent<MeshRenderer>() != null;
		Component[] components = baseTransform.GetComponents<Component>();
		foreach (Component component3 in components)
		{
			if (!(component3 is Transform) && !(component3 is Renderer))
			{
				_ = targetTransform.GetComponent(component3.GetType()) == null;
			}
		}
	}
}
