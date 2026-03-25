using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFacialManager : MonoBehaviour
{
	public CharacterHead _characterHead;

	public CharacterTalkManager _characterTalkManager;

	[Header("Status")]
	public string currentStatus;

	public float currentHeart;

	[Space]
	public float currentHot;

	public float currentPleasure;

	[Header("Life Status")]
	public bool isLifeOverHeat;

	public bool isLifeHeart;

	[Header("Timer")]
	public bool manualSet;

	public bool timeStop;

	public float eyesTime;

	public float mouthTime;

	[Header("Face")]
	public CharacterFaceManager _characterFaceManager;

	public bool isBlush;

	public bool isFacial;

	public float currentBlush;

	public float targetBlush;

	public Vector2 facialData;

	public Vector2 facialTargetData;

	[Header("Tongue")]
	public CharacterTongueManager _characterTongueManager;

	public bool isIdle;

	public bool isBreath;

	public bool isDoggy;

	public bool isLipUpperLick;

	public bool isLipLowerLick;

	public bool isSwayX;

	public bool isSwayY;

	private List<Action> mouthActions;

	[Header("Mouth")]
	public CharacterMouthManager _characterMouthManager;

	public bool isTooth;

	public bool isMouthOpen01;

	public bool isMouthOpen02;

	public bool isMouthOpen03;

	[Header("Eyes")]
	public CharacterEyesManager _characterEyesManager;

	public bool isEmpty;

	public bool isHeart;

	public bool isTear;

	[Space]
	public bool isSleep;

	[Space]
	public bool isFocus;

	public bool isCross;

	public bool isClose;

	public bool isWinkL;

	public bool isWinkR;

	[Space]
	public bool isEyesClose01;

	public bool isEyesClose02;

	public bool isEyesClose03;

	private List<Action> eyeActionsUpper;

	private List<Action> eyeActionsLower;

	private List<Action> eyeActionsBoth;

	private List<Action> eyeActionsShock;

	[Header("Eyes Look")]
	public CharacterLookAtEyes _characterLookAtEyes;

	public bool isLookAwayEyes;

	public bool isAhe;

	public bool isBottom;

	[Header("Head Look")]
	public CharacterLookAtHead _characterLookAtHead;

	public bool isLookAwayHead;

	private void Start()
	{
		SetEyesAction();
		SetMouthAction();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Z))
		{
			if (!isAhe)
			{
				manualSet = true;
				eyesTime = 0f;
				mouthTime = 0f;
				ResetAllBool(idle: false);
				Debug.LogError("Debug Ahe", base.gameObject);
				isBlush = true;
				targetBlush = 1f;
				isFacial = true;
				facialTargetData = new Vector2(1f, -1f);
				isDoggy = true;
				isEmpty = true;
				isCross = true;
				isEyesClose02 = true;
				isAhe = true;
			}
			else if (isAhe)
			{
				manualSet = false;
				ResetAllBool(idle: true);
			}
		}
	}

	private void LateUpdate()
	{
		if (!timeStop && !manualSet && eyesTime > 0f)
		{
			eyesTime -= Time.deltaTime;
			if (eyesTime < 0f)
			{
				ResetEyesBool();
			}
		}
		if (!timeStop && !manualSet && mouthTime > 0f)
		{
			mouthTime -= Time.deltaTime;
			if (mouthTime < 0f)
			{
				ResetToothBool();
				isIdle = true;
			}
		}
		if (isBlush)
		{
			isBlush = false;
			_characterFaceManager.targetBlush = targetBlush;
		}
		if (isFacial)
		{
			isFacial = false;
			_characterFaceManager.facialTargetData = facialTargetData;
		}
		if (_characterTongueManager.isIdle != isIdle)
		{
			_characterTongueManager.changeTongue = true;
			_characterTongueManager.isIdle = isIdle;
		}
		if (_characterTongueManager.isBreath != isBreath)
		{
			_characterTongueManager.changeTongue = true;
			_characterTongueManager.isBreath = isBreath;
		}
		if (_characterTongueManager.isDoggy != isDoggy)
		{
			_characterTongueManager.changeTongue = true;
			_characterTongueManager.isDoggy = isDoggy;
		}
		if (_characterTongueManager.isLipUpperLick != isLipUpperLick)
		{
			_characterTongueManager.changeTongue = true;
			_characterTongueManager.isLipUpperLick = isLipUpperLick;
		}
		if (_characterTongueManager.isLipLowerLick != isLipLowerLick)
		{
			_characterTongueManager.changeTongue = true;
			_characterTongueManager.isLipLowerLick = isLipLowerLick;
		}
		if (_characterTongueManager.isSwayX != isSwayX)
		{
			_characterTongueManager.changeTongue = true;
			_characterTongueManager.isSwayX = isSwayX;
		}
		if (_characterTongueManager.isSwayY != isSwayY)
		{
			_characterTongueManager.changeTongue = true;
			_characterTongueManager.isSwayY = isSwayY;
		}
		_characterMouthManager.isTooth = isTooth;
		if (_characterEyesManager.isEmpty != isEmpty)
		{
			_characterEyesManager.SetEmptyEyes();
		}
		if (_characterEyesManager.isHeart != isHeart)
		{
			_characterEyesManager.SetHeartEyes();
		}
		if (_characterEyesManager.isTear != isTear)
		{
			_characterEyesManager.SetTearEyes();
		}
		_characterEyesManager.isSleep = isSleep;
		if (_characterTalkManager != null)
		{
			_characterTalkManager.sleep = isSleep;
		}
		_characterLookAtHead.isSleep = isSleep;
		_characterEyesManager.isFocus = isFocus;
		_characterEyesManager.isCross = isCross;
		_characterEyesManager.isClose = isClose;
		_characterEyesManager.isWinkL = isWinkL;
		_characterEyesManager.isWinkR = isWinkR;
		_characterEyesManager.isEyesClose01 = isEyesClose01;
		_characterEyesManager.isEyesClose02 = isEyesClose02;
		_characterEyesManager.isEyesClose03 = isEyesClose03;
		_characterLookAtEyes.isLookAwayEyes = isLookAwayEyes;
		_characterLookAtEyes.isAhe = isAhe;
		_characterLookAtEyes.isBottom = isBottom;
		_characterLookAtHead.isLookAwayHead = isLookAwayHead;
	}

	public void ResetAllBool(bool idle)
	{
		ResetTongueBool(idle);
		ResetToothBool();
		ResetEyesBool();
		ResetEyesClose();
	}

	public void ResetTongueBool(bool idle)
	{
		isIdle = idle;
		isBreath = false;
		isDoggy = false;
		isLipUpperLick = false;
		isLipLowerLick = false;
		isSwayX = false;
		isSwayY = false;
	}

	public void ResetToothBool()
	{
		isTooth = false;
		isMouthOpen01 = false;
		isMouthOpen02 = false;
		isMouthOpen03 = false;
	}

	public void ResetEyesBool()
	{
		if (isEmpty)
		{
			_characterEyesManager.SetEmptyEyes();
		}
		if (isHeart)
		{
			_characterEyesManager.SetHeartEyes();
		}
		if (isTear)
		{
			_characterEyesManager.SetTearEyes();
		}
		isFocus = false;
		isCross = false;
		isClose = false;
		isWinkL = false;
		isWinkR = false;
		isLookAwayEyes = false;
		isAhe = false;
		isBottom = false;
		isLookAwayHead = false;
	}

	public void ResetEyesClose()
	{
		isEyesClose01 = false;
		isEyesClose02 = false;
		isEyesClose03 = false;
	}

	public void SetPleasure(int value)
	{
		currentPleasure += value;
	}

	public void SetHot(int value)
	{
		currentHot += value;
	}

	public void SetFacial(string type, float chance)
	{
		currentStatus = type;
		bool flag = false;
		bool flag2 = false;
		if (manualSet)
		{
			flag = true;
		}
		if (timeStop)
		{
			flag = true;
		}
		if (!flag && type == "Ahe")
		{
			timeStop = true;
			flag = true;
			ResetAllBool(idle: false);
			eyesTime = 0f;
			mouthTime = 0f;
			isBlush = true;
			targetBlush = 1f;
			isFacial = true;
			facialTargetData = new Vector2(1f, -1f);
			isDoggy = true;
			isEmpty = true;
			isCross = true;
			isEyesClose02 = true;
			isAhe = true;
		}
		if (!flag && type == "Despair")
		{
			flag = true;
			ResetAllBool(idle: false);
			eyesTime = 0f;
			mouthTime = 0f;
			isFacial = true;
			facialTargetData = new Vector2(0f, -1f);
			isEyesClose02 = true;
			isTear = true;
		}
		if (!flag && type == "Heart")
		{
			flag = true;
			ResetAllBool(idle: false);
			eyesTime = 0f;
			mouthTime = 0f;
			isFacial = true;
			facialTargetData = new Vector2(-1f, 0f);
			isHeart = true;
			isEyesClose02 = true;
		}
		if (!flag && type == "Suppress")
		{
			flag = true;
			ResetAllBool(idle: false);
			eyesTime = 0f;
			mouthTime = 0f;
			isTooth = true;
			isCross = true;
			isEyesClose02 = true;
			isAhe = true;
		}
		if (timeStop || manualSet || flag)
		{
			return;
		}
		if (eyesTime <= 0f)
		{
			flag = false;
			if (flag2 && type == "Upper")
			{
				ResetEyesBool();
				eyeActionsUpper[UnityEngine.Random.Range(0, eyeActionsUpper.Count)]();
				Debug.LogError("Set Eyes Upper Action");
			}
			else if (flag2 && type == "Lower")
			{
				ResetEyesBool();
				eyeActionsLower[UnityEngine.Random.Range(0, eyeActionsLower.Count)]();
				Debug.LogError("Set Eyes Lower Action");
			}
			else if (flag2 && type == "Both")
			{
				ResetEyesBool();
				eyeActionsBoth[UnityEngine.Random.Range(0, eyeActionsBoth.Count)]();
				Debug.LogError("Set Eyes Both Action");
			}
			else if (flag2 && type == "Shock")
			{
				ResetEyesBool();
				eyeActionsShock[UnityEngine.Random.Range(0, eyeActionsShock.Count)]();
				Debug.LogError("Set Eyes Both Action");
			}
			if (!flag && currentHot >= 0f && UnityEngine.Random.value < chance)
			{
				flag = true;
				if (UnityEngine.Random.value < 0.1f)
				{
					isEyesClose01 = true;
				}
				else if (UnityEngine.Random.value < 0.1f)
				{
					isEyesClose02 = true;
				}
				else if (UnityEngine.Random.value < 0.05f)
				{
					isEyesClose03 = true;
				}
			}
			if (!isLifeHeart && !isLifeOverHeat && UnityEngine.Random.value < chance)
			{
				int num = UnityEngine.Random.Range(0, 3);
				isFacial = true;
				switch (num)
				{
				case 0:
					facialTargetData = new Vector2(1f, 0f);
					break;
				case 1:
					facialTargetData = new Vector2(0f, -1f);
					break;
				case 2:
					facialTargetData = new Vector2(0.75f, -0.75f);
					break;
				}
			}
			else if (isLifeHeart && !isLifeOverHeat && UnityEngine.Random.value < chance)
			{
				int num2 = UnityEngine.Random.Range(0, 4);
				isFacial = true;
				switch (num2)
				{
				case 0:
					facialTargetData = new Vector2(0f, 1f);
					break;
				case 1:
					facialTargetData = new Vector2(0f, -1f);
					break;
				case 2:
					facialTargetData = new Vector2(0.75f, 1f);
					break;
				case 3:
					facialTargetData = new Vector2(0.75f, -1f);
					break;
				}
			}
			else if (isLifeOverHeat && UnityEngine.Random.value < chance)
			{
				int num3 = UnityEngine.Random.Range(0, 5);
				isFacial = true;
				switch (num3)
				{
				case 0:
					facialTargetData = new Vector2(-1f, 0f);
					break;
				case 1:
					facialTargetData = new Vector2(0f, 1f);
					break;
				case 2:
					facialTargetData = new Vector2(0f, -1f);
					break;
				case 3:
					facialTargetData = new Vector2(-0.75f, 0.75f);
					break;
				case 4:
					facialTargetData = new Vector2(-0.75f, -0.75f);
					break;
				}
			}
		}
		if (!(mouthTime <= 0f))
		{
			return;
		}
		flag = false;
		if (UnityEngine.Random.value < chance)
		{
			flag2 = true;
		}
		if (flag2)
		{
			ResetToothBool();
			ResetTongueBool(idle: false);
			mouthActions[UnityEngine.Random.Range(0, mouthActions.Count)]();
			Debug.LogError("Set Mouth Action");
		}
		if (!flag && currentHot >= 0f && UnityEngine.Random.value < chance)
		{
			flag = true;
			if (UnityEngine.Random.value < 0.1f)
			{
				isMouthOpen01 = true;
			}
			else if (UnityEngine.Random.value < 0.1f)
			{
				isMouthOpen02 = true;
			}
			else if (UnityEngine.Random.value < 0.05f)
			{
				isMouthOpen03 = true;
			}
		}
	}

	public void SetEyesAction()
	{
		eyeActionsUpper = new List<Action>();
		eyeActionsLower = new List<Action>();
		eyeActionsBoth = new List<Action>();
		eyeActionsShock = new List<Action>();
		eyeActionsUpper.Add(delegate
		{
			isLookAwayEyes = true;
			isLookAwayHead = true;
			eyesTime = UnityEngine.Random.Range(0.5f, 2f);
		});
		eyeActionsLower.Add(delegate
		{
			isLookAwayEyes = true;
			isLookAwayHead = true;
			eyesTime = UnityEngine.Random.Range(0.5f, 2f);
		});
		eyeActionsBoth.Add(delegate
		{
			isLookAwayEyes = true;
			isLookAwayHead = true;
			eyesTime = UnityEngine.Random.Range(0.5f, 2f);
		});
		eyeActionsLower.Add(delegate
		{
			isBottom = true;
			eyesTime = UnityEngine.Random.Range(1f, 3f);
		});
		eyeActionsBoth.Add(delegate
		{
			isBottom = true;
			eyesTime = UnityEngine.Random.Range(1f, 3f);
		});
		eyeActionsUpper.Add(delegate
		{
			isAhe = true;
			eyesTime = UnityEngine.Random.Range(0.5f, 1f);
		});
		eyeActionsBoth.Add(delegate
		{
			isAhe = true;
			eyesTime = UnityEngine.Random.Range(0.5f, 1f);
		});
		eyeActionsUpper.Add(delegate
		{
			isClose = true;
			eyesTime = UnityEngine.Random.Range(0.5f, 1f);
		});
		eyeActionsLower.Add(delegate
		{
			isClose = true;
			eyesTime = UnityEngine.Random.Range(0.5f, 1f);
		});
		eyeActionsBoth.Add(delegate
		{
			isClose = true;
			eyesTime = UnityEngine.Random.Range(0.5f, 1f);
		});
		eyeActionsShock.Add(delegate
		{
			isClose = true;
			eyesTime = UnityEngine.Random.Range(0.5f, 1f);
		});
		eyeActionsShock.Add(delegate
		{
			isWinkL = true;
			eyesTime = UnityEngine.Random.Range(0.5f, 1f);
		});
		eyeActionsShock.Add(delegate
		{
			isLookAwayEyes = true;
			isLookAwayHead = true;
			eyesTime = UnityEngine.Random.Range(3f, 8f);
		});
		eyeActionsShock.Add(delegate
		{
			isFocus = true;
			eyesTime = UnityEngine.Random.Range(0.5f, 1f);
		});
	}

	public void SetMouthAction()
	{
		mouthActions = new List<Action>();
		mouthActions.Add(delegate
		{
			isLipLowerLick = true;
			mouthTime = UnityEngine.Random.Range(1f, 2f);
		});
		mouthActions.Add(delegate
		{
			isTooth = true;
			mouthTime = UnityEngine.Random.Range(1f, 2f);
		});
	}

	public void HeadDizzy()
	{
		_characterHead.HeadDizzy();
	}

	public void HeadHeart()
	{
		_characterHead.HeadHeart();
	}

	public void HeadOverHeat()
	{
		_characterHead.HeadOverHeat();
	}
}
