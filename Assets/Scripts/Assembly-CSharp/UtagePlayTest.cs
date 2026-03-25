using System;
using System.Collections;
using UnityEngine;
using Utage;

public class UtagePlayTest : MonoBehaviour
{
	public bool isPlay;

	public string talkScenario = "Coo";

	public string characterName = "Vein";

	[SerializeField]
	protected AdvEngine advEngine;

	private float defaultSpeed = -1f;

	public AdvEngine AdvEngine => advEngine;

	public bool IsPlaying { get; private set; }

	private void Start()
	{
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.L))
		{
			advEngine.Param.TrySetParameter("BarkName", characterName);
			JumpScenario("Coo");
			isPlay = true;
		}
	}

	public void HitBark()
	{
		advEngine.Param.TrySetParameter("BarkName", characterName);
		JumpScenario("Coo");
		isPlay = true;
	}

	public void JumpScenario(string label)
	{
		StartCoroutine(JumpScenarioAsync(label, null));
	}

	public void JumpScenario(string label, Action onComplete)
	{
		StartCoroutine(JumpScenarioAsync(label, onComplete));
	}

	private IEnumerator JumpScenarioAsync(string label, Action onComplete)
	{
		IsPlaying = true;
		AdvEngine.JumpScenario(label);
		while (!AdvEngine.IsEndOrPauseScenario)
		{
			yield return null;
		}
		IsPlaying = false;
		onComplete?.Invoke();
	}

	public void JumpScenario(string label, Action onComplete, Action onFailed)
	{
		JumpScenario(label, null, onComplete, onFailed);
	}

	public void JumpScenario(string label, Action onStart, Action onComplete, Action onFailed)
	{
		if (string.IsNullOrEmpty(label))
		{
			onFailed?.Invoke();
			Debug.LogErrorFormat("シナリオラベルが空です");
			return;
		}
		if (label[0] == '*')
		{
			label = label.Substring(1);
		}
		if (AdvEngine.DataManager.FindScenarioData(label) == null)
		{
			onFailed?.Invoke();
			Debug.LogErrorFormat("{0}はまだロードされていないか、存在しないシナリオです", label);
		}
		else
		{
			onStart?.Invoke();
			JumpScenario(label, onComplete);
		}
	}

	public void ChangeMessageSpeed(float speed)
	{
		if (defaultSpeed < 0f)
		{
			defaultSpeed = AdvEngine.Config.MessageSpeed;
		}
		AdvEngine.Config.MessageSpeed = speed;
	}

	public void ResetMessageSpeed()
	{
		if (defaultSpeed >= 0f)
		{
			AdvEngine.Config.MessageSpeed = defaultSpeed;
		}
	}
}
