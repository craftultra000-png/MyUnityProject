using UnityEngine;

public class FilamentTouchPlayer : MonoBehaviour
{
	public FilamentTouchObject _filamentTouchObject;

	[Header("Attack Type")]
	public string attackType = "Touch";

	public string onomatopoeiaType = "Touch";

	[Header("Raycast Data")]
	public Transform baseObject;

	public Transform aimObject;

	public Transform defaultObject;

	public Transform raycastObject;

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
		_filamentTouchObject.isSearch = true;
		_filamentTouchObject.isAim = true;
		_filamentTouchObject.isAimEnd = false;
		targetPosition = defaultObject.position;
		aimObject.position = targetPosition;
		defaultAimPosition = defaultObject.position;
		defaultAimPosition.y += 1f;
		baseObject.rotation = Quaternion.LookRotation(baseObject.position, defaultObject.position - baseObject.position);
		noiseSeed = new Vector3(Random.Range(0f, 100f), Random.Range(0f, 100f), Random.Range(0f, 100f));
		onomatopoeiaTime = Random.Range(onomatopoeiaTimeMin, onomatopoeiaTimeMax);
	}

	private void LateUpdate()
	{
		Ray ray = new Ray(raycastObject.position, raycastObject.up);
		Debug.DrawLine(ray.origin, ray.origin + ray.direction * raycastDistance, Color.red);
		if (isSearch)
		{
			Vector3 vector = raycastObject.position + raycastObject.up * raycastDistance;
			Quaternion quaternion = Quaternion.LookRotation(vector - baseObject.position);
			RandomRotation();
			Quaternion b = quaternion * randomRotationOffset;
			baseObject.rotation = Quaternion.Lerp(baseObject.rotation, b, Time.deltaTime * rotationSpeed);
			Ray ray2 = new Ray(raycastObject.position, raycastObject.up);
			Debug.DrawLine(ray2.origin, ray2.origin + ray2.direction * raycastDistance, Color.green);
			if (Physics.Raycast(ray2, out var hitInfo, raycastDistance, layerMask))
			{
				if (hitInfo.collider.CompareTag("Character"))
				{
					isHit = true;
					hitTargetWait = hitTargetWaitMax;
					currentTarget = hitInfo.transform;
					hitPosition = hitInfo.point;
					calcPosition = Vector3.Lerp(calcPosition, hitPosition, Time.deltaTime * aimSpeed);
					vector = Vector3.Lerp(aimObject.position, calcPosition, Time.deltaTime * aimSpeed);
					aimObject.position = vector;
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
						vector = Vector3.Lerp(aimObject.position, calcPosition, Time.deltaTime * aimSpeed);
						aimObject.position = vector;
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
					vector = Vector3.Lerp(aimObject.position, calcPosition, Time.deltaTime * aimSpeed);
					aimObject.position = vector;
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
		Quaternion b = Quaternion.LookRotation(raycastObject.position + raycastObject.up * raycastDistance - baseObject.position);
		baseObject.rotation = Quaternion.Lerp(baseObject.rotation, b, Time.deltaTime * rotationSpeed);
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
}
