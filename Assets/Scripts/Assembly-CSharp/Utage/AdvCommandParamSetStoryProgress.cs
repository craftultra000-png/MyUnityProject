using UnityEngine;

namespace Utage
{
	public class AdvCommandParamSetStoryProgress : AdvCommand
	{
		private int storyProgress;

		private int paramArg1;

		public AdvCommandParamSetStoryProgress(StringGridRow row)
			: base(row)
		{
			paramArg1 = ParseCell<int>(AdvColumnName.Arg1);
		}

		public override void DoCommand(AdvEngine engine)
		{
			if (ES3.KeyExists("StoryProgress"))
			{
				storyProgress = ES3.Load<int>("StoryProgress");
			}
			if (storyProgress < paramArg1)
			{
				storyProgress = paramArg1;
				ES3.Save("StoryProgress", storyProgress);
				Debug.LogWarning("Save StoryProgress :" + storyProgress);
			}
		}
	}
}
