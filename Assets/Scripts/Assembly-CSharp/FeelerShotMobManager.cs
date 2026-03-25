using System.Collections.Generic;
using UnityEngine;

public class FeelerShotMobManager : MonoBehaviour
{
	public List<FeelerShotMob> _feelerShotMob;

	[Header("Status")]
	public bool isAuto;

	public bool isShot;

	public float coolTime;

	public float coolTimeMax = 1f;

	private void LateUpdate()
	{
		if (isShot)
		{
			coolTime -= Time.deltaTime;
			if (coolTime < 0f)
			{
				isShot = false;
			}
		}
	}

	public void Shot(int skillID)
	{
		if (!isShot)
		{
			isShot = true;
			coolTime = coolTimeMax;
			SkillGUIDataBase.instance.SetCoolTime(skillID, coolTimeMax);
			for (int i = 0; i < _feelerShotMob.Count; i++)
			{
				_feelerShotMob[i].isVolley = true;
				_feelerShotMob[i].waitTime = 0f;
			}
		}
	}

	public void AutoShot(bool value)
	{
		isAuto = value;
		for (int i = 0; i < _feelerShotMob.Count; i++)
		{
			_feelerShotMob[i].isAuto = isAuto;
		}
	}
}
