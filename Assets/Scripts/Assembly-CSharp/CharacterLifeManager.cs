using UnityEngine;

public class CharacterLifeManager : MonoBehaviour
{
	public EXPGUIManager _EXPGUIManager;

	[Space]
	public CharacterTalkManager _characterTalkManager;

	public CharacterFaceManager _characterFaceManager;

	public CharacterMouthManager _characterMouthManager;

	public CharacterEffectManager _characterEffectManager;

	public MotionAnimancer _motionAnimancer;

	public ReactionAnimancer _reactionAnimancer;

	public UterusChildFeelerManager _uterusChildFeelerManager;

	public AnalChildFeelerManager _analChildFeelerManager;

	public FeelerControllerData _feelerControllerData;

	public CharacterFacialManager _characterFacialManager;

	public HeartBeatManager _heartBeatManager;

	public SqueezeManager _squeezeManager;

	[Header("Gauge")]
	public HeartGUIManager _heartGUIManager;

	public MilkGUIManager _milkGUIManager;

	[Header("Reaction Target")]
	public ReactionTargetAnimaton _reactionTargetAnimaton;

	[Header("Status Reaction")]
	public bool isReactionLock;

	[Header("Talk Param")]
	public bool isMouthGag;

	public bool isGimmickGag;

	public bool isOverHeat;

	public bool isHeart;

	[Header("Status Gag")]
	public bool isVaginaGag;

	public bool isAnalGag;

	public bool isUnBirth;

	public int isVaginaChildStack;

	public int isAnalChildStack;

	public bool isLostVirsin;

	[Header("Status Wait")]
	public bool isVaginaWait;

	public bool isAnalWait;

	public bool isMouthWait;

	public bool isTitsWait;

	[Header("Type")]
	public string bodyType;

	public string attackType;

	public string talkType;

	[Header("TalkTimer")]
	public float talkWaitTime;

	public float talkWaitTimeMin = 5f;

	public float talkWaitTimeMax = 10f;

	[Header("VoiceTimer")]
	public float voiceWaitTime;

	public float voiceWaitTimeMin = 3f;

	public float voiceWaitTimeMax = 6f;

	[Header("ReactionTimer")]
	public float reactionTime;

	public float reactionTimeMin = 3f;

	public float reactionTimeMax = 8f;

	public float bukkakeTime = 0.1f;

	public float pistonTime = 0.1f;

	public float rubTime = 0.01f;

	public float touchTime = 0.01f;

	public float spankingMobTime = 0.2f;

	public float suckTime = 0.015f;

	public float biteTime = 0.05f;

	public float lickTime = 0.01f;

	public float syringeTime = 0.5f;

	[Header("Spanking Pee Limit")]
	public int peeCurrent = 5;

	public int peeLimitMin = 10;

	public int peeLimitMax = 20;

	public int peeLimitMob = 3;

	[Header("Hit Count")]
	public int bukkakeCount;

	public int bodyCount;

	public int headCount;

	public int titsCount;

	public int hipCount;

	public int vaginaCount;

	public int analCount;

	[Header("Mission Count")]
	public int spankingCount;

	public int syringeCount;

	public int rubCount;

	public int touchCount;

	public int suckCount;

	public int biteCount;

	public int lickCount;

	public int squeezeCount;

	public int pistonCount;

	public int peeCount;

	public int playerTitsCount;

	public int playerHipCount;

	[Header("Child Count")]
	public int vaginaChildCount;

	public int analChildCount;

	public int vaginaChildStackCount;

	public int analChildStackCount;

	[Header("Orgasm Count")]
	public int orgasmCount;

	[Header("Graph Count")]
	public float heartCount;

	public float titsMilkCount;

	private void Start()
	{
		ResetReactionTime();
		ResetTalkTime();
		ResetVoiceTime();
		peeCurrent = Random.Range(peeLimitMin, peeLimitMax);
	}

	private void LateUpdate()
	{
		reactionTime -= Time.deltaTime;
		if (reactionTime < 0f)
		{
			_reactionAnimancer.ReactionSet("isReaction", 0.15f);
			ResetReactionTime();
		}
		if (talkWaitTime > 0f)
		{
			talkWaitTime -= Time.deltaTime;
		}
		if (voiceWaitTime > 0f)
		{
			voiceWaitTime -= Time.deltaTime;
		}
	}

	public void ResetReactionTime()
	{
		reactionTime = Random.Range(reactionTimeMin, reactionTimeMax);
	}

	public void ResetTalkTime()
	{
		talkWaitTime = Random.Range(talkWaitTimeMin, talkWaitTimeMax);
	}

	public void ResetVoiceTime()
	{
		voiceWaitTime = Random.Range(voiceWaitTimeMin, voiceWaitTimeMax);
	}

