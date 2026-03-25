using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utage;

public class TorsoManager : MonoBehaviour
{
	public static TorsoManager instance;

	public CharacterCostumeManager _characterCostumeManager;

	public CharacterCostumeSwitch _characterCostumeSwitch;

	[Header("Costume")]
	public int costume = -1;

	public int costumeCalc = -1;

	public List<SkillGUISkillTreeObject> costumeButtonGUI;

	[Header("Rect Transform")]
	public List<RectTransform> clotheTransform;

	[Header("Mesh")]
	public List<MeshRenderer> clotheMesh;

	public List<MeshFilter> clotheMeshFilter;

	[Header("Button")]
	public List<GameObject> button;

	public List<Image> buttonGauge;

	public List<int> buttonLevel;

	public List<float> buttonLife;

	public List<Color> levelColor;

	[Header("Lock Button")]
	public List<GameObject> lockButton;

	public List<Image> lockIcon;

	public List<ButtonTriggerGUI> lockButtonGUI;

	public List<bool> unlock;

	public Sprite lockImage;

	public Sprite unlockImage;

	public Color lockColor;

	public Color unlockColor;

	[Header("Text")]
	public List<UguiLocalize> clotheText;

	public List<TextMeshProUGUI> clotheEquip;

	[Header("Roll")]
	public Transform torsoObject;

	public float currentRoll;

	public float rollSpeed = 5f;

	public Vector3 calcRoll;

	[Header("Show Hide Frame")]
	public bool hideCostumeList;

	public GameObject frameCostumeList;

	public TextMeshProUGUI markCostumeList;

	[Space]
	public bool hideCostume;

	public GameObject frameCostume;

	public TextMeshProUGUI markCostume;

	[Space]
	public bool hideCostumeColor;

	public GameObject frameCostumeColor;

