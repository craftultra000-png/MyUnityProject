using UnityEngine;

public class CharacterShapeKeyLink : MonoBehaviour
{
	[Header("Skinned Mesh")]
	public SkinnedMeshRenderer _bodyMesh;

	public SkinnedMeshRenderer _currentMesh;

	[Header("Link Data")]
	public bool vagina;

	public bool conceive;

	[Header("Status")]
	public bool isChange;

	[Header("Param")]
	public float vaginaHoleParam;

	public float vaginaParam;

	public float conceiveParam;

	private void LateUpdate()
	{
		if (vagina)
		{
			if (vaginaHoleParam != _bodyMesh.GetBlendShapeWeight(2))
			{
				isChange = true;
				vaginaHoleParam = _bodyMesh.GetBlendShapeWeight(2);
			}
			if (vaginaParam != _bodyMesh.GetBlendShapeWeight(3))
			{
				isChange = true;
				vaginaParam = _bodyMesh.GetBlendShapeWeight(3);
			}
			if (isChange)
			{
				_currentMesh.SetBlendShapeWeight(2, vaginaHoleParam);
				_currentMesh.SetBlendShapeWeight(3, vaginaParam);
			}
		}
		if (conceive)
		{
			if (conceiveParam != _bodyMesh.GetBlendShapeWeight(5))
			{
				isChange = true;
				conceiveParam = _bodyMesh.GetBlendShapeWeight(5);
			}
			if (isChange)
			{
				_currentMesh.SetBlendShapeWeight(5, conceiveParam);
			}
		}
	}
}
