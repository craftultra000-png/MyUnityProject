using UnityEngine;

public class WalkPointDataBase : MonoBehaviour
{
	public static WalkPointDataBase instance;

	[Header("Stage Name")]
	public string stageName;

	[Header("Target")]
	public string characterName;

	public string targetName;

	public GameObject targetPoint;

	[Header("Blank Point")]
	public GameObject blankPoint;

	[Header("Start Point")]
	public GameObject startStartPointStella;

	[Space]
	public GameObject startIdlePointStella;

	public GameObject startIdlePointNuisance;

	[Header("Bed Point")]
	public GameObject bedIdlePointStella;

	public GameObject bedIdlePointVacua;

	public GameObject bedIdlePointNuisance;

	[Space]
	public GameObject bedStartPointStella;

	public GameObject bedStartPointNuisance;

	[Space]
	public GameObject bedLookOutPointNuisance;

	public GameObject bedLookInPointNuisance;

	[Header("Bath Point")]
	public GameObject bathIdlePointStella;

	public GameObject bathIdlePointVacua;

	public GameObject bathIdlePointNuisance;

	[Space]
	public GameObject bathStartPointStella;

	public GameObject bathStartPointNuisance;

	[Space]
	public GameObject bathLookOutPointNuisance;

	public GameObject bathLookInPointNuisance;

	[Header("Church Point")]
	public GameObject churchIdlePointStella;

	public GameObject churchIdlePointVacua;

	public GameObject churchIdlePointNuisance;

	[Space]
	public GameObject churchStartPointStella;

	public GameObject churchStartPointNuisance;

	[Space]
	public GameObject churchLookOutPointNuisance;

	public GameObject churchLookInPointNuisance;

	[Header("Road Point")]
	public GameObject roadIdlePointStella;

	public GameObject roadIdlePointVacua;

	public GameObject roadIdlePointNuisance;

	[Space]
	public GameObject roadStartPointStella;

	public GameObject roadStartPointNuisance;

	[Space]
	public GameObject roadLookOutPointNuisance;

	public GameObject roadLookInPointNuisance;

	[Header("Scenario First")]
	public GameObject startFirstPointStella;

	public GameObject startFirstPointVacua;

	public GameObject startFirstPointStella2;

	public GameObject startFirstPointVacua2;

	[Space]
	public GameObject bedFirstTalkPointStella;

	public GameObject bedFirstTalkPointVacua;

	public GameObject bedFirstTalkEndPointStella;

	[Header("Scenario Second")]
	public GameObject startSecondPointStella;

	public GameObject startSecondPointNuisance;

	public GameObject startSecondPointNuisance2;

	[Space]
	public GameObject bedSecondTalkPointStella;

	public GameObject bedSecondTalkPointStella2;

	public GameObject bedSecondTalkPointVacua;

	public GameObject bedSecondTalkEndPointStella;

	[Header("Scenario Third")]
	public GameObject startThirdPointStella;

	public GameObject startThirdPointVacua;

	public GameObject startThirdPointVacua2;

	[Space]
	public GameObject bedThirdTalkPointStella;

	public GameObject bedThirdTalkPointVacua;

	public GameObject bedThirdTalkEndPointStella;

	[Header("Scenario Fourth")]
	public GameObject startFourthPointStella;

	public GameObject startFourthPointNuisance;

	public GameObject startFourthPointNuisance2;

	[Space]
	public GameObject bedFourthTalkPointStella;

	public GameObject bedFourthTalkPointVacua;

	public GameObject bedFourthTalkPointNuisance;

	public GameObject bedFourthTalkPointStella2;

	public GameObject bedFourthTalkPointVacua2;

	public GameObject bedFourthTalkEndPointStella;

	[Header("Scenario End")]
	public GameObject startEndPointStella;

	public GameObject startEndPointNuisance;

	[Space]
	public GameObject startEndPointStella2;

	public GameObject startEndPointNuisance2;

	[Space]
	public GameObject startEndPointNuisance3;

