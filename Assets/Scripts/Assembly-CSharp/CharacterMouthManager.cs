using System.Collections.Generic;
using Animancer;
using UnityEngine;

public class CharacterMouthManager : MonoBehaviour
{
	public Animator _animator;

	public AnimancerComponent _animancer;

	public CharacterHead _characterHead;

	public Transform mouthPosition;

	[Header("Skinned Mesh")]
	public SkinnedMeshRenderer headMesh;

	private Mesh headSkinnedMesh;

	[Header("Data")]
	public bool active;

	[Header("Status")]
	public bool isBreath;

	public bool isAIUEO;

	[Header("Facial Status")]
	public bool isTooth;

	public bool isMouthOpen01;

	public bool isMouthOpen02;

	public bool isMouthOpen03;

	[Header("Facial Calc")]
	public float tooth;

	public float toothCalc;

	public float toothMax = 30f;

	public int shapeKeytoothL = 18;

	public int shapeKeytoothR = 19;

	public float facialMouth;

	[Header("Parameter")]
	public bool stopBreath;

	public bool useBreath;

	public bool useMouthAnim;

	[Header("Layer Number")]
	public int MouthNumberMotion = 4;

	public int MouthNumber = 5;

	public int MouthNumberAiueo = 6;

	public int MouthNumberBreath = 7;

	[Header("Mouth Motion 4")]
	public AnimationClip mouthIdleClip;

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

	[Header("Mouth 6")]
	public bool mouthForceEnd;

	public string currentMouth;

	public float mouthSpeed = 2f;

	public float mouthNoizeSpeed = 0.1f;

	private Vector2 mouthSeed;

	public Vector2 mouthData;

	public Vector2 mouthTargetData;

	public Vector2 mouthNoiseData;

	public Vector2 mouthCalcData;

	public List<AnimationClip> mouthClip;

	private CartesianMixerState mouthMixer;

	public AnimancerLayer mouthLayer;

	[Header("Mouth Open")]
	[Range(0f, 100f)]
	public float mouthOpen;

	[Range(0f, 100f)]
	public float mouthWidth;

	public float mouthOpenCurrent;

	public float mouthWidthCurrent;

	public float mouthOpenCalc;

	public float mouthWidthCalc;

	public float mouthOpenSpeed = 200f;

	[Header("Mouth AIUEO 7")]
	public float aiueoSpeed;

	public List<int> mouthAIUEOList;

	public AnimationClip mouthAIUEOIdleClip;

	public List<AnimationClip> mouthAIUEOClip;

	public AnimancerLayer mouthAIUEOLayer;

	[Header("Breath 8")]
	public float currentBreathWait;

	public float waitBreathMin = 5f;

	public float waitBreathMax = 15f;

	public float waitHardBreathMin = 3f;

	public float waitHardBreathMax = 6f;

	public float waitSqueezeBreathMin = 1.5f;

	public float waitSqueezeBreathMax = 4f;

	public float mouseSpeed;

	public List<AnimationClip> breathClip;

	public AnimancerLayer breathLayer;

	[Header("Voice")]
	public AudioSource audioVoice;

	private AudioClip playClip;

	public List<AudioClip> voiceHit = new List<AudioClip>();

	public List<AudioClip> voiceOrgasm = new List<AudioClip>();

	[Header("Fellatio")]
	public bool isMouthGag;

	public bool isGimmickGag;

	public List<AudioClip> voiceFellatio = new List<AudioClip>();

	public List<AudioClip> voiceFellatioOrgasm = new List<AudioClip>();

	[Header("Breath")]
	public bool isHardBreath;

	public bool isSqueeze;

	public List<AudioClip> voiceBreathSoft = new List<AudioClip>();

	public List<AudioClip> voiceBreathHard = new List<AudioClip>();

	private void Awake()
	{
		headSkinnedMesh = headMesh.sharedMesh;
		mouthAIUEOList.Clear();
	}

	private void Start()
	{
		currentBreathWait = Random.Range(waitBreathMin, waitBreathMax);
	}

