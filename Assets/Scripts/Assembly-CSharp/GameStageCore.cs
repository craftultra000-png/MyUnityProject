using UnityEngine;
using UnityEngine.Events;

public class GameStageCore : MonoBehaviour
{
	public static GameStageCore instance;

	public SystemCore _systemCore;

	public WorldLightManager _worldLightManager;

	public FeelerController _feelerController;

	public BGMManager _BGMManager;

	public ResultData _resultData;

	public CharacterCountGUI _characterCountGUI;

	[Header("Magic Effect")]
	public Transform magicStocker;

	public GameObject magicDesignEffect;

	public GameObject magicDesignEndEffect;

	public GameObject magicDesignStartEffect;

	public Vector3 magicRotation = new Vector3(-90f, 0f, 0f);

	[Header("SE")]
	public AudioSource _audioSourceSE;

	public AudioClip magicStartSe;

	[Header("On Enable Event")]
	[SerializeField]
	private UnityEvent m_StageEnd = new UnityEvent();

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
	}

	private void Start()
	{
		magicDesignStartEffect.SetActive(value: false);
	}

	public void StageEnd()
	{
		_worldLightManager.SetResultColor();
		m_StageEnd.Invoke();
	}

	public void FinishStage()
	{
		Debug.LogError("Finish Stage");
		_systemCore.isStageEnd = true;
		_systemCore.FinishButton();
		_feelerController.isStageEnd = true;
		_feelerController.StageEnd();
		_characterCountGUI.isFinishStage = true;
		_resultData.GetCountStatus();
		TalkGUI.instance.FroceEndTalk();
		TalkGUI.instance.SystemTalk("StageEnd");
		Debug.LogError("Save StageScene Name: GameScene");
		ES3.Save("PreviousSceneName", "GameScene");
	}

	public void StageEvent(string value)
	{
		switch (value)
		{
		case "StopBGM":
			_BGMManager.StopBGM();
			break;
		case "MagicField":
			magicDesignEffect.SetActive(value: true);
			break;
		case "MagicStart":
			_audioSourceSE.PlayOneShot(magicStartSe);
			magicDesignStartEffect.SetActive(value: true);
			break;
		case "StageEnd":
			StageEnd();
			break;
		}
	}
}
