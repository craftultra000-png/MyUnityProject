using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotObject : MonoBehaviour
{
	[Header("Attack Type")]
	public string attackType = "Bukkake";

	public string onomatopoeiaType = "SpermHit0";

	[Header("Target")]
	public GameObject hitEffect;

	public GameObject missEffect;

	public GameObject onomatopoeiaSperm;

	public Transform shotStocker;

	public Vector3 hitPosition;

	[Header("Self")]
	public Rigidbody _rigidBody;

	public SphereCollider _sphereCollider;

	public GameObject meshObject;

	public float currentTime;

	public float gravityWait = 0.5f;

	public float shotSpeed = 3f;

	[Header("Scale")]
	public float destroyScale = 0.5f;

	public Vector3 defaultScale;

	[Header("Destroy")]
	public bool isHit;

	public float destroyWait = 0.2f;

	public List<ParticleSystem> childEffect;

	private void Start()
	{
		_rigidBody.useGravity = false;
		currentTime = 0f;
		defaultScale = base.transform.localScale;
		destroyScale = destroyWait;
	}

	private void LateUpdate()
	{
		Vector3 velocity = _rigidBody.velocity;
		if (velocity.magnitude > shotSpeed)
		{
			_rigidBody.velocity = velocity.normalized * shotSpeed;
		}
		if (currentTime < gravityWait)
		{
			currentTime += Time.deltaTime;
			if (currentTime > gravityWait)
			{
				_rigidBody.useGravity = true;
			}
		}
		if (isHit)
		{
			destroyScale -= Time.deltaTime;
			base.transform.localScale = defaultScale * (destroyScale / destroyWait);
		}
	}

	private void OnDisable()
	{
		Object.Destroy(base.gameObject);
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (!isHit)
		{
			Hit(collision);
		}
	}

	private void OnCollisionStay(Collision collision)
	{
		if (!isHit)
		{
			Hit(collision);
		}
	}

	private void Hit(Collision collision)
	{
		if (collision.gameObject.CompareTag("Character") || collision.gameObject.CompareTag("StageObject"))
		{
			isHit = true;
			ContactPoint[] contacts = collision.contacts;
			foreach (ContactPoint contactPoint in contacts)
			{
				hitPosition = contactPoint.point;
			}
			base.transform.parent = collision.transform;
			base.transform.position = hitPosition;
			_rigidBody.velocity = Vector3.zero;
			_rigidBody.angularVelocity = Vector3.zero;
			_rigidBody.isKinematic = true;
			_rigidBody.useGravity = false;
			_sphereCollider.enabled = false;
			meshObject.SetActive(value: false);
			if (collision.gameObject.CompareTag("Character"))
			{
				HitSpawnEffect(collision.transform);
			}
			else
			{
				MissSpawnEffect(collision.transform);
			}
			ChangeChildParent();
			if ((bool)collision.gameObject.GetComponent<CharacterColliderObject>())
			{
				collision.gameObject.GetComponent<CharacterColliderObject>().HitData(attackType);
			}
			StartCoroutine(DestroyWait());
		}
		else if (!collision.gameObject.CompareTag("Effect"))
		{
			isHit = true;
			StartCoroutine(DestroyWait());
			ChangeChildParent();
		}
	}

	private IEnumerator DestroyWait()
	{
		yield return new WaitForSeconds(destroyWait);
		Object.Destroy(base.gameObject);
	}

	public void HitSpawnEffect(Transform obj)
	{
		GameObject gameObject = Object.Instantiate(hitEffect, hitPosition, base.transform.rotation, obj.transform);
		if (OnomatopoeiaManager.instance.useOtomanopoeia)
		{
			SpermOnomatopeObject component = gameObject.GetComponent<SpermOnomatopeObject>();
			component.useOnomatopoeia = true;
			component.shotStocker = shotStocker;
			OnomatopoeiaManager.instance.SpawnOnomatopoeia(hitPosition, null, onomatopoeiaType, Camera.main.transform);
			GameObject obj2 = Object.Instantiate(onomatopoeiaSperm, hitPosition, Quaternion.identity, shotStocker);
			Rigidbody component2 = obj2.GetComponent<Rigidbody>();
			Vector3 onUnitSphere = Random.onUnitSphere;
			component2.velocity = onUnitSphere * Random.Range(0.5f, 1f);
			obj2.GetComponent<OnomatopoeiaSperm>().shotStocker = shotStocker;
		}
	}

	public void MissSpawnEffect(Transform obj)
	{
		Object.Instantiate(missEffect, hitPosition, base.transform.rotation, obj.transform);
		if (OnomatopoeiaManager.instance.useOtomanopoeia)
		{
			OnomatopoeiaManager.instance.SpawnOnomatopoeia(hitPosition, null, onomatopoeiaType, Camera.main.transform);
		}
	}

	public void ChangeChildParent()
	{
		for (int i = 0; i < childEffect.Count; i++)
		{
			ParticleSystem.MainModule main = childEffect[i].main;
			main.loop = false;
			main.stopAction = ParticleSystemStopAction.Destroy;
			ParticleSystem.EmissionModule emission = childEffect[i].emission;
			emission.enabled = false;
			childEffect[i].transform.SetParent(shotStocker);
		}
	}
}
