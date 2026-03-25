using UnityEngine;

public class CharacterPositionManager : MonoBehaviour
{
	[Header("Main Script")]
	public BigTongueManager _bigTongueManager;

	public HorseRideObject _horseRideObject;

	public LimbEntombedObject _limbEntombedObject;

	public WartBedObject _wartBedObject;

	public WallHipObject _wallHipObject;

	public PillarBindObject _pillarBindObject;

	public MotionFuckAnimancer _motionFuckAnimancer;

	public CharacterTalkManager _characterTalkManager;

	[Header("Sub Script")]
	public CharacterLookAtHead _characterLookAtHead;

	public BindManager _bindManager;

	[Space]
	public MotionAnimancer _motionAnimancer;

	public ReactionAnimancer _reactionAnimancer;

	public PoseManager _poseManager;

	[Space]
	public CharacterHairMagica _characterHairMagica;

	public CharacterCostumeMagica _characterCostumeMagica;

	[Space]
	public CharacterFaceManager _characterFaceManager;

	public CharacterLifeManager _characterLifeManager;

	public CharacterEyesManager _characterEyesManager;

	public CharacterMouthManager _characterMouthManager;

	[Space]
	public FeelerControllerData _feelerControllerData;

	public FeelerPistonManager _feelerPistonManager;

	public ChildFeelerManager _childFeelerManager;

	public FeelerUnionCore _feelerUnionCore;

	public PistonGroundHoleManager _pistonGroundHoleManager;

	[Header("Transform")]
	public Transform character;

	[Space]
	public Transform neckBone;

	public Transform rootBone;

	[Space]
	public Transform targetEatBone;

	public Transform targetRideBone;

	public Transform targetLimbHoldBone;

	[Header("Position")]
	public Vector3 currentPosition;

	public Vector3 defaultPosition;

	public Vector3 targetPosition;

	public Vector3 calcPosition;

	public float positionLerpSpeed = 5f;

	[Header("Rotation")]
	public Quaternion defaultRotation;

	public Quaternion targetRotation;

	public float rotationLerpSpeed = 5f;

	[Header("Status")]
	public int gimmickID;

	public bool isGimmick;

	public bool isFuck;

	public bool isVaginaRub;

	public bool isEat;

	public bool isEating;

	public bool isRide;

	public bool isRiding;

	public bool isLimbHold;

	public bool isLimbHolding;

	public bool isWartBed;

	public bool isWartBedding;

	public bool isWallHip;

	public bool isWallHipper;

	public bool isPillarBind;

	public bool isPillarBinding;

	public bool isBackFuck;

	public bool isBackFucking;

	public bool isFrontFuck;

	public bool isFrontFucking;

	public bool isRideFuck;

	public bool isRideFucking;

	public bool isLiftFuck;

	public bool isLiftFucking;

	public bool isSideFuck;

	public bool isSideFucking;

	public bool isDoggyFuck;

	public bool isDoggyFucking;

	public bool isDefaultEnd;

	[Header("Wait")]
	public float actionWait;

	public float actionWaitMax = 4f;

	private void Start()
	{
		defaultPosition = character.position;
		defaultRotation = character.rotation;
		actionWait = 0f;
		gimmickID = -1;
	}

	private void LateUpdate()
	{
		if (actionWait > 0f)
		{
			actionWait -= Time.deltaTime;
			if (actionWait < 0f && isDefaultEnd)
			{
				isDefaultEnd = false;
				isGimmick = false;
				isFuck = false;
				gimmickID = -1;
				SkillGUIDataBase.instance.GimmickReset();
			}
		}
		if (isEating)
		{
			targetPosition = targetEatBone.position - (neckBone.position - character.position);
			calcPosition.x = Mathf.Lerp(character.position.x, targetPosition.x, positionLerpSpeed * Time.deltaTime);
			calcPosition.z = Mathf.Lerp(character.position.z, targetPosition.z, positionLerpSpeed * Time.deltaTime);
			calcPosition.y = targetPosition.y;
			character.position = calcPosition;
			character.rotation = Quaternion.Lerp(character.rotation, defaultRotation, rotationLerpSpeed * Time.deltaTime);
		}
		else if (isRiding)
		{
			targetPosition = targetRideBone.position - (rootBone.position - character.position);
			calcPosition.x = Mathf.Lerp(character.position.x, targetPosition.x, positionLerpSpeed * Time.deltaTime);
			calcPosition.z = Mathf.Lerp(character.position.z, targetPosition.z, positionLerpSpeed * Time.deltaTime);
			calcPosition.y = targetPosition.y;
			character.position = calcPosition;
			targetRotation = targetRideBone.rotation * defaultRotation;
			character.rotation = Quaternion.Lerp(character.rotation, targetRotation, rotationLerpSpeed * Time.deltaTime);
		}
		else if (isLimbHolding)
		{
			targetPosition = targetLimbHoldBone.position - (rootBone.position - character.position);
			calcPosition = Vector3.Lerp(character.position, targetPosition, positionLerpSpeed * Time.deltaTime);
			character.position = calcPosition;
			targetRotation = targetLimbHoldBone.rotation * defaultRotation;
			character.rotation = Quaternion.Lerp(character.rotation, targetRotation, rotationLerpSpeed * Time.deltaTime);
		}
		else
		{
			calcPosition = Vector3.Lerp(character.position, defaultPosition, positionLerpSpeed * Time.deltaTime);
			character.position = calcPosition;
			character.rotation = Quaternion.Lerp(character.rotation, defaultRotation, rotationLerpSpeed * Time.deltaTime);
		}
	}

