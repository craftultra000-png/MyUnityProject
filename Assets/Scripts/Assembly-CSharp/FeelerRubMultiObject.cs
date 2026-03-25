using UnityEngine;

public class FeelerRubMultiObject : MonoBehaviour
{
	public CameraRaycastManager _cameraRaycastManager;

	public FeelerRubObject _feelerRubObject;

	[Header("Status")]
	public bool isRub;

	[Header("Target")]
	public Transform targetObject;

	public Transform shotStocker;

	[Header("Transform")]
	public float adjustTarget = 0.07f;

	public Vector3 defaultRotation = new Vector3(90f, 0f, 0f);

	[Header("Se")]
	public AudioClip missSe;

	public void Rub(int skillID, int num)
	{
		Debug.LogError("Check0");
		if (num == 0 && _cameraRaycastManager.isRubA)
		{
			_cameraRaycastManager.RecheckRaycast();
			if (_cameraRaycastManager.hitTarget != null && _cameraRaycastManager.aimObject.activeSelf)
			{
				RubSet(skillID, value: true);
				AttachBody(_cameraRaycastManager.aimTransform, _cameraRaycastManager.hitTarget);
				SetDamage(_cameraRaycastManager.hitTarget);
			}
			else
			{
				EffectSeManager.instance.PlaySe(missSe);
				RubSet(skillID, value: false);
			}
		}
		else if (num == 1 && _cameraRaycastManager.isRubB)
		{
			Debug.LogError("Check1");
			_cameraRaycastManager.RecheckRaycast();
			if (_cameraRaycastManager.hitTarget != null && _cameraRaycastManager.aimObject.activeSelf)
			{
				Debug.LogError("Check2");
				RubSet(skillID, value: true);
				AttachBody(_cameraRaycastManager.aimTransform, _cameraRaycastManager.hitTarget);
				SetDamage(_cameraRaycastManager.hitTarget);
				Debug.LogError("Check3");
			}
			else
			{
				EffectSeManager.instance.PlaySe(missSe);
				RubSet(skillID, value: false);
			}
		}
		else if (num == 2 && _cameraRaycastManager.isRubC)
		{
			_cameraRaycastManager.RecheckRaycast();
			if (_cameraRaycastManager.hitTarget != null && _cameraRaycastManager.aimObject.activeSelf)
			{
				RubSet(skillID, value: true);
				AttachBody(_cameraRaycastManager.aimTransform, _cameraRaycastManager.hitTarget);
				SetDamage(_cameraRaycastManager.hitTarget);
			}
			else
			{
				EffectSeManager.instance.PlaySe(missSe);
				RubSet(skillID, value: false);
			}
		}
		else if (num == 3 && _cameraRaycastManager.isRubD)
		{
			_cameraRaycastManager.RecheckRaycast();
			if (_cameraRaycastManager.hitTarget != null && _cameraRaycastManager.aimObject.activeSelf)
			{
				RubSet(skillID, value: true);
				AttachBody(_cameraRaycastManager.aimTransform, _cameraRaycastManager.hitTarget);
				SetDamage(_cameraRaycastManager.hitTarget);
			}
			else
			{
				EffectSeManager.instance.PlaySe(missSe);
				RubSet(skillID, value: false);
			}
		}
		else
		{
			RubSet(skillID, value: false);
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

	public void RubSet(int skillID, bool value)
	{
		isRub = value;
		SkillGUIDataBase.instance.SetEnable(skillID, isRub);
		if (isRub)
		{
			_feelerRubObject.isAim = isRub;
		}
		else if (!isRub)
		{
			_feelerRubObject.isAim = isRub;
		}
	}

	public void RubAimA(bool value)
	{
		_cameraRaycastManager.SetMakerRubA(value);
	}

	public void RubAimB(bool value)
	{
		_cameraRaycastManager.SetMakerRubB(value);
	}

	public void RubAimC(bool value)
	{
		_cameraRaycastManager.SetMakerRubC(value);
	}

	public void RubAimD(bool value)
	{
		_cameraRaycastManager.SetMakerRubD(value);
	}

	public void SetDamage(Transform other)
	{
		if ((bool)other.gameObject.GetComponent<CharacterColliderObject>())
		{
			_feelerRubObject.damageCollider = other.gameObject.GetComponent<CharacterColliderObject>();
		}
	}
}
