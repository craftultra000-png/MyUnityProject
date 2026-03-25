using System.Collections.Generic;
using Animancer;
using UnityEngine;

public class CharacterEyesManager : MonoBehaviour
{
	public Animator _animator;

	public AnimancerComponent _animancer;

	public SideCharacterManger _sideCharacterManger;

	public CustomColorGUI _customColorGUI;

	public CharacterFacialManager _characterFacialManager;

	[Header("Skinned Mesh")]
	public SkinnedMeshRenderer eyesMesh;

	private Mesh eyesSkinnedMesh;

	public Material eyesMaterial;

	public int shapeKeyFocus = 3;

	public int shapeKeyCross = 4;

	[Header("Status")]
	public bool isBlink;

	public bool isSleep;

	public bool isSleepManual;

	[Space]
	public bool isEmpty;

	public bool isHeart;

	public bool isTear;

	public bool isEmptyManual;

	public bool isHeartManual;

	public bool isTearManual;

	public bool isTearStop;

	public float emptyEyes;

	public float heartEyes;

	public float tearEyes;

	[Header("Facial Status")]
	public bool isFocus;

	public bool isCross;

	public bool isClose;

	public bool isWinkL;

	public bool isWinkR;

	public float focusEyes;

	public float crossEyes;

	public float closeEyes;

	public float winkLEyes;

	public float winkREyes;

	[Header("Layer Number")]
	public int eyesNumberL = 1;

	public int eyesNumberR = 2;

	public int blinkNumber = 3;

	[Header("Eyes Status")]
	public bool isVaginaInsert;

	public bool isVaginaPiston;

	public bool isVaginaShot;

	public bool isAnalInsert;

	public bool isAnalPiston;

	public bool isAnalShot;

	public bool isMouthInsert;

	public bool isMouthPiston;

	public bool isMouthShot;

	public bool isTitsInsert;

	public bool isTitsPiston;

	public bool isTitsShot;

	[Space]
	public bool isSqueeze;

	[Space]
	public bool isEyesClose01;

	public bool isEyesClose02;

	public bool isEyesClose03;

	[Header("Eyes Open Half Close 3-4")]
	public float eyesSpeed = 0.25f;

	public float eyesReactionSpeed = 0.25f;

	public float eyesNoiseSpeed = 0.1f;

	public float currentEyes;

	[Range(0f, 1f)]
	public float targetEyes;

	[Space]
	public float reactionEyes;

	public float reactionTargetEyes;

	public float noiseEyes;

	public float calcEyes;

	public float calcEyesL;

	public float calcEyesR;

	public AnimationCurve eyesCurve;

	public List<AnimationClip> eyesOpenClipL;

	public List<AnimationClip> eyesOpenClipR;

	private LinearMixerState eyesOpenMixerL;

	private LinearMixerState eyesOpenMixerR;

	public AnimancerLayer eyesOpenLayerL;

	public AnimancerLayer eyesOpenLayerR;

	[Header("Blink 5")]
	public float currentBlinkWait;

	public float waitBlinkMin = 5f;

	public float waitBlinkMax = 10f;

	public float eyesBlinkSpeed;

	public List<AnimationClip> blinkClip;

	public AnimancerLayer blinkLayer;

	[Header("Eyes")]
	public Color eyeColor;

	public Color eyeEmissionColor;

	public Color calcColor;

	[Header("Custom Color")]
	public Color defaultColor;

	public Color defaultLightColor;

	public Color currentColor;

	public Color currentLightColor;

	[Header("Custom Color Data")]
	public bool useCostomColor;

	public bool isCostomColor;

	public bool isLoad;

	public float hueColor;

	public float brightnessColor;

	[Header("Color Save Data")]
	public bool isLoadData;

	public List<float> hueColorData;

	public List<float> brightnessColorData;

	[Header("Tear")]
	public Transform tearL;

	public Transform tearR;

	public ParticleSystem effectTearL;

	public ParticleSystem effectTearR;

	public float tearScale;

	public float tearEmissionMax = 5f;

	private void Awake()
	{
		eyesSkinnedMesh = eyesMesh.sharedMesh;
	}

	private void Start()
	{
		currentBlinkWait = Random.Range(waitBlinkMin, waitBlinkMax);
		ChangeHeartEyes(value: false);
		ChangeEmptyEyes(value: false);
		ChangeTearEyes(value: false);
		heartEyes = 0.01f;
		emptyEyes = 0.01f;
		tearEyes = 0.01f;
		_sideCharacterManger.SetEyes();
	}

