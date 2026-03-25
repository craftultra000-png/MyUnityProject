using UnityEngine;

namespace FIMSpace.Basics
{
	public class FBasic_OffsetMovement : MonoBehaviour
	{
		private Vector3 startPos;

		private Quaternion startRot;

		public Vector3 positionRange = new Vector3(1f, 0f, 0f);

		public Vector3 rotRange = Vector3.zero;

		public float speed = 2f;

		public bool fixedUpdate;

		private void Start()
		{
			startPos = base.transform.position;
			startRot = base.transform.rotation;
		}

		private void Update()
		{
			if (!fixedUpdate)
			{
				base.transform.position = startPos + positionRange * Mathf.Sin(Time.time * speed);
				base.transform.rotation = startRot * Quaternion.Euler(rotRange * Mathf.Sin(Time.time * speed));
			}
		}

		private void FixedUpdate()
		{
			if (fixedUpdate)
			{
				Rigidbody component = GetComponent<Rigidbody>();
				if ((bool)component)
				{
					component.MovePosition(startPos + positionRange * Mathf.Sin(Time.time * speed));
					component.MoveRotation(startRot * Quaternion.Euler(rotRange * Mathf.Sin(Time.time * speed)));
				}
				else
				{
					base.transform.position = startPos + positionRange * Mathf.Sin(Time.time * speed);
					base.transform.rotation = startRot * Quaternion.Euler(rotRange * Mathf.Sin(Time.time * speed));
				}
			}
		}
	}
}
