using Beautify.Universal;
using UnityEngine;

public class PlayerBlinkEyes : MonoBehaviour
{
	public static PlayerBlinkEyes instance;

	public CharacterModelManager _characterModelManagerPlayer;

	public CharacterModelManager _characterModelManagerEnemy;

	[Header("Initial")]
	public bool beautifyInitial;

	[Header("Status")]
	public bool isTitle;

	public bool isCamera;

	public int setType;

	[Header("Eyes")]
	public bool isBlink;

	public bool isBlinkForce;

	public bool isDrowsiness;

	public bool isDrowsinessCheck;

	public float currentEyes;

	public float blinkEyes = 1f;

	public float blinkSpeed = 3f;

	public float drowsinessEyes = 0.2f;

	public float drowsinessRange = 0.05f;

	public float drowsinessSpeed = 0.5f;

	[Header("Wait")]
	public bool isClose;

	public float currentWait;

	public float maxWait = 0.5f;

	public void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
	}

	private void Start()
	{
		currentWait = 0f;
		isBlink = false;
		isClose = false;
		if (isDrowsiness)
		{
			currentEyes = drowsinessEyes;
		}
		else
		{
			currentEyes = 0f;
		}
	}

	private void Update()
	{
		if (!beautifyInitial && BeautifySettings.settings != null)
		{
			beautifyInitial = true;
			BeautifySettings.sharedSettings.vignettingBlink.overrideState = true;
			BeautifySettings.sharedSettings.vignettingBlink.value = currentEyes;
		}
		if ((!Input.GetKeyDown(KeyCode.C) || isBlink || isTitle) && Input.GetMouseButtonDown(2) && !isBlink)
		{
			_ = isTitle;
		}
		if (isBlink)
		{
			if (!isClose && currentEyes != blinkEyes)
			{
				if (currentEyes >= blinkEyes)
				{
					currentEyes = blinkEyes;
					BeautifySettings.sharedSettings.vignettingBlink.value = currentEyes;
					currentWait = maxWait;
					isClose = false;
					EyesClose();
				}
				else
				{
					currentEyes += Time.deltaTime * blinkSpeed;
					BeautifySettings.sharedSettings.vignettingBlink.value = currentEyes;
				}
			}
			else if (!isClose && !isBlinkForce)
			{
				currentWait -= Time.deltaTime;
				if (currentWait <= 0f)
				{
					currentWait = 0f;
					isClose = true;
				}
			}
			else if (isClose && !isBlinkForce)
			{
				if (currentEyes <= 0f)
				{
					currentEyes = 0f;
					BeautifySettings.sharedSettings.vignettingBlink.value = currentEyes;
					isCamera = false;
					isBlink = false;
					isClose = false;
				}
				else
				{
					currentEyes -= Time.deltaTime * blinkSpeed;
					BeautifySettings.sharedSettings.vignettingBlink.value = currentEyes;
				}
			}
		}
		if (!isDrowsiness)
		{
			return;
		}
		if (!isDrowsinessCheck)
		{
			if (currentEyes > drowsinessEyes + drowsinessRange)
			{
				isDrowsinessCheck = true;
				currentEyes = drowsinessEyes + drowsinessRange;
				BeautifySettings.sharedSettings.vignettingBlink.value = currentEyes;
			}
			else
			{
				currentEyes += Time.deltaTime * drowsinessSpeed;
				BeautifySettings.sharedSettings.vignettingBlink.value = currentEyes;
			}
		}
		else if (currentEyes < drowsinessEyes - drowsinessRange)
		{
			isDrowsinessCheck = false;
			currentEyes = drowsinessEyes - drowsinessRange;
			BeautifySettings.sharedSettings.vignettingBlink.value = currentEyes;
		}
		else
		{
			currentEyes -= Time.deltaTime * drowsinessSpeed;
			BeautifySettings.sharedSettings.vignettingBlink.value = currentEyes;
		}
	}

	public void GameEnd()
	{
		BeautifySettings.sharedSettings.vignettingBlink.value = 0f;
	}

	public void BlinkForce(bool value)
	{
		isBlinkForce = value;
		if (isBlinkForce)
		{
			isBlink = true;
			isClose = false;
		}
		else
		{
			isBlink = true;
			isClose = true;
			currentWait = 0f;
		}
	}

	public void BlinkSet(string value)
	{
		if (value == "Camera")
		{
			isCamera = true;
		}
		else
		{
			isCamera = false;
		}
		isBlink = true;
		isClose = false;
		currentEyes = 0f;
	}

	public void EyesClose()
	{
		_ = isCamera;
	}
}