	public void InitalizeEyes()
	{
		eyesOpenLayerL = _animancer.Layers[eyesNumberL];
		eyesOpenLayerL.IsAdditive = false;
		eyesOpenLayerR = _animancer.Layers[eyesNumberR];
		eyesOpenLayerR.IsAdditive = false;
		MakeEyesMixer();
		eyesOpenLayerL.Play(eyesOpenMixerL, 0f);
		eyesOpenLayerR.Play(eyesOpenMixerR, 0f);
		blinkLayer = _animancer.Layers[blinkNumber];
		blinkLayer.IsAdditive = true;
		blinkLayer.Play(blinkClip[0], 0.25f);
	}

	public void MakeEyesMixer()
	{
		eyesOpenMixerL = new LinearMixerState();
		eyesOpenMixerL.Add(eyesOpenClipL[0], 0f);
		eyesOpenMixerL.Add(eyesOpenClipL[1], 0.3f);
		eyesOpenMixerL.Add(eyesOpenClipL[2], 0.6f);
		eyesOpenMixerL.Add(eyesOpenClipL[3], 0.95f);
		eyesOpenMixerL.Add(eyesOpenClipL[4], 1f);
		eyesOpenMixerR = new LinearMixerState();
		eyesOpenMixerR.Add(eyesOpenClipR[0], 0f);
		eyesOpenMixerR.Add(eyesOpenClipR[1], 0.3f);
		eyesOpenMixerR.Add(eyesOpenClipR[2], 0.6f);
		eyesOpenMixerR.Add(eyesOpenClipR[3], 0.95f);
		eyesOpenMixerR.Add(eyesOpenClipR[4], 1f);
	}