	public void InitalizeMouth()
	{
		mouthMotionLayer = _animancer.Layers[MouthNumberMotion];
		mouthMotionLayer.IsAdditive = true;
		mouthMotionLayer.Play(mouthIdleClip, 0.1f);
		mouthLayer = _animancer.Layers[MouthNumber];
		mouthLayer.IsAdditive = true;
		mouthSeed = new Vector2(Random.Range(0f, 100f), Random.Range(0f, 100f));
		MakeMouthMixer();
		mouthLayer.Play(mouthMixer, 0f);
		mouthAIUEOLayer = _animancer.Layers[MouthNumberAiueo];
		mouthAIUEOLayer.IsAdditive = true;
		mouthAIUEOLayer.Play(mouthAIUEOIdleClip, 0.1f);
		breathLayer = _animancer.Layers[MouthNumberBreath];
		breathLayer.IsAdditive = true;
		breathLayer.Play(breathClip[0], 0.25f);
	}

	public void MakeMouthMixer()
	{
		mouthMixer = new CartesianMixerState();
		mouthMixer.Add(mouthClip[0], new Vector2(0f, 0f));
		mouthMixer.Add(mouthClip[1], new Vector2(-0.5f, 0f));
		mouthMixer.Add(mouthClip[2], new Vector2(0.5f, 0f));
		mouthMixer.Add(mouthClip[3], new Vector2(-0.5f, -0.25f));
		mouthMixer.Add(mouthClip[4], new Vector2(0f, -0.25f));
		mouthMixer.Add(mouthClip[5], new Vector2(0.5f, -0.25f));
		mouthMixer.Add(mouthClip[6], new Vector2(-0.5f, 0.25f));
		mouthMixer.Add(mouthClip[7], new Vector2(0f, 0.25f));
		mouthMixer.Add(mouthClip[8], new Vector2(0.5f, 0.25f));
	}

