using System.Collections.Generic;
using FIMSpace.Basics;
using UnityEngine;

public class TailAnimatorDemo_InsectMover : FBasics_RigidbodyMover
{
	[FPD_Header("References", 6f, 4f, 2)]
	public List<Transform> WheelsFront;

	public List<Transform> WheelsBack;

	protected override void UpdateMotor()
	{
		base.UpdateMotor();
		Vector3 velocity = rigbody.velocity;
		velocity.y = 0f;
		for (int i = 0; i < WheelsFront.Count; i++)
		{
			WheelsFront[i].Rotate(velocity.magnitude * 1.4f, 0f, 0f);
		}
		for (int j = 0; j < WheelsBack.Count; j++)
		{
			WheelsBack[j].Rotate(velocity.magnitude * 1.4f, 0f, 0f);
			WheelsBack[j].Rotate(rigbody.angularVelocity.sqrMagnitude * 0.6f, 0f, 0f);
		}
	}
}
