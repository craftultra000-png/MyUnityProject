using System.Collections.Generic;
using System.Linq;
using Animancer;
using UnityEngine;

public class MotionAnimancer : MonoBehaviour
{
	public AnimancerComponent _animancer;

	public ReactionAnimancer _reactionAnimancer;

	public PoseManager _poseManager;

	public PoseDataGUI _poseDataGUI;

	public CharacterLookAt _characterLookAt;

	public UterusController _uterusController;

	[Header("Link")]
	public MotionFuckAnimancer _motionFuckAnimancer;

	[Header("Status")]
	public string StateName = "isIdle";

	public string PreviousStateName = "isIdle";

	public string StockStateName = "isIdle";

	public string FuckName = "AnyFuck";

	public bool isEndWait;

	[Header("Status")]
	[Range(-1f, 1f)]
	public float paramX;

	[Range(-1f, 1f)]
	public float paramY;

	public Vector2 poseParam;

	[Header("Status")]
	public bool isVaginaPiston;

	public bool isAnalPiston;

	public bool isFuck;

	public bool isFuckEnd;

	public bool isInsert;

	public bool isVaginaInsert;

	public bool isPiston;

	public bool isInsertLoop;

	[Header("Anim Speed")]
	public float animationSpeed;

	[Header("Anim FeedTime")]
	public float feedTime = 0.5f;

	public float pistonMixParameter;

	[Header("Layer")]
	public AnimancerLayer animancerLayer1;

	[Header("Animation Mixer")]
	public int mixerCount;

	public List<Vector2> mixParam;

	public List<Vector2> mixParamTmp;

	public List<Vector2> mixParamTmp2;

	private DirectionalMixerState poseMixer;

	private DirectionalMixerState poseMixer2;

	private DirectionalMixerState poseMixer3;

	private DirectionalMixerState poseMixer4;

	[Space]
	private DirectionalMixerState pistonIdleMixer;

	private DirectionalMixerState pistonMixer;

	[Header("Animation Clip")]
	public int poseType;

	public int normalPose = 8;

	public AnimationClip tPose;

	public List<AnimationClip> idleClip;

	public List<AnimationClip> insertClip;

	public List<AnimationClip> pistonClip;

	public List<AnimationClip> pistonHighClip;

	public List<AnimationClip> shotClip;

	public List<AnimationClip> shotIdleClip;

	[Header("Expend")]
	public List<AnimationClip> eatClip;

	public List<AnimationClip> rideClip;

	public List<AnimationClip> limbHoldClip;

	public List<AnimationClip> wartBedClip;

	public List<AnimationClip> wallHipClip;

	public List<AnimationClip> pillarBindClip;

	[Space]
	public List<AnimationClip> backFuckClip;

	public List<AnimationClip> frontFuckClip;

	public List<AnimationClip> rideFuckClip;

	public List<AnimationClip> liftFuckClip;

	public List<AnimationClip> sideFuckClip;

	public List<AnimationClip> doggyFuckClip;

	private List<LinearMixerState> pistonSpeedMixer = new List<LinearMixerState>();

	private AnimancerState _state;

	private AnimancerState _stateOld;

	private AnimancerState _stateOld2;

	private AnimancerState _stateOld3;

	[ContextMenu("Set Sort AnimationList")]
	public void SortAnimationList()
	{
		idleClip = SortClipsByPrefix(idleClip);
		insertClip = SortClipsByPrefix(insertClip);
		pistonClip = SortClipsByPrefix(pistonClip);
		pistonHighClip = SortClipsByPrefix(pistonHighClip);
		shotClip = SortClipsByPrefix(shotClip);
		shotIdleClip = SortClipsByPrefix(shotIdleClip);
		Debug.LogError("Sort Animation Clip List", base.gameObject);
	}

	private List<AnimationClip> SortClipsByPrefix(List<AnimationClip> list)
	{
		return (from clip in list
			orderby ExtractPrefixNumber(clip.name), clip.name
			select clip).ToList();
	}

	private int ExtractPrefixNumber(string name)
	{
		int num = name.IndexOf('_');
		if (num > 0 && int.TryParse(name.Substring(0, num), out var result))
		{
			return result;
		}
		return int.MaxValue;
	}

	private void Start()
	{
		_animancer.Graph.ApplyFootIK = true;
		_animancer.Layers[0].ApplyFootIK = true;
		for (int i = 0; i < pistonClip.Count; i++)
		{
			LinearMixerState linearMixerState = new LinearMixerState();
			linearMixerState.Add(pistonClip[i], 0f);
			linearMixerState.Add(pistonHighClip[i], 1f);
			pistonSpeedMixer.Add(linearMixerState);
			pistonSpeedMixer[i].Parameter = pistonMixParameter;
		}
		_poseManager.mixParam = mixParam;
		_poseDataGUI.mixParam = mixParam;
		SetPistonMixer();
		if (idleClip[poseType] != null)
		{
			animancerLayer1 = _animancer.Layers[1];
			animancerLayer1.IsAdditive = true;
			animancerLayer1.Weight = 0f;
			animancerLayer1.ApplyFootIK = true;
		}
		StartMixer(idleClip);
		PreviousStateName = "isSkip";
		StateName = "isSkip";
		StateSet("isIdle", 0.25f);
	}

