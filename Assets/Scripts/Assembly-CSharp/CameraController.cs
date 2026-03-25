using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public FeelerController _feelerController;

	public FeelerControllerData _feelerControllerData;

	public Transform target;

	public Rigidbody _rigidbody;

	public SphereCollider _collider;

	public CharacterColliderBelly _characterColliderBelly;

	[Header("Attack Type")]
	public string attackType = "Player";

	[Header("Mouse Wheel Switch Option")]
	public bool mouseWheelSwitch;

	public float mouseSensitivity = 1f;

	public float wheelSensitivity = 1f;

	[Header("Status")]
	public bool isPause;

	public bool isSet;

	public bool isScreenLock;

	[Header("Status")]
	public bool isCharge;

	public bool isChargeHit;

	public bool isAuto;

	[Header("Status")]
	public bool isMelt;

	public bool isRestore;

	[Header("Auto")]
	public Transform autoBase;

	public Transform autoTarget;

	public float autoPositionSpeed = 0.1f;

	public float autoRotationSpeed = 50f;

	public float autoWaitTime;

	[Header("Mouse")]
	public float mouseWait = 1f;

	public float xSpeed = 200f;

	public float ySpeed = 200f;

	public float x;

	public float y;

	[Header("Move")]
	public float currentSpeed;

	public float currentSpeedMax;

	public float moveSpeed = 1f;

	public float moveSpeedMax = 3f;

	public float sprintSpeed = 2f;

	public float sprintSpeedMax = 6f;

	[Header("Calc")]
	public float currentZoom;

	public Vector3 startPosition = new Vector3(0f, 1f, -1f);

	public Vector3 currentPosition;

	public Vector3 targetPosition;

	public Vector3 calcPosition;

	public Vector3 calcMove;

	[Header("StageCollider")]
	public StageColliderObject _stageColliderObject;

	[Header("Charge")]
	public float chargeWaitTime;

	public float chargeWaitTimeMax = 1f;

	public float chargeHitWaitTimeMax = 1f;

	public float chargeForce = 2f;

	public float releaseForce = 0.25f;

	public float chargeAmount = 0.075f;

	[Header("Wait Time")]
	public float waitTime;

	[Header("Bounce")]
	public float bounceForce = 0.1f;

	public float damping = 0.2f;

	public Vector3 currentBounceForce = Vector3.zero;

	public float bounceTimeMax = 0.5f;

	[Header("Cannon")]
	public float cannonForce = 5f;

	public float cannonTimeMax = 0.05f;

	public float cannonDistance;

	[Header("Effect")]
	public Transform effectStocker;

	public GameObject meltEffect;

	public GameObject restoreEffect;

	[Header("Se")]
	public AudioClip titsSe;

	public List<AudioClip> bounceSe;

	public AudioClip stageFeelSe;

	[Space]
	public List<AudioClip> meltSe;

	public AudioClip restoreSe;

	private void Start()
	{
		x = 0f;
		y = 0f;
		currentPosition = base.transform.position;
		targetPosition = base.transform.position;
	}

	private void Update()
	{
		if (isPause)
		{
			return;
		}
		if (chargeWaitTime > 0f)
		{
			chargeWaitTime -= Time.deltaTime;
			if (chargeWaitTime < 0f)
			{
				chargeWaitTime = 0f;
				isCharge = false;
				_feelerControllerData.isClickCharge = false;
				if (isChargeHit)
				{
					isChargeHit = false;
					if (_characterColliderBelly != null)
					{
						_characterColliderBelly.HitPosition(base.transform.position);
						_characterColliderBelly = null;
					}
					base.transform.parent = null;
					_collider.enabled = true;
					Release();
				}
			}
		}
		if (!isAuto)
		{
			if (!isScreenLock)
			{
				x += Input.GetAxis("Mouse X") * xSpeed * Time.deltaTime * mouseSensitivity;
				y -= Input.GetAxis("Mouse Y") * ySpeed * Time.deltaTime * mouseSensitivity;
				if (isSet)
				{
					if (x < -360f)
					{
						x += 360f;
					}
					else if (x > 360f)
					{
						x -= 360f;
					}
					if (y > 90f)
					{
						y = 90f;
					}
					else if (y < -90f)
					{
						y = -90f;
					}
					base.transform.rotation = Quaternion.Euler(y, x, 0f);
				}
				else
				{
					SetUp();
				}
			}
			if (waitTime <= 0f && !isCharge)
			{
				if (Input.GetKey(KeyCode.LeftShift))
				{
					currentSpeed = sprintSpeed;
					currentSpeedMax = sprintSpeedMax;
				}
				else
				{
					currentSpeed = moveSpeed;
					currentSpeedMax = moveSpeedMax;
				}
				if (!mouseWheelSwitch)
				{
					float axis = Input.GetAxis("Mouse ScrollWheel");
					if (axis != 0f)
					{
						calcMove += base.transform.forward * axis * currentSpeed * wheelSensitivity;
					}
					if (Input.GetKey(KeyCode.W))
					{
						calcMove += base.transform.up * currentSpeed * Time.deltaTime;
					}
					else if (Input.GetKey(KeyCode.S))
					{
						calcMove -= base.transform.up * currentSpeed * Time.deltaTime;
					}
				}
				else
				{
					float axis2 = Input.GetAxis("Mouse ScrollWheel");
					if (axis2 != 0f)
					{
						calcMove += base.transform.up * axis2 * currentSpeed * wheelSensitivity;
					}
					if (Input.GetKey(KeyCode.W))
					{
						calcMove += base.transform.forward * currentSpeed * Time.deltaTime;
					}
					else if (Input.GetKey(KeyCode.S))
					{
						calcMove -= base.transform.forward * currentSpeed * Time.deltaTime;
					}
				}
				if (Input.GetKey(KeyCode.A))
				{
					calcMove -= base.transform.right * currentSpeed * Time.deltaTime;
				}
				else if (Input.GetKey(KeyCode.D))
				{
					calcMove += base.transform.right * currentSpeed * Time.deltaTime;
				}
				_rigidbody.AddForce(calcMove, ForceMode.VelocityChange);
				if (_rigidbody.velocity.magnitude > currentSpeedMax)
				{
					_rigidbody.velocity = _rigidbody.velocity.normalized * currentSpeedMax;
				}
			}
			else
			{
				waitTime -= Time.deltaTime;
			}
			calcMove = Vector3.zero;
		}
		else if (isAuto)
		{
			if (autoWaitTime < 1f)
			{
				autoWaitTime += Time.deltaTime;
				base.transform.position = Vector3.Lerp(base.transform.position, autoBase.position, autoWaitTime);
				Vector3 normalized = (autoTarget.position - base.transform.position).normalized;
				Quaternion b = Quaternion.LookRotation(normalized, Vector3.up);
				base.transform.rotation = Quaternion.Slerp(base.transform.rotation, b, autoWaitTime);
				if (autoWaitTime > 1f)
				{
					Cannon();
					isAuto = false;
					autoBase = null;
					autoTarget = null;
					x = Mathf.Atan2(normalized.x, normalized.z) * 57.29578f;
					y = (0f - Mathf.Asin(normalized.y)) * 57.29578f;
				}
			}
		}
		else if (isScreenLock)
		{
			waitTime -= Time.deltaTime;
		}
	}

	public void SetUp()
	{
		mouseWait -= Time.deltaTime;
		if (mouseWait <= 0f)
		{
			isSet = true;
		}
		x = 0f;
		y = 0f;
	}

	public void Respawn()
	{
		base.transform.position = startPosition;
		base.transform.rotation = Quaternion.identity;
		base.transform.localScale = Vector3.one;
		base.transform.parent = null;
		_rigidbody.velocity = Vector3.zero;
		_rigidbody.angularVelocity = Vector3.zero;
		_collider.enabled = true;
		isSet = false;
		isAuto = false;
		isCharge = false;
		isChargeHit = false;
		_feelerControllerData.isClickCharge = false;
		autoBase = null;
		autoTarget = null;
		autoWaitTime = 0f;
		waitTime = 0f;
		chargeWaitTime = 0f;
		x = 0f;
		y = 0f;
		_feelerController.clickMissGurad = true;
		SystemCore.instance.Pause(value: false);
	}

	private void OnCollisionEnter(Collision collision)
	{
		SetCollisiton(collision);
	}

	private void OnCollisionStay(Collision collision)
	{
		SetCollisiton(collision);
	}

	public void SetCollisiton(Collision collision)
	{
		if (collision.gameObject.CompareTag("Character"))
		{
			if (isCharge && !isChargeHit)
			{
				isChargeHit = true;
				_collider.enabled = false;
				_rigidbody.velocity = Vector3.zero;
				_rigidbody.angularVelocity = Vector3.zero;
				Vector3 position = collision.contacts[0].point + collision.contacts[0].normal * chargeAmount;
				base.transform.position = position;
				base.transform.SetParent(collision.transform);
				chargeWaitTime = chargeHitWaitTimeMax;
				SkillGUIDataBase.instance.MouseCoolTime(chargeHitWaitTimeMax);
				EffectSeManager.instance.PlaySe(titsSe);
				if (collision.gameObject.GetComponent<CharacterColliderObject>().bodyType == CharacterColliderObject.BodyType.TitsL)
				{
					collision.gameObject.GetComponent<CharacterColliderObject>().HitData("Charge");
				}
				if (collision.gameObject.GetComponent<CharacterColliderObject>().bodyType == CharacterColliderObject.BodyType.TitsR)
				{
					collision.gameObject.GetComponent<CharacterColliderObject>().HitData("Charge");
				}
				if (collision.gameObject.GetComponent<CharacterColliderObject>().bodyType == CharacterColliderObject.BodyType.HipL)
				{
					collision.gameObject.GetComponent<CharacterColliderObject>().HitData("Charge");
				}
				if (collision.gameObject.GetComponent<CharacterColliderObject>().bodyType == CharacterColliderObject.BodyType.HipR)
				{
					collision.gameObject.GetComponent<CharacterColliderObject>().HitData("Charge");
				}
				if ((bool)collision.gameObject.GetComponent<CharacterColliderBelly>())
				{
					_characterColliderBelly = collision.gameObject.GetComponent<CharacterColliderBelly>();
				}
				if (OnomatopoeiaManager.instance.useOtomanopoeia)
				{
					Vector3 point = collision.contacts[0].point;
					Vector3 vector = Camera.main.transform.forward * 0.1f;
					Vector3 position2 = point + vector;
					OnomatopoeiaManager.instance.SpawnOnomatopoeia(position2, null, "Stick", Camera.main.transform);
				}
				if (isMelt)
				{
					Melt();
				}
				else if (isRestore)
				{
					Restore();
				}
			}
			else if (!isCharge)
			{
				currentBounceForce = (base.transform.position - collision.contacts[0].point).normalized * bounceForce;
				_rigidbody.AddForce(currentBounceForce, ForceMode.Impulse);
				if (waitTime <= 0f)
				{
					EffectSeManager.instance.PlaySe(bounceSe[Random.Range(0, bounceSe.Count)]);
					if ((bool)collision.gameObject.GetComponent<CharacterColliderObject>())
					{
						collision.gameObject.GetComponent<CharacterColliderObject>().HitData(attackType);
						if (OnomatopoeiaManager.instance.useOtomanopoeia)
						{
							Vector3 point2 = collision.contacts[0].point;
							Vector3 vector2 = Camera.main.transform.forward * 0.1f;
							Vector3 position3 = point2 + vector2;
							OnomatopoeiaManager.instance.SpawnOnomatopoeia(position3, null, "Bounce", Camera.main.transform);
						}
					}
					if ((bool)collision.gameObject.GetComponent<CharacterColliderBelly>())
					{
						collision.gameObject.GetComponent<CharacterColliderBelly>().HitPosition(collision.contacts[0].point);
					}
					if (isMelt)
					{
						Melt();
					}
					else if (isRestore)
					{
						Restore();
					}
				}
				waitTime = bounceTimeMax;
			}
		}
		if (collision.gameObject.CompareTag("StageActionObject"))
		{
			_stageColliderObject = collision.gameObject.GetComponent<StageColliderObject>();
			if (_stageColliderObject.StageType == StageColliderObject.BodyType.Bounce)
			{
				currentBounceForce = (base.transform.position - collision.contacts[0].point).normalized * bounceForce * 2f;
				_rigidbody.AddForce(currentBounceForce, ForceMode.Impulse);
				waitTime = bounceTimeMax;
			}
			if (_stageColliderObject.StageType == StageColliderObject.BodyType.Cannon && waitTime <= 0f)
			{
				isAuto = true;
				autoBase = _stageColliderObject.baseTransform;
				autoTarget = _stageColliderObject.targetTransform;
				waitTime = cannonTimeMax;
				autoWaitTime = 0f;
			}
		}
	}

	public void Melt()
	{
		Object.Instantiate(meltEffect, base.transform.position, Quaternion.identity, effectStocker);
		EffectSeManager.instance.PlaySe(meltSe[Random.Range(0, meltSe.Count)]);
	}

	public void MeltSe()
	{
		EffectSeManager.instance.PlaySe(meltSe[Random.Range(0, meltSe.Count)]);
	}

	public void Restore()
	{
		Object.Instantiate(restoreEffect, base.transform.position, Quaternion.identity, effectStocker);
		EffectSeManager.instance.PlaySe(restoreSe);
	}

	public void RestoreSe()
	{
		EffectSeManager.instance.PlaySe(restoreSe);
	}

	public void Cannon()
	{
		Vector3 normalized = (autoTarget.position - base.transform.position).normalized;
		cannonDistance = Vector3.Distance(base.transform.position, autoTarget.position);
		float num = cannonDistance * cannonForce;
		_rigidbody.AddForce(normalized * num, ForceMode.Impulse);
		if (OnomatopoeiaManager.instance.useOtomanopoeia)
		{
			Vector3 position = Camera.main.transform.position + Camera.main.transform.forward * 0.1f;
			OnomatopoeiaManager.instance.SpawnOnomatopoeia(position, Camera.main.transform, "JellyShot", Camera.main.transform);
		}
	}

	public void Charge()
	{
		if (chargeWaitTime <= 0f && !isAuto && !isCharge)
		{
			isCharge = true;
			_feelerControllerData.isClickCharge = true;
			Vector3 normalized = base.transform.forward.normalized;
			float num = chargeForce;
			_rigidbody.AddForce(normalized * num, ForceMode.Impulse);
			chargeWaitTime = chargeWaitTimeMax;
			SkillGUIDataBase.instance.MouseCoolTime(chargeWaitTimeMax);
		}
	}

	public void Release()
	{
		currentBounceForce = (-base.transform.forward).normalized * releaseForce;
		_rigidbody.AddForce(currentBounceForce, ForceMode.Impulse);
		EffectSeManager.instance.PlaySe(bounceSe[Random.Range(0, bounceSe.Count)]);
		waitTime = bounceTimeMax;
		if (OnomatopoeiaManager.instance.useOtomanopoeia)
		{
			Vector3 position = Camera.main.transform.position + Camera.main.transform.forward * 0.1f;
			OnomatopoeiaManager.instance.SpawnOnomatopoeia(position, null, "Bounce", Camera.main.transform);
		}
	}
}
