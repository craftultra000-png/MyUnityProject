using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuckObject : MonoBehaviour
{
	public Animator _animator;

	public Transform suckBone;

	public Transform attachObject;

	public Transform shotStocker;

	public Rigidbody _rigidBody;

	public CapsuleCollider _collider;

	[Header("Attack Type")]
	public string attackType = "Suck";

	public CharacterColliderObject damageCollider;

	[Header("Status")]
	public bool isMove;

	public bool isScale;

	public bool isSuck;

	public bool isDrop;

	public bool isBounce;

	public bool isDestroy;

	[Header("Wait Time")]
	public float hitWait = 0.5f;

	public float destroyWait = 20f;

	public float destroyScale = 1f;

	[Header("Suck Time")]
	public float suckWaitTime;

	public float suckWaitTimeMax = 5f;

	[Header("Scale")]
	public float scaleCurrent = 1f;

	public float scaleMin = 0.1f;

	public float scaleSpeed = 1f;

	[Header("Move")]
	public float moveSpeed = 5f;

	public float moveThreshold = 0.1f;

	[Header("Effect")]
	public float dropLerpSpeed = 2f;

	public float removeForce = 0.1f;

	public float bounceForce = 0.2f;

	[Header("Paint Move")]
	public GameObject paintBody;

	public GameObject paintCostume;

	public float paintPositionStart = 0.05f;

	public float paintPositionEnd;

	[Header("Effect")]
	public GameObject suckEffect;

	public GameObject suckSpwan;

	[Header("Se")]
	public List<AudioClip> suckSe;

	public List<AudioClip> injectSe;

	public List<AudioClip> dropSe;

	private void Start()
	{
		_collider.enabled = false;
	}

	private void FixedUpdate()
	{
		if (isMove)
		{
			base.transform.position = Vector3.MoveTowards(base.transform.position, attachObject.position, moveSpeed * Time.deltaTime);
			if (Vector3.Distance(base.transform.position, attachObject.position) <= moveThreshold)
			{
				base.transform.position = attachObject.position;
				base.transform.parent = attachObject;
				isMove = false;
				StartCoroutine(HitWait());
				if (OnomatopoeiaManager.instance.useOtomanopoeia)
				{
					OnomatopoeiaManager.instance.SpawnOnomatopoeia(base.transform.position, null, "SuckFit", Camera.main.transform);
				}
			}
		}
		else if (isSuck)
		{
			if (suckWaitTime < suckWaitTimeMax)
			{
				suckWaitTime += Time.deltaTime;
				if (suckWaitTime > suckWaitTimeMax)
				{
					isSuck = false;
					isDrop = true;
					_animator.SetBool("isDrop", value: true);
					suckWaitTime = 2f;
				}
			}
		}
		else if (isDrop && suckWaitTime > 0f)
		{
			suckWaitTime -= Time.deltaTime;
			if (suckWaitTime < 0f)
			{
				_rigidBody.isKinematic = false;
				_collider.enabled = true;
				base.transform.parent = shotStocker;
				Vector3 vector = -base.transform.forward;
				_rigidBody.AddForce(vector * removeForce, ForceMode.Impulse);
				StartCoroutine(DestroyWait());
			}
		}
		if (isScale)
		{
			scaleCurrent = Mathf.Lerp(scaleCurrent, scaleMin, scaleSpeed * Time.deltaTime);
			float t = Mathf.InverseLerp(1f, scaleMin, scaleCurrent);
			float y = Mathf.Lerp(paintPositionStart, paintPositionEnd, t);
			Vector3 localPosition = new Vector3(0f, y, 0f);
			paintBody.transform.localPosition = localPosition;
			if (Mathf.Abs(scaleCurrent - scaleMin) < 0.01f)
			{
				isScale = false;
				paintBody.SetActive(value: false);
			}
		}
		if (isDestroy)
		{
			destroyScale -= Time.deltaTime;
			if (destroyScale < 0f)
			{
				destroyScale = 0f;
			}
			base.transform.localScale = Vector3.one * destroyScale;
		}
	}

	private IEnumerator HitWait()
	{
		yield return new WaitForSeconds(hitWait);
		SetEffect();
		isScale = true;
		isSuck = true;
		_animator.SetBool("isSuck", value: true);
	}

	private IEnumerator DestroyWait()
	{
		yield return new WaitForSeconds(destroyWait);
		isDestroy = true;
		yield return new WaitForSeconds(1f);
		Object.Destroy(base.gameObject);
	}

	private void OnDestroy()
	{
		if (suckSpwan != null)
		{
			Object.Destroy(suckSpwan);
		}
		if (attachObject != null)
		{
			Object.Destroy(attachObject.gameObject);
		}
	}

	public void AttachBody(Transform position, Transform target)
	{
		attachObject.parent = target;
		attachObject.position = position.position;
		isMove = true;
	}

	public void SetEffect()
	{
		suckSpwan = Object.Instantiate(suckEffect, suckBone.position, suckBone.rotation, attachObject);
		EffectSeManager.instance.PlaySe(suckSe[Random.Range(0, suckSe.Count)]);
		if (OnomatopoeiaManager.instance.useOtomanopoeia)
		{
			OnomatopoeiaManager.instance.SpawnOnomatopoeia(base.transform.position, base.transform, "Suck", Camera.main.transform);
		}
		damageCollider.HitData(attackType);
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (!isBounce && collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
		{
			isBounce = true;
			_rigidBody.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
			EffectSeManager.instance.PlaySe(dropSe[Random.Range(0, dropSe.Count)]);
			if (OnomatopoeiaManager.instance.useOtomanopoeia)
			{
				OnomatopoeiaManager.instance.SpawnOnomatopoeia(base.transform.position, null, "Drip", Camera.main.transform);
			}
		}
		if (collision.gameObject.layer == LayerMask.NameToLayer("Character"))
		{
			Vector3 up = Vector3.up;
			up = (Vector3.up + new Vector3(Random.Range(-0.5f, 0.5f), 0f, Random.Range(-0.5f, 0.5f))).normalized * removeForce;
			_rigidBody.AddForce(up, ForceMode.Impulse);
		}
	}
}
