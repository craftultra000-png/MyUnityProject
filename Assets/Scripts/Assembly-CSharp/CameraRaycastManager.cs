using UnityEngine;

public class CameraRaycastManager : MonoBehaviour
{
	public float raycastDistance = 5f;

	[Header("Status")]
	public bool isSpanking;

	public bool isSyringe;

	public bool isSuck;

	[Space]
	public bool isRubA;

	public bool isRubB;

	public bool isRubC;

	public bool isRubD;

	[Space]
	public bool isBite;

	public bool isBiteA;

	public bool isBiteB;

	public bool isBiteC;

	public bool isBiteD;

	[Space]
	public bool isLickA;

	public bool isLickB;

	public bool isLickC;

	public bool isLickD;

	[Space]
	public bool isActiveSpanking;

	public bool isActiveSyringe;

	public bool isActiveSuck;

	[Space]
	public bool isActiveRubA;

	public bool isActiveRubB;

	public bool isActiveRubC;

	public bool isActiveRubD;

	[Space]
	public bool isActiveBite;

	public bool isActiveBiteA;

	public bool isActiveBiteB;

	public bool isActiveBiteC;

	public bool isActiveBiteD;

	[Space]
	public bool isActiveLickA;

	public bool isActiveLickB;

	public bool isActiveLickC;

	public bool isActiveLickD;

	[Header("Aim Object")]
	public GameObject aimObject;

	public Transform aimTransform;

	public int layerMask;

	[Header("Aim GUI")]
	public RectTransform aimMarker;

	public RectTransform stageGUI;

	public Camera subCamera;

	public GameObject spankingMaker;

	public GameObject syringeMaker;

	public GameObject suckMaker;

	[Space]
	public GameObject rubAMaker;

	public GameObject rubBMaker;

	public GameObject rubCMaker;

	public GameObject rubDMaker;

	[Space]
	public GameObject biteMaker;

	public GameObject biteAMaker;

	public GameObject biteBMaker;

	public GameObject biteCMaker;

	public GameObject biteDMaker;

	[Space]
	public GameObject lickAMaker;

	public GameObject lickBMaker;

	public GameObject lickCMaker;

	public GameObject lickDMaker;

	[Header("Target")]
	public Transform hitTarget;

	public Transform hitTargetPrevious;

	[Header("CoolTime")]
	public float coolTimeSpanking;

	public float coolTimeSyringe;

	public float coolTimeSuck;

	public float coolTimeBite;

	[Header("Calc")]
	public float lerpSpeed = 5f;

	public float hitTime;

	public float hitTimeMax = 3f;

	public Vector3 targetPosition;

	[Header("Distance")]
	public float distance;

	public float distanceScale;

	public float distanceThreshold = 0.5f;

	public AnimationCurve distanceCurve;

	private void Start()
	{
		aimObject.SetActive(value: false);
		aimMarker.gameObject.SetActive(value: false);
		spankingMaker.SetActive(value: false);
		syringeMaker.SetActive(value: false);
		suckMaker.SetActive(value: false);
		rubAMaker.SetActive(value: false);
		rubBMaker.SetActive(value: false);
		rubCMaker.SetActive(value: false);
		rubDMaker.SetActive(value: false);
		biteMaker.SetActive(value: false);
		biteAMaker.SetActive(value: false);
		biteBMaker.SetActive(value: false);
		biteCMaker.SetActive(value: false);
		biteDMaker.SetActive(value: false);
		lickAMaker.SetActive(value: false);
		lickBMaker.SetActive(value: false);
		lickCMaker.SetActive(value: false);
		lickDMaker.SetActive(value: false);
		layerMask = 1;
		hitTime = -1f;
	}

