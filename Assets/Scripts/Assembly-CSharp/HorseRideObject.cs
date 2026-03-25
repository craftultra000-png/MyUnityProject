using System;
using System.Collections.Generic;
using UnityEngine;

public class HorseRideObject : MonoBehaviour
{
	public CharacterPositionManager _characterPositionManager;

	public CharacterLifeManager _characterLifeManager;

	public Transform baseObject;

	public Transform rideObject;

	public Transform onomatopoeiaPoint;

	[Header("Status")]
	public bool isHorse;

	[Space]
	public bool isRotate;

	public bool isSet;

	public bool isRide;

	[Header("Damage")]
	public float damageTime;

	public float damageTimeMin = 5f;

	public float damageTimeMax = 10f;

	[Header("Foot")]
	public float footMoveTime;

	public float footMoveTimeMax = 1f;

	public float footMoveSpeed = 2f;

	[Space]
	public List<Transform> footBone;

	public List<Transform> footTarget;

	public List<Vector3> footBoneDefaultPosition;

	[Header("Set Transform")]
	public float rotateTime;

	public float rotateTimeMax = 1f;

	public float currentSetRotation;

	public float setRotation = -180f;

	public float currentYOffset = -0.1f;

	public float setSpeed = 5f;

	[Header("RidePosition")]
	public Vector3 defaultPosition;

	public Vector3 setPosition;

	public Vector3 currentPosition;

	public float moveSize = 0.2f;

	public float moveSpeed = 0.5f;

	[Header("Ride Bounce")]
	public Vector3 defaultRidePosition;

	public float bounceHeight = 0.1f;

	public float bounceSpeed = 1f;

	public float bouncePhase;

	[Header("Shake")]
	public float shakePhase;

	public float shakePower = 0.05f;

	[Range(0f, 1f)]
	public float shakePowerCurrnet;

	public float shakePowerCalc;

	public float shakePowerMin = 0.05f;

	public float shakePowerMax = 0.5f;

	[Space]
	public float shakeSpeed = 2f;

	[Range(0f, 1f)]
	public float shakeSpeedCurrent;

	public float shakeSpeedCalc;

	public float shakeSpeedMin = 1f;

	public float shakeSpeedMax = 20f;

	[Header("RideRotation")]
	public float rotationSpeed = 20f;

	public Vector3 rotationRange = new Vector3(5f, 30f, 5f);

	private Vector3 currentRotation;

	private Quaternion defaultRotation;

	[Header("Onomatopoeia")]
	public float onomatopoeiaTime;

	public float onomatopoeiaTimeMin = 3f;

	public float onomatopoeiaTimeMax = 6f;

	[Header("Se")]
	public List<AudioClip> rideSe;

	public List<AudioClip> ridingSe;

	public List<AudioClip> releaseSe;

	private void Start()
	{
		defaultRotation = baseObject.localRotation;
		currentRotation = Vector3.zero;
		defaultPosition = baseObject.localPosition;
		defaultRidePosition = rideObject.localPosition;
		footBoneDefaultPosition.Clear();
		for (int i = 0; i < footBone.Count; i++)
		{
			footBoneDefaultPosition.Add(footBone[i].localPosition);
		}
		base.transform.localRotation = Quaternion.Euler(setRotation, 0f, 0f);
		base.transform.localPosition = new Vector3(defaultPosition.x, currentYOffset, defaultPosition.z);
		onomatopoeiaTime = UnityEngine.Random.Range(onomatopoeiaTimeMin, onomatopoeiaTimeMax);
	}

	private void LateUpdate()
	{
		if (isHorse)
		{
			isRotate = true;
			isSet = true;
		}
		else
		{
			isRotate = false;
			isSet = false;
		}
		if (isHorse)
		{
			if (rotateTime < rotateTimeMax)
			{
				SetTransform();
			}
			else if (footMoveTime < footMoveTimeMax)
			{
				MoveFootBones();
			}
			RideMove();
		}
		else
		{
			if (footMoveTime > 0f)
			{
				MoveFootBones();
			}
			else if (rotateTime > 0f)
			{
				SetTransform();
			}
			RideMove();
		}
	}

	private void SetTransform()
	{
		if (isRotate)
		{
			rotateTime += Time.deltaTime * setSpeed;
			if (rotateTime > rotateTimeMax)
			{
				rotateTime = rotateTimeMax;
			}
		}
		else
		{
			rotateTime -= Time.deltaTime * setSpeed;
			if (rotateTime < 0f)
			{
				rotateTime = 0f;
			}
		}
		float t = rotateTime / rotateTimeMax;
		if (isRotate)
		{
			_ = base.transform.localRotation.eulerAngles;
			float x = Mathf.LerpAngle(setRotation, 0f, t);
			base.transform.localRotation = Quaternion.Euler(x, 0f, 0f);
			float b = 0f;
			float y = Mathf.Lerp(base.transform.localPosition.y, b, t);
			base.transform.localPosition = new Vector3(base.transform.localPosition.x, y, base.transform.localPosition.z);
		}
		else
		{
			_ = base.transform.localRotation.eulerAngles;
			float x2 = Mathf.LerpAngle(setRotation, 0f, t);
			base.transform.localRotation = Quaternion.Euler(x2, 0f, 0f);
			float y2 = Mathf.Lerp(currentYOffset, base.transform.localPosition.y, t);
			base.transform.localPosition = new Vector3(base.transform.localPosition.x, y2, base.transform.localPosition.z);
		}
	}

