using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Utage;

public class BriefingCore : MonoBehaviour
{
	public static BriefingCore instance;

	public GuildQuestCameraManager _guildQuestCameraManager;

	public GuildCameraManager _guildCameraManager;

	public GuildTongueObject _guildTongueObject;

	public BriefingCaracterAnimancer _brefingCaracterAnimancer;

	public BriefingCharacterManager _briefingCharacterManager;

	public CharacterMouthManager _characterMouthManager;

	public GuildClockObject _guildClockObject;

	public BGMManager _BGMManager;

	public BriefingCameraManager _briefingCameraManager;

	public BriefingCameraController _briefingCameraController;

	public MissionSelectManager _missionSelectManager;

	[SerializeField]
	protected AdvEngine advEngine;

	[Header("Status")]
	public bool isPause;

	public bool isScreenLock;

	[Space]
	public bool isCameraMode;

	public bool isGamePlay;

	public string previousSceneName;

	[Header("GUI")]
	public GameObject pauseGUI;

	public GameObject buttonsGUI;

	public GameObject manualOpenGUI;

	public GameObject manualGUI;

	[Header("GUI")]
	public int cameraType;

	public ButtonTriggerGUI missionButton;

	public ButtonTriggerGUI cameraButton;

	public ButtonTriggerGUI jobListButton;

	public GameObject curosrIcon;

	public GameObject cameraIcon;

	public GameObject missionGUI;

	public GameObject cameraGUI;

	public GameObject jobListGUI;

	public Color enableColor;

	public Color disableColor;

	[Header("Cursor Icon")]
	public Image cursorIcon;

	public TextMeshProUGUI cursorText;

	[Header("Cursor Icon")]
	public GameObject cursor;

	[Header("First Information")]
	public bool firstInfo;

	public GameObject feedOutMask;

	public GameObject stageClearButton;

	public GameObject stageClearButton2;

	public GameObject stageClearEffect;

	public GameObject stageClearEffect2;

	public GameObject stageClearEffect3;

	[Header("Audio")]
	public AudioSource _audioSource;

	public AudioSource _audioSource2;

	public float pausePitch = 0.5f;

	[Header("System Status")]
	public bool isFeed;

	public bool isCameraFeedOut;

	public bool isCameraFeedIn;

	public bool isTongue;

	[Header("Talk")]
	public string talk;

	[Header("Object")]
	public GameObject questAgreeButon;

	public GameObject skipButon;

	public GameObject feedLogo;

	[Header("Tangue")]
	public Transform tongueObject;

	public float tongueMove;

	public Vector3 currentTongue;

	public Vector3 startTongue;

	public Vector3 endTongue;

	[Header("Material")]
	public Image feedImage;

	public Material feedMaterial;

	public float feed;

	[Header("BGM")]
	public AudioSource _audioSourceBGM_Se;

	public AudioClip logoSe;

	[Header("SE")]
	public AudioSource _audioSourceSE;

	public AudioClip spawnSe;

	public AudioClip buttonSe;

	public AudioClip signSe;

	public AudioClip eatStartSe;

	public AudioClip wetVaginaSe;

	public AudioClip eatEndSe;

	public AudioClip woodKnockSe;

	public AudioClip magicStartSe;

	public AudioClip buttonEffectSe;

	[Header("Se")]
	public AudioClip cursorOnSe;

	public AudioClip cursorOffSe;

	[Header("On Enable Event")]
	[SerializeField]
	private UnityEvent m_PlayStart = new UnityEvent();

	[SerializeField]
	private UnityEvent m_NextChapter = new UnityEvent();

	[SerializeField]
	private UnityEvent m_Skip = new UnityEvent();

	[SerializeField]
	private UnityEvent m_ReturnTitle = new UnityEvent();

