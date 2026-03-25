using UnityEngine;

namespace Utage
{
	public class AdvCommandParamBattleStart : AdvCommand
	{
		public ActionCore _actionCore;

		private string paramArg1;

		private string paramArg2;

		public AdvCommandParamBattleStart(StringGridRow row)
			: base(row)
		{
			paramArg1 = ParseCell<string>(AdvColumnName.Arg1);
			paramArg2 = ParseCell<string>(AdvColumnName.Arg2);
		}

		public override void DoCommand(AdvEngine engine)
		{
			if (UtageCustomCommand.instance.debugMode)
			{
				Debug.LogWarning("AdvCommandParam SetLight  Parameter Name: " + paramArg1);
			}
			if (_actionCore == null)
			{
				_actionCore = ActionCore.instance;
			}
			if (paramArg2 == "TRUE")
			{
				_actionCore.BattleStart(paramArg1, look: true);
			}
			else if (paramArg2 == "FALSE")
			{
				_actionCore.BattleStart(paramArg1, look: false);
			}
		}
	}
}
