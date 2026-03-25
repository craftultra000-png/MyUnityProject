using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class ComponentBodyConstraint : MonoBehaviour
{
	public Transform _body;

	[Header("Elbow")]
	public string elbowBoneL = "cc_arm_sub.l";

	public string elbowBoneR = "cc_arm_sub.r";

	public string elbowTargetL = "forearm_stretch.l";

	public string elbowTargetR = "forearm_stretch.r";

	[Space]
	public float elbowWeight = 0.8f;

	public float elbowParam = 1f;

	[Header("Knee")]
	public string kneeBoneL = "cc_thigh_sub.l";

	public string kneeBoneR = "cc_thigh_sub.r";

	public string kneeTargetL = "leg_stretch.l";

	public string kneeTargetR = "leg_stretch.r";

	[Space]
	public float kneeWeight = 0.5f;

	public float kneeParam = -1f;

	[Header("Attach")]
	public RotationConstraint rotConstraint;

	[ContextMenu("Set Up")]
	public void SetupManual()
	{
		Setup(_body);
	}

	public void Setup(Transform target)
	{
		if (target != null)
		{
			_body = target;
		}
		SetConstraint(elbowBoneL, elbowTargetL, elbowWeight, elbowParam);
		rotConstraint.rotationAxis = Axis.Y;
		rotConstraint.locked = true;
		rotConstraint.constraintActive = true;
		SetConstraint(elbowBoneR, elbowTargetR, elbowWeight, elbowParam);
		rotConstraint.rotationAxis = Axis.Y;
		rotConstraint.locked = true;
		rotConstraint.constraintActive = true;
		SetConstraint(kneeBoneL, kneeTargetL, kneeWeight, kneeParam);
		rotConstraint.rotationAxis = Axis.X;
		rotConstraint.locked = true;
		rotConstraint.constraintActive = true;
		SetConstraint(kneeBoneR, kneeTargetR, kneeWeight, kneeParam);
		rotConstraint.rotationAxis = Axis.X;
		rotConstraint.locked = true;
		rotConstraint.constraintActive = true;
		Debug.LogWarning("End Constraint Setup!");
	}

	public void SetConstraint(string name, string target, float weight, float param)
	{
		Transform transform = FindChildRecursive(_body.transform, name);
		rotConstraint = transform.gameObject.AddComponent<RotationConstraint>();
		Transform sourceTransform = FindChildRecursive(_body.transform, target);
		rotConstraint.weight = weight;
		ConstraintSource item = new ConstraintSource
		{
			sourceTransform = sourceTransform,
			weight = param
		};
		List<ConstraintSource> list = new List<ConstraintSource>();
		list.Add(item);
		rotConstraint.SetSources(list);
	}

	private Transform FindChildRecursive(Transform parent, string name)
	{
		foreach (Transform item in parent)
		{
			if (item.name == name)
			{
				return item;
			}
			Transform transform2 = FindChildRecursive(item, name);
			if (transform2 != null)
			{
				return transform2;
			}
		}
		return null;
	}
}
