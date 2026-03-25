using UnityEngine;

public class CharacterCostumeBind : MonoBehaviour
{
	[Header("Set Data")]
	public GameObject body;

	public GameObject costume;

	[Header("Bones")]
	public Transform rootBone;

	public Transform[] bodyBones;

	public Transform[] costumeBones;

	private void Start()
	{
		SkinnedMeshRenderer component = body.GetComponent<SkinnedMeshRenderer>();
		SkinnedMeshRenderer component2 = costume.GetComponent<SkinnedMeshRenderer>();
		rootBone = component.rootBone;
		bodyBones = component.bones;
		costumeBones = component2.bones;
		component2.rootBone = component.rootBone;
		component2.bones = component.bones;
	}
}
