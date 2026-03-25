using System.Collections.Generic;
using MagicaCloth2;
using UnityEngine;

public class CharacterConceiveManager : MonoBehaviour
{
	[Header("Target")]
	public SkinnedMeshRenderer _body;

	public Transform bellyBone;

	public Transform bellyMagica;

	[Header("Status")]
	public bool isChange;

	public float conceiveParamCurrent;

	public float conceiveParamTarget;

	public float conceiveParamCalc;

	public float conceiveSpeed = 200f;

	[Header("Magica Collider")]
	public MagicaCapsuleCollider bellyMagicaCollider;

	public float radiusCurrent;

	public float radiusMin = 0.017f;

	public float radiusMax = 0.02f;

	public AnimationCurve radiusCurve;

	public float lenghtCurrent;

	public float lenghtMin = 0.125f;

	public float lenghtMax = 0.3f;

	public AnimationCurve lenghtCurve;

	[Header("Belly Bone Scale")]
	public Vector3 bellyScale = new Vector3(1f, 1f, 1f);

	public Vector3 bellyScaleMin = new Vector3(1f, 1f, 1f);

	public Vector3 bellyScaleMax = new Vector3(2f, 5f, 1.5f);

	[Header("Collider")]
	public Transform conceiveColliderObject2;

	public CapsuleCollider conceiveCollider2;

	public Transform conceiveColliderObject1;

	public CapsuleCollider conceiveCollider1;

	[Space]
	public AnimationCurve positionYCurve2;

	public AnimationCurve positionZCurve2;

	public AnimationCurve rotationXCurve2;

	public AnimationCurve radiusCurve2;

	public AnimationCurve heightCurve2;

	[Space]
	public AnimationCurve positionYCurve1;

	public AnimationCurve positionZCurve1;

	public AnimationCurve rotationXCurve1;

	public AnimationCurve radiusCurve1;

	public AnimationCurve heightCurve1;

	[Header("Child")]
	public int vaginaChildCount;

	public int analChildCount;

	private Dictionary<Transform, (Vector3 localPos, Vector3 parentScale)> childData = new Dictionary<Transform, (Vector3, Vector3)>();

	private void Start()
	{
		conceiveParamCurrent = 0f;
		SetConceiveCollider();
	}

	private void LateUpdate()
	{
		if (conceiveParamCurrent < conceiveParamTarget)
		{
			isChange = true;
			conceiveParamCurrent += Time.deltaTime * conceiveSpeed;
			if (conceiveParamCurrent > conceiveParamTarget)
			{
				conceiveParamCurrent = conceiveParamTarget;
			}
		}
		else if (conceiveParamCurrent > conceiveParamTarget)
		{
			isChange = true;
			conceiveParamCurrent -= Time.deltaTime * conceiveSpeed;
			if (conceiveParamCurrent < conceiveParamTarget)
			{
				conceiveParamCurrent = conceiveParamTarget;
			}
		}
		if (isChange)
		{
			isChange = false;
			SetConceiveCollider();
			_body.SetBlendShapeWeight(5, conceiveParamCurrent);
			if (_body.GetBlendShapeWeight(5) < 0f)
			{
				_body.SetBlendShapeWeight(5, 0f);
			}
			else if (_body.GetBlendShapeWeight(5) > 100f)
			{
				_body.SetBlendShapeWeight(5, 100f);
			}
			GetChildData();
			float num = conceiveParamCurrent / 100f;
			bellyScale = Vector3.Lerp(bellyScaleMin, bellyScaleMax, num);
			bellyBone.localScale = bellyScale;
			Vector3 localScale = new Vector3(1f / bellyScale.x, 1f / bellyScale.y, 1f / bellyScale.z);
			bellyMagica.localScale = localScale;
			radiusCurrent = radiusCurve.Evaluate(num);
			lenghtCurrent = lenghtCurve.Evaluate(num);
			bellyMagicaCollider.SetSize(radiusCurrent, radiusCurrent, lenghtCurrent);
			SetChildData();
		}
	}

	public void SetConceive(float value)
	{
		if (value <= 0f && vaginaChildCount + analChildCount <= 20)
		{
			conceiveParamTarget += value * 100f;
			if (conceiveParamTarget < 0f)
			{
				conceiveParamTarget = 0f;
			}
		}
		else if (value >= 0f)
		{
			conceiveParamTarget += value * 100f;
			if (conceiveParamTarget > 100f)
			{
				conceiveParamTarget = 100f;
			}
		}
	}

	public void SetConceiveCollider()
	{
		conceiveParamCalc = conceiveParamCurrent / 100f;
		float y = positionYCurve2.Evaluate(conceiveParamCalc);
		float z = positionZCurve2.Evaluate(conceiveParamCalc);
		float x = rotationXCurve2.Evaluate(conceiveParamCalc);
		float radius = radiusCurve2.Evaluate(conceiveParamCalc);
		float height = heightCurve2.Evaluate(conceiveParamCalc);
		Vector3 localPosition = conceiveColliderObject2.localPosition;
		localPosition.y = y;
		localPosition.z = z;
		conceiveColliderObject2.localPosition = localPosition;
		Quaternion localRotation = conceiveColliderObject2.localRotation;
		localRotation = Quaternion.Euler(x, localRotation.eulerAngles.y, localRotation.eulerAngles.z);
		conceiveColliderObject2.localRotation = localRotation;
		conceiveCollider2.radius = radius;
		conceiveCollider2.height = height;
		y = positionYCurve1.Evaluate(conceiveParamCalc);
		z = positionZCurve1.Evaluate(conceiveParamCalc);
		float x2 = rotationXCurve1.Evaluate(conceiveParamCalc);
		radius = radiusCurve1.Evaluate(conceiveParamCalc);
		height = heightCurve1.Evaluate(conceiveParamCalc);
		localPosition = conceiveColliderObject1.localPosition;
		localPosition.y = y;
		localPosition.z = z;
		conceiveColliderObject1.localPosition = localPosition;
		localRotation = conceiveColliderObject1.localRotation;
		localRotation = Quaternion.Euler(x2, localRotation.eulerAngles.y, localRotation.eulerAngles.z);
		conceiveColliderObject1.localRotation = localRotation;
		conceiveCollider1.radius = radius;
		conceiveCollider1.height = height;
	}

	public void GetChildData()
	{
		childData.Clear();
		foreach (Transform item in conceiveColliderObject2)
		{
			childData[item] = (item.localPosition, bellyBone.localScale);
		}
		foreach (Transform item2 in conceiveColliderObject1)
		{
			childData[item2] = (item2.localPosition, bellyBone.localScale);
		}
	}

	public void SetChildData()
	{
		foreach (KeyValuePair<Transform, (Vector3, Vector3)> childDatum in childData)
		{
			Transform key = childDatum.Key;
			Vector3 item = childDatum.Value.Item1;
			Vector3 item2 = childDatum.Value.Item2;
			Vector3 localPosition = new Vector3(item2.x * item.x / bellyScale.x, item2.y * item.y / bellyScale.y, item2.z * item.z / bellyScale.z);
			key.localPosition = localPosition;
		}
	}
}
