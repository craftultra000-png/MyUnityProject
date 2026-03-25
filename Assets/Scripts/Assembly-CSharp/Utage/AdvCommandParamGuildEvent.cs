using UnityEngine;

namespace Utage
{
	public class AdvCommandParamGuildEvent : AdvCommand
	{
		public GuildCore _guildCore;

		private string paramArg1;

		public AdvCommandParamGuildEvent(StringGridRow row)
			: base(row)
		{
			paramArg1 = ParseCell<string>(AdvColumnName.Arg1);
		}

		public override void DoCommand(AdvEngine engine)
		{
			if (UtageCustomCommand.instance.debugMode)
			{
				Debug.LogWarning("AdvCommandParam Guild Event");
			}
			if (_guildCore == null)
			{
				_guildCore = GuildCore.instance;
			}
			_guildCore.GuildEvent(paramArg1);
		}
	}
}
