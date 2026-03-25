using System.Collections.Generic;
using UnityEngine;

public class SkillGUIDataBase : MonoBehaviour
{
	public static SkillGUIDataBase instance;

	public List<SkillData> _skillDataList;

	public List<SkillData> _skillDataListTmp;

	public QuickSkillGUI _quickSkillGUI;

	public SkillGUIAnimationConrollObject _currentSkillGUIAnimationConrollObject;

	[Header("Icon Button")]
	public List<SkillGUIButton> _skillGUIButton;

	[Header("Icon Management")]
	public List<bool> _skillEnableList;

	public List<bool> _skillLockList;

	public List<float> _skillCurrentTimeList;

	public List<float> _skillCoolTimeList;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
	}

	private void Start()
	{
		_skillEnableList.Clear();
		_skillLockList.Clear();
		_skillCurrentTimeList.Clear();
		_skillCoolTimeList.Clear();
		for (int i = 0; i < _skillDataList.Count; i++)
		{
			_skillEnableList.Add(item: false);
			_skillLockList.Add(item: false);
			_skillCurrentTimeList.Add(0f);
			_skillCoolTimeList.Add(1f);
		}
		_skillLockList[39] = true;
		_currentSkillGUIAnimationConrollObject.ChangeSkill(null);
		_quickSkillGUI.ChangeSkill(-1);
	}

	private void LateUpdate()
	{
		for (int i = 0; i < _skillCurrentTimeList.Count; i++)
		{
			if (_skillCurrentTimeList[i] > 0f)
			{
				_skillCurrentTimeList[i] -= Time.deltaTime;
				if (_skillCurrentTimeList[i] < 0f)
				{
					_skillCurrentTimeList[i] = 0f;
				}
			}
		}
	}

	public void SetEnable(int id, bool value)
	{
		_skillEnableList[id] = value;
	}

	public void SetLock(int id, bool value)
	{
		_skillLockList[id] = value;
	}

	public void SetCoolTime(int id, float time)
	{
		_skillCurrentTimeList[id] = time;
		_skillCoolTimeList[id] = time;
	}

	public void ResetIcon()
	{
		for (int i = 0; i < _skillGUIButton.Count; i++)
		{
			_skillGUIButton[i].ResetIcon();
		}
	}

	public void MouseCoolTime(float time)
	{
		_skillCurrentTimeList[1] = time;
		_skillCurrentTimeList[2] = time;
		_skillCurrentTimeList[3] = time;
		_skillCurrentTimeList[10] = time;
		_skillCurrentTimeList[11] = time;
		_skillCurrentTimeList[12] = time;
		_skillCurrentTimeList[13] = time;
		_skillCoolTimeList[1] = time;
		_skillCoolTimeList[2] = time;
		_skillCoolTimeList[3] = time;
		_skillCoolTimeList[10] = time;
		_skillCoolTimeList[11] = time;
		_skillCoolTimeList[12] = time;
		_skillCoolTimeList[13] = time;
	}

	public void GimmickCoolTime(float time)
	{
		_skillCurrentTimeList[20] = time;
		_skillCurrentTimeList[21] = time;
		_skillCurrentTimeList[22] = time;
		_skillCurrentTimeList[23] = time;
		_skillCurrentTimeList[24] = time;
		_skillCurrentTimeList[25] = time;
		_skillCurrentTimeList[30] = time;
		_skillCurrentTimeList[31] = time;
		_skillCurrentTimeList[32] = time;
		_skillCurrentTimeList[33] = time;
		_skillCurrentTimeList[34] = time;
		_skillCurrentTimeList[39] = time;
		_skillCoolTimeList[20] = time;
		_skillCoolTimeList[21] = time;
		_skillCoolTimeList[22] = time;
		_skillCoolTimeList[23] = time;
		_skillCoolTimeList[24] = time;
		_skillCoolTimeList[25] = time;
		_skillCoolTimeList[30] = time;
		_skillCoolTimeList[31] = time;
		_skillCoolTimeList[32] = time;
		_skillCoolTimeList[33] = time;
		_skillCoolTimeList[34] = time;
		_skillCoolTimeList[39] = time;
	}

	public void GimmickReset()
	{
		_skillEnableList[20] = false;
		_skillEnableList[21] = false;
		_skillEnableList[22] = false;
		_skillEnableList[23] = false;
		_skillEnableList[24] = false;
		_skillEnableList[25] = false;
		_skillEnableList[30] = false;
		_skillEnableList[31] = false;
		_skillEnableList[32] = false;
		_skillEnableList[33] = false;
		_skillEnableList[34] = false;
		_skillEnableList[35] = false;
		LockGimmick(value: false);
		LockFuck(value: false);
		_skillLockList[42] = false;
		_skillLockList[50] = false;
		_skillLockList[51] = false;
		_skillLockList[52] = false;
		_skillLockList[60] = false;
		_skillLockList[61] = false;
		_skillLockList[65] = false;
		_skillLockList[70] = false;
		_skillLockList[71] = false;
		_skillLockList[75] = false;
		if (_skillLockList[80])
		{
			_skillLockList[80] = false;
		}
		if (_skillLockList[81])
		{
			_skillLockList[81] = false;
		}
		if (_skillLockList[85])
		{
			_skillLockList[85] = false;
		}
		_skillLockList[58] = false;
		_skillLockList[59] = false;
		if (_skillLockList[90])
		{
			_skillLockList[90] = false;
		}
		if (_skillLockList[91])
		{
			_skillLockList[91] = false;
		}
		if (_skillLockList[95])
		{
			_skillLockList[95] = false;
		}
	}

	public void GimmickEat(bool value, float time)
	{
		if (value)
		{
			_currentSkillGUIAnimationConrollObject.ChangeSkill(_skillDataList[20]);
			_quickSkillGUI.ChangeSkill(20);
			_skillCurrentTimeList[20] = time;
			_skillCoolTimeList[20] = time;
			_skillEnableList[20] = true;
			_skillLockList[21] = true;
			_skillLockList[22] = true;
			_skillLockList[23] = true;
			_skillLockList[24] = true;
			_skillLockList[25] = true;
			DisableAll(value: false, time);
			LockFuck(value: true);
			_skillLockList[70] = true;
			_skillLockList[71] = true;
			_skillLockList[75] = true;
		}
		else
		{
			_currentSkillGUIAnimationConrollObject.ChangeSkill(null);
			_quickSkillGUI.ChangeSkill(-1);
			_skillCurrentTimeList[20] = time;
			_skillCoolTimeList[20] = time;
			DisableAll(value: true, time);
		}
		FeelerCoolTime(time);
	}

	public void GimmickHorseRide(bool value, float time)
	{
		if (value)
		{
			_currentSkillGUIAnimationConrollObject.ChangeSkill(_skillDataList[21]);
			_quickSkillGUI.ChangeSkill(21);
			_skillCurrentTimeList[21] = time;
			_skillCoolTimeList[21] = time;
			_skillEnableList[21] = true;
			_skillLockList[20] = true;
			_skillLockList[22] = true;
			_skillLockList[23] = true;
			_skillLockList[24] = true;
			_skillLockList[25] = true;
			DisableAll(value: false, time);
			LockFuck(value: true);
			_skillLockList[58] = true;
			_skillLockList[59] = true;
		}
		else
		{
			_currentSkillGUIAnimationConrollObject.ChangeSkill(null);
			_quickSkillGUI.ChangeSkill(-1);
			_skillCurrentTimeList[21] = time;
			_skillCoolTimeList[21] = time;
			DisableAll(value: true, time);
			_skillLockList[80] = true;
			_skillLockList[81] = true;
			_skillLockList[85] = true;
			_skillLockList[58] = true;
			_skillLockList[59] = true;
			_skillLockList[90] = true;
			_skillLockList[91] = true;
			_skillLockList[95] = true;
		}
		FeelerCoolTime(time);
	}

	public void GimmickLimbHold(bool value, float time)
	{
		if (value)
		{
			_currentSkillGUIAnimationConrollObject.ChangeSkill(_skillDataList[22]);
			_quickSkillGUI.ChangeSkill(22);
			_skillCurrentTimeList[22] = time;
			_skillCoolTimeList[22] = time;
			_skillEnableList[22] = true;
			_skillLockList[20] = true;
			_skillLockList[21] = true;
			_skillLockList[23] = true;
			_skillLockList[24] = true;
			_skillLockList[25] = true;
			DisableAll(value: false, time);
			LockFuck(value: true);
		}
		else
		{
			_currentSkillGUIAnimationConrollObject.ChangeSkill(null);
			_quickSkillGUI.ChangeSkill(-1);
			_skillCurrentTimeList[22] = time;
			_skillCoolTimeList[22] = time;
			DisableAll(value: true, time);
		}
		FeelerCoolTime(time);
	}

	public void GimmickWartBed(bool value, float time)
	{
		if (value)
		{
			_currentSkillGUIAnimationConrollObject.ChangeSkill(_skillDataList[23]);
			_quickSkillGUI.ChangeSkill(23);
			_skillCurrentTimeList[23] = time;
			_skillCoolTimeList[23] = time;
			_skillEnableList[23] = true;
			_skillLockList[20] = true;
			_skillLockList[21] = true;
			_skillLockList[22] = true;
			_skillLockList[24] = true;
			_skillLockList[25] = true;
			DisableAll(value: false, time);
			LockFuck(value: true);
			_skillLockList[42] = true;
		}
		else
		{
			_currentSkillGUIAnimationConrollObject.ChangeSkill(null);
			_quickSkillGUI.ChangeSkill(-1);
			_skillCurrentTimeList[23] = time;
			_skillCoolTimeList[23] = time;
			DisableAll(value: true, time);
		}
		FeelerCoolTime(time);
	}

	public void GimmickWallHip(bool value, float time)
	{
		if (value)
		{
			_currentSkillGUIAnimationConrollObject.ChangeSkill(_skillDataList[24]);
			_quickSkillGUI.ChangeSkill(24);
			_skillCurrentTimeList[24] = time;
			_skillCoolTimeList[24] = time;
			_skillEnableList[24] = true;
			_skillLockList[20] = true;
			_skillLockList[21] = true;
			_skillLockList[22] = true;
			_skillLockList[23] = true;
			_skillLockList[25] = true;
			DisableAll(value: false, time);
			LockFuck(value: true);
		}
		else
		{
			_currentSkillGUIAnimationConrollObject.ChangeSkill(null);
			_quickSkillGUI.ChangeSkill(-1);
			_skillCurrentTimeList[24] = time;
			_skillCoolTimeList[24] = time;
			DisableAll(value: true, time);
		}
		FeelerCoolTime(time);
	}

	public void GimmickPillarBind(bool value, float time)
	{
		if (value)
		{
			_currentSkillGUIAnimationConrollObject.ChangeSkill(_skillDataList[25]);
			_quickSkillGUI.ChangeSkill(25);
			_skillCurrentTimeList[25] = time;
			_skillCoolTimeList[25] = time;
			_skillEnableList[25] = true;
			_skillLockList[20] = true;
			_skillLockList[21] = true;
			_skillLockList[22] = true;
			_skillLockList[23] = true;
			_skillLockList[24] = true;
			DisableAll(value: false, time);
			_skillLockList[42] = true;
			LockFuck(value: true);
		}
		else
		{
			_currentSkillGUIAnimationConrollObject.ChangeSkill(null);
			_quickSkillGUI.ChangeSkill(-1);
			_skillCurrentTimeList[25] = time;
			_skillCoolTimeList[25] = time;
			DisableAll(value: true, time);
		}
		FeelerCoolTime(time);
	}

	public void LockGimmick(bool value)
	{
		_skillLockList[20] = value;
		_skillLockList[21] = value;
		_skillLockList[22] = value;
		_skillLockList[23] = value;
		_skillLockList[24] = value;
		_skillLockList[25] = value;
	}

	public void FeelerCoolTime(float time)
	{
		_skillCurrentTimeList[60] = time;
		_skillCurrentTimeList[61] = time;
		_skillCurrentTimeList[65] = time;
		_skillCurrentTimeList[70] = time;
		_skillCurrentTimeList[71] = time;
		_skillCurrentTimeList[75] = time;
		_skillCurrentTimeList[80] = time;
		_skillCurrentTimeList[81] = time;
		_skillCurrentTimeList[85] = time;
		_skillCurrentTimeList[88] = time;
		_skillCurrentTimeList[58] = time;
		_skillCurrentTimeList[59] = time;
		_skillCurrentTimeList[90] = time;
		_skillCurrentTimeList[91] = time;
		_skillCurrentTimeList[95] = time;
		_skillCurrentTimeList[98] = time;
		_skillCoolTimeList[60] = time;
		_skillCoolTimeList[61] = time;
		_skillCoolTimeList[65] = time;
		_skillCoolTimeList[70] = time;
		_skillCoolTimeList[71] = time;
		_skillCoolTimeList[75] = time;
		_skillCoolTimeList[80] = time;
		_skillCoolTimeList[81] = time;
		_skillCoolTimeList[85] = time;
		_skillCoolTimeList[88] = time;
		_skillCoolTimeList[58] = time;
		_skillCoolTimeList[59] = time;
		_skillCoolTimeList[90] = time;
		_skillCoolTimeList[91] = time;
		_skillCoolTimeList[95] = time;
		_skillCoolTimeList[98] = time;
	}

	public void GimmickBackFuck(bool value, float time)
	{
		if (value)
		{
			_currentSkillGUIAnimationConrollObject.ChangeSkill(_skillDataList[30]);
			_quickSkillGUI.ChangeSkill(30);
			GimmickFuckCoolTime(time);
			_skillEnableList[30] = true;
			_skillEnableList[31] = false;
			_skillEnableList[32] = false;
			_skillEnableList[33] = false;
			_skillEnableList[34] = false;
			_skillEnableList[35] = false;
			DisableAll(value: false, time);
			LockGimmick(value: true);
			_skillLockList[42] = true;
			_skillLockList[50] = true;
			_skillLockList[51] = true;
			_skillLockList[52] = true;
			_skillLockList[58] = true;
			_skillLockList[59] = true;
		}
		else
		{
			_currentSkillGUIAnimationConrollObject.ChangeSkill(null);
			_quickSkillGUI.ChangeSkill(-1);
			GimmickFuckCoolTime(time);
			DisableAll(value: true, time);
			_skillLockList[58] = true;
			_skillLockList[59] = true;
			_skillLockList[90] = true;
			_skillLockList[91] = true;
			_skillLockList[95] = true;
		}
	}

	public void GimmickFrontFuck(bool value, float time)
	{
		if (value)
		{
			_currentSkillGUIAnimationConrollObject.ChangeSkill(_skillDataList[31]);
			_quickSkillGUI.ChangeSkill(31);
			GimmickFuckCoolTime(time);
			_skillEnableList[30] = false;
			_skillEnableList[31] = true;
			_skillEnableList[32] = false;
			_skillEnableList[33] = false;
			_skillEnableList[34] = false;
			_skillEnableList[35] = false;
			DisableAll(value: false, time);
			LockGimmick(value: true);
			_skillLockList[42] = true;
			_skillLockList[50] = true;
			_skillLockList[51] = true;
			_skillLockList[52] = true;
			_skillLockList[58] = true;
			_skillLockList[59] = true;
		}
		else
		{
			_currentSkillGUIAnimationConrollObject.ChangeSkill(null);
			_quickSkillGUI.ChangeSkill(-1);
			GimmickFuckCoolTime(time);
			DisableAll(value: true, time);
			_skillLockList[58] = true;
			_skillLockList[59] = true;
			_skillLockList[90] = true;
			_skillLockList[91] = true;
			_skillLockList[95] = true;
		}
	}

	public void GimmickRideFuck(bool value, float time)
	{
		if (value)
		{
			_currentSkillGUIAnimationConrollObject.ChangeSkill(_skillDataList[32]);
			_quickSkillGUI.ChangeSkill(32);
			GimmickFuckCoolTime(time);
			_skillEnableList[30] = false;
			_skillEnableList[31] = false;
			_skillEnableList[32] = true;
			_skillEnableList[33] = false;
			_skillEnableList[34] = false;
			_skillEnableList[35] = false;
			DisableAll(value: false, time);
			LockGimmick(value: true);
			_skillLockList[42] = true;
			_skillLockList[50] = true;
			_skillLockList[51] = true;
			_skillLockList[52] = true;
			_skillLockList[58] = true;
			_skillLockList[59] = true;
		}
		else
		{
			_currentSkillGUIAnimationConrollObject.ChangeSkill(null);
			_quickSkillGUI.ChangeSkill(-1);
			GimmickFuckCoolTime(time);
			DisableAll(value: true, time);
			_skillLockList[58] = true;
			_skillLockList[59] = true;
			_skillLockList[90] = true;
			_skillLockList[91] = true;
			_skillLockList[95] = true;
		}
	}

	public void GimmickLiftFuck(bool value, float time)
	{
		if (value)
		{
			_currentSkillGUIAnimationConrollObject.ChangeSkill(_skillDataList[33]);
			_quickSkillGUI.ChangeSkill(33);
			GimmickFuckCoolTime(time);
			_skillEnableList[30] = false;
			_skillEnableList[31] = false;
			_skillEnableList[32] = false;
			_skillEnableList[33] = true;
			_skillEnableList[34] = false;
			_skillEnableList[35] = false;
			DisableAll(value: false, time);
			LockGimmick(value: true);
			_skillLockList[42] = true;
			_skillLockList[50] = true;
			_skillLockList[51] = true;
			_skillLockList[52] = true;
			_skillLockList[58] = true;
			_skillLockList[59] = true;
		}
		else
		{
			_currentSkillGUIAnimationConrollObject.ChangeSkill(null);
			_quickSkillGUI.ChangeSkill(-1);
			GimmickFuckCoolTime(time);
			DisableAll(value: true, time);
			_skillLockList[58] = true;
			_skillLockList[59] = true;
			_skillLockList[90] = true;
			_skillLockList[91] = true;
			_skillLockList[95] = true;
		}
	}

	public void GimmickSideFuck(bool value, float time)
	{
		if (value)
		{
			_currentSkillGUIAnimationConrollObject.ChangeSkill(_skillDataList[34]);
			_quickSkillGUI.ChangeSkill(34);
			GimmickFuckCoolTime(time);
			_skillEnableList[30] = false;
			_skillEnableList[31] = false;
			_skillEnableList[32] = false;
			_skillEnableList[33] = false;
			_skillEnableList[34] = true;
			_skillEnableList[35] = false;
			DisableAll(value: false, time);
			LockGimmick(value: true);
			_skillLockList[42] = true;
			_skillLockList[50] = true;
			_skillLockList[51] = true;
			_skillLockList[52] = true;
			_skillLockList[58] = true;
			_skillLockList[59] = true;
		}
		else
		{
			_currentSkillGUIAnimationConrollObject.ChangeSkill(null);
			_quickSkillGUI.ChangeSkill(-1);
			GimmickFuckCoolTime(time);
			DisableAll(value: true, time);
			_skillLockList[58] = true;
			_skillLockList[59] = true;
			_skillLockList[90] = true;
			_skillLockList[91] = true;
			_skillLockList[95] = true;
		}
	}

	public void GimmickDoggyFuck(bool value, float time)
	{
		if (value)
		{
			_currentSkillGUIAnimationConrollObject.ChangeSkill(_skillDataList[35]);
			_quickSkillGUI.ChangeSkill(35);
			GimmickFuckCoolTime(time);
			_skillEnableList[30] = false;
			_skillEnableList[31] = false;
			_skillEnableList[32] = false;
			_skillEnableList[33] = false;
			_skillEnableList[34] = false;
			_skillEnableList[35] = true;
			DisableAll(value: false, time);
			LockGimmick(value: true);
			_skillLockList[42] = true;
			_skillLockList[50] = true;
			_skillLockList[51] = true;
			_skillLockList[52] = true;
			_skillLockList[58] = true;
			_skillLockList[59] = true;
		}
		else
		{
			_currentSkillGUIAnimationConrollObject.ChangeSkill(null);
			_quickSkillGUI.ChangeSkill(-1);
			GimmickFuckCoolTime(time);
			DisableAll(value: true, time);
			_skillLockList[58] = true;
			_skillLockList[59] = true;
			_skillLockList[90] = true;
			_skillLockList[91] = true;
			_skillLockList[95] = true;
		}
	}

	public void GimmickFuckCoolTime(float time)
	{
		_skillCurrentTimeList[30] = time;
		_skillCurrentTimeList[31] = time;
		_skillCurrentTimeList[32] = time;
		_skillCurrentTimeList[33] = time;
		_skillCurrentTimeList[34] = time;
		_skillCurrentTimeList[35] = time;
		_skillCoolTimeList[30] = time;
		_skillCoolTimeList[31] = time;
		_skillCoolTimeList[32] = time;
		_skillCoolTimeList[33] = time;
		_skillCoolTimeList[34] = time;
		_skillCoolTimeList[35] = time;
		_skillCurrentTimeList[90] = time;
		_skillCurrentTimeList[91] = time;
		_skillCurrentTimeList[95] = time;
		_skillCoolTimeList[90] = time;
		_skillCoolTimeList[91] = time;
		_skillCoolTimeList[95] = time;
	}

	public void LockFuck(bool value)
	{
		_skillLockList[30] = value;
		_skillLockList[31] = value;
		_skillLockList[32] = value;
		_skillLockList[33] = value;
		_skillLockList[34] = value;
		_skillLockList[35] = value;
	}

	public void DisableAll(bool value, float time)
	{
		if (value)
		{
			_skillCurrentTimeList[39] = time;
			_skillCoolTimeList[39] = time;
			_skillLockList[39] = true;
		}
		else
		{
			_skillCurrentTimeList[39] = time;
			_skillCoolTimeList[39] = time;
			_skillLockList[39] = false;
		}
	}
}