	public void Eat(int SkillID)
	{
		if (actionWait <= 0f && ((gimmickID == SkillID && isEat) || !isGimmick))
		{
			gimmickID = SkillID;
			isGimmick = true;
			actionWait = actionWaitMax;
			SetEat(!isEat);
		}
	}

	public void SetEat(bool value)
	{
		if (_motionAnimancer.isEndWait)
		{
			return;
		}
		Debug.LogError("isEat: " + isEat, base.gameObject);
		isEat = value;
		if (isEat)
		{
			_bigTongueManager.EatAnim();
		}
		else
		{
			_bigTongueManager.ReleaseAnim();
		}
		_bindManager.EatMode();
		_poseManager.ButtonLock(value: true, 0, stunt: false);
		if (isEat)
		{
			_motionAnimancer.PoseChange(-20, expend: true, 0);
			_reactionAnimancer.PoseChange(-20, expend: true);
			_characterLookAtHead.UseLook(head: false, body: false);
			_feelerControllerData.isEat = true;
			if (_feelerPistonManager.isMouthInsert)
			{
				_feelerControllerData.MouthInsert();
			}
			_characterLifeManager.HitData("Gimmick", "Eater");
			SkillGUIDataBase.instance.GimmickEat(value: true, actionWaitMax);
		}
		else
		{
			SkillGUIDataBase.instance.GimmickEat(value: false, actionWaitMax);
			_poseManager.ButtonUnLock(actionWaitMax);
			_motionAnimancer.PoseChange(-20, expend: true, 1);
			_reactionAnimancer.PoseChange(-20, expend: true);
			_characterHairMagica.SetEatWeight(value: false);
			_characterCostumeMagica.SetEatWeight(value: false);
		}
		_feelerUnionCore.SetEat(isEat);
	}

	public void SetEatBody()
	{
		isEating = true;
		Debug.LogError("Eat Character:" + isEating, base.gameObject);
		_characterHairMagica.SetEatWeight(value: true);
		_characterCostumeMagica.SetEatWeight(value: true);
		_characterMouthManager.isGimmickGag = true;
		_characterLifeManager.isGimmickGag = true;
		_characterTalkManager.gimmickGag = true;
		_characterEyesManager.TearStop(value: true);
		_characterMouthManager.PlayHitSe();
	}

	public void Ride(int SkillID)
	{
		if (actionWait <= 0f && ((gimmickID == SkillID && isRiding) || !isGimmick))
		{
			gimmickID = SkillID;
			isGimmick = true;
			actionWait = actionWaitMax;
			SetRide(!isRiding);
		}
	}

	public void SetRide(bool value)
	{
		if (_motionAnimancer.isEndWait)
		{
			return;
		}
		Debug.LogError("isRide: " + isRide, base.gameObject);
		isRide = value;
		if (isRide)
		{
			_horseRideObject.isHorse = true;
		}
		else
		{
			_horseRideObject.isHorse = false;
		}
		_bindManager.RideMode();
		_poseManager.ButtonLock(value: true, 1, stunt: false);
		_characterLifeManager.isUnBirth = isRide;
		if (isRide)
		{
			_motionAnimancer.PoseChange(-21, expend: true, 0);
			_reactionAnimancer.PoseChange(-21, expend: true);
			_feelerControllerData.isRide = true;
			_feelerPistonManager.isRide = true;
			_childFeelerManager._uterusChildFeelerManager.isRide = true;
			_childFeelerManager._analChildFeelerManager.isRide = true;
			if (_feelerPistonManager.isVaginaInsert)
			{
				_feelerControllerData.VaginaInsert(disable: false);
			}
			if (_feelerPistonManager.isAnalInsert)
			{
				_feelerControllerData.AnalInsert();
			}
			if (_feelerControllerData.isVaginaRub)
			{
				_feelerControllerData.VaginaRub(disable: false);
			}
			if (_feelerControllerData.isVaginaOpen)
			{
				_feelerControllerData.VaginaOpen();
			}
			if (_feelerControllerData.isVaginaRub)
			{
				_feelerControllerData.VaginaRub(disable: false);
			}
			_characterLifeManager.HitData("Gimmick", "HorseRide");
			SkillGUIDataBase.instance.GimmickHorseRide(value: true, actionWaitMax);
		}
		else
		{
			if (_feelerPistonManager.isVaginaInsert)
			{
				_feelerControllerData.VaginaInsert(disable: false);
			}
			if (_feelerPistonManager.isAnalInsert)
			{
				_feelerControllerData.AnalInsert();
			}
			SkillGUIDataBase.instance.GimmickHorseRide(value: false, actionWaitMax);
			_poseManager.ButtonUnLock(actionWaitMax);
			_motionAnimancer.PoseChange(-21, expend: true, 1);
			_reactionAnimancer.PoseChange(-21, expend: true);
		}
	}

	public void SetRideBody()
	{
		isRiding = true;
		Debug.LogError("Ride Character:" + isRiding, base.gameObject);
	}

	public void LimbHold(int SkillID)
	{
		if (actionWait <= 0f && ((gimmickID == SkillID && isLimbHold) || !isGimmick))
		{
			gimmickID = SkillID;
			isGimmick = true;
			actionWait = actionWaitMax;
			SetLimbHold(!isLimbHold);
		}
	}

