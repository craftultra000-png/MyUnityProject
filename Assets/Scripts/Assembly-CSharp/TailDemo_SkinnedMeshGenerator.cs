using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FIMSpace.FTail;
using UnityEngine;

public class TailDemo_SkinnedMeshGenerator : MonoBehaviour
{
	private class VertGenHelper
	{
		public int index;

		public int l;

		public int c;

		public Vector3 p;

		public Vector3 n;

		public List<int> triangles;

		public List<Vector3> trianglesPos;
	}

	[FPD_Header("Generate Tail Settings", 0f, 4f, 2)]
	public int circlePoints = 16;

	public int LengthSegments = 18;

	public float ForwardLength = 5f;

	public float Fatness = 0.25f;

	[FPD_FixedCurveWindow(0f, 0f, 1f, 1f, 0f, 1f, 1f, 1f)]
	public AnimationCurve LengthScale = AnimationCurve.EaseInOut(0.5f, 1f, 1f, 0.1f);

	public bool RandomizeAtStart;

	public Material mat;

	public bool DrawGizmos = true;

	[FPD_Header("Skinning Settings", 6f, 4f, 2)]
	public int BonesCount = 10;

	[FPD_Header("Tail Animtor Settings", 6f, 4f, 2)]
	public bool AddTailAnimator = true;

	public bool DetachForOptimization = true;

	[Header("If not adding tail animator")]
	public TailAnimator2 TargetTailAnimator;

	public bool SetAsParent = true;

	private Vector3[,] toDraw;

	private List<VertGenHelper> toDrawHelpers;

	private void Start()
	{
		if (RandomizeAtStart)
		{
			Keyframe[] array = new Keyframe[UnityEngine.Random.Range(9, 24)];
			float num = 1f / (float)(array.Length - 1);
			for (int i = 0; i < array.Length; i++)
			{
				float minInclusive = Mathf.Lerp(1.5f, 0.8f, num * (float)i);
				float maxInclusive = Mathf.Lerp(0.4f, 0.1f, num * (float)i);
				float value = UnityEngine.Random.Range(minInclusive, maxInclusive);
				array[i] = new Keyframe(num * (float)i, value);
			}
			LengthScale.keys = array;
			for (int j = 0; j < array.Length; j++)
			{
				LengthScale.SmoothTangents(j, UnityEngine.Random.Range(0.35f, 0.6f));
			}
			BonesCount += UnityEngine.Random.Range(-3, 8);
			Fatness *= UnityEngine.Random.Range(0.85f, 1.25f);
			ForwardLength *= UnityEngine.Random.Range(0.925f, 1.125f);
		}
		SkinMesh();
	}

	private void OnValidate()
	{
		if (BonesCount < 2)
		{
			BonesCount = 2;
		}
		if (Fatness < 0.01f)
		{
			Fatness = 0.01f;
		}
		if (ForwardLength < 0.01f)
		{
			ForwardLength = 0.01f;
		}
		if (LengthSegments < 2)
		{
			LengthSegments = 2;
		}
		if (circlePoints < 3)
		{
			circlePoints = 3;
		}
		toDraw = GetTailPoints();
	}

	private Vector3[,] GetTailPoints()
	{
		Vector3[,] array = new Vector3[LengthSegments, circlePoints];
		float num = 360f / (float)circlePoints;
		for (int i = 0; i < LengthSegments; i++)
		{
			float num2 = Fatness * LengthScale.Evaluate((float)i / (float)(LengthSegments - 1));
			for (int j = 0; j < circlePoints; j++)
			{
				Vector3 vector = Vector3.forward * i * (ForwardLength / (float)LengthSegments);
				vector.y += Mathf.Sin(num * (float)j * (MathF.PI / 180f)) * num2;
				vector.x += Mathf.Cos(num * (float)j * (MathF.PI / 180f)) * num2;
				array[i, j] = vector;
			}
		}
		return array;
	}

