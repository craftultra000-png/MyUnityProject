using System.Collections.Generic;
using UnityEngine;

public class FeelerSyringeObject : MonoBehaviour
{
	public CameraRaycastManager _cameraRaycastManager;

	[Header("Target")]
	public Transform targetObject;

	public Transform shotStocker;

	[Header("Attack Type")]
	public string attackType = "Syringe";

	[Header("CoolTime")]
	public bool isCoolTime;

	public float coolTime;

	public float coolTimeMax = 1.5f;

	[Header("Effect")]
	public GameObject syringeObject;

	public SyringeObject syringeScript;

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

	public void Syringe(int skillID)
	{
		if (isCoolTime)
		{
			return;
		}
		if (_cameraRaycastManager.isActiveSyringe && (bool)_cameraRaycastManager.hitTargetPrevious)
		{
			_cameraRaycastManager.RecheckRaycast();
			if (_cameraRaycastManager.hitTarget != null)
			{
				GameObject gameObject = Object.Instantiate(syringeObject, base.transform.position, base.transform.rotation, shotStocker);
				syringeScript = gameObject.GetComponent<SyringeObject>();
				syringeScript.shotStocker = shotStocker;
				syringeScript.AttachBody(_cameraRaycastManager.aimTransform, _cameraRaycastManager.hitTargetPrevious);
				EffectSeManager.instance.PlaySe(shotSe[Random.Range(0, shotSe.Count)]);
				SetDamage(_cameraRaycastManager.hitTargetPrevious);
				isCoolTime = true;
				coolTime = coolTimeMax;
				_cameraRaycastManager.coolTimeSyringe = coolTimeMax;
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

	public void SyringeAim(bool value)
	{
		_cameraRaycastManager.SetMakerSyringe(value);
	}

	public void SetDamage(Transform other)
	{
		if ((bool)other.gameObject.GetComponent<CharacterColliderObject>())
		{
			other.gameObject.GetComponent<CharacterColliderObject>().HitData(attackType);
		}
		if ((bool)other.gameObject.GetComponent<CharacterColliderBelly>())
		{
			other.gameObject.GetComponent<CharacterColliderBelly>().HitPosition(_cameraRaycastManager.aimTransform.position);
		}
	}
}
