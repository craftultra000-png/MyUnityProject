using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillGUIManager : MonoBehaviour
{
	public static SkillGUIManager instance;

	public FeelerControllerData _feelerControllerData;

	public SkillGUIDataBase _skillGUIDataBase;

	[Header("Debug")]
	public bool isDebug;

	[Header("Status")]
	public bool isInitalize;

	public bool isCamera;

	public bool isUseLock;

	public bool isDrag;

	[Header("Skill Page GUI")]
	public int page;

	public List<GameObject> skillPage;

	public List<TextMeshProUGUI> pageText;

	public List<ButtonTriggerGUI> pageButtonGUI;

	[Header("Off On GUI")]
	public SkillGUIInformation informationObject;

	public SkillGUIQuickSet quickSetObject;

	[Header("Skill Data")]
	public List<SkillData> _skillDataKey;

	public List<SkillData> _skillDataMouse;

	public SkillData _skillDataBukkake;

	private SkillData tmpSkillData;

	public List<int> _skillDataKeyNumber;

	public List<int> _skillDataMouseNumber;

	[Header("Preset Data")]
	public int presetNum;

	public List<SkillData> _skillPreset0Key;

	public List<SkillData> _skillPreset1Key;

	public List<SkillData> _skillPreset2Key;

	public List<SkillData> _skillPreset3Key;

	public List<SkillData> _skillPreset4Key;

	[Space]
	public List<SkillData> _skillPreset0Mouse;

	public List<SkillData> _skillPreset1Mouse;

	public List<SkillData> _skillPreset2Mouse;

	public List<SkillData> _skillPreset3Mouse;

	public List<SkillData> _skillPreset4Mouse;

	[Space]
	public List<int> _skillPreset0KeyNumber;

	public List<int> _skillPreset1KeyNumber;

	public List<int> _skillPreset2KeyNumber;

	public List<int> _skillPreset3KeyNumber;

	public List<int> _skillPreset4KeyNumber;

	[Space]
	public List<int> _skillPreset0MouseNumber;

	public List<int> _skillPreset1MouseNumber;

	public List<int> _skillPreset2MouseNumber;

	public List<int> _skillPreset3MouseNumber;

	public List<int> _skillPreset4MouseNumber;

	[Header("Preset Data")]
	public bool isCopyKey;

	public bool isCopyMouse;

	public List<SkillData> _skillCopyKey;

	public List<SkillData> _skillCopyMouse;

	[Space]
	public List<int> _skillCopyKeyNumber;

	public List<int> _skillCopyMouseNumber;

	[Header("Preset Icon")]
	public TextMeshProUGUI copyKeyText;

	public TextMeshProUGUI copyMouseText;

	public ButtonTriggerGUI copyKeyIconButton;

	public ButtonTriggerGUI copyMouseIconButton;

	[Header("Preset Icon")]
	public List<Image> presetIcon;

	public List<Image> presetIconSkill;

	public List<ButtonTriggerGUI> presetIconButton;

	public List<ButtonTriggerGUI> presetIconSkillButton;

	[Header("Button")]
	public List<SkillGUIButton> _skillGUIButtonKey;

	public List<SkillGUIButton> _skillGUIButtonMouse;

	public SkillGUIIconOnly _skillGUIButtonBukkake;

	[Header("Button Position Reset")]
	public List<SkillGUISkillTreeObject> _skillGUISkillTreeObject;

	[Header("Color")]
	public Color enableColor;

	public Color activeColor;

	public Color disableColor;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		BlinkKey(value: false);
		BlinkMouse(value: false);
		CheckSameSkill(value: false, null);
		if (!ES3.KeyExists("SkillPreset"))
		{
			presetNum = 0;
			InitalizePreset();
		}
		else
		{
			presetNum = ES3.Load<int>("SkillPreset");
		}
	}

	private void Start()
	{
		SetPage(page);
		SetPreset(-1);
		isInitalize = true;
		_feelerControllerData._skillDataKey = _skillDataKey;
		_feelerControllerData._skillDataMouse = _skillDataMouse;
		_feelerControllerData.ReloadKeyID();
		_feelerControllerData.ReloadMouseID();
		_skillGUIButtonBukkake._skillData = _skillDataBukkake;
		_skillGUIButtonBukkake.SetIcon(_skillDataBukkake);
		_skillGUISkillTreeObject.Clear();
		isCopyKey = true;
		isCopyMouse = true;
		copyKeyText.color = disableColor;
		copyKeyIconButton.unuse = true;
		copyKeyIconButton.Unuse();
		copyMouseText.color = disableColor;
		copyMouseIconButton.unuse = true;
		copyMouseIconButton.Unuse();
	}

	public void SetPage(int value)
	{
		page = value;
		for (int i = 0; i < skillPage.Count; i++)
		{
			if (i == page)
			{
				skillPage[i].SetActive(value: true);
				pageText[i].color = enableColor;
				pageButtonGUI[i].defaultColor = enableColor;
			}
			else
			{
				skillPage[i].SetActive(value: false);
				pageText[i].color = disableColor;
				pageButtonGUI[i].defaultColor = disableColor;
			}
		}
	}

	public void InitalizePreset()
	{
		Debug.LogError("Initialize Preset");
		for (int i = 0; i < _skillPreset0KeyNumber.Count; i++)
		{
			if (_skillPreset0Key[i] != null)
			{
				_skillPreset0KeyNumber[i] = _skillPreset0Key[i].skillID;
			}
			else
			{
				_skillPreset0KeyNumber[i] = 0;
			}
		}
		for (int j = 0; j < _skillPreset0MouseNumber.Count; j++)
		{
			if (_skillPreset0Mouse[j] != null)
			{
				_skillPreset0MouseNumber[j] = _skillPreset0Mouse[j].skillID;
			}
			else
			{
				_skillPreset0MouseNumber[j] = 0;
			}
		}
		ES3.Save("SkillPreset0Key", _skillPreset0KeyNumber);
		ES3.Save("SkillPreset0Mouse", _skillPreset0MouseNumber);
		for (int k = 0; k < _skillPreset1KeyNumber.Count; k++)
		{
			if (_skillPreset1Key[k] != null)
			{
				_skillPreset1KeyNumber[k] = _skillPreset1Key[k].skillID;
			}
			else
			{
				_skillPreset1KeyNumber[k] = 0;
			}
		}
		for (int l = 0; l < _skillPreset1MouseNumber.Count; l++)
		{
			if (_skillPreset1Mouse[l] != null)
			{
				_skillPreset1MouseNumber[l] = _skillPreset1Mouse[l].skillID;
			}
			else
			{
				_skillPreset1MouseNumber[l] = 0;
			}
		}
		ES3.Save("SkillPreset1Key", _skillPreset1KeyNumber);
		ES3.Save("SkillPreset1Mouse", _skillPreset1MouseNumber);
		for (int m = 0; m < _skillPreset2KeyNumber.Count; m++)
		{
			if (_skillPreset2Key[m] != null)
			{
				_skillPreset2KeyNumber[m] = _skillPreset2Key[m].skillID;
			}
			else
			{
				_skillPreset2KeyNumber[m] = 0;
			}
		}
		for (int n = 0; n < _skillPreset2MouseNumber.Count; n++)
		{
			if (_skillPreset2Mouse[n] != null)
			{
				_skillPreset2MouseNumber[n] = _skillPreset2Mouse[n].skillID;
			}
			else
			{
				_skillPreset2MouseNumber[n] = 0;
			}
		}
		ES3.Save("SkillPreset2Key", _skillPreset2KeyNumber);
		ES3.Save("SkillPreset2Mouse", _skillPreset2MouseNumber);
		for (int num = 0; num < _skillPreset3KeyNumber.Count; num++)
		{
			if (_skillPreset3Key[num] != null)
			{
				_skillPreset3KeyNumber[num] = _skillPreset3Key[num].skillID;
			}
			else
			{
				_skillPreset3KeyNumber[num] = 0;
			}
		}
		for (int num2 = 0; num2 < _skillPreset3MouseNumber.Count; num2++)
		{
			if (_skillPreset3Mouse[num2] != null)
			{
				_skillPreset3MouseNumber[num2] = _skillPreset3Mouse[num2].skillID;
			}
			else
			{
				_skillPreset3MouseNumber[num2] = 0;
			}
		}
		ES3.Save("SkillPreset3Key", _skillPreset3KeyNumber);
		ES3.Save("SkillPreset3Mouse", _skillPreset3MouseNumber);
		for (int num3 = 0; num3 < _skillPreset4KeyNumber.Count; num3++)
		{
			if (_skillPreset4Key[num3] != null)
			{
				_skillPreset4KeyNumber[num3] = _skillPreset4Key[num3].skillID;
			}
			else
			{
				_skillPreset4KeyNumber[num3] = 0;
			}
		}
		for (int num4 = 0; num4 < _skillPreset4MouseNumber.Count; num4++)
		{
			if (_skillPreset4Mouse[num4] != null)
			{
				_skillPreset4MouseNumber[num4] = _skillPreset4Mouse[num4].skillID;
			}
			else
			{
				_skillPreset4MouseNumber[num4] = 0;
			}
		}
		ES3.Save("SkillPreset4Key", _skillPreset4KeyNumber);
		ES3.Save("SkillPreset4Mouse", _skillPreset4MouseNumber);
	}

	public void SetPreset(int value)
	{
		if (value > -1)
		{
			SaveSkill();
			presetNum = value;
			ES3.Save("SkillPreset", presetNum);
		}
		LoadSkill(presetNum);
		for (int i = 0; i < presetIcon.Count; i++)
		{
			if (presetNum == i)
			{
				presetIcon[i].color = activeColor;
				presetIconSkill[i].color = activeColor;
				presetIconButton[i].defaultColor = activeColor;
				presetIconSkillButton[i].defaultColor = activeColor;
			}
			else
			{
				presetIcon[i].color = disableColor;
				presetIconSkill[i].color = disableColor;
				presetIconButton[i].defaultColor = disableColor;
				presetIconSkillButton[i].defaultColor = disableColor;
			}
		}
	}

	public void SaveSkill()
	{
		Debug.LogError("Save Preset: " + presetNum);
		for (int i = 0; i < _skillDataKeyNumber.Count; i++)
		{
			if (_skillDataKey[i] != null)
			{
				_skillDataKeyNumber[i] = _skillDataKey[i].skillID;
			}
			else
			{
				_skillDataKeyNumber[i] = 0;
			}
		}
		for (int j = 0; j < _skillDataMouseNumber.Count; j++)
		{
			if (_skillDataMouse[j] != null)
			{
				_skillDataMouseNumber[j] = _skillDataMouse[j].skillID;
			}
			else
			{
				_skillDataMouseNumber[j] = 0;
			}
		}
		if (presetNum == 0)
		{
			_skillPreset0KeyNumber = new List<int>(_skillDataKeyNumber);
			_skillPreset0MouseNumber = new List<int>(_skillDataMouseNumber);
			ES3.Save("SkillPreset0Key", _skillPreset0KeyNumber);
			ES3.Save("SkillPreset0Mouse", _skillPreset0MouseNumber);
		}
		else if (presetNum == 1)
		{
			_skillPreset1KeyNumber = new List<int>(_skillDataKeyNumber);
			_skillPreset1MouseNumber = new List<int>(_skillDataMouseNumber);
			ES3.Save("SkillPreset1Key", _skillPreset1KeyNumber);
			ES3.Save("SkillPreset1Mouse", _skillPreset1MouseNumber);
		}
		else if (presetNum == 2)
		{
			_skillPreset2KeyNumber = new List<int>(_skillDataKeyNumber);
			_skillPreset2MouseNumber = new List<int>(_skillDataMouseNumber);
			ES3.Save("SkillPreset2Key", _skillPreset2KeyNumber);
			ES3.Save("SkillPreset2Mouse", _skillPreset2MouseNumber);
		}
		else if (presetNum == 3)
		{
			_skillPreset3KeyNumber = new List<int>(_skillDataKeyNumber);
			_skillPreset3MouseNumber = new List<int>(_skillDataMouseNumber);
			ES3.Save("SkillPreset3Key", _skillPreset3KeyNumber);
			ES3.Save("SkillPreset3Mouse", _skillPreset3MouseNumber);
		}
		else if (presetNum == 4)
		{
			_skillPreset4KeyNumber = new List<int>(_skillDataKeyNumber);
			_skillPreset4MouseNumber = new List<int>(_skillDataMouseNumber);
			ES3.Save("SkillPreset4Key", _skillPreset4KeyNumber);
			ES3.Save("SkillPreset4Mouse", _skillPreset4MouseNumber);
		}
	}

	public void LoadSkill(int preset)
	{
		if (presetNum == 0)
		{
			_skillPreset0KeyNumber = ES3.Load<List<int>>("SkillPreset0Key");
			_skillPreset0MouseNumber = ES3.Load<List<int>>("SkillPreset0Mouse");
			_skillDataKeyNumber = new List<int>(_skillPreset0KeyNumber);
			_skillDataMouseNumber = new List<int>(_skillPreset0MouseNumber);
		}
		else if (presetNum == 1)
		{
			_skillPreset1KeyNumber = ES3.Load<List<int>>("SkillPreset1Key");
			_skillPreset1MouseNumber = ES3.Load<List<int>>("SkillPreset1Mouse");
			_skillDataKeyNumber = new List<int>(_skillPreset1KeyNumber);
			_skillDataMouseNumber = new List<int>(_skillPreset1MouseNumber);
		}
		else if (presetNum == 2)
		{
			_skillPreset2KeyNumber = ES3.Load<List<int>>("SkillPreset2Key");
			_skillPreset2MouseNumber = ES3.Load<List<int>>("SkillPreset2Mouse");
			_skillDataKeyNumber = new List<int>(_skillPreset2KeyNumber);
			_skillDataMouseNumber = new List<int>(_skillPreset2MouseNumber);
		}
		else if (presetNum == 3)
		{
			_skillPreset3KeyNumber = ES3.Load<List<int>>("SkillPreset3Key");
			_skillPreset3MouseNumber = ES3.Load<List<int>>("SkillPreset3Mouse");
			_skillDataKeyNumber = new List<int>(_skillPreset3KeyNumber);
			_skillDataMouseNumber = new List<int>(_skillPreset3MouseNumber);
		}
		else if (presetNum == 4)
		{
			_skillPreset4KeyNumber = ES3.Load<List<int>>("SkillPreset4Key");
			_skillPreset4MouseNumber = ES3.Load<List<int>>("SkillPreset4Mouse");
			_skillDataKeyNumber = new List<int>(_skillPreset4KeyNumber);
			_skillDataMouseNumber = new List<int>(_skillPreset4MouseNumber);
		}
		for (int i = 0; i < _skillDataKeyNumber.Count; i++)
		{
			_skillDataKey[i] = _skillGUIDataBase._skillDataList[_skillDataKeyNumber[i]];
		}
		for (int j = 0; j < _skillDataMouseNumber.Count; j++)
		{
			_skillDataMouse[j] = _skillGUIDataBase._skillDataList[_skillDataMouseNumber[j]];
		}
		for (int k = 0; k < _skillGUIButtonKey.Count; k++)
		{
			_skillGUIButtonKey[k]._skillData = _skillDataKey[k];
			_skillGUIButtonKey[k].SetIcon(_skillDataKey[k]);
		}
		for (int l = 0; l < _skillGUIButtonMouse.Count; l++)
		{
			_skillGUIButtonMouse[l]._skillData = _skillDataMouse[l];
			_skillGUIButtonMouse[l].SetIcon(_skillDataMouse[l]);
		}
		_feelerControllerData._skillDataKey = _skillDataKey;
		_feelerControllerData._skillDataMouse = _skillDataMouse;
		_feelerControllerData.ReloadKeyID();
		_feelerControllerData.ReloadMouseID();
		SkillGUIDataBase.instance.ResetIcon();
	}

	public void CopyKeyPreset()
	{
		isCopyKey = true;
		copyKeyText.color = enableColor;
		copyKeyIconButton.unuse = false;
		copyKeyIconButton.ColorReset();
		_skillCopyKey = new List<SkillData>(_skillDataKey);
	}

	public void CopyMousePreset()
	{
		isCopyMouse = true;
		copyMouseText.color = enableColor;
		copyMouseIconButton.unuse = false;
		copyMouseIconButton.ColorReset();
		_skillCopyMouse = new List<SkillData>(_skillDataMouse);
	}

	public void PasteKeyPreset()
	{
		_skillDataKey = new List<SkillData>(_skillCopyKey);
		for (int i = 0; i < _skillDataKeyNumber.Count; i++)
		{
			if (_skillDataKey[i] != null)
			{
				_skillDataKeyNumber[i] = _skillDataKey[i].skillID;
			}
			else
			{
				_skillDataKeyNumber[i] = 0;
			}
		}
		if (presetNum == 0)
		{
			_skillPreset0KeyNumber = new List<int>(_skillDataKeyNumber);
			ES3.Save("SkillPreset0Key", _skillPreset0KeyNumber);
		}
		else if (presetNum == 1)
		{
			_skillPreset1KeyNumber = new List<int>(_skillDataKeyNumber);
			ES3.Save("SkillPreset1Key", _skillPreset1KeyNumber);
		}
		else if (presetNum == 2)
		{
			_skillPreset2KeyNumber = new List<int>(_skillDataKeyNumber);
			ES3.Save("SkillPreset2Key", _skillPreset2KeyNumber);
		}
		else if (presetNum == 3)
		{
			_skillPreset3KeyNumber = new List<int>(_skillDataKeyNumber);
			ES3.Save("SkillPreset3Key", _skillPreset3KeyNumber);
		}
		else if (presetNum == 4)
		{
			_skillPreset4KeyNumber = new List<int>(_skillDataKeyNumber);
			ES3.Save("SkillPreset4Key", _skillPreset4KeyNumber);
		}
		for (int j = 0; j < _skillGUIButtonKey.Count; j++)
		{
			_skillGUIButtonKey[j]._skillData = _skillDataKey[j];
			_skillGUIButtonKey[j].SetIcon(_skillDataKey[j]);
		}
		_feelerControllerData._skillDataKey = _skillDataKey;
		_feelerControllerData.ReloadKeyID();
		SkillGUIDataBase.instance.ResetIcon();
	}

	public void PasteMousePreset()
	{
		_skillDataMouse = new List<SkillData>(_skillCopyMouse);
		for (int i = 0; i < _skillDataMouseNumber.Count; i++)
		{
			if (_skillDataMouse[i] != null)
			{
				_skillDataMouseNumber[i] = _skillDataMouse[i].skillID;
			}
			else
			{
				_skillDataMouseNumber[i] = 0;
			}
		}
		if (presetNum == 0)
		{
			_skillPreset0MouseNumber = new List<int>(_skillDataMouseNumber);
			ES3.Save("SkillPreset0Mouse", _skillPreset0MouseNumber);
		}
		else if (presetNum == 1)
		{
			_skillPreset1MouseNumber = new List<int>(_skillDataMouseNumber);
			ES3.Save("SkillPreset1Mouse", _skillPreset1MouseNumber);
		}
		else if (presetNum == 2)
		{
			_skillPreset2MouseNumber = new List<int>(_skillDataMouseNumber);
			ES3.Save("SkillPreset2Mouse", _skillPreset2MouseNumber);
		}
		else if (presetNum == 3)
		{
			_skillPreset3MouseNumber = new List<int>(_skillDataMouseNumber);
			ES3.Save("SkillPreset3Mouse", _skillPreset3MouseNumber);
		}
		else if (presetNum == 4)
		{
			_skillPreset4MouseNumber = new List<int>(_skillDataMouseNumber);
			ES3.Save("SkillPreset4Mouse", _skillPreset4MouseNumber);
		}
		for (int j = 0; j < _skillGUIButtonMouse.Count; j++)
		{
			_skillGUIButtonMouse[j]._skillData = _skillDataMouse[j];
			_skillGUIButtonMouse[j].SetIcon(_skillDataMouse[j]);
		}
		_feelerControllerData._skillDataMouse = _skillDataMouse;
		_feelerControllerData.ReloadMouseID();
		SkillGUIDataBase.instance.ResetIcon();
	}

	private void OnDisable()
	{
		SkillGUIOff();
	}

	public void SkillGUIOff()
	{
		for (int i = 0; i < _skillGUIButtonKey.Count; i++)
		{
			_skillGUIButtonKey[i].SetBlinkEffect(value: false);
		}
		for (int j = 0; j < _skillGUIButtonMouse.Count; j++)
		{
			_skillGUIButtonMouse[j].SetBlinkEffect(value: false);
		}
		informationObject.InfomationOff();
		quickSetObject.QuickOff(sound: false);
		CheckSameSkill(value: false, null);
		if (isInitalize)
		{
			SaveSkill();
		}
		ResetPosition();
	}

	public void ResetPosition()
	{
		Debug.LogError("Reset SkillIcon Position");
		for (int i = 0; i < _skillGUISkillTreeObject.Count; i++)
		{
			_skillGUISkillTreeObject[i].ResetPosition();
		}
		BlinkMouse(value: false);
		BlinkKey(value: false);
		SkillGUIInformation.instance.isDrag = false;
	}

	public void BlinkKey(bool value)
	{
		for (int i = 0; i < _skillGUIButtonKey.Count; i++)
		{
			_skillGUIButtonKey[i].SetBlinkEffect(value);
		}
	}

	public void BlinkMouse(bool value)
	{
		for (int i = 0; i < _skillGUIButtonMouse.Count; i++)
		{
			_skillGUIButtonMouse[i].SetBlinkEffect(value);
		}
	}

	public void CheckSameSkill(bool value, SkillData data)
	{
		if (value)
		{
			for (int i = 0; i < _skillGUIButtonMouse.Count; i++)
			{
				if (data == _skillGUIButtonMouse[i]._skillData)
				{
					_skillGUIButtonMouse[i].SetSameSkillEffect(value: true);
				}
				else
				{
					_skillGUIButtonMouse[i].SetSameSkillEffect(value: false);
				}
			}
			for (int j = 0; j < _skillGUIButtonKey.Count; j++)
			{
				if (data == _skillGUIButtonKey[j]._skillData)
				{
					_skillGUIButtonKey[j].SetSameSkillEffect(value: true);
				}
				else
				{
					_skillGUIButtonKey[j].SetSameSkillEffect(value: false);
				}
			}
		}
		else
		{
			for (int k = 0; k < _skillGUIButtonMouse.Count; k++)
			{
				_skillGUIButtonMouse[k].SetSameSkillEffect(value: false);
			}
			for (int l = 0; l < _skillGUIButtonKey.Count; l++)
			{
				_skillGUIButtonKey[l].SetSameSkillEffect(value: false);
			}
		}
	}

	public void SetKeySkill(int value, SkillData script)
	{
		int num = -1;
		for (int i = 0; i < _skillDataKey.Count; i++)
		{
			if (i != value && script == _skillDataKey[i])
			{
				num = i;
				break;
			}
		}
		_skillDataKey[value] = script;
		_skillGUIButtonKey[value].SetIcon(_skillDataKey[value]);
		_skillGUIButtonKey[value].SetSkillEffect();
		if (num >= 0)
		{
			_skillDataKey[num] = null;
			_skillGUIButtonKey[num].SetIcon(_skillDataKey[num]);
			_skillGUIButtonKey[num].SetSkillSubEffect();
		}
		_feelerControllerData._skillDataKey = _skillDataKey;
		_feelerControllerData.ReloadKeyID();
		SkillGUIDataBase.instance.ResetIcon();
	}

	public void SetMouseSkill(int value, SkillData script)
	{
		int num = -1;
		for (int i = 0; i < _skillDataMouse.Count; i++)
		{
			if (i != value && script == _skillDataMouse[i])
			{
				num = i;
				break;
			}
		}
		_skillDataMouse[value] = script;
		_skillGUIButtonMouse[value].SetIcon(_skillDataMouse[value]);
		_skillGUIButtonMouse[value].SetSkillEffect();
		if (num >= 0)
		{
			_skillDataMouse[num] = null;
			_skillGUIButtonMouse[num].SetIcon(_skillDataMouse[num]);
			_skillGUIButtonMouse[num].SetSkillSubEffect();
		}
		_feelerControllerData._skillDataMouse = _skillDataMouse;
		_feelerControllerData.ReloadMouseID();
		SkillGUIDataBase.instance.ResetIcon();
	}

	public void SwitchKeySkill(int value, int value2)
	{
		if (value != value2)
		{
			tmpSkillData = _skillDataKey[value];
			_skillDataKey[value] = _skillDataKey[value2];
			_skillDataKey[value2] = tmpSkillData;
			_skillGUIButtonKey[value].SetIcon(_skillDataKey[value]);
			_skillGUIButtonKey[value].SetSkillSubEffect();
			_skillGUIButtonKey[value2].SetIcon(_skillDataKey[value2]);
			_skillGUIButtonKey[value2].SetSkillEffect();
			_feelerControllerData._skillDataKey = _skillDataKey;
			_feelerControllerData.ReloadKeyID();
			SkillGUIDataBase.instance.ResetIcon();
		}
	}

	public void SwitchMouseSkill(int value, int value2)
	{
		if (value != value2)
		{
			tmpSkillData = _skillDataMouse[value];
			_skillDataMouse[value] = _skillDataMouse[value2];
			_skillDataMouse[value2] = tmpSkillData;
			_skillGUIButtonMouse[value].SetIcon(_skillDataMouse[value]);
			_skillGUIButtonMouse[value].SetSkillSubEffect();
			_skillGUIButtonMouse[value2].SetIcon(_skillDataMouse[value2]);
			_skillGUIButtonMouse[value2].SetSkillEffect();
		}
		_feelerControllerData._skillDataMouse = _skillDataMouse;
		_feelerControllerData.ReloadMouseID();
		SkillGUIDataBase.instance.ResetIcon();
	}

	public void PlaySe(AudioClip clip)
	{
		if (!isDrag)
		{
			EffectSeManager.instance.PlaySe(clip);
		}
	}
}
