using UnityEngine;

namespace Utage
{
	public class AdvCommandParamResultEvent : AdvCommand
	{
		public ResultCore _resultCore;

		private string paramArg1;

		public AdvCommandParamResultEvent(StringGridRow row)
			: base(row)
		{
			paramArg1 = ParseCell<string>(AdvColumnName.Arg1);
		}

		public override void DoCommand(AdvEngine engine)
		{
			if (UtageCustomCommand.instance.debugMode)
			{
				Debug.LogWarning("AdvCommandParam Result Event");
			}
			if (_resultCore == null)
			{
				_resultCore = ResultCore.instance;
			}
			_resultCore.ResultEvent(paramArg1);
		}
	}
}
