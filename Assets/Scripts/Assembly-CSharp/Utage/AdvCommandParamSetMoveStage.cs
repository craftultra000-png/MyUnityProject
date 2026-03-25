using UnityEngine;

namespace Utage
{
	public class AdvCommandParamSetMoveStage : AdvCommand
	{
		public WalkPointDataBase _walkPointDataBase;

		private string paramArg1;

		public AdvCommandParamSetMoveStage(StringGridRow row)
			: base(row)
		{
			paramArg1 = ParseCell<string>(AdvColumnName.Arg1);
		}

		public override void DoCommand(AdvEngine engine)
		{
			if (UtageCustomCommand.instance.debugMode)
			{
				Debug.LogWarning("AdvCommandParam SetMoveStage Parameter Name: " + paramArg1);
			}
			if (_walkPointDataBase == null)
			{
				_walkPointDataBase = WalkPointDataBase.instance;
			}
			_walkPointDataBase.SetStageName(paramArg1);
		}
	}
}