	private void LateUpdate()
	{
		if (isFocus)
		{
			focusEyes += Time.deltaTime * 5f;
			if (focusEyes > 1f)
			{
				focusEyes = 1f;
			}
		}
		else if (!isFocus)
		{
			focusEyes -= Time.deltaTime * 5f;
			if (focusEyes < 0f)
			{
				focusEyes = 0f;
			}
		}
		eyesMesh.SetBlendShapeWeight(shapeKeyFocus, focusEyes * 100f);
		if (isCross)
		{
			crossEyes += Time.deltaTime * 2f;
			if (crossEyes > 0.5f)
			{
				crossEyes = 0.5f;
			}
		}
		else if (!isCross)
		{
			crossEyes -= Time.deltaTime * 2f;
			if (crossEyes < 0f)
			{
				crossEyes = 0f;
			}
		}
		eyesMesh.SetBlendShapeWeight(shapeKeyCross, crossEyes * 100f);
		if (isClose)
		{
			closeEyes += Time.deltaTime * 5f;
			if (closeEyes > 1f)
			{
				closeEyes = 1f;
			}
		}
		else if (!isClose)
		{
			closeEyes -= Time.deltaTime * 5f;
			if (closeEyes < 0f)
			{
				closeEyes = 0f;
			}
		}
		if (isWinkL)
		{
			winkLEyes += Time.deltaTime * 5f;
			if (winkLEyes > 1f)
			{
				winkLEyes = 1f;
			}
		}
		else if (!isWinkL)
		{
			winkLEyes -= Time.deltaTime * 5f;
			if (winkLEyes < 0f)
			{
				winkLEyes = 0f;
			}
		}
		if (isWinkR)
		{
			winkREyes += Time.deltaTime * 5f;
			if (winkREyes > 1f)
			{
				winkREyes = 1f;
			}
		}
		else if (!isWinkR)
		{
			winkREyes -= Time.deltaTime * 5f;
			if (winkREyes < 0f)
			{
				winkREyes = 0f;
			}
		}
		if (isEmpty && emptyEyes != 1f)
		{
			emptyEyes += Time.deltaTime;
			if (emptyEyes > 1f)
			{
				emptyEyes = 1f;
			}
			EmptyEyes();
		}
		else if (!isEmpty && emptyEyes != 0f)
		{
			emptyEyes -= Time.deltaTime;
			if (emptyEyes < 0f)
			{
				emptyEyes = 0f;
			}
			EmptyEyes();
		}
		if (isHeart && heartEyes != 1f)
		{
			heartEyes += Time.deltaTime;
			if (heartEyes > 1f)
			{
				heartEyes = 1f;
			}
			HeartEyes();
		}
		else if (!isHeart && heartEyes != 0f)
		{
			heartEyes -= Time.deltaTime;
			if (heartEyes < 0f)
			{
				heartEyes = 0f;
			}
			HeartEyes();
		}
		if (isTear)
		{
			tearEyes += Time.deltaTime * 2f;
			if (tearEyes > 1f)
			{
				tearEyes = 1f;
			}
			TearEyes();
		}
		else if (!isTear)
		{
			tearEyes -= Time.deltaTime * 2f;
			if (tearEyes < 0f)
			{
				tearEyes = 0f;
			}
			TearEyes();
		}
		currentBlinkWait -= Time.deltaTime;
		if (currentBlinkWait < 0f)
		{
			BlinkOn();
			currentBlinkWait = Random.Range(waitBlinkMin, waitBlinkMax);
		}
		targetEyes = 0f;
		if (isVaginaInsert)
		{
			targetEyes += 0.1f;
		}
		if (isVaginaPiston)
		{
			targetEyes += 0.1f;
		}
		if (isVaginaShot)
		{
			targetEyes += 0.2f;
		}
		if (isAnalInsert)
		{
			targetEyes += 0.1f;
		}
		if (isAnalPiston)
		{
			targetEyes += 0.1f;
		}
		if (isAnalShot)
		{
			targetEyes += 0.2f;
		}
		if (isMouthInsert)
		{
			targetEyes += 0.2f;
		}
		if (isMouthPiston)
		{
			targetEyes += 0.2f;
		}
		if (isMouthShot)
		{
			targetEyes += 0.3f;
		}
		if (isTitsInsert)
		{
			targetEyes += 0.1f;
		}
		if (isTitsPiston)
		{
			targetEyes += 0.1f;
		}
		if (isTitsShot)
		{
			targetEyes += 0.2f;
		}
		if (isSqueeze)
		{
			targetEyes += 0.3f;
		}
		if (isEyesClose01)
		{
			targetEyes += 0.1f;
		}
		if (isEyesClose02)
		{
			targetEyes += 0.2f;
		}
		if (isEyesClose03)
		{
			targetEyes += 0.3f;
		}
		if (isWinkL || isWinkR)
		{
			targetEyes += 0.3f;
		}
		if (targetEyes > 0.7f)
		{
			targetEyes = 0.7f;
		}
		if (isSleep)
		{
			targetEyes = 1f;
		}
		if (!isSleep)
		{
			currentEyes = Mathf.MoveTowards(currentEyes, targetEyes, Time.deltaTime * eyesSpeed);
		}
		else
		{
			currentEyes = Mathf.MoveTowards(currentEyes, targetEyes, Time.deltaTime);
		}
		reactionEyes = Mathf.MoveTowards(reactionEyes, reactionTargetEyes, Time.deltaTime * eyesReactionSpeed);
		noiseEyes = Mathf.PerlinNoise(calcEyes, Time.time * eyesNoiseSpeed) * 2f - 1f;
		noiseEyes *= 0.1f;
		if (isSleep && noiseEyes < 0f)
		{
			noiseEyes = 0f;
		}
		if (reactionTargetEyes > 0f)
		{
			reactionTargetEyes -= Time.deltaTime * eyesReactionSpeed;
			if (reactionTargetEyes < 0f)
			{
				reactionTargetEyes = 0f;
			}
		}
		calcEyes = currentEyes + noiseEyes + reactionEyes;
		if (calcEyes > 1f)
		{
			calcEyes = 1f;
		}
		else if (calcEyes < 0f)
		{
			calcEyes = 0f;
		}
		calcEyesL = eyesCurve.Evaluate(calcEyes) + winkLEyes + closeEyes;
		if (calcEyesL > 1f)
		{
			calcEyesL = 1f;
		}
		eyesOpenMixerL.Parameter = calcEyesL;
		calcEyesR = eyesCurve.Evaluate(calcEyes) + winkREyes + closeEyes;
		if (calcEyesR > 1f)
		{
			calcEyesR = 1f;
		}
		eyesOpenMixerR.Parameter = calcEyesR;
		for (int i = 0; i < eyesSkinnedMesh.blendShapeCount; i++)
		{
			if (eyesMesh.GetBlendShapeWeight(i) > 100f)
			{
				eyesMesh.SetBlendShapeWeight(i, 100f);
			}
			else if (eyesMesh.GetBlendShapeWeight(i) < -100f)
			{
				eyesMesh.SetBlendShapeWeight(i, -100f);
			}
		}
	}