	public void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
	}

	public void SetStageName(string value)
	{
		Debug.LogWarning("WayPoint StageName Set: " + value, base.gameObject);
		stageName = value;
	}

	public void SetTargetPoint(string character, string value)
	{
		characterName = character;
		targetName = value;
		Debug.LogWarning("WayPoint Set: " + characterName + "  Target:" + targetName, base.gameObject);
		if (targetName == "Blank")
		{
			targetPoint = blankPoint;
		}
		else if (targetName == "Start Start")
		{
			if (characterName == "Stella")
			{
				targetPoint = startStartPointStella;
				return;
			}
			Debug.LogWarning("WayPoint Target Miss!");
			Debug.LogError("WayPoint Target Miss!");
		}
		else if (targetName == "Start Idle")
		{
			if (characterName == "Stella")
			{
				targetPoint = startIdlePointStella;
				return;
			}
			if (characterName == "Vacua")
			{
				targetPoint = startIdlePointNuisance;
				return;
			}
			if (characterName == "Nuisance")
			{
				targetPoint = startIdlePointNuisance;
				return;
			}
			Debug.LogWarning("WayPoint Target Miss!");
			Debug.LogError("WayPoint Target Miss!");
		}
		else if (targetName == "Start First")
		{
			if (characterName == "Stella")
			{
				targetPoint = startFirstPointStella;
				return;
			}
			if (characterName == "Vacua")
			{
				targetPoint = startFirstPointVacua;
				return;
			}
			Debug.LogWarning("WayPoint Target Miss!");
			Debug.LogError("WayPoint Target Miss!");
		}
		else if (targetName == "Start First2")
		{
			if (characterName == "Stella")
			{
				targetPoint = startFirstPointStella2;
				return;
			}
			if (characterName == "Vacua")
			{
				targetPoint = startFirstPointVacua2;
				return;
			}
			Debug.LogWarning("WayPoint Target Miss!");
			Debug.LogError("WayPoint Target Miss!");
		}
		else if (targetName == "Start Second")
		{
			if (characterName == "Stella")
			{
				targetPoint = startSecondPointStella;
				return;
			}
			if (characterName == "Nuisance")
			{
				targetPoint = startSecondPointNuisance;
				return;
			}
			Debug.LogWarning("WayPoint Target Miss!");
			Debug.LogError("WayPoint Target Miss!");
		}
		else if (targetName == "Start Second2")
		{
			if (characterName == "Nuisance")
			{
				targetPoint = startSecondPointNuisance2;
				return;
			}
			Debug.LogWarning("WayPoint Target Miss!");
			Debug.LogError("WayPoint Target Miss!");
		}
		else if (targetName == "Start Third")
		{
			if (characterName == "Stella")
			{
				targetPoint = startThirdPointStella;
				return;
			}
			if (characterName == "Vacua")
			{
				targetPoint = startThirdPointVacua;
				return;
			}
			Debug.LogWarning("WayPoint Target Miss!");
			Debug.LogError("WayPoint Target Miss!");
		}
		else if (targetName == "Start Third2")
		{
			if (characterName == "Vacua")
			{
				targetPoint = startThirdPointVacua2;
				return;
			}
			Debug.LogWarning("WayPoint Target Miss!");
			Debug.LogError("WayPoint Target Miss!");
		}
		else if (targetName == "Start Fourth")
		{
			if (characterName == "Stella")
			{
				targetPoint = startFourthPointStella;
				return;
			}
			if (characterName == "Nuisance")
			{
				targetPoint = startFourthPointNuisance;
				return;
			}
			Debug.LogWarning("WayPoint Target Miss!");
			Debug.LogError("WayPoint Target Miss!");
		}
		else if (targetName == "Start Fourth2")
		{
			if (characterName == "Nuisance")
			{
				targetPoint = startFourthPointNuisance2;
				return;
			}
			Debug.LogWarning("WayPoint Target Miss!");
			Debug.LogError("WayPoint Target Miss!");
		}
		else if (targetName == "Start End")
		{
			if (characterName == "Stella")
			{
				targetPoint = startEndPointStella;
				return;
			}
			if (characterName == "Nuisance")
			{
				targetPoint = startEndPointNuisance;
				return;
			}
			Debug.LogWarning("WayPoint Target Miss!");
			Debug.LogError("WayPoint Target Miss!");
		}
		else if (targetName == "Start End2")
		{
			if (characterName == "Stella")
			{
				targetPoint = startEndPointStella2;
				return;
			}
			if (characterName == "Nuisance")
			{
				targetPoint = startEndPointNuisance2;
				return;
			}
			Debug.LogWarning("WayPoint Target Miss!");
			Debug.LogError("WayPoint Target Miss!");
		}
		else if (targetName == "Start End3")
		{
			if (characterName == "Nuisance")
			{
				targetPoint = startEndPointNuisance3;
				return;
			}
			Debug.LogWarning("WayPoint Target Miss!");
			Debug.LogError("WayPoint Target Miss!");
		}
		else if (targetName == "Bed Talk First")
		{
			if (characterName == "Stella")
			{
				targetPoint = bedFirstTalkPointStella;
				return;
			}
			if (characterName == "Vacua")
			{
				targetPoint = bedFirstTalkPointVacua;
				return;
			}
			Debug.LogWarning("WayPoint Target Miss!");
			Debug.LogError("WayPoint Target Miss!");
		}
		else if (targetName == "Bed Talk First End")
		{
			if (characterName == "Stella")
			{
				targetPoint = bedFirstTalkEndPointStella;
				return;
			}
			Debug.LogWarning("WayPoint Target Miss!");
			Debug.LogError("WayPoint Target Miss!");
		}
		else if (targetName == "Bed Talk Second")
		{
			if (characterName == "Stella")
			{
				targetPoint = bedSecondTalkPointStella;
				return;
			}
			if (characterName == "Vacua")
			{
				targetPoint = bedSecondTalkPointVacua;
				return;
			}
			Debug.LogWarning("WayPoint Target Miss!");
			Debug.LogError("WayPoint Target Miss!");
		}
		else if (targetName == "Bed Talk Second2")
		{
			if (characterName == "Stella")
			{
				targetPoint = bedSecondTalkPointStella2;
				return;
			}
			Debug.LogWarning("WayPoint Target Miss!");
			Debug.LogError("WayPoint Target Miss!");
		}
		else if (targetName == "Bed Talk Second End")
		{
			if (characterName == "Stella")
			{
				targetPoint = bedSecondTalkEndPointStella;
				return;
			}
			Debug.LogWarning("WayPoint Target Miss!");
			Debug.LogError("WayPoint Target Miss!");
		}
		else if (targetName == "Bed Talk Third")
		{
			if (characterName == "Stella")
			{
				targetPoint = bedThirdTalkPointStella;
				return;
			}
			if (characterName == "Vacua")
			{
				targetPoint = bedThirdTalkPointVacua;
				return;
			}
			Debug.LogWarning("WayPoint Target Miss!");
			Debug.LogError("WayPoint Target Miss!");
		}
		else if (targetName == "Bed Talk Third End")
		{
			if (characterName == "Stella")
			{
				targetPoint = bedThirdTalkEndPointStella;
				return;
			}
			Debug.LogWarning("WayPoint Target Miss!");
			Debug.LogError("WayPoint Target Miss!");
		}
		else if (targetName == "Bed Talk Fourth")
		{
			if (characterName == "Stella")
			{
				targetPoint = bedFourthTalkPointStella;
				return;
			}
			if (characterName == "Vacua")
			{
				targetPoint = bedFourthTalkPointVacua;
				return;
			}
			if (characterName == "Nuisance")
			{
				targetPoint = bedFourthTalkPointNuisance;
				return;
			}
			Debug.LogWarning("WayPoint Target Miss!");
			Debug.LogError("WayPoint Target Miss!");
		}
		else if (targetName == "Bed Talk Fourth2")
		{
			if (characterName == "Stella")
			{
				targetPoint = bedFourthTalkPointStella2;
				return;
			}
			if (characterName == "Vacua")
			{
				targetPoint = bedFourthTalkPointVacua2;
				return;
			}
			Debug.LogWarning("WayPoint Target Miss!");
			Debug.LogError("WayPoint Target Miss!");
		}
		else if (targetName == "Bed Talk Fourth End")
		{
			if (characterName == "Stella")
			{
				targetPoint = bedFourthTalkEndPointStella;
				return;
			}
			Debug.LogWarning("WayPoint Target Miss!");
			Debug.LogError("WayPoint Target Miss!");
		}
		else if (targetName == "Bed Start")
		{
			if (characterName == "Stella")
			{
				targetPoint = bedStartPointStella;
				return;
			}
			Debug.LogWarning("WayPoint Target Miss!");
			Debug.LogError("WayPoint Target Miss!");
		}
		else if (targetName == "Bed Idle")
		{
			if (characterName == "Stella")
			{
				targetPoint = bedIdlePointStella;
				return;
			}
			if (characterName == "Vacua")
			{
				targetPoint = bedIdlePointVacua;
				return;
			}
			if (characterName == "Nuisance")
			{
				targetPoint = bedIdlePointNuisance;
				return;
			}
			Debug.LogWarning("WayPoint Target Miss!");
			Debug.LogError("WayPoint Target Miss!");
		}
		else if (targetName == "Bath Start")
		{
			if (characterName == "Stella")
			{
				targetPoint = bathStartPointStella;
				return;
			}
			Debug.LogWarning("WayPoint Target Miss!");
			Debug.LogError("WayPoint Target Miss!");
		}
		else if (targetName == "Bath Idle")
		{
			if (characterName == "Stella")
			{
				targetPoint = bathIdlePointStella;
				return;
			}
			if (characterName == "Vacua")
			{
				targetPoint = bathIdlePointVacua;
				return;
			}
			if (characterName == "Nuisance")
			{
				targetPoint = bathIdlePointNuisance;
				return;
			}
			Debug.LogWarning("WayPoint Target Miss!");
			Debug.LogError("WayPoint Target Miss!");
		}
		else if (targetName == "Church Start")
		{
			if (characterName == "Stella")
			{
				targetPoint = churchStartPointStella;
				return;
			}
			if (characterName == "Nuisance")
			{
				targetPoint = churchStartPointNuisance;
				return;
			}
			Debug.LogWarning("WayPoint Target Miss!");
			Debug.LogError("WayPoint Target Miss!");
		}
		else if (targetName == "Church Idle")
		{
			if (characterName == "Stella")
			{
				targetPoint = churchIdlePointStella;
				return;
			}
			if (characterName == "Vacua")
			{
				targetPoint = churchIdlePointVacua;
				return;
			}
			if (characterName == "Nuisance")
			{
				targetPoint = churchIdlePointNuisance;
				return;
			}
			Debug.LogWarning("WayPoint Target Miss!");
			Debug.LogError("WayPoint Target Miss!");
		}
		else if (targetName == "Road Start")
		{
			if (characterName == "Stella")
			{
				targetPoint = roadStartPointStella;
				return;
			}
			if (characterName == "Nuisance")
			{
				targetPoint = roadStartPointNuisance;
				return;
			}
			Debug.LogWarning("WayPoint Target Miss!");
			Debug.LogError("WayPoint Target Miss!");
		}
		else if (targetName == "Road Idle")
		{
			if (characterName == "Stella")
			{
				targetPoint = roadIdlePointStella;
				return;
			}
			if (characterName == "Vacua")
			{
				targetPoint = roadIdlePointVacua;
				return;
			}
			if (characterName == "Nuisance")
			{
				targetPoint = roadIdlePointNuisance;
				return;
			}
			Debug.LogWarning("WayPoint Target Miss!");
			Debug.LogError("WayPoint Target Miss!");
		}
		else if (targetName == "Look Start")
		{
			if (characterName == "Nuisance")
			{
				if (stageName == "Bed")
				{
					targetPoint = bedStartPointNuisance;
					return;
				}
				if (stageName == "Bath")
				{
					targetPoint = bathStartPointNuisance;
					return;
				}
				if (stageName == "Church")
				{
					targetPoint = churchStartPointNuisance;
					return;
				}
				if (stageName == "Road")
				{
					targetPoint = roadStartPointNuisance;
					return;
				}
				Debug.LogWarning("WayPoint Target Miss!");
				Debug.LogError("WayPoint Target Miss!");
			}
			else
			{
				Debug.LogWarning("WayPoint Target Miss!");
				Debug.LogError("WayPoint Target Miss!");
			}
		}
		else if (targetName == "Look Out")
		{
			if (characterName == "Nuisance")
			{
				if (stageName == "Bed")
				{
					targetPoint = bedLookOutPointNuisance;
					return;
				}
				if (stageName == "Bath")
				{
					targetPoint = bathLookOutPointNuisance;
					return;
				}
				if (stageName == "Church")
				{
					targetPoint = churchLookOutPointNuisance;
					return;
				}
				if (stageName == "Road")
				{
					targetPoint = roadLookOutPointNuisance;
					return;
				}
				Debug.LogWarning("WayPoint Target Miss!");
				Debug.LogError("WayPoint Target Miss!");
			}
			else
			{
				Debug.LogWarning("WayPoint Target Miss!");
				Debug.LogError("WayPoint Target Miss!");
			}
		}
		else if (targetName == "Look In")
		{
			if (characterName == "Nuisance")
			{
				if (stageName == "Bed")
				{
					targetPoint = bedLookInPointNuisance;
					return;
				}
				if (stageName == "Bath")
				{
					targetPoint = bathLookInPointNuisance;
					return;
				}
				if (stageName == "Church")
				{
					targetPoint = churchLookInPointNuisance;
					return;
				}
				if (stageName == "Road")
				{
					targetPoint = roadLookInPointNuisance;
					return;
				}
				Debug.LogWarning("WayPoint Target Miss!");
				Debug.LogError("WayPoint Target Miss!");
			}
			else
			{
				Debug.LogWarning("WayPoint Target Miss!");
				Debug.LogError("WayPoint Target Miss!");
			}
		}
		else
		{
			Debug.LogError("WayPoint Target Miss!  WayPoint Set: " + characterName + "  Target: " + targetName, base.gameObject);
		}
	}
}
