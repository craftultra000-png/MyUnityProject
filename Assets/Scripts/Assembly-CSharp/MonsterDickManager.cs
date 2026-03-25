using System.Collections.Generic;
using UnityEngine;

public class MonsterDickManager : MonoBehaviour
{
	public List<Transform> bone;

	[Header("Stauts")]
	public bool isErect;

	[Header("Lerp")]
	public float lerpSpeed = 5f;

	private Vector3 calcRotation;

	[Header("Data")]
	public List<Vector3> defaultRotation;

	public List<Vector3> downRotation;

	public List<Vector3> erectRotation;

	private void Start()
	{
	}

	private void LateUpdate()
	{
		for (int i = 0; i < bone.Count; i++)
		{
			Quaternion identity = Quaternion.identity;
			if (isErect)
			{
				calcRotation = erectRotation[i];
				identity = Quaternion.Lerp(Quaternion.Euler(bone[i].localEulerAngles), Quaternion.Euler(calcRotation), lerpSpeed);
			}
			else
			{
				calcRotation = downRotation[i];
				identity = Quaternion.Lerp(Quaternion.Euler(bone[i].localEulerAngles), Quaternion.Euler(calcRotation), lerpSpeed / 2f);
			}
			bone[i].localEulerAngles = identity.eulerAngles;
		}
	}

	[ContextMenu("Set Default")]
	public void SetDefault()
	{
		for (int i = 0; i < bone.Count; i++)
		{
			bone[i].localRotation = Quaternion.Euler(defaultRotation[i]);
		}
	}

	[ContextMenu("Set Down")]
	public void SetDown()
	{
		for (int i = 0; i < bone.Count; i++)
		{
			bone[i].localRotation = Quaternion.Euler(downRotation[i]);
		}
	}

	[ContextMenu("Set Erect")]
	public void SetErect()
	{
		for (int i = 0; i < bone.Count; i++)
		{
			bone[i].localRotation = Quaternion.Euler(erectRotation[i]);
		}
	}
}
