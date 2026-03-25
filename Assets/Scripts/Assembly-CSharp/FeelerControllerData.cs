using System.Collections.Generic;
using UnityEngine;

public class FeelerControllerData : MonoBehaviour
{
	public static FeelerControllerData instance;

	public SystemCore _systemCore;

	public SkillGUIManager _skillGUIManager;

	public SkillGUIDataBase _skillGUIDataBase;

	public SkillGUIQuickSet _skillGUIQuickSet;

	public QuickSkillGUI _quickSkillGUI;

	public CharacterAnimationSpeedManager _characterAnimationSpeedManager;

	public CharacterEffectManager _characterEffectManager;

	public TorsoManager _torsoManager;

	public FeelerPistonManager _feelerPistonManager;

	public CameraController _cameraController;

	public BindManager _bindManager;

	[Header("Status")]
	public bool initialize;

	public bool isPause;

	[Header("Control")]
	public bool isScreenLock;

	public bool isControl;

	public bool isClickSpank;

	public bool isClickTouch;

	public bool isClickSyringe;

	public bool isClickSuck;

	public bool isClickCharge;

	public bool isClickSqueeze;

	[Space]
	public bool isClickRubA;

	public bool isClickRubB;

	public bool isClickRubC;

	public bool isClickRubD;

	[Space]
	public bool isClickBite;

	public bool isClickBiteA;

	public bool isClickBiteB;

	public bool isClickBiteC;

	public bool isClickBiteD;

	[Header("Skill Data")]
	public List<SkillData> _skillDataKey;

	public List<SkillData> _skillDataMouse;

	public List<int> keyID;

	public List<int> mouseID;

	public int currentSkill;

	[Header("Status Character")]
	public bool isEat;

	public bool isRide;

	public bool isLimbHold;

	public bool isFuck;

	[Header("Status")]
	public bool isClick;

	public bool isFilamentTouch;

	public bool isCoil;

	public bool isVaginaRub;

	public bool isTitsRub;

	public bool isTitsPress;

	public bool isTitsSuck;

	public bool isMobShot;

	public bool isMobTouch;

	public bool isMobSpanking;

	public bool isVaginaOpen;

	public bool isConceive;

	public bool isHideEyes;

	public bool isUpperBind;

	public bool isLowerBind;

	public bool isSqueezeMouse;

	public bool isSqueezeKey;

	[Header("Reaction Target")]
	public CharacterLifeManager _characterLifeManager;

	[Header("Script")]
	public List<FeelerCoilAimObject> _feelerCoilAimObject;

	public List<FeelerShotObject> _feelerShotObject;

	[Space]
	public FeelerRubMultiObject _feelerMultiRubObjectA;

	public FeelerRubMultiObject _feelerMultiRubObjectB;

	public FeelerRubMultiObject _feelerMultiRubObjectC;

	public FeelerRubMultiObject _feelerMultiRubObjectD;

	[Space]
	public FeelerBiteMultiObject _feelerBiteObject;

	public FeelerBiteMultiObject _feelerMultiBiteObjectA;

	public FeelerBiteMultiObject _feelerMultiBiteObjectB;

	public FeelerBiteMultiObject _feelerMultiBiteObjectC;

	public FeelerBiteMultiObject _feelerMultiBiteObjectD;

	[Space]
	public FeelerRubObject _feelerVaginaRubObject;

	public List<FeelerRubObject> _feelerTitsRubObject;

	public List<FeelerTitsSuckObject> _feelerTitsSuckObject;

	public List<FeelerMilkTankObject> _feelerMilkTankObject;

	public List<FeelerPressTitsObject> _feelerPressTitsObject;

	[Space]
	public FeelerSpankingObject _feelerSpankingObject;

	public FeelerSyringeObject _feelerSyringeObject;

	public FeelerSuckObject _feelerSuckObject;

	public FilamentVaginaOpenObject _filamentVaginaOpenObject;

	public FilamentTouchManager _filamentTouchManager;