	public void StartMixer(List<AnimationClip> clips)
	{
		mixerCount = 0;
		poseMixer = new DirectionalMixerState();
		for (int i = 0; i < mixParam.Count; i++)
		{
			poseMixer.Add(clips[i], mixParam[i]);
		}
		poseMixer2 = new DirectionalMixerState();
		for (int j = 0; j < mixParam.Count; j++)
		{
			poseMixer2.Add(clips[j], mixParam[j]);
		}
		poseMixer3 = new DirectionalMixerState();
		for (int k = 0; k < mixParam.Count; k++)
		{
			poseMixer3.Add(clips[k], mixParam[k]);
		}
		poseMixer4 = new DirectionalMixerState();
		for (int l = 0; l < mixParam.Count; l++)
		{
			poseMixer4.Add(clips[l], mixParam[l]);
		}
	}

	public DirectionalMixerState SetMixer(List<AnimationClip> clips)
	{
		if (mixerCount == 0)
		{
			mixerCount = 1;
			if (poseMixer != null)
			{
				poseMixer.Destroy();
			}
			poseMixer = new DirectionalMixerState();
			for (int i = 0; i < mixParam.Count; i++)
			{
				poseMixer.Add(clips[i], mixParam[i]);
			}
			poseMixer.Parameter = poseParam;
			return poseMixer;
		}
		if (mixerCount == 1)
		{
			mixerCount = 2;
			if (poseMixer2 != null)
			{
				poseMixer2.Destroy();
			}
			poseMixer2 = new DirectionalMixerState();
			for (int j = 0; j < mixParam.Count; j++)
			{
				poseMixer2.Add(clips[j], mixParam[j]);
			}
			poseMixer2.Parameter = poseParam;
			return poseMixer2;
		}
		if (mixerCount == 2)
		{
			mixerCount = 3;
			if (poseMixer3 != null)
			{
				poseMixer3.Destroy();
			}
			poseMixer3 = new DirectionalMixerState();
			for (int k = 0; k < mixParam.Count; k++)
			{
				poseMixer3.Add(clips[k], mixParam[k]);
			}
			poseMixer3.Parameter = poseParam;
			return poseMixer3;
		}
		if (mixerCount == 3)
		{
			mixerCount = 0;
			if (poseMixer4 != null)
			{
				poseMixer4.Destroy();
			}
			poseMixer4 = new DirectionalMixerState();
			for (int l = 0; l < mixParam.Count; l++)
			{
				poseMixer4.Add(clips[l], mixParam[l]);
			}
			poseMixer4.Parameter = poseParam;
			return poseMixer4;
		}
		mixerCount = 0;
		if (poseMixer != null)
		{
			poseMixer.Destroy();
		}
		poseMixer = new DirectionalMixerState();
		for (int m = 0; m < mixParam.Count; m++)
		{
			poseMixer.Add(clips[m], mixParam[m]);
		}
		poseMixer.Parameter = poseParam;
		return poseMixer;
	}

	public void SetPistonMixer()
	{
		pistonMixer = new DirectionalMixerState();
		for (int i = 0; i < mixParam.Count; i++)
		{
			pistonMixer.Add(pistonHighClip[i], mixParam[i]);
		}
		pistonMixer.Parameter = poseParam;
		pistonIdleMixer = new DirectionalMixerState();
		for (int j = 0; j < mixParam.Count; j++)
		{
			pistonIdleMixer.Add(idleClip[j], mixParam[j]);
		}
		pistonIdleMixer.Parameter = poseParam;
	}

	private void LateUpdate()
	{
		poseParam.x = paramX;
		poseParam.y = paramY;
		if (_animancer.States.Current == poseMixer)
		{
			poseMixer.Parameter = poseParam;
		}
		if (_animancer.States.Current == poseMixer2)
		{
			poseMixer2.Parameter = poseParam;
		}
		if (_animancer.States.Current == poseMixer3)
		{
			poseMixer3.Parameter = poseParam;
		}
		if (_animancer.States.Current == poseMixer4)
		{
			poseMixer4.Parameter = poseParam;
		}
		_reactionAnimancer.poseParam = poseParam;
		_poseManager.poseParam = poseParam;
		if (StateName == "isPiston" && poseType < normalPose)
		{
			animancerLayer1.Weight = pistonMixParameter;
		}
		else
		{
			animancerLayer1.Weight = 0f;
		}
	}