	private List<VertGenHelper> GetVertexHelpers()
	{
		List<VertGenHelper> list = new List<VertGenHelper>();
		Vector3[,] tailPoints = GetTailPoints();
		int num = 0;
		for (int i = 0; i < tailPoints.GetLength(0); i++)
		{
			for (int j = 0; j < tailPoints.GetLength(1); j++)
			{
				VertGenHelper vertGenHelper = new VertGenHelper();
				vertGenHelper.index = num;
				vertGenHelper.l = i;
				vertGenHelper.c = j;
				vertGenHelper.p = tailPoints[i, j];
				vertGenHelper.n = vertGenHelper.p.normalized;
				num++;
				vertGenHelper.triangles = new List<int>();
				vertGenHelper.trianglesPos = new List<Vector3>();
				list.Add(vertGenHelper);
			}
		}
		for (int k = 0; k < list.Count; k++)
		{
			VertGenHelper vertGenHelper2 = list[k];
			int helper = GetHelper(vertGenHelper2.l, vertGenHelper2.c + 1);
			int helper2;
			if (helper < list.Count)
			{
				helper2 = GetHelper(vertGenHelper2.l + 1, vertGenHelper2.c);
				if (helper2 < list.Count)
				{
					vertGenHelper2.triangles.Add(GetHelper(vertGenHelper2.l, vertGenHelper2.c));
					vertGenHelper2.triangles.Add(helper);
					vertGenHelper2.triangles.Add(helper2);
					vertGenHelper2.trianglesPos.Add(list[vertGenHelper2.triangles[0]].p);
					vertGenHelper2.trianglesPos.Add(list[vertGenHelper2.triangles[1]].p);
					vertGenHelper2.trianglesPos.Add(list[vertGenHelper2.triangles[2]].p);
				}
			}
			helper = GetHelper(vertGenHelper2.l + 1, vertGenHelper2.c);
			if (helper >= list.Count)
			{
				continue;
			}
			helper2 = GetHelper(vertGenHelper2.l, vertGenHelper2.c + 1);
			if (helper2 < list.Count)
			{
				int helper3 = GetHelper(vertGenHelper2.l + 1, vertGenHelper2.c + 1);
				if (helper3 < list.Count)
				{
					vertGenHelper2.triangles.Add(helper);
					vertGenHelper2.triangles.Add(helper2);
					vertGenHelper2.triangles.Add(helper3);
					vertGenHelper2.trianglesPos.Add(list[vertGenHelper2.triangles[0]].p);
					vertGenHelper2.trianglesPos.Add(list[vertGenHelper2.triangles[1]].p);
					vertGenHelper2.trianglesPos.Add(list[vertGenHelper2.triangles[2]].p);
				}
			}
		}
		return list;
	}

	public int GetHelper(int length, int circle)
	{
		if (circle >= circlePoints)
		{
			circle -= circlePoints;
		}
		return length * circlePoints + circle;
	}

	public IEnumerator GenerateFrameByFrame()
	{
		yield break;
	}

