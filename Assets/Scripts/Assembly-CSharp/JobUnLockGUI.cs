using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Utage;

public class JobUnLockGUI : MonoBehaviour
{
	[Header("Liquid")]
	public List<bool> liquidUnLock;

	public List<TextMeshProUGUI> liquidText;

	public List<UguiLocalize> liquidKey;

	public List<string> liquidJob;

	[Header("Flesh")]
	public List<bool> fleshUnLock;

	public List<TextMeshProUGUI> fleshText;

	public List<UguiLocalize> fleshKey;

	public List<string> fleshJob;

	[Header("Genitals")]
	public List<bool> genitalsUnLock;

	public List<TextMeshProUGUI> genitalsText;

	public List<UguiLocalize> genitalsKey;

	public List<string> genitalsJob;

	[Header("Calc")]
	public int jobRank;

	private void Start()
	{
		LoadJobList();
		SetJobList();
	}

	public void LoadJobList()
	{
		if (!ES3.KeyExists("UnLockJobLiquid"))
		{
			liquidUnLock[0] = true;
			fleshUnLock[0] = true;
			genitalsUnLock[0] = true;
			ES3.Save("UnLockJobLiquid", liquidUnLock);
			ES3.Save("UnLockJobFlesh", fleshUnLock);
			ES3.Save("UnLockJobGenitals", genitalsUnLock);
		}
		else
		{
			liquidUnLock = ES3.Load<List<bool>>("UnLockJobLiquid");
			fleshUnLock = ES3.Load<List<bool>>("UnLockJobFlesh");
			genitalsUnLock = ES3.Load<List<bool>>("UnLockJobGenitals");
			liquidUnLock[0] = true;
			fleshUnLock[0] = true;
			genitalsUnLock[0] = true;
		}
	}

	public void UnLockJob(int type, int rank)
	{
		jobRank = liquidUnLock.Count - 1 - rank;
		Debug.LogError("Type: " + type + "  Rank: " + rank);
		switch (type)
		{
		case 0:
			if (!liquidUnLock[jobRank])
			{
				liquidUnLock[jobRank] = true;
				ES3.Save("UnLockJobLiquid", liquidUnLock);
				SetJobList();
			}
			break;
		case 1:
			if (!fleshUnLock[jobRank])
			{
				fleshUnLock[jobRank] = true;
				ES3.Save("UnLockJobFlesh", fleshUnLock);
				SetJobList();
			}
			break;
		case 2:
			if (!genitalsUnLock[jobRank])
			{
				genitalsUnLock[jobRank] = true;
				ES3.Save("UnLockJobGenitals", genitalsUnLock);
				SetJobList();
			}
			break;
		}
	}

	public void SetJobList()
	{
		for (int i = 0; i < liquidUnLock.Count; i++)
		{
			if (liquidUnLock[i])
			{
				liquidText[i].text = liquidJob[i];
				liquidKey[i].Key = liquidJob[i];
			}
			else
			{
				liquidText[i].text = "????";
				liquidKey[i].Key = "????";
			}
		}
		for (int j = 0; j < fleshUnLock.Count; j++)
		{
			if (fleshUnLock[j])
			{
				fleshText[j].text = fleshJob[j];
				fleshKey[j].Key = fleshJob[j];
			}
			else
			{
				fleshText[j].text = "????";
				fleshKey[j].Key = "????";
			}
		}
		for (int k = 0; k < genitalsUnLock.Count; k++)
		{
			if (genitalsUnLock[k])
			{
				genitalsText[k].text = genitalsJob[k];
				genitalsKey[k].Key = genitalsJob[k];
			}
			else
			{
				genitalsText[k].text = "????";
				genitalsKey[k].Key = "????";
			}
		}
	}
}
