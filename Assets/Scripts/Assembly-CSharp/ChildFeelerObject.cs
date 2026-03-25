using Animancer;
using UnityEngine;

public class ChildFeelerObject : MonoBehaviour
{
	public AnimancerComponent _animancer;

	public Transform effectStocker;

	public Rigidbody _rigidbody;

	[Header("Status")]
	public bool isBounce;

	public bool isLand;

	public bool isMove;

	public bool isDig;

	public bool isRotateEnd;

	[Header("Setting")]
	public float waitTime = 2f;

	public float waitTimeMin = 1.5f;

	public float waitTimeMax = 3f;

	public float moveSpeed = 0.5f;

	public float moveSpeedMin = 0.3f;

	public float moveSpeedMax = 0.7f;

	public float moveDistance = 1f;

	public float moveDistanceMin = 0.7f;

	public float moveDistanceMax = 1.1f;

	public Vector3 dropPosition;

	public float moveAngle;

	[Header("Motion")]
	public AnimationClip standClip;

	public AnimationClip moveClip;

	public AnimationClip digClip;

	public AnimationClip curlClip;

	public AnimationClip growClip;

	public AnimationClip rollClip;

	[Header("Effect")]
	public GameObject landEffect;

	public GameObject digEffect;

	[Header("SE")]
	public AudioClip landSe;

	public AudioClip digSe;

	public AudioClip moveSe;

	private void Start()
	{
		_animancer.Play(curlClip, 0f);
		moveAngle = base.transform.localRotation.eulerAngles.y;
		waitTime = Random.Range(waitTimeMin, waitTimeMax);
		moveSpeed = Random.Range(moveSpeedMin, moveSpeedMax);
		moveDistance = Random.Range(moveDistanceMin, moveDistanceMax);
	}

	private void LateUpdate()
	{
		if (!isRotateEnd)
		{
			Vector3 eulerAngles = base.transform.rotation.eulerAngles;
			float num = Mathf.LerpAngle(eulerAngles.x, 0f, Time.deltaTime * 3f);
			float num2 = Mathf.LerpAngle(eulerAngles.z, 0f, Time.deltaTime * 3f);
			base.transform.rotation = Quaternion.Euler(num, moveAngle, num2);
			if (Mathf.Abs(num) < 0.1f && Mathf.Abs(num2) < 0.1f)
			{
				isRotateEnd = true;
			}
		}
		if (isLand && !isMove)
		{
			waitTime -= Time.deltaTime;
			if (waitTime < 0f)
			{
				isMove = true;
				_animancer.Play(moveClip, 0.1f);
				EffectSeManager.instance.PlaySe(moveSe);
			}
		}
		else if (isLand && isMove && !isDig)
		{
			if (Vector3.Distance(dropPosition, base.transform.position) < moveDistance)
			{
				base.transform.position += base.transform.forward * moveSpeed * Time.deltaTime;
				return;
			}
			isDig = true;
			_animancer.Play(digClip, 0.25f);
			EffectSeManager.instance.PlaySe(digSe);
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (!isBounce && collision.gameObject.layer == 20)
		{
			isBounce = true;
			Vector3 velocity = _rigidbody.velocity;
			Vector3 velocity2 = new Vector3(velocity.x, 0f, velocity.z) * 0.5f;
			Vector3 vector = new Vector3(Random.Range(-0.2f, 0.2f), 0f, Random.Range(-0.2f, 0.2f));
			velocity2 += vector;
			float num = Random.Range(0.2f, 0.4f);
			_rigidbody.velocity = velocity2;
			_rigidbody.AddForce(Vector3.up * num, ForceMode.Impulse);
			_animancer.Play(standClip, 0.25f);
			EffectSeManager.instance.PlaySe(landSe);
			if (OnomatopoeiaManager.instance.useOtomanopoeia)
			{
				OnomatopoeiaManager.instance.SpawnOnomatopoeia(base.transform.position, null, "ChildDrop", Camera.main.transform);
			}
		}
		else if (!isLand && collision.gameObject.layer == 20)
		{
			isLand = true;
			_rigidbody.isKinematic = true;
			dropPosition = base.transform.position;
			Object.Instantiate(landEffect, base.transform.position, base.transform.rotation, effectStocker);
		}
	}

	public void DigEffect()
	{
		Object.Instantiate(digEffect, base.transform.position, base.transform.rotation, effectStocker);
		if (OnomatopoeiaManager.instance.useOtomanopoeia)
		{
			OnomatopoeiaManager.instance.SpawnOnomatopoeia(base.transform.position, null, "ChildDig", Camera.main.transform);
		}
	}

	public void DigDestroy()
	{
		Object.Destroy(base.gameObject);
	}
}
