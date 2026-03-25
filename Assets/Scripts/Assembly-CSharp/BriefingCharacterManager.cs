using UnityEngine;

public class BriefingCharacterManager : MonoBehaviour
{
	public CharacterTalkManager _characterTalkManager;

	[Header("Status")]
	public bool isGamePlay;

	public void Talk(string type)
	{
		if (!isGamePlay)
		{
			_characterTalkManager.SetForceTalk(type);
		}
	}

	public void BarkTalk(string type)
	{
		if (!isGamePlay)
		{
			_characterTalkManager.SetBarkTalk(type);
		}
	}

	public void TalkCount(string type)
	{
		if (!isGamePlay)
		{
			_characterTalkManager.SetCountTalk(type);
		}
	}
}
