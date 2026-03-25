using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utage;

public class SkillGUISkillTreeObject : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
	public SkillGUIManager _skillGUIManager;

	public SkillGUIDataBase _skillGUIDataBase;

	[Header("SkillData")]
	public SkillData _skillData;

	public SkillGUIButton _skillGUIButton;

	public SkillGUISkillControllObject _skillGUIControl;

	[Header("Type")]
	public bool otherButton;

	[Header("Status")]
	public bool initialize;

	public bool isMove;

	public bool isDrag;

	public bool isDragStart;

	public bool isDragEnd;

	[Header("Status")]
	public bool isEnable;

	public bool isPointerEnter;

	public bool isLock;

	[Header("Icon Data")]
	public UguiLocalize _uguiLocalize;

	public TextMeshProUGUI _nameText;

	public Image _skillImage;

	public GameObject mouseIcon;

	[Header("Color")]
	public Color defaultColor;

	public Color enableColor;

	public Color disableColor;

	public Color pointerEnterColor;

	public Color enableEnterColor;

	[Header("CoolTime")]
	public Image _skillFill;

	public int skillID;

	public float coolTime;

	[Header("Default Setting")]
	public RectTransform _rectTransform;

	public RectTransform _rectTransformParent;

	public Image _dragImage;

	public Transform defaultParent;

	public Transform moveParent;

	public Vector3 defaultPosition;

	[Header("SE")]
	public AudioClip enterSe;

	public AudioClip dragSe;

	public AudioClip successSe;

	public AudioClip failSe;

	public AudioClip clickSe;

	private void Start()
	{
		base.transform.SetParent(defaultParent, worldPositionStays: true);
		defaultPosition = _rectTransform.anchoredPosition;
		_uguiLocalize.Key = _skillData.skillName;
		_skillImage.sprite = _skillData.skillIcon;
		defaultColor = _skillData.skillcolor;
		_nameText.color = defaultColor;
		_skillImage.color = defaultColor;
		mouseIcon.SetActive(_skillData.mouse);
		skillID = _skillData.skillID;
		_skillFill.fillAmount = 0f;
		_skillGUIManager._skillGUISkillTreeObject.Add(this);
	}

	private void LateUpdate()
	{
		coolTime = _skillGUIDataBase._skillCurrentTimeList[skillID];
		if (coolTime >= 0f)
		{
			_skillFill.fillAmount = coolTime / _skillGUIDataBase._skillCoolTimeList[skillID];
		}
		isLock = _skillGUIDataBase._skillLockList[skillID];
		isEnable = _skillGUIDataBase._skillEnableList[skillID];
		ChangeColor();
	}

	public void ResetPosition()
	{
		isPointerEnter = false;
		ChangeColor();
		_rectTransform.anchoredPosition = defaultPosition;
		base.transform.SetParent(defaultParent, worldPositionStays: false);
		_rectTransform.localPosition = Vector3.zero;
		_skillGUIButton = null;
		_skillGUIControl = null;
		isMove = false;
		isDrag = false;
		_dragImage.raycastTarget = true;
		_dragImage.maskable = true;
		_skillGUIManager.isDrag = false;
		isDragEnd = true;
	}

	public void ChangeColor()
	{
		if (isLock)
		{
			_skillImage.color = disableColor;
		}
		else if (isEnable && isPointerEnter)
		{
			_skillImage.color = enableEnterColor;
		}
		else if (isEnable)
		{
			_skillImage.color = enableColor;
		}
		else if (isPointerEnter)
		{
			_skillImage.color = pointerEnterColor;
		}
		else
		{
			_skillImage.color = defaultColor;
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		isMove = false;
		isPointerEnter = true;
		SkillGUIInformation.instance.InfomationOn(_rectTransformParent.anchoredPosition, _skillData.skillName, _skillData.infomation);
		if (!isDrag)
		{
			_skillGUIManager.PlaySe(enterSe);
		}
		if (otherButton)
		{
			_skillGUIManager.CheckSameSkill(value: true, _skillData);
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		isMove = false;
		isPointerEnter = false;
		SkillGUIInformation.instance.InfomationOff();
		if (otherButton)
		{
			_skillGUIManager.CheckSameSkill(value: false, _skillData);
		}
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Left)
		{
			SkillGUIInformation.instance.InfomationOff();
		}
		else if (eventData.button == PointerEventData.InputButton.Right)
		{
			_ = otherButton;
		}
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		if (isMove || !isPointerEnter)
		{
			return;
		}
		if (eventData.button == PointerEventData.InputButton.Left)
		{
			if (!isLock && coolTime <= 0f)
			{
				if (!_skillData.mouse)
				{
					FeelerControllerData.instance.KeyCheck(_skillData.skillID, direct: true);
					_skillGUIManager.PlaySe(clickSe);
				}
				if (_skillData.mouseToggle)
				{
					FeelerControllerData.instance.MouseDisable(_skillData.skillID);
				}
			}
		}
		else
		{
			if (eventData.button != PointerEventData.InputButton.Right || otherButton)
			{
				return;
			}
			Debug.LogError("Instance Check: " + SkillGUIQuickSet.instance);
			if (!SkillGUIQuickSet.instance.isQuickOn)
			{
				_skillGUIManager.CheckSameSkill(value: true, _skillData);
				SkillGUIQuickSet.instance.QuickOn(_rectTransformParent.anchoredPosition, _skillData);
				SkillGUIQuickSet.instance.isQuickClick = false;
				SkillGUIQuickSet.instance.isQuickOn = true;
				SkillGUIInformation.instance.InfomationOff();
				if (_skillData.mouse)
				{
					_skillGUIManager.BlinkMouse(value: true);
				}
				else if (!_skillData.mouse)
				{
					_skillGUIManager.BlinkKey(value: true);
				}
			}
			else if (SkillGUIQuickSet.instance.isQuickOn && !_skillData.mouse)
			{
				SkillGUIQuickSet.instance.QuickOff(sound: true);
				_skillGUIManager.BlinkMouse(value: false);
				_skillGUIManager.BlinkKey(value: false);
			}
		}
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Left && !otherButton)
		{
			base.transform.SetParent(moveParent, worldPositionStays: false);
			EffectSeManager.instance.PlaySe(dragSe);
			isDragStart = true;
			isDragEnd = false;
		}
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Left && !otherButton && (!isDragStart || !isDragEnd))
		{
			isMove = true;
			isDrag = true;
			_skillGUIManager.isDrag = true;
			SkillGUIInformation.instance.isDrag = true;
			_dragImage.raycastTarget = false;
			_dragImage.maskable = false;
			if (_skillData.mouse)
			{
				_skillGUIManager.BlinkMouse(value: true);
			}
			else if (!_skillData.mouse)
			{
				_skillGUIManager.BlinkKey(value: true);
			}
			_skillGUIManager.CheckSameSkill(value: true, _skillData);
			RectTransformUtility.ScreenPointToLocalPointInRectangle(_rectTransform.parent as RectTransform, eventData.position, eventData.pressEventCamera, out var localPoint);
			localPoint.y -= 8f;
			_rectTransform.anchoredPosition = localPoint;
		}
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		if (eventData.button != PointerEventData.InputButton.Left || otherButton || (isDragStart && isDragEnd))
		{
			return;
		}
		isDragStart = false;
		isDragEnd = true;
		_skillGUIManager.CheckSameSkill(value: false, _skillData);
		_skillGUIButton = null;
		_skillGUIControl = null;
		if (eventData.pointerEnter != null)
		{
			_skillGUIButton = eventData.pointerEnter.GetComponent<SkillGUIButton>();
			_skillGUIControl = eventData.pointerEnter.GetComponent<SkillGUISkillControllObject>();
		}
		if (_skillGUIControl != null)
		{
			_skillGUIButton = _skillGUIControl._skillGUIButtonLink;
		}
		if (_skillGUIButton != null && _skillGUIButton._skillData != _skillData)
		{
			if (_skillGUIButton.mouseType == _skillData.mouse)
			{
				EffectSeManager.instance.PlaySe(successSe);
				if (_skillGUIButton.mouseType)
				{
					_skillGUIManager.SetMouseSkill(_skillGUIButton.controlType, _skillData);
				}
				else
				{
					_skillGUIManager.SetKeySkill(_skillGUIButton.controlType, _skillData);
				}
			}
			else
			{
				EffectSeManager.instance.PlaySe(failSe);
			}
		}
		else
		{
			EffectSeManager.instance.PlaySe(failSe);
		}
		_rectTransform.anchoredPosition = defaultPosition;
		base.transform.SetParent(defaultParent, worldPositionStays: false);
		isMove = false;
		isDrag = false;
		_skillGUIManager.isDrag = false;
		SkillGUIInformation.instance.isDrag = false;
		_dragImage.raycastTarget = true;
		_dragImage.maskable = true;
		_skillGUIManager.BlinkMouse(value: false);
		_skillGUIManager.BlinkKey(value: false);
	}
}
