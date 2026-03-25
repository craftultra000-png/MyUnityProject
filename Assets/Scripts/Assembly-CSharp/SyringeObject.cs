using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyringeObject : MonoBehaviour
{
	public Transform bodyBone;

	public Transform edgeBone;

	public Transform attachObject;

	public Transform shotStocker;

	public Rigidbody _rigidBody;

	public CapsuleCollider _collider;

	[Header("Status")]
	public bool isMove;

	public bool isScale;

	public bool isDrop;

	public bool isBounce;

	[Header("Wait Time")]
	public float hitWait = 1f;

	public float destroyWait = 20f;

	[Header("Inject Time")]
	public float injectWaitTime;

	public float injectWaitTimeMax = 5f;

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

	public float paintPositionStart = -0.025f;

	public float paintPositionEnd = 0.01f;

	[Header("Effect")]
	public GameObject syringeEffect;

	public GameObject syringeSpwan;

	[Header("Se")]
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
					OnomatopoeiaManager.instance.SpawnOnomatopoeia(base.transform.position, null, "SyringeShot", Camera.main.transform);
				}
			}
		}
		else if (isScale)
		{
			scaleCurrent = Mathf.Lerp(scaleCurrent, scaleMin, scaleSpeed * Time.deltaTime);
			bodyBone.localScale = new Vector3(scaleCurrent, 1f, scaleCurrent);
			float t = Mathf.InverseLerp(1f, scaleMin, scaleCurrent);
			float y = Mathf.Lerp(paintPositionStart, paintPositionEnd, t);
			Vector3 localPosition = new Vector3(0f, y, 0f);
			paintBody.transform.localPosition = localPosition;
			paintCostume.transform.localPosition = localPosition;
			if (Mathf.Abs(scaleCurrent - scaleMin) < 0.01f)
			{
				bodyBone.localScale = new Vector3(scaleMin, 1f, scaleMin);
				isScale = false;
				isDrop = true;
				paintBody.SetActive(value: false);
				paintCostume.SetActive(value: false);
			}
		}
		else if (isDrop && injectWaitTime < injectWaitTimeMax)
		{
			injectWaitTime += Time.deltaTime;
			if (injectWaitTime > injectWaitTimeMax)
			{
				_rigidBody.isKinematic = false;
				_collider.enabled = true;
				base.transform.parent = shotStocker;
				Vector3 vector = -base.transform.forward;
				_rigidBody.AddForce(vector * removeForce, ForceMode.Impulse);
				StartCoroutine(DestroyWait());
			}
		}
	}

	private IEnumerator HitWait()
	{
		yield return new WaitForSeconds(hitWait);
		SetEffect();
		isScale = true;
		if (OnomatopoeiaManager.instance.useOtomanopoeia)
		{
			OnomatopoeiaManager.instance.SpawnOnomatopoeia(base.transform.position, base.transform, "SyringeHit", Camera.main.transform);
		}
	}

	private IEnumerator DestroyWait()
	{
		yield return new WaitForSeconds(destroyWait);
		Object.Destroy(base.gameObject);
	}

	private void OnDestroy()
	{
		if (syringeSpwan != null)
		{
			Object.Destroy(syringeSpwan);
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
		syringeSpwan = Object.Instantiate(syringeEffect, edgeBone.position, edgeBone.rotation, attachObject);
		EffectSeManager.instance.PlaySe(injectSe[Random.Range(0, injectSe.Count)]);
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
				OnomatopoeiaManager.instance.SpawnOnomatopoeia(base.transform.position, null, "SyringeDrop", Camera.main.transform);
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
