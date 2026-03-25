using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResultData : MonoBehaviour
{
	public static ResultData instance;

	public CharacterCountGUI _characterCountGUI;

	public GuildCardObject _guildCardObject;

	public MissionGUIManager _missionGUIManager;

	public JobUnLockGUI _jobUnLockGUI;

	[Header("DontDestroy")]
	public GameObject countStatus;

	public GameObject guildCard;

	public GameObject missionList;

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

	[Header("Total")]
	public int liquidCount;

	public int fleshCount;

	public int genitalsCount;

	public int scoreCount;

	public int liquidRank;

	public int fleshRank;

	public int genitalsRank;

	public int scoreRank;

	public List<string> rankName = new List<string> { "S", "A", "B", "C", "D", "E" };

	[Header("Result Support")]
	public int liquidPlusRank;

	public int fleshPlusRank;

	public int genitalsPlusRank;

	public List<bool> missionClearList;

	[Header("JobName")]
	public int currentRank;

	public string currentJob;

	public int resultJob;

	public string[] jobLiquidReversed = new string[6] { "Milk Device", "Milking Tool", "Milk Bottle", "Toy", "Trash", "Thief" };

	public string[] jobFleshReversed = new string[6] { "Flesh Lump", "Bitch", "Doll", "Pet", "Junk", "Thief" };

	public string[] jobGenitalsReversed = new string[6] { "Breeding Unit", "Public Use", "Partner", "Used Hole", "Waste", "Thief" };

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
			Object.DontDestroyOnLoad(base.gameObject);
		}
		else
		{
			Object.Destroy(base.gameObject);
		}
	}

	private void Start()
	{
		currentRank = 5;
		currentJob = "Thief";
		liquidRank = 5;
		fleshRank = 5;
		genitalsRank = 5;
		scoreRank = 5;
	}

	public void GetParent()
	{
		Debug.LogError("Set DontDestroy Parent");
		countStatus.transform.parent = base.transform;
		guildCard.transform.parent = base.transform;
		missionList.transform.parent = base.transform;
	}

	public void GetCountStatus()
	{
		isLostVirgin = _characterCountGUI.isLostVirgin;
		bukkakeCount = _characterCountGUI.bukkakeCount;
		bodyCount = _characterCountGUI.bodyCount;
		headCount = _characterCountGUI.headCount;
		titsCount = _characterCountGUI.titsCount;
		hipCount = _characterCountGUI.hipCount;
		vaginaCount = _characterCountGUI.vaginaCount;
		analCount = _characterCountGUI.analCount;
		vaginaChildCount = _characterCountGUI.vaginaChildCount;
		analChildCount = _characterCountGUI.analChildCount;
		vaginaChildStackCount = _characterCountGUI.vaginaChildStackCount;
		analChildStackCount = _characterCountGUI.analChildStackCount;
		heartCount = (int)_characterCountGUI.heartCount;
		titsMilkCount = (int)_characterCountGUI.titsMilkCount;
		orgasmCount = _characterCountGUI.orgasmCount;
	}

	public void SelfDestroy()
	{
		Object.Destroy(base.gameObject);
	}

	public void SetRank()
	{
		int[] source = new int[2] { bukkakeCount, titsMilkCount };
		int[] source2 = new int[4] { bodyCount, headCount, titsCount, hipCount };
		int[] source3 = new int[4]
		{
			vaginaCount,
			analCount,
			vaginaChildCount + analChildCount * 5,
			orgasmCount * 50
		};
		liquidCount = source.Sum();
		fleshCount = source2.Sum();
		genitalsCount = source3.Sum();
		liquidPlusRank = 0;
		fleshPlusRank = 0;
		genitalsPlusRank = 0;
		missionClearList = _missionGUIManager.missionClearList;
		if (missionClearList.Count > 0)
		{
			for (int i = 0; i < 3; i++)
			{
				if (missionClearList[i])
				{
					if (_missionGUIManager.missionPlusType[i] == 0)
					{
						liquidPlusRank++;
					}
					if (_missionGUIManager.missionPlusType[i] == 1)
					{
						fleshPlusRank++;
					}
					if (_missionGUIManager.missionPlusType[i] == 2)
					{
						genitalsPlusRank++;
					}
				}
			}
		}
		liquidCount += liquidPlusRank * 100;
		fleshCount += fleshPlusRank * 100;
		genitalsCount += genitalsPlusRank * 100;
		scoreCount = liquidCount + fleshCount + genitalsCount;
		if (liquidCount > 1000)
		{
			liquidRank = 0;
		}
		else if (liquidCount > 750)
		{
			liquidRank = 1;
		}
		else if (liquidCount > 500)
		{
			liquidRank = 2;
		}
		else if (liquidCount > 250)
		{
			liquidRank = 3;
		}
		else if (liquidCount > 100)
		{
			liquidRank = 4;
		}
		else
		{
			liquidRank = 5;
		}
		if (fleshCount > 1000)
		{
			fleshRank = 0;
		}
		else if (fleshCount > 750)
		{
			fleshRank = 1;
		}
		else if (fleshCount > 500)
		{
			fleshRank = 2;
		}
		else if (fleshCount > 250)
		{
			fleshRank = 3;
		}
		else if (fleshCount > 100)
		{
			fleshRank = 4;
		}
		else
		{
			fleshRank = 5;
		}
		if (genitalsCount > 1000)
		{
			genitalsRank = 0;
		}
		else if (genitalsCount > 750)
		{
			genitalsRank = 1;
		}
		else if (genitalsCount > 500)
		{
			genitalsRank = 2;
		}
		else if (genitalsCount > 250)
		{
			genitalsRank = 3;
		}
		else if (genitalsCount > 100)
		{
			genitalsRank = 4;
		}
		else
		{
			genitalsRank = 5;
		}
		if (scoreCount > 3000)
		{
			scoreRank = 0;
		}
		else if (scoreCount > 2000)
		{
			scoreRank = 1;
		}
		else if (scoreCount > 1500)
		{
			scoreRank = 2;
		}
		else if (scoreCount > 1000)
		{
			scoreRank = 3;
		}
		else if (scoreCount > 500)
		{
			scoreRank = 4;
		}
		else
		{
			scoreRank = 5;
		}
		_guildCardObject.SetTotal();
	}

	public void SetJob()
	{
		if (currentRank != scoreRank)
		{
			currentRank = scoreRank;
			if (liquidCount >= fleshCount && liquidCount >= genitalsCount)
			{
				currentJob = jobLiquidReversed[currentRank];
				resultJob = 0;
			}
			else if (fleshCount >= liquidCount && fleshCount >= genitalsCount)
			{
				currentJob = jobFleshReversed[currentRank];
				resultJob = 1;
			}
			else if (genitalsCount >= fleshCount && genitalsCount >= liquidCount)
			{
				currentJob = jobGenitalsReversed[currentRank];
				resultJob = 2;
			}
			_guildCardObject.SetRank(currentRank, currentJob);
			_jobUnLockGUI.UnLockJob(resultJob, currentRank);
		}
	}
}
