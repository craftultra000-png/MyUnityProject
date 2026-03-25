using UnityEngine;

public class FeelerPistonManager : MonoBehaviour
{
	public FeelerControllerData _feelerControllerData;

	public QuickSkillGUI _quickSkillGUI;

	public CharacterAnimationSpeedManager _characterAnimationSpeedManager;

	public CharacterTalkManager _characterTalkManager;

	[Header("Type")]
	public bool isRide;

	public bool isFuck;

	[Header("Status")]
	public bool isVaginaInsert;

	public bool isVaginaPiston;

	public bool isVaginaShot;

	public bool isAnalInsert;

	public bool isAnalPiston;

	public bool isAnalShot;

	public bool isMouthInsert;

	public bool isMouthPiston;

	public bool isMouthShot;

	public bool isTitsInsert;

	public bool isTitsPiston;

	public bool isTitsShot;

	[Header("CoolTime")]
	public bool isCoolTimeVagina;

	public bool isCoolTimeAnal;

	public bool isCoolTimeMouth;

	public bool isCoolTimeTits;

	[Space]
	public float coolTimeVagina;

	public float coolTimeAnal;

	public float coolTimeMouth;

	public float coolTimeTits;

	[Space]
	public float coolTimeInsert = 0.5f;

	public float coolTimePiston = 0.5f;

	public float coolTimeShot = 3.5f;

	[Header("Reaction Target")]
	public ReactionTargetAnimaton _reactionTargetAnimaton;

	public CharacterFaceManager _characterFaceManager;

	public CharacterEyesManager _characterEyesManager;

	public CharacterMouthManager _characterMouthManager;

	public CharacterLifeManager _characterLifeManager;

	public MotionAnimancer _characterAnimancer;

	[Header("Selection GUI")]
	public SelectionManager _selectionManager;

	[Header("Piston Vagina")]
	public CharacterVaginaManager _characterVaginaManager;

	public UterusController _uterusController;

	public UterusChildFeelerManager _uterusChildFeelerManager;

	[Space]
	public PistonAnimancer _feelerVaginaPistonAnimancer;

	public FeelerPistonObject _feelerVaginaPistonObject;

	[Space]
	public PistonAnimancer _feelerVaginaPistonRideAnimancer;

	public FeelerPistonShortOpbject _feelerVaginaPistonRideObject;

	[Header("Piston Anal")]
	public CharacterAnalManager _characterAnalManager;

	public AnalController _analController;

	public AnalChildFeelerManager _analChildFeelerManager;

	[Space]
	public PistonAnimancer _feelerAnalPistonAnimancer;

	public FeelerPistonObject _feelerAnalPistonObject;

	[Space]
	public PistonAnimancer _feelerAnalPistonRideAnimancer;

	public FeelerPistonShortOpbject _feelerAnalPistonRideObject;

	[Header("Piston Mouth")]
	public PistonAnimancer _feelerMouthAnimancer;

	public FeelerPistonObject _feelerMouthPistonObject;

	[Header("Piston Tits")]
	public PistonAnimancer _feelerTitsAnimancer;

	public FeelerPistonObject _feelerTitsPistonObject;

	[Header("Script")]
	public FilamentVaginaOpenObject _filamentVaginaOpenObject;

	public ChildFeelerManager _childFeelerManager;

	private void Start()
	{
		_characterVaginaManager.MosaicInsertOff();
		_characterAnalManager.MosaicInsertOff();
	}

	private void LateUpdate()
	{
		if (isCoolTimeVagina)
		{
			coolTimeVagina -= Time.deltaTime;
			if (coolTimeVagina < 0f)
			{
				isCoolTimeVagina = false;
				_characterLifeManager.VaginaShotEnd();
			}
		}
		if (isCoolTimeAnal)
		{
			coolTimeAnal -= Time.deltaTime;
			if (coolTimeAnal < 0f)
			{
				isCoolTimeAnal = false;
				_characterLifeManager.AnalShotEnd();
			}
		}
		if (isCoolTimeMouth)
		{
			coolTimeMouth -= Time.deltaTime;
			if (coolTimeMouth < 0f)
			{
				isCoolTimeMouth = false;
				_characterLifeManager.MouthShotEnd();
			}
		}
		if (isCoolTimeTits)
		{
			coolTimeTits -= Time.deltaTime;
			if (coolTimeTits < 0f)
			{
				isCoolTimeTits = false;
				_characterLifeManager.TitsShotEnd();
			}
		}
	}

