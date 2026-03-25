using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultDataManager : MonoBehaviour
{
	public ResultData _resultData;

	[Header("ResultTalk")]
	public int resultTalk;

	public int resultTalk2;

	public int resultTalk3;

	public int resultMission;

	[Header("Total")]
	public int liquidRank;

	public int fleshRank;

	public int genitalsRank;

	public int scoreRank;

	public int resultJob;

	public List<string> rankName = new List<string> { "S", "A", "B", "C", "D", "E" };

	public TextMeshProUGUI liquidText;

	public TextMeshProUGUI fleshText;

	public TextMeshProUGUI genitalsText;

	public TextMeshProUGUI scoreText;

	[Header("Virgin")]
	public bool isLostVirgin;

	[Header("Hit Count")]
	public int bukkakeCount;

	public int bodyCount;

	public int headCount;

	public int titsCount;

	public int hipCount;

	public int vaginaCount;

	public int analCount;

	[Header("Child Count")]
	public int vaginaChildCount;

	public int analChildCount;

	public int vaginaChildStackCount;

	public int analChildStackCount;

	[Header("Other Count")]
	public int heartCount;

	public int titsMilkCount;

	public int orgasmCount;

	[Header("Mission")]
	public List<bool> missionClearList;

	public void GetCountStatus()
	{
		_resultData = ResultData.instance;
		isLostVirgin = _resultData.isLostVirgin;
		bukkakeCount = _resultData.bukkakeCount;
		bodyCount = _resultData.bodyCount;
		headCount = _resultData.headCount;
		titsCount = _resultData.titsCount;
		hipCount = _resultData.hipCount;
		vaginaCount = _resultData.vaginaCount;
		analCount = _resultData.analCount;
		vaginaChildCount = _resultData.vaginaChildCount;
		analChildCount = _resultData.analChildCount;
		vaginaChildStackCount = _resultData.vaginaChildStackCount;
		analChildStackCount = _resultData.analChildStackCount;
		heartCount = _resultData.heartCount;
		titsMilkCount = _resultData.titsMilkCount;
		orgasmCount = _resultData.orgasmCount;
		missionClearList = _resultData.missionClearList;
		SetResult();
	}

	public void SetResult()
	{
		liquidRank = _resultData.liquidRank;
		fleshRank = _resultData.fleshRank;
		genitalsRank = _resultData.genitalsRank;
		scoreRank = _resultData.scoreRank;
		resultJob = _resultData.resultJob;
		liquidText.text = rankName[liquidRank];
		fleshText.text = rankName[fleshRank];
		genitalsText.text = rankName[genitalsRank];
		scoreText.text = rankName[scoreRank];
		SetResultTalk();
	}

	public void SetResultTalk()
	{
		int num = 0;
		int num2 = 0;
		int[] array = new int[10]
		{
			bukkakeCount,
			bodyCount,
			headCount,
			titsCount,
			hipCount,
			vaginaCount,
			analCount,
			(vaginaChildCount + analChildCount) * 5,
			titsMilkCount,
			orgasmCount * 50
		};
		_ = new string[10] { "bukkake", "body", "head", "tits", "hip", "vagina", "anal", "child", "milk", "orgasm" };
		for (int i = 0; i < array.Length; i++)
		{
			if (num2 < array[i])
			{
				num = i;
				num2 = array[i];
			}
		}
		if (num2 < 50)
		{
			num = -1;
		}
		resultTalk = num;
	}

	public void SetMissionTalk()
	{
		resultMission = 0;
		for (int i = 0; i < missionClearList.Count; i++)
		{
			if (missionClearList[i])
			{
				resultMission++;
			}
		}
		resultTalk = resultMission;
	}

	public void SetRankTalk()
	{
		resultTalk = scoreRank;
	}

	public void SetJobTalk()
	{
		resultTalk = scoreRank;
	}
}