	public void PoseChange(int value, bool expend, int mode)
	{
		Debug.LogError("PoseChange: " + value + ", " + expend + ", " + mode);
		if (_motionFuckAnimancer != null)
		{
			_motionFuckAnimancer.PoseChange(value, expend, mode);
		}
		if (!expend)
		{
			poseType = value;
			StateSet(StateName, 0.25f);
		}
		else if (expend && value == -20)
		{
			poseType = idleClip.Count - 12;
			if (mode == 0)
			{
				StateSet("isEatStart", 0.5f);
			}
			else
			{
				StateSet("isEatEnd", 0.25f);
			}
		}
		else if (expend && value == -21)
		{
			poseType = idleClip.Count - 11;
			if (mode == 0)
			{
				StateSet("isRideStart", 0.5f);
			}
			else
			{
				StateSet("isRideEnd", 0.25f);
			}
		}
		else if (expend && value == -22)
		{
			poseType = idleClip.Count - 10;
			if (mode == 0)
			{
				StateSet("isLimbHoldStart", 0.5f);
			}
			else
			{
				StateSet("isLimbHoldEnd", 0.25f);
			}
		}
		else if (expend && value == -23)
		{
			poseType = idleClip.Count - 9;
			if (mode == 0)
			{
				StateSet("isWartBedStart", 0.5f);
			}
			else
			{
				StateSet("isWartBedEnd", 0.25f);
			}
		}
		else if (expend && value == -24)
		{
			poseType = idleClip.Count - 8;
			if (mode == 0)
			{
				StateSet("isWallHipStart", 0.5f);
			}
			else
			{
				StateSet("isWallHipEnd", 0.25f);
			}
		}
		else if (expend && value == -25)
		{
			poseType = idleClip.Count - 7;
			if (mode == 0)
			{
				StateSet("isPillarBindStart", 0.5f);
			}
			else
			{
				StateSet("isPillarBindEnd", 0.25f);
			}
		}
		else if (expend && value == -30)
		{
			poseType = idleClip.Count - 6;
			switch (mode)
			{
			case 0:
				StateSet("isBackFuckStart", 0.5f);
				break;
			case 2:
				StateSet("isBackFuckChange", 0.5f);
				break;
			default:
				StateSet("isBackFuckEnd", 0.25f);
				break;
			}
			FuckName = "BackFuck";
		}
		else if (expend && value == -31)
		{
			poseType = idleClip.Count - 5;
			switch (mode)
			{
			case 0:
				StateSet("isFrontFuckStart", 0.5f);
				break;
			case 2:
				StateSet("isFrontFuckChange", 0.5f);
				break;
			default:
				StateSet("isFrontFuckEnd", 0.25f);
				break;
			}
			FuckName = "FrontFuck";
		}
		else if (expend && value == -32)
		{
			poseType = idleClip.Count - 4;
			switch (mode)
			{
			case 0:
				StateSet("isRideFuckStart", 0.5f);
				break;
			case 2:
				StateSet("isRideFuckChange", 0.5f);
				break;
			default:
				StateSet("isRideFuckEnd", 0.25f);
				break;
			}
			FuckName = "RideFuck";
		}
		else if (expend && value == -33)
		{
			poseType = idleClip.Count - 3;
			switch (mode)
			{
			case 0:
				StateSet("isLiftFuckStart", 0.5f);
				break;
			case 2:
				StateSet("isLiftFuckChange", 0.5f);
				break;
			default:
				StateSet("isLiftFuckEnd", 0.25f);
				break;
			}
			FuckName = "LiftFuck";
		}
		else if (expend && value == -34)
		{
			poseType = idleClip.Count - 2;
			switch (mode)
			{
			case 0:
				StateSet("isSideFuckStart", 0.5f);
				break;
			case 2:
				StateSet("isSideFuckChange", 0.5f);
				break;
			default:
				StateSet("isSideFuckEnd", 0.25f);
				break;
			}
			FuckName = "SideFuck";
		}
		else if (expend && value == -35)
		{
			poseType = idleClip.Count - 1;
			switch (mode)
			{
			case 0:
				StateSet("isDoggyFuckStart", 0.5f);
				break;
			case 2:
				StateSet("isDoggyFuckChange", 0.5f);
				break;
			default:
				StateSet("isDoggyFuckEnd", 0.25f);
				break;
			}
			FuckName = "DoggyFuck";
		}
		else
		{
			Debug.LogError("Motin is not found", base.gameObject);
		}
	}

	public void ChangeAnimationSpeed(float value)
	{
		animationSpeed = value;
		if (StateName == "isPiston")
		{
			_state.Speed = animationSpeed;
			if (poseType < normalPose)
			{
				animancerLayer1.Weight = pistonMixParameter;
			}
			else
			{
				animancerLayer1.Weight = 0f;
			}
		}
		else
		{
			animancerLayer1.Weight = 0f;
		}
		for (int i = 0; i < pistonSpeedMixer.Count; i++)
		{
			pistonSpeedMixer[i].Parameter = pistonMixParameter;
		}
	}

