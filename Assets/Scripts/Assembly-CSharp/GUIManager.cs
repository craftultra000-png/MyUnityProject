using System.Collections.Generic;
using UnityEngine;

public class GUIManager : MonoBehaviour
{
	public static GUIManager instance;

	public MotionAnimancer _motionAnimancer;

	public ReactionAnimancer _reactionAnimancer;

	public ControlButtonManager _controlButtonManager;

	public SkillGUIManager _skillGUIManager;

	public MissionGUIManager _missionGUIManager;

	public HeartBeatManager _heartBeatManager;

	public GuildCardObject _guildCardObject;

	[Header("GUI")]
	public GameObject skillGUI;

	public GameObject torsoGUI;

	public GameObject poseGUI;

	public GameObject avatarGUI;

	public GameObject jobListGUI;

	public QuickSkillGUI _quickSkillGUI;

	public GameObject selectionGUI;

	public GameObject countGUI;

	[Header("Button")]
	public ButtonTriggerGUI countButton;

	public ButtonTriggerGUI heatBeatButton;

	public List<GameObject> buttonList;

	[Header("Cursor On")]
	public GameObject cursorOnFrame;

	public GameObject cursorOffFrame;

	[Header("Control")]
	public bool isScreenLock;

	public bool isCameraMode;

	[Header("Status")]
	public bool isGUIOpen;

	public bool isSkill;

	public bool isTorso;

	public bool isPose;

	public bool isAvatar;

	public bool isJobList;

	public bool isCount;

	public bool isHeartBeat;

	public int heartBeatType;

	[Header("Color")]
	public Color enableColor;

