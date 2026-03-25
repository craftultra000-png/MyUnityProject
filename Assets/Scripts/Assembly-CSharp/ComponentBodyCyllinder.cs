using UnityEngine;

public class ComponentBodyCyllinder : MonoBehaviour
{
	public Transform targetModel;

	[Header("Target")]
	public string armLeft = "forearm_stretch.l";

	public string armRight = "forearm_stretch.r";

	public string legLeft = "thigh_stretch.l";

	public string legRight = "thigh_stretch.r";

	[ContextMenu("Set Up")]
	public void Setup()
	{
		Transform[] componentsInChildren = targetModel.GetComponentsInChildren<Transform>();
		foreach (Transform transform in componentsInChildren)
		{
			if (transform.name == armLeft)
			{
				Debug.Log("Found target child: " + transform.name);
			}
		}
	}
}
