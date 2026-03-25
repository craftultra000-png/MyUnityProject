using System.Collections.Generic;
using UnityEngine;

public class CharacterMagicaConceiveCloth : MonoBehaviour
{
	public CharacterConceiveManager _characterConceiveManager;

	[Header("Set Data")]
	public Transform rootBone;

	public Transform conceiveBone;

	public Transform bellyBone;

	[Space]
	public Transform clothBone;

	[Header("Status")]
	public bool isConceive;

	public float conceiveParam;

	public float conceiveParamCalc;

	[Header("Transform")]
	public List<Transform> boneCloth;

	public List<Transform> boneBelly;

	[Header("Position")]
	public List<Vector3> currentPosition;

	public List<Vector3> targetPosition;

	[Header("Belly Calc")]
	public Vector3 bellyDefaultPosition;

	public Vector3 bellyCalcPosition;

	private List<Vector3> boneClothBaseLocalPosition;

	private Vector3 curretScale;

	private Vector3 bellyScale;

	private void Start()
	{
		bellyDefaultPosition = rootBone.InverseTransformPoint(bellyBone.position);
		boneClothBaseLocalPosition = new List<Vector3>();
		foreach (Transform item in boneCloth)
		{
			boneClothBaseLocalPosition.Add(item.localPosition);
		}
		curretScale = bellyBone.localScale;
		clothBone.SetParent(bellyBone);
		clothBone.localPosition = Vector3.zero;
		clothBone.localRotation = Quaternion.identity;
	}

	private void LateUpdate()
	{
		conceiveParam = _characterConceiveManager.conceiveParamCurrent;
		conceiveParamCalc = conceiveParam / 100f;
		if (isConceive)
		{
			bellyScale = conceiveBone.localScale;
			clothBone.localScale = new Vector3(curretScale.x / bellyScale.x, curretScale.y / bellyScale.y, curretScale.z / bellyScale.z);
			Vector3 vector = rootBone.InverseTransformPoint(bellyBone.position);
			bellyCalcPosition = vector - bellyDefaultPosition;
			for (int i = 0; i < boneCloth.Count; i++)
			{
				Vector3 position = boneBelly[i].position;
				Vector3 vector2 = Vector3.Lerp(Vector3.zero, targetPosition[i], conceiveParamCalc);
				Vector3 vector3 = boneBelly[i].TransformVector(vector2);
				boneCloth[i].position = position - vector3;
			}
		}
		if (conceiveParam == 0f)
		{
			isConceive = false;
		}
		else
		{
			isConceive = true;
		}
	}
}
