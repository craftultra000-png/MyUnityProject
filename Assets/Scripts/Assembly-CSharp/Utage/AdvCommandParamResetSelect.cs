using UnityEngine;

namespace Utage
{
	public class AdvCommandParamResetSelect : AdvCommand
	{
		public AdvCommandParamResetSelect(StringGridRow row)
			: base(row)
		{
		}

		public override void DoCommand(AdvEngine engine)
		{
			if (UtageCustomCommand.instance.debugMode)
			{
				Debug.LogWarning("AdvCommandParam ResetSelect");
			}
			engine.Param.SetParameterBoolean("Selection1", value: false);
			engine.Param.SetParameterBoolean("Selection2", value: false);
			engine.Param.SetParameterBoolean("Selection3", value: false);
			engine.Param.SetParameterBoolean("Selection4", value: false);
		}
	}
}
