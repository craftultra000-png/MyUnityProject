using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utage;

public class MissionSelectManager : MonoBehaviour
{
	[Header("Start Button")]
	public ButtonTriggerGUI startButton;

	public GameObject startBlinkEffect;

	public GameObject startSetEffect;

	public List<GameObject> missionSetEffect;

	public bool isStartCheck;

	public bool isSameCheck;

	public bool isUnUseCheck;

	[Header("Current Mission Name")]
	public List<Text> missionCurrentText;

	public List<UguiLocalize> missionCurrentTextLocalize;

	public List<TextMeshProUGUI> missionCurrentCountText;

	public List<string> missionCurrentName;

	public List<int> missionCurrentNumber;

	public List<ButtonTriggerGUI> missionCurrentButton;

	public List<Image> missionCurrentIcon;

	[Header("Mission Name")]
	public List<Text> missionText;

	public List<UguiLocalize> missionTextLocalize;

	public List<TextMeshProUGUI> missionCountText;

	public List<string> missionName;

	public List<ButtonTriggerGUI> missionButton;

	public List<Image> missionIcon;

	[Header("Mission Icon")]
	public Sprite missionNullIcon;

	[Header("Mission Data")]
	public MissionData missionData;

	[Header("Mission Count")]
	public List<int> missionCount;

	public List<int> missionStart;

	public List<int> missionEnd;

	[Header("Magic Effect")]
	public Transform magicStocker;

	public GameObject magicDesignEffect;

	public GameObject magicDesignEndEffect;

	public GameObject magicDesignStartEffect;

	public Vector3 magicRotation = new Vector3(-90f, 0f, 0f);

	[Header("Color")]
	public Color defaultColor;

	public Color defaultEnterColor;

	public Color defaultUnUseColor;

	public Color selectColor;

	public Color selectEnterColor;

	public Color selectUnUseColor;

	[Header("SE")]
	public AudioSource _audioSourceSE;

	public AudioClip startSe;

	private void Start()
	{
		startButton.unuse = true;
		startBlinkEffect.SetActive(value: false);
		startSetEffect.SetActive(value: false);
		for (int i = 0; i < missionSetEffect.Count; i++)
		{
			missionSetEffect[i].SetActive(value: false);
		}
		for (int j = 0; j < missionCurrentText.Count; j++)
		{
			SetNull(j);
		}
		for (int k = 0; k < missionText.Count; k++)
		{
			SetMissionData(k);
		}
		magicDesignEffect.SetActive(value: false);
		magicDesignStartEffect.SetActive(value: false);
	}

	private void OnDisable()
	{
		startSetEffect.SetActive(value: false);
		for (int i = 0; i < missionSetEffect.Count; i++)
		{
			missionSetEffect[i].SetActive(value: false);
		}
	}

	public void SetNull(int num)
	{
		missionCurrentText[num].text = "????";
		missionCurrentTextLocalize[num].Key = "????";
		missionCurrentCountText[num].text = "?";
		missionCurrentName[num] = "????";
		missionCurrentNumber[num] = -1;
		missionCurrentIcon[num].sprite = missionNullIcon;
		missionCurrentButton[num].unuse = true;
		missionCurrentButton[num].Unuse();
		NullMissionCheck();
	}

	public void SetMissionData(int num)
	{
		if (missionData.missionCount[num] < 0)
		{
			missionText[num].text = "????";
			missionTextLocalize[num].Key = "????";
			missionCountText[num].text = "?";
			missionName[num] = "????";
			missionIcon[num].sprite = missionNullIcon;
		}
		else
		{
			missionText[num].text = missionData.missionName[num];
			missionTextLocalize[num].Key = missionData.missionName[num];
			missionCountText[num].text = "0 / " + missionData.missionCount[num];
			missionName[num] = missionData.missionName[num];
			missionIcon[num].sprite = missionData.missionSprite[num];
		}
	}

	public void SetMission(int num)
	{
		if (missionData.missionCount[num] <= 0)
		{
			return;
		}
		isSameCheck = false;
		for (int i = 0; i < missionCurrentNumber.Count; i++)
		{
			if (missionCurrentNumber[i] == num)
			{
				isSameCheck = true;
				SetNull(i);
				break;
			}
		}
		if (isSameCheck)
		{
			return;
		}
		for (int j = 0; j < missionCurrentNumber.Count; j++)
		{
			if (missionCurrentNumber[j] < 0)
			{
				missionCurrentText[j].text = missionData.missionName[num];
				missionCurrentTextLocalize[j].Key = missionData.missionName[num];
				missionCurrentCountText[j].text = "0 / " + missionData.missionCount[num];
				missionCurrentName[j] = missionData.missionName[num];
				missionCurrentNumber[j] = num;
				missionCurrentIcon[j].sprite = missionData.missionSprite[num];
				missionCurrentButton[j].unuse = false;
				missionCurrentButton[j].ColorReset();
				missionSetEffect[j].SetActive(value: true);
				break;
			}
		}
		NullMissionCheck();
	}

	public void NullMissionCheck()
	{
		isStartCheck = false;
		for (int i = 0; i < missionCurrentNumber.Count; i++)
		{
			if (missionCurrentNumber[i] < 0)
			{
				isStartCheck = true;
				break;
			}
		}
		MissionButton();
		if (!isStartCheck)
		{
			if (!magicDesignEffect.activeSelf)
			{
				startButton.unuse = false;
				startButton.ColorReset();
				startBlinkEffect.SetActive(value: true);
				startSetEffect.SetActive(value: true);
				_audioSourceSE.PlayOneShot(startSe);
				Debug.LogError("Save Mission List");
				ES3.Save("MissionList", missionCurrentNumber);
				magicDesignEffect.SetActive(value: true);
			}
		}
		else
		{
			startButton.unuse = true;
			startButton.Unuse();
			startBlinkEffect.SetActive(value: false);
			startSetEffect.SetActive(value: false);
			if (magicDesignEffect.activeSelf)
			{
				Object.Instantiate(magicDesignEndEffect, magicStocker.position, Quaternion.Euler(magicRotation), magicStocker);
			}
			magicDesignEffect.SetActive(value: false);
		}
	}

	public void MissionButton()
	{
		if (isStartCheck)
		{
			for (int i = 0; i < missionButton.Count; i++)
			{
				missionButton[i].unuse = false;
				missionButton[i].defaultColor = defaultColor;
				missionButton[i].pointerEnterColor = defaultEnterColor;
				missionButton[i].mouseClickColor = defaultEnterColor;
				missionButton[i].unUseColor = defaultUnUseColor;
				missionButton[i].ColorReset();
			}
		}
		else
		{
			for (int j = 0; j < missionButton.Count; j++)
			{
				missionButton[j].unuse = true;
				missionButton[j].Unuse();
			}
		}
		for (int k = 0; k < missionCurrentNumber.Count; k++)
		{
			for (int l = 0; l < missionButton.Count; l++)
			{
				if (l == missionCurrentNumber[k])
				{
					missionButton[l].unuse = false;
					missionButton[l].defaultColor = selectColor;
					missionButton[l].pointerEnterColor = selectEnterColor;
					missionButton[l].mouseClickColor = selectEnterColor;
					missionButton[l].unUseColor = selectUnUseColor;
					missionButton[l].ColorReset();
				}
			}
		}
	}

	public void MagicStart()
	{
		magicDesignStartEffect.SetActive(value: true);
	}
}
