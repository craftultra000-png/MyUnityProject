using System.Collections.Generic;
using Animancer;
using UnityEngine;

public class CharacterFaceManager1 : MonoBehaviour
{
	public Animator _animator;

	public AnimancerComponent _animancer;

	public CharacterHead _characterHead;

	public CharacterLookAt _characterLookAt;

	public CharacterSoundManager _characterSoundManager;

	public CharacterEffectManager _characterEffectManager;

	[Header("Data")]
	public bool active;

	public bool unCloseEyes;

	[Header("Parameter")]
	public bool rightClick;

	public bool stopBreath;

	public bool useBreath;

	public bool unLookHead;

	public bool unLookEyes;

	public bool useLook;

	public bool useMouthAnim;

	[Header("Status")]
	public bool isBlink;

	public bool isBreath;

	public bool isAIUEO;

	[Header("Skinned Mesh")]
	public SkinnedMeshRenderer headMesh;

	public SkinnedMeshRenderer eyesMesh;

	private Mesh headSkinnedMesh;

	private Mesh eyesSkinnedMesh;

	[Header("Bone")]
	public Transform headBone;

	[Header("Emote Count")]
	public int emoteCount;

	public int emoteCountMin = 3;

	public int emoteCountMax = 8;

	[Header("Eyes Open Half Close 0")]
	public int eyesChoice;

	public List<int> eyesChoiceList;

	public List<AnimationClip> eyesOpenClip;

	public AnimancerLayer eyesOpenLayer;

	[Header("Facial 1")]
	public float facialSpeed;

	public string currentFacial;

	public float faicalWait;

	public List<AnimationClip> facialClip;

	public List<AnimationClip> facialLightClip;

	public List<AnimationClip> facialEmoteClip;

	public AnimancerLayer facialLayer;

	[Header("Mouth 3")]
	public bool mouthForceEnd;

	public AnimationClip mouthIdleClip;

	public List<AnimationClip> mouseLightClip;

	public List<AnimationClip> mouseEmoteClip;

	public AnimancerLayer mouthLayer;

	[Header("Mouth Motion2")]
	public AnimationClip mouthOpenClip;

	public AnimationClip mouthFullOpenClip;

	public AnimationClip mouthKissClip;

	public AnimationClip mouthTongueDripOutClip;

	public AnimationClip mouthToothClip;

	public AnimationClip mouthThreatClip;

	public AnimationClip mouthFingerBiteClip;

	public AnimationClip mouthFingerChewClip;

	public AnimationClip mouthFingerSuckClip;

	public List<AnimationClip> mouthDeepKissClip;

	public List<AnimationClip> mouthLickSideClip;

	public AnimancerLayer mouthMotionLayer;

	[Header("Mouth AIUEO 4")]
	public float aiueoSpeed;

	public List<int> mouthAIUEOList;

	public AnimationClip mouthAIUEOIdleClip;

	public List<AnimationClip> mouthAIUEOClip;

	public AnimancerLayer mouthAIUEOLayer;

	[Header("Blink 5")]
	public float currentBlinkWait;

	public float waitBlinkMin = 5f;

	public float waitBlinkMax = 10f;

	public float eyesSpeed;

	public List<AnimationClip> blinkClip;

	public AnimancerLayer blinkLayer;

	[Header("Breath 6")]
	public float currentBreathWait;

	public float waitBreathMin = 5f;

	public float waitBreathMax = 15f;

	public float waitHardBreathMin = 3f;

	public float waitHardBreathMax = 6f;

	public float mouseSpeed;

	public List<AnimationClip> breathClip;

	public AnimancerLayer breathLayer;

	[Header("Voice")]
	public AudioSource audioVoice;

	private AudioClip playClip;

	public List<AudioClip> voiceHit = new List<AudioClip>();

	public List<AudioClip> voiceOrgasm = new List<AudioClip>();

	[Header("Breath")]
	public bool hardBreath;

	public List<AudioClip> voiceBreathSoft = new List<AudioClip>();

	public List<AudioClip> voiceBreathHard = new List<AudioClip>();

