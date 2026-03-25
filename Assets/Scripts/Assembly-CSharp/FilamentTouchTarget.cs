using System.Collections.Generic;
using UnityEngine;

public class FilamentTouchTarget : MonoBehaviour
{
	public FilamentTouchManager _filamentTouchManager;

	public FilamentTouchObject _filamentTouchObject;

	[Header("Attack Type")]
	public string attackType = "Touch";

	public string onomatopoeiaType = "Touch";

	[Header("Raycast Data")]
	public Transform baseObject;

	public Transform targetObject;

	public Transform aimObject;

	public Transform defaultObject;

	public Transform raycastObject;

	[Header("Target")]
	public bool targetFrontL;

	public bool targetFrontR;

	public bool targetBackL;

	public bool targetBackR;

	public List<Transform> targetList;

	public float targetChangeWait;

	public float targetChangeWaitMin = 5f;

	public float targetChangeWaitMax = 15f;

	[Header("Status")]
	public bool isSearch;

	public bool isHit;

	public Transform currentTarget;

	[Header("Setting")]
	public float raycastDistance = 5f;

	public Vector3 defaultAimPosition;

	[Header("ChangeTargetWait")]
	public float hitTargetWait;

	public float hitTargetWaitMax = 1f;

	[Header("Calc")]
	public float rotationSpeed = 5f;

	public float aimSpeed = 2f;

	public float aimLostSpeed = 1f;

	public float aimEndSpeed = 6f;

	public Vector3 targetPosition;

	public Vector3 calcPosition;

	public Vector3 hitPosition;

	[Header("Bend Onomatopoeia")]
	public Transform onomatopoeiaPoint;

	public float onomatopoeiaTime;

	public float onomatopoeiaTimeMin = 5f;

	public float onomatopoeiaTimeMax = 15f;

	[Header("Layer Settings")]
	public int layerMask;

	[Header("Random Rotation")]
	public float randomAngle = 20f;

	public float randomSpeed = 1f;

	private float noiseTime;

	private float noiseSpeed = 1f;

	private Vector3 noiseSeed;

	private Vector3 noiseRotation;

	private Quaternion randomRotationOffset;

	private void Start()
	{
		layerMask = 1;
		randomRotationOffset = Quaternion.identity;
		_filamentTouchObject.isAim = true;
		_filamentTouchObject.isAimEnd = false;
		targetPosition = defaultObject.position;
		aimObject.position = targetPosition;
		defaultAimPosition = defaultObject.position;
		defaultAimPosition.y += 1f;
		raycastObject.rotation = Quaternion.LookRotation(defaultAimPosition, raycastObject.up);
		noiseSeed = new Vector3(Random.Range(0f, 100f), Random.Range(0f, 100f), Random.Range(0f, 100f));
		if (targetFrontL)
		{
			targetList = _filamentTouchManager.targetFrontL;
		}
		else if (targetFrontR)
		{
			targetList = _filamentTouchManager.targetFrontR;
		}
		else if (targetBackL)
		{
			targetList = _filamentTouchManager.targetBackL;
		}
		else if (targetBackR)
		{
			targetList = _filamentTouchManager.targetBackR;
		}
		ChangeTarget();
		onomatopoeiaTime = Random.Range(onomatopoeiaTimeMin, onomatopoeiaTimeMax);
	}

