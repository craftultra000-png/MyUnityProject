using UnityEngine;

namespace FIMSpace.Basics
{
	public abstract class FBasics_Rigidbody2DMoverBase : MonoBehaviour
	{
		protected float moveSpeed = 5f;

		protected float rotateSpeed = 10f;

		protected Vector3 smoothedAcceleration;

		protected Quaternion smoothedRotation;

		protected Vector3 veloHelper = Vector3.zero;

		protected bool isGrounded;

		protected float triggerJumping;

		protected Rigidbody2D rigbody;

		protected Collider2D charCollider;

		protected virtual void Start()
		{
			rigbody = GetComponentInChildren<Rigidbody2D>();
			charCollider = GetComponentInChildren<Collider2D>();
			if ((bool)charCollider)
			{
				charCollider.sharedMaterial = FEngineering.PMFrict2D;
			}
			rigbody.interpolation = RigidbodyInterpolation2D.Interpolate;
			smoothedAcceleration = Vector3.zero;
			smoothedRotation = base.transform.rotation;
		}

		protected virtual void UpdateMotor()
		{
			Quaternion rotation = base.transform.rotation;
			if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
			{
				rotation = Quaternion.Euler(0f, 180f, 0f);
			}
			else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
			{
				rotation = Quaternion.Euler(0f, 0f, 0f);
			}
			if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space))
			{
				triggerJumping = 8f;
			}
			base.transform.rotation = rotation;
		}

		protected virtual void FixedUpdate()
		{
			Vector3 vector = rigbody.velocity;
			Vector3 vector2 = base.transform.TransformVector(smoothedAcceleration) * moveSpeed;
			if (triggerJumping != 0f)
			{
				vector2.y = triggerJumping;
				rigbody.MovePosition(rigbody.position + Vector2.up * triggerJumping * 0.05f);
				OnJump();
				triggerJumping = 0f;
				isGrounded = false;
			}
			else
			{
				vector2.y = vector.y;
			}
			if (!isGrounded || vector2.sqrMagnitude > moveSpeed * 0.2f)
			{
				charCollider.sharedMaterial = FEngineering.PMSliding2D;
			}
			else
			{
				charCollider.sharedMaterial = FEngineering.PMFrict2D;
			}
			rigbody.velocity = vector2;
		}

		protected virtual void OnCollisionEnter2D(Collision2D collision)
		{
			if (isGrounded || collision == null || collision.contacts.Length == 0)
			{
				return;
			}
			for (int i = 0; i < collision.contacts.Length; i++)
			{
				if (Vector2.Dot(rigbody.transform.up, collision.contacts[i].normal) > 0.25f)
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
