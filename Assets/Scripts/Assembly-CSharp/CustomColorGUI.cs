using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CustomColorGUI : MonoBehaviour
{
	public CharacterCostumeSwitch _characterCostumeSwitch;

	public CharacterTypeManager _characterTypeManager;

	public CharacterEyesManager _characterEyesManager;

	[Header("Status")]
	public bool useCostomColor;

	public bool isCostomColor;

	[Header("Color")]
	public Image colorDefaultImage;

	public Image colorCustomImage;

	public Image colorBrightnessImage;

	[Space]
	public Color defaultColor;

	public Color currentColor;

	public Color currentLightColor;

	public Color calcColor;

	[Header("Color Setting")]
	public float hueColor;

	public float brightnessColor;

	[Header("UI Slider")]
	public bool hueStartUp;

	public bool brightnessStartUp;

	public Slider hueSlider;

	public Slider brightnessSlider;

	[Header("Equip")]
	public GameObject lockImage;

	public List<TextMeshProUGUI> colorEquip;

	public List<TextMeshProUGUI> textEquip;

	public List<ButtonTriggerGUI> buttonEquip;

	[Header("Color Save Data")]
	public List<Image> colorSaveImage;

	public List<ButtonTriggerGUI> buttunLoad;

	public List<ButtonTriggerGUI> buttonSave;

	public List<TextMeshProUGUI> buttonSaveText;

	public List<GameObject> loadLockImage;

	public List<float> hueColorData;

	public List<float> brightnessColorData;

	public List<Color> colorData;

	[Header("Color")]
	public Color enableColor;

	public Color disableColor;

	public Color lockColor;

	private void Start()
	{
		brightnessColor = 15f;
		GetCustomColor();
		SetColorEquip(0);
	}

	public void GetCustomColor()
	{
		if (_characterCostumeSwitch != null)
		{
			_characterCostumeSwitch.GetCustomColorData();
			useCostomColor = _characterCostumeSwitch.useCostomColor;
			isCostomColor = _characterCostumeSwitch.isCostomColor;
			_characterCostumeSwitch.LoadColor();
			hueColor = _characterCostumeSwitch.hueColor;
			brightnessColor = _characterCostumeSwitch.brightnessColor;
			hueSlider.value = hueColor;
			brightnessSlider.value = brightnessColor;
			defaultColor = _characterCostumeSwitch.defaultColor;
			buttonEquip[0].defaultColor = defaultColor;
			buttonEquip[0].pointerEnterColor = defaultColor * 0.8f;
			buttonEquip[0].mouseClickColor = defaultColor * 0.8f;
			buttonEquip[0].ColorReset();
			_characterCostumeSwitch.LoadColorData();
			_characterCostumeSwitch.hueColorData = hueColorData;
			_characterCostumeSwitch.brightnessColorData = brightnessColorData;
		}
		if (_characterTypeManager != null)
		{
			useCostomColor = _characterTypeManager.useCostomColor;
			isCostomColor = _characterTypeManager.isCostomColor;
			_characterTypeManager.LoadColor();
			hueColor = _characterTypeManager.hueColor;
			brightnessColor = _characterTypeManager.brightnessColor;
			hueSlider.value = hueColor;
			brightnessSlider.value = brightnessColor;
			defaultColor = _characterTypeManager.defaultColor;
			buttonEquip[0].defaultColor = defaultColor;
			buttonEquip[0].pointerEnterColor = defaultColor * 0.8f;
			buttonEquip[0].mouseClickColor = defaultColor * 0.8f;
			buttonEquip[0].ColorReset();
			_characterTypeManager.LoadColorData();
			_characterTypeManager.hueColorData = hueColorData;
			_characterTypeManager.brightnessColorData = brightnessColorData;
		}
		if (_characterEyesManager != null)
		{
			useCostomColor = _characterEyesManager.useCostomColor;
			isCostomColor = _characterEyesManager.isCostomColor;
			_characterEyesManager.LoadColor();
			hueColor = _characterEyesManager.hueColor;
			brightnessColor = _characterEyesManager.brightnessColor;
			hueSlider.value = hueColor;
			brightnessSlider.value = brightnessColor;
			defaultColor = _characterEyesManager.defaultColor;
			buttonEquip[0].defaultColor = defaultColor;
			buttonEquip[0].pointerEnterColor = defaultColor * 0.8f;
			buttonEquip[0].mouseClickColor = defaultColor * 0.8f;
			buttonEquip[0].ColorReset();
			_characterEyesManager.LoadColorData();
			_characterEyesManager.hueColorData = hueColorData;
			_characterEyesManager.brightnessColorData = brightnessColorData;
		}
		if (!useCostomColor)
		{
			SetColorEquip(0);
			LockCustomColor(value: true);
		}
		else if (!isCostomColor)
		{
			SetColorEquip(0);
			LockCustomColor(value: false);
		}
		else
		{
			SetColorEquip(1);
			LockCustomColor(value: false);
		}
		MixColor();
	}

	public void SetHue()
	{
		hueColor = hueSlider.value;
		if (hueStartUp)
		{
			MixColor();
		}
		hueStartUp = true;
	}

	public void SetBrightness()
	{
		brightnessColor = brightnessSlider.value;
		if (brightnessStartUp)
		{
			MixColor();
		}
		brightnessStartUp = true;
	}

	public void MixColor()
	{
		if (useCostomColor)
		{
			float h = (hueColor + 15f) / 30f;
			calcColor = Color.HSVToRGB(h, 1f, 1f);
			float num = brightnessColor / 15f;
			if (num < 0f)
			{
				Color black = Color.black;
				currentColor = Color.Lerp(calcColor, black, 0f - num);
				black = Color.gray;
				currentLightColor = Color.Lerp(calcColor, black, 0f - num);
			}
			else
			{
				Color black = Color.white;
				currentColor = Color.Lerp(calcColor, black, num);
				black = Color.white * 1.1f;
				currentLightColor = Color.Lerp(calcColor, black, num);
				currentLightColor = ClampColor(currentLightColor);
			}
			currentColor.a = 1f;
			colorBrightnessImage.color = calcColor;
			buttonEquip[1].defaultColor = currentColor;
			buttonEquip[1].pointerEnterColor = currentColor * 0.8f;
			buttonEquip[1].mouseClickColor = currentColor * 0.8f;
			buttonEquip[1].ColorReset();
			if (_characterCostumeSwitch != null)
			{
				if (isCostomColor)
				{
					_characterCostumeSwitch.SetCustomColor(currentColor, currentLightColor);
				}
				else
				{
					_characterCostumeSwitch.SetDefaultColor();
				}
			}
			if (_characterTypeManager != null)
			{
				if (isCostomColor)
				{
					_characterTypeManager.SetCustomColor(currentColor, currentLightColor);
				}
				else
				{
					_characterTypeManager.SetDefaultColor();
				}
			}
			if (_characterEyesManager != null)
			{
				if (isCostomColor)
				{
					_characterEyesManager.SetCustomColor(currentColor, currentLightColor);
				}
				else
				{
					_characterEyesManager.SetDefaultColor();
				}
			}
		}
		else
		{
			buttonEquip[1].defaultColor = lockColor;
			buttonEquip[1].pointerEnterColor = lockColor * 0.8f;
			buttonEquip[1].mouseClickColor = lockColor * 0.8f;
			buttonEquip[1].ColorReset();
			for (int i = 0; i < loadLockImage.Count; i++)
			{
				colorSaveImage[i].color = lockColor;
				buttunLoad[i].defaultColor = lockColor;
				buttonSaveText[i].color = lockColor;
				buttonSave[i].defaultColor = lockColor;
			}
			if (_characterCostumeSwitch != null)
			{
				_characterCostumeSwitch.SetDefaultColor();
			}
			if (_characterTypeManager != null)
			{
				_characterTypeManager.SetDefaultColor();
			}
			if (_characterEyesManager != null)
			{
				_characterEyesManager.SetDefaultColor();
			}
		}
		if (_characterCostumeSwitch != null)
		{
			_characterCostumeSwitch.isCostomColor = isCostomColor;
			_characterCostumeSwitch.useCostomColor = useCostomColor;
			_characterCostumeSwitch.hueColor = hueColor;
			_characterCostumeSwitch.brightnessColor = brightnessColor;
			_characterCostumeSwitch.SetCustomColorData();
		}
		if (_characterTypeManager != null)
		{
			_characterTypeManager.useCostomColor = useCostomColor;
			_characterTypeManager.hueColor = hueColor;
			_characterTypeManager.brightnessColor = brightnessColor;
			_characterTypeManager.SaveColor();
			if (_characterTypeManager.isLoad)
			{
				_characterTypeManager.LoadColorData();
				hueColorData = _characterTypeManager.hueColorData;
				brightnessColorData = _characterTypeManager.brightnessColorData;
			}
			else
			{
				_characterTypeManager.hueColorData = hueColorData;
				_characterTypeManager.brightnessColorData = brightnessColorData;
			}
		}
		if (_characterEyesManager != null)
		{
			_characterEyesManager.useCostomColor = useCostomColor;
			_characterEyesManager.hueColor = hueColor;
			_characterEyesManager.brightnessColor = brightnessColor;
			_characterEyesManager.SaveColor();
			if (_characterEyesManager.isLoad)
			{
				_characterEyesManager.LoadColorData();
				hueColorData = _characterEyesManager.hueColorData;
				brightnessColorData = _characterEyesManager.brightnessColorData;
			}
			else
			{
				_characterEyesManager.hueColorData = hueColorData;
				_characterEyesManager.brightnessColorData = brightnessColorData;
			}
		}
	}

	private Color ClampColor(Color target)
	{
		target.r = Mathf.Clamp01(target.r);
		target.g = Mathf.Clamp01(target.g);
		target.b = Mathf.Clamp01(target.b);
		target.a = Mathf.Clamp01(target.a);
		return target;
	}

	public void SaveColor(int value)
	{
		hueColorData[value] = hueColor;
		brightnessColorData[value] = brightnessColor;
		colorSaveImage[value].color = currentColor;
		buttunLoad[value].defaultColor = currentColor;
		buttunLoad[value].pointerEnterColor = currentColor * 0.8f;
		buttunLoad[value].mouseClickColor = currentColor * 0.8f;
		if (_characterCostumeSwitch != null)
		{
			_characterCostumeSwitch.SaveColorData();
		}
		if (_characterTypeManager != null)
		{
			_characterTypeManager.SaveColorData();
		}
		if (_characterEyesManager != null)
		{
			_characterEyesManager.SaveColorData();
		}
	}

	public void LoadColor(int value)
	{
		hueColor = hueColorData[value];
		brightnessColor = brightnessColorData[value];
		hueSlider.value = hueColor;
		brightnessSlider.value = brightnessColor;
		MixColor();
		SetColorEquip(1);
	}

	public void LoadAllColor()
	{
		for (int i = 0; i < colorData.Count; i++)
		{
			float h = (hueColorData[i] + 15f) / 30f;
			calcColor = Color.HSVToRGB(h, 1f, 1f);
			float num = brightnessColorData[i] / 15f;
			if (num < 0f)
			{
				Color black = Color.black;
				colorData[i] = Color.Lerp(calcColor, black, 0f - num);
			}
			else
			{
				Color black = Color.white;
				colorData[i] = Color.Lerp(calcColor, black, num);
			}
			colorSaveImage[i].color = colorData[i];
			buttunLoad[i].defaultColor = colorData[i];
			buttunLoad[i].pointerEnterColor = colorData[i] * 0.8f;
			buttunLoad[i].mouseClickColor = colorData[i] * 0.8f;
			buttonSaveText[i].color = enableColor;
			buttonSave[i].defaultColor = enableColor;
		}
	}

	public void SetColorEquip(int value)
	{
		if (value == 0)
		{
			colorEquip[0].text = "E";
			colorEquip[1].text = "-";
			textEquip[0].color = enableColor;
			textEquip[1].color = disableColor;
			buttonEquip[0].defaultColor = defaultColor;
			buttonEquip[0].pointerEnterColor = defaultColor * 0.8f;
			buttonEquip[0].mouseClickColor = defaultColor * 0.8f;
			if (useCostomColor)
			{
				buttonEquip[1].defaultColor = currentColor;
				buttonEquip[1].pointerEnterColor = currentColor * 0.8f;
				buttonEquip[1].mouseClickColor = currentColor * 0.8f;
			}
			else
			{
				buttonEquip[1].defaultColor = lockColor;
				buttonEquip[1].pointerEnterColor = lockColor * 0.8f;
				buttonEquip[1].mouseClickColor = lockColor * 0.8f;
			}
			isCostomColor = false;
		}
		else
		{
			colorEquip[0].text = "-";
			colorEquip[1].text = "E";
			textEquip[0].color = disableColor;
			textEquip[1].color = enableColor;
			buttonEquip[0].defaultColor = defaultColor;
			buttonEquip[0].pointerEnterColor = defaultColor * 0.8f;
			buttonEquip[0].mouseClickColor = defaultColor * 0.8f;
			if (useCostomColor)
			{
				buttonEquip[1].defaultColor = currentColor;
				buttonEquip[1].pointerEnterColor = currentColor * 0.8f;
				buttonEquip[1].mouseClickColor = currentColor * 0.8f;
			}
			else
			{
				buttonEquip[1].defaultColor = lockColor;
				buttonEquip[1].pointerEnterColor = lockColor * 0.8f;
				buttonEquip[1].mouseClickColor = lockColor * 0.8f;
			}
			isCostomColor = true;
		}
		MixColor();
	}

	public void LockCustomColor(bool value)
	{
		lockImage.SetActive(value);
		buttonEquip[1].unuse = value;
		for (int i = 0; i < loadLockImage.Count; i++)
		{
			loadLockImage[i].SetActive(value);
			buttunLoad[i].unuse = value;
			buttonSave[i].unuse = value;
		}
		if (!value)
		{
			LoadAllColor();
		}
	}
}
