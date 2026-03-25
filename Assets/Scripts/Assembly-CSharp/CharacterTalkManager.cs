using UnityEngine;
using Utage;

public class CharacterTalkManager : MonoBehaviour
{
	public CharacterFaceManager _characterFaceManager;

	public CharacterMouthManager _characterMouthManager;

	public CharacterLifeManager _characterLifeManager;

	[SerializeField]
	protected AdvEngine advEngine;

	[Header("CharacterName")]
	public string characterName = "Vein";

	[Header("Text")]
	public string talk;

	[Header("Paramater")]
	public bool mouthGag;

	public bool gimmickGag;

	public bool squeeze;

	public bool overHeat;

	public bool sleep;

	[Header("Bark")]
	public int barkCurrent;

	public int barkMin = 3;

	public int barkMax = 8;

	public AdvEngine AdvEngine => advEngine;

	private void Start()
	{
		SetBark();
		Debug.LogError("Utage Paramater Reset");
		advEngine.Param.TrySetParameter("MouthGag", false);
		advEngine.Param.TrySetParameter("Squeeze", false);
		advEngine.Param.TrySetParameter("OverHeat", false);
		advEngine.Param.TrySetParameter("Sleep", false);
		advEngine.Param.TrySetParameter("BarkName", characterName);
	}

	public void SetBark()
	{
		barkCurrent = Random.Range(barkMin, barkMax);
	}

	public void SetForceTalk(string type)
	{
		if (type != "")
		{
			Debug.LogError("ForceTalk :" + type + "  MouthGag : " + mouthGag + " GimmickGag" + gimmickGag, base.gameObject);
			talk = type;
			if (mouthGag || gimmickGag)
			{
				advEngine.Param.TrySetParameter("MouthGag", true);
			}
			else
			{
				advEngine.Param.TrySetParameter("MouthGag", false);
			}
			advEngine.Param.TrySetParameter("Squeeze", squeeze);
			advEngine.Param.TrySetParameter("OverHeat", overHeat);
			advEngine.Param.TrySetParameter("Sleep", sleep);
			advEngine.Param.TrySetParameter("BarkName", characterName);
			TalkGUI.instance.SpownTalk(talk, force: true);
		}
	}

	public void SetBarkTalk(string type)
	{
		Debug.LogError("BarkTalk :" + type + "  MouthGag : " + mouthGag + " GimmickGag" + gimmickGag, base.gameObject);
		if (TalkGUI.instance.talkScript == null && type != "")
		{
			Debug.LogError("BarkTalk :" + type);
			SetBark();
			talk = type;
			SpawnCountTalk();
			_characterMouthManager.PlayHitSe();
		}
	}

	public void SetCountTalk(string type)
	{
		if (TalkGUI.instance.talkScript == null && type != "")
		{
			if (barkCurrent > 0)
			{
				barkCurrent--;
				return;
			}
			SetBark();
			talk = type;
			SpawnCountTalk();
			_characterMouthManager.PlayHitSe();
		}
	}

	public void SpawnCountTalk()
	{
		if (mouthGag || gimmickGag)
		{
			advEngine.Param.TrySetParameter("MouthGag", true);
		}
		else
		{
			advEngine.Param.TrySetParameter("MouthGag", false);
		}
		advEngine.Param.TrySetParameter("Squeeze", squeeze);
		advEngine.Param.TrySetParameter("OverHeat", overHeat);
		advEngine.Param.TrySetParameter("Sleep", sleep);
		advEngine.Param.TrySetParameter("BarkName", characterName);
		TalkGUI.instance.SpownTalk(talk, force: false);
	}
}