	private void Awake()
	{
		headSkinnedMesh = headMesh.sharedMesh;
		eyesSkinnedMesh = eyesMesh.sharedMesh;
	}

	private void Start()
	{
		currentBlinkWait = Random.Range(waitBlinkMin, waitBlinkMax);
		currentBreathWait = Random.Range(waitBreathMin, waitBreathMax);
		useBreath = true;
		emoteCount = Random.Range(emoteCountMin, emoteCountMax);
		eyesChoiceList.Clear();
		eyesChoiceList = new List<int> { 0, 0, 0, 0, 1, 1, 2 };
		eyesOpenLayer = _animancer.Layers[0];
		eyesOpenLayer.IsAdditive = false;
		eyesOpenLayer.Play(eyesOpenClip[0], 0.25f);
		facialLayer = _animancer.Layers[1];
		facialLayer.IsAdditive = false;
		facialLayer.Play(facialClip[0], 0.25f);
		mouthMotionLayer = _animancer.Layers[2];
		mouthMotionLayer.IsAdditive = false;
		mouthMotionLayer.Play(mouthIdleClip, 0.1f);
		mouthLayer = _animancer.Layers[3];
		mouthLayer.IsAdditive = true;
		mouthLayer.Play(mouthIdleClip, 0.1f);
		mouthAIUEOLayer = _animancer.Layers[4];
		mouthAIUEOLayer.IsAdditive = true;
		mouthAIUEOLayer.Play(mouthAIUEOIdleClip, 0.1f);
		blinkLayer = _animancer.Layers[5];
		blinkLayer.IsAdditive = true;
		blinkLayer.Play(blinkClip[0], 0.25f);
		breathLayer = _animancer.Layers[6];
		breathLayer.IsAdditive = true;
		breathLayer.Play(breathClip[0], 0.25f);
	}

	private void LateUpdate()
	{
		currentBlinkWait -= Time.deltaTime;
		if (currentBlinkWait < 0f)
		{
			BlinkOn();
			currentBlinkWait = Random.Range(waitBlinkMin, waitBlinkMax);
		}
		if (useBreath)
		{
			currentBreathWait -= Time.deltaTime;
			if (currentBreathWait < 0f)
			{
				BreathOn();
				if (!hardBreath)
				{
					currentBreathWait = Random.Range(waitBreathMin, waitBreathMax);
				}
				else
				{
					currentBreathWait = Random.Range(waitHardBreathMin, waitHardBreathMax);
				}
			}
		}
		if (!isAIUEO && mouthAIUEOList.Count > 0)
		{
			StartAIUEO();
		}
		else if (isAIUEO && mouthAIUEOLayer.CurrentState?.Clip == mouthAIUEOIdleClip)
		{
			Debug.LogError("Face Anim Clip Aiueo Idle and Clip Remains");
			isAIUEO = false;
		}
		if (faicalWait > 0f)
		{
			faicalWait -= Time.deltaTime;
			if (faicalWait < 0f)
			{
				faicalWait = 0f;
				SetFacial("Idle");
			}
		}
		for (int i = 0; i < headSkinnedMesh.blendShapeCount; i++)
		{
			if (headMesh.GetBlendShapeWeight(i) > 100f)
			{
				headMesh.SetBlendShapeWeight(i, 100f);
			}
			else if (headMesh.GetBlendShapeWeight(i) < -100f)
			{
				headMesh.SetBlendShapeWeight(i, -100f);
			}
		}
		for (int j = 0; j < eyesSkinnedMesh.blendShapeCount; j++)
		{
			if (eyesMesh.GetBlendShapeWeight(j) > 100f)
			{
				eyesMesh.SetBlendShapeWeight(j, 100f);
			}
			else if (eyesMesh.GetBlendShapeWeight(j) < -100f)
			{
				eyesMesh.SetBlendShapeWeight(j, -100f);
			}
		}
	}

	public void CountEmote(string value)
	{
		if (emoteCount <= 0)
		{
			emoteCount = Random.Range(emoteCountMin, emoteCountMax);
			SetEmote(value);
		}
	}

	public void ResetEmote()
	{
		EndMouth();
	}

