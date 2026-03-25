using UnityEngine;

namespace Utage
{
	public class AdvCommandParamBriefingEvent : AdvCommand
	{
		public BriefingCore _briefingCore;

		private string paramArg1;

		public AdvCommandParamBriefingEvent(StringGridRow row)
			: base(row)
		{
			paramArg1 = ParseCell<string>(AdvColumnName.Arg1);
		}

		public override void DoCommand(AdvEngine engine)
		{
			if (UtageCustomCommand.instance.debugMode)
			{
				Debug.LogWarning("AdvCommandParam Briefing Event");
			}
			if (_briefingCore == null)
			{
				_briefingCore = BriefingCore.instance;
			}
			_briefingCore.BriefingEvent(paramArg1);
		}
	}
}
