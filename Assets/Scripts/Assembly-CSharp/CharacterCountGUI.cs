using TMPro;
using UnityEngine;
using Utage;

public class CharacterCountGUI : MonoBehaviour
{
	public CharacterLifeManager _characterLifeManager;

	public CharacterEffectManager _characterEffectManager;

	public UterusChildFeelerManager _uterusChildFeelerManager;

	public AnalChildFeelerManager _analChildFeelerManager;

	public CharacterCostumeSwitch _characterCostumeSwitch;

	public ResultData _resultData;

	[Header("Status")]
	public bool isFinishStage;

	public bool isChange;

	[Header("Virgin")]
	public bool isLostVirgin;

	public UguiLocalize virginText;

	[Header("Hit Count")]
	public int bukkakeCount;

	public int bodyCount;

	public int headCount;

	public int titsCount;

	public int hipCount;

	public int vaginaCount;

	public int analCount;

	private int bukkakeCountCurrent;

	private int bodyCountCurrent;

	private int headCountCurrent;

	private int titsCountCurrent;

	private int hipCountCurrent;

	private int vaginaCountCurrent;

	private int analCountCurrent;

	[Header("Costume Count")]
	public int costume;

	public int wearCount;

	public int nakedCount;

	[Header("Mission Count")]
	public int spankingCount;

	public int syringeCount;

	public int touchCount;

	public int pistonCount;

	public int peeCount;

	public int playerTitsCount;

	public int playerHipCount;

	private int costumeCountCount;

	private int spankingCountCount;

	private int syringeCountCount;

	private int touchCountCount;

	private int pistonCountCount;

	private int peeCountCount;

	private int playerTitsCountCount;

	private int playerHipCountCount;

	[Header("Child Count")]
	public int vaginaChildCount;

	public int analChildCount;

	public int vaginaChildStackCount;

	public int analChildStackCount;

	private int vaginaChildCountCurrent;

	private int analChildCountCurrent;

	private int vaginaChildStackCountCurrent;

	private int analChildStackCountCurrent;

	[Header("Other Count")]
	public float heartCount;

	public float titsMilkCount;

	public int orgasmCount;

	private float heartCountCurrent;

	private float titsMilkCountCurrent;

	private float orgasmCountCurrent;

	[Header("Count Text")]
	public TextMeshProUGUI bukkakeText;

	public TextMeshProUGUI bodyText;

	public TextMeshProUGUI headText;

	public TextMeshProUGUI titsText;

	public TextMeshProUGUI hipText;

	public TextMeshProUGUI vaginaText;

	public TextMeshProUGUI analText;

	public TextMeshProUGUI childText;

	public TextMeshProUGUI milkText;

	public TextMeshProUGUI orgasmText;

	private void Start()
	{
		if (!isLostVirgin && _characterEffectManager.isLostVirsin)
		{
			isLostVirgin = true;
			virginText.Key = "Not a Virgin";
		}
		else
		{
			virginText.Key = "Virgin";
		}
		bukkakeText.text = bukkakeCount.ToString();
		bodyText.text = bodyCount.ToString();
		headText.text = headCount.ToString();
		titsText.text = titsCount.ToString();
		hipText.text = hipCount.ToString();
		vaginaText.text = vaginaCount.ToString();
		analText.text = analCount.ToString();
		childText.text = (vaginaChildCount + analChildCount).ToString();
		milkText.text = titsMilkCount.ToString();
		orgasmText.text = orgasmCount.ToString();
	}

