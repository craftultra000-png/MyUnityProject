using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GuildCore : MonoBehaviour
{
	public static GuildCore instance;

	public GuildQuestCameraManager _guildQuestCameraManager;

	public GuildCameraManager _guildCameraManager;

	public GuildTongueObject _guildTongueObject;

	public GuildCaracterAnimancer _guildCaracterAnimancer;

	public CharacterMouthManager _characterMouthManager;

	public CharacterFacialManager _characterFacialManager;

	public GuildClockObject _guildClockObject;

	public BGMManager _BGMManager;

	[Header("Status")]
	public bool isPause;

	[Header("GUI")]
	public GameObject pauseGUI;

	public GameObject buttonsGUI;

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
	public GameObject secretQuestPaper;

	public GameObject signName;

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

	public AudioClip footStepSe;

	public AudioClip eatStartSe;

	public AudioClip wetVaginaSe;

	public AudioClip eatEndSe;

	[Header("On Enable Event")]
	[SerializeField]
	private UnityEvent m_NextChapter = new UnityEvent();

	[SerializeField]
	private UnityEvent m_Skip = new UnityEvent();

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
		secretQuestPaper.SetActive(value: false);
		signName.SetActive(value: false);
		questAgreeButon.SetActive(value: false);
		skipButon.SetActive(value: false);
		feedLogo.SetActive(value: false);
		pauseGUI.SetActive(value: false);
		buttonsGUI.SetActive(value: true);
		isCameraFeedOut = true;
		feedMaterial = feedImage.material;
		feed = 1f;
		feedMaterial.SetFloat("_Feed", 1f);
		tongueObject.gameObject.SetActive(value: true);
		tongueObject.localPosition = startTongue;
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
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

	private void OnDestroy()
	{
		feed = 0f;
		feedMaterial.SetFloat("_Feed", feed);
	}

	public void Pause(bool value)
	{
		if (!isFeed)
		{
			isPause = value;
			if (value)
			{
				Time.timeScale = 0f;
				_audioSource.pitch = pausePitch;
				_audioSource2.pitch = pausePitch;
				buttonsGUI.SetActive(value: false);
				pauseGUI.SetActive(value: true);
			}
			else if (!value)
			{
				Time.timeScale = 1f;
				_audioSource.pitch = 1f;
				_audioSource2.pitch = 1f;
				buttonsGUI.SetActive(value: true);
				pauseGUI.SetActive(value: false);
			}
		}
	}

	public void FeedWait()
	{
		isFeed = false;
	}

	public void Skip()
	{
		TalkGUI.instance.FroceEndTalk();
		_BGMManager.StopBGM();
		m_Skip.Invoke();
	}

	public void SpawnTalk(string value)
	{
		talk = value;
		TalkGUI.instance.SpownTalk(talk, force: true);
	}

	public void GuildEvent(string value)
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
			skipButon.SetActive(value: true);
			break;
		case "MoveCamera1":
			_guildQuestCameraManager.isTime = true;
			break;
		case "MoveCamera2":
			TalkGUI.instance.SpownTalk("GuildStart2", force: true);
			break;
		case "MoveCamera3":
			_guildQuestCameraManager.isTime = true;
			break;
		case "MoveCamera4":
			_guildCameraManager.StartSet();
			_guildClockObject.isClock = true;
			break;
		case "MoveCamera5":
			_guildCameraManager.isTime = true;
			break;
		case "MoveCamera6":
			_guildCameraManager.isTime = true;
			break;
		case "SpawnQuest":
			secretQuestPaper.SetActive(value: true);
			_audioSourceSE.PlayOneShot(spawnSe);
			break;
		case "StopBGM":
			_BGMManager.StopBGM();
			break;
		case "QuestAgreeStart":
			questAgreeButon.SetActive(value: true);
			_audioSourceSE.PlayOneShot(buttonSe);
			break;
		case "QuestAgreeEnd":
			signName.SetActive(value: true);
			questAgreeButon.SetActive(value: false);
			_audioSourceSE.PlayOneShot(signSe);
			TalkGUI.instance.SpownTalk("GuildStart3", force: true);
			break;
		case "HitSe":
			_characterMouthManager.PlayHitSe();
			break;
		case "FootStepSe":
			_audioSourceSE.PlayOneShot(footStepSe);
			break;
		case "WetSe":
			_audioSourceSE.PlayOneShot(wetVaginaSe);
			break;
		case "EatEndSe":
			_audioSourceSE.PlayOneShot(eatEndSe);
			break;
		case "Masturbation":
			_guildCaracterAnimancer.SetAnimationClip("Masturbation");
			_characterFacialManager.isBottom = true;
			_characterFacialManager.isEyesClose02 = true;
			break;
		case "LookFront":
			_guildCaracterAnimancer.SetAnimationClip("LookFront");
			_characterFacialManager.isBottom = false;
			_characterFacialManager.isEyesClose02 = false;
			break;
		case "HandDown":
			_guildCaracterAnimancer.SetAnimationClip("HandDown");
			break;
		case "TongueEat":
			isTongue = true;
			_guildTongueObject.SetAnimationClip("Eat");
			_guildCaracterAnimancer.SetAnimationClip("Eat");
			_audioSourceSE.PlayOneShot(eatStartSe);
			break;
		case "Logo":
			skipButon.SetActive(value: false);
			feedLogo.SetActive(value: true);
			_guildClockObject.isClock = false;
			break;
		case "LogoJingle":
			_audioSourceSE.PlayOneShot(logoSe);
			break;
		}
	}
}
