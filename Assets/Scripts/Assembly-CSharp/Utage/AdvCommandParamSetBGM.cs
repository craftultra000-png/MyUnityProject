using UnityEngine;

namespace Utage
{
	public class AdvCommandParamSetBGM : AdvCommand
	{
		public BGMManager _BGMManager;

		private string paramArg1;

		public AdvCommandParamSetBGM(StringGridRow row)
			: base(row)
		{
			paramArg1 = ParseCell<string>(AdvColumnName.Arg1);
		}

		public override void DoCommand(AdvEngine engine)
		{
			if (UtageCustomCommand.instance.debugMode)
			{
				Debug.LogWarning("AdvCommandParam SetBGM Parameter Name: " + paramArg1);
			}
			if (_BGMManager == null)
			{
				_BGMManager = BGMManager.instance;
			}
			_BGMManager.SetBGM(paramArg1);
		}
	}
}
