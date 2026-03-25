using Animancer;
using UnityEngine;

public class UterusChildFeelerObject : MonoBehaviour
{
	public AnimancerComponent _animancer;

	public UterusChildFeelerManager _uterusChildFeelerManager;

	public Transform onomatopoeiaLookTarget;

	[Header("Status")]
	public bool isStay;

	public bool isMove;

	public bool isStayMove;

	public bool isRotateEnd;

	public bool isImpregnate;

	public bool isBirth;

	public bool isExit;

	public bool isSizeChange;

	[Header("Move")]
	public float moveSpeed = 1.5f;

	public float moveSpeedMin = 1.2f;

	public float moveSpeedMax = 1.8f;

	public float stayMoveSpeed = 0.2f;

	public float stayMoveSpeedMin = 0.1f;

	public float stayMoveSpeedMax = 0.3f;

	public float stayWaitTime = 5f;

	public float stayWaitTimeMin = 2f;

	public float stayWaitTimeMax = 10f;

	public float birthSpeed = 3f;

	public float moveRotationSpeed = 10f;

	public float stayMoveRotationSpeed = 0.5f;

	[Space]
	public Vector3 birthPosition;

	public Vector3 impregnatePosition;

	public Vector3 exitPosition;

	public Vector3 movePosition;

	public Vector3 movePositionCurrent;

	public Vector3 stayPosition;

	public Vector3 stayPositionCurrent;

	public Vector3 calcPosition;

	public Vector3 digPosition;

	[Header("Uterus Param")]
	public float conceiveParameter;

	[Header("Grow")]
	public float currentSize = 0.2f;

	public float maxSize = 0.5f;

	public float growSpeed = 0.02f;

	public Vector3 currentScale;

	[Header("Collider")]
	public Collider _collider;

	[Header("RandomRotate")]
	public float currentRotation;

	[Header("Onomatopoeia")]
	public float onomatopoeiaTime;

	public float onomatopoeiaTimeMin = 5f;

	public float onomatopoeiaTimeMax = 15f;

	[Header("Motion")]
	public float feedTime = 0.5f;

	public AnimationClip growClip;

	public AnimationClip curlClip;

	public AnimationClip rollClip;

	private AnimancerState _state;

	[Header("SE")]
	public AudioClip digSe;

	public AudioClip moveSe;

	private void Start()
	{
		_animancer.Play(curlClip, 0f);
		moveSpeed = Random.Range(moveSpeedMin, moveSpeedMax);
		isMove = true;
		_collider.enabled = false;
		onomatopoeiaTime = Random.Range(onomatopoeiaTimeMin, onomatopoeiaTimeMax);
	}

