using System.Collections.Generic;
using UnityEngine;

public class FeelerController : MonoBehaviour
{
	public static FeelerController instance;

	public SystemCore _systemCore;

	public FeelerControllerData _feelerControllerData;

	public CameraController _cameraController;

	[Header("GUI")]
	public ControlButtonManager _controlButtonManager;

	public SkillGUIManager _skillGUIManager;

	public GUIManager _GUIManager;

	public CharacterCostumeManager _characterCostumeManager;

	public SelectionManager _selectionManager;

	[Header("Status")]
	public bool initialize;

	public bool isPause;

	public bool clickMissGurad;

	[Header("Control")]
	public bool isScreenLock;

	public bool isControl;

	public bool isStageEnd;

	[Header("Se")]
	public AudioClip cursorOnSe;

	public AudioClip cursorOffSe;

	public List<AudioClip> meltPowerSe;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
	}

	private void Start()
	{
		ScreenLock(value: false);
	}

	private void Update()
	{
		if (isPause || isStageEnd)
		{
			return;
		}
		if (!clickMissGurad)
		{
			if (Input.GetKeyDown(KeyCode.Mouse0) && !isScreenLock)
			{
				_feelerControllerData.MouseCheck(0, up: false);
			}
			if (Input.GetKeyUp(KeyCode.Mouse0) && !isScreenLock)
			{
				_feelerControllerData.MouseCheck(0, up: true);
			}
			if (Input.GetKeyDown(KeyCode.Mouse1) && !isScreenLock)
			{
				_feelerControllerData.MouseCheck(1, up: false);
			}
			if (Input.GetKeyUp(KeyCode.Mouse1) && !isScreenLock)
			{
				_feelerControllerData.MouseCheck(1, up: true);
			}
			if (Input.GetKeyDown(KeyCode.Q) && !isScreenLock)
			{
				_feelerControllerData.MouseCheck(2, up: false);
			}
			if (Input.GetKeyUp(KeyCode.Q) && !isScreenLock)
			{
				_feelerControllerData.MouseCheck(2, up: true);
			}
			if (Input.GetKeyDown(KeyCode.E) && !isScreenLock)
			{
				_feelerControllerData.MouseCheck(3, up: false);
			}
			if (Input.GetKeyUp(KeyCode.E) && !isScreenLock)
			{
				_feelerControllerData.MouseCheck(3, up: true);
			}
			else if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
			{
				_feelerControllerData.KeyCheck(0, direct: false);
			}
			else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
			{
				_feelerControllerData.KeyCheck(1, direct: false);
			}
			else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
			{
				_feelerControllerData.KeyCheck(2, direct: false);
			}
			else if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
			{
				_feelerControllerData.KeyCheck(3, direct: false);
			}
			else if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5))
			{
				_feelerControllerData.KeyCheck(4, direct: false);
			}
			else if (Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Keypad6))
			{
				_feelerControllerData.KeyCheck(5, direct: false);
			}
			else if (Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Keypad7))
			{
				_feelerControllerData.KeyCheck(6, direct: false);
			}
			else if (Input.GetKeyDown(KeyCode.Alpha8) || Input.GetKeyDown(KeyCode.Keypad8))
			{
				_feelerControllerData.KeyCheck(7, direct: false);
			}
			else if (Input.GetKeyDown(KeyCode.Alpha9) || Input.GetKeyDown(KeyCode.Keypad9))
			{
				_feelerControllerData.KeyCheck(8, direct: false);
			}
			else if (Input.GetKeyDown(KeyCode.Z))
			{
				_feelerControllerData.BukkakeKeyCheck(0);
			}
			else if (Input.GetKeyDown(KeyCode.X))
			{
				_feelerControllerData.BukkakeKeyCheck(1);
			}
			else if (Input.GetKeyDown(KeyCode.C))
			{
				_feelerControllerData.BukkakeKeyCheck(2);
			}
			else if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse2))
			{
				_GUIManager.CameraMode(value: false);
				if (_systemCore.isHideGUI)
				{
					_systemCore.HideGUI(!_systemCore.isHideGUI);
					_GUIManager.CloseGUI();
				}
				if (!isScreenLock)
				{
					ScreenLock(value: true);
					_GUIManager.SetButtonList(value: true);
					_GUIManager.AnimationGUI(setting: true, frame: true);
				}
				else if (_GUIManager.isGUIOpen && isScreenLock)
				{
					EffectSeManager.instance.PlaySe(cursorOffSe);
					_GUIManager.CloseGUI();
					_GUIManager.AnimationGUI(setting: true, frame: true);
				}
				else
				{
					ScreenLock(value: false);
					_GUIManager.CloseGUI();
					_GUIManager.AnimationGUI(setting: true, frame: false);
				}
			}
			else if (Input.GetKeyDown(KeyCode.R) && !isControl)
			{
				if (!_GUIManager.isGUIOpen)
				{
					ScreenLock(value: true);
				}
				else if (_GUIManager.isSkill)
				{
					ScreenLock(value: false);
				}
				else
				{
					EffectSeManager.instance.PlaySe(cursorOnSe);
				}
				_GUIManager.SetGUI("Skill");
			}
			else if (Input.GetKeyDown(KeyCode.T) && !isControl)
			{
				if (!_GUIManager.isGUIOpen)
				{
					ScreenLock(value: true);
				}
				else if (_GUIManager.isPose)
				{
					ScreenLock(value: false);
				}
				else
				{
					EffectSeManager.instance.PlaySe(cursorOnSe);
				}
				_GUIManager.SetGUI("Pose");
			}
			else if (Input.GetKeyDown(KeyCode.Y) && !isControl)
			{
				if (!_GUIManager.isGUIOpen)
				{
					ScreenLock(value: true);
				}
				else if (_GUIManager.isAvatar)
				{
					ScreenLock(value: false);
				}
				else
				{
					EffectSeManager.instance.PlaySe(cursorOnSe);
				}
				_GUIManager.SetGUI("Avatar");
			}
			else if (Input.GetKeyDown(KeyCode.Tab) && !isControl)
			{
				if (!_GUIManager.isGUIOpen)
				{
					ScreenLock(value: true);
				}
				else if (_GUIManager.isTorso)
				{
					ScreenLock(value: false);
				}
				else
				{
					EffectSeManager.instance.PlaySe(cursorOnSe);
				}
				_GUIManager.SetGUI("Torso");
			}
			else if (Input.GetKeyDown(KeyCode.J) && !isControl)
			{
				if (!_GUIManager.isGUIOpen)
				{
					ScreenLock(value: true);
				}
				else if (_GUIManager.isJobList)
				{
					ScreenLock(value: false);
				}
				else
				{
					EffectSeManager.instance.PlaySe(cursorOnSe);
				}
				_GUIManager.SetGUI("JobList");
			}
			else if (Input.GetKeyDown(KeyCode.G) && !isControl)
			{
				_systemCore.HideGUI(!_systemCore.isHideGUI);
				if (_systemCore.isHideGUI)
				{
					ScreenLock(value: false);
					_GUIManager.CloseGUI();
					_GUIManager.CameraMode(value: true);
				}
				else
				{
					EffectSeManager.instance.PlaySe(cursorOnSe);
					_GUIManager.CloseGUI();
					_GUIManager.CameraMode(value: false);
				}
			}
			else if (Input.GetKeyDown(KeyCode.V) && !isControl)
			{
				_selectionManager.SelectionVaginaSlide(!_selectionManager.isSelectionVagina);
			}
			else if (Input.GetKeyDown(KeyCode.B) && !isControl)
			{
				_selectionManager.SelectionAnalSlide(!_selectionManager.isSelectionAnal);
			}
		}
		else
		{
			clickMissGurad = false;
		}
	}

	public void StageEnd()
	{
		ScreenLock(value: false);
		_GUIManager.CloseGUI();
		_GUIManager.AnimationGUI(setting: true, frame: false);
	}

	public void HideGUI()
	{
		clickMissGurad = true;
		_systemCore.HideGUI(!_systemCore.isHideGUI);
		if (_systemCore.isHideGUI)
		{
			ScreenLock(value: false);
			_GUIManager.CloseGUI();
			_GUIManager.CameraMode(value: true);
		}
		else
		{
			EffectSeManager.instance.PlaySe(cursorOnSe);
			_GUIManager.CloseGUI();
			_GUIManager.CameraMode(value: false);
		}
	}

	public void ScreenLock(bool value)
	{
		if (initialize)
		{
			if (value)
			{
				EffectSeManager.instance.PlaySe(cursorOnSe);
				_controlButtonManager.ControlMode("Cursor");
				GUIManager.instance.CursorFrame(value: true);
				_feelerControllerData.MouseReset();
			}
			else
			{
				EffectSeManager.instance.PlaySe(cursorOffSe);
				_controlButtonManager.ControlMode("Cancel");
				GUIManager.instance.CursorFrame(value: false);
				SkillGUIInformation.instance.InfomationOff();
			}
		}
		initialize = true;
		isScreenLock = value;
		_systemCore.Coursor(isScreenLock);
		_cameraController.isScreenLock = isScreenLock;
		_GUIManager.isScreenLock = isScreenLock;
	}

	public void ControlOn()
	{
		isControl = true;
		_feelerControllerData.isControl = true;
	}

	public void ControlOff()
	{
		isControl = false;
		_feelerControllerData.isControl = false;
	}
}
