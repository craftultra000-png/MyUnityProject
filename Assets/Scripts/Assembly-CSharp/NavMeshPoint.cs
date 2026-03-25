using System.Collections.Generic;
using UnityEngine;

public class NavMeshPoint : MonoBehaviour
{
	[Header("Use Scenario")]
	public bool useScenario;

	public string setScenarioString;

	public TalkGUI _talkGUI;

	[Header("Random Walk")]
	public bool useRandomPoint;

	public float waitTime = 10f;

	public GameObject nextPoint;

	public List<GameObject> randomPoint;

	public void Goal(CharacterWalkManager script)
	{
		if (useScenario)
		{
			Debug.LogWarning("Set Scenario Point:" + setScenarioString);
			_talkGUI = TalkGUI.instance;
			_talkGUI.ScenarioStart(setScenarioString);
		}
		if (useRandomPoint)
		{
			nextPoint = randomPoint[Random.Range(0, randomPoint.Count)];
			script.MoveWait(nextPoint, waitTime);
		}
	}
}
