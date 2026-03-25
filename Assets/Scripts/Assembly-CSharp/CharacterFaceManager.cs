using System.Collections.Generic;
using Animancer;
using UnityEngine;

public class CharacterFaceManager : MonoBehaviour
{
	public Animator _animator;

	public AnimancerComponent _animancer;

	public CharacterLookAt _characterLookAt;

	public CharacterSoundManager _characterSoundManager;

	public CharacterEffectManager _characterEffectManager;

	public CharacterEyesManager _characterEyesManager;

	public CharacterMouthManager _characterMouthManager;

	public CharacterTongueManager _characterTongueManager;

	[Header("Skinned Mesh")]
	public SkinnedMeshRenderer headMesh;

	private Mesh headSkinnedMesh;

	public Material headMaterial;

	[Header("Data")]
	public bool unCloseEyes;

	[Header("Parameter")]
	public bool rightClick;

	public bool stopBreath;

	public bool useBreath;

	public bool unLookHead;

	public bool unLookEyes;

	public bool useLook;

	public bool useMouthAnim;

	[Header("Layer Number")]
	public int FacialNumber;

	[Header("Blush Paramater")]
	public float currentBlush;

	public float targetBlush;

	public float blushUpSpeed = 0.05f;

	public float blushDownSpeed = 0.01f;

	public Color blushCalcColor = new Color(1f, 1f, 1f, 0f);

	[Header("Facial 0-1")]
	public string currentFacial;

	public float facialSpeed = 5f;

	public float facialTargetSpeed = 0.1f;

	public float facialNoizeSpeed = 0.1f;

	private Vector2 facialSeed;

	public Vector2 facialData;

	public Vector2 facialDataEmote;

	public Vector2 facialTargetData;

	public Vector2 facialTargetDataEmote;

	public Vector2 facialNoiseData;

	public Vector2 facialCalcData;

	[Header("Facial Anim")]
	public List<AnimationClip> facialClipBase;

	public List<AnimationClip> facialClipJoy;

	public List<AnimationClip> facialClipAngry;

	public List<AnimationClip> facialClipMock;

	public List<AnimationClip> facialClipSad;

	private CartesianMixerState facialMixer;

	public AnimancerLayer facialLayer;

	public float feedTime = 0.25f;

	private AnimancerState _state;

	private void Awake()
	{
		headSkinnedMesh = headMesh.sharedMesh;
	}

	private void Start()
	{
		headMaterial = headMesh.sharedMaterials[0];
		blushCalcColor.a = currentBlush;
		headMaterial.SetColor("_Color2nd", blushCalcColor);
		facialLayer = _animancer.Layers[FacialNumber];
		facialLayer.IsAdditive = false;
		facialSeed = new Vector2(Random.Range(0f, 100f), Random.Range(0f, 100f));
		MakeFacialMixer();
		facialLayer.Play(facialMixer, 0f);
		_characterEyesManager.InitalizeEyes();
		_characterMouthManager.InitalizeMouth();
		_characterTongueManager.InitalizeMouth();
	}

	public void MakeFacialMixer()
	{
		facialMixer = new CartesianMixerState();
		facialMixer.Add(facialClipBase[0], new Vector2(0f, 0f));
		facialMixer.Add(facialClipBase[1], new Vector2(0f, 1f));
		facialMixer.Add(facialClipBase[2], new Vector2(-1f, 0f));
		facialMixer.Add(facialClipBase[3], new Vector2(1f, 0f));
		facialMixer.Add(facialClipBase[4], new Vector2(0f, -1f));
		facialMixer.Add(facialClipBase[5], new Vector2(0.5f, 0.85f));
		facialMixer.Add(facialClipBase[6], new Vector2(-0.5f, 0.85f));
		facialMixer.Add(facialClipBase[7], new Vector2(-0.85f, 0.5f));
		facialMixer.Add(facialClipBase[8], new Vector2(-0.85f, -0.5f));
		facialMixer.Add(facialClipBase[9], new Vector2(0.85f, 0.5f));
		facialMixer.Add(facialClipBase[10], new Vector2(0.85f, -0.5f));
		facialMixer.Add(facialClipBase[11], new Vector2(0.5f, -0.85f));
		facialMixer.Add(facialClipBase[12], new Vector2(-0.5f, -0.85f));
	}

