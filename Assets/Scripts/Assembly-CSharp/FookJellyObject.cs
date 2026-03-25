using System;
using System.Collections.Generic;
using UnityEngine;

public class FookJellyObject : MonoBehaviour
{
	public WorldLightManager _worldLightManager;

	public PoseDataGUI _poseDataGUI;

	public Transform baseObject;

	public Transform bodyBone;

	public Transform tailBone;

	[Header("Position")]
	public Vector3 defaultPosition;

	public Vector3 currentPosition;

	public float moveSize = 0.2f;

	public float moveSpeed = 0.1f;

	[Header("Rotation")]
	public float rotationSpeed = 50f;

	public Vector3 rotationRange = new Vector3(10f, 45f, 10f);

	private Vector3 currentRotation;

	private Quaternion defaultRotation;

	[Header("Scale")]
	public float minSize = 0.8f;

	public float maxSize = 1.2f;

	public float scaleSpeed = 1f;

	public float scaleTime;

	[Header("Material")]
	public SkinnedMeshRenderer fookJellyMesh;

	public Material fookJellyMaterial;

	[Header("Player Collision")]
	public Transform effectStocker;

	public GameObject electricEffect;

	public List<AudioClip> electricSe;

	[Header("Cool Time")]
	public float coolTime;

	public float coolTimeMax = 0.75f;

	[Header("Onomatopoeia")]
	public float onomatopoeiaTime;

	public float onomatopoeiaTimeMin = 10f;

	public float onomatopoeiaTimeMax = 30f;

	private void Awake()
	{
		defaultRotation = baseObject.localRotation;
		currentRotation = Vector3.zero;
		defaultPosition = baseObject.localPosition;
		fookJellyMaterial = fookJellyMesh.material;
		_worldLightManager.jellyFishMaterial.Add(fookJellyMaterial);
		onomatopoeiaTime = UnityEngine.Random.Range(onomatopoeiaTimeMin, onomatopoeiaTimeMax);
	}

	private void LateUpdate()
	{
		if (coolTime >= 0f)
		{
			coolTime -= Time.deltaTime;
		}
		currentRotation.x = rotationRange.x * Mathf.Sin(Time.time * rotationSpeed * (MathF.PI / 180f));
		currentRotation.y = Mathf.PerlinNoise(Time.time * 0.1f, 0f) * rotationRange.y * 2f - rotationRange.y;
		currentRotation.z = rotationRange.z * Mathf.Cos(Time.time * rotationSpeed * 1.2f * (MathF.PI / 180f));
		Quaternion localRotation = defaultRotation * Quaternion.Euler(currentRotation);
		baseObject.localRotation = localRotation;
		currentPosition.x = defaultPosition.x + Mathf.PerlinNoise(Time.time * moveSpeed, 0f) * moveSize * 2f - moveSize;
		currentPosition.y = defaultPosition.y + Mathf.PerlinNoise(0f, Time.time * moveSpeed) * moveSize * 2f - moveSize;
		currentPosition.z = defaultPosition.z + Mathf.PerlinNoise(Time.time * moveSpeed * 0.8f, Time.time * moveSpeed * 0.5f) * moveSize * 2f - moveSize;
		baseObject.localPosition = currentPosition;
		scaleTime += Time.deltaTime * scaleSpeed;
		float num = Mathf.Lerp(minSize, maxSize, (Mathf.Sin(scaleTime) + 1f) * 0.5f);
		bodyBone.localScale = new Vector3(num, 1f, num);
		Quaternion quaternion = Quaternion.FromToRotation(tailBone.up, Vector3.down);
		tailBone.rotation = quaternion * tailBone.rotation;
		if (OnomatopoeiaManager.instance.useOtomanopoeia)
		{
			onomatopoeiaTime -= Time.deltaTime;
			if (onomatopoeiaTime < 0f)
			{
				onomatopoeiaTime = UnityEngine.Random.Range(onomatopoeiaTimeMin, onomatopoeiaTimeMax);
				OnomatopoeiaManager.instance.SpawnOnomatopoeia(base.transform.position, base.transform, "Jelly", Camera.main.transform);
			}
		}
	}

	public void OnCollisionEnter(Collision collision)
	{
		if (coolTime < 0f && collision.gameObject.CompareTag("MainCamera"))
		{
			Debug.LogError("Collision Player Camera", base.gameObject);
			coolTime = coolTimeMax;
			EffectSeManager.instance.PlaySe(electricSe[UnityEngine.Random.Range(0, electricSe.Count)]);
			UnityEngine.Object.Instantiate(electricEffect, baseObject.position, Quaternion.identity, effectStocker);
			_poseDataGUI.SetRandomTarget();
			if (OnomatopoeiaManager.instance.useOtomanopoeia)
			{
				Vector3 position = Camera.main.transform.position + Camera.main.transform.forward * 0.1f;
				OnomatopoeiaManager.instance.SpawnOnomatopoeia(position, Camera.main.transform, "JellyFook", Camera.main.transform);
			}
		}
	}
}
