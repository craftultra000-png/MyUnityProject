using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnomatopoeiaSperm : MonoBehaviour
{
	[Header("Onomatopoeia Type")]
	public bool isCharacterOnly;

	public bool isStageObjectOnly;

	public string onomatopoeiaType = "SpermHit0";

	[Header("Target")]
	public Transform shotStocker;

	[Header("Self")]
	public Rigidbody _rigidBody;

	public float currentTime;

	public float shotSpeed = 3f;

	public float waitTime = 0.5f;

	private float spawnTime;

	public Vector3 hitPosition;

	[Header("Scale")]
	public float destroyScale = 0.5f;

	public Vector3 defaultScale;

	[Header("Collider")]
	public SphereCollider _collider;

	[Header("Destroy")]
	public bool isHit;

	public float destroyWait = 0.5f;

	public List<ParticleSystem> childEffect;

	private void Start()
	{
		currentTime = 0f;
		spawnTime = Time.time;
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
		if (Time.time - spawnTime < waitTime)
		{
			return;
		}
		if (collision.gameObject.CompareTag("Character") && !isStageObjectOnly)
		{
			isHit = true;
			OnomatopoeiaManager.instance.SpawnOnomatopoeia(base.transform.position, null, onomatopoeiaType, Camera.main.transform);
			StartCoroutine(DestroyWait());
			ChangeChildParent();
		}
		else if (collision.gameObject.CompareTag("StageObject"))
		{
			isHit = true;
			if (!isCharacterOnly)
			{
				OnomatopoeiaManager.instance.SpawnOnomatopoeia(base.transform.position, null, onomatopoeiaType, Camera.main.transform);
			}
			StartCoroutine(DestroyWait());
			ChangeChildParent();
		}
		else if (collision.gameObject.CompareTag("MainCamera"))
		{
			Rigidbody component = base.gameObject.GetComponent<Rigidbody>();
			Vector3 normalized = (base.transform.position - collision.transform.position).normalized;
			Vector3 vector = Random.insideUnitSphere * 0.2f;
			Vector3 normalized2 = (normalized + vector).normalized;
			normalized2.y = Mathf.Abs(normalized2.y) + 0.1f;
			component.velocity = normalized2 * Random.Range(0.2f, 0.4f);
		}
	}

	private IEnumerator DestroyWait()
	{
		yield return new WaitForSeconds(destroyWait);
		Object.Destroy(base.gameObject);
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
