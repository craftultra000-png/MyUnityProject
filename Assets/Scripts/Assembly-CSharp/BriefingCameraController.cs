using System.Collections.Generic;
using UnityEngine;

public class BriefingCameraController : MonoBehaviour
{
	public Rigidbody _rigidbody;

	public SphereCollider _collider;

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

	[Header("Mouse")]
	public float mouseWait = 1f;

	public float xSpeed = 200f;

	public float ySpeed = 200f;

	public float x;

	public float y;

	public float defaultX = -185f;

	public float defaultY = -63f;

	[Header("Move")]
	public float currentSpeed;

	public float currentSpeedMax;

	public float moveSpeed = 3f;

	public float moveSpeedMax = 3f;

	public float sprintSpeed = 8f;

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

	[Header("Wait Time")]
	public float waitTime;

	[Header("Bounce")]
	public float bounceForce = 0.1f;

	public float damping = 0.2f;

	public Vector3 currentBounceForce = Vector3.zero;

	public float bounceTimeMax = 0.5f;

	[Header("Se")]
	public AudioClip titsSe;

	public List<AudioClip> bounceSe;

	public AudioClip stageFeelSe;

	private void Start()
	{
		x = defaultX;
		y = defaultY;
		currentPosition = base.transform.position;
		targetPosition = base.transform.position;
	}

	private void Update()
	{
		if (isPause)
		{
			return;
		}
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
			if (waitTime <= 0f)
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
		else if (isScreenLock)
		{
			waitTime -= Time.deltaTime;
		}
	}

	public void CameraLock(bool value)
	{
		isScreenLock = value;
	}

	public void SetUp()
	{
		isSet = true;
		x = defaultX;
		y = defaultY;
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
		waitTime = 0f;
		x = defaultX;
		y = defaultY;
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
		BriefingCore.instance.Pause(value: false);
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
		if (!collision.gameObject.CompareTag("Character"))
		{
			return;
		}
		currentBounceForce = (base.transform.position - collision.contacts[0].point).normalized * bounceForce;
		_rigidbody.AddForce(currentBounceForce, ForceMode.Impulse);
		if (waitTime <= 0f)
		{
			EffectSeManager.instance.PlaySe(bounceSe[Random.Range(0, bounceSe.Count)]);
			if ((bool)collision.gameObject.GetComponent<CharacterColliderObject>() && OnomatopoeiaManager.instance.useOtomanopoeia)
			{
				Vector3 point = collision.contacts[0].point;
				Vector3 vector = Camera.main.transform.forward * 0.1f;
				Vector3 position = point + vector;
				OnomatopoeiaManager.instance.SpawnOnomatopoeia(position, null, "Bounce", Camera.main.transform);
			}
			if ((bool)collision.gameObject.GetComponent<CharacterColliderBelly>())
			{
				collision.gameObject.GetComponent<CharacterColliderBelly>().HitPosition(collision.contacts[0].point);
			}
		}
		waitTime = bounceTimeMax;
	}
}