	public void SetEmote(string value)
	{
		Debug.Log("Set Emote: " + value, base.gameObject);
		if (!mouthForceEnd)
		{
			switch (value)
			{
			case "":
				eyesChoiceList.Clear();
				if (!unCloseEyes)
				{
					eyesChoiceList = new List<int>
					{
						0, 0, 0, 1, 1, 2, 2, 3, 3, 4,
						4
					};
				}
				else
				{
					eyesChoiceList = new List<int> { 0, 0, 0, 1, 1, 2, 2, 3, 3 };
				}
				EyesChange();
				mouthLayer.Play(mouthIdleClip, 0.1f);
				break;
			case "Close":
				eyesChoiceList.Clear();
				eyesChoiceList = new List<int>
				{
					4, 4, 4, 4, 4, 4, 4, 4, 4, 3,
					2
				};
				EyesChange();
				mouthLayer.Play(mouthIdleClip, 0.1f);
				break;
			case "UnClose":
				eyesChoiceList.Clear();
				eyesChoiceList = new List<int> { 0, 0, 0, 1, 1, 1, 2, 2, 3 };
				EyesChange();
				mouthLayer.Play(mouthIdleClip, 0.1f);
				break;
			case "Open":
				eyesChoiceList = new List<int>
				{
					0, 1, 1, 2, 2, 3, 3, 4, 4, 4,
					4
				};
				mouthMotionLayer.Play(mouthOpenClip, 0.1f);
				break;
			case "FullOpen":
				eyesChoiceList = new List<int>
				{
					0, 1, 1, 2, 2, 3, 3, 4, 4, 4,
					4
				};
				mouthMotionLayer.Play(mouthFullOpenClip, 0.1f);
				break;
			case "TongueDripOut":
				eyesChoiceList = new List<int>
				{
					0, 1, 1, 2, 2, 3, 3, 4, 4, 4,
					4
				};
				mouthMotionLayer.Play(mouthTongueDripOutClip, 0.1f);
				break;
			case "Tooth":
				eyesChoiceList = new List<int>
				{
					0, 1, 1, 2, 2, 3, 3, 2, 3, 4,
					4
				};
				mouthMotionLayer.Play(mouthToothClip, 0.1f);
				break;
			case "Threat":
				eyesChoiceList = new List<int>
				{
					0, 0, 0, 1, 1, 2, 2, 3, 3, 4,
					4
				};
				mouthMotionLayer.Play(mouthThreatClip, 0.1f);
				break;
			default:
				Debug.LogWarning("FacialSet miss! :" + value, base.gameObject);
				Debug.LogError("FacialSet miss! :" + value, base.gameObject);
				break;
			}
		}
		else if (value == "DeepKiss")
		{
			eyesChoiceList.Clear();
			eyesChoiceList = new List<int>
			{
				4, 4, 4, 4, 4, 4, 4, 4, 4, 3,
				2
			};
			EyesChange();
		}
	}

	public void EyesChange()
	{
		eyesChoice = eyesChoiceList[Random.Range(0, eyesChoiceList.Count)];
		eyesOpenLayer.Play(eyesOpenClip[eyesChoice], 0.25f);
		if (!useMouthAnim)
		{
			if (Random.Range(0, 10) < 3)
			{
				SetFacial("Light Facial");
			}
			Random.Range(0, 10);
		}
	}

	public void LookChangeFace()
	{
		if (!useMouthAnim)
		{
			Debug.LogWarning("Locking Face Change On", base.gameObject);
			SetFacial("Single");
		}
	}

	public void UseLook(bool head, bool eyes)
	{
		unLookHead = head;
		unLookEyes = eyes;
		_characterLookAt.useLookHead = !unLookHead;
		_characterLookAt.useLookEyes = !unLookEyes;
	}

	public void BlinkOn()
	{
		isBlink = true;
		eyesSpeed = Random.Range(0.1f, 0.2f);
		blinkLayer.Play(blinkClip[1], eyesSpeed);
	}

	public void BlinkOff()
	{
		isBlink = false;
		blinkLayer.Play(blinkClip[0], 0.25f);
	}

