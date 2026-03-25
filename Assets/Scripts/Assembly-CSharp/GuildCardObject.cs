using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Utage;

public class GuildCardObject : MonoBehaviour
{
	public WorldLightManager _worldLightManager;

	public MilkGUIManager _milkGUIManager;

	public CharacterConceiveManager _characterConceiveManager;

	public Transform rollPoint;

	[Header("Status")]
	public bool isUnMove;

	public bool isDisplay;

	public bool isGUIOpen;

	[Header("Roll")]
	public float currentTime;

	public float rollSpeed = 2f;

	[Header("Calc")]
	public Vector3 currentRotation;

	public Vector3 showRotation = new Vector3(0f, 0f, 0f);

	public Vector3 hideRotation = new Vector3(0f, 0f, 70f);

	public Vector3 calcRotation;

	[Header("Material")]
	public MeshRenderer GuildCardMesh;

	public Material guildCardMaterial;

	[Header("Feeler")]
	public GameObject feelerObject;

	[Header("Language")]
	public string langBase;

	public LanguageManagerBase langManager;

	public string nameDefault = "Vein Lurk";

	public string rankDefault = "RANK";

	public string jobDefault = "JOB";

	public TextMeshPro nameDefaultText;

	public TextMeshPro rankDefaultText;

	public TextMeshPro jobDefaultText;

	[Header("Milk Count")]
	public float currentMilk;

	public float currentMilkCurrent;

	[Header("Rank Alphabet")]
	public int rank;

	public TextMeshPro rankText;

	public RectTransform rankTransform;

	public List<Vector3> rankPosition;

	public List<Vector3> rankPositionTMP;

	[Header("Job Name")]
	public string jobName;

	public TextMeshPro jobNameText;

	public GameObject jobChangeEffect;

	public GameObject resultEffect;

	[Header("Status Total Count")]
	public ResultData _resultData;

	public TextMeshPro totalLiquidText;

	public TextMeshPro totalFleshText;

	public TextMeshPro totalGenitalsText;

	public int liquidCount;

	public int fleshCount;

	public int genitalsCount;

	[Header("Child Count")]
	public int vaginaChildStackCount;

	public int analChildStackCount;

	private int vaginaChildStackCountCurrent;

	private int analChildStackCountCurrent;

	[Header("Three Size")]
	public TextMeshPro threeSizeText;

	public string threeSizeString;

	public float waist;

	public float hip;

	public Vector3 threeSizeCurrent;

	public Vector3 threeSizeCalc;

	public Vector3 threeSizeDefault = new Vector3(92f, 59f, 88f);

	public Vector3 threeSizeMax = new Vector3(95f, 105f, 92f);

	[Header("Se")]
	public AudioClip jobChangeSe;

	private void Start()
	{
		if (!isUnMove)
		{
			currentTime = 0.01f;
			currentRotation = hideRotation;
			rollPoint.localRotation = Quaternion.Euler(hideRotation);
		}
		guildCardMaterial = GuildCardMesh.material;
		_worldLightManager.cardMaterial = guildCardMaterial;
		threeSizeCalc = threeSizeDefault;
		threeSizeCurrent = threeSizeCalc;
		threeSizeString = "B:" + Mathf.Min(threeSizeCurrent.x, 999f).ToString("F1");
		threeSizeString = threeSizeString + " W:" + Mathf.Min(threeSizeCurrent.y, 999f).ToString("F1");
		threeSizeString = threeSizeString + " H:" + Mathf.Min(threeSizeCurrent.z, 999f).ToString("F1");
		threeSizeText.text = threeSizeString;
		_resultData = ResultData.instance;
		StartCoroutine(WaitStart());
		jobChangeEffect.SetActive(value: false);
		resultEffect.SetActive(value: false);
	}

	private IEnumerator WaitStart()
	{
		yield return null;
		SetRank(5, "Thief");
		SetTotal();
	}

