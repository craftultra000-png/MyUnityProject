using System.Collections.Generic;
using UnityEngine;

public class HandIKObject : MonoBehaviour
{
	public CharacterAnimancerManager _characterAnimancerManager;

	[Header("Debug")]
	public bool gizmoz = true;

	public bool self = true;

	[Header("Animator")]
	public Animator _animator;

	public int handAnim;

	[Header("Hand")]
	public Transform hand;

	public List<Transform> fingerBones;

	[Header("Parent")]
	public Transform targetParent;

	[Header("Magica Initial Check")]
	public bool isBuildComplete;

	public bool isInitial;

	[Header("Hand Data")]
	public Vector3 defaultPosition;

	public Quaternion defaultRotation;

	[Header("Inspector Setting")]
	public Transform handStocker;

	public Vector3 positon;

	public Vector3 rotation;

	private void Awake()
	{
		isInitial = true;
		if (targetParent != null)
		{
			base.transform.parent = targetParent;
		}
		base.transform.localPosition = defaultPosition;
		base.transform.localRotation = defaultRotation;
		base.transform.localScale = new Vector3(1f, 1f, 1f);
	}

	private void OnDestroy()
	{
	}

	private void HandleBuildComplete(bool chk)
	{
		if (chk)
		{
			isInitial = true;
			if (targetParent != null)
			{
				base.transform.parent = targetParent;
			}
			base.transform.localPosition = defaultPosition;
			base.transform.localRotation = defaultRotation;
			base.transform.localScale = new Vector3(1f, 1f, 1f);
			base.gameObject.SetActive(value: false);
		}
	}

	[ContextMenu("1 Set Parent EscapeTitsBone")]
	private void SetParent()
	{
		if (handStocker != null)
		{
			defaultPosition = base.transform.localPosition;
			defaultRotation = base.transform.localRotation;
			base.transform.parent = handStocker;
			base.transform.localScale = new Vector3(1f, 1f, 1f);
		}
		if (fingerBones == null)
		{
			SortFingerTransform();
		}
	}

	[ContextMenu("0 Return Tits Bone")]
	private void ReturnTitsBone()
	{
		if (handStocker != null)
		{
			base.transform.parent = targetParent;
		}
	}

	public void SetHandAnim(int value)
	{
		if (_animator != null)
		{
			Debug.Log("Set Hand Anim :" + value);
			handAnim = value;
			_animator.SetInteger("HandAnim", handAnim);
		}
	}

	public void SortFingerTransform()
	{
		fingerBones = new List<Transform>();
		Transform[] componentsInChildren = hand.GetComponentsInChildren<Transform>();
		foreach (Transform item in componentsInChildren)
		{
			fingerBones.Add(item);
		}
		fingerBones.Sort(delegate(Transform a, Transform b)
		{
			if (a.IsChildOf(hand) && !b.IsChildOf(hand))
			{
				return -1;
			}
			return (!a.IsChildOf(hand) && b.IsChildOf(hand)) ? 1 : a.name.CompareTo(b.name);
		});
	}
}
