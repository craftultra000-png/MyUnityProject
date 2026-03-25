using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuickSkillGUI : MonoBehaviour
{
	public static QuickSkillGUI instance;

	public CharacterAnimationSpeedManager _characterAnimationSpeedManager;

	[Header("Skill GUI disable")]
	public List<GameObject> settingFrameList;

	[Header("Sub GUI disable")]
	public int skillID;

	public GameObject currentIcon;

	public GameObject frameHorseRide;

	public GameObject frameHoldLimb;

	public GameObject frameWartBed;

	[Header("Gimmik FeelerDoll GUI")]
	public int page;

	public GameObject gimmickPage;

	public GameObject feelerDollPage;

	public TextMeshProUGUI gimmickText;

	public TextMeshProUGUI feelerDollText;

	public ButtonTriggerGUI gimmickButtonGUI;

	public ButtonTriggerGUI feelerDollButtonGUI;

	[Header("Cousor Off/On")]
	public List<GameObject> frameList;

	[Header("Volume Setting")]
	public float vaginaPistonSpeed;

	public float analPistonSpeed;

	public float mouthPistonSpeed;

	public float titsPistonSpeed;

	public float vaginaOpenSpeed;

	public float horseRideSpeed;

	public float horseRidePower;

	public float limbHoldAngle;

	public float bedOpenAngle;

	[Header("UI Slider")]
	public bool useSliderSe;

	public AudioClip slideSe;

	public Slider vaginaPistonSlider;

	public Slider analPistonSlider;

	public Slider mouthPistonSlider;

	public Slider titsPistonSlider;

	public Slider vaginaOpenSlider;

	public Slider horseRideSpeedSlider;

	public Slider horseRidePowerSlider;

	public Slider limbHoldAngleSlider;

	public Slider wartBedOpenSlider;

	[Header("Color")]
	public Color enableColor;

	public Color disableColor;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
	}

	private void Start()
	{
		SetPage(0);
		FrameList(value: false);
		vaginaPistonSpeed = 0.5f;
		analPistonSpeed = 0.5f;
		mouthPistonSpeed = 0.5f;
		titsPistonSpeed = 0.5f;
		vaginaOpenSpeed = 0f;
		horseRideSpeed = 0f;
		horseRidePower = 0f;
		limbHoldAngle = 0f;
		bedOpenAngle = 0f;
		vaginaPistonSlider.value = vaginaPistonSpeed;
		analPistonSlider.value = analPistonSpeed;
		mouthPistonSlider.value = mouthPistonSpeed;
		titsPistonSlider.value = titsPistonSpeed;
		vaginaOpenSlider.value = vaginaOpenSpeed;
		horseRideSpeedSlider.value = horseRideSpeed;
		horseRidePowerSlider.value = horseRidePower;
		limbHoldAngleSlider.value = limbHoldAngle;
		wartBedOpenSlider.value = bedOpenAngle;
		_characterAnimationSpeedManager.ChangeVaginaPiston(vaginaPistonSpeed);
		_characterAnimationSpeedManager.ChangeAnalPiston(analPistonSpeed);
		_characterAnimationSpeedManager.ChangeMouthPiston(mouthPistonSpeed);
		_characterAnimationSpeedManager.ChangeTitsPiston(titsPistonSpeed);
		_characterAnimationSpeedManager.ChangeVaginaOpen(vaginaOpenSpeed);
		_characterAnimationSpeedManager.ChangeHorseRideSpeed(horseRideSpeed);
		_characterAnimationSpeedManager.ChangeHorseRidePower(horseRidePower);
		_characterAnimationSpeedManager.ChangeLimbHoldAngle(limbHoldAngle);
		_characterAnimationSpeedManager.ChangeBedOpenAngle(bedOpenAngle);
		UseSlide(value: true);
	}

	public void SetPage(int value)
	{
		page = value;
		if (page == 0)
		{
			gimmickPage.SetActive(value: true);
			gimmickText.color = enableColor;
			gimmickButtonGUI.defaultColor = enableColor;
			feelerDollPage.SetActive(value: false);
			feelerDollText.color = disableColor;
			feelerDollButtonGUI.defaultColor = disableColor;
		}
		else
		{
			gimmickPage.SetActive(value: false);
			gimmickText.color = disableColor;
			gimmickButtonGUI.defaultColor = disableColor;
			feelerDollPage.SetActive(value: true);
			feelerDollText.color = enableColor;
			feelerDollButtonGUI.defaultColor = enableColor;
		}
	}

	public void ChangeSkill(int value)
	{
		skillID = value;
		frameHorseRide.SetActive(value: false);
		frameHorseRide.SetActive(value: false);
		frameHoldLimb.SetActive(value: false);
		frameWartBed.SetActive(value: false);
		if (skillID == 21)
		{
			frameHorseRide.SetActive(value: true);
		}
		else if (skillID == 22)
		{
			frameHoldLimb.SetActive(value: true);
		}
		else if (skillID == 23)
		{
			frameWartBed.SetActive(value: true);
		}
	}

	public void ChangeVaginaPiston()
	{
		vaginaPistonSpeed = vaginaPistonSlider.value;
		_characterAnimationSpeedManager.ChangeVaginaPiston(vaginaPistonSpeed);
	}

	public void ChangeAnalPiston()
	{
		analPistonSpeed = analPistonSlider.value;
		_characterAnimationSpeedManager.ChangeAnalPiston(analPistonSpeed);
	}

	public void ChangeMouthPiston()
	{
		mouthPistonSpeed = mouthPistonSlider.value;
		_characterAnimationSpeedManager.ChangeMouthPiston(mouthPistonSpeed);
	}

	public void ChangeTitsPiston()
	{
		titsPistonSpeed = titsPistonSlider.value;
		_characterAnimationSpeedManager.ChangeTitsPiston(titsPistonSpeed);
	}

	public void ChangeVaginaOpen()
	{
		vaginaOpenSpeed = vaginaOpenSlider.value;
		_characterAnimationSpeedManager.ChangeVaginaOpen(vaginaOpenSpeed);
	}

	public void ChangeHorseRideSpeed()
	{
		horseRideSpeed = horseRideSpeedSlider.value;
		_characterAnimationSpeedManager.ChangeHorseRideSpeed(horseRideSpeed);
	}

	public void ChangeHorseRidePower()
	{
		horseRidePower = horseRidePowerSlider.value;
		_characterAnimationSpeedManager.ChangeHorseRidePower(horseRidePower);
	}

	public void ChangeLimbHoldAngle()
	{
		limbHoldAngle = limbHoldAngleSlider.value;
		_characterAnimationSpeedManager.ChangeLimbHoldAngle(limbHoldAngle);
	}

	public void ChangeBedOpenAngle()
	{
		bedOpenAngle = wartBedOpenSlider.value;
		_characterAnimationSpeedManager.ChangeBedOpenAngle(bedOpenAngle);
	}

	public void UseSlide(bool value)
	{
		useSliderSe = value;
	}

	public void SliderSe()
	{
	}

	public void SettingList(bool value)
	{
		for (int i = 0; i < settingFrameList.Count; i++)
		{
			settingFrameList[i].SetActive(value);
		}
	}

	public void FrameList(bool value)
	{
		for (int i = 0; i < frameList.Count; i++)
		{
			frameList[i].SetActive(value);
		}
		currentIcon.SetActive(value: false);
		if (!value && skillID > 0)
		{
			currentIcon.SetActive(value: true);
		}
	}
}
