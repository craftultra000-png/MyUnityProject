using System.Collections.Generic;
using UnityEngine;

public class ComponentHairBone : MonoBehaviour
{
	public Transform targetModel;

	public int targetZone = 3;

	public List<Transform> targetBones = new List<Transform>();

	[ContextMenu("Find Children At Depth")]
	public void FindAndSelect()
	{
		targetBones.Clear();
		FindChildrenAtDepth(targetModel, targetZone);
		if (targetBones.Count > 0)
		{
			Debug.Log($"{targetBones.Count} child(ren) found at depth {targetZone}:");
			{
				foreach (Transform targetBone in targetBones)
				{
					Debug.Log(targetBone.name);
				}
				return;
			}
		}
		Debug.LogWarning($"No child found at depth {targetZone}.");
	}

	private void FindChildrenAtDepth(Transform parent, int depth)
	{
		if (depth <= 0)
		{
			targetBones.Add(parent);
			return;
		}
		foreach (Transform item in parent)
		{
			FindChildrenAtDepth(item, depth - 1);
		}
	}
}
