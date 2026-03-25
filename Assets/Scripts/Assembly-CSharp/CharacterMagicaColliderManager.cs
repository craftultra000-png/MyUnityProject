using System.Collections.Generic;
using MagicaCloth2;
using UnityEngine;

public class CharacterMagicaColliderManager : MonoBehaviour
{
	[Header("Upper")]
	public MagicaCapsuleCollider head;

	public MagicaCapsuleCollider neck;

	public MagicaCapsuleCollider spine03;

	public MagicaCapsuleCollider spine02;

	public MagicaCapsuleCollider armL;

	public MagicaCapsuleCollider armR;

	public MagicaCapsuleCollider eyes;

	[Header("Lower")]
	public MagicaCapsuleCollider root;

	public MagicaCapsuleCollider belly;

	public MagicaCapsuleCollider thighL;

	public MagicaCapsuleCollider thighR;

	[Header("Plane")]
	public MagicaPlaneCollider plane;

	[Header("List")]
	public List<MagicaCapsuleCollider> upperBones;

	public List<MagicaCapsuleCollider> bodyBones;

	public List<MagicaCapsuleCollider> skirtBones;

	public void SetList(Transform targetModel)
	{
		FindAndSetComponents(targetModel);
		SetUpper();
		SetBody();
		SetSkirt();
	}

	private void FindAndSetComponents(Transform parent)
	{
		foreach (Transform item in parent)
		{
			if (item.name == "head.x")
			{
				head = item.GetComponent<MagicaCapsuleCollider>();
			}
			if (item.name == "neck.x")
			{
				neck = item.GetComponent<MagicaCapsuleCollider>();
			}
			if (item.name == "spine_03.x")
			{
				spine03 = item.GetComponent<MagicaCapsuleCollider>();
			}
			if (item.name == "spine_02.x")
			{
				spine02 = item.GetComponent<MagicaCapsuleCollider>();
			}
			if (item.name == "arm_stretch.l")
			{
				armL = item.GetComponent<MagicaCapsuleCollider>();
			}
			if (item.name == "arm_stretch.l")
			{
				armL = item.GetComponent<MagicaCapsuleCollider>();
			}
			if (item.name == "MagicaCapsule (Eyes)")
			{
				eyes = item.GetComponent<MagicaCapsuleCollider>();
			}
			if (item.name == "MagicaCapsule (Root)")
			{
				root = item.GetComponent<MagicaCapsuleCollider>();
			}
			if (item.name == "MagicaCapsule (ThighL)")
			{
				thighL = item.GetComponent<MagicaCapsuleCollider>();
			}
			if (item.name == "MagicaCapsule (ThighR)")
			{
				thighR = item.GetComponent<MagicaCapsuleCollider>();
			}
			if (item.childCount > 0)
			{
				FindAndSetComponents(item);
			}
		}
	}

	public void SetUpper()
	{
		upperBones.Clear();
		upperBones.Add(head);
		upperBones.Add(neck);
		upperBones.Add(spine03);
		upperBones.Add(spine02);
		upperBones.Add(armL);
		upperBones.Add(armR);
		upperBones.Add(eyes);
	}

	public void SetBody()
	{
		bodyBones.Clear();
		bodyBones.Add(neck);
		bodyBones.Add(spine03);
		bodyBones.Add(armL);
		bodyBones.Add(armR);
		bodyBones.Add(spine02);
	}

	public void SetSkirt()
	{
		skirtBones.Clear();
		skirtBones.Add(root);
		skirtBones.Add(belly);
		skirtBones.Add(thighL);
		skirtBones.Add(thighR);
	}
}