	public void SetLimbHold(bool value)
	{
		if (!_motionAnimancer.isEndWait)
		{
			Debug.LogError("isLimbHold: " + isLimbHold, base.gameObject);
			isLimbHold = value;
			_bindManager.LimbHoldMode();
			_poseManager.ButtonLock(value: true, 2, stunt: false);
			if (isLimbHold)
			{
				_limbEntombedObject.SetHold(value: true);
				_motionAnimancer.PoseChange(-22, expend: true, 0);
				_reactionAnimancer.PoseChange(-22, expend: true);
				_characterLifeManager.HitData("Gimmick", "LimbHold");
				SkillGUIDataBase.instance.GimmickLimbHold(value: true, actionWaitMax);
			}
			else
			{
				_limbEntombedObject.SetHold(value: false);
				SkillGUIDataBase.instance.GimmickLimbHold(value: false, actionWaitMax);
				_poseManager.ButtonUnLock(actionWaitMax);
			}
		}
	}

	public void SetLimbHoldBody()
	{
		isLimbHolding = true;
		Debug.LogError("Limb Hold Character:" + isLimbHolding, base.gameObject);
	}

	public void ReleaseLimbHoldBody()
	{
		Debug.LogError("Limb Hold Release Body:" + isLimbHolding, base.gameObject);
		SetDefaultBody();
		_motionAnimancer.PoseChange(-22, expend: true, 1);
		_reactionAnimancer.PoseChange(-22, expend: true);
	}

	public void WartBed(int SkillID)
	{
		if (actionWait <= 0f && ((gimmickID == SkillID && isWartBed) || !isGimmick))
		{
			gimmickID = SkillID;
			isGimmick = true;
			actionWait = actionWaitMax;
			SetWartBed(!isWartBed);
		}
	}

	public void SetWartBed(bool value)
	{
		if (_motionAnimancer.isEndWait)
		{
			return;
		}
		Debug.LogError("isWartBed: " + isWartBed, base.gameObject);
		isWartBed = value;
		_bindManager.WartBedMode();
		_poseManager.ButtonLock(value: true, 3, stunt: false);
		if (isWartBed)
		{
			_wartBedObject.SetHold(value: true);
			_motionAnimancer.PoseChange(-23, expend: true, 0);
			_reactionAnimancer.PoseChange(-23, expend: true);
			if (_feelerControllerData.isMobSpanking)
			{
				_feelerControllerData.MobSpanking(42);
			}
			_characterLifeManager.HitData("Gimmick", "WartBed");
			SkillGUIDataBase.instance.GimmickWartBed(value: true, actionWaitMax);
		}
		else
		{
			_wartBedObject.SetHold(value: false);
			SkillGUIDataBase.instance.GimmickWartBed(value: false, actionWaitMax);
			_poseManager.ButtonUnLock(actionWaitMax);
		}
	}

	public void SetWartBedBody()
	{
		isWartBedding = true;
		Debug.LogError("WartBed Character:" + isWartBedding, base.gameObject);
	}

	public void ReleaseWartBedBody()
	{
		Debug.LogError("WartBed Release Body:" + isWartBedding, base.gameObject);
		SetDefaultBody();
		_motionAnimancer.PoseChange(-23, expend: true, 1);
		_reactionAnimancer.PoseChange(-23, expend: true);
	}

	public void WallHip(int SkillID)
	{
		if (actionWait <= 0f && ((gimmickID == SkillID && isWallHip) || !isGimmick))
		{
			gimmickID = SkillID;
			isGimmick = true;
			actionWait = actionWaitMax;
			SetWallHip(!isWallHip);
		}
	}

	public void SetWallHip(bool value)
	{
		if (!_motionAnimancer.isEndWait)
		{
			Debug.LogError("isWallHip: " + isWallHip, base.gameObject);
			isWallHip = value;
			_bindManager.WartBedMode();
			_poseManager.ButtonLock(value: true, 4, stunt: false);
			if (isWallHip)
			{
				_wallHipObject.SetHold(value: true);
				_motionAnimancer.PoseChange(-24, expend: true, 0);
				_reactionAnimancer.PoseChange(-24, expend: true);
				_characterLifeManager.HitData("Gimmick", "WallHip");
				SkillGUIDataBase.instance.GimmickWallHip(value: true, actionWaitMax);
			}
			else
			{
				_wallHipObject.SetHold(value: false);
				SkillGUIDataBase.instance.GimmickWallHip(value: false, actionWaitMax);
				_poseManager.ButtonUnLock(actionWaitMax);
			}
			_feelerUnionCore.SetWallHip(isWallHip);
		}
	}

	public void SetWallHipBody()
	{
		isWallHipper = true;
		Debug.LogError("WallHip Character:" + isWallHipper, base.gameObject);
	}

	public void ReleaseWallHipBody()
	{
		Debug.LogError("WallHip Release Body:" + isWallHipper, base.gameObject);
		SetDefaultBody();
		_motionAnimancer.PoseChange(-24, expend: true, 1);
		_reactionAnimancer.PoseChange(-24, expend: true);
	}

	public void PillarBind(int SkillID)
	{
		if (actionWait <= 0f && ((gimmickID == SkillID && isPillarBind) || !isGimmick))
		{
			gimmickID = SkillID;
			isGimmick = true;
			actionWait = actionWaitMax;
			SetPillarBind(!isPillarBind);
		}
	}