	public Mesh GenerateMesh(bool drawMesh)
	{
		Mesh mesh = new Mesh();
		List<VertGenHelper> vertexHelpers = GetVertexHelpers();
		List<Vector3> list = new List<Vector3>();
		for (int i = 0; i < vertexHelpers.Count; i++)
		{
			list.Add(vertexHelpers[i].p);
		}
		mesh.SetVertices(list);
		List<int> list2 = new List<int>();
		for (int j = 0; j < vertexHelpers.Count; j++)
		{
			for (int k = 0; k < vertexHelpers[j].triangles.Count; k++)
			{
				list2.Add(vertexHelpers[j].triangles[k]);
			}
		}
		mesh.SetTriangles(list2, 0);
		List<Vector3> list3 = new List<Vector3>();
		List<Vector4> list4 = new List<Vector4>();
		for (int l = 0; l < vertexHelpers.Count; l++)
		{
			VertGenHelper vertGenHelper = vertexHelpers[l];
			vertGenHelper.n = new Vector3(vertGenHelper.p.x, vertGenHelper.p.y, 0f).normalized;
			list3.Add(vertGenHelper.n);
			Vector4 item = Vector3.Cross(Vector3.forward, vertGenHelper.n);
			item.w = 1f;
			list4.Add(item);
		}
		List<Vector2> list5 = new List<Vector2>();
		for (int m = 0; m < vertexHelpers.Count; m++)
		{
			VertGenHelper vertGenHelper2 = vertexHelpers[m];
			Vector2 vector = new Vector2
			{
				x = (float)vertGenHelper2.l / (float)LengthSegments
			};
			float num = (float)vertGenHelper2.c + (float)circlePoints * 0.25f;
			if (num >= (float)circlePoints)
			{
				num -= (float)circlePoints;
			}
			vector.y = num / (float)circlePoints;
			vector = new Vector2(vector.y, vector.x);
			list5.Add(vector);
		}
		mesh.SetNormals(list3);
		mesh.RecalculateNormals();
		mesh.SetTangents(list4);
		mesh.uv = list5.ToArray();
		mesh.RecalculateBounds();
		if (drawMesh)
		{
			MeshFilter meshFilter = base.gameObject.AddComponent<MeshFilter>();
			MeshRenderer meshRenderer = base.gameObject.AddComponent<MeshRenderer>();
			meshFilter.mesh = mesh;
			if ((bool)mat)
			{
				meshRenderer.material = mat;
			}
		}
		return mesh;
	}

	private void SkinMesh()
	{
		Mesh baseMesh = GenerateMesh(drawMesh: false);
		Vector3[] array = new Vector3[BonesCount];
		Quaternion[] array2 = new Quaternion[BonesCount];
		for (int i = 0; i < BonesCount; i++)
		{
			array[i] = GetBonePos(i);
			array2[i] = Quaternion.identity;
		}
		FTail_SkinningVertexData[] vertData = FTail_Skinning.CalculateVertexWeightingData(baseMesh, array, array2, Vector3.zero, 2, 0.7f, 0.3f);
		SkinnedMeshRenderer skinnedMeshRenderer = FTail_Skinning.SkinMesh(baseMesh, array, array2, vertData);
		skinnedMeshRenderer.transform.position = base.transform.position;
		skinnedMeshRenderer.transform.rotation = base.transform.rotation;
		skinnedMeshRenderer.transform.localScale = base.transform.lossyScale;
		skinnedMeshRenderer.material = mat;
		AddTailAnimatorToSkinnedMesh(skinnedMeshRenderer);
	}

	private Vector3 GetBonePos(int index)
	{
		float num = ForwardLength / (float)BonesCount;
		_ = Vector3.forward * num * index;
		return Vector3.forward * num * index;
	}

	private void AddTailAnimatorToSkinnedMesh(SkinnedMeshRenderer skin)
	{
		if ((bool)TargetTailAnimator)
		{
			if (SetAsParent)
			{
				skin.transform.SetParent(TargetTailAnimator.transform, worldPositionStays: true);
				skin.transform.localPosition = Vector3.zero;
				skin.transform.localRotation = Quaternion.identity;
				skin.transform.localScale = Vector3.one;
			}
			TargetTailAnimator.DetachChildren = DetachForOptimization;
			TargetTailAnimator.User_SetTailTransforms(skin.bones.ToList());
			TargetTailAnimator.enabled = true;
		}
		else if (AddTailAnimator)
		{
			TailAnimator2 tailAnimator = skin.gameObject.AddComponent<TailAnimator2>();
			tailAnimator.StartBone = skin.bones[0];
			tailAnimator.EndBone = skin.bones[skin.bones.Length - 1];
			tailAnimator.DetachChildren = DetachForOptimization;
		}
	}
}
