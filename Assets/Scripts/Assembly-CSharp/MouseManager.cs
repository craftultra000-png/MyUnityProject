using UnityEngine;
using UnityEngine.UI;

public class MouseManager : MonoBehaviour
{
	public static MouseManager instance;

	public CameraController _cameraController;

	public BriefingCameraController _briefingCameraController;

	[Header("MouseWheel Switch")]
	public bool mouseWheelSwitch;

	[Header("Mouse Sensitivity")]
	public int mouseSensitivityDefault;

	public int mouseSensitivity;

	public float mouseSensitivityCalc = 1f;

	[Header("Wheel Sensitivity")]
	public int wheelSensitivityDefault;

	public int wheelSensitivity;

	public float wheelSensitivityCalc = 1f;

	[Header("Key Sensitivity")]
	public int keySensitivityDefault;

	public int keySensitivity;

	public float keySensitivityCalc = 1f;

	[Header("Mouse WheelSwitch")]
	public GameObject wheelSwitchFalse;

	public GameObject wheelSwitchTrue;

	public GameObject manualMoveFalse;

	public GameObject manualMoveTrue;

	public GameObject manualZoomFalse;

	public GameObject manualZoomTrue;

	[Header("UI Slider")]
	public bool useSliderSe;

	public Slider mouseSensitivitySlider;

	public Slider wheelSensitivitySlider;

	public Slider keySensitivitySlider;

	public AudioSource _audioSourceSe;

	public AudioClip slideSe;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		if (!ES3.KeyExists("MouseSensitivity"))
		{
			ES3.Save("MouseSensitivity", mouseSensitivityDefault);
		}
		mouseSensitivity = ES3.Load<int>("MouseSensitivity");
		if (!ES3.KeyExists("WheelSensitivity"))
		{
			ES3.Save("WheelSensitivity", wheelSensitivityDefault);
		}
		wheelSensitivity = ES3.Load<int>("WheelSensitivity");
		if (!ES3.KeyExists("MouseWheelSwitch"))
		{
			ES3.Save("MouseWheelSwitch", mouseWheelSwitch);
		}
		mouseWheelSwitch = ES3.Load<bool>("MouseWheelSwitch");
		if (!ES3.KeyExists("KeySensitivity"))
		{
			ES3.Save("KeySensitivity", keySensitivityDefault);
		}
		keySensitivity = ES3.Load<int>("KeySensitivity");
	}

	private void Start()
	{
		if (mouseSensitivitySlider != null)
		{
			mouseSensitivitySlider.value = mouseSensitivity;
		}
		if (wheelSensitivitySlider != null)
		{
			wheelSensitivitySlider.value = wheelSensitivity;
		}
		if (keySensitivitySlider != null)
		{
			keySensitivitySlider.value = keySensitivity;
		}
		UseSlide(value: true);
		SetMouseSensitivity();
		SwitchWheelText();
	}

	public void UseSlide(bool value)
	{
		useSliderSe = value;
	}

	public void SliderSe()
	{
		if (useSliderSe)
		{
			_audioSourceSe.PlayOneShot(slideSe);
		}
	}

	public void SetMouseSensitivity()
	{
		mouseSensitivity = ES3.Load<int>("MouseSensitivity");
		mouseSensitivityCalc = 1f + (float)mouseSensitivity * 0.05f;
		if (_cameraController != null)
		{
			_cameraController.mouseSensitivity = mouseSensitivityCalc;
		}
		if (_briefingCameraController != null)
		{
			_briefingCameraController.mouseSensitivity = mouseSensitivityCalc;
		}
	}

	public void SaveMouseSensitivity()
	{
		ES3.Save("MouseSensitivity", Mathf.RoundToInt(mouseSensitivitySlider.value));
		SetMouseSensitivity();
	}

	public void SetWheelSensitivity()
	{
		wheelSensitivity = ES3.Load<int>("WheelSensitivity");
		wheelSensitivityCalc = 1f + (float)wheelSensitivity * 0.05f;
		if (_cameraController != null)
		{
			_cameraController.wheelSensitivity = wheelSensitivityCalc;
		}
		if (_briefingCameraController != null)
		{
			_briefingCameraController.wheelSensitivity = wheelSensitivityCalc;
		}
	}

	public void SaveWheelSensitivity()
	{
		ES3.Save("WheelSensitivity", Mathf.RoundToInt(wheelSensitivitySlider.value));
		SetWheelSensitivity();
	}

	public void SetMouseWheelSwitch()
	{
		mouseWheelSwitch = ES3.Load<bool>("MouseWheelSwitch");
	}

	public void SaveMouseWheelSwitch()
	{
		mouseWheelSwitch = !mouseWheelSwitch;
		ES3.Save("MouseWheelSwitch", mouseWheelSwitch);
		SwitchWheelText();
	}

	public void SwitchWheelText()
	{
		if (!mouseWheelSwitch)
		{
			wheelSwitchFalse.SetActive(value: true);
			wheelSwitchTrue.SetActive(value: false);
			if (manualMoveFalse != null)
			{
				manualMoveFalse.SetActive(value: true);
				manualMoveTrue.SetActive(value: false);
				manualZoomFalse.SetActive(value: true);
				manualZoomTrue.SetActive(value: false);
			}
		}
		else
		{
			wheelSwitchFalse.SetActive(value: false);
			wheelSwitchTrue.SetActive(value: true);
			if (manualMoveFalse != null)
			{
				manualMoveFalse.SetActive(value: false);
				manualMoveTrue.SetActive(value: true);
				manualZoomFalse.SetActive(value: false);
				manualZoomTrue.SetActive(value: true);
			}
		}
		if (_cameraController != null)
		{
			_cameraController.mouseWheelSwitch = mouseWheelSwitch;
		}
		if (_briefingCameraController != null)
		{
			_briefingCameraController.mouseWheelSwitch = mouseWheelSwitch;
		}
	}

	public void SetKeySensitivity()
	{
		keySensitivity = ES3.Load<int>("KeySensitivity");
		keySensitivityCalc = 1f + (float)keySensitivity * 0.05f;
	}

	public void SaveKeySensitivity()
	{
		ES3.Save("KeySensitivity", Mathf.RoundToInt(keySensitivitySlider.value));
		SetKeySensitivity();
	}
}