	public void HitData(string value, string attack)
	{
		bodyType = value;
		attackType = attack;
		talkType = "";
		if (bodyType == "Reaction")
		{
			if (attackType == "Reaction")
			{
				talkType = "Reaction";
				_reactionAnimancer.ReactionSet("isReaction", 0.5f);
				ResetReactionTime();
				Damage(0.5f, 0f);
				HeartBeat(0.1f, 0f, 0.1f, 0.05f);
			}
		}
		else if (bodyType == "Orgasm")
		{
			if (attackType == "Orgasm")
			{
				orgasmCount++;
				talkType = "Orgasm";
				talkWaitTime = 0f;
				_reactionAnimancer.ReactionSet("isReaction", 0.5f);
				ResetReactionTime();
				Damage(5f, 0f);
				HeartBeat(1f, 0.4f, 0.4f, 0.2f);
				_characterFaceManager.targetBlush += 0.1f;
			}
		}
		else if (bodyType == "Body" || bodyType == "Belly")
		{
			if (attackType == "Bukkake")
			{
				bukkakeCount++;
				reactionTime -= bukkakeTime;
				talkType = "Bukkake";
				Damage(0.5f, 0.1f);
				HeartBeat(0.025f, 0f, 0f, 0.025f);
				_characterFacialManager.SetFacial("Lower", 0.03f);
			}
			else if (attackType == "BukkakeMob")
			{
				bukkakeCount++;
				Damage(0.25f, 0.05f);
				HeartBeat(0.025f, 0f, 0f, 0.025f);
				_characterFacialManager.SetFacial("Lower", 0.02f);
			}
			else if (attackType == "Spanking")
			{
				bodyCount++;
				spankingCount++;
				if (bodyType == "Body")
				{
					talkType = "SpankingBody";
				}
				if (bodyType == "Belly")
				{
					talkType = "SpankingBelly";
					BellyBirthChild();
				}
				SpankingPee(mob: false);
				_reactionAnimancer.ReactionSet("isReaction", 0.25f);
				ResetReactionTime();
				Damage(2f, 1f);
				HeartBeat(0.3f, 0.05f, 0.05f, 0.05f);
				_characterFacialManager.SetFacial("Both", 0.1f);
				if (isHeart && Random.value < 0.1f)
				{
					_characterEffectManager.VaginaDroplets();
				}
			}
			else if (attackType == "SpankingMob")
			{
				bodyCount++;
				SpankingPee(mob: true);
				talkType = "SpankingBody";
				reactionTime -= spankingMobTime;
				Damage(0.5f, 0.5f);
				HeartBeat(0.3f, 0.05f, 0.05f, 0.05f);
				_characterFacialManager.SetFacial("Both", 0.05f);
				if (isHeart && Random.value < 0.1f)
				{
					_characterEffectManager.VaginaDroplets();
				}
			}
			else if (attackType == "Syringe")
			{
				bodyCount++;
				syringeCount++;
				reactionTime -= syringeTime;
				talkType = "SyringeBody";
				Damage(1f, 1f);
				HeartBeat(0.3f, 0.05f, 0.05f, 0.05f);
				_characterFacialManager.SetFacial("Both", 0.1f);
				if (isHeart && Random.value < 0.1f)
				{
					_characterEffectManager.VaginaDroplets();
				}
			}
			else if (attackType == "Player")
			{
				bodyCount++;
				if (bodyType == "Belly")
				{
					BellyBirthChild();
				}
				_reactionAnimancer.ReactionSet("isReaction", 0.25f);
				ResetReactionTime();
				Damage(1f, 0.5f);
				HeartBeat(0.3f, 0.05f, 0.05f, 0.05f);
				_characterFacialManager.SetFacial("Lower", 0.1f);
			}
			else if (attackType == "Rub")
			{
				bodyCount++;
				rubCount++;
				reactionTime -= rubTime;
				talkType = "RubBody";
				BodyReaction();
				Damage(0.5f, 0.25f);
				HeartBeat(0.05f, 0.025f, 0.025f, 0.025f);
				_characterFacialManager.SetFacial("Lower", 0.02f);
			}
			else if (attackType == "Bite")
			{
				bodyCount++;
				biteCount++;
				reactionTime -= biteTime;
				talkType = "BiteBody";
				BodyReaction();
				Damage(1f, 0.75f);
				HeartBeat(0.15f, 0.02f, 0.02f, 0.02f);
				_characterFacialManager.SetFacial("Lower", 0.1f);
			}
			else if (attackType == "Lick")
			{
				bodyCount++;
				lickCount++;
				reactionTime -= lickTime;
				talkType = "LickBody";
				BodyReaction();
				Damage(0.5f, 0.5f);
				HeartBeat(0.05f, 0.025f, 0.025f, 0.025f);
				_characterFacialManager.SetFacial("Lower", 0.1f);
			}
			else if (attackType == "Touch")
			{
				bodyCount++;
				touchCount++;
				reactionTime -= touchTime;
				talkType = "TouchBody";
				BodyReaction();
				Damage(0.25f, 0.25f);
				HeartBeat(0.025f, 0.05f, 0.05f, 0.025f);
				_characterFacialManager.SetFacial("Lower", 0.02f);
			}
			else if (attackType == "Suck")
			{
				bodyCount++;
				suckCount++;
				reactionTime -= suckTime;
				talkType = "SuckBody";
				BodyReaction();
				Damage(0.025f, 1f);
				HeartBeat(0.1f, 0.025f, 0.025f, 0.025f);
				_characterFacialManager.SetFacial("Lower", 0.02f);
			}
		}
		else if (bodyType == "Head")
		{
			if (attackType == "Bukkake")
			{
				bukkakeCount++;
				reactionTime -= bukkakeTime;
				talkType = "Bukkake";
				Damage(0.5f, 1f);
				HeartBeat(0.025f, 0f, 0f, 0.025f);
				_characterFacialManager.SetFacial("Lower", 0.03f);
			}
			else if (attackType == "BukkakeMob")
			{
				bukkakeCount++;
				Damage(0.25f, 0.05f);
				HeartBeat(0.025f, 0f, 0f, 0.025f);
				_characterFacialManager.SetFacial("Lower", 0.02f);
			}
			else if (attackType == "Spanking")
			{
				headCount++;
				spankingCount++;
				talkType = "SpankingFace";
				_reactionAnimancer.ReactionSet("isReaction", 0.5f);
				ResetReactionTime();
				Damage(1.5f, 1.5f);
				HeartBeat(0.3f, 0.05f, 0.05f, 0.05f);
				_characterFacialManager.SetFacial("Both", 0.1f);
				if (isHeart && Random.value < 0.1f)
				{
					_characterEffectManager.VaginaDroplets();
				}
			}
			else if (attackType == "SpankingMob")
			{
				headCount++;
				talkType = "SpankingFace";
				reactionTime -= spankingMobTime;
				Damage(0.5f, 0.75f);
				HeartBeat(0.3f, 0.05f, 0.05f, 0.05f);
				_characterFacialManager.SetFacial("Both", 0.05f);
				if (isHeart && Random.value < 0.1f)
				{
					_characterEffectManager.VaginaDroplets();
				}
			}
			else if (attackType == "Syringe")
			{
				headCount++;
				syringeCount++;
				reactionTime -= syringeTime;
				talkType = "SyringeBody";
				Damage(1f, 1f);
				HeartBeat(0.3f, 0.05f, 0.05f, 0.05f);
				_characterFacialManager.SetFacial("Both", 0.1f);
				if (isHeart && Random.value < 0.1f)
				{
					_characterEffectManager.VaginaDroplets();
				}
			}
			else if (attackType == "Player")
			{
				headCount++;
				_reactionAnimancer.ReactionSet("isReaction", 0.5f);
				ResetReactionTime();
				Damage(1f, 0.5f);
				HeartBeat(0.3f, 0.05f, 0.05f, 0.05f);
				_characterFaceManager.targetBlush += 0.05f;
				_characterFacialManager.SetFacial("Lower", 0.1f);
			}
			else if (attackType == "Rub")
			{
				headCount++;
				rubCount++;
				reactionTime -= rubTime;
				talkType = "RubBody";
				BodyReaction();
				Damage(0.5f, 0.25f);
				HeartBeat(0.05f, 0.025f, 0.025f, 0.025f);
				_characterFacialManager.SetFacial("Lower", 0.02f);
			}
			else if (attackType == "Bite")
			{
				headCount++;
				biteCount++;
				reactionTime -= biteTime;
				talkType = "BiteBody";
				BodyReaction();
				Damage(1f, 0.75f);
				HeartBeat(0.15f, 0.02f, 0.02f, 0.02f);
				_characterFacialManager.SetFacial("Lower", 0.1f);
			}
			else if (attackType == "Lick")
			{
				headCount++;
				lickCount++;
				reactionTime -= lickTime;
				talkType = "LickBody";
				BodyReaction();
				Damage(0.5f, 0.5f);
				HeartBeat(0.05f, 0.025f, 0.025f, 0.025f);
				_characterFacialManager.SetFacial("Lower", 0.1f);
			}
			else if (attackType == "Touch")
			{
				headCount++;
				touchCount++;
				reactionTime -= touchTime;
				talkType = "TouchBody";
				BodyReaction();
				Damage(0.25f, 0.25f);
				HeartBeat(0.025f, 0.05f, 0.05f, 0.025f);
				_characterFacialManager.SetFacial("Lower", 0.02f);
			}
			else if (attackType == "Suck")
			{
				headCount++;
				suckCount++;
				reactionTime -= suckTime;
				talkType = "SuckBody";
				BodyReaction();
				Damage(0.025f, 1f);
				HeartBeat(0.1f, 0.025f, 0.025f, 0.025f);
				_characterFacialManager.SetFacial("Lower", 0.02f);
			}
		}
		else if (bodyType == "Mouth")
		{
			headCount++;
			if (attackType == "Piston")
			{
				pistonCount++;
				talkType = "MouthGag";
				_reactionAnimancer.ReactionSet("isReaction", 0.5f);
				ResetReactionTime();
				Damage(0.2f, 0.1f);
				HeartBeat(0.1f, 0.025f, 0.025f, 0.025f);
				_characterFacialManager.SetFacial("Both", 0.03f);
			}
			else if (attackType == "Ejaculation")
			{
				talkWaitTime = 0f;
				talkType = "MouthGagHard";
				_reactionAnimancer.ReactionSet("isReaction", 0.5f);
				ResetReactionTime();
				Damage(3f, 3f);
				HeartBeat(1f, 0.2f, 0.2f, 0.1f);
				_characterFaceManager.targetBlush += 0.2f;
				_characterFacialManager.SetFacial("Both", 0.3f);
				_characterFacialManager.HeadDizzy();
			}
		}
		else if (bodyType == "Eyes")
		{
			headCount++;
			if (attackType == "Hide")
			{
				talkWaitTime = 0f;
				talkType = "HideEyes";
				_reactionAnimancer.ReactionSet("isReaction", 0.5f);
				ResetReactionTime();
				Damage(0.5f, 0.5f);
				HeartBeat(0.3f, 0.1f, 0.1f, 0.1f);
				_characterFaceManager.targetBlush += 0.1f;
				_characterFacialManager.SetFacial("Both", 0.1f);
			}
		}
		else if (bodyType == "TitsL" || bodyType == "TitsR")
		{
			if (attackType == "Bukkake")
			{
				bukkakeCount++;
				reactionTime -= bukkakeTime;
				talkType = "Bukkake";
				Damage(0.5f, 0.1f);
				HeartBeat(0.025f, 0f, 0f, 0.025f);
				_characterFacialManager.SetFacial("Lower", 0.03f);
			}
			else if (attackType == "BukkakeMob")
			{
				bukkakeCount++;
				Damage(0.25f, 0.05f);
				HeartBeat(0.025f, 0f, 0f, 0.025f);
				_characterFacialManager.SetFacial("Lower", 0.02f);
			}
			else if (attackType == "Spanking")
			{
				spankingCount++;
				TitsMilk(3f);
				if (bodyType == "TitsL")
				{
					_characterEffectManager.TitsSplashL(3f);
				}
				if (bodyType == "TitsR")
				{
					_characterEffectManager.TitsSplashR(3f);
				}
				talkType = "SpankingTits";
				_reactionAnimancer.ReactionSet("isReaction", 0.5f);
				ResetReactionTime();
				Damage(1.5f, 1.5f);
				HeartBeat(0.3f, 0.05f, 0.05f, 0.05f);
				_characterFaceManager.targetBlush += 0.05f;
				_characterFacialManager.SetFacial("Both", 0.1f);
				if (isHeart && Random.value < 0.1f)
				{
					_characterEffectManager.VaginaDroplets();
				}
			}
			else if (attackType == "SpankingMob")
			{
				TitsMilk(2f);
				if (bodyType == "TitsL")
				{
					_characterEffectManager.TitsSplashL(1f);
				}
				if (bodyType == "TitsR")
				{
					_characterEffectManager.TitsSplashR(1f);
				}
				talkType = "SpankingTits";
				reactionTime -= spankingMobTime;
				Damage(0.5f, 0.75f);
				HeartBeat(0.3f, 0.05f, 0.05f, 0.05f);
				_characterFacialManager.SetFacial("Both", 0.05f);
				if (isHeart && Random.value < 0.1f)
				{
					_characterEffectManager.VaginaDroplets();
				}
			}
			else if (attackType == "Syringe")
			{
				syringeCount++;
				TitsMilk(5f);
				reactionTime -= syringeTime;
				talkType = "SyringeCritical";
				Damage(2f, 1f);
				HeartBeat(0.3f, 0.05f, 0.05f, 0.05f);
				_characterFacialManager.SetFacial("Both", 0.1f);
				if (isHeart && Random.value < 0.1f)
				{
					_characterEffectManager.VaginaDroplets();
				}
			}
			else if (attackType == "Player")
			{
				playerTitsCount++;
				TitsMilk(1f);
				if (bodyType == "TitsL")
				{
					_characterEffectManager.TitsSplashL(1f);
				}
				if (bodyType == "TitsR")
				{
					_characterEffectManager.TitsSplashR(1f);
				}
				talkType = "TouchCritical";
				_reactionAnimancer.ReactionSet("isReaction", 0.5f);
				ResetReactionTime();
				Damage(1.5f, 0.5f);
				HeartBeat(0.3f, 0.05f, 0.05f, 0.05f);
				_characterFaceManager.targetBlush += 0.025f;
				_characterFacialManager.SetFacial("Lower", 0.05f);
				if (isHeart && Random.value < 0.1f)
				{
					_characterEffectManager.VaginaDroplets();
				}
			}
			else if (attackType == "Charge")
			{
				playerTitsCount++;
				TitsMilk(2f);
				if (bodyType == "TitsL")
				{
					_characterEffectManager.TitsSplashL(2f);
				}
				if (bodyType == "TitsR")
				{
					_characterEffectManager.TitsSplashR(2f);
				}
				talkType = "TouchCritical";
				_reactionAnimancer.ReactionSet("isReaction", 0.5f);
				ResetReactionTime();
				Damage(3f, 1f);
				HeartBeat(0.3f, 0.05f, 0.05f, 0.05f);
				_characterFaceManager.targetBlush += 0.05f;
				_characterFacialManager.SetFacial("Lower", 0.1f);
				if (isHeart && Random.value < 0.1f)
				{
					_characterEffectManager.VaginaDroplets();
				}
			}
			else if (attackType == "Rub")
			{
				rubCount++;
				TitsMilk(0.5f);
				reactionTime -= rubTime;
				talkType = "RubCritical";
				BodyReaction();
				Damage(0.5f, 0.25f);
				HeartBeat(0.05f, 0.025f, 0.025f, 0.025f);
				_characterFacialManager.SetFacial("Lower", 0.02f);
				if (isHeart && Random.value < 0.05f)
				{
					_characterEffectManager.VaginaDroplets();
				}
			}
			else if (attackType == "Bite")
			{
				biteCount++;
				TitsMilk(0.5f);
				reactionTime -= biteTime;
				talkType = "BiteCritical";
				BodyReaction();
				Damage(1f, 0.75f);
				HeartBeat(0.15f, 0.02f, 0.02f, 0.02f);
				_characterFacialManager.SetFacial("Lower", 0.1f);
				if (isHeart && Random.value < 0.05f)
				{
					_characterEffectManager.VaginaDroplets();
				}
			}
			else if (attackType == "Lick")
			{
				lickCount++;
				TitsMilk(0.5f);
				reactionTime -= lickTime;
				talkType = "LickCritical";
				BodyReaction();
				Damage(0.5f, 0.5f);
				HeartBeat(0.05f, 0.025f, 0.025f, 0.025f);
				_characterFacialManager.SetFacial("Lower", 0.1f);
				if (isHeart && Random.value < 0.05f)
				{
					_characterEffectManager.VaginaDroplets();
				}
			}
			else if (attackType == "Touch")
			{
				touchCount++;
				TitsMilk(1f);
				reactionTime -= touchTime;
				talkType = "TouchCritical";
				BodyReaction();
				Damage(0.25f, 0.25f);
				HeartBeat(0.025f, 0.05f, 0.05f, 0.025f);
				_characterFacialManager.SetFacial("Lower", 0.02f);
				if (isHeart && Random.value < 0.1f)
				{
					_characterEffectManager.VaginaDroplets();
				}
			}
			else if (attackType == "Suck")
			{
				TitsMilk(0.5f);
				if (bodyType == "TitsL")
				{
					_characterEffectManager.TitsSplashL(1f);
				}
				if (bodyType == "TitsR")
				{
					_characterEffectManager.TitsSplashR(1f);
				}
				reactionTime -= suckTime;
				talkType = "SuckCritical";
				BodyReaction();
				Damage(0.05f, 1f);
				HeartBeat(0.1f, 0.025f, 0.025f, 0.025f);
				_characterFacialManager.SetFacial("Lower", 0.02f);
				if (isHeart && Random.value < 0.1f)
				{
					_characterEffectManager.VaginaDroplets();
				}
			}
			else if (attackType == "SuckTits")
			{
				TitsMilk(2f);
				reactionTime -= suckTime;
				talkType = "SuckCritical";
				BodyReaction();
				Damage(0.25f, 1f);
				HeartBeat(0.2f, 0.025f, 0.025f, 0.025f);
				_characterFacialManager.SetFacial("Lower", 0.02f);
				if (isHeart && Random.value < 0.1f)
				{
					_characterEffectManager.VaginaDroplets();
				}
			}
		}
		else if (bodyType == "Tits")
		{
			TitsMilk(1f);
			if (attackType == "Press")
			{
				talkType = "TouchCritical";
				_reactionAnimancer.ReactionSet("isReaction", 0.5f);
				ResetReactionTime();
				Damage(0.5f, 0.25f);
				HeartBeat(0.2f, 0.1f, 0.1f, 0.05f);
				if (isHeart && Random.value < 0.05f)
				{
					_characterEffectManager.VaginaDroplets();
				}
			}
			else if (attackType == "Piston")
			{
				pistonCount++;
				talkType = "TitsPiston";
				_reactionAnimancer.ReactionSet("isReaction", 0.5f);
				ResetReactionTime();
				Damage(0.5f, 0.1f);
				HeartBeat(0.1f, 0.025f, 0.025f, 0.025f);
				_characterFacialManager.SetFacial("Both", 0.03f);
			}
			else if (attackType == "Ejaculation")
			{
				talkWaitTime = 0f;
				talkType = "TitsEjaculation";
				_reactionAnimancer.ReactionSet("isReaction", 0.5f);
				ResetReactionTime();
				Damage(5f, 3f);
				HeartBeat(1f, 0.2f, 0.2f, 0.1f);
				_characterFaceManager.targetBlush += 0.2f;
				_characterFacialManager.SetFacial("Both", 0.3f);
				_characterFacialManager.HeadDizzy();
			}
		}
		else if (bodyType == "HipL" || bodyType == "HipR")
		{
			if (attackType == "Bukkake")
			{
				bukkakeCount++;
				reactionTime -= bukkakeTime;
				talkType = "Bukkake";
				Damage(0.5f, 0.1f);
				HeartBeat(0.025f, 0f, 0f, 0.025f);
				_characterFacialManager.SetFacial("Lower", 0.03f);
			}
			else if (attackType == "BukkakeMob")
			{
				bukkakeCount++;
				Damage(0.25f, 0.05f);
				HeartBeat(0.025f, 0f, 0f, 0.025f);
				_characterFacialManager.SetFacial("Lower", 0.02f);
			}
			else if (attackType == "Spanking")
			{
				hipCount++;
				spankingCount++;
				talkType = "SpankingBody";
				_reactionAnimancer.ReactionSet("isReaction", 0.5f);
				ResetReactionTime();
				Damage(1.5f, 1.5f);
				HeartBeat(0.3f, 0.05f, 0.05f, 0.05f);
				_characterFaceManager.targetBlush += 0.05f;
				_characterFacialManager.SetFacial("Both", 0.1f);
				if (isHeart && Random.value < 0.1f)
				{
					_characterEffectManager.VaginaDroplets();
				}
			}
			else if (attackType == "SpankingMob")
			{
				hipCount++;
				talkType = "SpankingBody";
				reactionTime -= spankingMobTime;
				Damage(0.5f, 0.75f);
				HeartBeat(0.3f, 0.05f, 0.05f, 0.05f);
				_characterFacialManager.SetFacial("Both", 0.05f);
				if (isHeart && Random.value < 0.1f)
				{
					_characterEffectManager.VaginaDroplets();
				}
			}
			else if (attackType == "Syringe")
			{
				hipCount++;
				syringeCount++;
				reactionTime -= syringeTime;
				talkType = "SyringeBody";
				Damage(1f, 1f);
				HeartBeat(0.3f, 0.05f, 0.05f, 0.05f);
				_characterFacialManager.SetFacial("Both", 0.1f);
				if (isHeart && Random.value < 0.1f)
				{
					_characterEffectManager.VaginaDroplets();
				}
			}
			else if (attackType == "Player")
			{
				hipCount++;
				playerHipCount++;
				_reactionAnimancer.ReactionSet("isReaction", 0.5f);
				ResetReactionTime();
				Damage(1.5f, 0.5f);
				HeartBeat(0.3f, 0.05f, 0.05f, 0.05f);
				_characterFaceManager.targetBlush += 0.05f;
				_characterFacialManager.SetFacial("Lower", 0.1f);
				if (isHeart && Random.value < 0.1f)
				{
					_characterEffectManager.VaginaDroplets();
				}
			}
			else if (attackType == "Charge")
			{
				hipCount++;
				playerHipCount++;
				_reactionAnimancer.ReactionSet("isReaction", 0.5f);
				ResetReactionTime();
				Damage(2f, 1f);
				HeartBeat(0.3f, 0.05f, 0.05f, 0.05f);
				_characterFaceManager.targetBlush += 0.05f;
				_characterFacialManager.SetFacial("Lower", 0.1f);
				if (isHeart && Random.value < 0.1f)
				{
					_characterEffectManager.VaginaDroplets();
				}
			}
			else if (attackType == "Rub")
			{
				hipCount++;
				rubCount++;
				reactionTime -= rubTime;
				talkType = "RubBody";
				BodyReaction();
				Damage(0.5f, 0.25f);
				HeartBeat(0.05f, 0.025f, 0.025f, 0.025f);
				_characterFacialManager.SetFacial("Lower", 0.02f);
				if (isHeart && Random.value < 0.1f)
				{
					_characterEffectManager.VaginaDroplets();
				}
			}
			else if (attackType == "Bite")
			{
				hipCount++;
				biteCount++;
				reactionTime -= biteTime;
				talkType = "BiteBody";
				BodyReaction();
				Damage(1f, 0.75f);
				HeartBeat(0.15f, 0.02f, 0.02f, 0.02f);
				_characterFacialManager.SetFacial("Lower", 0.1f);
				if (isHeart && Random.value < 0.1f)
				{
					_characterEffectManager.VaginaDroplets();
				}
			}
			else if (attackType == "Lick")
			{
				hipCount++;
				lickCount++;
				reactionTime -= lickTime;
				talkType = "LickBody";
				BodyReaction();
				Damage(0.5f, 0.5f);
				HeartBeat(0.05f, 0.025f, 0.025f, 0.025f);
				_characterFacialManager.SetFacial("Lower", 0.1f);
				if (isHeart && Random.value < 0.1f)
				{
					_characterEffectManager.VaginaDroplets();
				}
			}
			else if (attackType == "Touch")
			{
				hipCount++;
				touchCount++;
				reactionTime -= touchTime;
				talkType = "TouchBody";
				BodyReaction();
				Damage(0.25f, 0.25f);
				HeartBeat(0.025f, 0.05f, 0.05f, 0.025f);
				_characterFacialManager.SetFacial("Lower", 0.02f);
				if (isHeart && Random.value < 0.1f)
				{
					_characterEffectManager.VaginaDroplets();
				}
			}
			else if (attackType == "Suck")
			{
				hipCount++;
				suckCount++;
				reactionTime -= suckTime;
				talkType = "SuckBody";
				BodyReaction();
				Damage(0.05f, 1f);
				HeartBeat(0.1f, 0.025f, 0.025f, 0.025f);
				_characterFacialManager.SetFacial("Lower", 0.02f);
			}
		}
		else if (bodyType == "Vagina")
		{
			if (attackType == "Bukkake")
			{
				bukkakeCount++;
				reactionTime -= bukkakeTime;
				talkType = "Bukkake";
				Damage(0.5f, 1f);
				HeartBeat(0.025f, 0f, 0f, 0.025f);
				_characterFacialManager.SetFacial("Lower", 0.03f);
			}
			else if (attackType == "BukkakeMob")
			{
				bukkakeCount++;
				Damage(0.25f, 0.05f);
				HeartBeat(0.025f, 0f, 0f, 0.025f);
				_characterFacialManager.SetFacial("Lower", 0.02f);
			}
			else if (attackType == "Spanking")
			{
				vaginaCount++;
				spankingCount++;
				talkType = "SpankingBody";
				_reactionAnimancer.ReactionSet("isReaction", 0.5f);
				ResetReactionTime();
				Damage(1.5f, 1.5f);
				HeartBeat(0.3f, 0.05f, 0.05f, 0.05f);
				_characterFacialManager.SetFacial("Both", 0.1f);
				if (isHeart && Random.value < 0.1f)
				{
					_characterEffectManager.VaginaDroplets();
				}
			}
			else if (attackType == "SpankingMob")
			{
				vaginaCount++;
				talkType = "SpankingBody";
				reactionTime -= spankingMobTime;
				Damage(0.5f, 0.75f);
				HeartBeat(0.3f, 0.05f, 0.05f, 0.05f);
				_characterFacialManager.SetFacial("Both", 0.05f);
				if (isHeart && Random.value < 0.1f)
				{
					_characterEffectManager.VaginaDroplets();
				}
			}
			else if (attackType == "Syringe")
			{
				vaginaCount++;
				syringeCount++;
				reactionTime -= syringeTime;
				talkType = "SyringeCritical";
				Damage(2f, 1f);
				HeartBeat(0.3f, 0.05f, 0.05f, 0.05f);
				_characterFacialManager.SetFacial("Both", 0.1f);
				if (isHeart && Random.value < 0.1f)
				{
					_characterEffectManager.VaginaDroplets();
				}
			}
			else if (attackType == "Player")
			{
				vaginaCount++;
				_reactionAnimancer.ReactionSet("isReaction", 0.5f);
				ResetReactionTime();
				Damage(1.5f, 0.5f);
				HeartBeat(0.3f, 0.05f, 0.05f, 0.05f);
				_characterFacialManager.SetFacial("Lower", 0.1f);
				if (isHeart && Random.value < 0.1f)
				{
					_characterEffectManager.VaginaDroplets();
				}
			}
			else if (attackType == "Rub")
			{
				vaginaCount++;
				rubCount++;
				reactionTime -= rubTime;
				talkType = "RubCritical";
				BodyReaction();
				Damage(0.5f, 0.25f);
				HeartBeat(0.05f, 0.025f, 0.025f, 0.025f);
				_characterFacialManager.SetFacial("Lower", 0.02f);
				if (isHeart && Random.value < 0.1f)
				{
					_characterEffectManager.VaginaDroplets();
				}
			}
			else if (attackType == "Bite")
			{
				vaginaCount++;
				biteCount++;
				TitsMilk(0.5f);
				reactionTime -= biteTime;
				talkType = "BiteCritical";
				BodyReaction();
				Damage(1f, 0.75f);
				HeartBeat(0.15f, 0.02f, 0.02f, 0.02f);
				_characterFacialManager.SetFacial("Lower", 0.1f);
				if (isHeart && Random.value < 0.05f)
				{
					_characterEffectManager.VaginaDroplets();
				}
			}
			else if (attackType == "Lick")
			{
				vaginaCount++;
				lickCount++;
				TitsMilk(0.5f);
				reactionTime -= lickTime;
				talkType = "LickCritical";
				BodyReaction();
				Damage(0.5f, 0.5f);
				HeartBeat(0.05f, 0.025f, 0.025f, 0.025f);
				_characterFacialManager.SetFacial("Lower", 0.1f);
				if (isHeart && Random.value < 0.05f)
				{
					_characterEffectManager.VaginaDroplets();
				}
			}
			else if (attackType == "Touch")
			{
				vaginaCount++;
				touchCount++;
				reactionTime -= touchTime;
				talkType = "TouchCritical";
				BodyReaction();
				Damage(0.25f, 0.25f);
				HeartBeat(0.025f, 0.05f, 0.05f, 0.025f);
				_characterFacialManager.SetFacial("Lower", 0.03f);
				if (isHeart && Random.value < 0.1f)
				{
					_characterEffectManager.VaginaDroplets();
				}
			}
			else if (attackType == "Suck")
			{
				vaginaCount++;
				suckCount++;
				reactionTime -= suckTime;
				talkType = "SuckCritical";
				BodyReaction();
				Damage(0.05f, 1f);
				HeartBeat(0.1f, 0.025f, 0.025f, 0.025f);
				_characterFacialManager.SetFacial("Lower", 0.02f);
			}
			else if (attackType == "LostVirsin" && !isLostVirsin)
			{
				talkWaitTime = 0f;
				talkType = "LostVirsin";
				Damage(5f, 0.5f);
				HeartBeat(1f, 0.5f, 0.5f, 0.2f);
				_characterFacialManager.SetFacial("Both", 0.03f);
				isLostVirsin = true;
				_characterEffectManager.LostVirsin();
				_characterFacialManager.HeadDizzy();
				_heartGUIManager.LostVirginEffect();
			}
			else if (attackType == "Insert")
			{
				vaginaCount++;
				talkWaitTime = 0f;
				talkType = "Reaction";
				Damage(1f, 0.5f);
				HeartBeat(1f, 0.2f, 0.2f, 0.1f);
				_characterFacialManager.SetFacial("Both", 0.03f);
			}
			else if (attackType == "Piston")
			{
				vaginaCount++;
				pistonCount++;
				reactionTime -= pistonTime;
				talkType = "VaginaPiston";
				Damage(0.5f, 0.1f);
				HeartBeat(0.1f, 0.025f, 0.025f, 0.025f);
				_characterFacialManager.SetFacial("Both", 0.03f);
			}
			else if (attackType == "Ejaculation")
			{
				vaginaCount++;
				talkWaitTime = 0f;
				talkType = "VaginaEjaculation";
				_reactionAnimancer.ReactionSet("isReaction", 0.5f);
				ResetReactionTime();
				Damage(5f, 3f);
				HeartBeat(1f, 0.2f, 0.2f, 0.1f);
				_characterFaceManager.targetBlush += 0.2f;
				_characterFacialManager.SetFacial("Both", 0.3f);
				_characterFacialManager.HeadDizzy();
			}
			else if (attackType == "Child")
			{
				talkWaitTime = 0f;
				talkType = "VaginaChild";
				_reactionAnimancer.ReactionSet("isReaction", 0.5f);
				ResetReactionTime();
				Damage(10f, 2f);
				HeartBeat(0.4f, 0.1f, 0.1f, 0.1f);
				_characterFaceManager.targetBlush += 0.1f;
				_characterFacialManager.SetFacial("Both", 0.05f);
				_characterFacialManager.HeadDizzy();
			}
		}
		else if (bodyType == "Anal")
		{
			if (attackType == "Bukkake")
			{
				bukkakeCount++;
				reactionTime -= bukkakeTime;
				talkType = "Bukkake";
				Damage(0.5f, 0.1f);
				HeartBeat(0.025f, 0f, 0f, 0.025f);
				_characterFacialManager.SetFacial("Lower", 0.03f);
			}
			else if (attackType == "BukkakeMob")
			{
				bukkakeCount++;
				Damage(0.25f, 0.05f);
				HeartBeat(0.025f, 0f, 0f, 0.025f);
				_characterFacialManager.SetFacial("Lower", 0.02f);
			}
			else if (attackType == "Spanking")
			{
				analCount++;
				spankingCount++;
				talkType = "SpankingBody";
				_reactionAnimancer.ReactionSet("isReaction", 0.5f);
				ResetReactionTime();
				Damage(1.5f, 1.5f);
				HeartBeat(0.3f, 0.05f, 0.05f, 0.05f);
				_characterFacialManager.SetFacial("Both", 0.1f);
				if (isHeart && Random.value < 0.1f)
				{
					_characterEffectManager.VaginaDroplets();
				}
			}
			else if (attackType == "SpankingMob")
			{
				analCount++;
				talkType = "SpankingBody";
				reactionTime -= spankingMobTime;
				Damage(0.5f, 0.75f);
				HeartBeat(0.3f, 0.05f, 0.05f, 0.05f);
				_characterFacialManager.SetFacial("Both", 0.05f);
				if (isHeart && Random.value < 0.1f)
				{
					_characterEffectManager.VaginaDroplets();
				}
			}
			else if (attackType == "Syringe")
			{
				analCount++;
				syringeCount++;
				reactionTime -= syringeTime;
				talkType = "SyringeCritical";
				Damage(2f, 1f);
				HeartBeat(0.3f, 0.05f, 0.05f, 0.05f);
				_characterFacialManager.SetFacial("Both", 0.1f);
				if (isHeart && Random.value < 0.1f)
				{
					_characterEffectManager.VaginaDroplets();
				}
			}
			else if (attackType == "Player")
			{
				analCount++;
				_reactionAnimancer.ReactionSet("isReaction", 0.5f);
				ResetReactionTime();
				Damage(1.5f, 0.5f);
				HeartBeat(0.3f, 0.05f, 0.05f, 0.05f);
				_characterFacialManager.SetFacial("Lower", 0.1f);
				if (isHeart && Random.value < 0.1f)
				{
					_characterEffectManager.VaginaDroplets();
				}
			}
			else if (attackType == "Rub")
			{
				analCount++;
				rubCount++;
				reactionTime -= rubTime;
				talkType = "RubCritical";
				BodyReaction();
				Damage(0.5f, 0.25f);
				HeartBeat(0.05f, 0.025f, 0.025f, 0.025f);
				_characterFacialManager.SetFacial("Lower", 0.02f);
				if (isHeart && Random.value < 0.1f)
				{
					_characterEffectManager.VaginaDroplets();
				}
			}
			else if (attackType == "Bite")
			{
				vaginaCount++;
				biteCount++;
				TitsMilk(0.5f);
				reactionTime -= biteTime;
				talkType = "BiteCritical";
				BodyReaction();
				Damage(1f, 0.75f);
				HeartBeat(0.15f, 0.02f, 0.02f, 0.02f);
				_characterFacialManager.SetFacial("Lower", 0.1f);
				if (isHeart && Random.value < 0.05f)
				{
					_characterEffectManager.VaginaDroplets();
				}
			}
			else if (attackType == "Lick")
			{
				vaginaCount++;
				lickCount++;
				TitsMilk(0.5f);
				reactionTime -= lickTime;
				talkType = "LickCritical";
				BodyReaction();
				Damage(0.5f, 0.5f);
				HeartBeat(0.05f, 0.025f, 0.025f, 0.025f);
				_characterFacialManager.SetFacial("Lower", 0.1f);
				if (isHeart && Random.value < 0.05f)
				{
					_characterEffectManager.VaginaDroplets();
				}
			}
			else if (attackType == "Touch")
			{
				analCount++;
				touchCount++;
				reactionTime -= touchTime;
				talkType = "TouchCritical";
				BodyReaction();
				Damage(0.25f, 0.25f);
				HeartBeat(0.025f, 0.05f, 0.05f, 0.025f);
				_characterFacialManager.SetFacial("Lower", 0.02f);
				if (isHeart && Random.value < 0.1f)
				{
					_characterEffectManager.VaginaDroplets();
				}
			}
			else if (attackType == "Suck")
			{
				analCount++;
				suckCount++;
				reactionTime -= suckTime;
				talkType = "SuckCritical";
				BodyReaction();
				Damage(0.05f, 1f);
				HeartBeat(0.1f, 0.025f, 0.025f, 0.025f);
				_characterFacialManager.SetFacial("Lower", 0.02f);
			}
			else if (attackType == "Piston")
			{
				analCount++;
				pistonCount++;
				reactionTime -= pistonTime;
				talkType = "AnalPiston";
				Damage(0.5f, 0.1f);
				HeartBeat(0.1f, 0.025f, 0.025f, 0.025f);
				_characterFacialManager.SetFacial("Both", 0.03f);
			}
			else if (attackType == "Ejaculation")
			{
				analCount++;
				talkWaitTime = 0f;
				talkType = "AnalEjaculation";
				_reactionAnimancer.ReactionSet("isReaction", 0.5f);
				ResetReactionTime();
				Damage(5f, 3f);
				HeartBeat(1f, 0.2f, 0.2f, 0.1f);
				_characterFaceManager.targetBlush += 0.2f;
				_characterFacialManager.SetFacial("Both", 0.3f);
				_characterFacialManager.HeadDizzy();
			}
			else if (attackType == "Child")
			{
				talkWaitTime = 0f;
				talkType = "AnalChild";
				_reactionAnimancer.ReactionSet("isReaction", 0.5f);
				ResetReactionTime();
				Damage(10f, 2f);
				HeartBeat(0.4f, 0.1f, 0.1f, 0.1f);
				_characterFaceManager.targetBlush += 0.1f;
				_characterFacialManager.SetFacial("Both", 0.05f);
				_characterFacialManager.HeadDizzy();
			}
		}
		else if (bodyType == "Squeeze")
		{
			if (attackType == "Squeeze")
			{
				int bindBodyCount = _squeezeManager.bindBodyCount;
				bodyCount += bindBodyCount;
				headCount += _squeezeManager.bindHeadCount;
				squeezeCount += bindBodyCount;
				talkType = "Squeeze";
				_reactionAnimancer.ReactionSet("isReaction", 0.5f);
				ResetReactionTime();
				Damage((float)bindBodyCount * 0.1f, (float)bindBodyCount * 0.1f);
				HeartBeat(0.2f, 0.05f, 0.05f, 0.05f);
				_characterFaceManager.targetBlush += 0.25f;
				if (isHeart && Random.value < 0.1f)
				{
					_characterEffectManager.VaginaDroplets();
				}
			}
		}
		else if (bodyType == "Gimmick")
		{
			HeartBeat(0.75f, 0.2f, 0.2f, 0.1f);
			if (attackType == "Eater")
			{
				talkWaitTime = 0f;
				talkType = "Eater";
				Damage(3f, 1f);
				_characterFaceManager.targetBlush += 0.2f;
				_characterFacialManager.SetFacial("Upper", 0.1f);
			}
			else if (attackType == "HorseRide")
			{
				talkWaitTime = 0f;
				talkType = "HorseRide";
				Damage(3f, 1f);
				_characterFaceManager.targetBlush += 0.2f;
				_characterFacialManager.SetFacial("Lower", 0.1f);
			}
			else if (attackType == "LimbHold")
			{
				talkWaitTime = 0f;
				talkType = "LimbHold";
				Damage(3f, 1f);
				_characterFaceManager.targetBlush += 0.2f;
				_characterFacialManager.SetFacial("Lower", 0.1f);
			}
			else if (attackType == "WartBed")
			{
				talkWaitTime = 0f;
				talkType = "WartBed";
				Damage(3f, 1f);
				_characterFaceManager.targetBlush += 0.2f;
				_characterFacialManager.SetFacial("Lower", 0.1f);
			}
			else if (attackType == "WallHip")
			{
				talkWaitTime = 0f;
				talkType = "WallHip";
				Damage(3f, 1f);
				_characterFaceManager.targetBlush += 0.2f;
				_characterFacialManager.SetFacial("Lower", 0.1f);
			}
		}
		else if (bodyType == "Fuck")
		{
			HeartBeat(0.75f, 0.2f, 0.2f, 0.1f);
			if (attackType == "FuckOn")
			{
				talkWaitTime = 0f;
				talkType = "FuckOn";
				Damage(5f, 1f);
				_characterFaceManager.targetBlush += 0.1f;
				_characterFacialManager.SetFacial("Lower", 0.1f);
			}
		}
		else if (bodyType == "Costume0")
		{
			HeartBeat(0.5f, 0.2f, 0.2f, 0.1f);
			if (attackType == "Break")
			{
				talkWaitTime = 0f;
				talkType = "Costume0Break";
				_reactionAnimancer.ReactionSet("isReaction", 0.5f);
				ResetReactionTime();
				BodyReaction();
				Damage(10f, 0.5f);
				_characterFacialManager.SetFacial("Shock", 0.1f);
				_characterFaceManager.targetBlush += 0.1f;
			}
		}
		else if (bodyType == "Costume1")
		{
			HeartBeat(0.5f, 0.2f, 0.2f, 0.2f);
			if (attackType == "Break")
			{
				talkWaitTime = 0f;
				talkType = "Costume1Break";
				_reactionAnimancer.ReactionSet("isReaction", 0.5f);
				ResetReactionTime();
				BodyReaction();
				Damage(20f, 1f);
				_characterFacialManager.SetFacial("Shock", 0.2f);
				_characterFaceManager.targetBlush += 0.2f;
			}
		}
		else if (bodyType == "Costume2")
		{
			HeartBeat(0.5f, 0.2f, 0.2f, 0.3f);
			if (attackType == "Break")
			{
				talkWaitTime = 0f;
				talkType = "Costume2Break";
				_reactionAnimancer.ReactionSet("isReaction", 0.5f);
				ResetReactionTime();
				BodyReaction();
				Damage(30f, 1.5f);
				_characterFacialManager.SetFacial("Shock", 0.25f);
				_characterFaceManager.targetBlush += 0.3f;
			}
		}
		if (talkType != "" && talkWaitTime <= 0f)
		{
			if (talkType == "Bukkake" && Random.Range(0, 10) < 2)
			{
				talkType = "";
			}
			else if (talkType == "SpankingBody" && Random.Range(0, 10) < 3)
			{
				talkType = "";
			}
			else if (talkType == "VaginaPiston" && Random.Range(0, 10) < 2)
			{
				talkType = "";
			}
			else if (talkType == "MouthPiston" && Random.Range(0, 10) < 2)
			{
				talkType = "";
			}
			else if (talkType == "TouchBody" && Random.Range(0, 10) < 3)
			{
				talkType = "";
			}
			else if (talkType == "SuckBody" && Random.Range(0, 10) < 3)
			{
				talkType = "";
			}
			if (talkType == "VaginaEjaculation" || talkType == "MouthGagHard")
			{
				_characterTalkManager.SetForceTalk(talkType);
				ResetTalkTime();
			}
			else if (talkType == "Reaction")
			{
				_characterMouthManager.PlayHitSe();
			}
			else if (talkType == "Orgasm" || talkType == "LostVirsin")
			{
				_characterTalkManager.SetForceTalk(talkType);
				ResetTalkTime();
				_characterMouthManager.PlayOrgasmSe();
			}
			else if (talkType != "")
			{
				_characterTalkManager.SetBarkTalk(talkType);
				ResetTalkTime();
			}
			else
			{
				_characterMouthManager.PlayHitSe();
			}
		}
		else if (voiceWaitTime < 0f && talkWaitTime > 1f)
		{
			_characterMouthManager.PlayHitSe();
			ResetVoiceTime();
		}
	}

