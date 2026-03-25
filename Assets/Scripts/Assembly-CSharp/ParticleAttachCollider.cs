using UnityEngine;

public class ParticleAttachCollider : MonoBehaviour
{
	public ParticleSystem _particle;

	public Rigidbody _rigidbody;

	public float gravity = 0.02f;

	private void LateUpdate()
	{
		if (_particle == null)
		{
			Object.Destroy(base.gameObject);
		}
	}

	private void OnCollisionStay(Collision collision)
	{
		if (collision.transform.tag != "Effect")
		{
			_rigidbody.useGravity = false;
			_rigidbody.velocity = Vector3.zero;
			base.transform.parent = collision.transform;
		}
	}

	private void OnCollisionExit(Collision collision)
	{
		if (collision.transform.tag != "Effect")
		{
			_rigidbody.useGravity = true;
			base.transform.parent = EffectStocker.instance.stocker;
		}
	}
}
