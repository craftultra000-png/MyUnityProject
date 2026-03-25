using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class WorldLightManager : MonoBehaviour
{
	public Light directionalLight;

	[Header("Status")]
	public int type;

	public bool isDark;

	[Header("Default")]
	[ColorUsage(true, true)]
	public List<Color> skyColorList;

	[ColorUsage(true, true)]
	public List<Color> equatorColorList;

	[ColorUsage(true, true)]
	public List<Color> groundColorList;

	[Space]
	public List<Color> fogColorList;

	[Space]
	[ColorUsage(true, true)]
	public List<Color> lightColorList;

	public List<float> directionalIntensityList;

	[Header("Tmp")]
	[ColorUsage(true, true)]
	public Color skyColorTmp;

	[ColorUsage(true, true)]
	public Color equatorColorTmp;

	[ColorUsage(true, true)]
	public Color groundColorTmp;

	[Space]
	public Color fogColorTmp;

	[Space]
	[ColorUsage(true, true)]
	public Color lightColorTmp;

	public float directionalIntensityTmp = 0.05f;

	[Header("Default")]
	[ColorUsage(true, true)]
	public Color skyColor;

	[ColorUsage(true, true)]
	public Color equatorColor;

	[ColorUsage(true, true)]
	public Color groundColor;

	[Space]
	public Color fogColor;

	[Space]
	[ColorUsage(true, true)]
	public Color lightColor;

	public float directionalIntensity = 0.05f;

	[Header("Dark")]
	[ColorUsage(true, true)]
	public Color skyDarkColor;

	[ColorUsage(true, true)]
	public Color equatorDarkColor;

	[ColorUsage(true, true)]
	public Color groundDarkColor;

	[Space]
	public Color fogDarkColor;

	[Space]
	[ColorUsage(true, true)]
	public Color lightDarkColor;

	public float directionalDarkIntensity = 0.05f;

	[Header("Camera Spot Light")]
	public Light cameraSpotLight;

	public List<Color> playerLightColorList;

	public List<float> spotIntensityList;

	public List<float> spotShapeInnerList;

	public List<float> spotShapeOuterList;

	public Color playerLightColor;

	public Color playerLightDarkColor;

	public float spotIntensityTmp = 0.1f;

	public float spotIntensity = 0.1f;

	public float spotIntensityDark = 0.5f;

	[Header("DeepMaterial")]
	public Material deepMaterial;

	[ColorUsage(true, true)]
	public List<Color> deepColorList;

	[ColorUsage(true, true)]
	public Color deepColor;

	[ColorUsage(true, true)]
	public Color deepDarkColor;

	[Header("JerryFish")]
	public List<Material> jellyFishMaterial;

	public List<float> emitJellyPowerList;

	[ColorUsage(false, true)]
	public List<Color> colorJellyPowerList;

	[Header("Card")]
	public Material cardMaterial;

	public List<float> emitCardPowerList;

	[ColorUsage(false, true)]
	public List<Color> colorCardPowerList;

	[Header("Emit")]
	public float emitPowerOn = 0.5f;

	public float emitPowerOff = 0.1f;

	[ColorUsage(false, true)]
	public Color colorPowerOn;

	[ColorUsage(false, true)]
	public Color colorPowerOff;

	private void Start()
	{
		SetWorldLight(0);
	}

	public void ChangeLight()
	{
		type++;
		if (type >= skyColorList.Count)
		{
			type = 0;
		}
		SetWorldLight(type);
	}

	public void SetWorldLight(int value)
	{
		Debug.LogError("Set WorldLight: " + value, base.gameObject);
		type = value;
		RenderSettings.ambientMode = AmbientMode.Trilight;
		RenderSettings.ambientSkyColor = skyColorList[type];
		RenderSettings.ambientEquatorColor = equatorColorList[type];
		RenderSettings.ambientGroundColor = groundColorList[type];
		RenderSettings.fogColor = fogColorList[type];
		directionalLight.color = lightColorList[type];
		directionalLight.intensity = directionalIntensityList[type];
		cameraSpotLight.color = playerLightColorList[type];
		cameraSpotLight.intensity = spotIntensityList[type];
		cameraSpotLight.innerSpotAngle = spotShapeInnerList[type];
		cameraSpotLight.spotAngle = spotShapeOuterList[type];
		deepMaterial.SetColor("_EmissionColor", deepColorList[type]);
		for (int i = 0; i < jellyFishMaterial.Count; i++)
		{
			jellyFishMaterial[i].SetFloat("_EmissionSelfGlow", emitJellyPowerList[type]);
			jellyFishMaterial[i].SetColor("_EmissionColor", colorJellyPowerList[type]);
		}
		if (cardMaterial != null)
		{
			cardMaterial.SetFloat("_EmissionSelfGlow", emitCardPowerList[type]);
			cardMaterial.SetColor("_EmissionColor", colorCardPowerList[type]);
		}
	}

	public void SetResultColor()
	{
		if (cardMaterial != null)
		{
			cardMaterial.SetFloat("_EmissionSelfGlow", emitCardPowerList[0]);
			cardMaterial.SetColor("_EmissionColor", colorCardPowerList[0]);
		}
	}

	[ContextMenu("Set Default")]
	public void SetDefault()
	{
		Debug.LogError("Set Default WorldLight", base.gameObject);
		RenderSettings.ambientMode = AmbientMode.Trilight;
		RenderSettings.ambientSkyColor = skyColor;
		RenderSettings.ambientEquatorColor = equatorColor;
		RenderSettings.ambientGroundColor = groundColor;
		RenderSettings.fogColor = fogColor;
		directionalLight.color = lightColor;
		directionalLight.intensity = directionalIntensity;
		cameraSpotLight.color = playerLightColor;
		cameraSpotLight.intensity = spotIntensity;
		deepMaterial.SetColor("_EmissionColor", deepColor);
		for (int i = 0; i < jellyFishMaterial.Count; i++)
		{
			jellyFishMaterial[i].SetFloat("_EmissionSelfGlow", emitPowerOn);
			jellyFishMaterial[i].SetColor("_EmissionColor", colorPowerOn);
		}
		cardMaterial.SetFloat("_EmissionSelfGlow", emitPowerOn);
		cardMaterial.SetColor("_EmissionColor", colorPowerOn);
	}

	[ContextMenu("Set Dark")]
	public void SetDark()
	{
		Debug.LogError("Set Dark WorldLight", base.gameObject);
		RenderSettings.ambientMode = AmbientMode.Trilight;
		RenderSettings.ambientSkyColor = skyDarkColor;
		RenderSettings.ambientEquatorColor = equatorDarkColor;
		RenderSettings.ambientGroundColor = groundDarkColor;
		RenderSettings.fogColor = fogDarkColor;
		directionalLight.color = lightDarkColor;
		directionalLight.intensity = directionalDarkIntensity;
		cameraSpotLight.color = playerLightDarkColor;
		cameraSpotLight.intensity = spotIntensityDark;
		deepMaterial.SetColor("_EmissionColor", deepDarkColor);
		for (int i = 0; i < jellyFishMaterial.Count; i++)
		{
			jellyFishMaterial[i].SetFloat("_EmissionSelfGlow", emitPowerOff);
			jellyFishMaterial[i].SetColor("_EmissionColor", colorPowerOff);
		}
		cardMaterial.SetFloat("_EmissionSelfGlow", emitPowerOff);
		cardMaterial.SetColor("_EmissionColor", colorPowerOff);
	}
}
