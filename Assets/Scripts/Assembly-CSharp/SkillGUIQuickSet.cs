using UnityEngine;
using UtageExtensions;

public class SkillGUIQuickSet : MonoBehaviour
{
	public static SkillGUIQuickSet instance;

	public SkillGUIManager _skillGUIManager;

	public SkillData _skillData;

	public Camera cameraGUI;

	[Header("Status")]
	public bool useClick;

	public bool isShow;

	public bool isQuickOn;

	public bool isQuickClick;

	[Header("Quick Data")]
	public GameObject mouseInfo;

	public GameObject keyInfo;

	public RectTransform mouseFrame;

	public RectTransform keyFrame;

	[Header("Quick Panel")]
	public RectTransform _rectTransform;

	public RectTransform _iconRectTransform;

	public Vector2 frameSize = new Vector2(200f, 100f);

	public Vector2 iconSize = new Vector2(60f, 60f);

	public Vector2 offset = new Vector2(10f, 10f);

	public Vector2 screenSize;

	[Header("Quick Calc")]
	public Vector2 potisionCurrent;

	public Vector2 potisionCalc;

	public Vector2 frameCalc;

	public Vector2 screenCalc;

	[Header("SE")]
	public AudioClip dragSe;

	public AudioClip successSe;

	public AudioClip failSe;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
	}

	private void Start()
	{
	}

	private void Update()
	{
		if (isShow && !RectTransformUtility.RectangleContainsScreenPoint(_iconRectTransform, Input.mousePosition, cameraGUI))
		{
			QuickOff(sound: false);
			_skillGUIManager.BlinkMouse(value: false);
			_skillGUIManager.BlinkKey(value: false);
		}
		if (Input.GetKeyUp(KeyCode.Mouse0) && useClick)
		{
			_skillGUIManager.SetMouseSkill(0, _skillData);
			SetSkill();
		}
		else if (Input.GetKeyUp(KeyCode.Mouse1) && useClick && !isQuickClick)
		{
			isQuickClick = true;
			isQuickOn = false;
			_skillGUIManager.SetMouseSkill(1, _skillData);
			SetSkill();
		}
		else if (Input.GetKeyUp(KeyCode.Q) && useClick)
		{
			_skillGUIManager.SetMouseSkill(2, _skillData);
			SetSkill();
		}
		else if (Input.GetKeyUp(KeyCode.E) && useClick)
		{
			_skillGUIManager.SetMouseSkill(3, _skillData);
			SetSkill();
		}
		else if (Input.GetKeyUp(KeyCode.Alpha1) && !useClick)
		{
			_skillGUIManager.SetKeySkill(0, _skillData);
			SetSkill();
		}
		else if (Input.GetKeyUp(KeyCode.Alpha2) && !useClick)
		{
			_skillGUIManager.SetKeySkill(1, _skillData);
			SetSkill();
		}
		else if (Input.GetKeyUp(KeyCode.Alpha3) && !useClick)
		{
			_skillGUIManager.SetKeySkill(2, _skillData);
			SetSkill();
		}
		else if (Input.GetKeyUp(KeyCode.Alpha4) && !useClick)
		{
			_skillGUIManager.SetKeySkill(3, _skillData);
			SetSkill();
		}
		else if (Input.GetKeyUp(KeyCode.Alpha5) && !useClick)
		{
			_skillGUIManager.SetKeySkill(4, _skillData);
			SetSkill();
		}
		else if (Input.GetKeyUp(KeyCode.Alpha6) && !useClick)
		{
			_skillGUIManager.SetKeySkill(5, _skillData);
			SetSkill();
		}
		else if (Input.GetKeyUp(KeyCode.Alpha7) && !useClick)
		{
			_skillGUIManager.SetKeySkill(6, _skillData);
			SetSkill();
		}
		else if (Input.GetKeyUp(KeyCode.Alpha8) && !useClick)
		{
			_skillGUIManager.SetKeySkill(7, _skillData);
			SetSkill();
		}
		else if (Input.GetKeyUp(KeyCode.Alpha9) && !useClick)
		{
			_skillGUIManager.SetKeySkill(8, _skillData);
			SetSkill();
		}
	}

	public void SetSkill()
	{
		QuickOff(sound: false);
		_skillGUIManager.BlinkMouse(value: false);
		_skillGUIManager.BlinkKey(value: false);
		_skillGUIManager.CheckSameSkill(value: true, _skillData);
		EffectSeManager.instance.PlaySe(successSe);
	}

	private bool IsMouseInsideRect(RectTransform rectTransform)
	{
		RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, Input.mousePosition, null, out var localPoint);
		return rectTransform.rect.Contains(localPoint);
	}

	public void OnDisable()
	{
		QuickOff(sound: false);
		isShow = false;
		isQuickClick = false;
		isQuickOn = false;
	}

	public void QuickOn(Vector2 value, SkillData data)
	{
		if (isQuickOn)
		{
			return;
		}
		potisionCurrent = value;
		screenSize.x = Screen.width;
		screenSize.y = Screen.height;
		screenCalc = screenSize / 2f;
		frameCalc.y = frameSize.y / 2f;
		potisionCalc.x = potisionCurrent.x;
		potisionCalc.y = potisionCurrent.y + frameCalc.y;
		if (potisionCalc.y > screenCalc.y - frameCalc.y)
		{
			potisionCalc.y = screenCalc.y - frameCalc.y - offset.y;
		}
		if (potisionCalc.y < 0f - screenCalc.y + frameCalc.y)
		{
			potisionCalc.y = 0f - screenCalc.y + frameCalc.y + offset.y;
		}
		_rectTransform.anchoredPosition = potisionCalc;
		if (RectTransformUtility.RectangleContainsScreenPoint(_iconRectTransform, Input.mousePosition, cameraGUI))
		{
			base.gameObject.SetActive(value: true);
			EffectSeManager.instance.PlaySe(dragSe);
			isShow = true;
			_skillData = data;
			_skillGUIManager.CheckSameSkill(value: true, _skillData);
			useClick = _skillData.mouse;
			if (useClick)
			{
				mouseInfo.SetActive(value: true);
				keyInfo.SetActive(value: false);
				_rectTransform.SetWidth(mouseFrame.GetWith());
				_rectTransform.SetHeight(mouseFrame.GetHeight());
			}
			else
			{
				mouseInfo.SetActive(value: false);
				keyInfo.SetActive(value: true);
				_rectTransform.SetWidth(keyFrame.GetWith());
				_rectTransform.SetHeight(keyFrame.GetHeight());
			}
		}
	}

	public void QuickOff(bool sound)
	{
		if (sound)
		{
			EffectSeManager.instance.PlaySe(failSe);
		}
		isShow = false;
		isQuickClick = false;
		isQuickOn = false;
		base.gameObject.SetActive(value: false);
	}
}
