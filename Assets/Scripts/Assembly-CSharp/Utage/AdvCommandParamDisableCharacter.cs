using UnityEngine;

namespace Utage
{
	public class AdvCommandParamDisableCharacter : AdvCommand
	{
		public WalkPointCore _walkPointCore;

		private string paramArg1;

		public AdvCommandParamDisableCharacter(StringGridRow row)
			: base(row)
		{
			paramArg1 = ParseCell<string>(AdvColumnName.Arg1);
		}

		public override void DoCommand(AdvEngine engine)
		{
			if (UtageCustomCommand.instance.debugMode)
			{
				Debug.LogWarning("AdvCommandParam DisableCharacter Parameter Name: " + paramArg1);
			}
			if (_walkPointCore == null)
			{
				_walkPointCore = WalkPointCore.instance;
			}
			_walkPointCore.CharacterDisable(paramArg1);
		}
	}
}
