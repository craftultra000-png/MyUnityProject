using System.Collections.Generic;
using UnityEngine;

public class CharacterCostumeManager : MonoBehaviour
{
	public CharacterFaceManager _characterFaceManager;

	public CharacterMouthManager _characterMouthManager;

	public CharacterEffectManager _characterEffectManager;

	public CharacterVaginaManager _characterVaginaManager;

	public CharacterCostumeDamageManager _characterCostumeDamageManager;

	public TorsoManager _torsoManager;

	public CharacterCostumeSwitch _characterCostumeSwitch;

	[Header("Model")]
	public List<GameObject> clotheModel;

	public List<CharacterCostumeObject> clotheScript;

	public List<SkinnedMeshRenderer> customColorSkinnedMesh;

	public List<MeshRenderer> customColorMesh;

	private Material clothMaterial;

	[Header("Status")]
	public bool isInitialize;

	public bool isWet;

	public int costume;

	public int wearCount;

	public int nakedCount;

	public List<bool> equip;

	public List<bool> unlock;

	[Header("Custom Color Data")]
	public bool useCostomColor;

	public bool isCostomColor;

	public bool isLoad;

	public float hueColor;

	public float brightnessColor;

	[Header("Custom Color")]
	public bool useMatcap2nd;

	public Color defaultColor;

	public Color defaultLightColor;

	public Color currentColor;

	public Color currentLightColor;

	[Header("Color Save Data")]
	public string costumeName = "SaveName";

	public bool isLoadData;

	public List<float> hueColorData;

	public List<float> brightnessColorData;

	[Header("Mosaic")]
	public bool isMosaic;

	[Header("Effect")]
	public GameObject breakCloseEffect;

	public GameObject meltCloseEffect;

	public Transform effectStocker;

	public AudioSource _audioSource;

	public AudioClip breakSe;

	public AudioClip meltSe;

	public AudioClip wareSe;

	private void Start()
	{
		InitializeCostumeSet();
		SetDefaultColor();
		MaterialWet(isWet);
	}

	private void OnDestroy()
	{
		SetDefaultColor();
	}

	public void MaterialWet(bool wet)
	{
		isWet = wet;
		for (int i = 0; i < clotheScript.Count; i++)
		{
			if (clotheScript[i]._mesh != null)
			{
				if (isWet)
				{
					clotheScript[i]._mesh.sharedMaterial.SetInt("_UseMatCap", 1);
				}
				else
				{
					clotheScript[i]._mesh.sharedMaterial.SetInt("_UseMatCap", 0);
				}
			}
			if (clotheScript[i]._skinnedMesh != null)
			{
				if (isWet)
				{
					clotheScript[i]._skinnedMesh.sharedMaterial.SetInt("_UseMatCap", 1);
				}
				else
				{
					clotheScript[i]._skinnedMesh.sharedMaterial.SetInt("_UseMatCap", 0);
				}
			}
		}
	}

	public void SetDefaultColor()
	{
		for (int i = 0; i < customColorSkinnedMesh.Count; i++)
		{
			Material material = customColorSkinnedMesh[i].material;
			material.SetColor("_Color", defaultColor);
			if (useMatcap2nd)
			{
				material.SetColor("_MatCap2ndColor", defaultLightColor);
			}
		}
		for (int j = 0; j < customColorMesh.Count; j++)
		{
			Material material2 = customColorMesh[j].material;
			material2.SetColor("_Color", defaultColor);
			if (useMatcap2nd)
			{
				material2.SetColor("_MatCap2ndColor", defaultLightColor);
			}
		}
	}

	public void SetCustomColor(Color baseColor, Color ligghtColor)
	{
		currentColor = baseColor;
		currentLightColor = ligghtColor;
		for (int i = 0; i < customColorSkinnedMesh.Count; i++)
		{
			Material material = customColorSkinnedMesh[i].material;
			material.SetColor("_Color", currentColor);
			if (useMatcap2nd)
			{
				material.SetColor("_MatCap2ndColor", currentLightColor);
			}
		}
		for (int j = 0; j < customColorMesh.Count; j++)
		{
			Material material2 = customColorMesh[j].material;
			material2.SetColor("_Color", currentColor);
			if (useMatcap2nd)
			{
				material2.SetColor("_MatCap2ndColor", currentLightColor);
			}
		}
	}

