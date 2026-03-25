using System.Collections.Generic;
using System.Linq;
using MagicaCloth2;
using UnityEngine;

public class CharacterCostumeBoneBind : MonoBehaviour
{
	[Header("Set Data")]
	public GameObject body;

	public GameObject costume;

	public Transform TargetBone;

	[Header("Magica Cloth2")]
	public bool bodyCollider;

	public bool skirtCollider;

	public CharacterMagicaColliderManager _characterMagicaColliderManager;

	public MagicaCloth _magicaCloth;

	public List<MagicaCapsuleCollider> colliderList;

	[Header("Bones")]
	public Transform rootBone;

	public Transform[] bodyBones;

	public Transform[] costumeBones;

	public Transform[] diffBones;

	[Header("Folder")]
	public GameObject folder;

	[Header("Bones")]
	public List<Transform> BoneList;

	private void Start()
	{
		_magicaCloth.SerializeData.colliderCollisionConstraint.colliderList.Clear();
		if (bodyCollider)
		{
			_magicaCloth.SerializeData.colliderCollisionConstraint.colliderList.AddRange(_characterMagicaColliderManager.bodyBones);
		}
		if (skirtCollider)
		{
			_magicaCloth.SerializeData.colliderCollisionConstraint.colliderList.AddRange(_characterMagicaColliderManager.skirtBones);
		}
		SkinnedMeshRenderer component = body.GetComponent<SkinnedMeshRenderer>();
		SkinnedMeshRenderer component2 = costume.GetComponent<SkinnedMeshRenderer>();
		rootBone = component.rootBone;
		bodyBones = component.bones;
		costumeBones = component2.bones;
		component2.rootBone = component.rootBone;
		component2.bones = component.bones;
		folder = new GameObject(base.name + "_Folder");
		folder.transform.SetParent(TargetBone, worldPositionStays: false);
		foreach (Transform item in new List<Transform>(BoneList))
		{
			item.SetParent(folder.transform);
			item.localPosition = item.localPosition;
			item.localRotation = item.localRotation;
			item.localScale = item.localScale;
		}
		List<Transform> list = new List<Transform>(bodyBones);
		foreach (Transform bone in BoneList)
		{
			AddBoneAndChildren(bone, list);
		}
		List<Transform> list2 = new List<Transform>();
		List<Transform> list3 = new List<Transform>();
		Transform[] array = costumeBones;
		foreach (Transform costumeBone in array)
		{
			Transform transform = list.FirstOrDefault((Transform b) => b.name == costumeBone.name);
			if (transform != null)
			{
				list2.Add(transform);
				continue;
			}
			Transform transform2 = list.FirstOrDefault((Transform b) => b.name == costumeBone.name);
			if (transform2 != null)
			{
				list3.Add(transform2);
			}
		}
		list2.AddRange(list3);
		component2.bones = list2.ToArray();
		bodyBones = list2.ToArray();
		diffBones = component2.bones;
	}

	private void AddBoneAndChildren(Transform bone, List<Transform> bonesList)
	{
		if (!bonesList.Contains(bone))
		{
			bonesList.Add(bone);
		}
		foreach (Transform item in bone)
		{
			AddBoneAndChildren(item, bonesList);
		}
	}
}
