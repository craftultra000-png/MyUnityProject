using Beautify.Universal;
using UnityEngine;

public class BeautifyManager : MonoBehaviour
{
	public static BeautifyManager instance;

	private void Awake()
	{
		instance = this;
		SetFinalBlur(value: false);
	}

	public void SetFinalBlur(bool value)
	{
		if (value)
		{
			BeautifySettings.sharedSettings.blurIntensity.overrideState = true;
		}
		else if (!value)
		{
			BeautifySettings.sharedSettings.blurIntensity.overrideState = false;
		}
	}
}
