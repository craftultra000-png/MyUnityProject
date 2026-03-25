using UnityEngine;
using UnityEngine.UI;

public class MilkGUIManager : MonoBehaviour
{
	public static MilkGUIManager instance;

	public FeelerControllerData _feelerControllerData;

	[Header("Status")]
	public bool isMilk;

	[Header("EXP")]
	public float currentMilk;

	public float targetMilk;

	public float maxMilk = 100f;

	[Header("Gauge")]
	public Image gaugeImage;

	public Image milkTankInsideImage;

	public float currentGauge;

	[Header("Effect")]
	public RectTransform effectStocker;

	public GameObject milkEffect;

	[Header("Calc")]
	public float gaugeSpeed = 100f;

	[Header("Se")]
	public AudioClip milkSe;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
	}

	private void Start()
	{
		currentMilk = 0f;
		targetMilk = 0f;
		currentGauge = 0f;
		gaugeImage.fillAmount = currentGauge;
		milkTankInsideImage.fillAmount = currentGauge;
	}

	private void LateUpdate()
	{
		if (currentMilk < targetMilk && !isMilk)
		{
			currentMilk += Time.deltaTime * gaugeSpeed;
			if (currentMilk >= maxMilk)
			{
				isMilk = true;
				currentMilk = targetMilk;
				_feelerControllerData.TitsMilkCheck();
				Object.Instantiate(milkEffect, effectStocker).transform.localPosition = Vector3.zero;
				EffectSeManager.instance.PlaySe(milkSe);
			}
			currentGauge = currentMilk / maxMilk;
			gaugeImage.fillAmount = currentGauge;
			milkTankInsideImage.fillAmount = currentGauge;
		}
	}

	public void SetMilk(float value)
	{
		if (targetMilk < maxMilk)
		{
			targetMilk += value;
			if (targetMilk > maxMilk)
			{
				targetMilk = maxMilk;
			}
		}
	}
}
