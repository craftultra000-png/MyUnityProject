using TMPro;
using UnityEngine;
using Utage;

public class SkillGUIInformation : MonoBehaviour
{
	public static SkillGUIInformation instance;

	[Header("Status")]
	public bool isDrag;

	[Header("Infomation Data")]
	public TextMeshProUGUI skillName;

	public TextMeshProUGUI infomation;

	public UguiLocalize _uguiLocalizeName;

	public UguiLocalize _uguiLocalizeInfomation;

	[Header("Infomation Panel")]
	public RectTransform _rectTransform;

	public Vector2 frameSize = new Vector2(200f, 100f);

	public Vector2 iconSize = new Vector2(60f, 60f);

	public Vector2 offset = new Vector2(10f, 10f);

	public Vector2 screenSize;

	[Header("Infomation Calc")]
	public Vector2 potisionCurrent;

	public Vector2 potisionCalc;

	public Vector2 frameCalc;

	public Vector2 screenCalc;

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

	public void OnDisable()
	{
		InfomationOff();
		isDrag = false;
	}

	public void InfomationOn(Vector2 value, string nameText, string infomationText)
	{
		if (!isDrag)
		{
			_uguiLocalizeName.Key = nameText;
			_uguiLocalizeInfomation.Key = infomationText;
			potisionCurrent = value;
			screenSize.x = Screen.width;
			screenSize.y = Screen.height;
			screenCalc = screenSize / 2f;
			frameCalc.x = iconSize.x / 2f + frameSize.x / 2f + offset.x;
			frameCalc.y = frameSize.y / 2f;
			if (potisionCurrent.x >= 0f)
			{
				potisionCalc.x = potisionCurrent.x - frameCalc.x;
			}
			else
			{
				potisionCalc.x = potisionCurrent.x + frameCalc.x;
			}
			potisionCalc.y = potisionCurrent.y;
			if (potisionCalc.y > screenCalc.y - frameCalc.y)
			{
				potisionCalc.y = screenCalc.y - frameCalc.y - offset.y;
			}
			if (potisionCalc.y < 0f - screenCalc.y + frameCalc.y)
			{
				potisionCalc.y = 0f - screenCalc.y + frameCalc.y + offset.y;
			}
			_rectTransform.anchoredPosition = potisionCalc;
			_rectTransform.gameObject.SetActive(value: true);
		}
	}

	public void InfomationOff()
	{
		_rectTransform.gameObject.SetActive(value: false);
	}
}