	public void VaginaInsert()
	{
		Debug.LogError("Vagina Insert:" + isVaginaInsert);
		if (!isVaginaInsert)
		{
			isVaginaInsert = true;
			isVaginaPiston = false;
			isVaginaShot = false;
			_uterusChildFeelerManager.isVaginaInsert = true;
			_characterAnimancer.isVaginaPiston = isVaginaPiston;
			VaginaSetCoolTimeStatus(coolTimeInsert);
			VaginaSetStatus();
			_characterAnimationSpeedManager.isVaginaPiston = isVaginaPiston;
			CharacterMotinon("isInsert", vagina: true, anal: false);
			if (isRide)
			{
				_feelerVaginaPistonRideAnimancer.StateSet("isInsert", 0.25f);
				_feelerVaginaPistonRideObject.isAim = true;
			}
			else if (!isFuck)
			{
				_feelerVaginaPistonAnimancer.StateSet("isInsert", 0.25f);
				_feelerVaginaPistonObject.isAim = true;
			}
			_uterusController.Insert(isVaginaInsert);
			_selectionManager.SelectionVaginaSlide(value: true);
			_characterVaginaManager.MosaicInsertOn();
			_characterVaginaManager.SetPaint3D();
			VaginaOpenFeeler();
			_characterLifeManager.isVaginaGag = true;
			_characterAnimancer.isInsert = true;
			_characterAnimancer.isVaginaInsert = true;
			if (_characterAnimancer._motionFuckAnimancer != null)
			{
				_characterLifeManager.HitData("Vagina", "Insert");
				_characterAnimancer._motionFuckAnimancer.isInsert = true;
				_characterAnimancer._motionFuckAnimancer.StateSet("isInsert", 0.25f);
			}
			return;
		}
		isVaginaInsert = false;
		isVaginaPiston = false;
		isVaginaShot = false;
		_uterusChildFeelerManager.isVaginaInsert = false;
		_characterAnimancer.isVaginaPiston = isVaginaPiston;
		VaginaSetCoolTimeStatus(coolTimeInsert);
		VaginaSetStatus();
		_characterAnimationSpeedManager.isVaginaPiston = isVaginaPiston;
		CharacterMotinon("isIdle", vagina: true, anal: false);
		if (isRide)
		{
			_feelerVaginaPistonRideAnimancer.StateSet("isIdle", 0.25f);
			_feelerVaginaPistonRideObject.isAim = false;
		}
		_feelerVaginaPistonAnimancer.StateSet("isIdle", 0.25f);
		_feelerVaginaPistonObject.isAim = false;
		_feelerVaginaPistonObject.isFirstInsert = false;
		_feelerVaginaPistonRideObject.isFirstInsert = false;
		_uterusController.Piston(isVaginaPiston);
		_selectionManager.SelectionVaginaSlide(value: false);
		_characterFaceManager._characterEffectManager.VaginaGlobs();
		_characterVaginaManager.MosaicInsertOff();
		VaginaOpenFeeler();
		_characterLifeManager.isVaginaGag = false;
		_characterLifeManager.GagBirthChild();
		_characterAnimancer.isInsert = false;
		_characterAnimancer.isVaginaInsert = false;
		_characterAnimancer.isPiston = false;
		if (_characterAnimancer._motionFuckAnimancer != null)
		{
			_characterAnimancer.StateSet("isIdle", 0.25f);
			_characterAnimancer._motionFuckAnimancer.isInsert = false;
			_characterAnimancer._motionFuckAnimancer.StateSet("isIdle", 0.25f);
		}
	}

