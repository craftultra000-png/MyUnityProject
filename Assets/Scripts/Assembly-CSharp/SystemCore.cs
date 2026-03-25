using UnityEngine;
using UnityEngine.Events;

public class SystemCore : MonoBehaviour
{
	public static SystemCore instance;

	public CameraController _cameraController;

	public FeelerController _feelerController;

	public FeelerControllerData _feelerControllerData;

	public MissionGUIManager _missionGUIManager;

	[Header("System Status")]
	public bool isFeed;

	public bool isCursor;

	[Header("Status")]
	public bool isHideGUI;

	public bool isPause;

	public bool isManual;

	public bool isScreenLock;

	public bool isStageEnd;

	[Header("GUI")]
	public GameObject playGUI;

	public GameObject pauseGUI;

	public GameObject manualGUI;

	public GameObject selectioGUI;

	[Header("Cursor Icon")]
	public GameObject cursor;

	[Header("PauseOffObject")]
	public bool isPlayerLight;

	public GameObject pauseCameraLight;

	[Header("Audio")]
	public AudioSource _audioSource;

	public AudioSource _audioSource2;

	public float pausePitch = 0.5f;

	[Header("SE")]
	public AudioSource _audioSourceSE;

	public AudioClip optionSe;

	public AudioClip lightSe;

	[Header("On Enable Event")]
	[SerializeField]
	private UnityEvent m_NextChapter = new UnityEvent();

	[SerializeField]
	private UnityEvent m_ReturnTitle = new UnityEvent();

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
	}

	private void Start()
	{
		isScreenLock = true;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		playGUI.SetActive(value: true);
		pauseGUI.SetActive(value: false);
		manualGUI.SetActive(value: false);
		selectioGUI.SetActive(value: true);
		isPlayerLight = false;
		pauseCameraLight.SetActive(value: true);
		Debug.LogError("Save StageScene Name: TitleScene");
		ES3.Save("PreviousSceneName", "TitleScene");
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape) && !isStageEnd)
		{
			isPause = !isPause;
			Pause(isPause);
		}
		if (Input.GetKeyDown(KeyCode.M) && !isStageEnd)
		{
			isManual = !isManual;
			Manual(isManual);
		}
	}

	public void PlayerLight(int value)
	{
		Debug.LogError("Set PlayerLight: " + value);
		isPlayerLight = !isPlayerLight;
		pauseCameraLight.SetActive(!isPlayerLight);
		EffectSeManager.instance.PlaySe(lightSe);
		SkillGUIDataBase.instance.SetEnable(value, isPlayerLight);
	}

	public void Coursor(bool value)
	{
		isScreenLock = value;
		if (!isScreenLock)
		{
			isScreenLock = true;
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
			cursor.SetActive(value: true);
		}
		else
		{
			isScreenLock = false;
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			cursor.SetActive(value: false);
			HideGUI(value: false);
		}
	}

	public void GameEnd()
	{
		Application.Quit();
	}

	public void FinishButton()
	{
		playGUI.SetActive(value: false);
		pauseGUI.SetActive(value: false);
		manualGUI.SetActive(value: false);
		selectioGUI.SetActive(value: false);
	}

	public void FeedWait()
	{
		isFeed = false;
		_missionGUIManager.StartMission();
	}

	public void NextChapter()
	{
		isFeed = true;
		m_NextChapter.Invoke();
	}

	public void ReturnTitle()
	{
		isFeed = true;
		m_ReturnTitle.Invoke();
	}

	public void HideGUI(bool value)
	{
		isHideGUI = value;
		playGUI.SetActive(!isHideGUI);
	}

	public void Pause(bool value)
	{
		if (!isFeed)
		{
			isPause = value;
			_cameraController.isPause = isPause;
			_feelerController.isPause = isPause;
			isManual = false;
			if (value)
			{
				isCursor = false;
				Cursor.visible = true;
				Cursor.lockState = CursorLockMode.None;
				cursor.SetActive(value: false);
				Time.timeScale = 0f;
				_audioSource.pitch = pausePitch;
				_audioSource2.pitch = pausePitch;
				playGUI.SetActive(value: false);
				pauseGUI.SetActive(value: true);
				manualGUI.SetActive(value: false);
				selectioGUI.SetActive(value: false);
				pauseCameraLight.SetActive(value: false);
				_feelerControllerData.MouseReset();
			}
			else if (!value)
			{
				WindowOff();
			}
		}
	}

	public void Manual(bool value)
	{
		if (!isFeed)
		{
			isManual = value;
			_cameraController.isPause = isManual;
			_feelerController.isPause = isManual;
			isPause = false;
			if (value)
			{
				isCursor = false;
				Cursor.visible = true;
				Cursor.lockState = CursorLockMode.None;
				cursor.SetActive(value: false);
				Time.timeScale = 0f;
				_audioSource.pitch = pausePitch;
				_audioSource2.pitch = pausePitch;
				playGUI.SetActive(value: false);
				pauseGUI.SetActive(value: false);
				manualGUI.SetActive(value: true);
				selectioGUI.SetActive(value: false);
				pauseCameraLight.SetActive(value: false);
				_feelerControllerData.MouseReset();
			}
			else if (!value)
			{
				WindowOff();
			}
		}
	}

	public void WindowOff()
	{
		Coursor(!isScreenLock);
		Time.timeScale = 1f;
		_audioSource.pitch = 1f;
		_audioSource2.pitch = 1f;
		playGUI.SetActive(value: true);
		pauseGUI.SetActive(value: false);
		manualGUI.SetActive(value: false);
		selectioGUI.SetActive(value: true);
		pauseCameraLight.SetActive(!isPlayerLight);
	}
}
