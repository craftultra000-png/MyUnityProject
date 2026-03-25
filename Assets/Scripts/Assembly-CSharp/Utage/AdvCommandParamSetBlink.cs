using UnityEngine;

namespace Utage
{
	public class AdvCommandParamSetBlink : AdvCommand
	{
		public PlayerBlinkEyes _playerBlinkEyes;

		private string paramArg1;

		public AdvCommandParamSetBlink(StringGridRow row)
			: base(row)
		{
			paramArg1 = ParseCell<string>(AdvColumnName.Arg1);
		}

		public override void DoCommand(AdvEngine engine)
		{
			if (UtageCustomCommand.instance.debugMode)
			{
				Debug.LogWarning("AdvCommandParam SetBlink Parameter Name: " + paramArg1);
			}
			if (_playerBlinkEyes == null)
			{
				_playerBlinkEyes = PlayerBlinkEyes.instance;
			}
			if (paramArg1 == "TRUE")
			{
				_playerBlinkEyes.BlinkForce(value: true);
			}
			else if (paramArg1 == "FALSE")
			{
				_playerBlinkEyes.BlinkForce(value: false);
			}
			else
			{
				Debug.LogError("EyeBlink Force Error:" + paramArg1);
			}
		}
	}
}