	public void SaveColor()
	{
		ES3.Save("Costume_" + costumeName + "_HueColor", hueColor);
		ES3.Save("Costume_" + costumeName + "_BrightnessColor", brightnessColor);
	}

	public void LoadColor()
	{
		isLoad = true;
		if (ES3.KeyExists("Costume_" + costumeName + "_HueColor"))
		{
			hueColor = ES3.Load<float>("Costume_" + costumeName + "_HueColor");
			brightnessColor = ES3.Load<float>("Costume_" + costumeName + "_BrightnessColor");
		}
		else
		{
			SaveColor();
		}
	}

	public void SaveColorData()
	{
		ES3.Save("Costume_" + costumeName + "_HueColorData", hueColorData);
		ES3.Save("Costume_" + costumeName + "_BrightnessColorData", brightnessColorData);
	}

	public void LoadColorData()
	{
		isLoadData = true;
		if (ES3.KeyExists("Costume_" + costumeName + "_HueColorData"))
		{
			hueColorData = ES3.Load<List<float>>("Costume_" + costumeName + "_HueColorData");
			brightnessColorData = ES3.Load<List<float>>("Costume_" + costumeName + "_BrightnessColorData");
		}
		else
		{
			SaveColorData();
		}
	}

	public void ChangeCostumeSet()
	{
		if (!isInitialize)
		{
			InitializeCostumeSet();
		}
		for (int i = 0; i < clotheModel.Count; i++)
		{
			equip[i] = true;
			unlock[i] = true;
			clotheModel[i].SetActive(value: true);
			if (_torsoManager != null)
			{
				if (clotheScript[i]._mesh != null)
				{
					_torsoManager.clotheMeshFilter[i].mesh = clotheScript[i].GetComponent<MeshFilter>().mesh;
				}
				if (clotheScript[i]._skinnedMesh != null)
				{
					_torsoManager.clotheMeshFilter[i].mesh = clotheScript[i]._skinnedMesh.sharedMesh;
				}
				bool skinnedMesh = true;
				if (clotheScript[i].unUseBodyBone)
				{
					skinnedMesh = false;
				}
				else if (clotheScript[i]._skinnedMesh == null)
				{
					skinnedMesh = false;
				}
				_torsoManager.SetClotheData(i, clotheScript[i].clotheName, skinnedMesh);
				_torsoManager.SetClotheLevel(i, clotheScript[i].clotheLevel);
				_torsoManager.SetDefaultClotheColor();
				_characterCostumeDamageManager.AllResetClothe();
				_torsoManager.SetClothe(i, set: true);
			}
		}
		_characterCostumeDamageManager.GetCounters();
		if (_torsoManager != null)
		{
			_torsoManager.ExcludeButton(clotheModel.Count);
		}
		if (_characterCostumeSwitch != null)
		{
			CheckWear();
		}
	}

	public void InitializeCostumeSet()
	{
		if (!isInitialize)
		{
			isInitialize = true;
			equip.Clear();
			unlock.Clear();
			clotheScript.Clear();
			_characterCostumeDamageManager._characterCostumeObject.Clear();
			for (int i = 0; i < clotheModel.Count; i++)
			{
				clotheScript.Add(clotheModel[i].GetComponent<CharacterCostumeObject>());
				clotheScript[i].clotheNum = i;
				equip.Add(item: true);
				unlock.Add(item: true);
				_characterCostumeDamageManager._characterCostumeObject.Add(clotheScript[i]);
			}
		}
	}

	public void DisableCostumeSet()
	{
		for (int i = 0; i < clotheModel.Count; i++)
		{
			clotheModel[i].SetActive(value: false);
		}
	}

	public void CheckMosaic()
	{
		isMosaic = true;
		for (int i = 0; i < equip.Count; i++)
		{
			if (clotheScript[i].mosaic && !equip[i])
			{
				isMosaic = false;
			}
		}
		if (isMosaic)
		{
			_characterVaginaManager.MosaicVaginaOn();
		}
		else
		{
			_characterVaginaManager.MosaicVaginaOff();
		}
	}

	public void SetClotheLevel(int value, int level)
	{
		_torsoManager.SetClotheLevel(value, level);
	}

