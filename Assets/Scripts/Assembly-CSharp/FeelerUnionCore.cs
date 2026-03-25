using System.Collections.Generic;
using UnityEngine;

public class FeelerUnionCore : MonoBehaviour
{
	[Header("Script")]
	public FeelerAdjustPosition unionLeft;

	public FeelerAdjustPosition unionRight;

	public FeelerAdjustPosition unionNeck;

	public FeelerAdjustPosition unionHip;

	public FeelerAdjustPosition unionTitsL;

	public FeelerAdjustPosition unionTitsR;

	public FeelerAdjustPosition unionHead;

	public FeelerAdjustPosition unionBody;

	[Header("Data")]
	public List<Vector3> wartBed;

	public List<Vector3> eat;

	public Vector3 wallHip;

	public List<Vector3> pillarBind;

	public Vector3 fuck;

	public void SetEat(bool value)
	{
		unionLeft.isOtherAdjust = value;
		unionRight.isOtherAdjust = value;
		if (value)
		{
			unionLeft.otherAdjustPositon = eat[0];
			unionRight.otherAdjustPositon = eat[1];
		}
	}

	public void SetWallHip(bool value)
	{
		unionNeck.isOtherAdjust = value;
		if (value)
		{
			unionNeck.otherAdjustPositon = wallHip;
		}
	}

	public void SetPillarBind(bool value)
	{
		unionLeft.isOtherAdjust = value;
		unionRight.isOtherAdjust = value;
		if (value)
		{
			unionLeft.otherAdjustPositon = pillarBind[0];
			unionRight.otherAdjustPositon = pillarBind[1];
		}
	}

	public void SetFuck(bool value)
	{
		unionNeck.isOtherAdjust = value;
		if (value)
		{
			unionNeck.otherAdjustPositon = fuck;
		}
	}
}
