using UnityEngine;
using UnityEngine.Events;

public class TitleCore : MonoBehaviour
{
	public static TitleCore instance;

	[Header("System Status")]
	public bool isFeed;

	[Header("Status")]
	public bool isPause;

	public bool isManual;

	public bool isCredit;

	[Header("GUI")]
	public GameObject titleGUI;

	public GameObject pauseGUI;

	public GameObject manualGUI;

	public GameObject creditGUI;

	[Header("Start")]
	public GameObject firstStart;

	public GameObject seccondStart;

	public GameObject prologuePlayCheck;

	[Header("Audio")]
	public AudioSource _audioSource;

	public AudioSource _audioSource2;

	public float pausePitch = 0.5f;

	[Header("SE")]
	public AudioSource _audioSourceSE;

	public AudioClip optionSe;

	[Header("On Enable Event")]
	[SerializeField]
	private UnityEvent m_NextChapter = new UnityEvent();

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
		if (!ES3.KeyExists("FirstStart"))
		{
			firstStart.SetActive(value: true);
			seccondStart.SetActive(value: false);
			prologuePlayCheck.SetActive(value: false);
		}
		else
		{
			firstStart.SetActive(value: false);
			seccondStart.SetActive(value: true);
			prologuePlayCheck.SetActive(value: false);
		}
	}

	private void Start()
	{
		titleGUI.SetActive(value: true);
		pauseGUI.SetActive(value: false);
		manualGUI.SetActive(value: false);
		creditGUI.SetActive(value: false);
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.O))
		{
			GameEnd();
		}
	}

	public void FeedWait()
	{
		isFeed = false;
	}

	public void GameEnd()
	{
		Application.Quit();
	}

	public void FirstStart()
	{
		ES3.Save("FirstStart", value: true);
	}

	public void Pause(bool value)
	{
		isPause = value;
		if (isFeed)
		{
			return;
		}
		if (value)
		{
			titleGUI.SetActive(value: false);
			pauseGUI.SetActive(value: true);
			manualGUI.SetActive(value: false);
			creditGUI.SetActive(value: false);
		}
		else if (!value)
		{
			if (manualGUI.activeSelf)
			{
				manualGUI.SetActive(value: false);
				return;
			}
			titleGUI.SetActive(value: true);
			pauseGUI.SetActive(value: false);
			manualGUI.SetActive(value: false);
			creditGUI.SetActive(value: false);
		}
	}

	public void Credit(bool value)
	{
		isCredit = value;
		titleGUI.SetActive(!isCredit);
		creditGUI.SetActive(isCredit);
	}

	public void Manual(bool value)
	{
		isManual = value;
		titleGUI.SetActive(!isManual);
		manualGUI.SetActive(isManual);
	}
}
