using UnityEngine;

namespace Utage
{
	public class AdvCommandParamSetSwapPosition : AdvCommand
	{
		public WalkPointCore _walkPointCore;

		private string paramArg1;

		private string paramArg2;

		public AdvCommandParamSetSwapPosition(StringGridRow row)
			: base(row)
		{
			paramArg1 = ParseCell<string>(AdvColumnName.Arg1);
			paramArg2 = ParseCell<string>(AdvColumnName.Arg2);
		}

		public override void DoCommand(AdvEngine engine)
		{
			if (UtageCustomCommand.instance.debugMode)
			{
				Debug.LogWarning("AdvCommandParam SetSwapPosition  Parameter Name: " + paramArg1 + "Name2: " + paramArg2);
			}
			if (_walkPointCore == null)
			{
				_walkPointCore = WalkPointCore.instance;
			}
			_walkPointCore.CharacterSwap(paramArg1, paramArg2);
		}
	}
}