	private void LateUpdate()
	{
		if (stayWaitTime > 0f)
		{
			stayWaitTime -= Time.deltaTime;
		}
		if (isStay && currentSize < maxSize)
		{
			currentSize += Time.deltaTime * growSpeed;
			if (currentSize > maxSize)
			{
				currentSize = maxSize;
			}
			currentScale = Vector3.one * currentSize;
			base.transform.localScale = currentScale;
			if (OnomatopoeiaManager.instance.useOtomanopoeia)
			{
				onomatopoeiaTime -= Time.deltaTime;
				if (onomatopoeiaTime < 0f)
				{
					onomatopoeiaTime = Random.Range(onomatopoeiaTimeMin, onomatopoeiaTimeMax);
					OnomatopoeiaManager.instance.SpawnOnomatopoeia(base.transform.position, null, "ChildGlow", onomatopoeiaLookTarget);
				}
			}
		}
		if (isSizeChange)
		{
			isSizeChange = false;
			GetMovePosition();
		}
		if (movePositionCurrent != movePosition && isImpregnate)
		{
			isMove = true;
			isStayMove = false;
			isRotateEnd = false;
			if (isBirth)
			{
				_animancer.Play(curlClip, 0f);
			}
		}
		if (isMove)
		{
			movePositionCurrent = movePosition;
			if (!isImpregnate)
			{
				calcPosition = birthPosition;
			}
			else if (isBirth && !isExit)
			{
				calcPosition = birthPosition;
			}
			else if (isExit)
			{
				calcPosition = exitPosition;
			}
			else
			{
				calcPosition = movePosition;
			}
			Vector3 normalized = (calcPosition - base.transform.position).normalized;
			if (isBirth || isExit)
			{
				base.transform.position += normalized * birthSpeed * Time.deltaTime;
			}
			else
			{
				base.transform.position += normalized * moveSpeed * Time.deltaTime;
			}
			if (isBirth || (!isBirth && !isStay))
			{
				float num = Mathf.Atan2(normalized.y, normalized.x) * 57.29578f;
				Quaternion b = Quaternion.Euler(0f, 0f, num + 90f);
				base.transform.rotation = Quaternion.Slerp(base.transform.rotation, b, Time.deltaTime * moveRotationSpeed);
			}
			else
			{
				_animancer.Play(growClip, feedTime);
				Quaternion b2 = Quaternion.Euler(currentRotation, -90f, 90f);
				base.transform.rotation = Quaternion.Slerp(base.transform.rotation, b2, Time.deltaTime * moveRotationSpeed);
			}
			if (Vector3.Distance(base.transform.position, calcPosition) < 0.05f)
			{
				if (!isImpregnate)
				{
					isImpregnate = true;
					return;
				}
				if (isBirth && !isExit)
				{
					isExit = true;
					return;
				}
				if (isExit)
				{
					_uterusChildFeelerManager.ClearList(this, currentSize);
					Object.Destroy(base.gameObject);
					return;
				}
				isMove = false;
				isStay = true;
				isStayMove = true;
				stayWaitTime = Random.Range(stayWaitTimeMin, stayWaitTimeMax);
			}
		}
		else if (isStay && !isRotateEnd)
		{
			Quaternion b3 = Quaternion.Euler(currentRotation, -90f, 90f);
			base.transform.rotation = Quaternion.Slerp(base.transform.rotation, b3, Time.deltaTime * moveRotationSpeed);
			if (Quaternion.Angle(base.transform.rotation, b3) < 1f)
			{
				isRotateEnd = true;
				_animancer.Play(growClip, feedTime);
				GetStayPosition();
			}
		}
		else if (isStayMove && stayWaitTime < 0f)
		{
			stayPositionCurrent = stayPosition;
			calcPosition = stayPosition;
			Vector3 normalized2 = (calcPosition - base.transform.position).normalized;
			base.transform.position += normalized2 * stayMoveSpeed * Time.deltaTime * conceiveParameter;
			Quaternion b4 = Quaternion.Euler(currentRotation, -90f, 90f);
			base.transform.rotation = Quaternion.Slerp(base.transform.rotation, b4, Time.deltaTime * stayMoveRotationSpeed);
			if (Vector3.Distance(base.transform.position, calcPosition) < 0.02f)
			{
				GetStayPosition();
				stayWaitTime = Random.Range(stayWaitTimeMin, stayWaitTimeMax);
			}
		}
	}

	public void GetMovePosition()
	{
		isStayMove = false;
		movePosition = _uterusChildFeelerManager.GetMovePoint();
		currentRotation = Random.Range(0f, 360f);
		conceiveParameter = _uterusChildFeelerManager.conceiveParameter;
	}

	public void GetStayPosition()
	{
		stayPosition = _uterusChildFeelerManager.GetMovePoint();
		currentRotation = Random.Range(0f, 360f);
		conceiveParameter = _uterusChildFeelerManager.conceiveParameter;
		stayMoveSpeed = Random.Range(stayMoveSpeedMin, stayMoveSpeedMax);
	}

	public void GetBirthPosition()
	{
		isBirth = true;
		isStay = false;
		isStayMove = false;
		movePosition = birthPosition;
		_collider.enabled = true;
	}
}
