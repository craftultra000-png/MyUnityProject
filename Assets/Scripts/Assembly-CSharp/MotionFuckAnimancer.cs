using System.Collections.Generic;
using Animancer;
using UnityEngine;

public class MotionFuckAnimancer : MonoBehaviour
{
	public AnimancerComponent _animancer;

	public CharacterPositionManager _characterPositionManager;

	public CharacterEffectManager _characterEffectManager;

	public CharacterLifeManager _characterLifeManager;

	[Header("Status")]
	public string StateName = "isIdle";

	public string PreviousStateName = "isIdle";

	public string StockStateName = "isIdle";

	public string FuckName = "AnyFuck";

	public bool isEndWait;

	[Header("Status")]
	public bool isInsert;

	public bool isVaginaPiston;

	public bool isAnalPiston;

	public bool isFirstInsert;

	[Header("Anim Speed")]
	public float animationSpeed;

	[Header("Anim FeedTime")]
	public float feedTime = 0.5f;

	public float pistonMixParameter;

	[Header("Position")]
	public Vector3 defaultPosition;

	[Header("Animation Clip")]
	public int poseType;

	public AnimationClip tPose;

	public List<AnimationClip> listCiip;

	public AnimationClip bottomClip;

	public List<AnimationClip> idleClip;

	public List<AnimationClip> insertClip;

	public List<AnimationClip> pistonClip;

	public List<AnimationClip> pistonHighClip;

	public List<AnimationClip> shotClip;

	public List<AnimationClip> shotIdleClip;

	[Header("Expend")]
	public List<AnimationClip> backFuckClip;

	public List<AnimationClip> frontFuckClip;

	public List<AnimationClip> rideFuckClip;

	public List<AnimationClip> liftFuckClip;

	public List<AnimationClip> sideFuckClip;

	public List<AnimationClip> doggyFuckClip;

	private List<LinearMixerState> pistonMixer = new List<LinearMixerState>();

	private AnimancerState _state;

	private void Start()
	{
		_animancer.Graph.ApplyFootIK = true;
		_animancer.Layers[0].ApplyFootIK = true;
		StateSet("isBottom", 0.25f);
		for (int i = 0; i < pistonClip.Count; i++)
		{
			LinearMixerState linearMixerState = new LinearMixerState();
			linearMixerState.Add(pistonClip[i], 0f);
			linearMixerState.Add(pistonHighClip[i], 1f);
			pistonMixer.Add(linearMixerState);
			pistonMixer[i].Parameter = pistonMixParameter;
		}
		base.transform.position = defaultPosition;
	}

	public void PoseChange(int value, bool expend, int mode)
	{
		if (!expend)
		{
			poseType = value;
			StateSet(StateName, 0.25f);
		}
		else if (expend && value == -30)
		{
			poseType = 0;
			if (mode == 0)
			{
				StateSet("isBackFuckStart", 0.5f);
			}
			if (mode == 2)
			{
				StateSet("isBackFuckChange", 0.5f);
			}
			else
			{
				StateSet("isBackFuckEnd", 0.25f);
			}
			FuckName = "BackFuck";
		}
		else if (expend && value == -31)
		{
			poseType = 1;
			if (mode == 0)
			{
				StateSet("isFrontFuckStart", 0.5f);
			}
			if (mode == 2)
			{
				StateSet("isFrontFuckChange", 0.5f);
			}
			else
			{
				StateSet("isFrontFuckEnd", 0.25f);
			}
			FuckName = "FrontFuck";
		}
		else if (expend && value == -32)
		{
			poseType = 2;
			if (mode == 0)
			{
				StateSet("isRideFuckStart", 0.5f);
			}
			if (mode == 2)
			{
				StateSet("isRideFuckChange", 0.5f);
			}
			else
			{
				StateSet("isRideFuckEnd", 0.25f);
			}
			FuckName = "RideFuck";
		}
		else if (expend && value == -33)
		{
			poseType = 3;
			if (mode == 0)
			{
				StateSet("isLiftFuckStart", 0.5f);
			}
			if (mode == 2)
			{
				StateSet("isLiftFuckChange", 0.5f);
			}
			else
			{
				StateSet("isLiftFuckEnd", 0.25f);
			}
			FuckName = "LiftFuck";
		}
		else if (expend && value == -34)
		{
			poseType = 4;
			if (mode == 0)
			{
				StateSet("isSideFuckStart", 0.5f);
			}
			if (mode == 2)
			{
				StateSet("isSideFuckChange", 0.5f);
			}
			else
			{
				StateSet("isSideFuckEnd", 0.25f);
			}
			FuckName = "SideFuck";
		}
		else if (expend && value == -35)
		{
			poseType = 5;
			if (mode == 0)
			{
				StateSet("isDoggyFuckStart", 0.5f);
			}
			if (mode == 2)
			{
				StateSet("isDoggyFuckChange", 0.5f);
			}
			else
			{
				StateSet("isDoggyFuckEnd", 0.25f);
			}
			FuckName = "DoggyFuck";
		}
	}

