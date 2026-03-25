using System.Collections;
using UnityEngine;
using Utage;

public class TalkGUI : MonoBehaviour
{
	public static TalkGUI instance;

	public GameObject talkWindow;

	[Header("Status")]
	public bool isEnd;

	public bool isReverse;

	public bool isSystem;

	[Header("Utage")]
	public AdvUguiManager uguiManager;

	public AdvEngine engine;

	public string scenarioLabel;

	public TalkObject talkScript;

	public TalkWait talkWait;

	[Header("Audio")]
	public AudioSource _audioSource;

	public AudioClip talkSe;

	public void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
	}

	private void Start()
	{
	}

	public void ScenarioStart(string value)
	{
		scenarioLabel = value;
		StartCoroutine(CoTalk());
	}

	private IEnumerator CoTalk()
	{
		Debug.LogWarning("Talk On Scenario:" + scenarioLabel, base.gameObject);
		engine.EndScenario();
		engine.JumpScenario(scenarioLabel);
		while (!engine.IsEndScenario)
		{
			yield return null;
		}
	}

	public void SetReverseParam(bool value)
	{
		isReverse = value;
	}

	public void SetUtageParamTalk(bool atk)
	{
		Debug.LogWarning("UtageParam Talk atk: " + atk + "  isReverse: " + isReverse, base.gameObject);
		if ((!atk && !isReverse) || (atk && isReverse))
		{
			engine.Param.SetParameterBoolean("TalkR_False", value: true);
			engine.Param.SetParameterBoolean("TalkR_True", value: false);
		}
		else
		{
			engine.Param.SetParameterBoolean("TalkR_False", value: false);
			engine.Param.SetParameterBoolean("TalkR_True", value: true);
		}
	}

	public void SetUtageParamBark(bool atk)
	{
		Debug.LogWarning("UtageParam Bark atk: " + atk + "  isReverse: " + isReverse, base.gameObject);
		if ((!atk && !isReverse) || (atk && isReverse))
		{
			engine.Param.SetParameterBoolean("BarkR_False", value: true);
			engine.Param.SetParameterBoolean("BarkR_True", value: false);
		}
		else
		{
			engine.Param.SetParameterBoolean("BarkR_False", value: false);
			engine.Param.SetParameterBoolean("BarkR_True", value: true);
		}
	}

	public void TalkEnd()
	{
		isEnd = true;
	}

	public void TalkSe()
	{
		_audioSource.PlayOneShot(talkSe);
	}

	public void SpownTalk(string value, bool force)
	{
		if (base.gameObject.activeInHierarchy && !isSystem)
		{
			Debug.LogError("Spawn Talk: " + engine.IsEndScenario + " Force: " + force);
			Debug.LogWarning("<color=#000099>Talk On: </color>" + value, base.gameObject);
			if (force)
			{
				isEnd = true;
			}
			else
			{
				isEnd = engine.IsEndScenario;
			}
			if (isEnd)
			{
				scenarioLabel = value;
				StartCoroutine(CoTalk());
			}
		}
	}

	public void SystemTalk(string value)
	{
		isSystem = true;
		Debug.LogError("System Talk: " + engine.IsEndScenario);
		Debug.LogWarning("<color=#000099>Talk On: </color>" + value, base.gameObject);
		scenarioLabel = value;
		StartCoroutine(CoTalk());
	}

	public void FroceEndTalk()
	{
		engine.EndScenario();
		talkWait.isStartText = false;
		talkWait.isEndText = true;
	}
}