	private void OnDestroy()
	{
		isCostomColor = false;
		calcColor = eyesMaterial.GetColor("_Color3rd");
		calcColor.a = 0f;
		eyesMaterial.SetColor("_Color3rd", calcColor);
		calcColor = eyesMaterial.GetColor("_EmissionColor");
		calcColor.a = 0.5f;
		eyesMaterial.SetColor("_EmissionColor", calcColor);
		calcColor = eyesMaterial.GetColor("_Emission2ndColor");
		calcColor.a = 0f;
		eyesMaterial.SetColor("_Emission2ndColor", calcColor);
	}

	public void BlinkOn()
	{
		isBlink = true;
		eyesBlinkSpeed = Random.Range(0.1f, 0.2f);
		blinkLayer.Play(blinkClip[1], eyesBlinkSpeed);
	}

	public void BlinkOff()
	{
		isBlink = false;
		blinkLayer.Play(blinkClip[0], 0.25f);
	}

	public void ReactionEyes(float valueMin, float valueMax)
	{
		reactionTargetEyes = Random.Range(valueMin, valueMax);
	}

	public void SetMaterial()
	{
		if (!isCostomColor)
		{
			eyeColor = defaultColor;
		}
		else
		{
			eyeColor = currentColor;
		}
		eyesMaterial.SetColor("_Color", eyeColor);
		Color value = Color.Lerp(eyeColor, Color.white, 0.5f);
		value.a = 0.75f;
		eyesMaterial.SetColor("_Color2nd", value);
		value = eyeColor;
		value.a = emptyEyes;
		eyesMaterial.SetColor("_Color3rd", value);
		eyeEmissionColor = eyeColor;
		eyeEmissionColor.a = (1f - emptyEyes) / 2f;
		eyesMaterial.SetColor("_EmissionColor", eyeEmissionColor);
		ChangeEmptyEyes(isEmpty);
		ChangeHeartEyes(isHeart);
	}

	public void SetManualEmptyEyes()
	{
		isEmptyManual = !isEmptyManual;
		EmptyEyes();
		_sideCharacterManger.isEmpty = isEmptyManual;
		_sideCharacterManger.SetEyes();
	}

	public void SetEmptyEyes()
	{
		_characterFacialManager.isEmpty = !isEmpty;
		ChangeEmptyEyes(!isEmpty);
	}

	public void ChangeEmptyEyes(bool value)
	{
		isEmpty = value;
	}

	public void EmptyEyes()
	{
		if (isEmptyManual)
		{
			calcColor = eyesMaterial.GetColor("_Color3rd");
			calcColor.a = 1f;
			eyesMaterial.SetColor("_Color3rd", calcColor);
			calcColor = eyesMaterial.GetColor("_EmissionColor");
			calcColor.a = 0f;
			eyesMaterial.SetColor("_EmissionColor", calcColor);
		}
		else
		{
			calcColor = eyesMaterial.GetColor("_Color3rd");
			calcColor.a = emptyEyes;
			eyesMaterial.SetColor("_Color3rd", calcColor);
			calcColor = eyesMaterial.GetColor("_EmissionColor");
			calcColor.a = (1f - emptyEyes) / 2f;
			eyesMaterial.SetColor("_EmissionColor", calcColor);
		}
	}

	public void SetManualHeartEyes()
	{
		isHeartManual = !isHeartManual;
		HeartEyes();
		_sideCharacterManger.isHeart = isHeartManual;
		_sideCharacterManger.SetEyes();
	}

	public void SetHeartEyes()
	{
		_characterFacialManager.isHeart = !isHeart;
		ChangeHeartEyes(!isHeart);
	}

	public void ChangeHeartEyes(bool value)
	{
		isHeart = value;
	}

	public void HeartEyes()
	{
		if (isHeartManual)
		{
			calcColor = eyesMaterial.GetColor("_Emission2ndColor");
			calcColor.a = 1f;
			eyesMaterial.SetColor("_Emission2ndColor", calcColor);
		}
		else
		{
			calcColor = eyesMaterial.GetColor("_Emission2ndColor");
			calcColor.a = heartEyes;
			eyesMaterial.SetColor("_Emission2ndColor", calcColor);
		}
	}

	public void SetManualTearEyes()
	{
		isTearManual = !isTearManual;
		TearEyes();
		_sideCharacterManger.isTear = isTearManual;
		_sideCharacterManger.SetEyes();
	}

	public void SetTearEyes()
	{
		_characterFacialManager.isTear = !isTear;
		ChangeTearEyes(!isTear);
	}

	public void ChangeTearEyes(bool value)
	{
		isTear = value;
	}

