using System.Collections.Generic;
using UnityEngine;

public class AnalController : MonoBehaviour
{
	public FeelerController _feelerController;

	public AnalPoolManager _analPoolManager;

	[Space]
	public ShareAnimancer _analAnimancer;

	public ShareAnimancer _analPistonAnimancer;

	public SelectionGUIManager _analCameraManager;

	public Transform onomatopoeiaLookTarget;

	[Header("Status")]
	public bool isInsert;

	public bool isPiston;

	public bool isShot;

	public bool isConceive;

	public bool isBirth;

	[Header("Anim Speed")]
	public float animationSpeed;

	[Header("Effect")]
	public GameObject shotEfect;

	public Transform shotPoint;

	public Transform shotStocker;

	public List<Transform> onomatopoiatPoint;

	[Header("Se")]
	public AudioClip insertSe;

	public List<AudioClip> pistonSe;

	public AudioClip conceiveSe;

	public void ChangeAnimationSpeed(float value)
	{
		animationSpeed = value;
		_analAnimancer.ChangeAnimationSpeed(animationSpeed);
		_analPistonAnimancer.ChangeAnimationSpeed(animationSpeed);
	}

	public void Insert(bool value)
	{
		isInsert = value;
		if (isInsert)
		{
			InsertSe();
			_analAnimancer.StateSet("isInsert", 0.25f);
			_analPistonAnimancer.StateSet("isInsert", 0.25f);
			_analCameraManager.CameraSet("Insert");
			_analPoolManager.SetGag(value: false);
		}
		else
		{
			_analAnimancer.StateSet("isIdle", 0.25f);
			_analPistonAnimancer.StateSet("isIdle", 0.25f);
			_analCameraManager.CameraSet("Idle");
			_analPoolManager.SetGag(value: false);
		}
	}

	public void Piston(bool value)
	{
		isPiston = value;
		if (isPiston)
		{
			InsertSe();
			_analAnimancer.StateSet("isPiston", 0.25f);
			_analPistonAnimancer.StateSet("isPiston", 0.25f);
			_analCameraManager.CameraSet("Piston");
			_analPoolManager.SetGag(value: false);
		}
		else
		{
			_analAnimancer.StateSet("isIdle", 0.25f);
			_analPistonAnimancer.StateSet("isIdle", 0.25f);
			_analCameraManager.CameraSet("Idle");
			_analPoolManager.SetGag(value: false);
		}
	}

	public void Shot(bool value)
	{
		isShot = value;
		if (isShot)
		{
			InsertSe();
			_analAnimancer.StateSet("isShot", 0.25f);
			_analPistonAnimancer.StateSet("isShot", 0.25f);
			_analCameraManager.CameraSet("Shot");
			_analPoolManager.SetGag(value: true);
		}
		else
		{
			_analAnimancer.StateSet("isIdle", 0.25f);
			_analPistonAnimancer.StateSet("isIdle", 0.25f);
			_analCameraManager.CameraSet("Idle");
			_analPoolManager.SetGag(value: false);
		}
	}

	public void Conceive(bool value)
	{
		isConceive = value;
		if (isConceive)
		{
			ConceiveSe();
			_analAnimancer.StateSet("isConceive", 0.25f);
		}
		else
		{
			_analAnimancer.StateSet("isIdle", 0.25f);
			_analCameraManager.CameraSet("Idle");
		}
	}

	public void Birth(bool value)
	{
		isBirth = value;
		if (isBirth)
		{
			_analAnimancer.StateSet("isBirth", 0.25f);
		}
		else
		{
			_analAnimancer.StateSet("isIdle", 0.25f);
		}
	}

	public void ShotEffect()
	{
		Object.Instantiate(shotEfect, shotPoint.position, shotPoint.rotation, shotStocker);
		_analPoolManager.AddFill(0.5f);
		if (OnomatopoeiaManager.instance.useOtomanopoeia)
		{
			OnomatopoeiaManager.instance.SpawnOnomatopoeia(shotPoint.position, null, "SelectionShot", onomatopoeiaLookTarget);
		}
	}

	public void InsertSe()
	{
		EffectSeManager.instance.PlaySe(insertSe);
	}

	public void PistonSe()
	{
		EffectSeManager.instance.PlaySe(pistonSe[Random.Range(0, pistonSe.Count)]);
		if (OnomatopoeiaManager.instance.useOtomanopoeia)
		{
			OnomatopoeiaManager.instance.SpawnOnomatopoeia(onomatopoiatPoint[Random.Range(0, onomatopoiatPoint.Count)].position, null, "SelectionGlobs", onomatopoeiaLookTarget);
		}
	}

	public void ConceiveSe()
	{
		EffectSeManager.instance.PlaySe(conceiveSe);
	}
}