	public void SetPillarBind(bool value)
	{
		if (!_motionAnimancer.isEndWait)
		{
			Debug.LogError("isPillarBind: " + isPillarBind, base.gameObject);
			isPillarBind = value;
			_bindManager.PillarBindMode();
			_poseManager.ButtonLock(value: true, 5, stunt: false);
			if (isPillarBind)
			{
				_pillarBindObject.SetHold(value: true);
				_motionAnimancer.PoseChange(-25, expend: true, 0);
				_reactionAnimancer.PoseChange(-25, expend: true);
				_characterLifeManager.HitData("Gimmick", "PillarBind");
				SkillGUIDataBase.instance.GimmickPillarBind(value: true, actionWaitMax);
			}
			else
			{
				_pillarBindObject.SetHold(value: false);
				SkillGUIDataBase.instance.GimmickPillarBind(value: false, actionWaitMax);
				_poseManager.ButtonUnLock(actionWaitMax);
			}
			_feelerUnionCore.SetPillarBind(isPillarBind);
		}
	}

	public void SetPillarBindBody()
	{
		isPillarBinding = true;
		Debug.LogError("PillarBind Character:" + isPillarBinding, base.gameObject);
	}

	public void ReleasePillarBindBody()
	{
		Debug.LogError("PillarBind Release Body:" + isPillarBinding, base.gameObject);
		SetDefaultBody();
		_motionAnimancer.PoseChange(-25, expend: true, 1);
		_reactionAnimancer.PoseChange(-25, expend: true);
	}

	public void FuckReset()
	{
		isBackFuck = false;
		isFrontFuck = false;
		isRideFuck = false;
		isLiftFuck = false;
		isSideFuck = false;
		isDoggyFuck = false;
	}

	public void BackFuck(int SkillID)
	{
		if (actionWait <= 0f && isFuck && !isBackFuck)
		{
			gimmickID = SkillID;
			actionWait = actionWaitMax;
			ChangeBackFuck();
		}
		else if (actionWait <= 0f && ((gimmickID == SkillID && isBackFuck) || !isGimmick))
		{
			gimmickID = SkillID;
			isGimmick = true;
			isFuck = true;
			actionWait = actionWaitMax;
			SetBackFuck(!isBackFuck);
		}
	}

	public void SetBackFuck(bool value)
	{
		if (_motionAnimancer.isEndWait)
		{
			return;
		}
		Debug.LogError("isBackFuck: " + isBackFuck, base.gameObject);
		isBackFuck = value;
		_bindManager.FuckMode();
		_poseManager.ButtonLock(value: true, 0, stunt: true);
		_characterLifeManager.isUnBirth = isBackFuck;
		_characterLookAtHead.UseLook(head: true, body: false);
		_characterLifeManager.isReactionLock = isBackFuck;
		_pistonGroundHoleManager.isAway = isBackFuck;
		if (isBackFuck)
		{
			_motionAnimancer._motionFuckAnimancer = _motionFuckAnimancer;
			_motionAnimancer.PoseChange(-30, expend: true, 0);
			_reactionAnimancer.PoseChange(-30, expend: true);
			_feelerControllerData.isFuck = true;
			_feelerPistonManager.isFuck = true;
			_childFeelerManager._uterusChildFeelerManager.isFuck = true;
			_childFeelerManager._analChildFeelerManager.isFuck = true;
			if (_feelerPistonManager.isVaginaInsert)
			{
				_feelerControllerData.VaginaInsert(disable: false);
			}
			if (_feelerControllerData.isMobSpanking)
			{
				_feelerControllerData.MobSpanking(42);
			}
			if (_feelerControllerData.isTitsRub)
			{
				_feelerControllerData.TitsRub();
			}
			if (_feelerControllerData.isTitsPress)
			{
				_feelerControllerData.TitsPress();
			}
			if (_feelerControllerData.isTitsSuck)
			{
				_feelerControllerData.TitsSuck();
			}
			if (_feelerControllerData.isVaginaRub)
			{
				_feelerControllerData.VaginaRub(disable: false);
			}
			if (_feelerControllerData.isVaginaOpen)
			{
				_feelerControllerData.VaginaOpen();
			}
			_characterLifeManager.HitData("Fuck", "FuckOn");
			SkillGUIDataBase.instance.GimmickBackFuck(value: true, actionWaitMax);
		}
		else
		{
			if (_feelerPistonManager.isVaginaInsert)
			{
				_feelerControllerData.VaginaInsert(disable: false);
			}
			SkillGUIDataBase.instance.GimmickBackFuck(value: false, actionWaitMax);
			_poseManager.ButtonUnLock(actionWaitMax);
			_motionAnimancer.PoseChange(-30, expend: true, 1);
			_reactionAnimancer.PoseChange(-30, expend: true);
			_motionAnimancer._motionFuckAnimancer = null;
		}
		_feelerUnionCore.SetFuck(isBackFuck);
	}

	public void ChangeBackFuck()
	{
		if (!_motionAnimancer.isEndWait)
		{
			Debug.LogError("ChangeBackFuck: " + isBackFuck, base.gameObject);
			FuckReset();
			isBackFuck = true;
			_poseManager.ButtonLock(value: true, 1, stunt: true);
			_motionAnimancer.PoseChange(-30, expend: true, 2);
			_reactionAnimancer.PoseChange(-30, expend: true);
			_characterLifeManager.HitData("Fuck", "FuckOn");
			SkillGUIDataBase.instance.GimmickBackFuck(value: true, actionWaitMax);
		}
	}