	public void BreathOn()
	{
		isBreath = true;
		mouseSpeed = Random.Range(0.1f, 0.2f);
		if (!stopBreath)
		{
			breathLayer.Play(breathClip[1], mouseSpeed);
			_characterHead.Breath(hardBreath);
			if (!hardBreath)
			{
				playClip = voiceBreathSoft[Random.Range(0, voiceBreathSoft.Count)];
				audioVoice.PlayOneShot(playClip);
			}
			else if (hardBreath)
			{
				playClip = voiceBreathHard[Random.Range(0, voiceBreathHard.Count)];
				audioVoice.PlayOneShot(playClip);
			}
		}
	}

	public void BreathOff()
	{
		breathLayer.Play(breathClip[0], 0.25f);
		isBreath = false;
	}

	private void BreathStop()
	{
		if (useBreath)
		{
			useBreath = false;
			audioVoice.Stop();
			currentBreathWait = Random.Range(waitBreathMin, waitBreathMax);
		}
	}

	public void StartMouthAnim(string idle, string click, bool right)
	{
		rightClick = right;
		if (!right)
		{
			if (idle != "")
			{
				mouthForceEnd = true;
				useMouthAnim = true;
				_ = mouthMotionLayer.CurrentState;
			}
			else
			{
				useMouthAnim = false;
				EndMouth();
			}
		}
		else if (right)
		{
			if (click != "")
			{
				mouthForceEnd = true;
				useMouthAnim = true;
				_ = mouthMotionLayer.CurrentState;
				_ = idle != click;
			}
			else
			{
				useMouthAnim = false;
				EndMouth();
			}
		}
	}

	public void IdleMouthAnim(string value)
	{
		if (value == "DeepKiss")
		{
			mouthMotionLayer.Play(mouthDeepKissClip[1], 0.1f);
		}
		else if (value == "LickSide")
		{
			mouthMotionLayer.Play(mouthLickSideClip[1], 0.1f);
		}
	}

	public void EndMouth()
	{
		mouthForceEnd = false;
		mouthMotionLayer.Play(mouthIdleClip, 0.1f);
		SetEmote("");
	}

	public void SetAIUEO(int vlaue)
	{
		if (!active)
		{
			Debug.Log(base.transform.parent.name + " SetAIUEO", base.gameObject);
			for (int i = 0; i < vlaue; i++)
			{
				mouthAIUEOList.Add(Random.Range(0, mouthAIUEOClip.Count));
			}
		}
	}

	public void StartAIUEO()
	{
		if (!isAIUEO)
		{
			isAIUEO = true;
			aiueoSpeed = Random.Range(0.1f, 0.2f);
			mouthAIUEOLayer.Play(mouthAIUEOClip[mouthAIUEOList[0]], aiueoSpeed);
		}
	}

	public void EndAIUEO()
	{
		isAIUEO = false;
		if (mouthAIUEOList.Count > 0)
		{
			mouthAIUEOList.RemoveAt(0);
		}
		if (mouthAIUEOList.Count > 0)
		{
			StartAIUEO();
		}
		else
		{
			mouthAIUEOLayer.Play(mouthAIUEOIdleClip, aiueoSpeed);
		}
	}

	public void CameraSet(bool value)
	{
		active = value;
		mouthAIUEOList.Clear();
		mouthAIUEOLayer.Play(mouthAIUEOIdleClip, aiueoSpeed);
	}

	public void PlayHitSe()
	{
		Debug.Log("Hit Se:" + base.transform.parent.name, base.gameObject);
		BreathStop();
		if (!audioVoice.isPlaying)
		{
			playClip = voiceHit[Random.Range(0, voiceHit.Count)];
			audioVoice.PlayOneShot(playClip);
			currentBreathWait = Random.Range(waitBreathMin, waitBreathMax);
			SetAIUEO(1);
			_characterEffectManager.HeartStream();
		}
	}

