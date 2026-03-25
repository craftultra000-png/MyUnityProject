using UnityEngine;

namespace Utage
{
	public class AdvCommandParamStopBGM : AdvCommand
	{
		public BGMManager _BGMManager;

		private string paramArg1;

		public AdvCommandParamStopBGM(StringGridRow row)
			: base(row)
		{
			paramArg1 = ParseCell<string>(AdvColumnName.Arg1);
		}

		public override void DoCommand(AdvEngine engine)
		{
			if (UtageCustomCommand.instance.debugMode)
			{
				Debug.LogWarning("AdvCommandParam Stop BGM");
			}
			if (_BGMManager == null)
			{
				_BGMManager = BGMManager.instance;
			}
			_BGMManager.StopBGM();
		}
	}
}
