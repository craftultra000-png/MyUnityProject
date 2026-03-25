using System.Collections.Generic;
using UnityEngine;

public class ActionCore : MonoBehaviour
{
	public static ActionCore instance;

	public ActionManager _actionManager;

	public ActionAnimData _actionAnimData;

	[Header("Character")]
	public List<CharacterAnimancerManager> _characterAnimancerManager;

	public List<CharacterFaceManager> _characterFaceManager;

	public List<CharacterMouthManager> _characterMouthManager;

	[Header("Set Data")]
	public string isAction = "isAction";

	public string isActionToIdle = "isActionToIdle";

	public string characterName;

	public string actionName;

	public void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
	}

	public void BattleStart(string value, bool look)
	{
	}

	public void AnimationSet(string character, string action)
	{
		Debug.Log("Animation Set :" + character + " Action: " + action, base.gameObject);
		characterName = character;
		actionName = action;
		_actionAnimData.AnimationSet(characterName, actionName);
		if (actionName != "Action")
		{
			if (characterName == "Vein")
			{
				_characterAnimancerManager[0].actionClip = _actionAnimData.targetClip;
				if (_actionAnimData.targetIdleClip != null)
				{
					_characterAnimancerManager[0].idleClip = _actionAnimData.targetIdleClip;
					_characterAnimancerManager[0].StateSet(isActionToIdle, 0.25f, playWait: false);
				}
				else
				{
					_characterAnimancerManager[0].StateSet(isAction, 0.25f, playWait: false);
				}
			}
			else if (characterName == "Vacua")
			{
				_characterAnimancerManager[1].actionClip = _actionAnimData.targetClip;
				if (_actionAnimData.targetIdleClip != null)
				{
					_characterAnimancerManager[1].idleClip = _actionAnimData.targetIdleClip;
					_characterAnimancerManager[1].StateSet(isActionToIdle, 0.25f, playWait: false);
				}
				else
				{
					_characterAnimancerManager[1].StateSet(isAction, 0.25f, playWait: false);
				}
			}
			else if (characterName == "Nuisance")
			{
				_characterAnimancerManager[2].actionClip = _actionAnimData.targetClip;
				if (_actionAnimData.targetIdleClip != null)
				{
					_characterAnimancerManager[2].idleClip = _actionAnimData.targetIdleClip;
					_characterAnimancerManager[2].StateSet(isActionToIdle, 0.25f, playWait: false);
				}
				else
				{
					_characterAnimancerManager[2].StateSet(isAction, 0.25f, playWait: false);
				}
			}
		}
		else if (characterName == "Vein")
		{
			_characterAnimancerManager[0].StateSet(isAction, 0.25f, playWait: false);
		}
		else if (characterName == "Vacua")
		{
			_characterAnimancerManager[1].StateSet(isAction, 0.25f, playWait: false);
		}
		else if (characterName == "Nuisance")
		{
			_characterAnimancerManager[2].StateSet(isAction, 0.25f, playWait: false);
		}
	}

	public void SetAIUEO(string character, int value)
	{
		Debug.Log("AIUEO Set :" + character + " AIUEO Count: " + value, base.gameObject);
		characterName = character;
		if (characterName == "Vein")
		{
			_characterMouthManager[0].SetAIUEO(value);
		}
		else if (characterName == "Vacua")
		{
			_characterMouthManager[1].SetAIUEO(value);
		}
		else if (characterName == "Nuisance")
		{
			_characterMouthManager[2].SetAIUEO(value);
		}
	}

	public void SetFacial(string character, string value)
	{
		Debug.Log("Facial Set :" + character + " Facial: " + value, base.gameObject);
		characterName = character;
		if (characterName == "Vein")
		{
			_characterFaceManager[0].SetFacial(value);
		}
		else if (characterName == "Vacua")
		{
			_characterFaceManager[1].SetFacial(value);
		}
		else if (characterName == "Nuisance")
		{
			_characterFaceManager[2].SetFacial(value);
		}
	}

	public void SetVoice(string character, string value)
	{
		Debug.Log("Voice Set :" + character + " Voice: " + value, base.gameObject);
		characterName = character;
		if (characterName == "Vein")
		{
			if (value == "Hit")
			{
				_characterMouthManager[0].PlayHitSe();
			}
			else if (value == "Orgasm")
			{
				_characterMouthManager[0].PlayOrgasmSe();
			}
		}
		else if (characterName == "Vacua")
		{
			if (value == "Hit")
			{
				_characterMouthManager[1].PlayHitSe();
			}
			else if (value == "Orgasm")
			{
				_characterMouthManager[1].PlayOrgasmSe();
			}
		}
		else if (characterName == "Nuisance")
		{
			if (value == "Hit")
			{
				_characterMouthManager[2].PlayHitSe();
			}
			else if (value == "Orgasm")
			{
				_characterMouthManager[2].PlayOrgasmSe();
			}
		}
	}
}