	public void FrontFuck(int SkillID)
	{
		if (actionWait <= 0f && isFuck && !isFrontFuck)
		{
			gimmickID = SkillID;
			actionWait = actionWaitMax;
			ChangeFrontFuck();
		}
		else if (actionWait <= 0f && ((gimmickID == SkillID && isFrontFuck) || !isGimmick))
		{
			gimmickID = SkillID;
			isGimmick = true;
			isFuck = true;
			actionWait = actionWaitMax;
			SetFrontFuck(!isFrontFuck);
		}
	}

	public void SetFrontFuck(bool value)
	{
		if (_motionAnimancer.isEndWait)
		{
			return;
		}
		Debug.LogError("isFrontFuck: " + isFrontFuck, base.gameObject);
		isFrontFuck = value;
		_bindManager.FuckMode();
		_poseManager.ButtonLock(value: true, 1, stunt: true);
		_characterLifeManager.isUnBirth = isFrontFuck;
		_characterLookAtHead.UseLook(head: true, body: false);
		_characterLifeManager.isReactionLock = isFrontFuck;
		_pistonGroundHoleManager.isAway = isFrontFuck;
		if (isFrontFuck)
		{
			_motionAnimancer._motionFuckAnimancer = _motionFuckAnimancer;
			_motionAnimancer.PoseChange(-31, expend: true, 0);
			_reactionAnimancer.PoseChange(-31, expend: true);
			_feelerControllerData.isFuck = true;
			_feelerPistonManager.isFuck = true;
			_childFeelerManager._uterusChildFeelerManager.isFuck = true;
			_childFeelerManager._analChildFeelerManager.isFuck = true;
			if (_feelerPistonManager.isVaginaInsert)
			{
				_feelerControllerData.VaginaInsert(disable: false);
			}
			if (_feelerControllerData.isMobSpanking)
			{
				_feelerControllerData.MobSpanking(42);
			}
			if (_feelerControllerData.isTitsRub)
			{
				_feelerControllerData.TitsRub();
			}
			if (_feelerControllerData.isTitsPress)
			{
				_feelerControllerData.TitsPress();
			}
			if (_feelerControllerData.isTitsSuck)
			{
				_feelerControllerData.TitsSuck();
			}
			if (_feelerControllerData.isVaginaRub)
			{
				_feelerControllerData.VaginaRub(disable: false);
			}
			if (_feelerControllerData.isVaginaOpen)
			{
				_feelerControllerData.VaginaOpen();
			}
			_characterLifeManager.HitData("Fuck", "FuckOn");
			SkillGUIDataBase.instance.GimmickFrontFuck(value: true, actionWaitMax);
		}
		else
		{
			if (_feelerPistonManager.isVaginaInsert)
			{
				_feelerControllerData.VaginaInsert(disable: false);
			}
			SkillGUIDataBase.instance.GimmickFrontFuck(value: false, actionWaitMax);
			_poseManager.ButtonUnLock(actionWaitMax);
			_motionAnimancer.PoseChange(-31, expend: true, 1);
			_reactionAnimancer.PoseChange(-31, expend: true);
			_motionAnimancer._motionFuckAnimancer = null;
		}
		_feelerUnionCore.SetFuck(isFrontFuck);
	}

	public void ChangeFrontFuck()
	{
		if (!_motionAnimancer.isEndWait)
		{
			Debug.LogError("ChangeFrontFuck: " + isFrontFuck, base.gameObject);
			FuckReset();
			isFrontFuck = true;
			_poseManager.ButtonLock(value: true, 1, stunt: true);
			_motionAnimancer.PoseChange(-31, expend: true, 2);
			_reactionAnimancer.PoseChange(-31, expend: true);
			_characterLifeManager.HitData("Fuck", "FuckOn");
			SkillGUIDataBase.instance.GimmickFrontFuck(value: true, actionWaitMax);
		}
	}

	public void RideFuck(int SkillID)
	{
		if (actionWait <= 0f && isFuck && !isRideFuck)
		{
			gimmickID = SkillID;
			actionWait = actionWaitMax;
			ChangeRideFuck();
		}
		else if (actionWait <= 0f && ((gimmickID == SkillID && isRideFuck) || !isGimmick))
		{
			gimmickID = SkillID;
			isGimmick = true;
			isFuck = true;
			actionWait = actionWaitMax;
			SetRideFuck(!isRideFuck);
		}
	}