	public void VaginaPiston()
	{
		Debug.LogError("Vagina Piston: " + isVaginaPiston);
		if (!isVaginaPiston)
		{
			isVaginaInsert = true;
			isVaginaPiston = true;
			isVaginaShot = false;
			_uterusChildFeelerManager.isVaginaInsert = true;
			_characterAnimancer.isVaginaPiston = isVaginaPiston;
			VaginaSetCoolTimeStatus(coolTimePiston);
			VaginaSetStatus();
			_characterAnimationSpeedManager.isVaginaPiston = isVaginaPiston;
			CharacterMotinon("isPiston", vagina: true, anal: false);
			if (isRide)
			{
				_feelerVaginaPistonRideAnimancer.StateSet("isPiston", 0.25f);
				_feelerVaginaPistonRideObject.isAim = true;
			}
			else if (!isFuck)
			{
				_feelerVaginaPistonAnimancer.StateSet("isPiston", 0.25f);
				_feelerVaginaPistonObject.isAim = true;
			}
			_uterusController.Piston(isVaginaPiston);
			_selectionManager.SelectionVaginaSlide(value: true);
			_characterVaginaManager.MosaicInsertOn();
			_characterVaginaManager.SetPaint3D();
			VaginaOpenFeeler();
			_characterLifeManager.isVaginaGag = true;
			_characterAnimancer.isInsert = true;
			_characterAnimancer.isVaginaInsert = true;
			_characterAnimancer.isPiston = true;
			if (_characterAnimancer._motionFuckAnimancer != null)
			{
				_characterAnimancer._motionFuckAnimancer.isInsert = true;
			}
		}
		else
		{
			isVaginaInsert = false;
			isVaginaPiston = false;
			isVaginaShot = false;
			_uterusChildFeelerManager.isVaginaInsert = false;
			_characterAnimancer.isVaginaPiston = isVaginaPiston;
			VaginaInsert();
		}
	}

	public void VaginaShot()
	{
		if (!isVaginaShot)
		{
			isVaginaInsert = true;
			isVaginaPiston = false;
			isVaginaShot = true;
			_uterusChildFeelerManager.isVaginaInsert = true;
			_characterAnimancer.isVaginaPiston = isVaginaPiston;
			VaginaSetCoolTimeStatus(coolTimeShot);
			SkillGUIDataBase.instance.GimmickCoolTime(coolTimeShot);
			VaginaSetStatus();
			_characterLifeManager.isVaginaWait = true;
			isCoolTimeVagina = true;
			coolTimeVagina = coolTimeShot;
			_characterAnimationSpeedManager.isVaginaPiston = isVaginaPiston;
			_characterAnimationSpeedManager.CheckPiston();
			CharacterMotinon("isShot", vagina: true, anal: false);
			if (isRide)
			{
				_feelerVaginaPistonRideAnimancer.StateSet("isShot", 0.25f);
				_feelerVaginaPistonRideObject.isAim = true;
			}
			else if (!isFuck)
			{
				_feelerVaginaPistonAnimancer.StateSet("isShot", 0.25f);
				_feelerVaginaPistonObject.isAim = true;
			}
			_uterusController.Shot(isVaginaShot);
			_selectionManager.SelectionVaginaSlide(value: true);
			_characterVaginaManager.MosaicInsertOn();
			_characterVaginaManager.SetPaint3D();
			VaginaOpenFeeler();
			_characterLifeManager.isVaginaGag = true;
			_characterAnimancer.isInsert = true;
			_characterAnimancer.isVaginaInsert = true;
			_characterAnimancer.isPiston = false;
			if (_characterAnimancer._motionFuckAnimancer != null)
			{
				_characterAnimancer._motionFuckAnimancer.isInsert = true;
			}
		}
	}

	public void VaginaShotEnd()
	{
		Debug.LogError("VaginaShotEnd");
		isVaginaInsert = true;
		isVaginaPiston = false;
		isVaginaShot = false;
		_uterusChildFeelerManager.isVaginaInsert = true;
		_characterAnimancer.isVaginaPiston = isVaginaPiston;
		VaginaSetStatus();
		_characterEyesManager.isVaginaShot = false;
	}

	public void VaginaSetCoolTimeStatus(float value)
	{
		SkillGUIDataBase.instance.SetCoolTime(90, value);
		SkillGUIDataBase.instance.SetCoolTime(91, value);
		SkillGUIDataBase.instance.SetCoolTime(95, value);
		SkillGUIDataBase.instance.SetCoolTime(58, value);
	}

