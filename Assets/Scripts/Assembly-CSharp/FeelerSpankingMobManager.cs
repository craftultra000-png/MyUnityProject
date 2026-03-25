using UnityEngine;

public class FeelerSpankingMobManager : MonoBehaviour
{
	public float raycastDistance = 5f;

	[Header("Script")]
	public FeelerSpankingMob _feelerSpankingMob;

	[Header("Status")]
	public bool isSet;

	public bool isHit;

	public Transform currentTarget;

	[Header("Aim Object")]
	public Transform aimObject;

	public int layerMask;

	[Header("Target")]
	public Transform target;

	public Transform hitTarget;

	[Header("CoolTime")]
	public bool isCoolTime;

	public float coolTime;

	public float coolTimeMax = 1.5f;

	[Header("Calc")]
	public float rotationSpeed = 5f;

	public float aimSpeed = 2f;

	public float aimLostSpeed = 1f;

	public float aimEndSpeed = 6f;

	public Vector3 targetPosition;

	public Vector3 calcPosition;

	public Vector3 hitPosition;

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
		aimObject.gameObject.SetActive(value: false);
		layerMask = 1;
		noiseSeed = new Vector3(Random.Range(0f, 100f), Random.Range(0f, 100f), Random.Range(0f, 100f));
	}

	private void FixedUpdate()
	{
		if (isCoolTime)
		{
			coolTime -= Time.deltaTime;
			if (coolTime < 0f)
			{
				isCoolTime = false;
			}
		}
		if (!isSet)
		{
			return;
		}
		Quaternion quaternion = Quaternion.LookRotation(target.position - base.transform.position, base.transform.up);
		RandomRotation();
		Quaternion b = quaternion * randomRotationOffset;
		base.transform.rotation = Quaternion.Lerp(base.transform.rotation, b, Time.deltaTime * rotationSpeed);
		if (isHit)
		{
			return;
		}
		Ray ray = new Ray(base.transform.position, base.transform.forward);
		Debug.DrawLine(ray.origin, ray.origin + ray.direction * raycastDistance, Color.red);
		if (Physics.Raycast(ray, out var hitInfo, raycastDistance, layerMask))
		{
			if (hitInfo.collider.CompareTag("Character"))
			{
				targetPosition = hitInfo.point;
				aimObject.position = targetPosition;
				hitTarget = hitInfo.transform;
				isHit = true;
				_feelerSpankingMob.Spanking();
			}
			else
			{
				hitTarget = null;
			}
		}
		else
		{
			hitTarget = null;
		}
	}

	public void SetMarker(bool value)
	{
		isSet = value;
		hitTarget = null;
		aimObject.position = targetPosition;
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