	public void SetRideFuck(bool value)
	{
		if (_motionAnimancer.isEndWait)
		{
			return;
		}
		Debug.LogError("isRideFuck: " + isRideFuck, base.gameObject);
		isRideFuck = value;
		_bindManager.FuckMode();
		_poseManager.ButtonLock(value: true, 2, stunt: true);
		_characterLifeManager.isUnBirth = isRideFuck;
		_characterLookAtHead.UseLook(head: true, body: false);
		_characterLifeManager.isReactionLock = isRideFuck;
		_pistonGroundHoleManager.isAway = isRideFuck;
		if (isRideFuck)
		{
			_motionAnimancer._motionFuckAnimancer = _motionFuckAnimancer;
			_motionAnimancer.PoseChange(-32, expend: true, 0);
			_reactionAnimancer.PoseChange(-32, expend: true);
			_feelerControllerData.isFuck = true;
			_feelerPistonManager.isFuck = true;
			_childFeelerManager._uterusChildFeelerManager.isFuck = true;
			_childFeelerManager._analChildFeelerManager.isFuck = true;
			if (_feelerPistonManager.isVaginaInsert)
			{
				_feelerControllerData.VaginaInsert(disable: false);
			}
			if (_feelerControllerData.isMobSpanking)
			{
				_feelerControllerData.MobSpanking(42);
			}
			if (_feelerControllerData.isTitsRub)
			{
				_feelerControllerData.TitsRub();
			}
			if (_feelerControllerData.isTitsPress)
			{
				_feelerControllerData.TitsPress();
			}
			if (_feelerControllerData.isTitsSuck)
			{
				_feelerControllerData.TitsSuck();
			}
			if (_feelerControllerData.isVaginaRub)
			{
				_feelerControllerData.VaginaRub(disable: false);
			}
			if (_feelerControllerData.isVaginaOpen)
			{
				_feelerControllerData.VaginaOpen();
			}
			_characterLifeManager.HitData("Fuck", "FuckOn");
			SkillGUIDataBase.instance.GimmickRideFuck(value: true, actionWaitMax);
		}
		else
		{
			if (_feelerPistonManager.isVaginaInsert)
			{
				_feelerControllerData.VaginaInsert(disable: false);
			}
			SkillGUIDataBase.instance.GimmickRideFuck(value: false, actionWaitMax);
			_poseManager.ButtonUnLock(actionWaitMax);
			_motionAnimancer.PoseChange(-32, expend: true, 1);
			_reactionAnimancer.PoseChange(-32, expend: true);
			_motionAnimancer._motionFuckAnimancer = null;
		}
		_feelerUnionCore.SetFuck(isRideFuck);
	}

	public void ChangeRideFuck()
	{
		if (!_motionAnimancer.isEndWait)
		{
			Debug.LogError("ChangeRideFuck: " + isRideFuck, base.gameObject);
			FuckReset();
			isRideFuck = true;
			_poseManager.ButtonLock(value: true, 2, stunt: true);
			_motionAnimancer.PoseChange(-32, expend: true, 2);
			_reactionAnimancer.PoseChange(-32, expend: true);
			_characterLifeManager.HitData("Fuck", "FuckOn");
			SkillGUIDataBase.instance.GimmickRideFuck(value: true, actionWaitMax);
		}
	}

	public void LiftFuck(int SkillID)
	{
		if (actionWait <= 0f && isFuck && !isLiftFuck)
		{
			gimmickID = SkillID;
			actionWait = actionWaitMax;
			ChangeLiftFuck();
		}
		else if (actionWait <= 0f && ((gimmickID == SkillID && isLiftFuck) || !isGimmick))
		{
			gimmickID = SkillID;
			isGimmick = true;
			isFuck = true;
			actionWait = actionWaitMax;
			SetLiftFuck(!isLiftFuck);
		}
	}

	public void SetLiftFuck(bool value)
	{
		if (_motionAnimancer.isEndWait)
		{
			return;
		}
		Debug.LogError("isLiftFuck: " + isLiftFuck, base.gameObject);
		isLiftFuck = value;
		_bindManager.FuckMode();
		_poseManager.ButtonLock(value: true, 3, stunt: true);
		_characterLifeManager.isUnBirth = isLiftFuck;
		_characterLookAtHead.UseLook(head: true, body: false);
		_characterLifeManager.isReactionLock = isLiftFuck;
		_pistonGroundHoleManager.isAway = isLiftFuck;
		if (isLiftFuck)
		{
			_motionAnimancer._motionFuckAnimancer = _motionFuckAnimancer;
			_motionAnimancer.PoseChange(-33, expend: true, 0);
			_reactionAnimancer.PoseChange(-33, expend: true);
			_feelerControllerData.isFuck = true;
			_feelerPistonManager.isFuck = true;
			_childFeelerManager._uterusChildFeelerManager.isFuck = true;
			_childFeelerManager._analChildFeelerManager.isFuck = true;
			if (_feelerPistonManager.isVaginaInsert)
			{
				_feelerControllerData.VaginaInsert(disable: false);
			}
			if (_feelerControllerData.isMobSpanking)
			{
				_feelerControllerData.MobSpanking(42);
			}
			if (_feelerControllerData.isTitsRub)
			{
				_feelerControllerData.TitsRub();
			}
			if (_feelerControllerData.isTitsPress)
			{
				_feelerControllerData.TitsPress();
			}
			if (_feelerControllerData.isTitsSuck)
			{
				_feelerControllerData.TitsSuck();
			}
			if (_feelerControllerData.isVaginaRub)
			{
				_feelerControllerData.VaginaRub(disable: false);
			}
			if (_feelerControllerData.isVaginaOpen)
			{
				_feelerControllerData.VaginaOpen();
			}
			_characterLifeManager.HitData("Fuck", "FuckOn");
			SkillGUIDataBase.instance.GimmickLiftFuck(value: true, actionWaitMax);
		}
		else
		{
			if (_feelerPistonManager.isVaginaInsert)
			{
				_feelerControllerData.VaginaInsert(disable: false);
			}
			SkillGUIDataBase.instance.GimmickLiftFuck(value: false, actionWaitMax);
			_poseManager.ButtonUnLock(actionWaitMax);
			_motionAnimancer.PoseChange(-33, expend: true, 1);
			_reactionAnimancer.PoseChange(-33, expend: true);
			_motionAnimancer._motionFuckAnimancer = null;
		}
		_feelerUnionCore.SetFuck(isLiftFuck);
	}