	public Color disableColor;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		CloseGUI();
		cursorOnFrame.SetActive(value: false);
		cursorOffFrame.SetActive(value: true);
		for (int i = 0; i < buttonList.Count; i++)
		{
			buttonList[i].SetActive(value: true);
		}
		_guildCardObject.isGUIOpen = false;
		_quickSkillGUI.gameObject.SetActive(value: true);
	}

	private void Start()
	{
		isCount = false;
		SetDamageCount(change: true);
		isHeartBeat = false;
		heartBeatType = 0;
		SetHeartBeat(change: true);
	}

	public void CloseGUI()
	{
		isGUIOpen = false;
		isSkill = false;
		isTorso = false;
		isPose = false;
		isAvatar = false;
		isJobList = false;
		skillGUI.SetActive(value: false);
		torsoGUI.SetActive(value: false);
		poseGUI.SetActive(value: false);
		avatarGUI.SetActive(value: false);
		jobListGUI.SetActive(value: false);
		SkillGUIInformation.instance.InfomationOff();
		SkillGUIManager.instance.SkillGUIOff();
		_controlButtonManager.SetEffect("", isGUIOpen);
		SelectionManager.instance.SelectionHideGUI(value: false);
		SetDamageCount(change: false);
		SetHeartBeat(change: false);
		SetButtonList(value: true);
	}

	public void CameraMode(bool value)
	{
		isCameraMode = !value;
		selectionGUI.SetActive(isCameraMode);
	}

	public void CursorFrame(bool value)
	{
		cursorOnFrame.SetActive(value);
		cursorOffFrame.SetActive(!value);
	}

	public void AnimationGUI(bool setting, bool frame)
	{
		_quickSkillGUI.SettingList(setting);
		_quickSkillGUI.FrameList(frame);
		if (setting && frame)
		{
			_missionGUIManager.FinishButton(value: true);
		}
		else
		{
			_missionGUIManager.FinishButton(value: false);
		}
	}

	public void SetButtonList(bool value)
	{
		_guildCardObject.isGUIOpen = !value;
		for (int i = 0; i < buttonList.Count; i++)
		{
			buttonList[i].SetActive(value);
		}
	}

	public void SetDamageCount(bool change)
	{
		if (change)
		{
			isCount = !isCount;
		}
		if (!isGUIOpen)
		{
			countGUI.SetActive(isCount);
		}
		if (isCount)
		{
			countButton.defaultColor = enableColor;
			countButton.ColorReset();
		}
		else
		{
			countButton.defaultColor = disableColor;
			countButton.ColorReset();
		}
	}

	public void DisableDamageCount()
	{
		countGUI.SetActive(value: false);
	}

	public void SetHeartBeat(bool change)
	{
		if (change)
		{
			if (heartBeatType == 0)
			{
				heartBeatType = 1;
			}
			else if (heartBeatType == 1)
			{
				heartBeatType = 2;
			}
			else if (heartBeatType == 2)
			{
				heartBeatType = 0;
			}
		}
		if (!isGUIOpen)
		{
			if (heartBeatType != 0)
			{
				_heartBeatManager.SetHeartBeat(value: true, heartBeatType);
			}
			else
			{
				_heartBeatManager.SetHeartBeat(value: false, heartBeatType);
			}
		}
		if (heartBeatType != 0)
		{
			heatBeatButton.defaultColor = enableColor;
			heatBeatButton.ColorReset();
		}
		else
		{
			heatBeatButton.defaultColor = disableColor;
			heatBeatButton.ColorReset();
		}
	}

	public void DisableHeartBeat()
	{
		_heartBeatManager.DisableHeartBeat();
	}

	public void SetGUI(string value)
	{
		_skillGUIManager.SkillGUIOff();
		switch (value)
		{
		case "Skill":
			isSkill = !isSkill;
			if (isSkill)
			{
				isGUIOpen = true;
			}
			else
			{
				isGUIOpen = false;
			}
			isTorso = false;
			isPose = false;
			isAvatar = false;
			isJobList = false;
			_controlButtonManager.SetEffect(value, isSkill);
			break;
		case "Torso":
			isTorso = !isTorso;
			if (isTorso)
			{
				isGUIOpen = true;
			}
			else
			{
				isGUIOpen = false;
			}
			isSkill = false;
			isPose = false;
			isAvatar = false;
			isJobList = false;
			_controlButtonManager.SetEffect(value, isTorso);
			break;
		case "Pose":
			isPose = !isPose;
			if (isPose)
			{
				isGUIOpen = true;
			}
			else
			{
				isGUIOpen = false;
			}
			isSkill = false;
			isTorso = false;
			isAvatar = false;
			isJobList = false;
			_controlButtonManager.SetEffect(value, isPose);
			break;
		case "Avatar":
			isAvatar = !isAvatar;
			if (isAvatar)
			{
				isGUIOpen = true;
			}
			else
			{
				isGUIOpen = false;
			}
			isSkill = false;
			isPose = false;
			isTorso = false;
			isJobList = false;
			_controlButtonManager.SetEffect(value, isAvatar);
			break;
		case "JobList":
			isJobList = !isJobList;
			if (isJobList)
			{
				isGUIOpen = true;
			}
			else
			{
				isGUIOpen = false;
			}
			isSkill = false;
			isPose = false;
			isTorso = false;
			isAvatar = false;
			_controlButtonManager.SetEffect(value, isAvatar);
			break;
		}
		skillGUI.SetActive(isSkill);
		torsoGUI.SetActive(isTorso);
		poseGUI.SetActive(isPose);
		avatarGUI.SetActive(isAvatar);
		jobListGUI.SetActive(isJobList);
		if (isSkill)
		{
			AnimationGUI(setting: false, frame: false);
			SelectionManager.instance.SelectionHideGUI(value: true);
			DisableDamageCount();
			DisableHeartBeat();
			SetButtonList(value: false);
		}
		else if (isTorso)
		{
			AnimationGUI(setting: false, frame: false);
			SelectionManager.instance.SelectionHideGUI(value: true);
			DisableDamageCount();
			DisableHeartBeat();
			SetButtonList(value: false);
		}
		else if (isPose)
		{
			AnimationGUI(setting: false, frame: false);
			SelectionManager.instance.SelectionHideGUI(value: true);
			DisableDamageCount();
			DisableHeartBeat();
			SetButtonList(value: false);
		}
		else if (isAvatar)
		{
			AnimationGUI(setting: false, frame: false);
			SelectionManager.instance.SelectionHideGUI(value: true);
			DisableDamageCount();
			DisableHeartBeat();
			SetButtonList(value: false);
		}
		else if (isJobList)
		{
			AnimationGUI(setting: false, frame: false);
			SelectionManager.instance.SelectionHideGUI(value: true);
			DisableDamageCount();
			DisableHeartBeat();
			SetButtonList(value: false);
		}
		else if (isScreenLock)
		{
			AnimationGUI(setting: true, frame: true);
			SelectionManager.instance.SelectionHideGUI(value: false);
			SetDamageCount(change: false);
			SetHeartBeat(change: false);
			SetButtonList(value: true);
		}
		else
		{
			AnimationGUI(setting: true, frame: false);
			SelectionManager.instance.SelectionHideGUI(value: false);
			SetDamageCount(change: false);
			SetHeartBeat(change: false);
			SetButtonList(value: true);
		}
		CameraMode(value: false);
	}

	public void SetPose()
	{
		Debug.LogError("SetPose GUIManager");
		_motionAnimancer.PoseChange(0, expend: false, 0);
		_reactionAnimancer.PoseChange(0, expend: false);
	}
}
