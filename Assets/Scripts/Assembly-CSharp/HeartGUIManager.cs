using UnityEngine;
using UnityEngine.UI;

public class HeartGUIManager : MonoBehaviour
{
	public static HeartGUIManager instance;

	public CharacterLifeManager _characterLifeManager;

	public CharacterFacialManager _characterFacialManager;

	public CharacterTalkManager _characterTalkManager;

	public CharacterEffectManager _characterEffectManager;

	public OnomatopoeiaManager _onomatopoeiaManager;

	[Header("Status")]
	public bool isHeart;

	public bool isOverHeat;

	[Header("Heart")]
	public float currentHeart;

	public float targetHeart;

	public float maxHeart = 200f;

	public float orgasmHeart = 150f;

	public float currentOverHeat;

	public float maxOverHeat = 300f;

	[Header("Gauge")]
	public Image gaugeImage;

	public Image gaugeImage2;

	public Image gaugeImage3;

	public float currentGauge;

	public float currentGauge2;

	public float currentGauge3;

	[Header("Effect")]
	public RectTransform effectStocker;

	public RectTransform virginTextStocker;

	public GameObject heartEffect;

	public GameObject overHeatEffect;

	public GameObject recoverEffect;

	public GameObject lostVirginEffect;

	public GameObject lostVirginTextEffect;

	[Header("Calc")]
	public float gaugeSpeed = 100f;

	[Header("Se")]
	public AudioClip heartSe;

	public AudioClip overHeatSe;

	public AudioClip recoverSe;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
	}

	private void Start()
	{
		currentHeart = 0f;
		targetHeart = 0f;
		currentGauge = 0f;
		gaugeImage.fillAmount = currentGauge;
		currentGauge2 = 0f;
		gaugeImage2.fillAmount = currentGauge2;
		currentOverHeat = 0f;
		currentGauge3 = 0f;
		gaugeImage3.fillAmount = currentGauge3;
	}

	private void LateUpdate()
	{
		if (isOverHeat)
		{
			currentOverHeat -= Time.deltaTime;
			if (currentOverHeat < 0f)
			{
				isOverHeat = false;
				_characterLifeManager.isOverHeat = false;
				_characterFacialManager.isLifeOverHeat = false;
				_characterTalkManager.overHeat = false;
				currentOverHeat = 0f;
				currentHeart = 100f;
				targetHeart = 100f;
				orgasmHeart = Random.Range(125f, 175f);
				_characterFacialManager.currentHeart = 100f;
				_characterFacialManager.timeStop = false;
				_characterFacialManager.ResetAllBool(idle: true);
				Object.Instantiate(recoverEffect, effectStocker).transform.localPosition = Vector3.zero;
				EffectSeManager.instance.PlaySe(recoverSe);
			}
			currentGauge3 = currentOverHeat / maxOverHeat;
			if (currentGauge3 < 0f)
			{
				currentGauge3 = 0f;
			}
			else if (currentGauge3 > 1f)
			{
				currentGauge3 = 1f;
			}
			gaugeImage3.fillAmount = currentGauge3;
		}
		if (!(currentHeart < targetHeart))
		{
			return;
		}
		float num = currentHeart;
		currentHeart += Time.deltaTime * gaugeSpeed;
		if (currentHeart > targetHeart)
		{
			currentHeart = targetHeart;
		}
		float num2 = currentHeart;
		_characterFacialManager.currentHeart = 100f;
		if (num < 100f && num2 >= 100f)
		{
			_characterLifeManager.HitData("Orgasm", "Orgasm");
			HeartEffect();
			_characterFacialManager.SetFacial("Heart", 1f);
			_characterFacialManager.HeadDizzy();
			_characterEffectManager.VaginaSplash();
			orgasmHeart = Random.Range(140f, 160f);
			_characterLifeManager.isHeart = true;
			_characterFacialManager.isLifeHeart = true;
			_onomatopoeiaManager.heartCount = 1;
		}
		else if (currentHeart < 200f && num < orgasmHeart && num2 >= orgasmHeart)
		{
			_characterLifeManager.HitData("Orgasm", "Orgasm");
			HeartEffect();
			_characterFacialManager.SetFacial("Despair", 1f);
			_characterFacialManager.HeadHeart();
			_characterEffectManager.VaginaSplash();
			if (Random.value < 0.1f)
			{
				orgasmHeart += 5f;
			}
			else
			{
				orgasmHeart = Random.Range(205f, 210f);
			}
			_onomatopoeiaManager.heartCount = 2;
		}
		else if (num < 200f && num2 >= 200f)
		{
			_characterLifeManager.HitData("Orgasm", "Orgasm");
			HeartEffect();
			_characterFacialManager.SetFacial("Ahe", 1f);
			_characterFacialManager.HeadOverHeat();
			_characterEffectManager.VaginaSplash();
			if (Random.value < 0.2f)
			{
				orgasmHeart += 5f;
			}
			else
			{
				orgasmHeart += Random.Range(10f, 20f);
			}
			_onomatopoeiaManager.heartCount = 3;
		}
		else if (currentHeart > 200f && num < orgasmHeart && num2 >= orgasmHeart)
		{
			_characterLifeManager.HitData("Orgasm", "Orgasm");
			HeartEffect();
			_characterFacialManager.SetFacial("Heart", 1f);
			_characterFacialManager.HeadHeart();
			_characterEffectManager.VaginaSplash();
			if (Random.value < 0.2f)
			{
				orgasmHeart += 5f;
			}
			else
			{
				orgasmHeart += Random.Range(15f, 30f);
			}
		}
		if (!isOverHeat && currentHeart > maxHeart)
		{
			isOverHeat = true;
			_characterLifeManager.isOverHeat = true;
			_characterFacialManager.isLifeOverHeat = true;
			_characterTalkManager.overHeat = true;
			currentOverHeat = maxOverHeat;
			gaugeImage.fillAmount = 1f;
			gaugeImage2.fillAmount = 0f;
			Object.Instantiate(overHeatEffect, effectStocker).transform.localPosition = Vector3.zero;
			EffectSeManager.instance.PlaySe(overHeatSe);
		}
		if (isOverHeat)
		{
			return;
		}
		currentGauge = currentHeart / 100f;
		if (currentGauge > 1f)
		{
			currentGauge = 1f;
		}
		gaugeImage.fillAmount = currentGauge;
		if (currentHeart >= 100f)
		{
			currentGauge2 = (currentHeart - 100f) / 100f;
			if (currentGauge2 < 0f)
			{
				currentGauge2 = 0f;
			}
			else if (currentGauge2 > 1f)
			{
				currentGauge2 = 1f;
			}
		}
		else
		{
			currentGauge2 = 0f;
		}
		gaugeImage2.fillAmount = currentGauge2;
	}

	public void SetHeart(float value)
	{
		targetHeart += value;
	}

	public void HeartEffect()
	{
		Object.Instantiate(heartEffect, effectStocker).transform.localPosition = Vector3.zero;
		EffectSeManager.instance.PlaySe(heartSe);
	}

	public void LostVirginEffect()
	{
		Object.Instantiate(lostVirginEffect, effectStocker).transform.localPosition = Vector3.zero;
		if (virginTextStocker.gameObject.activeSelf)
		{
			GameObject obj = Object.Instantiate(lostVirginEffect, virginTextStocker);
			Vector3 localPosition = new Vector3(0f, 5f, -0.5f);
			obj.transform.localPosition = localPosition;
		}
	}
}
