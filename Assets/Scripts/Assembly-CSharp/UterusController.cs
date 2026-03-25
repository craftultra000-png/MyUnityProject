using System.Collections.Generic;
using UnityEngine;

public class UterusController : MonoBehaviour
{
	public FeelerController _feelerController;

	public UterusPoolManager _uterusPoolManager;

	[Space]
	public UterusSelectionAnimancer _uterusAnimancer;

	public ShareAnimancer _uterusPistonAnimancer;

	public SelectionGUIManager _uterusCameraManager;

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
		_uterusAnimancer.ChangeAnimationSpeed(animationSpeed);
		_uterusPistonAnimancer.ChangeAnimationSpeed(animationSpeed);
	}

	public void Insert(bool value)
	{
		isInsert = value;
		if (isInsert)
		{
			InsertSe();
			_uterusAnimancer.StateSet("isInsert", 0.25f);
			_uterusPistonAnimancer.StateSet("isInsert", 0.25f);
			_uterusCameraManager.CameraSet("Insert");
			_uterusPoolManager.SetGag(value: false);
		}
		else
		{
			_uterusAnimancer.StateSet("isIdle", 0.25f);
			_uterusPistonAnimancer.StateSet("isIdle", 0.25f);
			_uterusCameraManager.CameraSet("Idle");
			_uterusPoolManager.SetGag(value: false);
		}
	}

	public void Piston(bool value)
	{
		isPiston = value;
		if (isPiston)
		{
			InsertSe();
			_uterusAnimancer.StateSet("isPiston", 0.25f);
			_uterusPistonAnimancer.StateSet("isPiston", 0.25f);
			_uterusCameraManager.CameraSet("Piston");
			_uterusPoolManager.SetGag(value: false);
		}
		else
		{
			_uterusAnimancer.StateSet("isIdle", 0.25f);
			_uterusPistonAnimancer.StateSet("isIdle", 0.25f);
			_uterusCameraManager.CameraSet("Idle");
			_uterusPoolManager.SetGag(value: false);
		}
	}

	public void Shot(bool value)
	{
		isShot = value;
		if (isShot)
		{
			InsertSe();
			_uterusAnimancer.StateSet("isShot", 0.25f);
			_uterusPistonAnimancer.StateSet("isShot", 0.25f);
			_uterusCameraManager.CameraSet("Shot");
			_uterusPoolManager.SetGag(value: true);
		}
		else
		{
			_uterusAnimancer.StateSet("isIdle", 0.25f);
			_uterusPistonAnimancer.StateSet("isIdle", 0.25f);
			_uterusCameraManager.CameraSet("Idle");
			_uterusPoolManager.SetGag(value: false);
		}
	}

	public void Conceive(bool value)
	{
		isConceive = value;
		if (isConceive)
		{
			ConceiveSe();
			_uterusAnimancer.StateSet("isConceive", 0.25f);
		}
		else
		{
			_uterusAnimancer.StateSet("isIdle", 0.25f);
			_uterusCameraManager.CameraSet("Idle");
		}
	}

	public void Birth(bool value)
	{
		isBirth = value;
		if (isBirth)
		{
			_uterusAnimancer.StateSet("isBirth", 0.25f);
		}
		else
		{
			_uterusAnimancer.StateSet("isIdle", 0.25f);
		}
	}

	public void ShotEffect()
	{
		Object.Instantiate(shotEfect, shotPoint.position, shotPoint.rotation, shotStocker);
		_uterusPoolManager.AddFill(0.4f);
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
