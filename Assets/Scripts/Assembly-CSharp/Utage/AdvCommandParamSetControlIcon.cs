using UnityEngine;

namespace Utage
{
	public class AdvCommandParamSetControlIcon : AdvCommand
	{
		public ControlIconGUI _controlIconGUI;

		private string paramArg1;

		public AdvCommandParamSetControlIcon(StringGridRow row)
			: base(row)
		{
			paramArg1 = ParseCell<string>(AdvColumnName.Arg1);
		}

		public override void DoCommand(AdvEngine engine)
		{
			if (UtageCustomCommand.instance.debugMode)
			{
				Debug.LogWarning("AdvCommandParam SetControl Icon");
			}
			if (_controlIconGUI == null)
			{
				_controlIconGUI = ControlIconGUI.instance;
			}
			_controlIconGUI.SetType(paramArg1);
		}
	}
}
