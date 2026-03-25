using UnityEngine;

public class WalkPointStageData : MonoBehaviour
{
	public BattleStageManager _battleStageManager;

	public SystemCore _systemCore;

	[Header("Set Data")]
	public string startPointName;

	public WalkPointData currentData;

	[Header("Point Data")]
	public WalkPointData dataFirst;

	public WalkPointData dataSecond;

	public WalkPointData dataThird;

	public WalkPointData dataFourth;

	public WalkPointData dataEnding;

	public WalkPointData data0;

	public WalkPointData data1;

	public WalkPointData data1Skip;

	public WalkPointData data2Skip;

	public WalkPointData data3Skip;

	public WalkPointData data4Skip;

	public void LoadPoint()
	{
		if (ES3.KeyExists("StageStartPoint"))
		{
			startPointName = ES3.Load<string>("StageStartPoint");
		}
		else
		{
			startPointName = "Chapter1";
			ES3.Save("StageStartPoint", startPointName);
		}
		Debug.LogWarning("LoadPoint Name :" + startPointName, base.gameObject);
		Debug.LogError("LoadPoint Name :" + startPointName, base.gameObject);
		if (startPointName == "Chapter1")
		{
			currentData = dataFirst;
			if (_battleStageManager != null)
			{
				_battleStageManager.SetStage("Start");
			}
		}
		else if (startPointName == "Chapter2")
		{
			currentData = dataSecond;
			if (_battleStageManager != null)
			{
				_battleStageManager.SetStage("Start");
			}
		}
		else if (startPointName == "Chapter3")
		{
			currentData = dataThird;
			if (_battleStageManager != null)
			{
				_battleStageManager.SetStage("Start");
			}
		}
		else if (startPointName == "Chapter4")
		{
			currentData = dataFourth;
			if (_battleStageManager != null)
			{
				_battleStageManager.SetStage("Start");
			}
		}
		else if (startPointName == "Ending")
		{
			currentData = dataEnding;
			if (_battleStageManager != null)
			{
				_battleStageManager.SetStage("End");
			}
		}
	}
}
