using System.Collections.Generic;
using FIMSpace.FTail;
using UnityEngine;

public class TailDemo_LineRenderer : MonoBehaviour
{
	[Header("Tail will be generated on Start()", order = 0)]
	[Header("References", order = 1)]
	public LineRenderer Line;

	public TailAnimator2 TailWithSettings;

	public Vector3 GeneratePosition = Vector3.down;

	[Header("Parameters")]
	public int SegmentsCount = 10;

	public bool DetachForOptimization;

	[Header("Optional")]
	public bool DrawGizmos = true;

	private void Reset()
	{
		Line = GetComponent<LineRenderer>();
		if (!Line)
		{
			Line = base.gameObject.AddComponent<LineRenderer>();
		}
	}

	private void Update()
	{
		Line.positionCount = TailWithSettings.TailSegments.Count;
		for (int i = 0; i < TailWithSettings.TailSegments.Count; i++)
		{
			Line.SetPosition(i, TailWithSettings.TailSegments[i].ProceduralPosition);
		}
	}

	private void Start()
	{
		List<Transform> list = new List<Transform>();
		Transform parent = base.transform;
		if (TailWithSettings == null)
		{
			TailWithSettings = GetComponent<TailAnimator2>();
		}
		for (int i = 0; i < SegmentsCount; i++)
		{
			Vector3 position = base.transform.position + base.transform.TransformVector(GeneratePosition * i);
			GameObject gameObject = new GameObject("Tail-LineRednerer-Segment[" + i + "]");
			gameObject.transform.rotation = base.transform.rotation;
			gameObject.transform.localScale = base.transform.lossyScale;
			gameObject.transform.SetParent(parent, worldPositionStays: true);
			gameObject.transform.position = position;
			parent = gameObject.transform;
			list.Add(gameObject.transform);
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
		if ((bool)Line)
		{
			Line.positionCount = TailWithSettings.TailSegments.Count;
		}
	}

	private void OnValidate()
	{
		if (SegmentsCount < 2)
		{
			SegmentsCount = 2;
		}
	}
}
