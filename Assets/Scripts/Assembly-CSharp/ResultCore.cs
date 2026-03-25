using UnityEngine;
using UnityEngine.Events;
using Utage;

public class ResultCore : MonoBehaviour
{
	public static ResultCore instance;

	public ResultDataManager _resultDataManager;

	public BGMManager _BGMManager;

	public GuildCardObject _guildCardObject;

	[SerializeField]
	protected AdvEngine advEngine;

	[Header("ResultTalk")]
	public int resultTalk;

	public int resultJob;

	[Header("Virgin")]
	public bool isLostVirgin;

	[Header("UnDestroy")]
	public GameObject countStatus;

	public GameObject guildCard;

	public GameObject missionList;

	[Header("Transform")]
	public GameObject rankFame;

	public GameObject countStatusFrame;

	public GameObject guildCardFrame;

	[Space]
	public Transform countStatusTransform;

	public Transform guildCardTransform;

	public Transform missionListTransform;

	[Space]
	public RectTransform countStatusRectTransform;

	public RectTransform guildCardRectTransform;

	public RectTransform missionListRectTransform;

	[Header("Button")]
	public GameObject skipButon;

	public GameObject resultEndButton;

	[Header("System Status")]
	public bool isFeed;

	[Header("SE")]
	public AudioSource _audioSourceSE;

	public AudioClip magicStartSe;

	public AudioClip framePopSe;

	[Header("On Enable Event")]
	[SerializeField]
	private UnityEvent m_ResultEnd = new UnityEvent();

	[SerializeField]
	private UnityEvent m_ReturnTitle = new UnityEvent();

	public AdvEngine AdvEngine => advEngine;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
	}

	private void Start()
	{
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		resultEndButton.SetActive(value: false);
		skipButon.SetActive(value: false);
		rankFame.SetActive(value: false);
		countStatusFrame.SetActive(value: false);
		guildCardFrame.SetActive(value: false);
		missionListTransform.gameObject.SetActive(value: false);
		_guildCardObject = ResultData.instance._guildCardObject;
		_guildCardObject.ResultGuildCard();
		_resultDataManager.GetCountStatus();
		MoveDontDestroy();
	}

	public void MoveDontDestroy()
	{
		countStatus = ResultData.instance.countStatus;
		guildCard = ResultData.instance.guildCard;
		missionList = ResultData.instance.missionList;
		countStatus.transform.parent = countStatusTransform;
		guildCard.transform.parent = guildCardTransform;
		missionList.transform.parent = missionListTransform;
		countStatusRectTransform = countStatus.GetComponent<RectTransform>();
		countStatusRectTransform.anchoredPosition = Vector3.zero;
		countStatusRectTransform.localRotation = Quaternion.identity;
		countStatusRectTransform.localScale = Vector3.one;
		guildCard.transform.localPosition = Vector3.zero;
		guildCard.transform.localRotation = Quaternion.identity;
		guildCard.transform.localScale = new Vector3(100f, 100f, 100f);
		missionListRectTransform = missionList.GetComponent<RectTransform>();
		missionListRectTransform.anchoredPosition = Vector3.zero;
		missionListRectTransform.localRotation = Quaternion.identity;
		missionListRectTransform.localScale = Vector3.one;
		ResultData.instance.SelfDestroy();
	}

	private void FixedUpdate()
	{
	}

	public void GameEnd()
	{
		Application.Quit();
	}

	public void Skip()
	{
		TalkGUI.instance.FroceEndTalk();
		rankFame.SetActive(value: true);
		countStatusFrame.SetActive(value: true);
		guildCardFrame.SetActive(value: true);
		missionListTransform.gameObject.SetActive(value: true);
		_audioSourceSE.PlayOneShot(framePopSe);
		resultEndButton.SetActive(value: true);
		skipButon.SetActive(value: false);
	}

	public void ReturnTitle()
	{
		isFeed = true;
		resultEndButton.SetActive(value: false);
		m_ReturnTitle.Invoke();
	}

	public void ResultEnd()
	{
		resultEndButton.SetActive(value: false);
		m_ResultEnd.Invoke();
	}

	public void ResultEvent(string value)
	{
		switch (value)
		{
		case "StopBGM":
			_BGMManager.StopBGM();
			break;
		case "DisplayCount":
			_audioSourceSE.PlayOneShot(framePopSe);
			countStatusFrame.SetActive(value: true);
			break;
		case "DisplayMission":
			_audioSourceSE.PlayOneShot(framePopSe);
			missionListTransform.gameObject.SetActive(value: true);
			break;
		case "DisplayRank":
			_audioSourceSE.PlayOneShot(framePopSe);
			rankFame.SetActive(value: true);
			break;
		case "DisplayJob":
			_audioSourceSE.PlayOneShot(framePopSe);
			guildCardFrame.SetActive(value: true);
			break;
		case "SetResultTalkParam":
			_resultDataManager.SetResult();
			resultTalk = _resultDataManager.resultTalk;
			isLostVirgin = _resultDataManager.isLostVirgin;
			advEngine.Param.TrySetParameter("ResultVirsin", !isLostVirgin);
			advEngine.Param.TrySetParameter("ResultStatus", resultTalk);
			skipButon.SetActive(value: true);
			break;
		case "SetMissionTalkParam":
			_resultDataManager.SetMissionTalk();
			resultTalk = _resultDataManager.resultTalk;
			advEngine.Param.TrySetParameter("ResultStatus", resultTalk);
			break;
		case "SetRankTalkParam":
			_resultDataManager.SetRankTalk();
			resultTalk = _resultDataManager.resultTalk;
			advEngine.Param.TrySetParameter("ResultStatus", resultTalk);
			break;
		case "SetJobTalkParam":
			_resultDataManager.SetJobTalk();
			resultTalk = _resultDataManager.resultTalk;
			resultJob = _resultDataManager.resultJob;
			advEngine.Param.TrySetParameter("ResultStatus", resultTalk);
			advEngine.Param.TrySetParameter("ResultJob", resultJob);
			break;
		case "ResultEndButton":
			_audioSourceSE.PlayOneShot(framePopSe);
			skipButon.SetActive(value: false);
			resultEndButton.SetActive(value: true);
			break;
		case "ResultEnd":
			ResultEnd();
			break;
		}
	}
}
