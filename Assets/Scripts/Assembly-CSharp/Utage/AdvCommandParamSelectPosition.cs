using UnityEngine;

namespace Utage
{
	public class AdvCommandParamSelectPosition : AdvCommand
	{
		public TalkSelectManager _talkSelectManager;

		private string paramArg1;

		public AdvCommandParamSelectPosition(StringGridRow row)
			: base(row)
		{
			paramArg1 = ParseCell<string>(AdvColumnName.Arg1);
		}

		public override void DoCommand(AdvEngine engine)
		{
			if (UtageCustomCommand.instance.debugMode)
			{
				Debug.LogWarning("AdvCommandParam SelectPosition Name: " + paramArg1);
			}
			if (_talkSelectManager == null)
			{
				_talkSelectManager = TalkSelectManager.instance;
			}
			if (paramArg1 == "Bottom")
			{
				_talkSelectManager.isBottom = true;
			}
		}
	}
}
