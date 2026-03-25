using UnityEngine;

namespace FIMSpace
{
	public class TailDemo_2DMover : MonoBehaviour
	{
		public float MovementSpeed = 4f;

		[Range(0f, 1f)]
		public float SmoothRotation;

		public float JumpPower = 12f;

		public bool DoubleJump = true;

		[Tooltip("Use keyboard keys movement implementation for quick debugging?")]
		public bool WSADMovement = true;

		[Range(0f, 0.5f)]
		[Tooltip("How slow accelerate/decelerate should be")]
		public float accelerationTime = 0.1f;

		protected float moveSpeed = 5f;

		protected float rotateSpeed = 10f;

		protected Vector3 smoothedAcceleration;

		private Vector3 moveDir = Vector3.zero;

		protected Quaternion targetRot;

		protected Vector3 veloHelper = Vector3.zero;

		protected bool isGrounded;

		protected float triggerJumping;

		private Rigidbody2D rigbody;

		private Collider2D charCollider;

		private int jumps;

		private void Start()
		{
			rigbody = GetComponentInChildren<Rigidbody2D>();
			charCollider = GetComponentInChildren<Collider2D>();
			targetRot = base.transform.rotation;
		}

		private void Update()
		{
			moveSpeed = MovementSpeed;
			moveDir = Vector3.zero;
			if (WSADMovement)
			{
				if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
				{
					moveDir = Vector3.right;
					targetRot = Quaternion.Euler(0f, 180f, 0f);
				}
				else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
				{
					moveDir = Vector3.right;
					targetRot = Quaternion.Euler(0f, 0f, 0f);
				}
				if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
				{
					moveSpeed *= 1.5f;
				}
				if (jumps < ((!DoubleJump) ? 1 : 2) && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space)))
				{
					triggerJumping = JumpPower;
				}
			}
			smoothedAcceleration = Vector3.SmoothDamp(smoothedAcceleration, moveDir, ref veloHelper, accelerationTime, float.PositiveInfinity, Time.deltaTime);
			if (SmoothRotation > 0f)
			{
				base.transform.rotation = Quaternion.Lerp(base.transform.rotation, targetRot, Time.deltaTime * (1.1f - SmoothRotation) * 60f);
			}
			else
			{
				base.transform.rotation = targetRot;
			}
		}

		protected virtual void FixedUpdate()
		{
			Vector3 vector = rigbody.velocity;
			Vector3 vector2 = base.transform.TransformVector(smoothedAcceleration) * moveSpeed;
			if (triggerJumping != 0f)
			{
				vector2.y = triggerJumping;
				rigbody.MovePosition(rigbody.position + Vector2.up * triggerJumping * 0.05f);
				triggerJumping = 0f;
				jumps++;
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

		protected virtual void OnGrounded()
		{
			isGrounded = true;
			jumps = 0;
		}
	}
}
