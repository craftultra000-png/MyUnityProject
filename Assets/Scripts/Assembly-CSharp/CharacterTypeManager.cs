using System.Collections.Generic;
using PaintIn3D;
using UnityEngine;

public class CharacterTypeManager : MonoBehaviour
{
	public CharacterHairManager _characterHairManager;

	public CharacterEyesManager _characterEyesManager;

	public CharacterCostumeSwitch _characterCostumeSwitch;

	public CharacterSoundManager _characterSoundManager;

	public CharacterEffectManager _characterEffectManager;

	public CharacterWetManager _characterWetManager;

	public SideCharacterManger _sideCharacterManger;

	public CustomColorGUI _customColorGUI;

	[Header("Code")]
	public CwPaintableMeshTexture headPaintable;

	public CwPaintableMeshTexture bodyPaintable;

	public Paint3DManager headPaint3DManager;

	public Paint3DManager bodyPaint3DManager;

	[Header("Type")]
	public int characterType = 1;

	public int nippleColor;

	[Header("Status")]
	public bool isWet;

	public bool lewdCrest;

	public bool isInitalize;

	public bool isDestroy;

	[Header("Material")]
	public Material headMaterial;

	public Material bodyMaterial;

	public Material eyesMaterial;

	public Material eyesLashMaterial;

	public Material underHairMaterial;

	public Material hairMaterial;

	[Header("Texture")]
	public List<Texture> headTexture;

	public List<Texture> bodyTexture;

	public List<Texture> bodyBTexture;

	public List<Texture> blushTexture;

	[Header("Custom Color")]
	public Color hairCurrentColor;

	public List<Color> hairColor;

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

	private void Awake()
	{
		lewdCrest = true;
		SetLewdCrest();
		_sideCharacterManger.Initalize(characterType);
	}

	private void OnDestroy()
	{
		isDestroy = true;
		isCostomColor = false;
		characterType = 1;
		nippleColor = 0;
		ChangeCharacter(characterType);
		SetDefaultColor();
		lewdCrest = true;
		SetLewdCrest();
	}

	public void MaterialWet(bool wet)
	{
		if (isInitalize && !isDestroy)
		{
			_characterSoundManager.Change();
			_characterEffectManager.CostumeChange();
		}
		isWet = wet;
		if (isWet)
		{
			headMaterial.SetInt("_UseMatCap", 1);
			bodyMaterial.SetInt("_UseMatCap", 1);
			hairMaterial.SetInt("_UseMatCap", 1);
		}
		else
		{
			headMaterial.SetInt("_UseMatCap", 0);
			bodyMaterial.SetInt("_UseMatCap", 0);
			hairMaterial.SetInt("_UseMatCap", 0);
		}
	}

	public void ChangeSkin(int value)
	{
		ChangeCharacter(value);
	}

	public void ChangeCharacter(int value)
	{
		characterType = value;
		headPaint3DManager.mainTex = headTexture[characterType];
		headPaintable.Texture = headTexture[characterType];
		headPaintable.Replace();
		headMaterial.SetTexture("_MainTex", headTexture[characterType]);
		headMaterial.SetTexture("_MainTex2nd", blushTexture[characterType]);
		headMaterial.SetTexture("_MainTex3rd", headTexture[characterType]);
		ChangeBody(nippleColor);
		_characterEyesManager.SetMaterial();
		SetHairColor();
	}

	public void ChangeBody(int value)
	{
		nippleColor = value;
		if (nippleColor == 0)
		{
			bodyPaint3DManager.mainTex = bodyTexture[characterType];
			bodyPaintable.Texture = bodyTexture[characterType];
			bodyPaintable.Replace();
			bodyMaterial.SetTexture("_MainTex", bodyTexture[characterType]);
		}
		else if (nippleColor == 1)
		{
			bodyPaint3DManager.mainTex = bodyBTexture[characterType];
			bodyPaintable.Texture = bodyBTexture[characterType];
			bodyPaintable.Replace();
			bodyMaterial.SetTexture("_MainTex", bodyBTexture[characterType]);
		}
		if (isInitalize && !isDestroy)
		{
			_characterSoundManager.Change();
			_characterEffectManager.NippleChange();
		}
	}

	public void SetLewdCrest()
	{
		lewdCrest = !lewdCrest;
		_sideCharacterManger.lewdCrest = lewdCrest;
		Color color = bodyMaterial.GetColor("_EmissionColor");
		if (!lewdCrest)
		{
			color.a = 0f;
		}
		else
		{
			color.a = 1f;
		}
		bodyMaterial.SetColor("_EmissionColor", color);
		if (isInitalize && !isDestroy)
		{
			_characterSoundManager.Change();
			_characterEffectManager.BellyChange();
		}
	}

	public void SetHairColor()
	{
		if (!isCostomColor)
		{
			hairCurrentColor = defaultColor;
		}
		else
		{
			hairCurrentColor = currentColor;
		}
		hairMaterial.SetColor("_Color", hairCurrentColor);
		Color value = Color.Lerp(hairCurrentColor, Color.white, 0.5f);
		hairMaterial.SetColor("_MatCapColor", value);
		headMaterial.SetColor("_Color3rd", hairCurrentColor);
		eyesLashMaterial.SetColor("_Color", hairCurrentColor);
		underHairMaterial.SetColor("_Color", hairCurrentColor);
	}

	public void SetDefaultColor()
	{
		isCostomColor = false;
		SetHairColor();
	}

	public void SetCustomColor(Color baseColor, Color ligghtColor)
	{
		currentColor = baseColor;
		currentLightColor = ligghtColor;
		isCostomColor = true;
		SetHairColor();
	}

	public void SaveColor()
	{
		ES3.Save("Hair_HueColor", hueColor);
		ES3.Save("Hair_BrightnessColor", brightnessColor);
	}

	public void LoadColor()
	{
		isLoad = true;
		if (ES3.KeyExists("Hair_HueColor"))
		{
			hueColor = ES3.Load<float>("Hair_HueColor");
			brightnessColor = ES3.Load<float>("Hair_BrightnessColor");
		}
		else
		{
			SaveColor();
		}
	}

	public void SaveColorData()
	{
		ES3.Save("Hair_HueColorData", hueColorData);
		ES3.Save("Hair_BrightnessColorData", brightnessColorData);
	}

	public void LoadColorData()
	{
		isLoadData = true;
		if (ES3.KeyExists("Hair_HueColorData"))
		{
			hueColorData = ES3.Load<List<float>>("Hair_HueColorData");
			brightnessColorData = ES3.Load<List<float>>("Hair_BrightnessColorData");
		}
		else
		{
			SaveColorData();
		}
	}
}
