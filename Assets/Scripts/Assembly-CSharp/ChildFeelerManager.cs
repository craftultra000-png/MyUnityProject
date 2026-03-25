using System.Collections.Generic;
using UnityEngine;

public class ChildFeelerManager : MonoBehaviour
{
	public UterusChildFeelerManager _uterusChildFeelerManager;

	public AnalChildFeelerManager _analChildFeelerManager;

	public CharacterLifeManager _characterLifeManager;

	public CharacterFaceManager _characterFaceManager;

	public CharacterMouthManager _characterMouthManager;

	public CharacterVaginaManager _characterVaginaManager;

	public CharacterAnalManager _characterAnalManager;

	[Header("Child")]
	public GameObject childFeeler;

	public GameObject currentChild;

	public Vector3 childScale = new Vector3(0.2f, 0.2f, 0.2f);

	[Header("Effect")]
	public GameObject globsEffect;

	[Header("CoolTime")]
	public float coolTimeVagina;

	public float coolTimeAnal;

	public float coolTimeMax = 1f;

	[Header("Spawn")]
	public Transform spawnVaginaPoint;

	public Transform spawnAnalPoint;

	public float spawnOffset = 0.2f;

	public float spawnForce = 0.5f;

	public float spawnForceMin = 0.3f;

	public float spawnForceMax = 0.7f;

	[Header("Collider")]
	public Collider childCollider;

	public List<Collider> vaginaCollider;

	[Header("SE")]
	public AudioClip birthSe;

	private void Start()
	{
	}

	private void LateUpdate()
	{
		if (coolTimeVagina > 0f)
		{
			coolTimeVagina -= Time.deltaTime;
		}
		if (coolTimeAnal > 0f)
		{
			coolTimeAnal -= Time.deltaTime;
		}
	}

	public void VaginaBirth()
	{
		if (coolTimeVagina <= 0f)
		{
			coolTimeVagina = coolTimeMax;
			_uterusChildFeelerManager.BirthFeeler(1);
			_uterusChildFeelerManager.BirthCoolTime(coolTimeMax);
		}
	}

	public void AnalBirth()
	{
		if (coolTimeAnal <= 0f)
		{
			coolTimeAnal = coolTimeMax;
			_analChildFeelerManager.BirthFeeler(1);
			_analChildFeelerManager.BirthCoolTime(coolTimeMax);
		}
	}

	public void BirthVaginaFeeler(float size)
	{
		_characterVaginaManager.SetPaint3D();
		SpawnVaginaFeeler(size);
		_characterMouthManager.PlayBirthSe();
		_characterLifeManager.HitData("Vagina", "Child");
	}

	public void BirthAnalFeeler(float size)
	{
		_characterAnalManager.SetPaint3D();
		SpawnAnalFeeler(size);
		_characterMouthManager.PlayBirthSe();
		_characterLifeManager.HitData("Anal", "Child");
	}

	public void SpawnVaginaFeeler(float size)
	{
		float y = Random.Range(0f, 360f);
		Quaternion quaternion = Quaternion.Euler(0f, y, 0f);
		Quaternion rotation = spawnVaginaPoint.rotation * quaternion;
		currentChild = Object.Instantiate(childFeeler, spawnVaginaPoint.position, rotation, base.transform);
		currentChild.transform.localScale = Vector3.one * size / 2f;
		Rigidbody component = currentChild.GetComponent<Rigidbody>();
		Vector3 vector = -spawnVaginaPoint.up;
		float num = 0.05f;
		Vector3 vector2 = new Vector3(Random.Range(0f - num, num), Random.Range(0f - num, num), Random.Range(0f - num, num));
		vector += vector2;
		vector.Normalize();
		component.AddForce(vector * spawnForce, ForceMode.Impulse);
		childCollider = currentChild.GetComponent<Collider>();
		for (int i = 0; i < vaginaCollider.Count; i++)
		{
			Physics.IgnoreCollision(childCollider, vaginaCollider[i], ignore: true);
		}
		Object.Instantiate(globsEffect, spawnVaginaPoint.position, rotation, base.transform);
		EffectSeManager.instance.PlaySe(birthSe);
		if (OnomatopoeiaManager.instance.useOtomanopoeia)
		{
			OnomatopoeiaManager.instance.SpawnOnomatopoeia(spawnVaginaPoint.position, null, "ChildSpawn", Camera.main.transform);
		}
	}

	public void SpawnAnalFeeler(float size)
	{
		float y = Random.Range(0f, 360f);
		Quaternion quaternion = Quaternion.Euler(0f, y, 0f);
		Quaternion rotation = spawnAnalPoint.rotation * quaternion;
		currentChild = Object.Instantiate(childFeeler, spawnAnalPoint.position, rotation, base.transform);
		currentChild.transform.localScale = Vector3.one * size / 2f;
		Rigidbody component = currentChild.GetComponent<Rigidbody>();
		Vector3 vector = -spawnAnalPoint.up;
		float num = 0.05f;
		Vector3 vector2 = new Vector3(Random.Range(0f - num, num), Random.Range(0f - num, num), Random.Range(0f - num, num));
		vector += vector2;
		vector.Normalize();
		component.AddForce(vector * spawnForce, ForceMode.Impulse);
		childCollider = currentChild.GetComponent<Collider>();
		for (int i = 0; i < vaginaCollider.Count; i++)
		{
			Physics.IgnoreCollision(childCollider, vaginaCollider[i], ignore: true);
		}
		Object.Instantiate(globsEffect, spawnAnalPoint.position, rotation, base.transform);
		EffectSeManager.instance.PlaySe(birthSe);
		if (OnomatopoeiaManager.instance.useOtomanopoeia)
		{
			OnomatopoeiaManager.instance.SpawnOnomatopoeia(spawnAnalPoint.position, null, "ChildSpawn", Camera.main.transform);
		}
	}
}