	public FilamentTouchPlayerManager _filamentTouchPlayerManager;

	public FeelerSpankingMobManager _feelerSpankingMobManager;

	public ChildFeelerManager _childFeelerManager;

	public CharacterPositionManager _characterPositionManager;

	public GuildCardObject _guildCardObject;

	public FeelerPaintPlayerObject _feelerPaintPlayerObject;

	[Header("Mob Shot")]
	public FeelerShotMobManager _feelerShotMobManager;

	[Header("SE")]
	public AudioClip clickSe;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
	}

	private void Start()
	{
		MobShot(40);
		_feelerPaintPlayerObject.SetSkillID(118, 119);
	}

	public void MouseCheck(int num, bool up)
	{
		if (isPause || isScreenLock || _skillGUIQuickSet.isShow || isClickCharge)
		{
			return;
		}
		if (mouseID[num] == 1 && up)
		{
			for (int i = 0; i < _feelerShotObject.Count; i++)
			{
				_feelerShotObject[i].SetMeltPower(0);
				_feelerShotObject[i].ShotReady();
			}
		}
		else if (mouseID[num] == 2 && up)
		{
			for (int j = 0; j < _feelerShotObject.Count; j++)
			{
				_feelerShotObject[j].SetMeltPower(1);
				_feelerShotObject[j].ShotReady();
			}
		}
		else if (mouseID[num] == 3 && up)
		{
			for (int k = 0; k < _feelerShotObject.Count; k++)
			{
				_feelerShotObject[k].SetMeltPower(2);
				_feelerShotObject[k].ShotReady();
			}
		}
		else if (mouseID[num] == 6 && !up)
		{
			isClickRubA = true;
			if (!_feelerMultiRubObjectA.isRub)
			{
				_feelerMultiRubObjectA.RubAimA(value: true);
			}
		}
		else if (mouseID[num] == 6 && up && isClickRubA)
		{
			isClickRubA = false;
			if (!_feelerMultiRubObjectA.isRub)
			{
				_feelerMultiRubObjectA.Rub(6, 0);
				_feelerMultiRubObjectA.RubAimA(value: false);
			}
			else
			{
				_feelerMultiRubObjectA.RubSet(6, value: false);
			}
		}
		else if (mouseID[num] == 7 && !up)
		{
			isClickRubB = true;
			if (!_feelerMultiRubObjectB.isRub)
			{
				_feelerMultiRubObjectB.RubAimB(value: true);
			}
		}
		else if (mouseID[num] == 7 && up && isClickRubB)
		{
			isClickRubB = false;
			if (!_feelerMultiRubObjectB.isRub)
			{
				_feelerMultiRubObjectB.Rub(7, 1);
				_feelerMultiRubObjectB.RubAimB(value: false);
			}
			else
			{
				_feelerMultiRubObjectB.RubSet(7, value: false);
			}
		}
		else if (mouseID[num] == 8 && !up)
		{
			isClickRubC = true;
			if (!_feelerMultiRubObjectC.isRub)
			{
				_feelerMultiRubObjectC.RubAimC(value: true);
			}
		}
		else if (mouseID[num] == 8 && up && isClickRubC)
		{
			isClickRubC = false;
			if (!_feelerMultiRubObjectC.isRub)
			{
				_feelerMultiRubObjectC.Rub(8, 2);
				_feelerMultiRubObjectC.RubAimC(value: false);
			}
			else
			{
				_feelerMultiRubObjectC.RubSet(8, value: false);
			}
		}
		else if (mouseID[num] == 9 && !up)
		{
			isClickRubD = true;
			if (!_feelerMultiRubObjectD.isRub)
			{
				_feelerMultiRubObjectD.RubAimD(value: true);
			}
		}
		else if (mouseID[num] == 9 && up && isClickRubD)
		{
			isClickRubD = false;
			if (!_feelerMultiRubObjectD.isRub)
			{
				_feelerMultiRubObjectD.Rub(9, 3);
				_feelerMultiRubObjectD.RubAimD(value: false);
			}
			else
			{
				_feelerMultiRubObjectD.RubSet(9, value: false);
			}
		}
		else if (mouseID[num] == 16 && !up)
		{
			isClickBite = true;
			_feelerBiteObject.BiteAim(value: true);
		}
		else if (mouseID[num] == 16 && up && isClickBite)
		{
			isClickBite = false;
			_feelerBiteObject.Bite(16, -1);
			_feelerBiteObject.BiteAim(value: false);
		}
		else if (mouseID[num] == 101 && !up)
		{
			isClickBiteA = true;
			if (!_feelerMultiBiteObjectA.isBite)
			{
				_feelerMultiBiteObjectA.BiteAimA(value: true);
			}
		}
		else if (mouseID[num] == 101 && up && isClickBiteA)
		{
			isClickBiteA = false;
			if (!_feelerMultiBiteObjectA.isBite)
			{
				_feelerMultiBiteObjectA.Bite(101, 0);
				_feelerMultiBiteObjectA.BiteAimA(value: false);
			}
			else
			{
				_feelerMultiBiteObjectA.BiteSet(101, value: false);
			}
		}
		else if (mouseID[num] == 102 && !up)
		{
			isClickBiteB = true;
			if (!_feelerMultiBiteObjectB.isBite)
			{
				_feelerMultiBiteObjectB.BiteAimB(value: true);
			}
		}
		else if (mouseID[num] == 102 && up && isClickBiteB)
		{
			isClickBiteB = false;
			if (!_feelerMultiBiteObjectB.isBite)
			{
				_feelerMultiBiteObjectB.Bite(102, 1);
				_feelerMultiBiteObjectB.BiteAimB(value: false);
			}
			else
			{
				_feelerMultiBiteObjectB.BiteSet(102, value: false);
			}
		}
		else if (mouseID[num] == 103 && !up)
		{
			isClickBiteC = true;
			if (!_feelerMultiBiteObjectC.isBite)
			{
				_feelerMultiBiteObjectC.BiteAimC(value: true);
			}
		}
		else if (mouseID[num] == 103 && up && isClickBiteC)
		{
			isClickBiteC = false;
			if (!_feelerMultiBiteObjectC.isBite)
			{
				_feelerMultiBiteObjectC.Bite(103, 2);
				_feelerMultiBiteObjectC.BiteAimC(value: false);
			}
			else
			{
				_feelerMultiBiteObjectC.BiteSet(103, value: false);
			}
		}
		else if (mouseID[num] == 104 && !up)
		{
			isClickBiteD = true;
			if (!_feelerMultiBiteObjectD.isBite)
			{
				_feelerMultiBiteObjectD.BiteAimD(value: true);
			}
		}
		else if (mouseID[num] == 104 && up && isClickBiteD)
		{
			isClickBiteD = false;
			if (!_feelerMultiBiteObjectD.isBite)
			{
				_feelerMultiBiteObjectD.Bite(104, 3);
				_feelerMultiBiteObjectD.BiteAimD(value: false);
			}
			else
			{
				_feelerMultiBiteObjectD.BiteSet(104, value: false);
			}
		}
		else if (mouseID[num] == 10 && !up)
		{
			isClickSpank = true;
			_feelerSpankingObject.SpankingAim(value: true);
		}
		else if (mouseID[num] == 10 && up && isClickSpank)
		{
			isClickSpank = false;
			_feelerSpankingObject.Spanking(10);
			_feelerSpankingObject.SpankingAim(value: false);
		}
		else if (mouseID[num] == 11 && !up)
		{
			isClickTouch = true;
			FilamentTouch(value: true);
		}
		else if (mouseID[num] == 11 && up && isClickTouch)
		{
			isClickTouch = false;
			FilamentTouch(value: false);
		}
		else if (mouseID[num] == 12 && !up)
		{
			isClickSyringe = true;
			_feelerSyringeObject.SyringeAim(value: true);
		}
		else if (mouseID[num] == 12 && up && isClickSyringe)
		{
			isClickSyringe = false;
			_feelerSyringeObject.Syringe(12);
			_feelerSyringeObject.SyringeAim(value: false);
		}
		else if (mouseID[num] == 13 && !up)
		{
			_cameraController.Charge();
			MouseReset();
		}
		else if (mouseID[num] == 14 && !up)
		{
			isClickSuck = true;
			_feelerSuckObject.SuckAim(value: true);
		}
		else if (mouseID[num] == 14 && up && isClickSuck)
		{
			isClickSuck = false;
			_feelerSuckObject.Suck(14);
			_feelerSuckObject.SuckAim(value: false);
		}
		else if (mouseID[num] == 15 && !up)
		{
			isClickSqueeze = true;
			SqueezeMouse(value: true);
		}
		else if (mouseID[num] == 15 && up && isClickSqueeze)
		{
			isClickSqueeze = false;
			SqueezeMouse(value: false);
		}
	}

	public void MouseDisable(int num)
	{
		switch (num)
		{
		case 6:
			if (_feelerMultiRubObjectA.isRub)
			{
				_feelerMultiRubObjectA.RubSet(6, value: false);
				_skillGUIManager.PlaySe(clickSe);
			}
			break;
		case 7:
			if (_feelerMultiRubObjectB.isRub)
			{
				_feelerMultiRubObjectB.RubSet(7, value: false);
				_skillGUIManager.PlaySe(clickSe);
			}
			break;
		case 8:
			if (_feelerMultiRubObjectC.isRub)
			{
				_feelerMultiRubObjectC.RubSet(8, value: false);
				_skillGUIManager.PlaySe(clickSe);
			}
			break;
		case 9:
			if (_feelerMultiRubObjectD.isRub)
			{
				_feelerMultiRubObjectD.RubSet(9, value: false);
				_skillGUIManager.PlaySe(clickSe);
			}
			break;
		}
		if (num == 16 && _feelerBiteObject.isBite)
		{
			_feelerBiteObject.BiteSet(16, value: false);
			_skillGUIManager.PlaySe(clickSe);
		}
		switch (num)
		{
		case 101:
			if (_feelerMultiBiteObjectA.isBite)
			{
				_feelerMultiBiteObjectA.BiteSet(101, value: false);
				_skillGUIManager.PlaySe(clickSe);
			}
			break;
		case 102:
			if (_feelerMultiBiteObjectB.isBite)
			{
				_feelerMultiBiteObjectB.BiteSet(102, value: false);
				_skillGUIManager.PlaySe(clickSe);
			}
			break;
		case 103:
			if (_feelerMultiBiteObjectC.isBite)
			{
				_feelerMultiBiteObjectC.BiteSet(103, value: false);
				_skillGUIManager.PlaySe(clickSe);
			}
			break;
		case 104:
			if (_feelerMultiBiteObjectD.isBite)
			{
				_feelerMultiBiteObjectD.BiteSet(104, value: false);
				_skillGUIManager.PlaySe(clickSe);
			}
			break;
		}
	}

	public void MouseReset()
	{
		Debug.LogError("Mouse Reset");
		isClickSpank = false;
		_feelerSpankingObject.SpankingAim(value: false);
		isClickTouch = false;
		FilamentTouch(value: false);
		isClickSyringe = false;
		_feelerSyringeObject.SyringeAim(value: false);
		isClickSuck = false;
		_feelerSuckObject.SuckAim(value: false);
		if (isClickSqueeze)
		{
			isClickSqueeze = false;
			SqueezeMouse(value: false);
		}
		if (isClickRubA)
		{
			isClickRubA = false;
			_feelerMultiRubObjectA.RubAimA(value: false);
		}
		if (isClickRubB)
		{
			isClickRubB = false;
			_feelerMultiRubObjectB.RubAimB(value: false);
		}
		if (isClickRubC)
		{
			isClickRubC = false;
			_feelerMultiRubObjectC.RubAimC(value: false);
		}
		if (isClickRubD)
		{
			isClickRubD = false;
			_feelerMultiRubObjectD.RubAimD(value: false);
		}
		if (isClickBite)
		{
			isClickBite = false;
			_feelerBiteObject.BiteAim(value: false);
		}
		if (isClickBiteA)
		{
			isClickBiteA = false;
			_feelerMultiBiteObjectA.BiteAimA(value: false);
		}
		if (isClickBiteB)
		{
			isClickBiteB = false;
			_feelerMultiBiteObjectB.BiteAimB(value: false);
		}
		if (isClickBiteC)
		{
			isClickBiteC = false;
			_feelerMultiBiteObjectC.BiteAimC(value: false);
		}
		if (isClickBiteD)
		{
			isClickBiteD = false;
			_feelerMultiBiteObjectD.BiteAimD(value: false);
		}
	}

	public void KeyCheck(int num, bool direct)
	{
		if (direct)
		{
			currentSkill = num;
		}
		else if (!direct)
		{
			currentSkill = keyID[num];
		}
		if (isPause || _skillGUIQuickSet.isShow)
		{
			return;
		}
		if (currentSkill == 20)
		{
			Eat(20);
		}
		else if (currentSkill == 21)
		{
			Ride(21);
		}
		else if (currentSkill == 22)
		{
			LimbHold(22);
		}
		else if (currentSkill == 23)
		{
			WartBed(23);
		}
		else if (currentSkill == 24)
		{
			WallHip(24);
		}
		else if (currentSkill == 25)
		{
			PillarBind(25);
		}
		else if (currentSkill == 30)
		{
			BackFuck(30);
		}
		else if (currentSkill == 31)
		{
			FrontFuck(31);
		}
		else if (currentSkill == 32)
		{
			RideFuck(32);
		}
		else if (currentSkill == 33)
		{
			LiftFuck(33);
		}
		else if (currentSkill == 34)
		{
			SideFuck(34);
		}
		else if (currentSkill == 35)
		{
			DoggyFuck(35);
		}
		else if (currentSkill == 39)
		{
			DisableAll(39);
		}
		else if (currentSkill == 40)
		{
			MobShot(40);
		}
		else if (currentSkill == 41)
		{
			MobTouch(41);
		}
		else if (currentSkill == 42)
		{
			MobSpanking(42);
		}
		else if (currentSkill == 47)
		{
			UpperBind();
		}
		else if (currentSkill == 48)
		{
			LowerBind();
		}
		else if (currentSkill == 49)
		{
			_feelerShotMobManager.Shot(49);
		}
		else if (currentSkill == 50)
		{
			TitsRub();
			if (isTitsRub && isTitsSuck)
			{
				TitsSuck();
			}
		}
		else if (currentSkill == 51)
		{
			TitsPress();
		}
		else if (currentSkill == 52)
		{
			TitsSuck();
			if (isTitsRub && isTitsSuck)
			{
				TitsRub();
			}
		}
		else if (currentSkill == 55)
		{
			SqueezeKey();
		}
		else if (currentSkill == 58 && !_characterLifeManager.isVaginaWait && !isRide && !isFuck)
		{
			VaginaRub(disable: true);
		}
		else if (currentSkill == 59 && !isRide && !isFuck)
		{
			VaginaOpen();
		}
		else if (currentSkill == 60 && !_characterLifeManager.isTitsWait)
		{
			TitsInsert();
		}
		else if (currentSkill == 61 && !_characterLifeManager.isTitsWait)
		{
			TitsPiston();
		}
		else if (currentSkill == 65 && !_characterLifeManager.isTitsWait)
		{
			TitsShot();
		}
		else if (currentSkill == 70 && !_characterLifeManager.isMouthWait && !isEat)
		{
			MouthInsert();
		}
		else if (currentSkill == 71 && !_characterLifeManager.isMouthWait && !isEat)
		{
			MouthPiston();
		}
		else if (currentSkill == 75 && !_characterLifeManager.isMouthWait && !isEat)
		{
			MouthShot();
		}
		else if (currentSkill == 79 && !_characterLifeManager.isMouthWait && !isEat)
		{
			EyesHide();
		}
		else if (currentSkill == 80 && !_characterLifeManager.isAnalWait)
		{
			AnalInsert();
		}
		else if (currentSkill == 81 && !_characterLifeManager.isAnalWait)
		{
			AnalPiston();
		}
		else if (currentSkill == 85 && !_characterLifeManager.isAnalWait)
		{
			AnalShot();
		}
		else if (currentSkill == 88 && !_characterLifeManager.isAnalWait && !isRide && !isFuck)
		{
			AnalBirth();
		}
		else if (currentSkill == 90 && !_characterLifeManager.isVaginaWait)
		{
			VaginaInsert(disable: true);
		}
		else if (currentSkill == 91 && !_characterLifeManager.isVaginaWait)
		{
			VaginaPiston(disable: true);
		}
		else if (currentSkill == 95 && !_characterLifeManager.isVaginaWait)
		{
			VaginaShot(disable: true);
		}
		else if (currentSkill == 98 && !_characterLifeManager.isVaginaWait && !isRide)
		{
			VaginaBirth();
		}
		else if (currentSkill == 111)
		{
			UseLight(111);
		}
		else if (currentSkill == 112)
		{
			UseGuildCard(112);
		}
		else if (currentSkill == 118)
		{
			UseMelt(118);
		}
		else if (currentSkill == 119)
		{
			UseRestore(119);
		}
		else if (currentSkill == 121)
		{
			CostumeChange(0);
		}
		else if (currentSkill == 122)
		{
			CostumeChange(1);
		}
		else if (currentSkill == 123)
		{
			CostumeChange(2);
		}
		else if (currentSkill == 124)
		{
			CostumeChange(3);
		}
		else if (currentSkill == 125)
		{
			CostumeChange(4);
		}
		else if (currentSkill == 126)
		{
			CostumeChange(5);
		}
		else if (currentSkill == 127)
		{
			CostumeChange(6);
		}
		else if (currentSkill == 128)
		{
			CostumeChange(7);
		}
	}

	public void BukkakeKeyCheck(int num)
	{
		currentSkill = num;
		if (isPause || _skillGUIQuickSet.isShow)
		{
			return;
		}
		if (currentSkill == 0)
		{
			for (int i = 0; i < _feelerShotObject.Count; i++)
			{
				_feelerShotObject[i].SetMeltPower(0);
				_feelerShotObject[i].ShotReady();
			}
		}
		if (currentSkill == 1)
		{
			for (int j = 0; j < _feelerShotObject.Count; j++)
			{
				_feelerShotObject[j].SetMeltPower(1);
				_feelerShotObject[j].ShotReady();
			}
		}
		if (currentSkill == 2)
		{
			for (int k = 0; k < _feelerShotObject.Count; k++)
			{
				_feelerShotObject[k].SetMeltPower(2);
				_feelerShotObject[k].ShotReady();
			}
		}
	}

	public void ReloadMouseID()
	{
		for (int i = 0; i < mouseID.Count; i++)
		{
			if (_skillDataMouse[i] != null)
			{
				mouseID[i] = _skillDataMouse[i].skillID;
			}
			else
			{
				mouseID[i] = 0;
			}
		}
	}

	public void ReloadKeyID()
	{
		for (int i = 0; i < keyID.Count; i++)
		{
			if (_skillDataKey[i] != null)
			{
				keyID[i] = _skillDataKey[i].skillID;
			}
			else
			{
				keyID[i] = 0;
			}
		}
	}

	public void UseLight(int value)
	{
		_systemCore.PlayerLight(value);
	}

	public void UseGuildCard(int value)
	{
		_guildCardObject.SetGuildCard(value);
	}

	public void UseMelt(int value)
	{
		_feelerPaintPlayerObject.Melt(value);
	}

	public void UseRestore(int value)
	{
		_feelerPaintPlayerObject.Restore(value);
	}

	public void CostumeChange(int value)
	{
		_torsoManager.CostumeChange(value);
	}

	public void VaginaInsert(bool disable)
	{
		if (isVaginaRub && disable)
		{
			VaginaRub(disable: false);
		}
		_feelerPistonManager.VaginaInsert();
	}

	public void VaginaPiston(bool disable)
	{
		if (isVaginaRub && disable)
		{
			VaginaRub(disable: false);
		}
		_feelerPistonManager.VaginaPiston();
	}

	public void VaginaShot(bool disable)
	{
		if (isVaginaRub && disable)
		{
			VaginaRub(disable: false);
		}
		_feelerPistonManager.VaginaShot();
	}

	public void VaginaShotEnd()
	{
		_feelerPistonManager.VaginaShotEnd();
	}

	public void VaginaBirth()
	{
		_feelerPistonManager.VaginaBirth();
	}

	public void AnalInsert()
	{
		_feelerPistonManager.AnalInsert();
	}

	public void AnalPiston()
	{
		_feelerPistonManager.AnalPiston();
	}

	public void AnalShot()
	{
		_feelerPistonManager.AnalShot();
	}

	public void AnalShotEnd()
	{
		_feelerPistonManager.AnalShotEnd();
	}

	public void AnalBirth()
	{
		_feelerPistonManager.AnalBirth();
	}

	public void MouthInsert()
	{
		_feelerPistonManager.MouthInsert();
	}

	public void MouthPiston()
	{
		_feelerPistonManager.MouthPiston();
	}

	public void MouthShot()
	{
		_feelerPistonManager.MouthShot();
	}

	public void MouthShotEnd()
	{
		_feelerPistonManager.MouthShotEnd();
	}

	public void TitsInsert()
	{
		_feelerPistonManager.TitsInsert();
	}

	public void TitsPiston()
	{
		_feelerPistonManager.TitsPiston();
	}

	public void TitsShot()
	{
		_feelerPistonManager.TitsShot();
	}

	public void TitsShotEnd()
	{
		_feelerPistonManager.TitsShotEnd();
	}

	public void EyesHide()
	{
		isHideEyes = !isHideEyes;
		SkillGUIDataBase.instance.SetEnable(79, isHideEyes);
		_bindManager.SetHeadEyes(isHideEyes);
	}

	public void UpperBind()
	{
		isUpperBind = !isUpperBind;
		SkillGUIDataBase.instance.SetEnable(47, isUpperBind);
		_bindManager.SetUpperBind(isUpperBind);
	}

	public void LowerBind()
	{
		isLowerBind = !isLowerBind;
		SkillGUIDataBase.instance.SetEnable(48, isLowerBind);
		_bindManager.SetLowerBind(isLowerBind);
	}

	public void SqueezeMouse(bool value)
	{
		isSqueezeMouse = value;
		isSqueezeKey = false;
		SkillGUIDataBase.instance.SetEnable(55, value: false);
		_bindManager.SetSqueeze(isSqueezeMouse);
	}

	public void SqueezeKey()
	{
		if (!isSqueezeMouse)
		{
			isSqueezeKey = !isSqueezeKey;
			SkillGUIDataBase.instance.SetEnable(55, isSqueezeKey);
			_bindManager.SetSqueeze(isSqueezeKey);
		}
	}

	public void VaginaRub(bool disable)
	{
		if (_feelerPistonManager.isVaginaInsert && disable)
		{
			VaginaInsert(disable: false);
		}
		isVaginaRub = !isVaginaRub;
		SkillGUIDataBase.instance.SetEnable(58, isVaginaRub);
		_feelerVaginaRubObject.isAim = isVaginaRub;
	}

	public void TitsRub()
	{
		isTitsRub = !isTitsRub;
		SkillGUIDataBase.instance.SetEnable(50, isTitsRub);
		for (int i = 0; i < _feelerTitsRubObject.Count; i++)
		{
			_feelerTitsRubObject[i].isAim = isTitsRub;
		}
	}

	public void TitsPress()
	{
		isTitsPress = !isTitsPress;
		SkillGUIDataBase.instance.SetEnable(51, isTitsPress);
		for (int i = 0; i < _feelerPressTitsObject.Count; i++)
		{
			_feelerPressTitsObject[i].isAim = isTitsPress;
		}
	}

	public void TitsSuck()
	{
		isTitsSuck = !isTitsSuck;
		SkillGUIDataBase.instance.SetEnable(52, isTitsSuck);
		for (int i = 0; i < _feelerTitsSuckObject.Count; i++)
		{
			_feelerTitsSuckObject[i].isAim = isTitsSuck;
			if (!isTitsSuck)
			{
				_feelerTitsSuckObject[i].SetGag(value: false);
			}
		}
	}

	public void TitsMilkCheck()
	{
		if (!_characterEffectManager.isTitsMilk)
		{
			_characterEffectManager.isTitsMilk = true;
			_characterEffectManager.TitsSplashL(1f);
			_characterEffectManager.TitsSplashR(1f);
			for (int i = 0; i < _feelerTitsSuckObject.Count; i++)
			{
				_feelerTitsSuckObject[i].isTitsMilk = true;
				_feelerMilkTankObject[i].isTitsMilk = true;
				_feelerMilkTankObject[i].MilkOn();
			}
		}
	}

	public void MobShot(int SkillID)
	{
		isMobShot = !isMobShot;
		SkillGUIDataBase.instance.SetEnable(SkillID, isMobShot);
		_feelerShotMobManager.AutoShot(isMobShot);
	}

	public void FilamentTouch(bool value)
	{
		isFilamentTouch = isClickTouch;
		_filamentTouchPlayerManager.SetSearch(isFilamentTouch);
	}

	public void MobTouch(int SkillID)
	{
		isMobTouch = !isMobTouch;
		SkillGUIDataBase.instance.SetEnable(SkillID, isMobTouch);
		_filamentTouchManager.SetSearch(isMobTouch);
	}

	public void MobSpanking(int SkillID)
	{
		isMobSpanking = !isMobSpanking;
		SkillGUIDataBase.instance.SetEnable(SkillID, isMobSpanking);
		_feelerSpankingMobManager.isSet = isMobSpanking;
	}

	public void Eat(int SkillID)
	{
		_characterPositionManager.Eat(SkillID);
	}

	public void Ride(int SkillID)
	{
		_characterPositionManager.Ride(SkillID);
	}

	public void LimbHold(int SkillID)
	{
		_characterPositionManager.LimbHold(SkillID);
	}

	public void WartBed(int SkillID)
	{
		_characterPositionManager.WartBed(SkillID);
	}

	public void WallHip(int SkillID)
	{
		_characterPositionManager.WallHip(SkillID);
	}

	public void PillarBind(int SkillID)
	{
		_characterPositionManager.PillarBind(SkillID);
	}

	public void BackFuck(int SkillID)
	{
		_characterPositionManager.BackFuck(SkillID);
	}

	public void FrontFuck(int SkillID)
	{
		_characterPositionManager.FrontFuck(SkillID);
	}

	public void RideFuck(int SkillID)
	{
		_characterPositionManager.RideFuck(SkillID);
	}

	public void LiftFuck(int SkillID)
	{
		_characterPositionManager.LiftFuck(SkillID);
	}

	public void SideFuck(int SkillID)
	{
		_characterPositionManager.SideFuck(SkillID);
	}

	public void DoggyFuck(int SkillID)
	{
		_characterPositionManager.DoggyFuck(SkillID);
	}

	public void DisableAll(int SkillID)
	{
		_characterPositionManager.DisableAll(SkillID);
	}

	public void VaginaOpen()
	{
		isVaginaOpen = !isVaginaOpen;
		SkillGUIDataBase.instance.SetEnable(59, isVaginaOpen);
		_filamentVaginaOpenObject.AimSet(isVaginaOpen);
	}
}
