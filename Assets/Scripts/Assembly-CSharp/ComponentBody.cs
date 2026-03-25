using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ComponentBody : MonoBehaviour
{
	public ComponentBodyConstraint _componentBodyConstraint;

	public ComponentScript _componentScript;

	public ComponentFeeler _componentFeeler;

	public CharacterMagicaColliderManager _characterMagicaColliderManager;

	public Transform baseModel;

	public Transform targetModel;

	private List<Transform> movedObjects = new List<Transform>();

	[Header("Material")]
	public Material[] materialHead;

	public Material materialEyes;

	public Material materialBody;

	[Header("Calc")]
	public Vector3 calcPosition;

	public Vector3 calcRotation;

	public Vector3 calcScale;

	[ContextMenu("Set Up")]
	public void Setup()
	{
		_componentBodyConstraint.Setup(targetModel);
		_componentFeeler.Setup(targetModel);
		_componentScript.Setup(targetModel);
		CopyMissingComponents(baseModel, targetModel);
		TransferHierarchy(baseModel, targetModel);
		_componentScript.Setup2(targetModel);
		_characterMagicaColliderManager.SetList(targetModel);
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
		if (baseTransform.GetComponent<SkinnedMeshRenderer>() != null)
		{
			SkinnedMeshRenderer component = targetTransform.GetComponent<SkinnedMeshRenderer>();
			if (component != null)
			{
				if (targetTransform.name == "0_Head")
				{
					component.materials = materialHead;
					targetTransform.gameObject.layer = 11;
				}
				if (targetTransform.name == "1_Eyes")
				{
					component.material = materialEyes;
					targetTransform.gameObject.layer = 11;
				}
				if (targetTransform.name == "2_Body")
				{
					component.material = materialBody;
					targetTransform.gameObject.layer = 11;
				}
				if (targetTransform.name == "3_UnderHair")
				{
					targetTransform.gameObject.layer = 11;
				}
				Bounds localBounds = component.localBounds;
				Vector3 center = localBounds.center;
				Vector3 size = new Vector3(1f, 1f, 1f) * 2f;
				localBounds.size = size;
				localBounds.center = center;
				component.localBounds = localBounds;
			}
		}
		_ = baseTransform.GetComponent<MeshRenderer>() != null;
		Component[] components = baseTransform.GetComponents<Component>();
		foreach (Component component2 in components)
		{
			if (!(component2 is Transform) && !(component2 is Renderer))
			{
				_ = targetTransform.GetComponent(component2.GetType()) == null;
			}
		}
	}
}