	public void Talk(string type)
	{
		_characterTalkManager.SetForceTalk(type);
	}

	public void TalkCount(string type)
	{
		_characterTalkManager.SetCountTalk(type);
	}

	public void BodyReaction()
	{
		if (!isReactionLock)
		{
			_reactionTargetAnimaton.targetX += Random.Range(0.25f, 0.5f);
			_reactionTargetAnimaton.targetY += Random.Range(-0.25f, 0.25f);
		}
	}

	public void SpankingPee(bool mob)
	{
		if (!mob)
		{
			peeCurrent--;
		}
		else if (mob && peeCurrent > peeLimitMob)
		{
			peeCurrent--;
		}
		if (peeCurrent < 0)
		{
			Damage(5f, 3f);
			peeCurrent = Random.Range(peeLimitMin, peeLimitMax);
			_characterEffectManager.PeeSplash();
			peeCount++;
		}
	}

	public void HeartBeat(float damage, float speed, float beat, float color)
	{
		_heartBeatManager.Beat(damage, speed, beat, color);
	}

	public void Damage(float value, float heart)
	{
		_EXPGUIManager.GetEXP(value);
		_heartGUIManager.SetHeart(heart);
	}

	public void TitsMilk(float value)
	{
		titsCount++;
		titsMilkCount += value;
		_milkGUIManager.SetMilk(value);
	}

