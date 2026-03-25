using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EXPGUIManager : MonoBehaviour
{
	public static EXPGUIManager instance;

	[Header("Status")]
	public bool isLevelCap;

	[Header("EXP")]
	public float currentEXP;

	public float targetEXP;

	public float maxEXP;

	[Header("Level")]
	public TextMeshProUGUI levelText;

	public int currentLevel;

	public int maxLevel = 20;

	public AnimationCurve levelCurve;

	[Header("Gauge")]
	public Image gaugeImage;

	public float currentGauge;

	[Header("Effect")]
	public RectTransform effectStocker;

	public GameObject levelUpEffect;

	[Header("Calc")]
	public float gaugeSpeed = 100f;

	[Header("Se")]
	public AudioClip levelUpSe;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
	}

	private void Start()
	{
		maxEXP = levelCurve.Evaluate(currentLevel);
		currentEXP = 0f;
		currentGauge = 0f;
		gaugeImage.fillAmount = currentGauge;
		currentLevel = 0;
		levelText.text = "Lv.0";
	}

	private void LateUpdate()
	{
		if (!(currentEXP < targetEXP) || isLevelCap)
		{
			return;
		}
		currentEXP += Time.deltaTime * gaugeSpeed;
		if (currentEXP > targetEXP)
		{
			currentEXP = targetEXP;
		}
		if (currentEXP > maxEXP)
		{
			currentEXP = 0f;
			targetEXP -= maxEXP;
			Object.Instantiate(levelUpEffect, effectStocker).transform.localPosition = Vector3.zero;
			EffectSeManager.instance.PlaySe(levelUpSe);
			currentLevel++;
			if (currentLevel < maxLevel)
			{
				maxEXP = levelCurve.Evaluate(currentLevel);
				levelText.text = "Lv." + currentLevel;
			}
			else
			{
				isLevelCap = true;
				levelText.text = "Lv.Max";
			}
		}
		if (isLevelCap)
		{
			currentEXP = 1f;
			maxEXP = 1f;
		}
		currentGauge = currentEXP / maxEXP;
		gaugeImage.fillAmount = currentGauge;
	}

	public void GetEXP(float value)
	{
		targetEXP += value;
	}
}