	public TextMeshProUGUI markCostumeColor;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
	}

	private void Start()
	{
		for (int i = 0; i < clotheEquip.Count; i++)
		{
			clotheEquip[i].text = "E";
		}
		for (int j = 0; j < buttonLife.Count; j++)
		{
			buttonLife[j] = 1f;
		}
		for (int k = 0; k < lockIcon.Count; k++)
		{
			lockIcon[k].sprite = unlockImage;
			lockIcon[k].color = unlockColor;
			lockButtonGUI[k].defaultColor = unlockColor;
			unlock[k] = true;
		}
		calcRoll = torsoObject.transform.localRotation.eulerAngles;
	}

	private void LateUpdate()
	{
		currentRoll += Time.deltaTime * rollSpeed;
		if (currentRoll > 360f)
		{
			currentRoll -= 360f;
		}
		calcRoll.z = currentRoll;
		torsoObject.localRotation = Quaternion.Euler(calcRoll);
	}

	public void OnEnable()
	{
		hideCostumeList = true;
		HideCostumeList();
		hideCostume = true;
		HideCostume();
		hideCostumeColor = true;
		HideCostumeColor();
		ResetClotheColor();
	}

	private void OnDisable()
	{
		currentRoll = -15f;
		calcRoll.z = currentRoll;
		torsoObject.localRotation = Quaternion.Euler(calcRoll);
	}

	public void ExcludeButton(int value)
	{
		for (int i = 0; i < button.Count; i++)
		{
			if (i < value)
			{
				button[i].SetActive(value: true);
				clotheMesh[i].gameObject.SetActive(value: true);
				lockButton[i].SetActive(value: true);
			}
			else
			{
				button[i].SetActive(value: false);
				clotheMesh[i].gameObject.SetActive(value: false);
				lockButton[i].SetActive(value: false);
			}
		}
	}

	public void SetCostume(int value)
	{
		_characterCostumeManager.SetCostume(value);
	}

	public void CostumeChange(int value)
	{
		_characterCostumeSwitch.CostumeChange(value);
		AllUnlockCostume();
		SetIcon(value);
	}

	public void SetIcon(int value)
	{
		costume = value;
		for (int i = 0; i < costumeButtonGUI.Count; i++)
		{
			costumeCalc = i + 121;
			if (i == costume)
			{
				SkillGUIDataBase.instance.SetEnable(costumeCalc, value: true);
			}
			else
			{
				SkillGUIDataBase.instance.SetEnable(costumeCalc, value: false);
			}
		}
	}

	public void LockCostume(int value)
	{
		if (unlock[value])
		{
			lockIcon[value].sprite = lockImage;
			lockIcon[value].color = lockColor;
			lockButtonGUI[value].defaultColor = lockColor;
			unlock[value] = false;
		}
		else
		{
			lockIcon[value].sprite = unlockImage;
			lockIcon[value].color = unlockColor;
			lockButtonGUI[value].defaultColor = unlockColor;
			unlock[value] = true;
		}
		_characterCostumeManager.LockCostume(value);
	}

	public void AllUnlockCostume()
	{
		for (int i = 0; i < lockIcon.Count; i++)
		{
			lockIcon[i].sprite = unlockImage;
			lockIcon[i].color = unlockColor;
			lockButtonGUI[i].defaultColor = unlockColor;
			unlock[i] = true;
		}
		_characterCostumeManager.AllUnlockCostume();
	}

	public void SetClotheData(int value, string name, bool skinnedMesh)
	{
		clotheText[value].Key = name;
		Vector3 zero = Vector3.zero;
		if (!skinnedMesh)
		{
			zero.x = 90f;
		}
		clotheTransform[value].localRotation = Quaternion.Euler(zero);
	}

	public void SetClotheLevel(int value, int level)
	{
		buttonLevel[value] = level;
		buttonGauge[value].color = levelColor[level];
		buttonGauge[value].fillAmount = 1f;
	}

	public void SetClotheDamage(int value, float life)
	{
		if (life > 1f)
		{
			life = 1f;
		}
		else if (life < 0f)
		{
			life = 0f;
		}
		buttonLife[value] = life;
		buttonGauge[value].fillAmount = life;
	}

	public void SetClothe(int value, bool set)
	{
		if (set)
		{
			clotheEquip[value].text = "E";
			clotheMesh[value].material.SetFloat("_isActive", 1f);
		}
		else
		{
			clotheEquip[value].text = "-";
			clotheMesh[value].material.SetFloat("_isActive", 0f);
		}
	}

	public void SetDefaultClotheColor()
	{
		for (int i = 0; i < buttonLevel.Count; i++)
		{
			if (buttonLevel[i] == 0)
			{
				clotheMesh[i].material.SetFloat("_Level1", 0f);
				clotheMesh[i].material.SetFloat("_Level2", 0f);
			}
			else if (buttonLevel[i] == 1)
			{
				clotheMesh[i].material.SetFloat("_Level1", 1f);
				clotheMesh[i].material.SetFloat("_Level2", 0f);
			}
			else if (buttonLevel[i] == 2)
			{
				clotheMesh[i].material.SetFloat("_Level1", 0f);
				clotheMesh[i].material.SetFloat("_Level2", 1f);
			}
		}
	}

	public void SetClotheColor(int value)
	{
		clotheMesh[value].material.SetFloat("_isBlink", 1f);
	}

	public void ResetClotheColor()
	{
		for (int i = 0; i < clotheMesh.Count; i++)
		{
			clotheMesh[i].material.SetFloat("_isBlink", 0f);
		}
	}

	public void HideCostumeList()
	{
		hideCostumeList = !hideCostumeList;
		if (hideCostumeList)
		{
			markCostumeList.text = "-";
		}
		else
		{
			markCostumeList.text = "+";
			if (!frameCostumeList.activeSelf)
			{
				OnDisable();
			}
		}
		frameCostumeList.SetActive(!hideCostumeList);
	}

	public void HideCostume()
	{
		hideCostume = !hideCostume;
		frameCostume.SetActive(!hideCostume);
		if (hideCostume)
		{
			markCostume.text = "-";
		}
		else
		{
			markCostume.text = "+";
		}
	}

	public void HideCostumeColor()
	{
		hideCostumeColor = !hideCostumeColor;
		frameCostumeColor.SetActive(!hideCostumeColor);
		if (hideCostumeColor)
		{
			markCostumeColor.text = "-";
		}
		else
		{
			markCostumeColor.text = "+";
		}
	}

	public void HideOtherCostumeList(bool value)
	{
		hideCostumeList = true;
		HideCostumeList();
		hideCostume = !value;
		HideCostume();
		hideCostumeColor = !value;
		HideCostumeColor();
	}

	public void HideOtherCostume(bool value)
	{
		hideCostumeList = !value;
		HideCostumeList();
		hideCostume = true;
		HideCostume();
		hideCostumeColor = !value;
		HideCostumeColor();
	}

	public void HideOtherCostumeColor(bool value)
	{
		hideCostumeList = !value;
		HideCostumeList();
		hideCostume = !value;
		HideCostume();
		hideCostumeColor = true;
		HideCostumeColor();
	}
}
