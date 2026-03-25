using Animancer;
using MagicaCloth2;
using UnityEngine;

public class ComponentScript : MonoBehaviour
{
	public CharacterAnimation _characterAnimation;

	[Space]
	public AnimancerComponent _animancerComponent;

	public CharacterEffectManager _characterEffectManager;

	public CharacterLookAtEyes _characterLookAtEyes;

	public CharacterLookAtHead _characterLookAtHead;

	public CharacterHead _characterHead;

	public CharacterFaceManager _characterFaceManager;

	public CharacterEyesManager _characterEyesManager;

	public CharacterMouthManager _characterMouthManager;

	public CharacterTitsManager _characterTitsManager;

	public CharacterConceiveManager _characterConceiveManager;

	[Space]
	public FilamentVaginaOpenObject _filamentVaginaOpenObject;

	public CharacterTypeManager _characterTypeManager;

	public CharacterPositionManager _characterPositionManager;

	public CharacterCostumeSwitch _characterCostumeSwitch;

	public CharacterHairManager _characterHairManager;

	[Space]
	public FeelerPressTitsObject _feelerPressTitsObjectL;

	public FeelerPressTitsObject _feelerPressTitsObjectR;

	[Header("Magica Cloth2")]
	public MagicaCloth _magicaHair;

	public MagicaCloth _magicaTits;

	public MagicaCloth _magicaHip;

	public MagicaCloth _magicaBelly;

	[ContextMenu("Set Up")]
	public void Setup(Transform target)
	{
		_characterAnimation = GetComponent<CharacterAnimation>();
		target.gameObject.AddComponent<CharacterAnimation>();
		_magicaTits.SerializeData.rootBones.Clear();
		_magicaHip.SerializeData.rootBones.Clear();
		_magicaBelly.SerializeData.rootBones.Clear();
		_animancerComponent.Animator = target.GetComponent<Animator>();
		AddBoneRecursive(target);
		Debug.LogError("VaginaOpen in script to FilamentVaginaOpenObject openMesh need Change. bottom root.x");
	}

	private void AddBoneRecursive(Transform target)
	{
		foreach (Transform item in target)
		{
			if (item.name == "0_Head")
			{
				_characterHead.headMesh = item.GetComponent<SkinnedMeshRenderer>();
				_characterFaceManager.headMesh = item.GetComponent<SkinnedMeshRenderer>();
				_characterMouthManager.headMesh = item.GetComponent<SkinnedMeshRenderer>();
				_characterTypeManager.headPaint3DManager = item.GetComponent<Paint3DManager>();
			}
			if (item.name == "1_Eyes")
			{
				_characterLookAtEyes.eyesMesh = item.GetComponent<SkinnedMeshRenderer>();
				_characterEyesManager.eyesMesh = item.GetComponent<SkinnedMeshRenderer>();
			}
			if (item.name == "2_Body")
			{
				_characterEffectManager.bodyMesh = item.GetComponent<SkinnedMeshRenderer>();
				_characterConceiveManager._body = item.GetComponent<SkinnedMeshRenderer>();
				_filamentVaginaOpenObject._body = item.GetComponent<SkinnedMeshRenderer>();
				_filamentVaginaOpenObject._openMesh[0] = item.GetComponent<SkinnedMeshRenderer>();
				_characterTypeManager.bodyPaint3DManager = item.GetComponent<Paint3DManager>();
			}
			if (item.name == "root.x")
			{
				_characterCostumeSwitch.effectRootPosition = item;
			}
			if (item.name == "head.x")
			{
				_characterHairManager.effectHeadPosition = item;
			}
			if (item.name == "neck.x")
			{
				_characterPositionManager.neckBone = item;
			}
			if (item.name == "cc_breast_02.l")
			{
				_magicaTits.SerializeData.rootBones.Add(item);
				_characterTitsManager.boneTitsL = item.transform;
			}
			if (item.name == "cc_breast_02.r")
			{
				_magicaTits.SerializeData.rootBones.Add(item);
				_characterTitsManager.boneTitsR = item.transform;
			}
			if (item.name == "cc_breast_out.l")
			{
				_feelerPressTitsObjectL.targetObject = item.transform;
			}
			if (item.name == "cc_breast_out.r")
			{
				_feelerPressTitsObjectR.targetObject = item.transform;
			}
			if (item.name == "cc_hip_03.l")
			{
				_magicaHip.SerializeData.rootBones.Add(item);
			}
			if (item.name == "cc_hip_03.r")
			{
				_magicaHip.SerializeData.rootBones.Add(item);
			}
			if (item.name == "cc_belly_02.x")
			{
				_magicaBelly.SerializeData.rootBones.Add(item);
				_characterConceiveManager.bellyBone = item.transform;
			}
			AddBoneRecursive(item);
		}
	}

	[ContextMenu("Set Up2")]
	public void Setup2(Transform target)
	{
		_magicaHair.SerializeData.colliderCollisionConstraint.colliderList.Clear();
		AddCollidersRecursive(target);
	}

	private void AddCollidersRecursive(Transform target)
	{
		foreach (Transform item in target)
		{
			if (item.name == "head.x")
			{
				_magicaHair.SerializeData.colliderCollisionConstraint.colliderList.Add(item.GetComponent<MagicaCapsuleCollider>());
			}
			if (item.name == "neck.x")
			{
				_magicaHair.SerializeData.colliderCollisionConstraint.colliderList.Add(item.GetComponent<MagicaCapsuleCollider>());
			}
			if (item.name == "spine_03.x")
			{
				_magicaHair.SerializeData.colliderCollisionConstraint.colliderList.Add(item.GetComponent<MagicaCapsuleCollider>());
			}
			if (item.name == "arm_stretch.l")
			{
				_magicaHair.SerializeData.colliderCollisionConstraint.colliderList.Add(item.GetComponent<MagicaCapsuleCollider>());
			}
			if (item.name == "arm_stretch.r")
			{
				_magicaHair.SerializeData.colliderCollisionConstraint.colliderList.Add(item.GetComponent<MagicaCapsuleCollider>());
			}
			AddCollidersRecursive(item);
		}
	}
}