	private void LateUpdate()
	{
		if (isUnMove)
		{
			return;
		}
		if (isDisplay && !isGUIOpen)
		{
			if (currentTime < 1f)
			{
				currentTime += Time.deltaTime * rollSpeed;
				if (currentTime > 1f)
				{
					currentTime = 1f;
				}
				calcRotation = Vector3.Lerp(hideRotation, showRotation, currentTime);
				rollPoint.localRotation = Quaternion.Euler(calcRotation);
			}
		}
		else if (currentTime > 0f)
		{
			currentTime -= Time.deltaTime * rollSpeed;
			if (currentTime < 0f)
			{
				currentTime = 0f;
			}
			calcRotation = Vector3.Lerp(hideRotation, showRotation, currentTime);
			rollPoint.localRotation = Quaternion.Euler(calcRotation);
		}
		currentMilk = _milkGUIManager.currentMilk;
		if (currentMilk != currentMilkCurrent)
		{
			currentMilkCurrent = currentMilk;
			SetThreeSize();
		}
		vaginaChildStackCount = _characterConceiveManager.vaginaChildCount;
		analChildStackCount = _characterConceiveManager.analChildCount;
		if (vaginaChildStackCount != vaginaChildStackCountCurrent || analChildStackCount != analChildStackCountCurrent)
		{
			vaginaChildStackCountCurrent = vaginaChildStackCount;
			analChildStackCountCurrent = analChildStackCount;
			SetThreeSize();
		}
		if (threeSizeCurrent != threeSizeCalc)
		{
			float num = 5f;
			threeSizeCurrent.x = Mathf.MoveTowards(threeSizeCurrent.x, threeSizeCalc.x, num * Time.deltaTime);
			threeSizeCurrent.y = Mathf.MoveTowards(threeSizeCurrent.y, threeSizeCalc.y, num * Time.deltaTime);
			threeSizeCurrent.z = Mathf.MoveTowards(threeSizeCurrent.z, threeSizeCalc.z, num * Time.deltaTime);
			threeSizeString = "B:" + Mathf.Min(threeSizeCurrent.x, 999f).ToString("F1");
			threeSizeString = threeSizeString + " W:" + Mathf.Min(threeSizeCurrent.y, 999f).ToString("F1");
			threeSizeString = threeSizeString + " H:" + Mathf.Min(threeSizeCurrent.z, 999f).ToString("F1");
			threeSizeText.text = threeSizeString;
		}
	}

	public void SetThreeSize()
	{
		waist = Mathf.Clamp01((float)(vaginaChildStackCount + analChildStackCount) / 20f);
		hip = Mathf.Clamp01(((float)vaginaChildStackCount / 2f + (float)analChildStackCount) / 20f);
		threeSizeCalc.x = Mathf.Lerp(threeSizeDefault.x, threeSizeMax.x, currentMilkCurrent / 100f);
		threeSizeCalc.y = Mathf.Lerp(threeSizeDefault.y, threeSizeMax.y, waist);
		threeSizeCalc.z = Mathf.Lerp(threeSizeDefault.z, threeSizeMax.z, hip);
	}

	public void SetRank(int value, string job)
	{
		rank = value;
		if (rank == 0)
		{
			rankText.text = "S";
		}
		else if (rank == 1)
		{
			rankText.text = "A";
		}
		else if (rank == 2)
		{
			rankText.text = "B";
		}
		else if (rank == 3)
		{
			rankText.text = "C";
		}
		else if (rank == 4)
		{
			rankText.text = "D";
		}
		else if (rank == 5)
		{
			rankText.text = "E";
		}
		rankTransform.localPosition = rankPosition[rank];
		jobName = job;
		LangaugeReset();
		if (rank != 5)
		{
			jobChangeEffect.SetActive(value: true);
			EffectSeManager.instance.PlaySe(jobChangeSe);
		}
	}

	public void SetTotal()
	{
		if (_resultData != null)
		{
			liquidCount = _resultData.liquidCount;
			fleshCount = _resultData.fleshCount;
			genitalsCount = _resultData.genitalsCount;
		}
		totalLiquidText.text = liquidCount.ToString();
		totalFleshText.text = fleshCount.ToString();
		totalGenitalsText.text = genitalsCount.ToString();
	}

	public void LangaugeReset()
	{
		if (langManager == null)
		{
			langManager = LanguageManagerBase.Instance;
		}
		langBase = langManager.CurrentLanguage;
		jobNameText.text = langManager.LocalizeText(jobName);
		nameDefaultText.text = langManager.LocalizeText(nameDefault);
		rankDefaultText.text = langManager.LocalizeText(rankDefault);
		jobDefaultText.text = langManager.LocalizeText(jobDefault);
	}

	public void SetGuildCard(int value)
	{
		isDisplay = !isDisplay;
		SkillGUIDataBase.instance.SetEnable(value, isDisplay);
	}

	public void ResultGuildCard()
	{
		isUnMove = true;
		feelerObject.SetActive(value: false);
		jobChangeEffect.SetActive(value: false);
		resultEffect.SetActive(value: true);
	}
}
