using System.Collections.Generic;
using UnityEngine;

public class CharacterModelManager : MonoBehaviour
{
	public CharacterSoundManager _characterSoundManager;

	[Header("Data")]
	public int characterType;

	public bool active;

	[Range(-100f, 100f)]
	public float titsSize;

	[Header("Set")]
	public bool setHabit;

	public bool setUnUniform;

	[Header("Status")]
	public bool isInitial;

	public bool costume;

	public bool brassiere;

	public bool shorts;

	[Header("Magica Tits")]
	[Header("Skinned Mesh")]
	private Mesh bodySkinnedMesh;

	private Mesh shortsSkinnedMesh;

	[Header("Material Head")]
	public SkinnedMeshRenderer headMesh;

	public Material headMaterial;

	public List<Texture> headTexList;

	public Material eyeblowMaterial;

	public List<Texture> eyeblowTexList;

	public List<Color> eyeblowColorList;

	public SkinnedMeshRenderer eyesMesh;

	public Material eyesMaterial;

	public List<Texture> eyesTexList;

	public List<Color> eyesColorList;

	[ColorUsage(false, true)]
	public List<Color> eyesEmitColorList;

	[Header("Material Body")]
	public SkinnedMeshRenderer bodyMesh;

	public Material bodyMaterial;

	public List<Texture> bodyTexList;

	public Texture bodyWetTex;

	[Header("Magica Costume")]
	public List<GameObject> _magicaCostume;

	[Header("Material Costume")]
	public int costumeType;

	public List<SkinnedMeshRenderer> costumeMesh;

	public Material costumeMaterial;

	public List<Texture> costumeTexList;

	public List<Texture> costumeNormalList;

	[Header("Material UnderWare")]
	public int UnderWareTex;

	public int brassiereType;

	public int shortsType;

	public List<SkinnedMeshRenderer> brassiereMesh;

	public List<SkinnedMeshRenderer> shortsMesh;

	public Material underWareMaterial;

	public List<Texture> underWareTexList;

	public List<Texture> underWareNormalList;

	public List<Texture> underWareTexListStella;

	public List<Texture> underWareTexListVacua;

	[Header("Break Effect")]
	public GameObject BreakClotheEffect;

	public Transform effectStocker;

	private void Awake()
	{
		bodySkinnedMesh = bodyMesh.sharedMesh;
	}

	private void Start()
	{
	}

	private void LateUpdate()
	{
		for (int i = 0; i < bodySkinnedMesh.blendShapeCount; i++)
		{
			if (bodyMesh.GetBlendShapeWeight(i) > 100f)
			{
				bodyMesh.SetBlendShapeWeight(i, 100f);
			}
			else if (bodyMesh.GetBlendShapeWeight(i) < -100f)
			{
				bodyMesh.SetBlendShapeWeight(i, -100f);
			}
			shortsMesh[shortsType].SetBlendShapeWeight(i, bodyMesh.GetBlendShapeWeight(i));
		}
	}

	private void OnDestroy()
	{
		if (headMaterial != null)
		{
			Object.Destroy(headMaterial);
			headMaterial = null;
		}
		if (bodyMaterial != null)
		{
			Object.Destroy(bodyMaterial);
			bodyMaterial = null;
		}
		if (underWareMaterial != null)
		{
			Object.Destroy(underWareMaterial);
			underWareMaterial = null;
		}
	}

	public void ClotheSetUp()
	{
		costume = true;
		brassiere = true;
		shorts = true;
		BrassiereSet(0);
		ShortsSet(0);
		isInitial = true;
	}

	public void CostumeSet(int value)
	{
		if (value == -1)
		{
			costume = !costume;
		}
		if (value >= 0)
		{
			costumeType = value;
		}
		if (isInitial)
		{
			_characterSoundManager.TakeOffSe();
		}
		for (int i = 0; i < costumeMesh.Count; i++)
		{
			costumeMesh[i].gameObject.SetActive(value: false);
			_magicaCostume[i].gameObject.SetActive(value: false);
		}
		if (costume)
		{
			if (value >= 4)
			{
				costumeMesh[3].gameObject.SetActive(value: true);
				_magicaCostume[3].gameObject.SetActive(value: true);
			}
			else
			{
				costumeMesh[costumeType].gameObject.SetActive(value: true);
				_magicaCostume[costumeType].gameObject.SetActive(value: true);
			}
		}
		else
		{
			_ = brassiere;
		}
	}

	public void BrassiereSet(int value)
	{
		if (value == -1)
		{
			brassiere = !brassiere;
		}
		if (value >= 0)
		{
			brassiereType = value;
		}
		if (isInitial)
		{
			_characterSoundManager.TakeOffSe();
		}
		for (int i = 0; i < brassiereMesh.Count; i++)
		{
			brassiereMesh[i].gameObject.SetActive(value: false);
		}
		if (brassiere)
		{
			brassiereMesh[brassiereType].gameObject.SetActive(value: true);
		}
		else
		{
			_ = costume;
		}
	}

