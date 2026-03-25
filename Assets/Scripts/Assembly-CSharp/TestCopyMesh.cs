using System;
using MagicaCloth2;
using UnityEngine;

public class TestCopyMesh : MonoBehaviour
{
	public bool isGet;

	public MeshFilter _mesh;

	public SkinnedMeshRenderer targetMesh;

	private Mesh bakedMesh;

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

	private void EjectEvent()
	{
		MagicaManager.OnPostSimulation = (Action)Delegate.Remove(MagicaManager.OnPostSimulation, new Action(UpdateMeshAfterSimulation));
	}

	private void UpdateMeshAfterSimulation()
	{
		targetMesh.BakeMesh(bakedMesh);
		_mesh.mesh = bakedMesh;
	}
}