	public void PlayOrgasmSe()
	{
		BreathStop();
		if (!audioVoice.isPlaying)
		{
			playClip = voiceOrgasm[Random.Range(0, voiceOrgasm.Count)];
			audioVoice.PlayOneShot(playClip);
			currentBreathWait = Random.Range(waitBreathMin, waitBreathMax);
			SetAIUEO(2);
			_characterEffectManager.HeartStream();
		}
	}

	public void PlayAheSe()
	{
		BreathStop();
		if (!audioVoice.isPlaying)
		{
			playClip = voiceOrgasm[Random.Range(0, voiceOrgasm.Count)];
			audioVoice.PlayOneShot(playClip);
			currentBreathWait = Random.Range(waitBreathMin, waitBreathMax);
		}
	}

	public void SetFacial(string value)
	{
		currentFacial = value;
		facialSpeed = Random.Range(0.2f, 0.5f);
		faicalWait = Random.Range(5f, 10f);
		if (currentFacial == "Random")
		{
			List<string> list = new List<string>
			{
				"", "", "", "", "", "", "", "", "", "",
				"", "Joy", "Angry", "Mock", "Sad"
			};
			currentFacial = list[Random.Range(0, list.Count)];
		}
		else if (currentFacial == "Random Upper")
		{
			List<string> list2 = new List<string>
			{
				"", "", "", "", "", "", "", "", "", "",
				"", "Joy", "Mock"
			};
			currentFacial = list2[Random.Range(0, list2.Count)];
		}
		if (currentFacial == "" || currentFacial == "Idle")
		{
			facialLayer.Play(facialClip[0], facialSpeed);
		}
		else if (currentFacial == "Joy")
		{
			facialLayer.Play(facialClip[1], facialSpeed);
		}
		else if (currentFacial == "Angry")
		{
			facialLayer.Play(facialClip[2], facialSpeed);
		}
		else if (currentFacial == "Mock")
		{
			facialLayer.Play(facialClip[3], facialSpeed);
		}
		else if (currentFacial == "Sad")
		{
			facialLayer.Play(facialClip[4], facialSpeed);
		}
		else if (currentFacial == "Light")
		{
			facialLayer.Play(facialLightClip[Random.Range(0, facialLightClip.Count)], facialSpeed);
			SetEmote(currentFacial);
		}
		else if (currentFacial == "Light Facial")
		{
			facialLayer.Play(facialLightClip[Random.Range(0, facialLightClip.Count)], facialSpeed);
		}
		else if (currentFacial == "Single")
		{
			facialLayer.Play(facialEmoteClip[Random.Range(0, facialEmoteClip.Count)], facialSpeed);
			SetEmote(currentFacial);
		}
		else if (currentFacial == "UnClose")
		{
			SetEmote(currentFacial);
		}
		else if (currentFacial == "UnCloseOn")
		{
			unCloseEyes = true;
		}
		else if (currentFacial == "UnCloseOff")
		{
			unCloseEyes = false;
		}
		else if (currentFacial == "MockSnarl")
		{
			facialLayer.Play(facialClip[3], facialSpeed);
			SetEmote(currentFacial);
		}
		else
		{
			Debug.LogWarning("Facial Spell Miss? :" + currentFacial, base.gameObject);
			Debug.LogError("Facial Spell Miss? :" + currentFacial, base.gameObject);
		}
	}

	public void SetBreath(string value)
	{
		currentFacial = value;
		facialSpeed = Random.Range(0.2f, 0.5f);
		faicalWait = Random.Range(5f, 10f);
		if (currentFacial == "" || currentFacial == "Normal")
		{
			hardBreath = false;
			stopBreath = false;
			BreathOn();
		}
		else if (currentFacial == "Hard")
		{
			hardBreath = true;
			stopBreath = false;
			BreathOn();
		}
		else if (currentFacial == "Stop")
		{
			stopBreath = true;
			BreathStop();
		}
		else
		{
			Debug.LogWarning("Breath Spell Miss?", base.gameObject);
			Debug.LogError("Breath Spell Miss?", base.gameObject);
		}
	}

	public void Tear()
	{
		_characterHead.Tear();
	}

	public void TearShort()
	{
		_characterHead.TearShort();
	}

	public void BiteSe()
	{
		_characterSoundManager.BiteSe();
	}
}