	public void ShortsSet(int value)
	{
		if (value == -1)
		{
			shorts = !shorts;
		}
		if (value >= 0)
		{
			shortsType = value;
		}
		if (isInitial)
		{
			_characterSoundManager.TakeOffSe();
		}
		for (int i = 0; i < shortsMesh.Count; i++)
		{
			shortsMesh[i].gameObject.SetActive(value: false);
		}
		if (shorts)
		{
			shortsMesh[shortsType].gameObject.SetActive(value: true);
		}
	}

	public void TitsSet()
	{
		if (titsSize > 0f)
		{
			bodyMesh.SetBlendShapeWeight(0, titsSize);
			bodyMesh.SetBlendShapeWeight(1, 0f);
			for (int i = 0; i < costumeMesh.Count; i++)
			{
				costumeMesh[i].SetBlendShapeWeight(0, titsSize);
				costumeMesh[i].SetBlendShapeWeight(1, 0f);
			}
			for (int j = 0; j < brassiereMesh.Count; j++)
			{
				brassiereMesh[j].SetBlendShapeWeight(0, titsSize);
				brassiereMesh[j].SetBlendShapeWeight(1, 0f);
			}
		}
		else if (titsSize < 0f)
		{
			bodyMesh.SetBlendShapeWeight(0, 0f);
			bodyMesh.SetBlendShapeWeight(1, 0f - titsSize);
			for (int k = 0; k < costumeMesh.Count; k++)
			{
				costumeMesh[k].SetBlendShapeWeight(0, 0f);
				costumeMesh[k].SetBlendShapeWeight(1, 0f - titsSize);
			}
			for (int l = 0; l < brassiereMesh.Count; l++)
			{
				brassiereMesh[l].SetBlendShapeWeight(0, 0f);
				brassiereMesh[l].SetBlendShapeWeight(1, 0f - titsSize);
			}
		}
	}

	public void MaterialSet()
	{
		List<float> list = new List<float> { 0f, -100f, 100f };
		headMesh.SetBlendShapeWeight(0, list[characterType]);
		list = new List<float> { 0f, 100f, 50f };
		headMesh.SetBlendShapeWeight(1, list[characterType]);
		list = new List<float> { 0f, 0f, 100f };
		headMesh.SetBlendShapeWeight(21, list[characterType]);
		list = new List<float> { 0f, 100f, 0f };
		headMesh.SetBlendShapeWeight(22, list[characterType]);
		if (headMaterial == null)
		{
			headMaterial = headMesh.materials[0];
		}
		headMaterial.SetTexture("_MainTex", headTexList[characterType]);
		headMesh.materials[0] = headMaterial;
		if (eyeblowMaterial == null)
		{
			eyeblowMaterial = headMesh.materials[1];
		}
		eyeblowMaterial.SetTexture("_MainTex", eyeblowTexList[characterType]);
		eyeblowMaterial.SetColor("_BaseColor", eyeblowColorList[characterType]);
		headMesh.materials[1] = eyeblowMaterial;
		if (eyesMaterial == null)
		{
			eyesMaterial = eyesMesh.material;
		}
		eyesMaterial.SetTexture("_MainTex", eyesTexList[characterType]);
		eyesMaterial.SetColor("_BaseColor", eyesColorList[characterType]);
		eyesMaterial.SetColor("_Emissive_Color", eyesEmitColorList[characterType]);
		eyesMesh.material = eyesMaterial;
		if (bodyMaterial == null)
		{
			bodyMaterial = bodyMesh.material;
		}
		bodyMaterial.SetTexture("_MainTex", bodyTexList[characterType]);
		bodyMesh.material = bodyMaterial;
		if (costumeMaterial == null)
		{
			costumeMaterial = costumeMesh[characterType].material;
		}
		costumeMaterial.SetTexture("_MainTex", costumeTexList[characterType]);
		costumeMaterial.SetTexture("_NormalMap", costumeNormalList[characterType]);
		costumeMesh[characterType].material = costumeMaterial;
		if (underWareMaterial == null)
		{
			underWareMaterial = brassiereMesh[0].material;
		}
		if (characterType == 0)
		{
			UnderWareTex = 0;
			underWareMaterial.SetTexture("_MainTex", underWareTexListStella[0]);
			underWareMaterial.SetTexture("_NormalMap", underWareNormalList[0]);
		}
		else if (characterType == 1)
		{
			UnderWareTex = 1;
			underWareMaterial.SetTexture("_MainTex", underWareTexListVacua[1]);
			underWareMaterial.SetTexture("_NormalMap", underWareNormalList[1]);
		}
		else if (characterType == 2)
		{
			underWareMaterial.SetTexture("_MainTex", underWareTexList[2]);
			underWareMaterial.SetTexture("_NormalMap", underWareNormalList[1]);
		}
		brassiereMesh[0].material = underWareMaterial;
		shortsMesh[0].material = underWareMaterial;
	}