	private void LateUpdate()
	{
		if (!isFinishStage)
		{
			if (!isLostVirgin && _characterEffectManager.isLostVirsin)
			{
				isLostVirgin = true;
				virginText.Key = "Not a Virgin";
			}
			bukkakeCount = _characterLifeManager.bukkakeCount;
			bodyCount = _characterLifeManager.bodyCount;
			headCount = _characterLifeManager.headCount;
			titsCount = _characterLifeManager.titsCount;
			hipCount = _characterLifeManager.hipCount;
			vaginaCount = _characterLifeManager.vaginaCount;
			analCount = _characterLifeManager.analCount;
			vaginaChildCount = _characterLifeManager.vaginaChildCount;
			analChildCount = _characterLifeManager.analChildCount;
			vaginaChildStackCount = _uterusChildFeelerManager.childScriptList.Count;
			analChildStackCount = _analChildFeelerManager.childScriptList.Count;
			if (vaginaChildStackCount > _uterusChildFeelerManager.maxSpawn)
			{
				vaginaChildStackCount = _uterusChildFeelerManager.maxSpawn;
			}
			if (analChildStackCount > _analChildFeelerManager.maxSpawn)
			{
				analChildStackCount = _analChildFeelerManager.maxSpawn;
			}
			heartCount = _characterLifeManager.heartCount;
			titsMilkCount = _characterEffectManager.milkCount;
			orgasmCount = _characterLifeManager.orgasmCount;
			wearCount = _characterCostumeSwitch._characterCostumeManager[costume].wearCount;
			nakedCount = _characterCostumeSwitch._characterCostumeManager[costume].nakedCount;
			spankingCount = _characterLifeManager.spankingCount;
			syringeCount = _characterLifeManager.syringeCount;
			touchCount = _characterLifeManager.touchCount;
			pistonCount = _characterLifeManager.pistonCount;
			pistonCount = _characterLifeManager.pistonCount;
			peeCount = _characterLifeManager.peeCount;
			playerTitsCount = _characterLifeManager.playerTitsCount;
			playerHipCount = _characterLifeManager.playerHipCount;
			isChange = false;
			if (bukkakeCount != bukkakeCountCurrent)
			{
				isChange = true;
				bukkakeText.text = Mathf.Min(bukkakeCount, 99999).ToString();
				bukkakeCountCurrent = bukkakeCount;
			}
			if (bodyCount != bodyCountCurrent)
			{
				isChange = true;
				bodyText.text = Mathf.Min(bodyCount, 99999).ToString();
				bodyCountCurrent = bodyCount;
			}
			if (headCount != headCountCurrent)
			{
				isChange = true;
				headText.text = Mathf.Min(headCount, 99999).ToString();
				headCountCurrent = headCount;
			}
			if (titsCount != titsCountCurrent)
			{
				isChange = true;
				titsText.text = Mathf.Min(titsCount, 99999).ToString();
				titsCountCurrent = titsCount;
			}
			if (hipCount != hipCountCurrent)
			{
				isChange = true;
				hipText.text = Mathf.Min(hipCount, 99999).ToString();
				hipCountCurrent = hipCount;
			}
			if (vaginaCount != vaginaCountCurrent)
			{
				isChange = true;
				vaginaText.text = Mathf.Min(vaginaCount, 99999).ToString();
				vaginaCountCurrent = vaginaCount;
			}
			if (analCount != analCountCurrent)
			{
				isChange = true;
				analText.text = analCount.ToString();
				analCountCurrent = analCount;
			}
			if (vaginaChildCount != vaginaChildCountCurrent)
			{
				isChange = true;
				childText.text = Mathf.Min(vaginaChildCount + analChildCount, 99999).ToString();
				vaginaChildCountCurrent = vaginaChildCount;
			}
			if (analChildCount != analChildCountCurrent)
			{
				isChange = true;
				childText.text = Mathf.Min(vaginaChildCount + analChildCount, 99999).ToString();
				analChildCountCurrent = analChildCount;
			}
			if (titsMilkCount != titsMilkCountCurrent)
			{
				isChange = true;
				milkText.text = Mathf.Min(titsMilkCount, 99999f).ToString("F0");
				titsMilkCountCurrent = titsMilkCount;
			}
			if ((float)orgasmCount != orgasmCountCurrent)
			{
				isChange = true;
				orgasmText.text = Mathf.Min(orgasmCount, 99999).ToString();
				orgasmCountCurrent = orgasmCount;
			}
			if (isChange)
			{
				_resultData.GetCountStatus();
				_resultData.SetRank();
				_resultData.SetJob();
			}
		}
	}
}