	public AdvEngine AdvEngine => advEngine;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		if (ResultData.instance != null)
		{
			ResultData.instance.SelfDestroy();
		}
		missionGUI.SetActive(value: false);
		cameraGUI.SetActive(value: false);
		jobListGUI.SetActive(value: false);
		if (!ES3.KeyExists("PreviousSceneName"))
		{
			ES3.Save("PreviousSceneName", "Null");
		}
		previousSceneName = ES3.Load<string>("PreviousSceneName");
		Debug.LogError("Save StageScene Name: " + previousSceneName);
		feedMaterial = feedImage.material;
		feed = 0f;
		feedMaterial.SetFloat("_Feed", 0f);
	}

	private void Start()
	{
		questAgreeButon.SetActive(value: false);
		feedLogo.SetActive(value: false);
		buttonsGUI.SetActive(value: true);
		pauseGUI.SetActive(value: false);
		manualOpenGUI.SetActive(value: false);
		manualGUI.SetActive(value: false);
		skipButon.SetActive(value: false);
		tongueObject.gameObject.SetActive(value: true);
		tongueObject.localPosition = startTongue;
		feedOutMask.SetActive(value: false);
		stageClearButton.SetActive(value: false);
		stageClearButton2.SetActive(value: false);
		stageClearEffect.SetActive(value: false);
		stageClearEffect2.SetActive(value: false);
		stageClearEffect3.SetActive(value: false);
		SetMission();
	}

	private void Update()
	{
		if (!isPause && !isGamePlay)
		{
			if (isCameraMode && cameraType == 1)
			{
				if (Input.GetKeyDown(KeyCode.Mouse0))
				{
					SetCameraMove(value: false);
				}
				if (Input.GetKeyDown(KeyCode.Mouse1))
				{
					SetCameraMove(value: false);
				}
				if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse2))
				{
					SetCameraMove(value: false);
					EffectSeManager.instance.PlaySe(cursorOnSe);
				}
			}
			else if (!isCameraMode && cameraType == 0)
			{
				if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse2))
				{
					SetCamera();
					SetCameraMove(value: true);
					EffectSeManager.instance.PlaySe(cursorOffSe);
				}
			}
			else if (!isCameraMode && cameraType == 1)
			{
				if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse2))
				{
					SetCameraMove(value: true);
					EffectSeManager.instance.PlaySe(cursorOffSe);
				}
			}
			else if (!isCameraMode && cameraType == 2 && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse2)))
			{
				SetCamera();
				SetCameraMove(value: true);
				EffectSeManager.instance.PlaySe(cursorOffSe);
			}
		}
		if (Input.GetKeyDown(KeyCode.Escape) && !isGamePlay)
		{
			isPause = !isPause;
			Pause(isPause);
		}
	}

	private void LateUpdate()
	{
		if (isCameraFeedOut)
		{
			feed += Time.deltaTime;
			if (feed > 1f)
			{
				isCameraFeedOut = false;
				feed = 1f;
			}
			feedMaterial.SetFloat("_Feed", feed);
		}
		if (isCameraFeedIn)
		{
			feed -= Time.deltaTime;
			if (feed < 0f)
			{
				isCameraFeedIn = false;
				feed = 0f;
			}
			feedMaterial.SetFloat("_Feed", feed);
		}
		if (isTongue)
		{
			tongueMove += Time.deltaTime;
			if (tongueMove > 1f)
			{
				tongueMove = 1f;
			}
			currentTongue = Vector3.Lerp(startTongue, endTongue, tongueMove);
			tongueObject.localPosition = currentTongue;
		}
	}

	public void Coursor(bool value)
	{
		Debug.LogError("Cousor: " + value);
		isScreenLock = value;
		_briefingCameraController.isScreenLock = isScreenLock;
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
		}
	}

	private void OnDestroy()
	{
		feed = 0f;
		feedMaterial.SetFloat("_Feed", feed);
	}

	public void Pause(bool value)
	{
		if (isFeed)
		{
			return;
		}
		isPause = value;
		_briefingCameraController.isPause = isPause;
		if (value)
		{
			Time.timeScale = 0f;
			_audioSource.pitch = pausePitch;
			_audioSource2.pitch = pausePitch;
			pauseGUI.SetActive(value: true);
			buttonsGUI.SetActive(value: false);
			missionGUI.SetActive(value: false);
			cameraGUI.SetActive(value: false);
			jobListGUI.SetActive(value: false);
			Coursor(value: true);
		}
		else if (!value)
		{
			Time.timeScale = 1f;
			_audioSource.pitch = 1f;
			_audioSource2.pitch = 1f;
			pauseGUI.SetActive(value: false);
			if (cameraType == 0)
			{
				buttonsGUI.SetActive(value: true);
				missionGUI.SetActive(value: true);
				cameraGUI.SetActive(value: false);
				jobListGUI.SetActive(value: false);
				Coursor(value: true);
			}
			else if (cameraType == 1)
			{
				buttonsGUI.SetActive(value: true);
				missionGUI.SetActive(value: false);
				cameraGUI.SetActive(value: true);
				jobListGUI.SetActive(value: true);
				Coursor(!isCameraMode);
			}
			else if (cameraType == 2)
			{
				buttonsGUI.SetActive(value: true);
				missionGUI.SetActive(value: false);
				cameraGUI.SetActive(value: false);
				jobListGUI.SetActive(value: true);
				Coursor(value: true);
			}
		}
	}

	public void GameEnd()
	{
		Application.Quit();
	}

	public void FeedWait()
	{
		isFeed = false;
	}

	public void Skip()
	{
		TalkGUI.instance.FroceEndTalk();
		m_Skip.Invoke();
	}

	public void ReturnTitle()
	{
		isFeed = true;
		m_ReturnTitle.Invoke();
	}

	public void SetCameraMove(bool value)
	{
		isCameraMode = value;
		cameraIcon.SetActive(!isCameraMode);
		curosrIcon.SetActive(isCameraMode);
		Coursor(!isCameraMode);
	}

	public void SetMission()
	{
		missionButton.defaultColor = enableColor;
		missionButton.ColorReset();
		cameraButton.defaultColor = disableColor;
		cameraButton.ColorReset();
		jobListButton.defaultColor = disableColor;
		jobListButton.ColorReset();
		missionGUI.SetActive(value: true);
		cameraGUI.SetActive(value: false);
		jobListGUI.SetActive(value: false);
		cameraType = 0;
		Coursor(value: true);
	}

	public void SetCamera()
	{
		missionButton.defaultColor = disableColor;
		missionButton.ColorReset();
		cameraButton.defaultColor = enableColor;
		cameraButton.ColorReset();
		jobListButton.defaultColor = disableColor;
		jobListButton.ColorReset();
		missionGUI.SetActive(value: false);
		cameraGUI.SetActive(value: true);
		jobListGUI.SetActive(value: false);
		cameraType = 1;
		SetCameraMove(value: true);
	}

	public void SetJobList()
	{
		missionButton.defaultColor = disableColor;
		missionButton.ColorReset();
		cameraButton.defaultColor = disableColor;
		cameraButton.ColorReset();
		jobListButton.defaultColor = enableColor;
		jobListButton.ColorReset();
		missionGUI.SetActive(value: false);
		cameraGUI.SetActive(value: false);
		jobListGUI.SetActive(value: true);
		cameraType = 2;
		Coursor(value: true);
	}

	public void SetGUI()
	{
		if (cameraType == 0)
		{
			missionGUI.SetActive(value: true);
		}
		else if (cameraType == 1)
		{
			cameraGUI.SetActive(value: true);
		}
		else if (cameraType == 2)
		{
			jobListGUI.SetActive(value: true);
		}
	}

	public void StartTalk()
	{
		Debug.LogError("Start Talk");
		SetCamera();
		SetCameraMove(value: true);
		isGamePlay = true;
		_briefingCharacterManager.isGamePlay = true;
		missionGUI.SetActive(value: false);
		cameraGUI.SetActive(value: false);
		jobListGUI.SetActive(value: false);
		buttonsGUI.SetActive(value: false);
		firstInfo = false;
		if (!ES3.KeyExists("FirstBriefing"))
		{
			firstInfo = true;
			ES3.Save("FirstBriefing", value: true);
			advEngine.Param.TrySetParameter("FirstBriefingTalk", firstInfo);
		}
		Debug.LogError("FirstBriefingCheck: " + firstInfo);
		TalkGUI.instance.FroceEndTalk();
		TalkGUI.instance.SpownTalk("BriefingEnd", force: true);
	}

	public void PlayStart()
	{
		m_PlayStart.Invoke();
	}

	public void SpawnTalk(string value)
	{
		talk = value;
		TalkGUI.instance.SpownTalk(talk, force: true);
	}

	public void BriefingEvent(string value)
	{
		switch (value)
		{
		case "FeedOut":
			isCameraFeedOut = true;
			isCameraFeedIn = false;
			break;
		case "FeedIn":
			isCameraFeedOut = false;
			isCameraFeedIn = true;
			break;
		case "StopBGM":
			_BGMManager.StopBGM();
			break;
		case "PlayStart":
			PlayStart();
			break;
		case "WoodKnock":
			_audioSourceSE.PlayOneShot(woodKnockSe);
			break;
		case "Finish":
			_brefingCaracterAnimancer.SetAnimationClip("Finish");
			break;
		case "MagicStart":
			_audioSourceSE.PlayOneShot(magicStartSe);
			_missionSelectManager.MagicStart();
			break;
		case "HitSe":
			_characterMouthManager.PlayHitSe();
			break;
		case "WetSe":
			_audioSourceSE.PlayOneShot(wetVaginaSe);
			break;
		case "EatEndSe":
			_audioSourceSE.PlayOneShot(eatEndSe);
			break;
		case "Masturbation":
			_brefingCaracterAnimancer.SetAnimationClip("Masturbation");
			break;
		case "ManualInfo":
			manualOpenGUI.SetActive(value: true);
			break;
		case "ManualStart":
			manualGUI.SetActive(value: true);
			manualOpenGUI.SetActive(value: false);
			break;
		case "ManualEnd":
			manualGUI.SetActive(value: false);
			manualOpenGUI.SetActive(value: false);
			TalkGUI.instance.FroceEndTalk();
			TalkGUI.instance.SpownTalk("BriefingEndFirst", force: true);
			break;
		case "FirstInfo":
			feedOutMask.SetActive(value: true);
			Coursor(value: true);
			break;
		case "FirstSkip":
			skipButon.SetActive(value: true);
			break;
		case "FirstInfoStageClear":
			_audioSourceSE.PlayOneShot(buttonEffectSe);
			stageClearEffect.SetActive(value: true);
			stageClearButton.SetActive(value: true);
			break;
		case "FirstInfoStageClear2":
			_audioSourceSE.PlayOneShot(buttonEffectSe);
			stageClearEffect2.SetActive(value: true);
			stageClearButton2.SetActive(value: true);
			break;
		case "FirstInfoStageClear3":
			_audioSourceSE.PlayOneShot(buttonEffectSe);
			stageClearEffect3.SetActive(value: true);
			break;
		case "Logo":
			feedLogo.SetActive(value: true);
			_guildClockObject.isClock = false;
			break;
		case "LogoJingle":
			_audioSourceSE.PlayOneShot(logoSe);
			break;
		}
	}
}
