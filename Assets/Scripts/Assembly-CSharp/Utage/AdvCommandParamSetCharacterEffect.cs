using UnityEngine;

namespace Utage
{
	public class AdvCommandParamSetCharacterEffect : AdvCommand
	{
		public ActionManager _actionManager;

		private string paramArg1;

		private string paramArg2;

		public AdvCommandParamSetCharacterEffect(StringGridRow row)
			: base(row)
		{
			paramArg1 = ParseCell<string>(AdvColumnName.Arg1);
			paramArg2 = ParseCell<string>(AdvColumnName.Arg2);
		}

		public override void DoCommand(AdvEngine engine)
		{
			if (UtageCustomCommand.instance.debugMode)
			{
				Debug.LogWarning("AdvCommandParam CharacterEffect Parameter Name: " + paramArg1 + " effect:" + paramArg2);
			}
			if (_actionManager == null)
			{
				_actionManager = ActionManager.instance;
			}
			if (paramArg1 == "<param=BarkName>")
			{
				if (UtageCustomCommand.instance.debugMode)
				{
					Debug.LogWarning("AdvCommandParam Rename: " + paramArg1 + " to " + engine.Param.GetParameterString("BarkName"));
				}
				paramArg1 = engine.Param.GetParameterString("BarkName");
			}
			else if (paramArg1 == "<param=BarkName2>")
			{
				if (UtageCustomCommand.instance.debugMode)
				{
					Debug.LogWarning("AdvCommandParam Rename: " + paramArg1 + " to " + engine.Param.GetParameterString("BarkName2"));
				}
				paramArg1 = engine.Param.GetParameterString("BarkName2");
			}
			else if (paramArg1 == "<param=BarkName3>")
			{
				if (UtageCustomCommand.instance.debugMode)
				{
					Debug.LogWarning("AdvCommandParam Rename: " + paramArg1 + " to " + engine.Param.GetParameterString("BarkName3"));
				}
				paramArg1 = engine.Param.GetParameterString("BarkName3");
			}
			else if (paramArg1 == "<param=TalkName>")
			{
				if (UtageCustomCommand.instance.debugMode)
				{
					Debug.LogWarning("AdvCommandParam Rename: " + paramArg1 + " to " + engine.Param.GetParameterString("TalkName"));
				}
				paramArg1 = engine.Param.GetParameterString("TalkName");
			}
			else if (paramArg1 == "<param=TalkName2>")
			{
				if (UtageCustomCommand.instance.debugMode)
				{
					Debug.LogWarning("AdvCommandParam Rename: " + paramArg1 + " to " + engine.Param.GetParameterString("TalkName2"));
				}
				paramArg1 = engine.Param.GetParameterString("TalkName2");
			}
			else if (paramArg1 == "<param=TalkName3>")
			{
				if (UtageCustomCommand.instance.debugMode)
				{
					Debug.LogWarning("AdvCommandParam Rename: " + paramArg1 + " to " + engine.Param.GetParameterString("TalkName3"));
				}
				paramArg1 = engine.Param.GetParameterString("TalkName3");
			}
			_actionManager.SetEffect(paramArg1, paramArg2);
		}
	}
}
