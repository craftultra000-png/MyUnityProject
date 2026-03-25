using System;
using MagicaCloth2;
using UnityEngine;

public class Paint3DMeshBakeShare : MonoBehaviour
{
	public SkinnedMeshRenderer baseMesh;

	public MeshFilter _mesh;

	public Mesh bakedMesh;

	[Header("Status")]
	public bool isGet;

	private void Start()
	{
		bakedMesh = new Mesh();
	}

	private void LateUpdate()
	{
		if (!isGet && MagicaManager.IsPlaying())
		{
			SetEvent();
			isGet = true;
		}
	}

	private void SetEvent()
	{
		MagicaManager.OnPostSimulation = (Action)Delegate.Combine(MagicaManager.OnPostSimulation, new Action(UpdateMeshAfterSimulation));
	}

	private void OnEnable()
	{
		if (isGet)
		{
			isGet = false;
		}
		else
		{
			MagicaManager.OnPostSimulation = (Action)Delegate.Remove(MagicaManager.OnPostSimulation, new Action(UpdateMeshAfterSimulation));
		}
	}

	private void OnDisable()
	{
		MagicaManager.OnPostSimulation = (Action)Delegate.Remove(MagicaManager.OnPostSimulation, new Action(UpdateMeshAfterSimulation));
	}

	private void UpdateMeshAfterSimulation()
	{
		baseMesh.BakeMesh(bakedMesh);
		_mesh.mesh = bakedMesh;
	}
}
