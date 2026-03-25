using UnityEngine;

namespace Utage
{
	public class AdvCommandParamSetTitsTouch : AdvCommand
	{
		public ActionManager _actionManager;

		private string paramArg1;

		private string paramArg2;

		private string paramArg3;

		private string paramArg4;

		private int paramArg5;

		public AdvCommandParamSetTitsTouch(StringGridRow row)
			: base(row)
		{
			paramArg1 = ParseCell<string>(AdvColumnName.Arg1);
			paramArg2 = ParseCell<string>(AdvColumnName.Arg2);
			paramArg3 = ParseCell<string>(AdvColumnName.Arg3);
			paramArg4 = ParseCell<string>(AdvColumnName.Arg4);
			paramArg5 = ParseCell<int>(AdvColumnName.Arg5);
		}

		public override void DoCommand(AdvEngine engine)
		{
			if (UtageCustomCommand.instance.debugMode)
			{
				Debug.LogWarning("AdvCommandParam ParamSetTitsTouch atk: " + paramArg1 + " def:" + paramArg2 + " tits:" + paramArg3 + " touch:" + paramArg4 + " hand:" + paramArg5);
			}
			if (_actionManager == null)
			{
				_actionManager = ActionManager.instance;
			}
			if (paramArg4 == "TRUE")
			{
				_actionManager.SetTitsSe(paramArg1);
			}
			if (paramArg3 == "InBoth")
			{
				if (paramArg4 == "TRUE")
				{
					_actionManager.InTitsTouch(paramArg1, paramArg2, touch: true, paramArg5);
				}
				else if (paramArg4 == "FALSE")
				{
					_actionManager.InTitsTouch(paramArg1, paramArg2, touch: false, paramArg5);
				}
			}
			else if (paramArg3 == "InLeft")
			{
				if (paramArg4 == "TRUE")
				{
					_actionManager.InTitsTouchL(paramArg1, paramArg2, touch: true, paramArg5);
				}
				else if (paramArg4 == "FALSE")
				{
					_actionManager.InTitsTouchL(paramArg1, paramArg2, touch: false, paramArg5);
				}
			}
			else if (paramArg3 == "InRight")
			{
				if (paramArg4 == "TRUE")
				{
					_actionManager.InTitsTouchR(paramArg1, paramArg2, touch: true, paramArg5);
				}
				else if (paramArg4 == "FALSE")
				{
					_actionManager.InTitsTouchR(paramArg1, paramArg2, touch: false, paramArg5);
				}
			}
			if (paramArg3 == "OutBoth")
			{
				if (paramArg4 == "TRUE")
				{
					_actionManager.OutTitsTouch(paramArg1, paramArg2, touch: true, paramArg5);
				}
				else if (paramArg4 == "FALSE")
				{
					_actionManager.OutTitsTouch(paramArg1, paramArg2, touch: false, paramArg5);
				}
			}
			else if (paramArg3 == "OutLeft")
			{
				if (paramArg4 == "TRUE")
				{
					_actionManager.OutTitsTouchL(paramArg1, paramArg2, touch: true, paramArg5);
				}
				else if (paramArg4 == "FALSE")
				{
					_actionManager.OutTitsTouchL(paramArg1, paramArg2, touch: false, paramArg5);
				}
			}
			else if (paramArg3 == "OutRight")
			{
				if (paramArg4 == "TRUE")
				{
					_actionManager.OutTitsTouchR(paramArg1, paramArg2, touch: true, paramArg5);
				}
				else if (paramArg4 == "FALSE")
				{
					_actionManager.OutTitsTouchR(paramArg1, paramArg2, touch: false, paramArg5);
				}
			}
			if (paramArg3 == "SideBoth")
			{
				if (paramArg4 == "TRUE")
				{
					_actionManager.SideTitsTouch(paramArg1, paramArg2, touch: true, paramArg5);
				}
				else if (paramArg4 == "FALSE")
				{
					_actionManager.SideTitsTouch(paramArg1, paramArg2, touch: false, paramArg5);
				}
			}
			else if (paramArg3 == "SideLeft")
			{
				if (paramArg4 == "TRUE")
				{
					_actionManager.SideTitsTouchL(paramArg1, paramArg2, touch: true, paramArg5);
				}
				else if (paramArg4 == "FALSE")
				{
					_actionManager.SideTitsTouchL(paramArg1, paramArg2, touch: false, paramArg5);
				}
			}
			else if (paramArg3 == "SideRight")
			{
				if (paramArg4 == "TRUE")
				{
					_actionManager.SideTitsTouchR(paramArg1, paramArg2, touch: true, paramArg5);
				}
				else if (paramArg4 == "FALSE")
				{
					_actionManager.SideTitsTouchR(paramArg1, paramArg2, touch: false, paramArg5);
				}
			}
		}
	}
}
