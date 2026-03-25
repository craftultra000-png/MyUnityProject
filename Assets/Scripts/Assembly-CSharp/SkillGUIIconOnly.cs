using UnityEngine;
using UnityEngine.UI;

public class SkillGUIIconOnly : MonoBehaviour
{
	public SkillGUIDataBase _skillGUIDataBase;

	[Header("Skill Data")]
	public SkillData _skillData;

	[Header("CoolTime")]
	public Image _skillFill;

	public int skillID;

	public float coolTime;

	private void Start()
	{
		_skillFill.fillAmount = 0f;
	}

	private void LateUpdate()
	{
		coolTime = _skillGUIDataBase._skillCurrentTimeList[skillID];
		if (coolTime >= 0f)
		{
			_skillFill.fillAmount = coolTime / _skillGUIDataBase._skillCoolTimeList[skillID];
		}
	}

	public void SetIcon(SkillData data)
	{
		_skillData = data;
		skillID = _skillData.skillID;
	}
}