	public void VaginaSetStatus()
	{
		SkillGUIDataBase.instance.SetEnable(90, isVaginaInsert);
		SkillGUIDataBase.instance.SetEnable(91, isVaginaPiston);
		SkillGUIDataBase.instance.SetEnable(95, isVaginaShot);
		_characterEyesManager.isVaginaInsert = isVaginaInsert;
		_characterEyesManager.isVaginaPiston = isVaginaPiston;
		_characterEyesManager.isVaginaShot = isVaginaShot;
	}

	public void VaginaBirth()
	{
		if (!isVaginaInsert)
		{
			_childFeelerManager.VaginaBirth();
		}
	}

	public void VaginaOpenFeeler()
	{
		if (isVaginaPiston || isVaginaShot)
		{
			_filamentVaginaOpenObject.isFeeler = true;
		}
		else
		{
			_filamentVaginaOpenObject.isFeeler = false;
		}
	}

	public void AnalInsert()
	{
		if (!isAnalInsert)
		{
			isAnalInsert = true;
			isAnalPiston = false;
			isAnalShot = false;
			_analChildFeelerManager.isAnalInsert = true;
			_characterAnimancer.isAnalPiston = isAnalPiston;
			AnalSetCoolTimeStatus(coolTimeInsert);
			AnalSetStatus();
			_characterAnimationSpeedManager.isAnalPiston = isAnalPiston;
			if (!isFuck)
			{
				CharacterMotinon("isInsert", vagina: true, anal: false);
			}
			if (isRide)
			{
				_feelerAnalPistonRideAnimancer.StateSet("isInsert", 0.25f);
				_feelerAnalPistonRideObject.isAim = true;
			}
			else
			{
				_feelerAnalPistonAnimancer.StateSet("isInsert", 0.25f);
				_feelerAnalPistonObject.isAim = true;
			}
			_analController.Insert(isAnalInsert);
			_selectionManager.SelectionAnalSlide(value: true);
			_characterAnalManager.MosaicInsertOn();
			_characterAnalManager.SetPaint3D();
			_characterLifeManager.isAnalGag = true;
			return;
		}
		isAnalInsert = false;
		isAnalPiston = false;
		isAnalShot = false;
		_analChildFeelerManager.isAnalInsert = false;
		_characterAnimancer.isAnalPiston = isAnalPiston;
		AnalSetCoolTimeStatus(coolTimeInsert);
		AnalSetStatus();
		_characterAnimationSpeedManager.isAnalPiston = isAnalPiston;
		if (!isFuck)
		{
			CharacterMotinon("isIdle", vagina: false, anal: true);
		}
		if (isRide)
		{
			_feelerAnalPistonRideAnimancer.StateSet("isIdle", 0.25f);
			_feelerAnalPistonRideObject.isAim = false;
		}
		_feelerAnalPistonAnimancer.StateSet("isIdle", 0.25f);
		_feelerAnalPistonObject.isAim = false;
		_feelerAnalPistonObject.isFirstInsert = false;
		_feelerAnalPistonRideObject.isFirstInsert = false;
		_analController.Insert(isAnalInsert);
		_selectionManager.SelectionAnalSlide(value: false);
		_characterFaceManager._characterEffectManager.AnalGlobs();
		_characterAnalManager.MosaicInsertOff();
		_characterLifeManager.isAnalGag = false;
		_characterLifeManager.GagBirthChild();
	}

