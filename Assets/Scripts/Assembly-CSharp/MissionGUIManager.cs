using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Utage;

public class MissionGUIManager : MonoBehaviour
{
	public static MissionGUIManager instance;

	public CharacterCountGUI _characterCountGUI;

	public EXPGUIManager _EXPGUIManager;

	public GUIManager _GUIManager;

	[Header("Status")]
	public bool isFinish;

	[Header("Mission Object")]
	public Transform missionStocker;

	public GameObject _missionObject;

	public List<GameObject> _missionObjectList;

	public List<MissionGUIObject> _missionGUIObject;

	public List<bool> missionClearList;

	[Header("Positon Calc")]
	public List<float> missionPositon;

	public float missionStartPositon;

	[Header("Mission Type")]
	public List<int> missionType;

	public List<int> missionPlusType;

	[Header("Mission Name")]
	public UguiLocalize missionTextLocalize;

	public TextMeshProUGUI missionText;

	public List<string> missionName;

	[Header("Mission Data")]
	public MissionData missionData;

	[Header("Mission Done")]
	public bool isNaked;

	[Header("Mission Count")]
	public List<int> missionCountData;

	public List<int> missionCount;

	public List<int> missionStart;

	public List<int> missionEnd;

	[Header("Mission Finish")]
	public GameObject missionFinishButton;

	public GameObject missionFinishButtonNormal;

	public GameObject missionFinishButtonClear;

	public GameObject missionFinishSelectButton;

	[Header("Color")]
	public Color defaultColor;

