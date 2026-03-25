using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenSizeMaager : MonoBehaviour
{
	[Header("Current Setting")]
	public Text screenMode;

	public Text screenSize;

	public GameObject windowSetObject;

	public GameObject fullScreenSetObject;

	[Header("Window Size")]
	public int screenWidth = 1280;

	public int screenHeight = 720;

	public List<int> screenWidthList;

	public List<int> screenHeightList;

	public List<Vector2> screenSizeList;

	[Header("Window Mode")]
	public bool fullScreen;

	public GameObject windowObject;

	public GameObject fullScreenObject;

	[Header("Window Set")]
	public GameObject screenSetObject;

	private void Start()
	{
		if (!ES3.KeyExists("ScreenMode"))
		{
			ES3.Save("ScreenMode", value: false);
		}
		fullScreen = ES3.Load<bool>("ScreenMode");
		if (!ES3.KeyExists("ScreenSizeX"))
		{
			ES3.Save("ScreenSizeX", screenWidth);
			ES3.Save("ScreenSizeY", screenHeight);
		}
		screenWidth = ES3.Load<int>("ScreenSizeX");
		screenHeight = ES3.Load<int>("ScreenSizeY");
		if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.LinuxPlayer)
		{
			Screen.SetResolution(screenWidth, screenHeight, fullscreen: false);
		}
		if (!fullScreen)
		{
			windowObject.SetActive(value: false);
			fullScreenObject.SetActive(value: true);
			if (Screen.fullScreen)
			{
				Screen.fullScreen = false;
			}
		}
		else if (fullScreen)
		{
			windowObject.SetActive(value: true);
			fullScreenObject.SetActive(value: false);
			if (!Screen.fullScreen)
			{
				Screen.fullScreen = true;
			}
		}
		screenSize.text = screenWidth + " x " + screenHeight;
		SetScreenModeText();
		screenSetObject.SetActive(value: false);
	}

	private void Update()
	{
	}

	private void OnDisable()
	{
		screenSetObject.SetActive(value: false);
		ResetScreen();
	}

	public void SetScreenModeText()
	{
		if (!fullScreen)
		{
			windowSetObject.SetActive(value: true);
			fullScreenSetObject.SetActive(value: false);
		}
		else if (fullScreen)
		{
			windowSetObject.SetActive(value: false);
			fullScreenSetObject.SetActive(value: true);
		}
	}

	public void SetScreen()
	{
		ES3.Save("ScreenSizeX", screenWidth);
		ES3.Save("ScreenSizeY", screenHeight);
		ES3.Save("ScreenMode", fullScreen);
		if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.LinuxPlayer)
		{
			Screen.SetResolution(screenWidth, screenHeight, fullScreen);
		}
		screenSize.text = screenWidth + " x " + screenHeight;
		SetScreenModeText();
		screenSetObject.SetActive(value: false);
	}

	public void ResetScreen()
	{
		fullScreen = ES3.Load<bool>("ScreenMode");
		screenWidth = ES3.Load<int>("ScreenSizeX");
		screenHeight = ES3.Load<int>("ScreenSizeY");
		if (!fullScreen)
		{
			windowObject.SetActive(value: false);
			fullScreenObject.SetActive(value: true);
		}
		else if (fullScreen)
		{
			windowObject.SetActive(value: true);
			fullScreenObject.SetActive(value: false);
		}
		screenSize.text = screenWidth + " x " + screenHeight;
		SetScreenModeText();
	}

	public void ChangeScreenSize(int value)
	{
		screenWidth = screenWidthList[value];
		screenHeight = screenHeightList[value];
		screenSize.text = screenWidth + " x " + screenHeight;
		screenSetObject.SetActive(value: true);
	}

	public void ChangeScreenMode()
	{
		if (!fullScreen)
		{
			fullScreen = true;
			windowObject.SetActive(value: true);
			fullScreenObject.SetActive(value: false);
		}
		else if (fullScreen)
		{
			fullScreen = false;
			windowObject.SetActive(value: false);
			fullScreenObject.SetActive(value: true);
		}
		SetScreenModeText();
		screenSetObject.SetActive(value: true);
	}
}
