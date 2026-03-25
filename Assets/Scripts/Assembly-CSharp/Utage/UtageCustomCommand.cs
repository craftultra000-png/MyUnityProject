using System;
using UnityEngine;

namespace Utage
{
	[AddComponentMenu("Utage/ADV/Examples/UtageCustomCommand")]
	public class UtageCustomCommand : AdvCustomCommandManager
	{
		public static UtageCustomCommand instance;

		public bool debugMode;

		public void Awake()
		{
			if (instance == null)
			{
				instance = this;
			}
			if (!debugMode)
			{
				Debug.LogWarning("<b><i>UnUse AdvCommandParam Debug.Log</i></b>");
			}
		}

		public override void OnBootInit()
		{
			AdvCommandParser.OnCreateCustomCommandFromID = (AdvCommandParser.CreateCustomCommandFromID)Delegate.Combine(AdvCommandParser.OnCreateCustomCommandFromID, new AdvCommandParser.CreateCustomCommandFromID(CreateCustomCommand));
		}

		private void OnDestroy()
		{
			AdvCommandParser.OnCreateCustomCommandFromID = (AdvCommandParser.CreateCustomCommandFromID)Delegate.Remove(AdvCommandParser.OnCreateCustomCommandFromID, new AdvCommandParser.CreateCustomCommandFromID(CreateCustomCommand));
		}

		public override void OnClear()
		{
		}

		public void CreateCustomCommand(string id, StringGridRow row, AdvSettingDataManager dataManager, ref AdvCommand command)
		{
			switch (id)
			{
			case "ResetSelect":
				command = new AdvCommandParamResetSelect(row);
				break;
			case "SelectPosition":
				command = new AdvCommandParamSelectPosition(row);
				break;
			case "SetAction":
				command = new AdvCommandParamSetAction(row);
				break;
			case "SetAIUEO":
				command = new AdvCommandParamSetAIUEO(row);
				break;
			case "SetFacial":
				command = new AdvCommandParamSetFacial(row);
				break;
			case "SetTalk":
				command = new AdvCommandParamSetTalk(row);
				break;
			case "SetVoice":
				command = new AdvCommandParamSetVoice(row);
				break;
			case "SetMoveStage":
				command = new AdvCommandParamSetMoveStage(row);
				break;
			case "SetMovePoint":
				command = new AdvCommandParamSetMovePoint(row);
				break;
			case "SkipMovePoint":
				command = new AdvCommandParamSkipMovePoint(row);
				break;
			case "DisableCharacter":
				command = new AdvCommandParamDisableCharacter(row);
				break;
			case "SetBlink":
				command = new AdvCommandParamSetBlink(row);
				break;
			case "SetClothe":
				command = new AdvCommandParamSetClothe(row);
				break;
			case "BreakClothe":
				command = new AdvCommandParamBreakClothe(row);
				break;
			case "SetCharacterEffect":
				command = new AdvCommandParamSetCharacterEffect(row);
				break;
			case "SetTitsTouch":
				command = new AdvCommandParamSetTitsTouch(row);
				break;
			case "SetStage":
				command = new AdvCommandParamSetStage(row);
				break;
			case "SetBGM":
				command = new AdvCommandParamSetBGM(row);
				break;
			case "StopBGM":
				command = new AdvCommandParamStopBGM(row);
				break;
			case "GuildEvent":
				command = new AdvCommandParamGuildEvent(row);
				break;
			case "BriefingEvent":
				command = new AdvCommandParamBriefingEvent(row);
				break;
			case "StageEvent":
				command = new AdvCommandParamStageEvent(row);
				break;
			case "ResultEvent":
				command = new AdvCommandParamResultEvent(row);
				break;
			}
		}
	}
}
