using System.Collections.Generic;
using UnityEngine;

public class CharacterCostumeSwitch : MonoBehaviour
{
	public TorsoManager _torsoManager;

	public SideCostumeManager _sideCostumeManager;

	public CharacterSoundManager _characterSoundManager;

	public CustomColorGUI _customColorGUI;

	public CharacterCountGUI _characterCountGUI;

	public List<CharacterCostumeManager> _characterCostumeManager;

	public List<string> costumeSetName;

	[Header("Data")]
	public int costume;

	public int wearCount;

	public int nakedCount;

	public bool isWet;

	public bool isInitalize;

	[Header("Onomatopoeia")]
	public Transform onomatopoeiaVagina;

	public Transform onomatopoeiaChest;

	[Header("UnderHair")]
	public bool underhair;

	public GameObject underhairObject;

	[Header("Custom Color Data")]
	public bool useCostomColor;

	public bool isCostomColor;

	public Color defaultColor;

	public float hueColor;

	public float brightnessColor;

	[Header("Color Save Data")]
	public List<float> hueColorData;

	public List<float> brightnessColorData;

	[Header("Effect")]
	public Transform effectRootPosition;

	public GameObject costumeChangeEfefct;

	public GameObject underHairChangeEfefct;

	private void Start()
	{
		for (int i = 0; i < _characterCostumeManager.Count; i++)
		{
			_characterCostumeManager[i].gameObject.SetActive(value: true);
		}
		SetCostume(costume);
	}

	public void MaterialWet(bool wet)
	{
		isWet = wet;
		for (int i = 0; i < _characterCostumeManager.Count; i++)
		{
			_characterCostumeManager[i].MaterialWet(isWet);
		}
	}

	public void RootEffect()
	{
		if (isInitalize)
		{
			_characterSoundManager.Change();
			Object.Instantiate(costumeChangeEfefct, effectRootPosition.position, effectRootPosition.rotation, base.transform);
		}
	}

	public void CostumeChange(int value)
	{
		RootEffect();
		SetCostume(value);
		GetCustomColorData();
		_customColorGUI.GetCustomColor();
		if (OnomatopoeiaManager.instance.useOtomanopoeia)
		{
			OnomatopoeiaManager.instance.SpawnOnomatopoeia(onomatopoeiaVagina.position, null, "CostumeChange", Camera.main.transform);
			OnomatopoeiaManager.instance.SpawnOnomatopoeia(onomatopoeiaChest.position, null, "CostumeChange", Camera.main.transform);
		}
	}

	public void SetCostume(int value)
	{
		costume = value;
		if (_characterCountGUI != null)
		{
			_characterCountGUI.costume = costume;
		}
		if (_torsoManager != null)
		{
			_torsoManager.SetIcon(value);
		}
		for (int i = 0; i < _characterCostumeManager.Count; i++)
		{
			if (i == costume)
			{
				_characterCostumeManager[i].ChangeCostumeSet();
				if (_torsoManager != null)
				{
					_torsoManager._characterCostumeManager = _characterCostumeManager[i];
				}
			}
			else
			{
				_characterCostumeManager[i].DisableCostumeSet();
			}
		}
	}

	public void SetUnderHair()
	{
		Debug.LogError("Underhair", base.gameObject);
		underhair = !underhair;
		underhairObject.SetActive(!underhair);
		if (isInitalize)
		{
			_characterSoundManager.Change();
			Object.Instantiate(underHairChangeEfefct, effectRootPosition.position, effectRootPosition.rotation, base.transform);
		}
	}

	public void GetCustomColorData()
	{
		defaultColor = _characterCostumeManager[costume].defaultColor;
		useCostomColor = _characterCostumeManager[costume].useCostomColor;
		isCostomColor = _characterCostumeManager[costume].isCostomColor;
		hueColor = _characterCostumeManager[costume].hueColor;
		brightnessColor = _characterCostumeManager[costume].brightnessColor;
		if (_characterCostumeManager[costume].isLoad)
		{
			_characterCostumeManager[costume].LoadColorData();
		}
		hueColorData = _characterCostumeManager[costume].hueColorData;
		brightnessColorData = _characterCostumeManager[costume].brightnessColorData;
	}

	public void SetCustomColorData()
	{
		_characterCostumeManager[costume].isCostomColor = isCostomColor;
		_characterCostumeManager[costume].useCostomColor = useCostomColor;
		_characterCostumeManager[costume].hueColor = hueColor;
		_characterCostumeManager[costume].brightnessColor = brightnessColor;
		SaveColor();
		if (_characterCostumeManager[costume].isLoad)
		{
			LoadColorData();
			hueColorData = _characterCostumeManager[costume].hueColorData;
			brightnessColorData = _characterCostumeManager[costume].brightnessColorData;
			_customColorGUI.hueColorData = hueColorData;
			_customColorGUI.brightnessColorData = brightnessColorData;
		}
		else
		{
			hueColorData = _customColorGUI.hueColorData;
			brightnessColorData = _customColorGUI.brightnessColorData;
			_characterCostumeManager[costume].hueColorData = hueColorData;
			_characterCostumeManager[costume].brightnessColorData = brightnessColorData;
		}
	}

	public void SaveColor()
	{
		_characterCostumeManager[costume].SaveColor();
	}

	public void LoadColor()
	{
		_characterCostumeManager[costume].LoadColor();
		hueColor = _characterCostumeManager[costume].hueColor;
		brightnessColor = _characterCostumeManager[costume].brightnessColor;
	}

	public void SaveColorData()
	{
		_characterCostumeManager[costume].SaveColorData();
	}

	public void LoadColorData()
	{
		_characterCostumeManager[costume].LoadColorData();
	}

	public void SetDefaultColor()
	{
		_characterCostumeManager[costume].SetDefaultColor();
	}

	public void SetCustomColor(Color baseColor, Color ligghtColor)
	{
		_characterCostumeManager[costume].SetCustomColor(baseColor, ligghtColor);
	}
}