	public void AnalPiston()
	{
		if (!isAnalPiston)
		{
			isAnalInsert = true;
			isAnalPiston = true;
			isAnalShot = false;
			_analChildFeelerManager.isAnalInsert = true;
			_characterAnimancer.isAnalPiston = isAnalPiston;
			AnalSetCoolTimeStatus(coolTimePiston);
			AnalSetStatus();
			_characterAnimationSpeedManager.isAnalPiston = isAnalPiston;
			if (!isFuck)
			{
				CharacterMotinon("isPiston", vagina: false, anal: true);
			}
			if (isRide)
			{
				_feelerAnalPistonRideAnimancer.StateSet("isPiston", 0.25f);
				_feelerAnalPistonRideObject.isAim = true;
			}
			else
			{
				_feelerAnalPistonAnimancer.StateSet("isPiston", 0.25f);
				_feelerAnalPistonObject.isAim = true;
			}
			_analController.Piston(isAnalPiston);
			_selectionManager.SelectionAnalSlide(value: true);
			_characterAnalManager.MosaicInsertOn();
			_characterAnalManager.SetPaint3D();
			_characterLifeManager.isAnalGag = true;
		}
		else
		{
			isAnalInsert = false;
			isAnalPiston = false;
			isAnalShot = false;
			_analChildFeelerManager.isAnalInsert = false;
			_characterAnimancer.isAnalPiston = isAnalPiston;
			AnalInsert();
		}
	}

	public void AnalShot()
	{
		if (!isAnalShot)
		{
			isAnalInsert = true;
			isAnalPiston = false;
			isAnalShot = true;
			_analChildFeelerManager.isAnalInsert = true;
			_characterAnimancer.isAnalPiston = isAnalPiston;
			AnalSetCoolTimeStatus(coolTimeShot);
			SkillGUIDataBase.instance.GimmickCoolTime(coolTimeShot);
			AnalSetStatus();
			_characterLifeManager.isAnalWait = true;
			isCoolTimeAnal = true;
			coolTimeAnal = coolTimeShot;
			_characterAnimationSpeedManager.isAnalPiston = isAnalPiston;
			_characterAnimationSpeedManager.CheckPiston();
			if (!isFuck)
			{
				CharacterMotinon("isShot", vagina: false, anal: true);
			}
			if (isRide)
			{
				_feelerAnalPistonRideAnimancer.StateSet("isShot", 0.25f);
				_feelerAnalPistonRideObject.isAim = true;
			}
			else
			{
				_feelerAnalPistonAnimancer.StateSet("isShot", 0.25f);
				_feelerAnalPistonObject.isAim = true;
			}
			_analController.Shot(isAnalShot);
			_selectionManager.SelectionAnalSlide(value: true);
			_characterAnalManager.MosaicInsertOn();
			_characterAnalManager.SetPaint3D();
			_characterLifeManager.isAnalGag = true;
		}
	}

	public void AnalShotEnd()
	{
		Debug.LogError("AnalShotEnd");
		isAnalInsert = true;
		isAnalPiston = false;
		isAnalShot = false;
		_analChildFeelerManager.isAnalInsert = true;
		_characterAnimancer.isAnalPiston = isAnalPiston;
		AnalSetStatus();
		_characterEyesManager.isAnalShot = false;
	}

	public void AnalSetCoolTimeStatus(float value)
	{
		SkillGUIDataBase.instance.SetCoolTime(80, value);
		SkillGUIDataBase.instance.SetCoolTime(81, value);
		SkillGUIDataBase.instance.SetCoolTime(85, value);
	}

	public void AnalSetStatus()
	{
		SkillGUIDataBase.instance.SetEnable(80, isAnalInsert);
		SkillGUIDataBase.instance.SetEnable(81, isAnalPiston);
		SkillGUIDataBase.instance.SetEnable(85, isAnalShot);
		_characterEyesManager.isAnalInsert = isAnalInsert;
		_characterEyesManager.isAnalPiston = isAnalPiston;
		_characterEyesManager.isAnalShot = isAnalShot;
	}

	public void AnalBirth()
	{
		if (!isAnalInsert)
		{
			_childFeelerManager.AnalBirth();
		}
	}