	private void LateUpdate()
	{
		if (useBreath)
		{
			currentBreathWait -= Time.deltaTime;
			if (currentBreathWait < 0f)
			{
				BreathOn();
				if (isHardBreath)
				{
					currentBreathWait = Random.Range(waitHardBreathMin, waitHardBreathMax);
				}
				else if (isSqueeze)
				{
					currentBreathWait = Random.Range(waitSqueezeBreathMin, waitSqueezeBreathMax);
				}
				else
				{
					currentBreathWait = Random.Range(waitBreathMin, waitBreathMax);
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
		if (mouthTargetData == Vector2.zero)
		{
			SetMouth("Idle");
		}
		mouthTargetData = Vector2.Lerp(mouthTargetData, Vector2.zero, Time.deltaTime * mouthSpeed / 2f);
		if (Mathf.Abs(mouthTargetData.x) < 0.01f)
		{
			mouthTargetData.x = 0f;
		}
		if (Mathf.Abs(mouthTargetData.y) < 0.01f)
		{
			mouthTargetData.y = 0f;
		}
		mouthData = Vector2.Lerp(mouthData, mouthTargetData, Time.deltaTime * mouthSpeed);
		mouthNoiseData.x = Mathf.PerlinNoise(mouthSeed.x, Time.time * mouthNoizeSpeed) * 2f - 1f;
		mouthNoiseData.y = Mathf.PerlinNoise(mouthSeed.y, Time.time * mouthNoizeSpeed) * 2f - 1f;
		mouthCalcData = mouthData + mouthNoiseData;
		if (mouthCalcData.x > 1f)
		{
			mouthCalcData.x = 1f;
		}
		else if (mouthCalcData.x < -1f)
		{
			mouthCalcData.x = -1f;
		}
		if (mouthCalcData.y > 1f)
		{
			mouthCalcData.y = 1f;
		}
		else if (mouthCalcData.y < -1f)
		{
			mouthCalcData.y = -1f;
		}
		mouthMixer.Parameter = mouthCalcData;
		if (facialMouth > 0f)
		{
			facialMouth -= Time.deltaTime * 10f;
			if (facialMouth < 0f)
			{
				facialMouth = 0f;
			}
		}
		if (isMouthOpen01 && facialMouth < 10f)
		{
			isMouthOpen01 = false;
			if (facialMouth == 0f)
			{
				facialMouth += 10f;
			}
			else if (facialMouth < 20f)
			{
				facialMouth += 10f;
			}
		}
		else if (isMouthOpen02 && facialMouth < 20f)
		{
			isMouthOpen02 = false;
			if (facialMouth == 0f)
			{
				facialMouth += 20f;
			}
			else if (facialMouth < 10f)
			{
				facialMouth += 10f;
			}
			else if (facialMouth < 20f)
			{
				facialMouth += 5f;
			}
		}
		else if (isMouthOpen03 && facialMouth < 30f)
		{
			isMouthOpen03 = false;
			if (facialMouth == 0f)
			{
				facialMouth += 30f;
			}
			else if (facialMouth < 10f)
			{
				facialMouth += 20f;
			}
			else if (facialMouth < 20f)
			{
				facialMouth += 10f;
			}
		}
		if (isSqueeze && facialMouth < 30f)
		{
			facialMouth += 0.5f;
		}
		if (mouthOpenCurrent < mouthOpen + facialMouth)
		{
			mouthOpenCurrent += Time.deltaTime * mouthOpenSpeed * 2f;
			if (mouthOpenCurrent > mouthOpen + facialMouth)
			{
				mouthOpenCurrent = mouthOpen + facialMouth;
			}
		}
		else if (mouthOpenCurrent > mouthOpen + facialMouth)
		{
			mouthOpenCurrent -= Time.deltaTime * mouthOpenSpeed * 2f;
			if (mouthOpenCurrent < mouthOpen + facialMouth)
			{
				mouthOpenCurrent = mouthOpen + facialMouth;
			}
		}
		if (mouthWidthCurrent < mouthWidth)
		{
			mouthWidthCurrent += Time.deltaTime * mouthOpenSpeed;
			if (mouthWidthCurrent > mouthWidth)
			{
				mouthWidthCurrent = mouthWidth;
			}
		}
		else if (mouthWidthCurrent > mouthWidth)
		{
			mouthWidthCurrent -= Time.deltaTime * mouthOpenSpeed;
			if (mouthWidthCurrent < mouthWidth)
			{
				mouthWidthCurrent = mouthWidth;
			}
		}
		mouthOpenCalc = headMesh.GetBlendShapeWeight(14) + mouthOpenCurrent;
		mouthWidthCalc = headMesh.GetBlendShapeWeight(15) + mouthWidthCurrent;
		mouthOpenCalc = Mathf.Clamp(mouthOpenCalc, 0f, 100f);
		mouthWidthCalc = Mathf.Clamp(mouthOpenCalc, 0f, 100f);
		headMesh.SetBlendShapeWeight(14, mouthOpenCalc);
		headMesh.SetBlendShapeWeight(15, mouthWidthCalc);
		if (isTooth)
		{
			tooth += Time.deltaTime * 2f;
			if (tooth > 1f)
			{
				tooth = 1f;
			}
			toothCalc = headMesh.GetBlendShapeWeight(shapeKeytoothL);
			toothCalc += tooth * toothMax - mouthOpenCalc;
			if (toothCalc > toothMax)
			{
				toothCalc = toothMax;
			}
			else if (toothCalc < 0f)
			{
				toothCalc = 0f;
			}
			headMesh.SetBlendShapeWeight(shapeKeytoothL, toothCalc);
			toothCalc = headMesh.GetBlendShapeWeight(shapeKeytoothR);
			toothCalc += tooth * toothMax - mouthOpenCalc;
			if (toothCalc > toothMax)
			{
				toothCalc = toothMax;
			}
			else if (toothCalc < 0f)
			{
				toothCalc = 0f;
			}
			headMesh.SetBlendShapeWeight(shapeKeytoothR, toothCalc);
		}
		else if (!isTooth)
		{
			tooth -= Time.deltaTime * 2f;
			if (tooth < 0f)
			{
				tooth = 0f;
			}
			toothCalc = headMesh.GetBlendShapeWeight(shapeKeytoothL);
			toothCalc += tooth * toothMax - mouthOpenCalc;
			if (toothCalc > toothMax)
			{
				toothCalc = toothMax;
			}
			else if (toothCalc < 0f)
			{
				toothCalc = 0f;
			}
			headMesh.SetBlendShapeWeight(shapeKeytoothL, toothCalc);
			toothCalc = headMesh.GetBlendShapeWeight(shapeKeytoothR);
			toothCalc += tooth * toothMax - mouthOpenCalc;
			if (toothCalc > toothMax)
			{
				toothCalc = toothMax;
			}
			else if (toothCalc < 0f)
			{
				toothCalc = 0f;
			}
			headMesh.SetBlendShapeWeight(shapeKeytoothR, toothCalc);
		}
	}

	public void SetMouth(string value)
	{
		mouthTargetData = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 1f));
		if (mouthTargetData.x > 0f)
		{
			mouthTargetData.x += 0.5f;
		}
		else if (mouthTargetData.x < 0f)
		{
			mouthTargetData.x -= 0.5f;
		}
		if (mouthTargetData.y > 0f)
		{
			mouthTargetData.y += 0.25f;
		}
		else if (mouthTargetData.y < 0f)
		{
			mouthTargetData.y -= 0.5f;
		}
	}

	public void BreathOn()
	{
		isBreath = true;
		mouseSpeed = Random.Range(0.1f, 0.2f);
		if (stopBreath)
		{
			return;
		}
		breathLayer.Play(breathClip[1], mouseSpeed);
		if (isHardBreath)
		{
			_characterHead.Breath(hardBreath: true);
		}
		else if (isSqueeze)
		{
			_characterHead.Breath(hardBreath: true);
		}
		else
		{
			_characterHead.Breath(hardBreath: false);
		}
		if (!isMouthGag && !isGimmickGag)
		{
			if (!isHardBreath && !isSqueeze)
			{
				playClip = voiceBreathSoft[Random.Range(0, voiceBreathSoft.Count)];
				audioVoice.PlayOneShot(playClip);
			}
			else if (isHardBreath || isSqueeze)
			{
				playClip = voiceBreathHard[Random.Range(0, voiceBreathHard.Count)];
				audioVoice.PlayOneShot(playClip);
			}
		}
		else
		{
			playClip = voiceFellatio[Random.Range(0, voiceFellatio.Count)];
			audioVoice.PlayOneShot(playClip);
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
			audioVoice.Stop();
			currentBreathWait = Random.Range(waitBreathMin, waitBreathMax);
		}
	}

	public void SetAIUEO(int vlaue)
	{
		if (!active)
		{
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
		if (!audioVoice.isPlaying)
		{
			if (!isMouthGag && !isGimmickGag)
			{
				playClip = voiceHit[Random.Range(0, voiceHit.Count)];
				audioVoice.PlayOneShot(playClip);
			}
			else
			{
				playClip = voiceFellatio[Random.Range(0, voiceFellatio.Count)];
				audioVoice.PlayOneShot(playClip);
			}
			currentBreathWait = Random.Range(waitBreathMin, waitBreathMax);
			SetAIUEO(1);
		}
	}

	public void PlayOrgasmSe()
	{
		BreathStop();
		if (!audioVoice.isPlaying)
		{
			if (!isMouthGag && !isGimmickGag)
			{
				playClip = voiceOrgasm[Random.Range(0, voiceOrgasm.Count)];
				audioVoice.PlayOneShot(playClip);
			}
			else
			{
				playClip = voiceFellatioOrgasm[Random.Range(0, voiceFellatioOrgasm.Count)];
				audioVoice.PlayOneShot(playClip);
			}
			currentBreathWait = Random.Range(waitBreathMin, waitBreathMax);
			SetAIUEO(2);
		}
	}

	public void PlayBirthSe()
	{
		if (!audioVoice.isPlaying && mouthAIUEOList.Count == 0)
		{
			if (!isMouthGag && !isGimmickGag)
			{
				playClip = voiceOrgasm[Random.Range(0, voiceOrgasm.Count)];
				audioVoice.PlayOneShot(playClip);
			}
			else
			{
				playClip = voiceFellatioOrgasm[Random.Range(0, voiceFellatioOrgasm.Count)];
				audioVoice.PlayOneShot(playClip);
			}
			currentBreathWait = Random.Range(waitBreathMin, waitBreathMax);
			SetAIUEO(2);
		}
	}
}