	public void SetCostume(int value)
	{
		equip[value] = !equip[value];
		clotheModel[value].SetActive(equip[value]);
		if (!equip[value])
		{
			BreakCostume(value);
			_characterCostumeDamageManager.BreakClothe(value);
		}
		else
		{
			_audioSource.PlayOneShot(wareSe);
			_characterCostumeDamageManager.ResetClothe(value);
			if (OnomatopoeiaManager.instance.useOtomanopoeia)
			{
				if (clotheScript[value]._mesh != null)
				{
					Vector3[] vertices = clotheScript[value]._mesh.GetComponent<MeshFilter>().sharedMesh.vertices;
					Vector3 position = vertices[Random.Range(0, vertices.Length)];
					Vector3 position2 = clotheScript[value]._mesh.transform.TransformPoint(position);
					OnomatopoeiaManager.instance.SpawnOnomatopoeia(position2, null, "CostumeWear", Camera.main.transform);
				}
				if (clotheScript[value]._skinnedMesh != null)
				{
					Mesh mesh = new Mesh();
					clotheScript[value]._skinnedMesh.BakeMesh(mesh);
					Vector3[] vertices2 = mesh.vertices;
					Vector3 position3 = vertices2[Random.Range(0, vertices2.Length)];
					Vector3 position4 = clotheScript[value]._skinnedMesh.transform.TransformPoint(position3);
					OnomatopoeiaManager.instance.SpawnOnomatopoeia(position4, null, "CostumeWear", Camera.main.transform);
				}
			}
		}
		_torsoManager.SetClothe(value, equip[value]);
	}

	public void LockCostume(int value)
	{
		unlock[value] = !unlock[value];
		clotheScript[value]._paint3DManager.LockPaint(unlock[value]);
	}

	public void AllUnlockCostume()
	{
		for (int i = 0; i < unlock.Count; i++)
		{
			unlock[i] = true;
			clotheScript[i]._paint3DManager.LockPaint(value: true);
		}
	}

	public void SetCostumeMelt(int value)
	{
		equip[value] = !equip[value];
		clotheModel[value].SetActive(equip[value]);
		if (!equip[value])
		{
			MeltCostume(value);
			_characterCostumeDamageManager.BreakClothe(value);
		}
		else
		{
			_audioSource.PlayOneShot(wareSe);
			_characterCostumeDamageManager.ResetClothe(value);
		}
		_torsoManager.SetClothe(value, equip[value]);
		Debug.LogWarning("Break Clothe:" + value + "  bool: " + equip[value], base.gameObject);
	}

	public void MakeBreakParticle(CharacterCostumeObject script)
	{
		Debug.Log("Break Costume");
		ParticleSystem component = Object.Instantiate(breakCloseEffect, Vector3.zero, Quaternion.identity, effectStocker).GetComponent<ParticleSystem>();
		ParticleSystem.ShapeModule shape = component.shape;
		if (script._mesh != null)
		{
			shape.shapeType = ParticleSystemShapeType.MeshRenderer;
			shape.meshRenderer = script._mesh;
			clothMaterial = script._mesh.material;
			if (OnomatopoeiaManager.instance.useOtomanopoeia)
			{
				Vector3[] vertices = script._mesh.GetComponent<MeshFilter>().sharedMesh.vertices;
				Vector3 position = vertices[Random.Range(0, vertices.Length)];
				Vector3 position2 = script._mesh.transform.TransformPoint(position);
				OnomatopoeiaManager.instance.SpawnOnomatopoeia(position2, null, "CostumeBreak", Camera.main.transform);
			}
		}
		if (script._skinnedMesh != null)
		{
			shape.shapeType = ParticleSystemShapeType.SkinnedMeshRenderer;
			shape.skinnedMeshRenderer = script._skinnedMesh;
			clothMaterial = script._skinnedMesh.material;
			if (OnomatopoeiaManager.instance.useOtomanopoeia)
			{
				Mesh mesh = new Mesh();
				script._skinnedMesh.BakeMesh(mesh);
				Vector3[] vertices2 = mesh.vertices;
				Vector3 position3 = vertices2[Random.Range(0, vertices2.Length)];
				Vector3 position4 = script._skinnedMesh.transform.TransformPoint(position3);
				OnomatopoeiaManager.instance.SpawnOnomatopoeia(position4, null, "CostumeBreak", Camera.main.transform);
			}
		}
		component.GetComponent<ParticleSystemRenderer>().material = clothMaterial;
		if (script.tits)
		{
			_characterEffectManager.TitsSteam();
		}
		if (script.vagina)
		{
			_characterEffectManager.VaginaSteam();
		}
		if (script.anal)
		{
			_characterEffectManager.AnalSteam();
		}
		if (script.handL)
		{
			_characterEffectManager.HandLSteam();
		}
		if (script.handR)
		{
			_characterEffectManager.HandRSteam();
		}
		if (script.footL)
		{
			_characterEffectManager.FootLSteam();
		}
		if (script.footR)
		{
			_characterEffectManager.FootRSteam();
		}
	}

