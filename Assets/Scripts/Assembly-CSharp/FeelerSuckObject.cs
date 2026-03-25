using System.Collections.Generic;
using UnityEngine;

public class FeelerSuckObject : MonoBehaviour
{
	public CameraRaycastManager _cameraRaycastManager;

	[Header("Target")]
	public Transform targetObject;

	public Transform shotStocker;

	[Header("Attack Type")]
	public string attackType = "Suck";

	[Header("CoolTime")]
	public bool isCoolTime;

	public float coolTime;

	public float coolTimeMax = 1.5f;

	[Header("Effect")]
	public GameObject suckObject;

	public SuckObject suckScript;

	[Header("Se")]
	public List<AudioClip> shotSe;

	public AudioClip missSe;

	private void Start()
	{
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
	}

	public void Suck(int skillID)
	{
		if (isCoolTime)
		{
			return;
		}
		if (_cameraRaycastManager.isActiveSuck && (bool)_cameraRaycastManager.hitTargetPrevious)
		{
			_cameraRaycastManager.RecheckRaycast();
			if (_cameraRaycastManager.hitTarget != null)
			{
				GameObject gameObject = Object.Instantiate(suckObject, base.transform.position, base.transform.rotation, shotStocker);
				suckScript = gameObject.GetComponent<SuckObject>();
				suckScript.shotStocker = shotStocker;
				suckScript.AttachBody(_cameraRaycastManager.aimTransform, _cameraRaycastManager.hitTargetPrevious);
				EffectSeManager.instance.PlaySe(shotSe[Random.Range(0, shotSe.Count)]);
				SetDamage(_cameraRaycastManager.hitTargetPrevious);
				isCoolTime = true;
				coolTime = coolTimeMax;
				_cameraRaycastManager.coolTimeSuck = coolTimeMax;
				SkillGUIDataBase.instance.SetCoolTime(skillID, coolTimeMax);
			}
			else
			{
				EffectSeManager.instance.PlaySe(missSe);
			}
		}
		else
		{
			EffectSeManager.instance.PlaySe(missSe);
		}
	}

	public void SuckAim(bool value)
	{
		_cameraRaycastManager.SetMakerSuck(value);
	}

	public void SetDamage(Transform other)
	{
		if ((bool)other.gameObject.GetComponent<CharacterColliderObject>())
		{
			other.gameObject.GetComponent<CharacterColliderObject>().HitData(attackType);
			suckScript.damageCollider = other.gameObject.GetComponent<CharacterColliderObject>();
		}
		if ((bool)other.gameObject.GetComponent<CharacterColliderBelly>())
		{
			other.gameObject.GetComponent<CharacterColliderBelly>().HitPosition(_cameraRaycastManager.aimTransform.position);
		}
	}
}