	public Color clearColor;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		if (!ES3.KeyExists("MissionList"))
		{
			ES3.Save("MissionList", missionType);
		}
		missionType = ES3.Load<List<int>>("MissionList");
		missionPlusType.Clear();
		for (int i = 0; i < missionType.Count; i++)
		{
			missionPlusType.Add(missionData.missionPulsType[missionType[i]]);
		}
	}

	private void Start()
	{
		missionTextLocalize.Key = "Mission";
		isFinish = false;
		FinishButton(isFinish);
	}

	private void LateUpdate()
	{
		for (int i = 0; i < _missionGUIObject.Count; i++)
		{
			if (missionType[i] == 0)
			{
				missionCount[i] = _characterCountGUI.bukkakeCount;
			}
			else if (missionType[i] == 1 && !isNaked)
			{
				missionStart[i] = 0;
				missionEnd[i] = _characterCountGUI.wearCount;
				missionCount[i] = _characterCountGUI.nakedCount;
				if (missionEnd[i] == missionCount[i])
				{
					isNaked = true;
				}
				if (_missionGUIObject[i].countEnd != missionEnd[i])
				{
					_missionGUIObject[i].countEnd = missionEnd[i];
					_missionGUIObject[i].countCurrentOld = -1;
				}
			}
			else if (missionType[i] == 2)
			{
				missionCount[i] = _characterCountGUI.spankingCount;
			}
			else if (missionType[i] == 3)
			{
				missionCount[i] = _characterCountGUI.syringeCount;
			}
			else if (missionType[i] == 4)
			{
				missionCount[i] = _characterCountGUI.touchCount;
			}
			else if (missionType[i] == 5)
			{
				missionCount[i] = _characterCountGUI.pistonCount;
			}
			else if (missionType[i] == 6)
			{
				if (missionCount[i] == 0 && _characterCountGUI.isLostVirgin)
				{
					Debug.LogError("Lost Virsin Misson True");
					missionCount[i]++;
				}
			}
			else if (missionType[i] == 7)
			{
				missionCount[i] = _characterCountGUI.orgasmCount;
			}
			else if (missionType[i] == 8)
			{
				missionCount[i] = _characterCountGUI.vaginaChildCount + _characterCountGUI.analChildCount;
			}
			else if (missionType[i] == 9)
			{
				missionCount[i] = (int)_characterCountGUI.titsMilkCount;
			}
			else if (missionType[i] == 10)
			{
				missionCount[i] = _characterCountGUI.playerTitsCount;
			}
			else if (missionType[i] == 11)
			{
				missionCount[i] = _characterCountGUI.playerHipCount;
			}
		}
	}

	public void StartMission()
	{
		Debug.LogError("Mission Start");
		missionName = new List<string>(missionData.missionName);
		missionCountData = new List<int>(missionData.missionCount);
		missionCount.Clear();
		missionStart.Clear();
		missionEnd.Clear();
		for (int i = 0; i < missionType.Count; i++)
		{
			SetMission(i, missionType[i]);
		}
	}

	public void SetMissionType(int missionNumber, int missionType, MissionGUIObject script)
	{
		Debug.LogError("Set Mission Type: " + missionType + " Number:" + missionNumber);
		script.missionName = missionName[missionType];
		script.missionNumber = missionNumber;
		script.missionType = missionType;
		switch (missionType)
		{
		case 0:
			missionStart[missionNumber] = _characterCountGUI.bukkakeCount;
			missionEnd[missionNumber] = missionStart[missionNumber] + missionCount[missionNumber];
			break;
		case 1:
			missionStart[missionNumber] = 0;
			missionEnd[missionNumber] = _characterCountGUI.wearCount;
			break;
		case 2:
			missionStart[missionNumber] = _characterCountGUI.spankingCount;
			missionEnd[missionNumber] = missionStart[missionNumber] + missionCount[missionNumber];
			break;
		case 3:
			missionStart[missionNumber] = _characterCountGUI.syringeCount;
			missionEnd[missionNumber] = missionStart[missionNumber] + missionCount[missionNumber];
			break;
		case 4:
			missionStart[missionNumber] = _characterCountGUI.touchCount;
			missionEnd[missionNumber] = missionStart[missionNumber] + missionCount[missionNumber];
			break;
		case 5:
			missionStart[missionNumber] = _characterCountGUI.pistonCount;
			missionEnd[missionNumber] = missionStart[missionNumber] + missionCount[missionNumber];
			break;
		case 6:
			missionCount[missionNumber] = 0;
			missionStart[missionNumber] = 0;
			missionEnd[missionNumber] = 1;
			break;
		case 7:
			missionStart[missionNumber] = _characterCountGUI.orgasmCount;
			missionEnd[missionNumber] = missionStart[missionNumber] + missionCount[missionNumber];
			break;
		case 8:
			missionStart[missionNumber] = _characterCountGUI.vaginaChildCount + _characterCountGUI.analChildCount;
			missionEnd[missionNumber] = missionStart[missionNumber] + missionCount[missionNumber];
			break;
		case 9:
			missionStart[missionNumber] = (int)_characterCountGUI.titsMilkCount;
			missionEnd[missionNumber] = missionStart[missionNumber] + missionCount[missionNumber];
			break;
		case 10:
			missionStart[missionNumber] = _characterCountGUI.playerTitsCount;
			missionEnd[missionNumber] = missionStart[missionNumber] + missionCount[missionNumber];
			break;
		case 11:
			missionStart[missionNumber] = _characterCountGUI.playerHipCount;
			missionEnd[missionNumber] = missionStart[missionNumber] + missionCount[missionNumber];
			break;
		default:
			Debug.LogError("Mission Type is Nothing.", base.gameObject);
			break;
		}
		script.countStart = missionStart[missionNumber];
		script.countEnd = missionEnd[missionNumber];
	}

	public void SetMission(int number, int type)
	{
		if (_missionObjectList.Count < 3)
		{
			GameObject gameObject = Object.Instantiate(_missionObject, Vector3.zero, Quaternion.identity, missionStocker);
			gameObject.GetComponent<RectTransform>().localPosition = new Vector3(0f, missionStartPositon, 0f);
			MissionGUIObject component = gameObject.GetComponent<MissionGUIObject>();
			component.isStart = true;
			component._missionGUIManager = this;
			component.missionStartPositon = missionStartPositon;
			component.missionTargetPositon = missionPositon[_missionObjectList.Count];
			missionCount.Add(missionCountData[type]);
			missionStart.Add(0);
			missionEnd.Add(0);
			SetMissionType(number, type, component);
			component.missionTypeIcon = missionData.missionSprite[type];
			_missionObjectList.Add(gameObject);
			_missionGUIObject.Add(component);
			missionClearList.Add(item: false);
		}
	}

	public void ClearMission(int type)
	{
		missionClearList[type] = true;
		bool flag = true;
		for (int i = 0; i < missionClearList.Count; i++)
		{
			if (!missionClearList[i])
			{
				flag = false;
			}
		}
		if (flag)
		{
			missionTextLocalize.Key = "Mission Complete";
			missionText.color = clearColor;
			isFinish = true;
			if (_GUIManager.isScreenLock && !_GUIManager.isGUIOpen)
			{
				FinishButton(value: true);
			}
		}
	}

	public void FinishButton(bool value)
	{
		if (value && isFinish)
		{
			missionFinishButtonNormal.SetActive(value: false);
			missionFinishButtonClear.SetActive(value: true);
			missionFinishSelectButton.SetActive(value: false);
		}
		else if (value && !isFinish)
		{
			missionFinishButtonNormal.SetActive(value: true);
			missionFinishButtonClear.SetActive(value: false);
			missionFinishSelectButton.SetActive(value: false);
		}
		else
		{
			missionFinishButtonNormal.SetActive(value: false);
			missionFinishButtonClear.SetActive(value: false);
			missionFinishSelectButton.SetActive(value: false);
		}
	}

	public void DestroyMission(MissionGUIObject script)
	{
		for (int i = 0; i < _missionGUIObject.Count; i++)
		{
			if (_missionGUIObject[i] == script)
			{
				_missionGUIObject.RemoveAt(i);
				_missionObjectList.RemoveAt(i);
				missionClearList.RemoveAt(i);
				break;
			}
		}
	}
}