	public void StateSet(string value, float feed)
	{
		Debug.Log(base.gameObject.name + " PoseType:" + poseType + " StateSet:" + value + " EndWait:" + isEndWait, base.gameObject);
		feedTime = feed;
		if (_motionFuckAnimancer != null)
		{
			_motionFuckAnimancer.StateSet(value, feed);
		}
		if (isEndWait)
		{
			return;
		}
		PreviousStateName = StateName;
		StateName = value;
		AnimancerState current = _animancer.States.Current;
		if (current != null && current.IsPlayingAndNotEnding())
		{
			current.Events(this).Clear();
		}
		if (StateName == "isSkip")
		{
			StockStateName = StateName;
		}
		else if (StateName == "isIdle")
		{
			if (PreviousStateName == "isShotIdle")
			{
				StockStateName = StateName;
				if (poseType < normalPose)
				{
					Debug.LogError("isShotIdle Normal current:" + StateName + " prev: " + PreviousStateName + " stock: " + StockStateName);
					_state = _animancer.Play(SetMixer(idleClip), feedTime * 2f);
					animancerLayer1.Weight = 0f;
					animancerLayer1.Play(pistonIdleMixer, feedTime * 2f);
				}
				else
				{
					Debug.LogError("isShotIdle Other current: " + StateName + " prev: " + PreviousStateName + " stock: " + StockStateName);
					_state = _animancer.Play(idleClip[poseType], feedTime * 2f);
				}
			}
			else if (isInsert && isInsertLoop && isVaginaInsert)
			{
				StockStateName = StateName;
				if (poseType < normalPose)
				{
					Debug.LogError("isInsertLoop Normal current: " + StateName + " prev: " + PreviousStateName + " stock: " + StockStateName);
					_state = _animancer.Play(SetMixer(insertClip), feedTime);
					animancerLayer1.Weight = 0f;
					animancerLayer1.Play(pistonIdleMixer, feedTime);
				}
				else
				{
					Debug.LogError("isInsertLoop Other current: " + StateName + " prev: " + PreviousStateName + " stock: " + StockStateName);
					_state = _animancer.Play(insertClip[poseType], feedTime);
				}
			}
			else
			{
				StockStateName = StateName;
				if (poseType < normalPose)
				{
					Debug.LogError("isIdle Normal current: " + StateName + " prev: " + PreviousStateName + " stock: " + StockStateName);
					_state = _animancer.Play(SetMixer(idleClip), feedTime);
					animancerLayer1.Weight = 0f;
					animancerLayer1.Play(pistonIdleMixer, feedTime);
				}
				else
				{
					Debug.LogError("isIdle Other current: " + StateName + " prev: " + PreviousStateName + " stock: " + StockStateName);
					_state = _animancer.Play(idleClip[poseType], feedTime);
				}
			}
			_state.Speed = 1f;
		}
		else if (StateName == "isInsert")
		{
			StockStateName = "isInsert";
			if (poseType < normalPose)
			{
				Debug.LogError("isInsert Normal current:" + StateName + " prev: " + PreviousStateName + " stock: " + StockStateName);
				_state = _animancer.Play(SetMixer(insertClip), feedTime);
				animancerLayer1.Weight = 0f;
				animancerLayer1.Play(pistonIdleMixer, feedTime);
			}
			else
			{
				Debug.LogError("isInsert Other current:" + StateName + " prev: " + PreviousStateName + " stock: " + StockStateName);
				_state = _animancer.Play(insertClip[poseType], feedTime);
			}
			_state.Speed = 1f;
			_state.Events(this).OnEnd = delegate
			{
				Debug.LogError("isInsert EndState current:" + StateName + " prev: " + PreviousStateName + " stock: " + StockStateName);
				_state.Events(this).OnEnd = null;
				StateSet("isIdle", 0.25f);
			};
		}
		else if (StateName == "isPiston")
		{
			StockStateName = StateName;
			for (int num = 0; num < pistonSpeedMixer.Count; num++)
			{
				pistonSpeedMixer[num].Parameter = pistonMixParameter;
			}
			if (poseType < normalPose)
			{
				_state = _animancer.Play(SetMixer(pistonClip), feedTime);
				animancerLayer1.Weight = pistonMixParameter;
				animancerLayer1.Play(pistonMixer, feedTime);
			}
			else
			{
				_state = _animancer.Play(pistonSpeedMixer[poseType], feedTime);
				animancerLayer1.Weight = 0f;
			}
			_state.Speed = animationSpeed;
		}
		else if (StateName == "isShot")
		{
			StockStateName = StateName;
			isEndWait = true;
			if (poseType < normalPose)
			{
				_state = _animancer.Play(SetMixer(shotClip), feedTime);
				animancerLayer1.Weight = 0f;
				animancerLayer1.Play(pistonIdleMixer, feedTime);
			}
			else
			{
				_state = _animancer.Play(shotClip[poseType], feedTime);
			}
			_state.Speed = 1f;
			_state.Events(this).OnEnd = delegate
			{
				isEndWait = false;
				if (isVaginaPiston || (isAnalPiston && !isFuck))
				{
					for (int i = 0; i < pistonSpeedMixer.Count; i++)
					{
						pistonSpeedMixer[i].Parameter = pistonMixParameter;
					}
					if (poseType < normalPose)
					{
						_state = _animancer.Play(SetMixer(pistonClip), feedTime);
						animancerLayer1.Weight = pistonMixParameter;
						animancerLayer1.Play(pistonMixer, feedTime);
					}
					else
					{
						animancerLayer1.Weight = 0f;
						_animancer.Play(pistonSpeedMixer[poseType], feedTime);
					}
					_state.Speed = animationSpeed;
					StateName = "isPiston";
					StockStateName = StateName;
				}
				else
				{
					if (poseType < normalPose)
					{
						_state = _animancer.Play(SetMixer(shotIdleClip), 0.25f);
						animancerLayer1.Weight = 0f;
						animancerLayer1.Play(pistonIdleMixer, feedTime);
					}
					else
					{
						_state = _animancer.Play(shotIdleClip[poseType], 0.25f);
					}
					animancerLayer1.Play(pistonIdleMixer, feedTime);
					_state.Speed = 1f;
					StateName = "isShotIdle";
					StockStateName = StateName;
				}
			};
		}
		else if (StateName == "isShotIdle")
		{
			StockStateName = StateName;
			if (poseType < normalPose)
			{
				_state = _animancer.Play(SetMixer(shotIdleClip), 0.25f);
				animancerLayer1.Weight = 0f;
				animancerLayer1.Play(pistonIdleMixer, feedTime);
			}
			else
			{
				_state = _animancer.Play(shotIdleClip[poseType], 0.25f);
			}
			animancerLayer1.Play(pistonIdleMixer, feedTime);
			_state.Speed = 1f;
		}
		else if (StateName == "isEatStart" || StateName == "isEatEnd")
		{
			isEndWait = true;
			if (StateName == "isEatStart")
			{
				_state = _animancer.Play(eatClip[0], feedTime);
			}
			if (StateName == "isEatEnd")
			{
				_state = _animancer.Play(eatClip[1], feedTime);
			}
			animancerLayer1.Play(pistonIdleMixer, feedTime);
			_state.Speed = 1f;
			_state.Events(this).OnEnd = delegate
			{
				isEndWait = false;
				StateSet(StockStateName, feedTime);
			};
		}
		else if (StateName == "isRideStart" || StateName == "isRideEnd")
		{
			isEndWait = true;
			StockStateName = "isIdle";
			if (StateName == "isRideStart")
			{
				_state = _animancer.Play(rideClip[0], feedTime);
			}
			if (StateName == "isRideEnd")
			{
				_state = _animancer.Play(rideClip[1], feedTime);
			}
			animancerLayer1.Play(pistonIdleMixer, feedTime);
			_state.Speed = 1f;
			_state.Events(this).OnEnd = delegate
			{
				isEndWait = false;
				StateSet(StockStateName, feedTime);
			};
		}
		else if (StateName == "isLimbHoldStart" || StateName == "isLimbHoldEnd")
		{
			isEndWait = true;
			if (StateName == "isLimbHoldStart")
			{
				_state = _animancer.Play(limbHoldClip[0], feedTime);
			}
			if (StateName == "isLimbHoldEnd")
			{
				_state = _animancer.Play(limbHoldClip[1], feedTime);
			}
			animancerLayer1.Play(pistonIdleMixer, feedTime);
			_state.Speed = 1f;
			_state.Events(this).OnEnd = delegate
			{
				isEndWait = false;
				StateSet(StockStateName, feedTime);
			};
		}
		else if (StateName == "isWartBedStart" || StateName == "isWartBedEnd")
		{
			isEndWait = true;
			if (StateName == "isWartBedStart")
			{
				_state = _animancer.Play(wartBedClip[0], feedTime);
			}
			if (StateName == "isWartBedEnd")
			{
				_state = _animancer.Play(wartBedClip[1], feedTime);
			}
			animancerLayer1.Play(pistonIdleMixer, feedTime);
			_state.Speed = 1f;
			_state.Events(this).OnEnd = delegate
			{
				isEndWait = false;
				StateSet(StockStateName, feedTime);
			};
		}
		else if (StateName == "isWallHipStart" || StateName == "isWallHipEnd")
		{
			isEndWait = true;
			if (StateName == "isWallHipStart")
			{
				_state = _animancer.Play(wallHipClip[0], feedTime);
			}
			if (StateName == "isWallHipEnd")
			{
				_state = _animancer.Play(wallHipClip[1], feedTime);
			}
			animancerLayer1.Play(pistonIdleMixer, feedTime);
			_state.Speed = 1f;
			_state.Events(this).OnEnd = delegate
			{
				isEndWait = false;
				StateSet(StockStateName, feedTime);
			};
		}
		else if (StateName == "isPillarBindStart" || StateName == "isPillarBindEnd")
		{
			isEndWait = true;
			if (StateName == "isPillarBindStart")
			{
				_state = _animancer.Play(pillarBindClip[0], feedTime);
			}
			if (StateName == "isPillarBindEnd")
			{
				_state = _animancer.Play(pillarBindClip[1], feedTime);
			}
			animancerLayer1.Play(pistonIdleMixer, feedTime);
			_state.Speed = 1f;
			_state.Events(this).OnEnd = delegate
			{
				isEndWait = false;
				StateSet(StockStateName, feedTime);
			};
		}
		else if (StateName == "isBackFuckStart" || StateName == "isBackFuckEnd")
		{
			isEndWait = true;
			StockStateName = "isIdle";
			if (StateName == "isBackFuckStart")
			{
				isFuckEnd = false;
				_state = _animancer.Play(backFuckClip[0], feedTime);
			}
			if (StateName == "isBackFuckEnd")
			{
				isFuckEnd = true;
				_state = _animancer.Play(backFuckClip[1], feedTime);
			}
			animancerLayer1.Play(pistonIdleMixer, feedTime);
			_state.Speed = 1f;
			_state.Events(this).OnEnd = delegate
			{
				isEndWait = false;
				isInsertLoop = false;
				if (isFuckEnd && isAnalPiston)
				{
					StateSet("isPiston", feedTime);
				}
				else
				{
					StateSet(StockStateName, feedTime);
				}
			};
		}
		else if (StateName == "isBackFuckChange")
		{
			isEndWait = true;
			if (isInsert)
			{
				_uterusController.Insert(value: false);
			}
			if (FuckName == "FrontFuck")
			{
				_state = _animancer.Play(frontFuckClip[2], feedTime);
			}
			if (FuckName == "RideFuck")
			{
				_state = _animancer.Play(rideFuckClip[2], feedTime);
			}
			if (FuckName == "BackFuck")
			{
				_state = _animancer.Play(backFuckClip[2], feedTime);
			}
			if (FuckName == "LiftFuck")
			{
				_state = _animancer.Play(liftFuckClip[2], feedTime);
			}
			if (FuckName == "SideFuck")
			{
				_state = _animancer.Play(sideFuckClip[2], feedTime);
			}
			if (FuckName == "DoggyFuck")
			{
				_state = _animancer.Play(doggyFuckClip[2], feedTime);
			}
			_state.Speed = 1f;
			_state.Events(this).OnEnd = delegate
			{
				_state = _animancer.Play(backFuckClip[3], 0.1f);
				_state.Events(this).OnEnd = delegate
				{
					isEndWait = false;
					isInsertLoop = false;
					if (isPiston)
					{
						_uterusController.Piston(value: true);
					}
					else if (isInsert)
					{
						_uterusController.Insert(value: true);
					}
					StateSet(StockStateName, feedTime);
				};
			};
		}
		else if (StateName == "isFrontFuckStart" || StateName == "isFrontFuckEnd")
		{
			isEndWait = true;
			StockStateName = "isIdle";
			if (StateName == "isFrontFuckStart")
			{
				isFuckEnd = false;
				_state = _animancer.Play(frontFuckClip[0], feedTime);
			}
			if (StateName == "isFrontFuckEnd")
			{
				isFuckEnd = true;
				_state = _animancer.Play(frontFuckClip[1], feedTime);
			}
			animancerLayer1.Play(pistonIdleMixer, feedTime);
			_state.Speed = 1f;
			_state.Events(this).OnEnd = delegate
			{
				isEndWait = false;
				isInsertLoop = false;
				if (isFuckEnd && isAnalPiston)
				{
					StateSet("isPiston", feedTime);
				}
				else
				{
					StateSet(StockStateName, feedTime);
				}
			};
		}
		else if (StateName == "isFrontFuckChange")
		{
			isEndWait = true;
			if (isInsert)
			{
				_uterusController.Insert(value: false);
			}
			if (FuckName == "FrontFuck")
			{
				_state = _animancer.Play(frontFuckClip[2], feedTime);
			}
			if (FuckName == "RideFuck")
			{
				_state = _animancer.Play(rideFuckClip[2], feedTime);
			}
			if (FuckName == "BackFuck")
			{
				_state = _animancer.Play(backFuckClip[2], feedTime);
			}
			if (FuckName == "LiftFuck")
			{
				_state = _animancer.Play(liftFuckClip[2], feedTime);
			}
			if (FuckName == "SideFuck")
			{
				_state = _animancer.Play(sideFuckClip[2], feedTime);
			}
			if (FuckName == "DoggyFuck")
			{
				_state = _animancer.Play(doggyFuckClip[2], feedTime);
			}
			_state.Speed = 1f;
			_state.Events(this).OnEnd = delegate
			{
				Debug.LogError(base.gameObject.name + " FuckAnim:" + FuckName, base.gameObject);
				_state = _animancer.Play(frontFuckClip[3], 0.1f);
				_state.Events(this).OnEnd = delegate
				{
					isEndWait = false;
					isInsertLoop = false;
					if (isPiston)
					{
						_uterusController.Piston(value: true);
					}
					else if (isInsert)
					{
						_uterusController.Insert(value: true);
					}
					StateSet(StockStateName, feedTime);
				};
			};
		}
		else if (StateName == "isRideFuckStart" || StateName == "isRideFuckEnd")
		{
			isEndWait = true;
			StockStateName = "isIdle";
			if (StateName == "isRideFuckStart")
			{
				isFuckEnd = false;
				_state = _animancer.Play(rideFuckClip[0], feedTime);
			}
			if (StateName == "isRideFuckEnd")
			{
				isFuckEnd = true;
				_state = _animancer.Play(rideFuckClip[1], feedTime);
			}
			animancerLayer1.Play(pistonIdleMixer, feedTime);
			_state.Speed = 1f;
			_state.Events(this).OnEnd = delegate
			{
				isEndWait = false;
				isInsertLoop = true;
				if (isFuckEnd && isAnalPiston)
				{
					StateSet("isPiston", feedTime);
				}
				else
				{
					StateSet(StockStateName, feedTime);
				}
			};
		}
		else if (StateName == "isRideFuckChange")
		{
			isEndWait = true;
			if (isInsert)
			{
				_uterusController.Insert(value: false);
			}
			if (FuckName == "FrontFuck")
			{
				_state = _animancer.Play(frontFuckClip[2], feedTime);
			}
			if (FuckName == "RideFuck")
			{
				_state = _animancer.Play(rideFuckClip[2], feedTime);
			}
			if (FuckName == "BackFuck")
			{
				_state = _animancer.Play(backFuckClip[2], feedTime);
			}
			if (FuckName == "LiftFuck")
			{
				_state = _animancer.Play(liftFuckClip[2], feedTime);
			}
			if (FuckName == "SideFuck")
			{
				_state = _animancer.Play(sideFuckClip[2], feedTime);
			}
			if (FuckName == "DoggyFuck")
			{
				_state = _animancer.Play(doggyFuckClip[2], feedTime);
			}
			_state.Speed = 1f;
			_state.Events(this).OnEnd = delegate
			{
				_state = _animancer.Play(rideFuckClip[3], 0.1f);
				_state.Events(this).OnEnd = delegate
				{
					isEndWait = false;
					isInsertLoop = true;
					if (isPiston)
					{
						_uterusController.Piston(value: true);
					}
					else if (isInsert)
					{
						_uterusController.Insert(value: true);
					}
					StateSet(StockStateName, feedTime);
				};
			};
		}
		else if (StateName == "isLiftFuckStart" || StateName == "isLiftFuckEnd")
		{
			isEndWait = true;
			StockStateName = "isIdle";
			if (StateName == "isLiftFuckStart")
			{
				isFuckEnd = false;
				_state = _animancer.Play(liftFuckClip[0], feedTime);
			}
			if (StateName == "isLiftFuckEnd")
			{
				isFuckEnd = true;
				_state = _animancer.Play(liftFuckClip[1], feedTime);
			}
			animancerLayer1.Play(pistonIdleMixer, feedTime);
			_state.Speed = 1f;
			_state.Events(this).OnEnd = delegate
			{
				isEndWait = false;
				isInsertLoop = false;
				if (isFuckEnd && isAnalPiston)
				{
					StateSet("isPiston", feedTime);
				}
				else
				{
					StateSet(StockStateName, feedTime);
				}
			};
		}
		else if (StateName == "isLiftFuckChange")
		{
			isEndWait = true;
			if (isInsert)
			{
				_uterusController.Insert(value: false);
			}
			if (FuckName == "FrontFuck")
			{
				_state = _animancer.Play(frontFuckClip[2], feedTime);
			}
			if (FuckName == "RideFuck")
			{
				_state = _animancer.Play(rideFuckClip[2], feedTime);
			}
			if (FuckName == "BackFuck")
			{
				_state = _animancer.Play(backFuckClip[2], feedTime);
			}
			if (FuckName == "LiftFuck")
			{
				_state = _animancer.Play(liftFuckClip[2], feedTime);
			}
			if (FuckName == "SideFuck")
			{
				_state = _animancer.Play(sideFuckClip[2], feedTime);
			}
			if (FuckName == "DoggyFuck")
			{
				_state = _animancer.Play(doggyFuckClip[2], feedTime);
			}
			_state.Speed = 1f;
			_state.Events(this).OnEnd = delegate
			{
				_state = _animancer.Play(liftFuckClip[3], 0.1f);
				_state.Events(this).OnEnd = delegate
				{
					isEndWait = false;
					isInsertLoop = false;
					if (isPiston)
					{
						_uterusController.Piston(value: true);
					}
					else if (isInsert)
					{
						_uterusController.Insert(value: true);
					}
					StateSet(StockStateName, feedTime);
				};
			};
		}
		else if (StateName == "isSideFuckStart" || StateName == "isSideFuckEnd")
		{
			isEndWait = true;
			StockStateName = "isIdle";
			if (StateName == "isSideFuckStart")
			{
				isFuckEnd = false;
				_state = _animancer.Play(sideFuckClip[0], feedTime);
			}
			if (StateName == "isSideFuckEnd")
			{
				isFuckEnd = true;
				_state = _animancer.Play(sideFuckClip[1], feedTime);
			}
			animancerLayer1.Play(pistonIdleMixer, feedTime);
			_state.Speed = 1f;
			_state.Events(this).OnEnd = delegate
			{
				isEndWait = false;
				isInsertLoop = false;
				if (isFuckEnd && isAnalPiston)
				{
					StateSet("isPiston", feedTime);
				}
				else
				{
					StateSet(StockStateName, feedTime);
				}
			};
		}
		else if (StateName == "isSideFuckChange")
		{
			isEndWait = true;
			if (isInsert)
			{
				_uterusController.Insert(value: false);
			}
			if (FuckName == "FrontFuck")
			{
				_state = _animancer.Play(frontFuckClip[2], feedTime);
			}
			if (FuckName == "RideFuck")
			{
				_state = _animancer.Play(rideFuckClip[2], feedTime);
			}
			if (FuckName == "BackFuck")
			{
				_state = _animancer.Play(backFuckClip[2], feedTime);
			}
			if (FuckName == "LiftFuck")
			{
				_state = _animancer.Play(liftFuckClip[2], feedTime);
			}
			if (FuckName == "SideFuck")
			{
				_state = _animancer.Play(sideFuckClip[2], feedTime);
			}
			if (FuckName == "DoggyFuck")
			{
				_state = _animancer.Play(doggyFuckClip[2], feedTime);
			}
			_state.Speed = 1f;
			_state.Events(this).OnEnd = delegate
			{
				_state = _animancer.Play(sideFuckClip[3], 0.1f);
				_state.Events(this).OnEnd = delegate
				{
					isEndWait = false;
					isInsertLoop = false;
					if (isPiston)
					{
						_uterusController.Piston(value: true);
					}
					else if (isInsert)
					{
						_uterusController.Insert(value: true);
					}
					StateSet(StockStateName, feedTime);
				};
			};
		}
		else if (StateName == "isDoggyFuckStart" || StateName == "isDoggyFuckEnd")
		{
			isEndWait = true;
			StockStateName = "isIdle";
			if (StateName == "isDoggyFuckStart")
			{
				isFuckEnd = false;
				_state = _animancer.Play(doggyFuckClip[0], feedTime);
			}
			if (StateName == "isDoggyFuckEnd")
			{
				isFuckEnd = true;
				_state = _animancer.Play(doggyFuckClip[1], feedTime);
			}
			animancerLayer1.Play(pistonIdleMixer, feedTime);
			_state.Speed = 1f;
			_state.Events(this).OnEnd = delegate
			{
				isEndWait = false;
				isInsertLoop = false;
				if (isFuckEnd && isAnalPiston)
				{
					StateSet("isPiston", feedTime);
				}
				else
				{
					StateSet(StockStateName, feedTime);
				}
			};
		}
		else if (StateName == "isDoggyFuckChange")
		{
			isEndWait = true;
			if (isInsert)
			{
				_uterusController.Insert(value: false);
			}
			if (FuckName == "FrontFuck")
			{
				_state = _animancer.Play(frontFuckClip[2], feedTime);
			}
			if (FuckName == "RideFuck")
			{
				_state = _animancer.Play(rideFuckClip[2], feedTime);
			}
			if (FuckName == "BackFuck")
			{
				_state = _animancer.Play(backFuckClip[2], feedTime);
			}
			if (FuckName == "LiftFuck")
			{
				_state = _animancer.Play(liftFuckClip[2], feedTime);
			}
			if (FuckName == "SideFuck")
			{
				_state = _animancer.Play(sideFuckClip[2], feedTime);
			}
			if (FuckName == "DoggyFuck")
			{
				_state = _animancer.Play(doggyFuckClip[2], feedTime);
			}
			_state.Speed = 1f;
			_state.Events(this).OnEnd = delegate
			{
				_state = _animancer.Play(doggyFuckClip[3], 0.1f);
				_state.Events(this).OnEnd = delegate
				{
					isEndWait = false;
					isInsertLoop = false;
					if (isPiston)
					{
						_uterusController.Piston(value: true);
					}
					else if (isInsert)
					{
						_uterusController.Insert(value: true);
					}
					StateSet(StockStateName, feedTime);
				};
			};
		}
		else
		{
			Debug.LogError("Animation State nothing!", base.gameObject);
		}
	}
}
