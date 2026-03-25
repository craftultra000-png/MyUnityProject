using UnityEngine;

namespace FIMSpace.Basics
{
	public abstract class FBasics_RigidbodyMoverBase : MonoBehaviour
	{
		protected float moveSpeed = 5f;

		protected float rotateSpeed = 10f;

		protected Vector3 smoothedAcceleration;

		protected Quaternion smoothedRotation;

		protected Vector3 veloHelper = Vector3.zero;

		protected bool isGrounded;

		protected float triggerJumping;

		protected Rigidbody rigbody;

		protected Collider charCollider;

		protected virtual void Start()
		{
			rigbody = GetComponentInChildren<Rigidbody>();
			charCollider = GetComponentInChildren<Collider>();
			if ((bool)charCollider)
			{
				charCollider.material = FEngineering.PMFrict;
			}
			rigbody.interpolation = RigidbodyInterpolation.Interpolate;
			rigbody.maxAngularVelocity = 100f;
			smoothedAcceleration = Vector3.zero;
			smoothedRotation = base.transform.rotation;
		}

		protected virtual void UpdateMotor()
		{
			Vector3 forward = base.transform.forward;
			Vector3 eulerAngles = base.transform.eulerAngles;
			float smoothTime = 0.1f;
			float num = 10f;
			smoothedAcceleration = Vector3.SmoothDamp(smoothedAcceleration, forward, ref veloHelper, smoothTime, float.PositiveInfinity, Time.deltaTime);
			smoothedRotation = Quaternion.Lerp(rigbody.rotation, Quaternion.Euler(eulerAngles), Time.deltaTime * num);
		}

		protected virtual void FixedUpdate()
		{
			Vector3 velocity = rigbody.velocity;
			Vector3 velocity2 = base.transform.TransformVector(smoothedAcceleration) * moveSpeed;
			if (triggerJumping != 0f)
			{
				velocity2.y = triggerJumping;
				rigbody.MovePosition(rigbody.position + base.transform.up * triggerJumping * 0.05f);
				OnJump();
				triggerJumping = 0f;
				isGrounded = false;
			}
			else
			{
				velocity2.y = velocity.y;
			}
			if (!isGrounded || velocity2.sqrMagnitude > moveSpeed * 0.2f)
			{
				charCollider.material = FEngineering.PMSliding;
			}
			else
			{
				charCollider.material = FEngineering.PMFrict;
			}
			rigbody.velocity = velocity2;
			rigbody.angularVelocity = (smoothedRotation * Quaternion.Inverse(base.transform.rotation)).QToAngularVelocity() * rotateSpeed * 10f;
		}

		protected virtual void OnCollisionEnter(Collision collision)
		{
			if (isGrounded || collision == null || collision.contacts.Length == 0)
			{
				return;
			}
			for (int i = 0; i < collision.contacts.Length; i++)
			{
				if (Vector3.Dot(base.transform.up, collision.contacts[i].normal) > 0.25f)
				{
					OnGrounded();
					break;
				}
			}
		}

		protected virtual void OnJump()
		{
		}

		protected virtual void OnGrounded()
		{
			isGrounded = true;
		}
	}
}