	private void LateUpdate()
	{
		if (isSearch)
		{
			targetChangeWait -= Time.deltaTime;
			if (targetChangeWait < 0f)
			{
				ChangeTarget();
			}
			Quaternion quaternion = Quaternion.LookRotation(targetObject.position - raycastObject.position, raycastObject.up);
			RandomRotation();
			Quaternion b = quaternion * randomRotationOffset;
			raycastObject.rotation = Quaternion.Lerp(raycastObject.rotation, b, Time.deltaTime * rotationSpeed);
			Ray ray = new Ray(raycastObject.position, raycastObject.forward);
			Debug.DrawLine(ray.origin, ray.origin + ray.direction * raycastDistance, Color.green);
			if (Physics.Raycast(ray, out var hitInfo, raycastDistance, layerMask))
			{
				if (hitInfo.collider.CompareTag("Character"))
				{
					isHit = true;
					hitTargetWait = hitTargetWaitMax;
					currentTarget = hitInfo.transform;
					hitPosition = hitInfo.point;
					calcPosition = Vector3.Lerp(calcPosition, hitPosition, Time.deltaTime * aimSpeed);
					targetPosition = Vector3.Lerp(aimObject.position, calcPosition, Time.deltaTime * aimSpeed);
					aimObject.position = targetPosition;
					onomatopoeiaTime -= Time.deltaTime;
					if (onomatopoeiaTime < 0f)
					{
						onomatopoeiaTime = Random.Range(onomatopoeiaTimeMin, onomatopoeiaTimeMax);
						if ((bool)hitInfo.collider.gameObject.GetComponent<CharacterColliderObject>())
						{
							hitInfo.collider.gameObject.GetComponent<CharacterColliderObject>().HitData(attackType);
						}
						OnomatopoeiaManager.instance.SpawnOnomatopoeia(onomatopoeiaPoint.position, null, onomatopoeiaType, Camera.main.transform);
					}
				}
				else
				{
					if (hitTargetWait >= 0f)
					{
						hitTargetWait -= Time.deltaTime;
					}
					if (hitTargetWait < 0f)
					{
						LookEnd();
						onomatopoeiaTime = Random.Range(onomatopoeiaTimeMin, onomatopoeiaTimeMax);
					}
					else
					{
						calcPosition = Vector3.Lerp(calcPosition, hitPosition, Time.deltaTime * aimSpeed);
						targetPosition = Vector3.Lerp(aimObject.position, calcPosition, Time.deltaTime * aimSpeed);
						aimObject.position = targetPosition;
					}
				}
			}
			else
			{
				if (hitTargetWait >= 0f)
				{
					hitTargetWait -= Time.deltaTime;
				}
				if (hitTargetWait < 0f)
				{
					LookEnd();
					onomatopoeiaTime = Random.Range(onomatopoeiaTimeMin, onomatopoeiaTimeMax);
				}
				else
				{
					calcPosition = Vector3.Lerp(calcPosition, hitPosition, Time.deltaTime * aimSpeed);
					targetPosition = Vector3.Lerp(aimObject.position, calcPosition, Time.deltaTime * aimSpeed);
					aimObject.position = targetPosition;
				}
			}
		}
		else
		{
			LookDefault();
			LookEnd();
		}
	}

	public void LookEnd()
	{
		isHit = false;
		currentTarget = null;
		if (isSearch)
		{
			targetPosition = Vector3.Lerp(aimObject.position, defaultObject.position, Time.deltaTime * aimLostSpeed);
		}
		else
		{
			targetPosition = Vector3.Lerp(aimObject.position, defaultObject.position, Time.deltaTime * aimEndSpeed);
		}
		calcPosition = targetPosition;
		hitPosition = targetPosition;
		aimObject.position = targetPosition;
	}

	public void LookDefault()
	{
		hitTargetWait = -1f;
		Quaternion b = Quaternion.LookRotation(defaultAimPosition, raycastObject.up);
		raycastObject.rotation = Quaternion.Lerp(raycastObject.rotation, b, Time.deltaTime * rotationSpeed);
	}

	public void RandomRotation()
	{
		noiseTime += Time.deltaTime * noiseSpeed;
		float x = Mathf.PerlinNoise(noiseTime + noiseSeed.x, 0f) * 2f - 1f;
		float y = Mathf.PerlinNoise(noiseTime + noiseSeed.y, 1f) * 2f - 1f;
		float z = Mathf.PerlinNoise(noiseTime + noiseSeed.z, 2f) * 2f - 1f;
		noiseRotation = new Vector3(x, y, z);
		noiseRotation *= randomAngle;
		randomRotationOffset = Quaternion.Euler(noiseRotation);
		randomRotationOffset = Quaternion.Slerp(randomRotationOffset, randomRotationOffset, Time.deltaTime * randomSpeed);
	}

	public void ChangeTarget()
	{
		targetChangeWait = Random.Range(targetChangeWaitMin, targetChangeWaitMax);
		targetObject = targetList[Random.Range(0, targetList.Count)];
	}
}