	private void LateUpdate()
	{
		if (targetBlush > 1.2f)
		{
			targetBlush = 1.2f;
		}
		if (currentBlush < targetBlush)
		{
			currentBlush += Time.deltaTime * blushUpSpeed;
			if (currentBlush > 1f)
			{
				currentBlush = 1f;
			}
			else if (currentBlush > targetBlush)
			{
				currentBlush = targetBlush;
			}
			blushCalcColor.a = currentBlush;
			headMaterial.SetColor("_Color2nd", blushCalcColor);
		}
		else if (currentBlush > targetBlush)
		{
			currentBlush -= Time.deltaTime * blushDownSpeed;
			if (currentBlush < targetBlush)
			{
				currentBlush = targetBlush;
			}
			blushCalcColor.a = currentBlush;
			headMaterial.SetColor("_Color2nd", blushCalcColor);
		}
		if (targetBlush > 0f)
		{
			targetBlush -= Time.deltaTime * blushDownSpeed;
			if (targetBlush < 0f)
			{
				targetBlush = 0f;
			}
		}
		if (facialTargetData == Vector2.zero)
		{
			SetFacial("Idle");
		}
		facialTargetData = Vector2.Lerp(facialTargetData, Vector2.zero, Time.deltaTime * facialTargetSpeed);
		facialTargetDataEmote = Vector2.Lerp(facialTargetDataEmote, Vector2.zero, Time.deltaTime * facialTargetSpeed);
		if (Mathf.Abs(facialTargetData.x) < 0.01f)
		{
			facialTargetData.x = 0f;
		}
		if (Mathf.Abs(facialTargetData.y) < 0.01f)
		{
			facialTargetData.y = 0f;
		}
		facialData = Vector2.Lerp(facialData, facialTargetData, Time.deltaTime * facialSpeed);
		facialDataEmote = Vector2.Lerp(facialDataEmote, facialTargetDataEmote, Time.deltaTime * facialSpeed);
		facialNoiseData.x = Mathf.PerlinNoise(facialSeed.x, Time.time * facialNoizeSpeed) * 2f - 1f;
		facialNoiseData.y = Mathf.PerlinNoise(facialSeed.y, Time.time * facialNoizeSpeed) * 2f - 1f;
		facialNoiseData *= 0.1f;
		facialCalcData = facialData + facialNoiseData;
		facialCalcData.x = Mathf.Clamp(facialCalcData.x, -1f, 1f);
		facialCalcData.y = Mathf.Clamp(facialCalcData.y, -1f, 1f);
		facialMixer.Parameter = facialCalcData;
		for (int i = 0; i < headSkinnedMesh.blendShapeCount; i++)
		{
			if (headMesh.GetBlendShapeWeight(i) > 100f)
			{
				headMesh.SetBlendShapeWeight(i, 100f);
			}
			else if ((i == 18 || i == 19) && headMesh.GetBlendShapeWeight(i) < 0f)
			{
				headMesh.SetBlendShapeWeight(i, 0f);
			}
			else if (headMesh.GetBlendShapeWeight(i) < -100f)
			{
				headMesh.SetBlendShapeWeight(i, -100f);
			}
		}
	}

	public void SetEmote(string value)
	{
		Debug.Log("Set Emote: " + value, base.gameObject);
		Debug.LogError("Set Emote is not use?", base.gameObject);
	}

	public void EyesChange()
	{
		_characterEyesManager.ReactionEyes(0f, 0.25f);
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

	public void BlinkOff()
	{
		_characterEyesManager.BlinkOff();
	}

	public void SetFacial(string value)
	{
		currentFacial = value;
		if (currentFacial == "Random")
		{
			facialTargetData = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
			if (facialTargetData.x > 0f)
			{
				facialTargetData.x += 0.5f;
			}
			else if (facialTargetData.x < 0f)
			{
				facialTargetData.x -= 0.5f;
			}
			if (facialTargetData.y > 0f)
			{
				facialTargetData.y += 0.5f;
			}
			else if (facialTargetData.y < 0f)
			{
				facialTargetData.y -= 0.5f;
			}
			facialTargetDataEmote = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
			if (facialTargetDataEmote.x > 0f)
			{
				facialTargetDataEmote.x += 0.5f;
			}
			else if (facialTargetDataEmote.x < 0f)
			{
				facialTargetDataEmote.x -= 0.5f;
			}
			if (facialTargetDataEmote.y > 0f)
			{
				facialTargetDataEmote.y += 0.5f;
			}
			else if (facialTargetDataEmote.y < 0f)
			{
				facialTargetDataEmote.y -= 0.5f;
			}
		}
		if (currentFacial == "" || currentFacial == "Idle")
		{
			facialTargetData = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
		}
		else if (currentFacial == "Joy")
		{
			facialTargetData = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(0.75f, 1f));
			_state = _animancer.Play(facialClipJoy[Random.Range(0, facialClipJoy.Count)], feedTime);
			_state.Speed = 1f;
			_state.Events(this).OnEnd = delegate
			{
				_animancer.Play(facialMixer, feedTime);
			};
		}
		else if (currentFacial == "Angry")
		{
			facialTargetData = new Vector2(Random.Range(-1f, -0.75f), Random.Range(-0.5f, 0.5f));
			_state = _animancer.Play(facialClipAngry[Random.Range(0, facialClipAngry.Count)], feedTime);
			_state.Speed = 1f;
			_state.Events(this).OnEnd = delegate
			{
				_animancer.Play(facialMixer, feedTime);
			};
		}
		else if (currentFacial == "Mock")
		{
			facialTargetData = new Vector2(Random.Range(0.75f, 1f), Random.Range(-0.5f, 0.5f));
			_state = _animancer.Play(facialClipMock[Random.Range(0, facialClipMock.Count)], feedTime);
			_state.Speed = 1f;
			_state.Events(this).OnEnd = delegate
			{
				_animancer.Play(facialMixer, feedTime);
			};
		}
		else if (currentFacial == "Sad")
		{
			facialTargetData = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-1f, -0.75f));
			_state = _animancer.Play(facialClipSad[Random.Range(0, facialClipSad.Count)], feedTime);
			_state.Speed = 1f;
			_state.Events(this).OnEnd = delegate
			{
				_animancer.Play(facialMixer, feedTime);
			};
		}
		else if (currentFacial == "UnCloseOn")
		{
			unCloseEyes = true;
		}
		else if (currentFacial == "UnCloseOff")
		{
			unCloseEyes = false;
		}
		else
		{
			Debug.LogWarning("Facial Spell Miss? :" + currentFacial, base.gameObject);
			Debug.LogError("Facial Spell Miss? :" + currentFacial, base.gameObject);
		}
		facialTargetData.x = Mathf.Clamp(facialTargetData.x, -1f, 1f);
		facialTargetData.y = Mathf.Clamp(facialTargetData.y, -1f, 1f);
		facialTargetDataEmote.x = Mathf.Clamp(facialTargetDataEmote.x, -1f, 1f);
		facialTargetDataEmote.y = Mathf.Clamp(facialTargetDataEmote.y, -1f, 1f);
	}

	public void BiteSe()
	{
		_characterSoundManager.BiteSe();
	}
}
