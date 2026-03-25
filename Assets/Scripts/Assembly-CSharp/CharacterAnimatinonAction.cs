using UnityEngine;

public class CharacterAnimatinonAction : MonoBehaviour
{
	public HorseRideObject _horseRideObject;

	public LimbEntombedObject _limbEntombedObject;

	public WartBedObject _wartBedObject;

	public WallHipObject _wallHipObject;

	public PillarBindObject _pillarBindObject;

	public void HorseRideOn()
	{
		_horseRideObject.Ride();
	}

	public void HorseRideOff()
	{
		_horseRideObject.Release();
	}

	public void HorseRideOffEnd()
	{
		_horseRideObject.ReleaseEnd();
	}

	public void LimbHoldEnd()
	{
		_limbEntombedObject.ReleaseEnd();
	}

	public void WartBedEnd()
	{
		_wartBedObject.ReleaseEnd();
	}

	public void WallHipEnd()
	{
		_wallHipObject.ReleaseEnd();
	}

	public void PillarBindEnd()
	{
		_pillarBindObject.ReleaseEnd();
	}
}
