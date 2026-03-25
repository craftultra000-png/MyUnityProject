using System.Collections.Generic;
using FIMSpace.FTail;
using UnityEngine;

public class TailDemo_SegmentedTailGenerator : MonoBehaviour
{
	[Header("Tail will be generated on Start()", order = 0)]
	[Header("References", order = 1)]
	public TailAnimator2 TailWithSettings;

	public GameObject SegmentModel;

	[Header("Parameters")]
	public int SegmentsCount = 10;

	public Vector3 SegmentSeparation = Vector3.forward;

	public bool DetachForOptimization;

	[Header("Optional")]
	public bool Dynamic;

	public bool AddTailAnimToCuttedSegment;

	public bool DrawGizmos = true;

	public bool Cuttable;

	private Vector3 referenceOffset;

	private bool dontReload = true;

	private void Start()
	{
		if (SegmentModel == null)
		{
			Debug.LogError("No Tail Segment Prefab");
			return;
		}
		GetReferenceParameters();
		List<Transform> list = new List<Transform>();
		Transform parent = base.transform;
		for (int i = 0; i < SegmentsCount; i++)
		{
			Vector3 position = base.transform.position + base.transform.TransformVector(referenceOffset * (0.1f + (float)i));
			GameObject gameObject = Object.Instantiate(SegmentModel);
			gameObject.transform.rotation = base.transform.rotation;
			gameObject.transform.localScale = base.transform.lossyScale;
			gameObject.transform.SetParent(parent, worldPositionStays: true);
			gameObject.transform.position = position;
			parent = gameObject.transform;
			list.Add(gameObject.transform);
			if (Cuttable)
			{
				TailDemo_TailCutId tailDemo_TailCutId = gameObject.AddComponent<TailDemo_TailCutId>();
				tailDemo_TailCutId.index = i;
				tailDemo_TailCutId.owner = this;
				gameObject.AddComponent<BoxCollider>().isTrigger = true;
			}
		}
		if ((bool)TailWithSettings)
		{
			TailWithSettings.DetachChildren = DetachForOptimization;
			TailWithSettings.User_SetTailTransforms(list);
			TailWithSettings.enabled = true;
		}
		else
		{
			TailWithSettings = base.gameObject.AddComponent<TailAnimator2>();
			TailWithSettings.DetachChildren = DetachForOptimization;
			TailWithSettings.User_SetTailTransforms(list);
			TailWithSettings.enabled = true;
		}
	}

	private void OnDrawGizmosSelected()
	{
		if (!(SegmentModel == null) && DrawGizmos)
		{
			GetReferenceParameters();
			Vector3 vector = new Vector3(SegmentSeparation.magnitude, SegmentSeparation.magnitude, 0f);
			MeshRenderer componentInChildren = SegmentModel.GetComponentInChildren<MeshRenderer>();
			if ((bool)componentInChildren)
			{
				vector = componentInChildren.bounds.extents;
			}
			Gizmos.matrix = base.transform.localToWorldMatrix;
			for (int i = 0; i < SegmentsCount; i++)
			{
				Vector3 center = referenceOffset * (0.5f + (float)i);
				Gizmos.DrawWireCube(center, vector + referenceOffset);
				Gizmos.DrawSphere(center, vector.sqrMagnitude * 0.1f);
			}
			Gizmos.matrix = Matrix4x4.identity;
		}
	}

	private void GetReferenceParameters()
	{
		referenceOffset = SegmentSeparation;
		MeshRenderer componentInChildren = SegmentModel.GetComponentInChildren<MeshRenderer>();
		if ((bool)componentInChildren)
		{
			referenceOffset = SegmentSeparation * componentInChildren.bounds.extents.z * 2f;
		}
	}

