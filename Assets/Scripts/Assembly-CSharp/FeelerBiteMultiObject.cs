using UnityEngine;

public class FeelerBiteMultiObject : MonoBehaviour
{
	public CameraRaycastManager _cameraRaycastManager;

	public FeelerBiteObject _feelerBiteObject;

	[Header("Status")]
	public bool isBite;

	[Header("Target")]
	public Transform targetObject;

	public Transform shotStocker;

	[Header("Transform")]
	public float adjustTarget = 0.07f;

	public Vector3 defaultRotation = new Vector3(90f, 0f, 0f);

	[Header("Se")]
	public AudioClip missSe;

	[Header("CoolTime")]
	public bool isCoolTime;

	public float coolTime;

	public float coolTimeMax = 2f;

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
	}

	public void Bite(int skillID, int num)
	{
		Debug.LogError("Bite");
		if (num == -1 && _cameraRaycastManager.isBite && _cameraRaycastManager.isActiveBite)
		{
			_cameraRaycastManager.RecheckRaycast();
			if (_cameraRaycastManager.hitTarget != null && _cameraRaycastManager.biteMaker.activeSelf)
			{
				SingleBiteSet(skillID, value: true);
				AttachBody(_cameraRaycastManager.aimTransform, _cameraRaycastManager.hitTarget);
				SetDamage(_cameraRaycastManager.hitTarget);
			}
			else
			{
				EffectSeManager.instance.PlaySe(missSe);
				BiteSet(skillID, value: false);
			}
		}
		else if (num == 0 && _cameraRaycastManager.isBiteA)
		{
			_cameraRaycastManager.RecheckRaycast();
			if (_cameraRaycastManager.hitTarget != null && _cameraRaycastManager.aimObject.activeSelf)
			{
				Debug.LogError("BiteA: " + _cameraRaycastManager.biteAMaker.activeSelf);
				BiteSet(skillID, value: true);
				AttachBody(_cameraRaycastManager.aimTransform, _cameraRaycastManager.hitTarget);
				SetDamage(_cameraRaycastManager.hitTarget);
			}
			else
			{
				EffectSeManager.instance.PlaySe(missSe);
				BiteSet(skillID, value: false);
			}
		}
		else if (num == 1 && _cameraRaycastManager.isBiteB)
		{
			_cameraRaycastManager.RecheckRaycast();
			if (_cameraRaycastManager.hitTarget != null && _cameraRaycastManager.aimObject.activeSelf)
			{
				BiteSet(skillID, value: true);
				AttachBody(_cameraRaycastManager.aimTransform, _cameraRaycastManager.hitTarget);
				SetDamage(_cameraRaycastManager.hitTarget);
			}
			else
			{
				EffectSeManager.instance.PlaySe(missSe);
				BiteSet(skillID, value: false);
			}
		}
		else if (num == 2 && _cameraRaycastManager.isBiteC)
		{
			_cameraRaycastManager.RecheckRaycast();
			if (_cameraRaycastManager.hitTarget != null && _cameraRaycastManager.aimObject.activeSelf)
			{
				BiteSet(skillID, value: true);
				AttachBody(_cameraRaycastManager.aimTransform, _cameraRaycastManager.hitTarget);
				SetDamage(_cameraRaycastManager.hitTarget);
			}
			else
			{
				EffectSeManager.instance.PlaySe(missSe);
				BiteSet(skillID, value: false);
			}
		}
		else if (num == 3 && _cameraRaycastManager.isBiteD)
		{
			_cameraRaycastManager.RecheckRaycast();
			if (_cameraRaycastManager.hitTarget != null && _cameraRaycastManager.aimObject.activeSelf)
			{
				BiteSet(skillID, value: true);
				AttachBody(_cameraRaycastManager.aimTransform, _cameraRaycastManager.hitTarget);
				SetDamage(_cameraRaycastManager.hitTargetPrevious);
			}
			else
			{
				EffectSeManager.instance.PlaySe(missSe);
				BiteSet(skillID, value: false);
			}
		}
		else
		{
			EffectSeManager.instance.PlaySe(missSe);
			BiteSet(skillID, value: false);
		}
	}

	public void AttachBody(Transform position, Transform target)
	{
		Vector3 normalized = (position.position - base.transform.position).normalized;
		Quaternion quaternion = Quaternion.LookRotation(normalized);
		targetObject.parent = target;
		targetObject.position = position.position - normalized * adjustTarget;
		targetObject.rotation = quaternion * Quaternion.Euler(defaultRotation);
	}

	public void SingleBiteSet(int skillID, bool value)
	{
		if (!isBite && !isCoolTime)
		{
			isBite = value;
			isCoolTime = true;
			coolTime = coolTimeMax;
			_cameraRaycastManager.coolTimeBite = coolTimeMax;
			SkillGUIDataBase.instance.SetCoolTime(skillID, coolTimeMax);
			if (isBite)
			{
				_feelerBiteObject.skillID = skillID;
				_feelerBiteObject.isAim = isBite;
			}
			else if (!isBite)
			{
				_feelerBiteObject.isAim = isBite;
				_feelerBiteObject.isBite = false;
			}
		}
	}

	public void BiteSet(int skillID, bool value)
	{
		isBite = value;
		SkillGUIDataBase.instance.SetEnable(skillID, isBite);
		if (isBite)
		{
			_feelerBiteObject.isAim = isBite;
		}
		else if (!isBite)
		{
			_feelerBiteObject.isAim = isBite;
			_feelerBiteObject.isBite = false;
		}
	}

	public void BiteAim(bool value)
	{
		_cameraRaycastManager.SetMakerBite(value);
	}

	public void BiteAimA(bool value)
	{
		_cameraRaycastManager.SetMakerBiteA(value);
	}

	public void BiteAimB(bool value)
	{
		_cameraRaycastManager.SetMakerBiteB(value);
	}

	public void BiteAimC(bool value)
	{
		_cameraRaycastManager.SetMakerBiteC(value);
	}

	public void BiteAimD(bool value)
	{
		_cameraRaycastManager.SetMakerBiteD(value);
	}

	public void SetDamage(Transform other)
	{
		if ((bool)other.gameObject.GetComponent<CharacterColliderObject>())
		{
			_feelerBiteObject.damageCollider = other.gameObject.GetComponent<CharacterColliderObject>();
		}
	}
}