	public void MakeMeltParticle(CharacterCostumeObject script)
	{
		Debug.Log("Break Costume");
		ParticleSystem component = Object.Instantiate(meltCloseEffect, Vector3.zero, Quaternion.identity, effectStocker).GetComponent<ParticleSystem>();
		ParticleSystem.ShapeModule shape = component.shape;
		if (script._mesh != null)
		{
			shape.shapeType = ParticleSystemShapeType.MeshRenderer;
			shape.meshRenderer = script._mesh;
			clothMaterial = script._mesh.material;
			if (OnomatopoeiaManager.instance.useOtomanopoeia)
			{
				Vector3[] vertices = script._mesh.GetComponent<MeshFilter>().sharedMesh.vertices;
				Vector3 position = vertices[Random.Range(0, vertices.Length)];
				Vector3 position2 = script._mesh.transform.TransformPoint(position);
				OnomatopoeiaManager.instance.SpawnOnomatopoeia(position2, null, "CostumeMelt", Camera.main.transform);
			}
		}
		if (script._skinnedMesh != null)
		{
			shape.shapeType = ParticleSystemShapeType.SkinnedMeshRenderer;
			shape.skinnedMeshRenderer = script._skinnedMesh;
			clothMaterial = script._skinnedMesh.material;
			if (OnomatopoeiaManager.instance.useOtomanopoeia)
			{
				Mesh mesh = new Mesh();
				script._skinnedMesh.BakeMesh(mesh);
				Vector3[] vertices2 = mesh.vertices;
				Vector3 position3 = vertices2[Random.Range(0, vertices2.Length)];
				Vector3 position4 = script._skinnedMesh.transform.TransformPoint(position3);
				OnomatopoeiaManager.instance.SpawnOnomatopoeia(position4, null, "CostumeMelt", Camera.main.transform);
			}
		}
		component.GetComponent<ParticleSystemRenderer>().material = clothMaterial;
		if (script.tits)
		{
			_characterEffectManager.TitsSteam();
		}
		if (script.vagina)
		{
			_characterEffectManager.VaginaSteam();
		}
		if (script.anal)
		{
			_characterEffectManager.AnalSteam();
		}
		if (script.handL)
		{
			_characterEffectManager.HandLSteam();
		}
		if (script.handR)
		{
			_characterEffectManager.HandRSteam();
		}
		if (script.footL)
		{
			_characterEffectManager.FootLSteam();
		}
		if (script.footR)
		{
			_characterEffectManager.FootRSteam();
		}
	}

	public void BreakCostume(int value)
	{
		if (!equip[value])
		{
			MakeBreakParticle(clotheScript[value]);
			_audioSource.PlayOneShot(breakSe);
			_characterMouthManager.PlayHitSe();
		}
		CheckWear();
	}

	public void MeltCostume(int value)
	{
		if (!equip[value])
		{
			MakeMeltParticle(clotheScript[value]);
			_audioSource.PlayOneShot(meltSe);
			_characterMouthManager.PlayHitSe();
		}
		CheckWear();
	}

	public void CheckWear()
	{
		Debug.LogError("CheckWear");
		wearCount = 0;
		nakedCount = 0;
		for (int i = 0; i < equip.Count; i++)
		{
			wearCount++;
			if (!equip[i])
			{
				nakedCount++;
			}
		}
		_characterCostumeSwitch.wearCount = wearCount;
		_characterCostumeSwitch.nakedCount = nakedCount;
	}
}
