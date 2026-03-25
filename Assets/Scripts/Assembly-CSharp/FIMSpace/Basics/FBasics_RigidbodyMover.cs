using UnityEngine;

namespace FIMSpace.Basics
{
	public class FBasics_RigidbodyMover : FBasics_RigidbodyMoverBase
	{
		public float MovementSpeed = 4f;

		public float RotationSpeed = 10f;

		public float JumpPower = 7f;

		[Tooltip("Use keyboard keys movement implementation for quick debugging?")]
		public bool WSADMovement = true;

		[Range(0f, 0.5f)]
		[Tooltip("How slow accelerate/decelerate should be")]
		public float accelerationTime = 0.1f;

		[Tooltip("Always rotate head towards movement direction")]
		public bool RotateInDir = true;

		private float offsetRotY;

		private Vector3 moveDir = Vector3.zero;

		private Vector3 targetRot;

		protected override void Start()
		{
			base.Start();
			targetRot = base.transform.rotation.eulerAngles;
		}

		protected virtual void Update()
		{
			UpdateMotor();
		}

		protected override void UpdateMotor()
		{
			moveSpeed = MovementSpeed;
			moveDir = Vector3.zero;
			offsetRotY = 0f;
			if (WSADMovement)
			{
				if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
				{
					moveDir += Vector3.forward;
				}
				else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
				{
					moveDir += Vector3.back;
				}
				if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
				{
					moveDir += Vector3.left;
				}
				else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
				{
					moveDir += Vector3.right;
				}
				if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
				{
					moveSpeed *= 1.5f;
				}
				if (isGrounded && Input.GetKeyDown(KeyCode.Space))
				{
					triggerJumping = JumpPower;
				}
			}
			if (moveDir != Vector3.zero)
			{
				moveDir.Normalize();
				if (RotateInDir)
				{
					targetRot = Quaternion.LookRotation(Vector3.ProjectOnPlane(Camera.main.transform.TransformDirection(moveDir), Vector3.up)).eulerAngles;
					moveDir = Vector3.forward;
				}
				else
				{
					targetRot = Quaternion.LookRotation(Vector3.ProjectOnPlane(Camera.main.transform.forward, Vector3.up)).eulerAngles;
				}
				targetRot.y += offsetRotY;
			}
			smoothedAcceleration = Vector3.SmoothDamp(smoothedAcceleration, moveDir, ref veloHelper, accelerationTime, float.PositiveInfinity, Time.deltaTime);
			smoothedRotation = Quaternion.Lerp(rigbody.rotation, Quaternion.Euler(targetRot), Time.deltaTime * RotationSpeed);
		}
	}
}
