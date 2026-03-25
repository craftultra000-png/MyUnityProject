using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillGUIAnimationConrollObject : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
	public SkillGUIManager _skillGUIManager;

	public SkillGUIDataBase _skillGUIDataBase;

	[Header("SkillData")]
	public SkillData _skillData;

	[Header("Data")]
	public bool clickDisableInfo;

	[Header("Status")]
	public bool isEnable;

	public bool isPointerEnter;

	public bool isLock;

	[Header("Icon Data")]
	public Image _skillImage;

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

	[Header("SE")]
	public AudioClip enterSe;

	public AudioClip clickSe;

	private void Start()
	{
		_skillImage.sprite = _skillData.skillIcon;
		defaultColor = _skillData.skillcolor;
		skillID = _skillData.skillID;
		_skillFill.fillAmount = 0f;
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

	public void ChangeSkill(SkillData data)
	{
		if (data != null)
		{
			_skillData = data;
			_skillImage.sprite = _skillData.skillIcon;
			defaultColor = _skillData.skillcolor;
			skillID = _skillData.skillID;
			_skillFill.fillAmount = 0f;
		}
	}

	public void OnDisable()
	{
		isPointerEnter = false;
		ChangeColor();
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
		isPointerEnter = true;
		SkillGUIInformation.instance.InfomationOn(_rectTransform.anchoredPosition + _rectTransformParent.anchoredPosition, _skillData.skillName, _skillData.infomation);
		ChangeColor();
		_skillGUIManager.PlaySe(enterSe);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		isPointerEnter = false;
		SkillGUIInformation.instance.InfomationOff();
		ChangeColor();
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Left && !isLock && coolTime <= 0f)
		{
			SkillGUIInformation.instance.InfomationOff();
			_skillGUIManager.PlaySe(clickSe);
			FeelerControllerData.instance.KeyCheck(_skillData.skillID, direct: true);
			ChangeColor();
		}
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Left && !clickDisableInfo)
		{
			SkillGUIInformation.instance.InfomationOn(_rectTransform.anchoredPosition + _rectTransformParent.anchoredPosition, _skillData.skillName, _skillData.infomation);
		}
	}
}
