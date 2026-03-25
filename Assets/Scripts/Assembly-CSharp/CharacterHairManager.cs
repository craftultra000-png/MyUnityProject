using System.Collections.Generic;
using UnityEngine;

public class CharacterHairManager : MonoBehaviour
{
	public SideCharacterManger _sideCharacterManger;

	[Header("Hair")]
	public int hairType;

	public List<GameObject> hairObject;

	public List<GameObject> hairMagica;

	[Header("Hair Mesh")]
	public List<Mesh> hairFront;

	public List<Mesh> hairBack;

	[Space]
	public List<MeshFilter> torsoHairFront;

	public List<MeshFilter> torsoHairBack;

	[Header("Texture")]
	public int characterType = 1;

	public List<Texture> hairTexture;

	public List<Color> hairColor;

	public Material hairMaterial;

	[Header("Stauts")]
	public bool isEat;

	[Header("Position")]
	public Vector3 currentPosition;

	public Vector3 calcPosition;

	public Vector3 defaultPosition = new Vector3(0f, -1.282051f, 0f);

	public Transform eatPosition;

	public float lerpSpeedUp = 2f;

	public float lerpSpeedDown = 10f;

	[Header("Effect")]
	public Transform effectHeadPosition;

	public GameObject hairChangeEfefct;

	public List<AudioClip> costumeChangeSe;

	private void Start()
	{
		currentPosition = base.transform.localPosition;
		_sideCharacterManger.SetHair(hairType);
	}

	private void Update()
	{
	}

	private void LateUpdate()
	{
		if (isEat)
		{
			Quaternion rotation = eatPosition.rotation;
			Quaternion b = Quaternion.Inverse(base.transform.parent.rotation) * rotation;
			base.transform.localRotation = Quaternion.Lerp(base.transform.localRotation, b, Time.deltaTime * lerpSpeedUp);
			Vector3 position = eatPosition.position - eatPosition.rotation * defaultPosition;
			Vector3 b2 = base.transform.parent.InverseTransformPoint(position);
			base.transform.localPosition = Vector3.Lerp(base.transform.localPosition, b2, Time.deltaTime * lerpSpeedUp);
		}
		else
		{
			base.transform.localPosition = Vector3.Lerp(base.transform.localPosition, defaultPosition, Time.deltaTime * lerpSpeedDown);
			base.transform.localRotation = Quaternion.Lerp(base.transform.localRotation, Quaternion.identity, Time.deltaTime * lerpSpeedDown);
		}
	}

	public void ChangeHair(int value)
	{
		Debug.LogError("Change Hair: " + hairType, base.gameObject);
		hairType = value;
		SetHairType(hairType);
		EffectSeManager.instance.PlaySe(costumeChangeSe[Random.Range(0, costumeChangeSe.Count)]);
		Object.Instantiate(hairChangeEfefct, effectHeadPosition.position, effectHeadPosition.rotation, base.transform);
	}

	public void SetHairType(int value)
	{
		hairType = value;
		for (int i = 0; i < hairObject.Count; i++)
		{
			hairObject[i].SetActive(value: false);
			hairMagica[i].SetActive(value: false);
		}
		hairObject[hairType].SetActive(value: true);
		hairMagica[hairType].SetActive(value: true);
		torsoHairFront[0].mesh = hairFront[hairType];
		torsoHairFront[1].mesh = hairFront[hairType];
		torsoHairBack[0].mesh = hairBack[hairType];
		torsoHairBack[1].mesh = hairBack[hairType];
	}
}