	public void WetBody(bool value)
	{
		if (bodyMaterial == null)
		{
			bodyMaterial = bodyMesh.material;
		}
		bodyMaterial.SetFloat("_MatCap", 1f);
		bodyMaterial.SetTexture("_MatCap_Sampler", bodyWetTex);
		bodyMesh.material = bodyMaterial;
	}

	public void HairSet()
	{
	}

	public void AccessorySetUp()
	{
	}

	public void DreamCatcherOff()
	{
	}

	public void CameraSet(bool value)
	{
		active = value;
		headMesh.gameObject.SetActive(!active);
		eyesMesh.gameObject.SetActive(!active);
	}

	public void MagicaChange(string value)
	{
	}

	public void BreakCostume()
	{
		if (costume)
		{
			CostumeSet(-1);
			Debug.LogWarning("Break Costume");
			ParticleSystem component = Object.Instantiate(BreakClotheEffect, Vector3.zero, Quaternion.identity, effectStocker).GetComponent<ParticleSystem>();
			ParticleSystem.ShapeModule shape = component.shape;
			shape.shapeType = ParticleSystemShapeType.SkinnedMeshRenderer;
			shape.skinnedMeshRenderer = costumeMesh[costumeType];
			Material material = component.GetComponent<ParticleSystemRenderer>().material;
			material.SetTexture("_MainTex", costumeTexList[costumeType]);
			material.SetTexture("_NormalMap", costumeNormalList[costumeType]);
		}
	}

	public void BreakBrassiere()
	{
		if (brassiere)
		{
			BrassiereSet(-1);
			Debug.LogWarning("Break Brassiere");
			ParticleSystem component = Object.Instantiate(BreakClotheEffect, Vector3.zero, Quaternion.identity, effectStocker).GetComponent<ParticleSystem>();
			ParticleSystem.ShapeModule shape = component.shape;
			shape.shapeType = ParticleSystemShapeType.SkinnedMeshRenderer;
			shape.skinnedMeshRenderer = brassiereMesh[brassiereType];
			_ = component.GetComponent<ParticleSystemRenderer>().material;
			if (characterType == 0)
			{
				underWareMaterial.SetTexture("_MainTex", underWareTexListStella[UnderWareTex]);
				underWareMaterial.SetTexture("_NormalMap", underWareNormalList[UnderWareTex]);
			}
			else if (characterType == 1)
			{
				underWareMaterial.SetTexture("_MainTex", underWareTexListVacua[UnderWareTex]);
				underWareMaterial.SetTexture("_NormalMap", underWareNormalList[UnderWareTex]);
			}
			else if (characterType == 2)
			{
				underWareMaterial.SetTexture("_MainTex", underWareTexList[2]);
				underWareMaterial.SetTexture("_NormalMap", underWareNormalList[1]);
			}
		}
	}

	public void BreakShorts()
	{
		if (shorts)
		{
			ShortsSet(-1);
			Debug.LogWarning("Break Shorts");
			ParticleSystem component = Object.Instantiate(BreakClotheEffect, Vector3.zero, Quaternion.identity, effectStocker).GetComponent<ParticleSystem>();
			ParticleSystem.ShapeModule shape = component.shape;
			shape.shapeType = ParticleSystemShapeType.SkinnedMeshRenderer;
			shape.skinnedMeshRenderer = shortsMesh[shortsType];
			_ = component.GetComponent<ParticleSystemRenderer>().material;
			if (characterType == 0)
			{
				underWareMaterial.SetTexture("_MainTex", underWareTexListStella[UnderWareTex]);
				underWareMaterial.SetTexture("_NormalMap", underWareNormalList[UnderWareTex]);
			}
			else if (characterType == 1)
			{
				underWareMaterial.SetTexture("_MainTex", underWareTexListVacua[UnderWareTex]);
				underWareMaterial.SetTexture("_NormalMap", underWareNormalList[UnderWareTex]);
			}
			else if (characterType == 2)
			{
				underWareMaterial.SetTexture("_MainTex", underWareTexList[2]);
				underWareMaterial.SetTexture("_NormalMap", underWareNormalList[1]);
			}
		}
	}

	public void ResetClothe()
	{
		if (!setUnUniform)
		{
			CostumeSet(-1);
		}
		if (!brassiere)
		{
			BrassiereSet(-1);
		}
		if (!shorts)
		{
			ShortsSet(-1);
		}
	}
}
