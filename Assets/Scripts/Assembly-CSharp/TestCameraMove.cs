using UnityEngine;

public class TestCameraMove : MonoBehaviour
{
	[Header("Stauts")]
	public bool isCursor;

	[Header("Control")]
	public float moveSpeed = 5f;

	public float mouseSensitivity = 100f;

	private float pitch;

	private float yaw;

	private void Start()
	{
		Vector3 eulerAngles = base.transform.eulerAngles;
		pitch = eulerAngles.x;
		yaw = eulerAngles.y;
		isCursor = false;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	private void Update()
	{
		if (!isCursor)
		{
			float num = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
			float num2 = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
			yaw += num;
			pitch -= num2;
			pitch = Mathf.Clamp(pitch, -90f, 90f);
			base.transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
			Vector3 zero = Vector3.zero;
			if (Input.GetKey(KeyCode.W))
			{
				zero += Vector3.forward;
			}
			if (Input.GetKey(KeyCode.S))
			{
				zero += Vector3.back;
			}
			if (Input.GetKey(KeyCode.A))
			{
				zero += Vector3.left;
			}
			if (Input.GetKey(KeyCode.D))
			{
				zero += Vector3.right;
			}
			if (zero != Vector3.zero)
			{
				zero.Normalize();
				base.transform.Translate(zero * moveSpeed * Time.deltaTime, Space.Self);
			}
		}
		if (Input.GetKeyDown(KeyCode.Escape) && !isCursor)
		{
			isCursor = true;
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
		else if (Input.GetKeyDown(KeyCode.Escape) && isCursor)
		{
			isCursor = false;
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
	}
}