	public void ChangeLiftFuck()
	{
		if (!_motionAnimancer.isEndWait)
		{
			Debug.LogError("ChangeLiftFuck: " + isLiftFuck, base.gameObject);
			FuckReset();
			isLiftFuck = true;
			_poseManager.ButtonLock(value: true, 3, stunt: true);
			_motionAnimancer.PoseChange(-33, expend: true, 2);
			_reactionAnimancer.PoseChange(-33, expend: true);
			_characterLifeManager.HitData("Fuck", "FuckOn");
			SkillGUIDataBase.instance.GimmickLiftFuck(value: true, actionWaitMax);
		}
	}

	public void SideFuck(int SkillID)
	{
		if (actionWait <= 0f && isFuck && !isSideFuck)
		{
			gimmickID = SkillID;
			actionWait = actionWaitMax;
			ChangeSideFuck();
		}
		else if (actionWait <= 0f && ((gimmickID == SkillID && isSideFuck) || !isGimmick))
		{
			gimmickID = SkillID;
			isGimmick = true;
			isFuck = true;
			actionWait = actionWaitMax;
			SetSideFuck(!isSideFuck);
		}
	}

	public void SetSideFuck(bool value)
	{
		if (_motionAnimancer.isEndWait)
		{
			return;
		}
		Debug.LogError("isSideFuck: " + isSideFuck, base.gameObject);
		isSideFuck = value;
		_bindManager.FuckMode();
		_poseManager.ButtonLock(value: true, 4, stunt: true);
		_characterLifeManager.isUnBirth = isSideFuck;
		_characterLookAtHead.UseLook(head: true, body: false);
		_characterLifeManager.isReactionLock = isSideFuck;
		_pistonGroundHoleManager.isAway = isSideFuck;
		if (isSideFuck)
		{
			_motionAnimancer._motionFuckAnimancer = _motionFuckAnimancer;
			_motionAnimancer.PoseChange(-34, expend: true, 0);
			_reactionAnimancer.PoseChange(-34, expend: true);
			_feelerControllerData.isFuck = true;
			_feelerPistonManager.isFuck = true;
			_childFeelerManager._uterusChildFeelerManager.isFuck = true;
			_childFeelerManager._analChildFeelerManager.isFuck = true;
			if (_feelerPistonManager.isVaginaInsert)
			{
				_feelerControllerData.VaginaInsert(disable: false);
			}
			if (_feelerControllerData.isMobSpanking)
			{
				_feelerControllerData.MobSpanking(42);
			}
			if (_feelerControllerData.isTitsRub)
			{
				_feelerControllerData.TitsRub();
			}
			if (_feelerControllerData.isTitsPress)
			{
				_feelerControllerData.TitsPress();
			}
			if (_feelerControllerData.isTitsSuck)
			{
				_feelerControllerData.TitsSuck();
			}
			if (_feelerControllerData.isVaginaRub)
			{
				_feelerControllerData.VaginaRub(disable: false);
			}
			if (_feelerControllerData.isVaginaOpen)
			{
				_feelerControllerData.VaginaOpen();
			}
			_characterLifeManager.HitData("Fuck", "FuckOn");
			SkillGUIDataBase.instance.GimmickSideFuck(value: true, actionWaitMax);
		}
		else
		{
			if (_feelerPistonManager.isVaginaInsert)
			{
				_feelerControllerData.VaginaInsert(disable: false);
			}
			SkillGUIDataBase.instance.GimmickSideFuck(value: false, actionWaitMax);
			_poseManager.ButtonUnLock(actionWaitMax);
			_motionAnimancer.PoseChange(-34, expend: true, 1);
			_reactionAnimancer.PoseChange(-34, expend: true);
			_motionAnimancer._motionFuckAnimancer = null;
		}
		_feelerUnionCore.SetFuck(isSideFuck);
	}

	public void ChangeSideFuck()
	{
		if (!_motionAnimancer.isEndWait)
		{
			Debug.LogError("ChangeSideFuck: " + isSideFuck, base.gameObject);
			FuckReset();
			isSideFuck = true;
			_poseManager.ButtonLock(value: true, 4, stunt: true);
			_motionAnimancer.PoseChange(-34, expend: true, 2);
			_reactionAnimancer.PoseChange(-34, expend: true);
			_characterLifeManager.HitData("Fuck", "FuckOn");
			SkillGUIDataBase.instance.GimmickSideFuck(value: true, actionWaitMax);
		}
	}

	public void DoggyFuck(int SkillID)
	{
		if (actionWait <= 0f && isFuck && !isDoggyFuck)
		{
			gimmickID = SkillID;
			actionWait = actionWaitMax;
			ChangeDoggyFuck();
		}
		else if (actionWait <= 0f && ((gimmickID == SkillID && isDoggyFuck) || !isGimmick))
		{
			gimmickID = SkillID;
			isGimmick = true;
			isFuck = true;
			actionWait = actionWaitMax;
			SetDoggyFuck(!isDoggyFuck);
		}
	}

