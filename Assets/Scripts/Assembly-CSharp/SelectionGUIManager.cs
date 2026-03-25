using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectionGUIManager : MonoBehaviour
{
	[Header("Camera")]
	public Transform cameraTransform;

	[Header("Status")]
	public bool isLock;

	[Header("Camera Position")]
	[Range(0f, 1f)]
	public float currentZoom = 0.5f;

	public Vector2 currentPosition;

	[Space]
	public float defaultX = 50f;

	public Vector3 calcPosition;

	public Vector3 targetPosition;

	[Header("Data")]
	public Vector2 cameraLimitX = new Vector2(0.5f, 0.5f);

	public Vector2 cameraLimitY = new Vector2(49f, 50f);

	public Vector2 cameraLimitZ = new Vector2(-0.9f, -2.3f);

	[Header("Movement Speed")]
	public float cameraSpeed = 5f;

	public float zoomSpeed = 0.2f;

	public float moveSpeed = 0.2f;

	[Header("Preset")]
	public string currentPreset;

	public float zoomIdle;

	public float zoomInsert;

	public float zoomPiston;

	public float zoomShot;

	public Vector2 positionIdle;

	public Vector2 positionInsert;

	public Vector2 positionPiston;

	public Vector2 positionShot;

	[Header("Text")]
	public TextMeshProUGUI zoomOutText;

	public TextMeshProUGUI zoomInText;

	public Image moveUpImage;

	public Image moveDownImage;

	public Image moveLeftImage;

	public Image moveRightImage;

	[Header("Button")]
	public ButtonTriggerGUI zoomOutButton;

	public ButtonTriggerGUI zoomInButton;

	public ButtonTriggerGUI moveUpButton;

	public ButtonTriggerGUI moveDownButton;

	public ButtonTriggerGUI moveLeftButton;

	public ButtonTriggerGUI moveRightButton;

	[Header("Lock Image")]
	public Image lockImage;

	public ButtonTriggerGUI lockButton;

	public Sprite lockIcon;

	public Sprite unlockIcon;

	[Header("Color")]
	public Color enableColor;

	public Color disableColor;

	private void Start()
	{
		ChangeButtonColor();
		LockButton(value: false);
		CameraSet("Idle");
	}

	private void LateUpdate()
	{
		calcPosition.z = Mathf.Lerp(cameraLimitZ.x, cameraLimitZ.y, currentZoom);
		float t = (currentPosition.x + 1f) * 0.5f;
		calcPosition.x = Mathf.Lerp(cameraLimitX.x, cameraLimitX.y, t);
		float t2 = (currentPosition.y + 1f) * 0.5f;
		calcPosition.y = Mathf.Lerp(cameraLimitY.x, cameraLimitY.y, t2);
		targetPosition = new Vector3(calcPosition.x + defaultX, calcPosition.y, calcPosition.z);
		cameraTransform.position = Vector3.Lerp(cameraTransform.position, targetPosition, Time.deltaTime * cameraSpeed);
	}

	public void ZoomOut()
	{
		currentZoom = Mathf.Clamp01(currentZoom - zoomSpeed);
		currentZoom = Mathf.Round(currentZoom * 10f) / 10f;
		ChangeButtonColor();
	}

	public void ZoomIn()
	{
		currentZoom = Mathf.Clamp01(currentZoom + zoomSpeed);
		currentZoom = Mathf.Round(currentZoom * 10f) / 10f;
		ChangeButtonColor();
	}

	public void MoveLeft()
	{
		currentPosition.x -= moveSpeed;
		currentPosition.x = Mathf.Round(currentPosition.x * 10f) / 10f;
		if (currentPosition.x < -1f)
		{
			currentPosition.x = -1f;
		}
		ChangeButtonColor();
	}

	public void MoveRight()
	{
		currentPosition.x += moveSpeed;
		currentPosition.x = Mathf.Round(currentPosition.x * 10f) / 10f;
		if (currentPosition.x > 1f)
		{
			currentPosition.x = 1f;
		}
		ChangeButtonColor();
	}

	public void MoveUp()
	{
		currentPosition.y += moveSpeed;
		currentPosition.y = Mathf.Round(currentPosition.y * 10f) / 10f;
		if (currentPosition.y > 1f)
		{
			currentPosition.y = 1f;
		}
		ChangeButtonColor();
	}

	public void MoveDown()
	{
		currentPosition.y -= moveSpeed;
		currentPosition.y = Mathf.Round(currentPosition.y * 10f) / 10f;
		if (currentPosition.y < -1f)
		{
			currentPosition.y = -1f;
		}
		ChangeButtonColor();
	}

	public void CameraSet(string value)
	{
		currentPreset = value;
		if (!isLock)
		{
			if (currentPreset == "Idle")
			{
				currentZoom = zoomIdle;
				currentPosition = positionIdle;
			}
			else if (currentPreset == "Insert")
			{
				currentZoom = zoomInsert;
				currentPosition = positionInsert;
			}
			else if (currentPreset == "Piston")
			{
				currentZoom = zoomPiston;
				currentPosition = positionPiston;
			}
			else if (currentPreset == "Shot")
			{
				currentZoom = zoomShot;
				currentPosition = positionShot;
			}
			else
			{
				Debug.LogError("Selection Camera Set is not found!", base.gameObject);
			}
			ChangeButtonColor();
		}
	}

	public void ChangeButtonColor()
	{
		zoomOutText.color = enableColor;
		zoomInText.color = enableColor;
		moveUpImage.color = enableColor;
		moveDownImage.color = enableColor;
		moveLeftImage.color = enableColor;
		moveRightImage.color = enableColor;
		zoomOutButton.defaultColor = enableColor;
		zoomInButton.defaultColor = enableColor;
		moveUpButton.defaultColor = enableColor;
		moveDownButton.defaultColor = enableColor;
		moveLeftButton.defaultColor = enableColor;
		moveRightButton.defaultColor = enableColor;
		if (currentZoom == 0f)
		{
			zoomOutText.color = disableColor;
			zoomOutButton.defaultColor = disableColor;
		}
		else if (currentZoom == 1f)
		{
			zoomInText.color = disableColor;
			zoomInButton.defaultColor = disableColor;
		}
		if (currentPosition.y == 1f)
		{
			moveUpImage.color = disableColor;
			moveUpButton.defaultColor = disableColor;
		}
		else if (currentPosition.y == -1f)
		{
			moveDownImage.color = disableColor;
			moveDownButton.defaultColor = disableColor;
		}
		if (currentPosition.x == -1f)
		{
			moveLeftImage.color = disableColor;
			moveLeftButton.defaultColor = disableColor;
		}
		else if (currentPosition.x == 1f)
		{
			moveRightImage.color = disableColor;
			moveRightButton.defaultColor = disableColor;
		}
	}

	public void LockButton(bool value)
	{
		isLock = value;
		if (isLock)
		{
			lockImage.sprite = lockIcon;
			lockImage.color = enableColor;
			lockButton.defaultColor = enableColor;
		}
		else
		{
			lockImage.sprite = unlockIcon;
			lockImage.color = disableColor;
			lockButton.defaultColor = disableColor;
		}
	}
}