	public void CharacterMotinon(string value, bool vagina, bool anal)
	{
		_characterAnimancer.isVaginaPiston = isVaginaPiston;
		_characterAnimancer.isAnalPiston = isAnalPiston;
		_characterAnimancer.isFuck = isFuck;
		if (!isFuck)
		{
			if (!isVaginaPiston && !isAnalPiston && !isVaginaShot && !isAnalShot)
			{
				if (value == "isInsert")
				{
					_characterAnimancer.StateSet("isInsert", 0.25f);
				}
				else
				{
					_characterAnimancer.StateSet("isIdle", 0.25f);
				}
			}
			else if ((isVaginaShot && vagina) || (isAnalShot && anal))
			{
				_characterAnimancer.StateSet("isShot", 0.25f);
			}
			else if (isVaginaPiston || isAnalPiston)
			{
				_characterAnimancer.StateSet("isPiston", 0.25f);
			}
			else
			{
				_characterAnimancer.StateSet("isIdle", 0.25f);
			}
		}
		else if (!isVaginaPiston && !isVaginaShot)
		{
			if (value == "isInsert")
			{
				_characterAnimancer.StateSet("isInsert", 0.25f);
			}
			else
			{
				_characterAnimancer.StateSet("isIdle", 0.25f);
			}
		}
		else if (isVaginaShot && vagina)
		{
			_characterAnimancer.StateSet("isShot", 0.25f);
		}
		else if (isVaginaPiston)
		{
			_characterAnimancer.StateSet("isPiston", 0.25f);
		}
		else
		{
			_characterAnimancer.StateSet("isIdle", 0.25f);
		}
	}

	public void MouthInsert()
	{
		if (!isMouthInsert)
		{
			isMouthInsert = true;
			isMouthPiston = false;
			isMouthShot = false;
			MouthSetCoolTimeStatus(coolTimeInsert);
			MouthSetStatus();
			_feelerMouthAnimancer.StateSet("isInsert", 0.25f);
			_feelerMouthPistonObject.isAim = true;
			_characterMouthManager.mouthOpen = 95f;
			_characterMouthManager.mouthWidth = 95f;
			_reactionTargetAnimaton.targetY += 0.5f;
			MouthGag(value: true);
		}
		else
		{
			isMouthInsert = false;
			isMouthPiston = false;
			isMouthShot = false;
			MouthSetCoolTimeStatus(coolTimeInsert);
			MouthSetStatus();
			_feelerMouthAnimancer.StateSet("isIdle", 0.25f);
			_feelerMouthPistonObject.isAim = false;
			_feelerMouthPistonObject.isFirstInsert = false;
			_characterMouthManager.mouthOpen = 0f;
			_characterMouthManager.mouthWidth = 0f;
			_reactionTargetAnimaton.targetY -= 0.75f;
			MouthGag(value: false);
			_characterFaceManager._characterEffectManager.MouthGlobs();
		}
	}

	public void MouthGag(bool value)
	{
		_characterMouthManager.isHardBreath = value;
		_characterMouthManager.isMouthGag = value;
		_characterLifeManager.isMouthGag = value;
		_characterTalkManager.mouthGag = value;
	}

	public void MouthPiston()
	{
		if (!isMouthPiston)
		{
			isMouthInsert = true;
			isMouthPiston = true;
			isMouthShot = false;
			MouthSetCoolTimeStatus(coolTimePiston);
			MouthSetStatus();
			_feelerMouthAnimancer.StateSet("isPiston", 0.25f);
			_feelerMouthPistonObject.isAim = true;
			_characterMouthManager.mouthOpen = 95f;
			_characterMouthManager.mouthWidth = 95f;
			_reactionTargetAnimaton.targetY += 0.5f;
			_characterMouthManager.isHardBreath = true;
			_characterMouthManager.isMouthGag = true;
			_characterLifeManager.isMouthGag = true;
			_characterTalkManager.mouthGag = true;
		}
		else
		{
			isMouthInsert = false;
			isMouthPiston = false;
			isMouthShot = false;
			MouthInsert();
		}
	}

	public void MouthShot()
	{
		if (!isMouthShot)
		{
			isMouthInsert = true;
			isMouthPiston = false;
			isMouthShot = true;
			MouthSetCoolTimeStatus(coolTimeShot);
			MouthSetStatus();
			_characterLifeManager.isMouthWait = true;
			isCoolTimeMouth = true;
			coolTimeMouth = coolTimeShot;
			_feelerMouthAnimancer.StateSet("isShot", 0.25f);
			_feelerMouthPistonObject.isAim = true;
			_characterMouthManager.mouthOpen = 95f;
			_characterMouthManager.mouthWidth = 95f;
			_reactionTargetAnimaton.targetY += 1f;
			_characterMouthManager.isHardBreath = true;
			_characterMouthManager.isMouthGag = true;
			_characterTalkManager.mouthGag = true;
		}
	}