	public void SetDoggyFuck(bool value)
	{
		if (_motionAnimancer.isEndWait)
		{
			return;
		}
		Debug.LogError("isDoggyFuck: " + isDoggyFuck, base.gameObject);
		isDoggyFuck = value;
		_bindManager.FuckMode();
		_poseManager.ButtonLock(value: true, 5, stunt: true);
		_characterLifeManager.isUnBirth = isDoggyFuck;
		_characterLookAtHead.UseLook(head: true, body: false);
		_characterLifeManager.isReactionLock = isDoggyFuck;
		_pistonGroundHoleManager.isAway = isDoggyFuck;
		if (isDoggyFuck)
		{
			_motionAnimancer._motionFuckAnimancer = _motionFuckAnimancer;
			_motionAnimancer.PoseChange(-35, expend: true, 0);
			_reactionAnimancer.PoseChange(-35, expend: true);
			_feelerControllerData.isFuck = true;
			_feelerPistonManager.isFuck = true;
			_childFeelerManager._uterusChildFeelerManager.isFuck = true;
			_childFeelerManager._analChildFeelerManager.isFuck = true;
			if (_feelerPistonManager.isVaginaInsert)
			{
				_feelerControllerData.VaginaInsert(disable: false);
			}
			if (_feelerControllerData.isMobSpanking)
			{
				_feelerControllerData.MobSpanking(42);
			}
			if (_feelerControllerData.isTitsRub)
			{
				_feelerControllerData.TitsRub();
			}
			if (_feelerControllerData.isTitsPress)
			{
				_feelerControllerData.TitsPress();
			}
			if (_feelerControllerData.isTitsSuck)
			{
				_feelerControllerData.TitsSuck();
			}
			if (_feelerControllerData.isVaginaRub)
			{
				_feelerControllerData.VaginaRub(disable: false);
			}
			if (_feelerControllerData.isVaginaOpen)
			{
				_feelerControllerData.VaginaOpen();
			}
			_characterLifeManager.HitData("Fuck", "FuckOn");
			SkillGUIDataBase.instance.GimmickDoggyFuck(value: true, actionWaitMax);
		}
		else
		{
			if (_feelerPistonManager.isVaginaInsert)
			{
				_feelerControllerData.VaginaInsert(disable: false);
			}
			SkillGUIDataBase.instance.GimmickDoggyFuck(value: false, actionWaitMax);
			_poseManager.ButtonUnLock(actionWaitMax);
			_motionAnimancer.PoseChange(-35, expend: true, 1);
			_reactionAnimancer.PoseChange(-35, expend: true);
			_motionAnimancer._motionFuckAnimancer = null;
		}
		_feelerUnionCore.SetFuck(isDoggyFuck);
	}

	public void ChangeDoggyFuck()
	{
		if (!_motionAnimancer.isEndWait)
		{
			Debug.LogError("ChangeDoggyFuck: " + isDoggyFuck, base.gameObject);
			FuckReset();
			isDoggyFuck = true;
			_poseManager.ButtonLock(value: true, 5, stunt: true);
			_motionAnimancer.PoseChange(-35, expend: true, 2);
			_reactionAnimancer.PoseChange(-35, expend: true);
			_characterLifeManager.HitData("Fuck", "FuckOn");
			SkillGUIDataBase.instance.GimmickDoggyFuck(value: true, actionWaitMax);
		}
	}

	public void DisableAll(int SkillID)
	{
		if (actionWait <= 0f)
		{
			gimmickID = SkillID;
			SkillGUIDataBase.instance.DisableAll(value: true, actionWaitMax);
			if (isEat)
			{
				Eat(SkillID);
			}
			else if (isRide)
			{
				Ride(SkillID);
			}
			else if (isLimbHold)
			{
				LimbHold(SkillID);
			}
			else if (isWartBed)
			{
				WartBed(SkillID);
			}
			else if (isWallHip)
			{
				WallHip(SkillID);
			}
			else if (isPillarBind)
			{
				PillarBind(SkillID);
			}
			else if (isBackFuck)
			{
				BackFuck(SkillID);
			}
			else if (isFrontFuck)
			{
				FrontFuck(SkillID);
			}
			else if (isRideFuck)
			{
				RideFuck(SkillID);
			}
			else if (isLiftFuck)
			{
				LiftFuck(SkillID);
			}
			else if (isSideFuck)
			{
				SideFuck(SkillID);
			}
			else if (isDoggyFuck)
			{
				DoggyFuck(SkillID);
			}
		}
	}

	public void SetDefaultBody()
	{
		isEating = false;
		isRiding = false;
		isLimbHolding = false;
		Debug.LogError("Release Character : Default Body", base.gameObject);
		character.position = defaultPosition;
		_characterLookAtHead.UseLook(head: true, body: true);
		_bindManager.ResetMode();
		_characterEyesManager.TearStop(value: false);
	}

	public void SetDefaultEnd()
	{
		isDefaultEnd = true;
		_feelerControllerData.isEat = false;
		_feelerControllerData.isRide = false;
		_feelerControllerData.isFuck = false;
		_feelerPistonManager.isRide = false;
		_childFeelerManager._uterusChildFeelerManager.isRide = false;
		_childFeelerManager._analChildFeelerManager.isRide = false;
		_feelerPistonManager.isFuck = false;
		_childFeelerManager._uterusChildFeelerManager.isFuck = false;
		_childFeelerManager._analChildFeelerManager.isFuck = false;
		_characterMouthManager.isGimmickGag = false;
		_characterLifeManager.isGimmickGag = false;
		_characterTalkManager.gimmickGag = false;
		_poseManager.GimmickEnd();
	}
}
