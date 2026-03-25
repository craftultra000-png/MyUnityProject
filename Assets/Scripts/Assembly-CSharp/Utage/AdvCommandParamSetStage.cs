using UnityEngine;

namespace Utage
{
	public class AdvCommandParamSetStage : AdvCommand
	{
		public BattleStageManager _battleStageManager;

		private string paramArg1;

		public AdvCommandParamSetStage(StringGridRow row)
			: base(row)
		{
			paramArg1 = ParseCell<string>(AdvColumnName.Arg1);
		}

		public override void DoCommand(AdvEngine engine)
		{
			if (UtageCustomCommand.instance.debugMode)
			{
				Debug.LogWarning("AdvCommandParam SetLight  Parameter Name: " + paramArg1);
			}
			if (_battleStageManager == null)
			{
				_battleStageManager = BattleStageManager.instance;
			}
			_battleStageManager.SetStage(paramArg1);
		}
	}
}