	public void MouthShotEnd()
	{
		Debug.LogError("MouthShotEnd");
		isMouthInsert = true;
		isMouthPiston = false;
		isMouthShot = false;
		MouthSetStatus();
		_characterEyesManager.isMouthShot = false;
	}

	public void MouthSetCoolTimeStatus(float value)
	{
		SkillGUIDataBase.instance.SetCoolTime(70, value);
		SkillGUIDataBase.instance.SetCoolTime(71, value);
		SkillGUIDataBase.instance.SetCoolTime(75, value);
	}

	public void MouthSetStatus()
	{
		SkillGUIDataBase.instance.SetEnable(70, isMouthInsert);
		SkillGUIDataBase.instance.SetEnable(71, isMouthPiston);
		SkillGUIDataBase.instance.SetEnable(75, isMouthShot);
		_characterEyesManager.isMouthInsert = isMouthInsert;
		_characterEyesManager.isMouthPiston = isMouthPiston;
		_characterEyesManager.isMouthShot = isMouthShot;
	}

	public void TitsInsert()
	{
		if (!isTitsInsert)
		{
			isTitsInsert = true;
			isTitsPiston = false;
			isTitsShot = false;
			TitsSetCoolTimeStatus(coolTimeInsert);
			TitsSetStatus();
			_feelerTitsAnimancer.StateSet("isInsert", 0.25f);
			_feelerTitsPistonObject.isAim = true;
		}
		else
		{
			isTitsInsert = false;
			isTitsPiston = false;
			isTitsShot = false;
			TitsSetCoolTimeStatus(coolTimeInsert);
			TitsSetStatus();
			_feelerTitsAnimancer.StateSet("isIdle", 0.25f);
			_feelerTitsPistonObject.isAim = false;
			_feelerTitsPistonObject.isFirstInsert = false;
			_characterFaceManager._characterEffectManager.TitsGlobs();
		}
	}

	public void TitsPiston()
	{
		if (!isTitsPiston)
		{
			isTitsInsert = true;
			isTitsPiston = true;
			isTitsShot = false;
			TitsSetCoolTimeStatus(coolTimePiston);
			TitsSetStatus();
			_feelerTitsAnimancer.StateSet("isPiston", 0.25f);
			_feelerTitsPistonObject.isAim = true;
		}
		else
		{
			isTitsInsert = false;
			isTitsPiston = false;
			isTitsShot = false;
			TitsInsert();
		}
	}

	public void TitsShot()
	{
		if (!isTitsShot)
		{
			isTitsInsert = true;
			isTitsPiston = false;
			isTitsShot = true;
			TitsSetCoolTimeStatus(coolTimeShot);
			TitsSetStatus();
			_characterLifeManager.isTitsWait = true;
			isCoolTimeTits = true;
			coolTimeTits = coolTimeShot;
			_feelerTitsAnimancer.StateSet("isShot", 0.25f);
			_feelerTitsPistonObject.isAim = true;
			_characterEyesManager.isTitsShot = true;
		}
	}

	public void TitsShotEnd()
	{
		Debug.LogError("TitsShotEnd");
		isTitsInsert = true;
		isTitsPiston = false;
		isTitsShot = false;
		TitsSetStatus();
		_characterEyesManager.isTitsShot = false;
	}

	public void TitsSetCoolTimeStatus(float value)
	{
		SkillGUIDataBase.instance.SetCoolTime(60, value);
		SkillGUIDataBase.instance.SetCoolTime(61, value);
		SkillGUIDataBase.instance.SetCoolTime(65, value);
	}

	public void TitsSetStatus()
	{
		SkillGUIDataBase.instance.SetEnable(60, isTitsInsert);
		SkillGUIDataBase.instance.SetEnable(61, isTitsPiston);
		SkillGUIDataBase.instance.SetEnable(65, isTitsShot);
		_characterEyesManager.isTitsInsert = isTitsInsert;
		_characterEyesManager.isTitsPiston = isTitsPiston;
		_characterEyesManager.isTitsShot = isTitsShot;
	}
}