	public void MoveFootBones()
	{
		if (isSet)
		{
			footMoveTime += Time.deltaTime * footMoveSpeed;
			if (footMoveTime > footMoveTimeMax)
			{
				footMoveTime = footMoveTimeMax;
			}
		}
		else
		{
			footMoveTime -= Time.deltaTime * footMoveSpeed;
			if (footMoveTime < 0f)
			{
				footMoveTime = 0f;
			}
		}
		float t = footMoveTime / footMoveTimeMax;
		for (int i = 0; i < footBone.Count; i++)
		{
			if (isSet)
			{
				footBone[i].position = Vector3.Lerp(footBone[i].position, footTarget[i].position, t);
			}
			else
			{
				footBone[i].localPosition = Vector3.Lerp(footBoneDefaultPosition[i], footBone[i].localPosition, t);
			}
		}
	}

	public void RideMove()
	{
		float deltaTime = Time.deltaTime;
		if (isRide)
		{
			currentRotation.x = rotationRange.x * Mathf.Sin(Time.time * rotationSpeed * (MathF.PI / 180f));
			currentRotation.y = Mathf.PerlinNoise(Time.time * 0.1f, 0f) * rotationRange.y * 2f - rotationRange.y;
			currentRotation.z = rotationRange.z * Mathf.Cos(Time.time * rotationSpeed * 1.2f * (MathF.PI / 180f));
			Quaternion b = defaultRotation * Quaternion.Euler(currentRotation);
			baseObject.localRotation = Quaternion.Lerp(baseObject.localRotation, b, deltaTime);
			currentPosition.x = defaultPosition.x + Mathf.PerlinNoise(Time.time * moveSpeed, 0f) * moveSize * 2f - moveSize;
			currentPosition.y = defaultPosition.y + Mathf.PerlinNoise(0f, Time.time * moveSpeed) * moveSize * 2f - moveSize;
			currentPosition.z = defaultPosition.z + Mathf.PerlinNoise(Time.time * moveSpeed * 0.8f, Time.time * moveSpeed * 0.5f) * moveSize * 2f - moveSize;
			shakePower = Mathf.Lerp(shakePowerMin, shakePowerMax, shakePowerCurrnet);
			shakeSpeed = Mathf.Lerp(shakeSpeedMin, shakeSpeedMax, shakeSpeedCurrent);
			shakePowerCalc = Mathf.Lerp(shakePowerCalc, shakePower, deltaTime * 10f);
			shakeSpeedCalc = Mathf.Lerp(shakeSpeedCalc, shakeSpeed, deltaTime * 10f);
			shakePhase += shakeSpeedCalc * deltaTime;
			if (shakePhase > MathF.PI * 2f)
			{
				shakePhase -= MathF.PI * 2f;
			}
			float num = Mathf.Sin(shakePhase) * shakePowerCalc;
			currentPosition.y += num;
			Vector3 vector = Vector3.Lerp(b: new Vector3(currentPosition.x, defaultPosition.y, currentPosition.z), a: baseObject.localPosition, t: deltaTime);
			baseObject.localPosition = new Vector3(vector.x, defaultPosition.y + (currentPosition.y - defaultPosition.y), vector.z);
			damageTime -= deltaTime * (shakePowerCalc * shakeSpeedCalc + 1f);
			if (damageTime <= 0f)
			{
				damageTime = UnityEngine.Random.Range(damageTimeMin, damageTimeMax);
				_characterLifeManager.HitData("Reaction", "Reaction");
				if (OnomatopoeiaManager.instance.useOtomanopoeia)
				{
					OnomatopoeiaManager.instance.SpawnOnomatopoeia(onomatopoeiaPoint.position, null, "HorseRide", Camera.main.transform);
				}
				EffectSeManager.instance.PlaySe(rideSe[UnityEngine.Random.Range(0, rideSe.Count)]);
			}
		}
		else
		{
			baseObject.localPosition = Vector3.Lerp(baseObject.localPosition, defaultPosition, deltaTime);
			baseObject.localRotation = Quaternion.Lerp(baseObject.localRotation, defaultRotation, deltaTime);
			rideObject.localPosition = Vector3.Lerp(rideObject.localPosition, defaultRidePosition, deltaTime);
		}
	}

	public void Ride()
	{
		isRide = true;
		_characterPositionManager.SetRideBody();
		EffectSeManager.instance.PlaySe(rideSe[UnityEngine.Random.Range(0, rideSe.Count)]);
	}

	public void Release()
	{
		isRide = false;
		_characterPositionManager.SetDefaultBody();
		EffectSeManager.instance.PlaySe(releaseSe[UnityEngine.Random.Range(0, releaseSe.Count)]);
		damageTime = 0f;
	}

	public void ReleaseEnd()
	{
		_characterPositionManager.SetDefaultEnd();
	}
}