	public void ChangeAnimationSpeed(float value)
	{
		animationSpeed = value;
		if (StateName == "isPiston")
		{
			_state.Speed = animationSpeed;
		}
		for (int i = 0; i < pistonMixer.Count; i++)
		{
			pistonMixer[i].Parameter = pistonMixParameter;
		}
	}

	public void StateSet(string value, float feed)
	{
		Debug.Log(base.gameObject.name + " StateSet:" + value, base.gameObject);
		feedTime = feed;
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
		else if (StateName == "isBottom")
		{
			StockStateName = StateName;
			_state = _animancer.Play(bottomClip, feedTime);
			_state.Speed = 1f;
		}
		else if (StateName == "isIdle" || StateName == "isInsert")
		{
			StockStateName = StateName;
			if (PreviousStateName == "isShotIdle")
			{
				if (!isInsert)
				{
					_state = _animancer.Play(idleClip[poseType], feedTime * 2f);
				}
				else
				{
					_state = _animancer.Play(insertClip[poseType], feedTime * 2f);
				}
			}
			else if (!isInsert)
			{
				_state = _animancer.Play(idleClip[poseType], feedTime);
			}
			else
			{
				_state = _animancer.Play(insertClip[poseType], feedTime);
			}
			_state.Speed = 1f;
			if (!isFirstInsert && StateName == "isInsert")
			{
				isFirstInsert = true;
				_characterLifeManager.HitData("Vagina", "LostVirsin");
			}
		}
		else if (StateName == "isPiston")
		{
			StockStateName = StateName;
			for (int i = 0; i < pistonMixer.Count; i++)
			{
				pistonMixer[i].Parameter = pistonMixParameter;
			}
			_state = _animancer.Play(pistonMixer[poseType], feedTime);
			_state.Speed = animationSpeed;
			if (!isFirstInsert)
			{
				isFirstInsert = true;
				_characterLifeManager.HitData("Vagina", "LostVirsin");
			}
		}
		else if (StateName == "isShot")
		{
			StockStateName = StateName;
			isEndWait = true;
			_state = _animancer.Play(shotClip[poseType], feedTime);
			_state.Speed = 1f;
			_state.Events(this).OnEnd = delegate
			{
				isEndWait = false;
				if (isVaginaPiston || isAnalPiston)
				{
					for (int j = 0; j < pistonMixer.Count; j++)
					{
						pistonMixer[j].Parameter = pistonMixParameter;
					}
					_state = _animancer.Play(pistonMixer[poseType], feedTime);
					_state.Speed = animationSpeed;
					StateName = "isPiston";
					StockStateName = StateName;
				}
				else
				{
					_state = _animancer.Play(shotIdleClip[poseType], 0.25f);
					_state.Speed = 1f;
					StateName = "isShotIdle";
					StockStateName = StateName;
				}
			};
		}
		else if (StateName == "isShotIdle")
		{
			StockStateName = StateName;
			_state = _animancer.Play(shotIdleClip[poseType], feedTime);
			_state.Speed = 1f;
		}
		else if (StateName == "isBackFuckStart" || StateName == "isBackFuckEnd")
		{
			isEndWait = true;
			if (StateName == "isBackFuckStart")
			{
				StockStateName = "isIdle";
				_state = _animancer.Play(backFuckClip[0], feedTime);
			}
			if (StateName == "isBackFuckEnd")
			{
				StockStateName = "isBottom";
				_state = _animancer.Play(backFuckClip[1], feedTime);
			}
			_state.Speed = 1f;
			_state.Events(this).OnEnd = delegate
			{
				isEndWait = false;
				StateSet(StockStateName, feedTime);
			};
		}
		else if (StateName == "isBackFuckChange")
		{
			isEndWait = true;
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
					StateSet(StockStateName, feedTime);
				};
			};
		}
		else if (StateName == "isFrontFuckStart" || StateName == "isFrontFuckEnd")
		{
			isEndWait = true;
			if (StateName == "isFrontFuckStart")
			{
				StockStateName = "isIdle";
				_state = _animancer.Play(frontFuckClip[0], feedTime);
			}
			if (StateName == "isFrontFuckEnd")
			{
				StockStateName = "isBottom";
				_state = _animancer.Play(frontFuckClip[1], feedTime);
			}
			_state.Speed = 1f;
			_state.Events(this).OnEnd = delegate
			{
				isEndWait = false;
				StateSet(StockStateName, feedTime);
			};
		}
		else if (StateName == "isFrontFuckChange")
		{
			isEndWait = true;
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
				_state = _animancer.Play(frontFuckClip[3], 0.1f);
				_state.Events(this).OnEnd = delegate
				{
					isEndWait = false;
					StateSet(StockStateName, feedTime);
				};
			};
		}
		else if (StateName == "isRideFuckStart" || StateName == "isRideFuckEnd")
		{
			isEndWait = true;
			if (StateName == "isRideFuckStart")
			{
				StockStateName = "isIdle";
				_state = _animancer.Play(rideFuckClip[0], feedTime);
			}
			if (StateName == "isRideFuckEnd")
			{
				StockStateName = "isBottom";
				_state = _animancer.Play(rideFuckClip[1], feedTime);
			}
			_state.Speed = 1f;
			_state.Events(this).OnEnd = delegate
			{
				isEndWait = false;
				StateSet(StockStateName, feedTime);
			};
		}
		else if (StateName == "isRideFuckChange")
		{
			isEndWait = true;
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
					StateSet(StockStateName, feedTime);
				};
			};
		}
		else if (StateName == "isLiftFuckStart" || StateName == "isLiftFuckEnd")
		{
			isEndWait = true;
			if (StateName == "isLiftFuckStart")
			{
				StockStateName = "isIdle";
				_state = _animancer.Play(liftFuckClip[0], feedTime);
			}
			if (StateName == "isLiftFuckEnd")
			{
				StockStateName = "isBottom";
				_state = _animancer.Play(liftFuckClip[1], feedTime);
			}
			_state.Speed = 1f;
			_state.Events(this).OnEnd = delegate
			{
				isEndWait = false;
				StateSet(StockStateName, feedTime);
			};
		}
		else if (StateName == "isLiftFuckChange")
		{
			isEndWait = true;
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
					StateSet(StockStateName, feedTime);
				};
			};
		}
		else if (StateName == "isSideFuckStart" || StateName == "isSideFuckEnd")
		{
			isEndWait = true;
			if (StateName == "isSideFuckStart")
			{
				StockStateName = "isIdle";
				_state = _animancer.Play(sideFuckClip[0], feedTime);
			}
			if (StateName == "isSideFuckEnd")
			{
				StockStateName = "isBottom";
				_state = _animancer.Play(sideFuckClip[1], feedTime);
			}
			_state.Speed = 1f;
			_state.Events(this).OnEnd = delegate
			{
				isEndWait = false;
				StateSet(StockStateName, feedTime);
			};
		}
		else if (StateName == "isSideFuckChange")
		{
			isEndWait = true;
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
					StateSet(StockStateName, feedTime);
				};
			};
		}
		else if (StateName == "isDoggyFuckStart" || StateName == "isDoggyFuckEnd")
		{
			isEndWait = true;
			if (StateName == "isDoggyFuckStart")
			{
				StockStateName = "isIdle";
				_state = _animancer.Play(doggyFuckClip[0], feedTime);
			}
			if (StateName == "isDoggyFuckEnd")
			{
				StockStateName = "isBottom";
				_state = _animancer.Play(doggyFuckClip[1], feedTime);
			}
			_state.Speed = 1f;
			_state.Events(this).OnEnd = delegate
			{
				isEndWait = false;
				StateSet(StockStateName, feedTime);
			};
		}
		else if (StateName == "isDoggyFuckChange")
		{
			isEndWait = true;
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