	private void FixedUpdate()
	{
		if (coolTimeSpanking >= 0f)
		{
			coolTimeSpanking -= Time.deltaTime;
		}
		if (coolTimeSyringe >= 0f)
		{
			coolTimeSyringe -= Time.deltaTime;
		}
		if (coolTimeSuck >= 0f)
		{
			coolTimeSuck -= Time.deltaTime;
		}
		if (coolTimeBite >= 0f)
		{
			coolTimeBite -= Time.deltaTime;
		}
		if (isSpanking || isSyringe || isSuck || isRubA || isRubB || isRubC || isRubD || isBite || isBiteA || isBiteB || isBiteC || isBiteD || isLickA || isLickB || isLickC || isLickD)
		{
			Ray ray = new Ray(base.transform.position, base.transform.forward);
			if (Physics.Raycast(ray, out var hitInfo, raycastDistance, layerMask))
			{
				Debug.DrawLine(ray.origin, ray.origin + ray.direction * raycastDistance, Color.green);
				if (hitInfo.collider.CompareTag("Character"))
				{
					targetPosition = hitInfo.point;
					if (!isActiveSpanking && !isActiveSyringe && !isActiveSuck && !isActiveRubA && !isActiveRubB && !isActiveRubC && !isActiveRubD && !isActiveBite && !isActiveBiteA && !isActiveBiteB && !isActiveBiteC && !isActiveBiteD && !isActiveLickA && !isActiveLickB && !isActiveLickC && !isActiveLickD)
					{
						aimTransform.position = targetPosition;
					}
					aimTransform.position = Vector3.Lerp(aimTransform.position, targetPosition, Time.deltaTime * lerpSpeed);
					MoveMarker();
					hitTargetPrevious = hitTarget;
					hitTarget = hitInfo.transform;
					hitTime = hitTimeMax;
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
		if (hitTarget == null && hitTime >= 0f)
		{
			hitTime -= Time.deltaTime;
			if (hitTime <= 0f)
			{
				AimMarker(value: false);
				return;
			}
			aimTransform.position = Vector3.Lerp(aimTransform.position, targetPosition, Time.deltaTime * lerpSpeed);
			MoveMarker();
		}
	}

	public void RecheckRaycast()
	{
		Vector3 vector = aimTransform.position - base.transform.position;
		Ray ray = new Ray(base.transform.position, vector.normalized);
		if (Physics.Raycast(ray, out var hitInfo, raycastDistance, layerMask))
		{
			Debug.DrawLine(ray.origin, hitInfo.point, Color.yellow);
			float num = Vector3.Distance(hitInfo.point, aimTransform.position);
			if (hitInfo.collider.CompareTag("Character") && num <= distanceThreshold)
			{
				hitTarget = hitInfo.transform;
				return;
			}
			hitTarget = null;
			AimMarker(value: false);
		}
		else
		{
			hitTarget = null;
			AimMarker(value: false);
		}
	}

	public void SetMakerSpanking(bool value)
	{
		Debug.LogError("Spanking Maker: " + value);
		isSpanking = value;
		if (!value)
		{
			isActiveSpanking = false;
		}
		if (isSpanking && !spankingMaker.activeSelf)
		{
			spankingMaker.SetActive(value: true);
		}
		else
		{
			spankingMaker.SetActive(value: false);
		}
		MakerCheck();
	}

	public void SetMakerSyringe(bool value)
	{
		isSyringe = value;
		if (!value)
		{
			isActiveSyringe = false;
		}
		if (isSyringe && !syringeMaker.activeSelf)
		{
			syringeMaker.SetActive(value: true);
		}
		else
		{
			syringeMaker.SetActive(value: false);
		}
		MakerCheck();
	}

	public void SetMakerSuck(bool value)
	{
		isSuck = value;
		if (!value)
		{
			isActiveSuck = false;
		}
		if (isSuck && !suckMaker.activeSelf)
		{
			suckMaker.SetActive(value: true);
		}
		else
		{
			suckMaker.SetActive(value: false);
		}
		MakerCheck();
	}

	public void SetMakerRubA(bool value)
	{
		Debug.LogError("Rub MakerA: " + value);
		isRubA = value;
		if (!value)
		{
			isActiveRubA = false;
		}
		if (isRubA && !rubAMaker.activeSelf)
		{
			rubAMaker.SetActive(value: true);
		}
		else
		{
			rubAMaker.SetActive(value: false);
		}
		MakerCheck();
	}

	public void SetMakerRubB(bool value)
	{
		Debug.LogError("Rub MakerB: " + value);
		isRubB = value;
		if (!value)
		{
			isActiveRubB = false;
		}
		if (isRubB && !rubBMaker.activeSelf)
		{
			rubBMaker.SetActive(value: true);
		}
		else
		{
			rubBMaker.SetActive(value: false);
		}
		MakerCheck();
	}

	public void SetMakerRubC(bool value)
	{
		Debug.LogError("Rub MakerC: " + value);
		isRubC = value;
		if (!value)
		{
			isActiveRubC = false;
		}
		if (isRubC && !rubCMaker.activeSelf)
		{
			rubCMaker.SetActive(value: true);
		}
		else
		{
			rubCMaker.SetActive(value: false);
		}
		MakerCheck();
	}

	public void SetMakerRubD(bool value)
	{
		Debug.LogError("Rub MakerD: " + value);
		isRubD = value;
		if (!value)
		{
			isActiveRubD = false;
		}
		if (isRubD && !rubDMaker.activeSelf)
		{
			rubDMaker.SetActive(value: true);
		}
		else
		{
			rubDMaker.SetActive(value: false);
		}
		MakerCheck();
	}

	public void SetMakerBite(bool value)
	{
		Debug.LogError("Bite Maker: " + value);
		isBite = value;
		if (!value)
		{
			isActiveBite = false;
		}
		if (isBite && !biteMaker.activeSelf)
		{
			biteMaker.SetActive(value: true);
		}
		else
		{
			biteMaker.SetActive(value: false);
		}
		MakerCheck();
	}

	public void SetMakerBiteA(bool value)
	{
		Debug.LogError("Bite MakerA: " + value);
		isBiteA = value;
		if (!value)
		{
			isActiveBiteA = false;
		}
		if (isBiteA && !biteAMaker.activeSelf)
		{
			biteAMaker.SetActive(value: true);
		}
		else
		{
			biteAMaker.SetActive(value: false);
		}
		MakerCheck();
	}

	public void SetMakerBiteB(bool value)
	{
		Debug.LogError("Bite MakerB: " + value);
		isBiteB = value;
		if (!value)
		{
			isActiveBiteB = false;
		}
		if (isBiteB && !biteBMaker.activeSelf)
		{
			biteBMaker.SetActive(value: true);
		}
		else
		{
			biteBMaker.SetActive(value: false);
		}
		MakerCheck();
	}

	public void SetMakerBiteC(bool value)
	{
		Debug.LogError("Bite MakerC: " + value);
		isBiteC = value;
		if (!value)
		{
			isActiveBiteC = false;
		}
		if (isBiteC && !biteCMaker.activeSelf)
		{
			biteCMaker.SetActive(value: true);
		}
		else
		{
			biteCMaker.SetActive(value: false);
		}
		MakerCheck();
	}

	public void SetMakerBiteD(bool value)
	{
		Debug.LogError("Bite MakerD: " + value);
		isBiteD = value;
		if (!value)
		{
			isActiveBiteD = false;
		}
		if (isBiteD && !biteDMaker.activeSelf)
		{
			biteDMaker.SetActive(value: true);
		}
		else
		{
			biteDMaker.SetActive(value: false);
		}
		MakerCheck();
	}

	public void SetMakerLickA(bool value)
	{
		Debug.LogError("Lick MakerA");
		isLickA = value;
		if (!value)
		{
			isActiveLickA = false;
		}
		if (isLickA && !lickAMaker.activeSelf)
		{
			lickAMaker.SetActive(value: true);
		}
		else
		{
			lickAMaker.SetActive(value: false);
		}
		MakerCheck();
	}

	public void SetMakerLickB(bool value)
	{
		Debug.LogError("Lick MakerB");
		isLickB = value;
		if (!value)
		{
			isActiveLickB = false;
		}
		if (isLickB && !lickBMaker.activeSelf)
		{
			lickBMaker.SetActive(value: true);
		}
		else
		{
			lickBMaker.SetActive(value: false);
		}
		MakerCheck();
	}

	public void SetMakerLickC(bool value)
	{
		Debug.LogError("Lick MakerC");
		isLickC = value;
		if (!value)
		{
			isActiveLickC = false;
		}
		if (isLickC && !lickCMaker.activeSelf)
		{
			lickCMaker.SetActive(value: true);
		}
		else
		{
			lickCMaker.SetActive(value: false);
		}
		MakerCheck();
	}

	public void SetMakerLickD(bool value)
	{
		Debug.LogError("Lick MakerD");
		isLickD = value;
		if (!value)
		{
			isActiveLickD = false;
		}
		if (isLickD && !lickDMaker.activeSelf)
		{
			lickDMaker.SetActive(value: true);
		}
		else
		{
			lickDMaker.SetActive(value: false);
		}
		MakerCheck();
	}

	public void MakerCheck()
	{
		if (!isSpanking && !isSyringe && !isSuck && !isRubA && !isRubB && !isRubC && !isRubD && !isBite && !isBiteA && !isBiteB && !isBiteC && !isBiteD && !isLickA && !isLickB && !isLickC && !isLickD)
		{
			aimObject.SetActive(value: false);
			aimMarker.gameObject.SetActive(value: false);
			hitTarget = null;
			hitTime = -1f;
		}
	}

	public void MoveMarker()
	{
		Vector3 vector = Camera.main.WorldToScreenPoint(aimTransform.position);
		if ((vector.z > 0f && hitTarget != null) || (hitTarget == null && hitTime > 0f))
		{
			RectTransformUtility.ScreenPointToLocalPointInRectangle(stageGUI, vector, subCamera, out var localPoint);
			distance = Vector3.Distance(base.transform.position, aimTransform.position);
			if (distance <= distanceThreshold)
			{
				distanceScale = 1f;
			}
			else
			{
				float value = (distance - distanceThreshold) / (2f - distanceThreshold);
				distanceScale = Mathf.Lerp(1f, 0f, Mathf.Clamp01(value));
			}
			aimMarker.localScale = Vector3.one * distanceCurve.Evaluate(distanceScale);
			aimMarker.localPosition = localPoint;
			AimMarker(value: true);
		}
		else if (vector.z < 0f)
		{
			AimMarker(value: false);
		}
	}

	public void AimMarker(bool value)
	{
		if (value)
		{
			if (isSpanking && coolTimeSpanking <= 0f)
			{
				isActiveSpanking = true;
			}
			else
			{
				isActiveSpanking = false;
			}
			if (isSyringe && coolTimeSyringe <= 0f)
			{
				isActiveSyringe = true;
			}
			else
			{
				isActiveSyringe = false;
			}
			if (isSuck && coolTimeSuck <= 0f)
			{
				isActiveSuck = true;
			}
			else
			{
				isActiveSuck = false;
			}
			if (isBite && coolTimeBite <= 0f)
			{
				isActiveBite = true;
			}
			else
			{
				isActiveBite = false;
			}
			if (isActiveSpanking || isActiveSyringe || isActiveSuck || isRubA || isRubB || isRubC || isRubD || isActiveBite || isBiteA || isBiteB || isBiteC || isBiteD || isLickA || isLickB || isLickC || isLickD)
			{
				aimObject.SetActive(value: true);
				if (!aimMarker.gameObject.activeSelf)
				{
					aimMarker.gameObject.SetActive(value: true);
				}
				if (isActiveSpanking && !spankingMaker.activeSelf)
				{
					Debug.LogError("Spanking Maker");
					spankingMaker.SetActive(value: true);
				}
				if (isActiveSyringe && !syringeMaker.activeSelf)
				{
					syringeMaker.SetActive(value: true);
				}
				if (isActiveSuck && !suckMaker.activeSelf)
				{
					suckMaker.SetActive(value: true);
				}
				if (isRubA && !rubAMaker.activeSelf)
				{
					rubAMaker.SetActive(value: true);
				}
				if (isRubB && !rubBMaker.activeSelf)
				{
					rubBMaker.SetActive(value: true);
				}
				if (isRubC && !rubCMaker.activeSelf)
				{
					rubCMaker.SetActive(value: true);
				}
				if (isRubD && !rubDMaker.activeSelf)
				{
					rubDMaker.SetActive(value: true);
				}
				if (isActiveBite && !biteMaker.activeSelf)
				{
					Debug.LogError("Bite Maker");
					biteMaker.SetActive(value: true);
				}
				if (isBiteA && !biteAMaker.activeSelf)
				{
					biteAMaker.SetActive(value: true);
				}
				if (isBiteB && !biteBMaker.activeSelf)
				{
					biteBMaker.SetActive(value: true);
				}
				if (isBiteC && !biteCMaker.activeSelf)
				{
					biteCMaker.SetActive(value: true);
				}
				if (isBiteD && !biteDMaker.activeSelf)
				{
					biteDMaker.SetActive(value: true);
				}
				if (isLickA && !lickAMaker.activeSelf)
				{
					lickAMaker.SetActive(value: true);
				}
				if (isLickB && !lickBMaker.activeSelf)
				{
					lickBMaker.SetActive(value: true);
				}
				if (isLickC && !lickCMaker.activeSelf)
				{
					lickCMaker.SetActive(value: true);
				}
				if (isLickD && !lickDMaker.activeSelf)
				{
					lickDMaker.SetActive(value: true);
				}
			}
			else
			{
				aimObject.gameObject.SetActive(value: false);
				aimMarker.gameObject.SetActive(value: false);
				spankingMaker.SetActive(value: false);
				syringeMaker.SetActive(value: false);
				suckMaker.SetActive(value: false);
				rubAMaker.SetActive(value: false);
				rubBMaker.SetActive(value: false);
				rubCMaker.SetActive(value: false);
				rubDMaker.SetActive(value: false);
				biteMaker.SetActive(value: false);
				biteAMaker.SetActive(value: false);
				biteBMaker.SetActive(value: false);
				biteCMaker.SetActive(value: false);
				biteDMaker.SetActive(value: false);
				lickAMaker.SetActive(value: false);
				lickBMaker.SetActive(value: false);
				lickCMaker.SetActive(value: false);
				lickDMaker.SetActive(value: false);
			}
		}
		else
		{
			isActiveSpanking = false;
			isActiveSyringe = false;
			isActiveSuck = false;
			isActiveBite = false;
			isActiveRubA = false;
			isActiveRubB = false;
			isActiveRubC = false;
			isActiveRubD = false;
			isActiveBite = false;
			isActiveBiteA = false;
			isActiveBiteB = false;
			isActiveBiteC = false;
			isActiveBiteD = false;
			isActiveLickA = false;
			isActiveLickB = false;
			isActiveLickC = false;
			isActiveLickD = false;
			aimObject.SetActive(value: false);
			aimMarker.gameObject.SetActive(value: false);
			spankingMaker.SetActive(value: false);
			syringeMaker.SetActive(value: false);
			suckMaker.SetActive(value: false);
			rubAMaker.SetActive(value: false);
			rubBMaker.SetActive(value: false);
			rubCMaker.SetActive(value: false);
			rubDMaker.SetActive(value: false);
			biteMaker.SetActive(value: false);
			biteAMaker.SetActive(value: false);
			biteBMaker.SetActive(value: false);
			biteCMaker.SetActive(value: false);
			biteDMaker.SetActive(value: false);
			lickAMaker.SetActive(value: false);
			lickBMaker.SetActive(value: false);
			lickCMaker.SetActive(value: false);
			lickDMaker.SetActive(value: false);
			hitTarget = null;
			hitTime = -1f;
		}
	}
}
