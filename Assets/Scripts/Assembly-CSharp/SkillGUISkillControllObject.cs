using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillGUISkillControllObject : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
	public SkillGUIManager _skillGUIManager;

	public SkillGUIDataBase _skillGUIDataBase;

	[Header("SkillData")]
	public SkillGUIButton _skillGUIButtonLink;

	public SkillGUIButton _skillGUIButton;

	public SkillGUISkillControllObject _skillGUIControl;

	[Header("Status")]
	public bool initialize;

	public bool useClick;

	public bool isMove;

	public bool isDrag;

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
	}

	public void OnDisable()
	{
		if (!initialize)
		{
			base.transform.SetParent(defaultParent, worldPositionStays: true);
			defaultPosition = _rectTransform.anchoredPosition;
			initialize = true;
		}
		_rectTransform.anchoredPosition = defaultPosition;
		base.transform.SetParent(defaultParent, worldPositionStays: false);
		isMove = false;
		isDrag = false;
		_dragImage.raycastTarget = true;
		_dragImage.maskable = true;
		_skillGUIButtonLink.isPointerEnter = false;
	}

	public void SetRaycast(bool value)
	{
		_dragImage.raycastTarget = value;
		_dragImage.maskable = value;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		isMove = false;
		if (_skillGUIButtonLink._skillData != null)
		{
			SkillGUIInformation.instance.InfomationOn(_rectTransformParent.anchoredPosition, _skillGUIButtonLink._skillData.skillName, _skillGUIButtonLink._skillData.infomation);
			if (useClick)
			{
				_skillGUIButtonLink.isPointerEnter = true;
			}
		}
		if (!isDrag)
		{
			SkillGUIManager.instance.PlaySe(enterSe);
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		isMove = false;
		SkillGUIInformation.instance.InfomationOff();
		if (useClick)
		{
			_skillGUIButtonLink.isPointerEnter = false;
		}
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Left)
		{
			SkillGUIInformation.instance.InfomationOff();
		}
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		if (!isMove && eventData.button == PointerEventData.InputButton.Left && _skillGUIButtonLink._skillData != null && !_skillGUIButtonLink.isLock && _skillGUIButtonLink.coolTime <= 0f && useClick)
		{
			SkillGUIManager.instance.PlaySe(clickSe);
			_skillGUIButtonLink.isPointerEnter = false;
			FeelerControllerData.instance.KeyCheck(_skillGUIButtonLink.controlType, direct: false);
		}
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Left)
		{
			base.transform.SetParent(moveParent, worldPositionStays: false);
			EffectSeManager.instance.PlaySe(dragSe);
			if (useClick)
			{
				_skillGUIButtonLink.isPointerEnter = false;
			}
		}
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Left)
		{
			isMove = true;
			isDrag = true;
			SkillGUIManager.instance.isDrag = true;
			SkillGUIInformation.instance.isDrag = true;
			_dragImage.raycastTarget = false;
			_dragImage.maskable = false;
			if (_skillGUIButtonLink.mouseType)
			{
				SkillGUIManager.instance.BlinkMouse(value: true);
			}
			else if (!_skillGUIButtonLink.mouseType)
			{
				SkillGUIManager.instance.BlinkKey(value: true);
			}
			RectTransformUtility.ScreenPointToLocalPointInRectangle(_rectTransform.parent as RectTransform, eventData.position, eventData.pressEventCamera, out var localPoint);
			localPoint.y -= 8f;
			_rectTransform.anchoredPosition = localPoint;
		}
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		if (eventData.button != PointerEventData.InputButton.Left)
		{
			return;
		}
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
		if (_skillGUIButton != null && _skillGUIButton != _skillGUIButtonLink)
		{
			if (_skillGUIButton.mouseType == _skillGUIButtonLink.mouseType)
			{
				EffectSeManager.instance.PlaySe(successSe);
				if (_skillGUIButtonLink.mouseType)
				{
					_skillGUIManager.SwitchMouseSkill(_skillGUIButtonLink.controlType, _skillGUIButton.controlType);
				}
				else
				{
					_skillGUIManager.SwitchKeySkill(_skillGUIButtonLink.controlType, _skillGUIButton.controlType);
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
		SkillGUIManager.instance.isDrag = false;
		SkillGUIInformation.instance.isDrag = false;
		_dragImage.raycastTarget = true;
		_dragImage.maskable = true;
		SkillGUIManager.instance.BlinkMouse(value: false);
		SkillGUIManager.instance.BlinkKey(value: false);
	}
}
