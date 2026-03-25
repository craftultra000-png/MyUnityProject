using UnityEngine;

public class AnimationMakeSkinnedBoneBind : MonoBehaviour
{
	[Header("Set Data")]
	public GameObject body;

	public GameObject costume;

	[Header("Bones")]
	public Transform rootBone;

	public Transform[] bodyBones;

	public Transform[] costumeBones;

	[ContextMenu("SetBone")]
	public void SetBone()
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
