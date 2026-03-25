using System.Collections.Generic;
using UnityEngine;

public class ComponentBoneCollider : MonoBehaviour
{
	public GameObject clotheCollider;

	public List<Transform> targetBone;

	[ContextMenu("Set Up")]
	public void Setup()
	{
		foreach (Transform item in targetBone)
		{
			GameObject gameObject = Object.Instantiate(clotheCollider, item.position, item.rotation);
			gameObject.transform.SetParent(item);
			gameObject.SetActive(value: true);
			AddCollidersToDescendants(item, gameObject);
		}
	}

	private void AddCollidersToDescendants(Transform parent, GameObject colliderPrefab)
	{
		foreach (Transform item in parent)
		{
			if (item != colliderPrefab.transform)
			{
				GameObject gameObject = Object.Instantiate(colliderPrefab, item.position, item.rotation);
				gameObject.transform.SetParent(item);
				gameObject.SetActive(value: true);
				AddCollidersToDescendants(item, gameObject);
			}
		}
	}
}
