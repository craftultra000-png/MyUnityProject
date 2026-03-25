using UnityEngine;

namespace Utage
{
	public class AdvCommandParamStageEvent : AdvCommand
	{
		public GameStageCore _gameStageCore;

		private string paramArg1;

		public AdvCommandParamStageEvent(StringGridRow row)
			: base(row)
		{
			paramArg1 = ParseCell<string>(AdvColumnName.Arg1);
		}

		public override void DoCommand(AdvEngine engine)
		{
			if (UtageCustomCommand.instance.debugMode)
			{
				Debug.LogWarning("AdvCommandParam Stage Event");
			}
			if (_gameStageCore == null)
			{
				_gameStageCore = GameStageCore.instance;
			}
			_gameStageCore.StageEvent(paramArg1);
		}
	}
}
