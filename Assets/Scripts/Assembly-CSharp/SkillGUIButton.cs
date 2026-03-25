using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillGUIButton : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IDropHandler
{
	public SkillGUISkillControllObject _skillGUISkillControllObject;

	public SkillGUIDataBase _skillGUIDataBase;

	public int controlType;

	public bool mouseType;

	[Header("Skill Data")]
	public SkillData _skillData;

	[Header("Status")]
	public bool isEnable;

	public bool isPointerEnter;

	public bool isLock;

	[Header("Image")]
	public Image skillIcon;

	public Image skillFrame;

	public GameObject blinkEffect;

	[Header("Color")]
	public Color defaultColor;

	public Color enableColor;

	public Color disableColor;

	public Color pointerEnterColor;

	public Color enableEnterColor;

	[Header("Null")]
	public Sprite nullImage;

	public Color nullColor;

	[Header("CoolTime")]
	public Image _skillFill;

	public int skillID;

	public float coolTime;

	[Header("Effect")]
	public RectTransform effectStocker;

	public GameObject setSkillEffect;

	public GameObject setSkillSubEffect;

	public GameObject sameSkillEffect;

	[Header("Default Setting")]
	public RectTransform _rectTransform;

	[Header("SE")]
	public AudioClip enterSe;

	private void Start()
	{
		ResetIcon();
		_skillFill.fillAmount = 0f;
	}

	private void LateUpdate()
	{
		if (skillID >= 0)
		{
			coolTime = _skillGUIDataBase._skillCurrentTimeList[skillID];
			if (coolTime >= 0f)
			{
				_skillFill.fillAmount = coolTime / _skillGUIDataBase._skillCoolTimeList[skillID];
			}
			isLock = _skillGUIDataBase._skillLockList[skillID];
			isEnable = _skillGUIDataBase._skillEnableList[skillID];
		}
		else
		{
			_skillFill.fillAmount = 0f;
		}
		ChangeColor();
	}

	public void ChangeColor()
	{
		if (isLock)
		{
			skillIcon.color = disableColor;
		}
		else if (isEnable && isPointerEnter)
		{
			skillIcon.color = enableEnterColor;
		}
		else if (isEnable)
		{
			skillIcon.color = enableColor;
		}
		else if (isPointerEnter)
		{
			skillIcon.color = pointerEnterColor;
		}
		else
		{
			skillIcon.color = defaultColor;
		}
	}

	public void SetIcon(SkillData data)
	{
		_skillData = data;
		if (_skillData == null)
		{
			Debug.LogError("Set icon: null");
		}
		if (_skillData == null)
		{
			isEnable = false;
			skillIcon.sprite = nullImage;
			defaultColor = nullColor;
		}
		else
		{
			skillIcon.sprite = _skillData.skillIcon;
			defaultColor = _skillData.skillcolor;
		}
	}

	public void ResetIcon()
	{
		if (_skillData == null)
		{
			Debug.LogError("Reset icon: null");
		}
		if (_skillData == null)
		{
			isEnable = false;
			skillIcon.sprite = nullImage;
			defaultColor = nullColor;
			skillID = -1;
		}
		else
		{
			skillIcon.sprite = _skillData.skillIcon;
			defaultColor = _skillData.skillcolor;
			skillID = _skillData.skillID;
		}
	}

	public void SetSkillEffect()
	{
		Object.Instantiate(setSkillEffect, effectStocker).transform.localPosition = Vector3.zero;
	}

	public void SetSkillSubEffect()
	{
		Object.Instantiate(setSkillSubEffect, effectStocker).transform.localPosition = Vector3.zero;
	}

	public void SetSameSkillEffect(bool value)
	{
		sameSkillEffect.SetActive(value);
	}

	public void SetBlinkEffect(bool value)
	{
		blinkEffect.SetActive(value);
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
	}

	public void OnPointerExit(PointerEventData eventData)
	{
	}

	public void OnDrop(PointerEventData eventData)
	{
		SkillGUISkillTreeObject obj = eventData.pointerDrag?.GetComponent<SkillGUISkillTreeObject>();
		if (obj != null)
		{
			Debug.Log("SkillGUIObject dropped onto SkillGUIDropZone: " + base.gameObject.name);
		}
		eventData.pointerDrag?.GetComponent<SkillGUISkillControllObject>();
		if (obj != null)
		{
			Debug.Log("SkillGUIObject dropped onto SkillGUIDropZone: " + base.gameObject.name);
		}
	}
}