	public void ExmapleCutAt(int index)
	{
		TailAnimator2.TailSegment tailSegment = TailWithSettings.TailSegments[index];
		if (!tailSegment.transform)
		{
			return;
		}
		tailSegment.transform.parent = null;
		if (tailSegment.transform.childCount > 0 && AddTailAnimToCuttedSegment)
		{
			GameObject gameObject = new GameObject(base.name + "-Cutted");
			gameObject.transform.position = Vector3.Lerp(tailSegment.ParentBone.ProceduralPosition, tailSegment.ProceduralPosition, 0.5f);
			gameObject.transform.rotation = tailSegment.ParentBone.LastFinalRotation;
			tailSegment.transform.SetParent(gameObject.transform);
			gameObject.gameObject.AddComponent<SphereCollider>().radius = 0.2f;
			Rigidbody rigidbody = gameObject.AddComponent<Rigidbody>();
			rigidbody.useGravity = false;
			rigidbody.freezeRotation = true;
			rigidbody.AddForce(new Vector3(4f, 1f, 0f), ForceMode.Impulse);
			TailAnimator2 tailAnimator = tailSegment.transform.gameObject.AddComponent<TailAnimator2>();
			tailAnimator.Axis2D = TailWithSettings.Axis2D;
			tailAnimator.Slithery = TailWithSettings.Slithery;
			tailAnimator.Curling = TailWithSettings.Curling;
			tailAnimator.ReactionSpeed = TailWithSettings.ReactionSpeed;
			List<Transform> list = new List<Transform>();
			TailAnimator2.TailSegment tailSegment2 = tailSegment;
			while (tailSegment2.ChildBone != null)
			{
				if ((bool)tailSegment2.transform)
				{
					list.Add(tailSegment2.transform);
				}
				tailSegment2 = tailSegment2.ChildBone;
			}
			tailAnimator.User_SetTailTransforms(list);
			for (int i = 0; i < tailAnimator.TailSegments.Count; i++)
			{
				tailAnimator.TailSegments[i].ParamsFromAll(TailWithSettings.TailSegments[tailSegment.Index + i]);
				tailAnimator.TailSegments[i].User_ReassignTransform(list[i]);
			}
		}
		TailWithSettings.User_CutEndSegmentsTo(index);
		Transform child = tailSegment.transform;
		while (child != null)
		{
			TailDemo_TailCutId component = child.gameObject.GetComponent<TailDemo_TailCutId>();
			if (!component)
			{
				break;
			}
			Object.Destroy(component);
			if (child.childCount <= 0)
			{
				break;
			}
			child = child.GetChild(0);
		}
		SegmentsCount = TailWithSettings.TailSegments.Count;
		dontReload = true;
	}

	public void OnValidate()
	{
		if (SegmentsCount < 2)
		{
			SegmentsCount = 2;
		}
		if (!Application.isPlaying || !Dynamic || !TailWithSettings || !TailWithSettings.IsInitialized)
		{
			return;
		}
		if (dontReload)
		{
			dontReload = false;
		}
		else
		{
			if (SegmentsCount == TailWithSettings.TailSegments.Count)
			{
				return;
			}
			if (SegmentsCount < TailWithSettings.TailSegments.Count)
			{
				if ((bool)TailWithSettings.TailSegments[SegmentsCount].transform)
				{
					Object.Destroy(TailWithSettings.TailSegments[SegmentsCount].transform.gameObject);
				}
				TailWithSettings.User_CutEndSegmentsTo(SegmentsCount);
				return;
			}
			TailAnimator2.TailSegment tailSegment = TailWithSettings.TailSegments[TailWithSettings.TailSegments.Count - 1];
			Transform transform = TailWithSettings.TailSegments[TailWithSettings.TailSegments.Count - 1].transform;
			int num = SegmentsCount - TailWithSettings.TailSegments.Count;
			for (int i = 0; i < num; i++)
			{
				Vector3 position = transform.position + tailSegment.ParentToFrontOffset();
				GameObject gameObject = Object.Instantiate(SegmentModel);
				gameObject.transform.rotation = tailSegment.transform.rotation;
				gameObject.transform.localScale = tailSegment.transform.lossyScale;
				gameObject.transform.SetParent(transform, worldPositionStays: true);
				gameObject.transform.position = position;
				if (Cuttable)
				{
					TailDemo_TailCutId tailDemo_TailCutId = gameObject.AddComponent<TailDemo_TailCutId>();
					tailDemo_TailCutId.index = tailSegment.Index + i;
					tailDemo_TailCutId.owner = this;
					gameObject.AddComponent<BoxCollider>();
				}
				TailWithSettings.User_AddTailTransform(gameObject.transform);
				transform = gameObject.transform;
			}
		}
	}
}
