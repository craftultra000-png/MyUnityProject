using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SideCharacterManger : MonoBehaviour
{
	public CharacterCostumeSwitch _characterCostumeSwitch;

	public CharacterHairManager _characterHairManager;

	public CharacterTypeManager _characterTypeManager;

	public CharacterWetManager _characterWetManager;

	[Header("Status")]
	public bool isInitalize;

	[Header("Hair Button")]
	public int hairType = -1;

	public List<TextMeshProUGUI> hairEquip;

	public List<ButtonTriggerGUI> hairButtonGUI;

	[Header("Skin Button")]
	public int skinType = -1;

	public List<TextMeshProUGUI> skinEquip;

	public List<ButtonTriggerGUI> skinButtonGUI;

	[Space]
	public Image skinLightIcon;

	public Image skinMediumIcon;

	public Image skinDarkIcon;

	public Color skinLightEnableColor;

	public Color skinLightDisableColor;

	public Color skinMediumEnableColor;

	public Color skinMediumDisableColor;

	public Color skinDarkEnableColor;

	public Color skinDarkDisableColor;

	[Header("UnderHair Button")]
	public bool underHair;

	public Image underHairIcon;

	public ButtonTriggerGUI underHairButtonGUI;

	[Header("LewdCrest Button")]
	public bool lewdCrest;

	public Image lewdCrestIcon;

	public ButtonTriggerGUI lewdCrestButtonGUI;

	[Header("UnderHair Button")]
	public bool wet;

	public Image wetIcon;

	public ButtonTriggerGUI wetButtonGUI;

	[Header("Eyes Status")]
	public bool isEmpty;

	public bool isHeart;

	public bool isTear;

	public bool isSleep;

	public List<Image> eyesIcon;

	public List<ButtonTriggerGUI> eyeButtonGUI;

	[Header("Nipple Button")]
	public int nippleColor;

	public Image nippleLightIcon;

	public Image nippleDarkIcon;

	public ButtonTriggerGUI nippleLightButtonGUI;

	public ButtonTriggerGUI nippleDarkButtonGUI;

	[Space]
	public Color nippleLightEnableColor;

	public Color nippleLightDisableColor;

	public Color nippleDarkEnableColor;

	public Color nippleDarkDisableColor;

	[Header("Color")]
	public Color enableColor;

	public Color disableColor;

	[Header("Show Hide Frame")]
	public bool hideHair;

	public GameObject frameHair;

	public TextMeshProUGUI markHair;

	[Space]
	public bool hideOption;

	public GameObject frameOption;

	public TextMeshProUGUI markOption;

	[Space]
	public bool hideEyes;

	public GameObject frameEyes;

	public TextMeshProUGUI markEyes;

	[Space]
	public bool hideHairColor;

	public GameObject frameHairColor;

	public TextMeshProUGUI markHairColor;

	[Space]
	public bool hideEyesColor;

	public GameObject frameEyesColor;

	public TextMeshProUGUI markEyesColor;

	private void Start()
	{
	}

	public void Initalize(int skin)
	{
		SetSkin(skin);
		SetEyes();
		_characterTypeManager.lewdCrest = !_characterTypeManager.lewdCrest;
		SetLewdCrest();
		_characterWetManager.isWet = !_characterWetManager.isWet;
		SetWet();
		SetNippleColor(nippleColor);
		isInitalize = true;
		Debug.LogError("CostumeSwitch Initalize");
		_characterCostumeSwitch.isInitalize = true;
		Debug.LogError("TypeManager Initalize");
		_characterTypeManager.isInitalize = true;
	}

	public void OnEnable()
	{
		hideHair = true;
		HideHair();
		hideOption = true;
		HideOption();
		hideEyes = true;
		HideEyes();
		hideHairColor = true;
		HideHairColor();
		hideEyesColor = true;
		HideEyesColor();
	}

	public void SetHair(int value)
	{
		if (hairType == -1)
		{
			_characterHairManager.SetHairType(value);
			hairType = value;
		}
		else
		{
			hairType = value;
			_characterHairManager.ChangeHair(hairType);
		}
		for (int i = 0; i < hairEquip.Count; i++)
		{
			if (i == hairType)
			{
				hairEquip[i].color = enableColor;
				hairButtonGUI[i].defaultColor = enableColor;
			}
			else
			{
				hairEquip[i].color = disableColor;
				hairButtonGUI[i].defaultColor = disableColor;
			}
		}
	}

	public void SetSkin(int value)
	{
		if (skinType == -1)
		{
			_characterTypeManager.ChangeCharacter(value);
			skinType = value;
		}
		else
		{
			skinType = value;
			_characterTypeManager.ChangeSkin(skinType);
		}
		if (skinLightIcon != null)
		{
			if (skinType == 0)
			{
				skinLightIcon.color = skinLightEnableColor;
				skinButtonGUI[0].defaultColor = skinLightEnableColor;
				skinMediumIcon.color = skinMediumDisableColor;
				skinButtonGUI[1].defaultColor = skinMediumDisableColor;
				skinDarkIcon.color = skinDarkDisableColor;
				skinButtonGUI[2].defaultColor = skinDarkDisableColor;
			}
			else if (skinType == 1)
			{
				skinLightIcon.color = skinLightDisableColor;
				skinButtonGUI[0].defaultColor = skinLightDisableColor;
				skinMediumIcon.color = skinMediumEnableColor;
				skinButtonGUI[1].defaultColor = skinMediumEnableColor;
				skinDarkIcon.color = skinDarkDisableColor;
				skinButtonGUI[2].defaultColor = skinDarkDisableColor;
			}
			else if (skinType == 2)
			{
				skinLightIcon.color = skinLightDisableColor;
				skinButtonGUI[0].defaultColor = skinLightDisableColor;
				skinMediumIcon.color = skinMediumDisableColor;
				skinButtonGUI[1].defaultColor = skinMediumDisableColor;
				skinDarkIcon.color = skinDarkEnableColor;
				skinButtonGUI[2].defaultColor = skinDarkEnableColor;
			}
		}
	}

	public void SetUnderHair()
	{
		_characterCostumeSwitch.SetUnderHair();
		if (underHairIcon != null)
		{
			if (_characterCostumeSwitch.underhair)
			{
				underHairIcon.color = disableColor;
				underHairButtonGUI.defaultColor = disableColor;
			}
			else
			{
				underHairIcon.color = enableColor;
				underHairButtonGUI.defaultColor = enableColor;
			}
		}
	}

	public void SetLewdCrest()
	{
		_characterTypeManager.SetLewdCrest();
		if (_characterTypeManager.lewdCrest)
		{
			lewdCrestIcon.color = enableColor;
			lewdCrestButtonGUI.defaultColor = enableColor;
		}
		else
		{
			lewdCrestIcon.color = disableColor;
			lewdCrestButtonGUI.defaultColor = disableColor;
		}
	}

	public void SetWet()
	{
		_characterWetManager.SetWet();
		if (wetIcon != null)
		{
			if (!_characterWetManager.isWet)
			{
				wetIcon.color = disableColor;
				wetButtonGUI.defaultColor = disableColor;
			}
			else
			{
				wetIcon.color = enableColor;
				wetButtonGUI.defaultColor = enableColor;
			}
		}
	}

	public void SetEyes()
	{
		if (eyesIcon[0] != null)
		{
			if (isEmpty)
			{
				eyesIcon[0].color = enableColor;
				eyeButtonGUI[0].defaultColor = enableColor;
			}
			else
			{
				eyesIcon[0].color = disableColor;
				eyeButtonGUI[0].defaultColor = disableColor;
			}
			if (isHeart)
			{
				eyesIcon[1].color = enableColor;
				eyeButtonGUI[1].defaultColor = enableColor;
			}
			else
			{
				eyesIcon[1].color = disableColor;
				eyeButtonGUI[1].defaultColor = disableColor;
			}
			if (isTear)
			{
				eyesIcon[2].color = enableColor;
				eyeButtonGUI[2].defaultColor = enableColor;
			}
			else
			{
				eyesIcon[2].color = disableColor;
				eyeButtonGUI[2].defaultColor = disableColor;
			}
			if (isSleep)
			{
				eyesIcon[3].color = enableColor;
				eyeButtonGUI[3].defaultColor = enableColor;
			}
			else
			{
				eyesIcon[3].color = disableColor;
				eyeButtonGUI[3].defaultColor = disableColor;
			}
		}
	}

	public void SetNippleColor(int value)
	{
		nippleColor = value;
		_characterTypeManager.ChangeBody(nippleColor);
		if (nippleLightIcon != null)
		{
			if (nippleColor == 0)
			{
				nippleLightIcon.color = nippleLightEnableColor;
				nippleLightButtonGUI.defaultColor = nippleLightEnableColor;
				nippleDarkIcon.color = nippleDarkDisableColor;
				nippleDarkButtonGUI.defaultColor = nippleDarkDisableColor;
			}
			else if (nippleColor == 1)
			{
				nippleLightIcon.color = nippleLightDisableColor;
				nippleLightButtonGUI.defaultColor = nippleLightDisableColor;
				nippleDarkIcon.color = nippleDarkEnableColor;
				nippleDarkButtonGUI.defaultColor = nippleDarkEnableColor;
			}
		}
	}

	public void HideHair()
	{
		hideHair = !hideHair;
		frameHair.SetActive(!hideHair);
		if (hideHair)
		{
			markHair.text = "-";
		}
		else
		{
			markHair.text = "+";
		}
	}

	public void HideOption()
	{
		hideOption = !hideOption;
		frameOption.SetActive(!hideOption);
		if (hideOption)
		{
			markOption.text = "-";
		}
		else
		{
			markOption.text = "+";
		}
	}

	public void HideEyes()
	{
		hideEyes = !hideEyes;
		frameEyes.SetActive(!hideEyes);
		if (hideEyes)
		{
			markEyes.text = "-";
		}
		else
		{
			markEyes.text = "+";
		}
	}

	public void HideHairColor()
	{
		hideHairColor = !hideHairColor;
		frameHairColor.SetActive(!hideHairColor);
		if (hideHairColor)
		{
			markHairColor.text = "-";
		}
		else
		{
			markHairColor.text = "+";
		}
	}

	public void HideEyesColor()
	{
		hideEyesColor = !hideEyesColor;
		frameEyesColor.SetActive(!hideEyesColor);
		if (hideEyesColor)
		{
			markEyesColor.text = "-";
		}
		else
		{
			markEyesColor.text = "+";
		}
	}

	public void HideOtherHair(bool value)
	{
		hideHair = true;
		HideHair();
		hideOption = !value;
		HideOption();
		hideEyes = !value;
		HideEyes();
		hideHairColor = !value;
		HideHairColor();
		hideEyesColor = !value;
		HideEyesColor();
	}

	public void HideOtherOption(bool value)
	{
		hideHair = !value;
		HideHair();
		hideOption = true;
		HideOption();
		hideEyes = !value;
		HideEyes();
		hideHairColor = !value;
		HideHairColor();
		hideEyesColor = !value;
		HideEyesColor();
	}

	public void HideOtherEyes(bool value)
	{
		hideHair = !value;
		HideHair();
		hideOption = !value;
		HideOption();
		hideEyes = true;
		HideEyes();
		hideHairColor = !value;
		HideHairColor();
		hideEyesColor = !value;
		HideEyesColor();
	}

	public void HideOtherHairColor(bool value)
	{
		hideHair = !value;
		HideHair();
		hideOption = !value;
		HideOption();
		hideEyes = !value;
		HideEyes();
		hideHairColor = true;
		HideHairColor();
		hideEyesColor = !value;
		HideEyesColor();
	}

	public void HideOtherEyesColor(bool value)
	{
		hideHair = !value;
		HideHair();
		hideOption = !value;
		HideOption();
		hideEyes = !value;
		HideEyes();
		hideHairColor = !value;
		HideHairColor();
		hideEyesColor = true;
		HideEyesColor();
	}
}