	public void TearStop(bool value)
	{
		isTearStop = value;
		if (isTearStop)
		{
			ParticleSystem.EmissionModule emission = effectTearL.emission;
			emission.rateOverTime = new ParticleSystem.MinMaxCurve(0f, 0f);
			emission = effectTearR.emission;
			emission.rateOverTime = new ParticleSystem.MinMaxCurve(0f, 0f);
		}
		else if (!isTearStop)
		{
			if (isTearManual)
			{
				ParticleSystem.EmissionModule emission2 = effectTearL.emission;
				emission2.rateOverTime = new ParticleSystem.MinMaxCurve(0f, tearEmissionMax);
				emission2 = effectTearR.emission;
				emission2.rateOverTime = new ParticleSystem.MinMaxCurve(0f, tearEmissionMax);
			}
			else
			{
				ParticleSystem.EmissionModule emission3 = effectTearL.emission;
				emission3.rateOverTime = new ParticleSystem.MinMaxCurve(0f, tearEyes * tearEmissionMax);
				emission3 = effectTearR.emission;
				emission3.rateOverTime = new ParticleSystem.MinMaxCurve(0f, tearEyes * tearEmissionMax);
			}
		}
	}

	public void TearEyes()
	{
		if (isTearManual)
		{
			tearL.localScale = Vector3.one;
			tearR.localScale = new Vector3(-1f, 1f, 1f);
			tearL.gameObject.SetActive(value: true);
			tearR.gameObject.SetActive(value: true);
			ParticleSystem.EmissionModule emission = effectTearL.emission;
			emission.rateOverTime = new ParticleSystem.MinMaxCurve(0f, tearEmissionMax);
			emission = effectTearR.emission;
			emission.rateOverTime = new ParticleSystem.MinMaxCurve(0f, tearEmissionMax);
			return;
		}
		Vector3 localScale = Vector3.one * tearEyes;
		Vector3 localScale2 = new Vector3(0f - tearEyes, tearEyes, tearEyes);
		tearL.localScale = localScale;
		tearR.localScale = localScale2;
		if (tearEyes <= 0f && tearL.gameObject.activeSelf)
		{
			tearL.gameObject.SetActive(value: false);
			tearR.gameObject.SetActive(value: false);
		}
		else if (tearEyes > 0f && !tearL.gameObject.activeSelf)
		{
			tearL.gameObject.SetActive(value: true);
			tearR.gameObject.SetActive(value: true);
		}
		ParticleSystem.EmissionModule emission2 = effectTearL.emission;
		emission2.rateOverTime = new ParticleSystem.MinMaxCurve(0f, tearEyes * tearEmissionMax);
		emission2 = effectTearR.emission;
		emission2.rateOverTime = new ParticleSystem.MinMaxCurve(0f, tearEyes * tearEmissionMax);
	}

	public void SetManualSleepEyes()
	{
		_characterFacialManager.isSleep = !_characterFacialManager.isSleep;
		isSleep = _characterFacialManager.isSleep;
		_sideCharacterManger.isSleep = isSleep;
		_sideCharacterManger.SetEyes();
	}

	public void SetDefaultColor()
	{
		isCostomColor = false;
		SetMaterial();
	}

	public void SetCustomColor(Color baseColor, Color ligghtColor)
	{
		currentColor = baseColor;
		currentLightColor = ligghtColor;
		isCostomColor = true;
		SetMaterial();
	}

	public void SaveColor()
	{
		ES3.Save("Eyes_HueColor", hueColor);
		ES3.Save("Eyes_BrightnessColor", brightnessColor);
	}

	public void LoadColor()
	{
		isLoad = true;
		if (ES3.KeyExists("Eyes_HueColor"))
		{
			hueColor = ES3.Load<float>("Eyes_HueColor");
			brightnessColor = ES3.Load<float>("Eyes_BrightnessColor");
		}
		else
		{
			SaveColor();
		}
	}

	public void SaveColorData()
	{
		ES3.Save("Eyes_HueColorData", hueColorData);
		ES3.Save("Eyes_BrightnessColorData", brightnessColorData);
	}

	public void LoadColorData()
	{
		isLoadData = true;
		if (ES3.KeyExists("Eyes_HueColorData"))
		{
			hueColorData = ES3.Load<List<float>>("Eyes_HueColorData");
			brightnessColorData = ES3.Load<List<float>>("Eyes_BrightnessColorData");
		}
		else
		{
			SaveColorData();
		}
	}
}
