using UnityEngine;
using UnityEngine.UI;

public class ControlButtonManager : MonoBehaviour
{
	public static ControlButtonManager instance;

	public FeelerController _feelerController;

	public GUIManager _GUIManager;

	[Header("Status")]
	public string mode;

	[Header("GUI Icon")]
	public GameObject skillIcon;

	public GameObject torsoIcon;

	public GameObject poseIcon;

	public GameObject jobIcon;

	public Image cursorImage;

	public Sprite cursorSprite;

	public Sprite canselSprite;

	[Header("Icon Effect")]
	public GameObject cursorEffect;

	public GameObject skillEffect;

	public GameObject torsoEffect;

	public GameObject poseEffect;

	public GameObject avatarEffect;

	public GameObject jobListEffect;

	public void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
	}

	private void Start()
	{
		ControlMode("Cursor");
	}

	public void Cursor()
	{
		if (_GUIManager.isGUIOpen && _feelerController.isScreenLock)
		{
			EffectSeManager.instance.PlaySe(_feelerController.cursorOffSe);
			_GUIManager.CloseGUI();
			_GUIManager.AnimationGUI(setting: true, frame: true);
		}
		else
		{
			_feelerController.ScreenLock(value: false);
			_feelerController.clickMissGurad = true;
			_GUIManager.CloseGUI();
			_GUIManager.AnimationGUI(setting: true, frame: false);
		}
	}

	public void Skill()
	{
		if (!_GUIManager.isSkill)
		{
			EffectSeManager.instance.PlaySe(_feelerController.cursorOnSe);
		}
		else
		{
			EffectSeManager.instance.PlaySe(_feelerController.cursorOffSe);
		}
		_GUIManager.SetGUI("Skill");
	}

	public void Torso()
	{
		if (!_GUIManager.isTorso)
		{
			EffectSeManager.instance.PlaySe(_feelerController.cursorOnSe);
		}
		else
		{
			EffectSeManager.instance.PlaySe(_feelerController.cursorOffSe);
		}
		_GUIManager.SetGUI("Torso");
	}

	public void Pose()
	{
		if (!_GUIManager.isPose)
		{
			EffectSeManager.instance.PlaySe(_feelerController.cursorOnSe);
		}
		else
		{
			EffectSeManager.instance.PlaySe(_feelerController.cursorOffSe);
		}
		_GUIManager.SetGUI("Pose");
	}

	public void Avatar()
	{
		if (!_GUIManager.isAvatar)
		{
			EffectSeManager.instance.PlaySe(_feelerController.cursorOnSe);
		}
		else
		{
			EffectSeManager.instance.PlaySe(_feelerController.cursorOffSe);
		}
		_GUIManager.SetGUI("Avatar");
	}

	public void JobList()
	{
		if (!_GUIManager.isAvatar)
		{
			EffectSeManager.instance.PlaySe(_feelerController.cursorOnSe);
		}
		else
		{
			EffectSeManager.instance.PlaySe(_feelerController.cursorOffSe);
		}
		_GUIManager.SetGUI("JobList");
	}

	public void ControlMode(string value)
	{
		mode = value;
		if (mode == "Cursor")
		{
			cursorImage.sprite = canselSprite;
		}
		else
		{
			cursorImage.sprite = cursorSprite;
		}
	}

	public void SetEffect(string type, bool value)
	{
		cursorEffect.SetActive(value: false);
		skillEffect.SetActive(value: false);
		torsoEffect.SetActive(value: false);
		poseEffect.SetActive(value: false);
		avatarEffect.SetActive(value: false);
		jobListEffect.SetActive(value: false);
		if (type == "Torso" && value)
		{
			torsoEffect.SetActive(value: true);
		}
		if (type == "Skill" && value)
		{
			skillEffect.SetActive(value: true);
		}
		if (type == "Pose" && value)
		{
			poseEffect.SetActive(value: true);
		}
		if (type == "Avatar" && value)
		{
			avatarEffect.SetActive(value: true);
		}
		if (type == "JobList" && value)
		{
			jobListEffect.SetActive(value: true);
		}
	}
}