	public void SpawnVaginaChild(int value)
	{
		_uterusChildFeelerManager.SpawnFeeler(value);
	}

	public void SpawnAnalChild(int value)
	{
		_analChildFeelerManager.SpawnFeeler(value);
	}

	public void BirthChild(int value)
	{
		if (!isVaginaGag && !isUnBirth)
		{
			_uterusChildFeelerManager.BirthFeeler(2);
			Damage(3f, 1f);
		}
		else
		{
			isVaginaChildStack += 2;
		}
		if (!isAnalGag && !isUnBirth)
		{
			_analChildFeelerManager.BirthFeeler(2);
			Damage(3f, 1f);
		}
		else
		{
			isAnalChildStack += 2;
		}
	}

	public void BellyBirthChild()
	{
		if (!isVaginaGag && !isUnBirth)
		{
			_uterusChildFeelerManager.BirthFeeler(1);
			Damage(3f, 1f);
		}
		else
		{
			isVaginaChildStack++;
		}
		if (!isAnalGag && !isUnBirth)
		{
			_analChildFeelerManager.BirthFeeler(1);
			Damage(3f, 1f);
		}
		else
		{
			isAnalChildStack++;
		}
	}

	public void GagBirthChild()
	{
		if (!isVaginaGag && !isUnBirth)
		{
			if (isVaginaChildStack > 5)
			{
				isVaginaChildStack = 5;
			}
			_uterusChildFeelerManager.BirthFeeler(isVaginaChildStack);
			isVaginaChildStack = 0;
		}
		if (!isAnalGag && !isUnBirth)
		{
			if (isAnalChildStack > 5)
			{
				isAnalChildStack = 5;
			}
			_analChildFeelerManager.BirthFeeler(isAnalChildStack);
			isAnalChildStack = 0;
		}
	}

	public void VaginaShotEnd()
	{
		isVaginaWait = false;
		_feelerControllerData.VaginaShotEnd();
	}

	public void AnalShotEnd()
	{
		isAnalWait = false;
		_feelerControllerData.AnalShotEnd();
	}

	public void MouthShotEnd()
	{
		isMouthWait = false;
		_feelerControllerData.MouthShotEnd();
	}

	public void TitsShotEnd()
	{
		isTitsWait = false;
		_feelerControllerData.TitsShotEnd();
	}

	public void PlayHitSe()
	{
		_characterMouthManager.PlayHitSe();
	}

	public void PlayOrgasmSe()
	{
		_characterMouthManager.PlayOrgasmSe();
	}
}
