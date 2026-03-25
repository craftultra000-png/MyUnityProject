using System.Collections.Generic;
using System.Linq;
using Animancer;
using UnityEngine;

public class ReactionAnimancer : MonoBehaviour
{
	public AnimancerComponent _animancer;

	public MotionAnimancer _motionAnimancer;

	[Header("Status")]
	public bool isReaction;

	public bool isReaction2;

	public bool isReactionForce;

	public string ReactionName = "isIdle";

	[Header("ReactionWeight")]
	public AnimationCurve animationTime;

	public float currentTime;

	public float maxTime;

	public float calcTime;

	public float currentWeight;

	public float targetWeight;

	public float addWeightMin = 0.5f;

	public float addWeightMax = 0.75f;

	public float reactionWeight;

	[Header("Anim ChangeWaitTime")]
	public float changeWaitTime = 1f;

	public float changeWaitTimeMax = 1f;

	[Header("Anim FeedTime")]
	public float feedTime = 0.5f;

	[Header("Layer")]
	public AnimancerLayer animancerLayer2;

	[Header("Animation")]
	public int poseType;

	public int normalPose = 8;

	[Header("Animation Mixer")]
	public Vector2 poseParam;

	public List<Vector2> mixParam;

	private DirectionalMixerState poseMixer;

	private DirectionalMixerState poseMixer2;

	[Header("Animation Layer2")]
	public bool reactionSwitch;

	public int reactionCount;

	public float reactionData;

	public float reactionData2;

	public AvatarMask _avatarMask;

	private LinearMixerState reactionMixer;

	private LinearMixerState reactionMixer2;

	public List<AnimationClip> idleClip;

	public List<AnimationClip> reactionClip;

	public List<AnimationClip> reactionClipA;

	public List<AnimationClip> reactionClipAR;

	public List<AnimationClip> reactionClipB;

	public List<AnimationClip> reactionClipBR;

	[Space]
	public List<AnimationClip> orgasmClipA;

	public List<AnimationClip> orgasmClipB;

	private AnimancerState _state;

	private AnimancerState _state2;

	private AnimancerState _stateOld;

	private AnimancerState _stateOld2;

	private AnimancerState _stateIdle;

	private AnimancerState _stateIdleOld;

	private AnimancerState _stateIdleOld2;

	[ContextMenu("Set Sort AnimationList")]
	public void SortAnimationList()
	{
		idleClip = SortClipsByPrefix(idleClip);
		reactionClip = SortClipsByPrefix(reactionClip);
		reactionClipA = SortClipsByPrefix(reactionClipA);
		reactionClipAR = SortClipsByPrefix(reactionClipAR);
		reactionClipB = SortClipsByPrefix(reactionClipB);
		reactionClipBR = SortClipsByPrefix(reactionClipBR);
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
		if (idleClip[poseType] != null)
		{
			animancerLayer2 = _animancer.Layers[2];
			animancerLayer2.IsAdditive = true;
			animancerLayer2.Weight = 0f;
			animancerLayer2.Play(idleClip[poseType], 0.25f);
			animancerLayer2.Mask = _avatarMask;
			animancerLayer2.ApplyFootIK = true;
		}
		normalPose = _motionAnimancer.normalPose;
		mixParam = _motionAnimancer.mixParam;
		reactionSwitch = false;
		reactionMixer = new LinearMixerState();
		reactionMixer.Add(reactionClipA[0], 0f);
		reactionMixer.Add(reactionClipB[0], 0.25f);
		reactionMixer.Add(reactionClip[0], 0.5f);
		reactionMixer.Add(reactionClipBR[0], 0.75f);
		reactionMixer.Add(reactionClipAR[0], 1f);
		reactionMixer2 = new LinearMixerState();
		reactionMixer2.Add(reactionClipB[0], 0f);
		reactionMixer2.Add(reactionClipA[0], 0.25f);
		reactionMixer2.Add(reactionClip[0], 0.5f);
		reactionMixer2.Add(reactionClipAR[0], 0.75f);
		reactionMixer2.Add(reactionClipBR[0], 1f);
	}

	public DirectionalMixerState SetMixer()
	{
		if (reactionCount > 5)
		{
			reactionCount = 0;
		}
		if (!reactionSwitch)
		{
			reactionSwitch = true;
			if (poseMixer != null)
			{
				poseMixer.Destroy();
			}
			poseMixer = new DirectionalMixerState();
			for (int i = 0; i < mixParam.Count; i++)
			{
				if (reactionCount == 0)
				{
					poseMixer.Add(reactionClip[i], mixParam[i]);
				}
				else if (reactionCount == 1)
				{
					poseMixer.Add(reactionClipA[i], mixParam[i]);
				}
				else if (reactionCount == 2)
				{
					poseMixer.Add(reactionClipBR[i], mixParam[i]);
				}
				else if (reactionCount == 3)
				{
					poseMixer.Add(reactionClip[i], mixParam[i]);
				}
				else if (reactionCount == 4)
				{
					poseMixer.Add(reactionClipAR[i], mixParam[i]);
				}
				else if (reactionCount == 5)
				{
					poseMixer.Add(reactionClipB[i], mixParam[i]);
				}
			}
			poseMixer.Parameter = poseParam;
			reactionCount++;
			return poseMixer;
		}
		reactionSwitch = false;
		if (poseMixer2 != null)
		{
			poseMixer2.Destroy();
		}
		poseMixer2 = new DirectionalMixerState();
		for (int j = 0; j < mixParam.Count; j++)
		{
			if (reactionCount == 0)
			{
				poseMixer2.Add(reactionClip[j], mixParam[j]);
			}
			else if (reactionCount == 1)
			{
				poseMixer2.Add(reactionClipBR[j], mixParam[j]);
			}
			else if (reactionCount == 2)
			{
				poseMixer2.Add(reactionClipA[j], mixParam[j]);
			}
			else if (reactionCount == 3)
			{
				poseMixer2.Add(reactionClip[j], mixParam[j]);
			}
			else if (reactionCount == 4)
			{
				poseMixer2.Add(reactionClipB[j], mixParam[j]);
			}
			else if (reactionCount == 5)
			{
				poseMixer2.Add(reactionClipBR[j], mixParam[j]);
			}
		}
		poseMixer2.Parameter = poseParam;
		reactionCount++;
		return poseMixer2;
	}

	private void LateUpdate()
	{
		if (targetWeight > currentWeight)
		{
			currentWeight += Time.deltaTime;
			if (targetWeight < currentWeight)
			{
				currentWeight = targetWeight;
			}
		}
		else if (targetWeight < currentWeight)
		{
			currentWeight -= Time.deltaTime;
			if (targetWeight > currentWeight)
			{
				currentWeight = targetWeight;
			}
		}
		if (currentTime < maxTime)
		{
			currentTime += Time.deltaTime;
			if (currentTime > maxTime)
			{
				currentTime = maxTime;
			}
			calcTime = currentTime / maxTime;
		}
		animancerLayer2.Weight = currentWeight;
		if (changeWaitTime > 0f)
		{
			changeWaitTime -= Time.deltaTime;
		}
	}

	public void PoseChange(int value, bool expend)
	{
		if (!expend)
		{
			poseType = value;
		}
		else if (expend && value == -20)
		{
			poseType = idleClip.Count - 12;
		}
		else if (expend && value == -21)
		{
			poseType = idleClip.Count - 11;
		}
		else if (expend && value == -22)
		{
			poseType = idleClip.Count - 10;
		}
		else if (expend && value == -23)
		{
			poseType = idleClip.Count - 9;
		}
		else if (expend && value == -24)
		{
			poseType = idleClip.Count - 8;
		}
		else if (expend && value == -25)
		{
			poseType = idleClip.Count - 7;
		}
		else if (expend && value == -30)
		{
			poseType = idleClip.Count - 6;
		}
		else if (expend && value == -31)
		{
			poseType = idleClip.Count - 5;
		}
		else if (expend && value == -32)
		{
			poseType = idleClip.Count - 4;
		}
		else if (expend && value == -33)
		{
			poseType = idleClip.Count - 3;
		}
		else if (expend && value == -34)
		{
			poseType = idleClip.Count - 2;
		}
		else if (expend && value == -35)
		{
			poseType = idleClip.Count - 1;
		}
		ReactionSet("isIdle", 0.25f);
	}

	public void PoseChangeCountUp()
	{
		poseType++;
		if (poseType >= idleClip.Count)
		{
			poseType = 0;
		}
		ReactionSet(ReactionName, 0.25f);
	}

	public void RefreshReaction()
	{
		reactionSwitch = !reactionSwitch;
		if (!reactionSwitch)
		{
			if (reactionMixer != null)
			{
				reactionMixer.Destroy();
			}
			reactionMixer = new LinearMixerState();
			reactionMixer.Add(reactionClipA[poseType], 0f);
			reactionMixer.Add(reactionClipB[poseType], 0.25f);
			reactionMixer.Add(reactionClip[poseType], 0.5f);
			reactionMixer.Add(reactionClipBR[poseType], 0.75f);
			reactionMixer.Add(reactionClipAR[poseType], 1f);
		}
		else if (reactionSwitch)
		{
			if (reactionMixer2 != null)
			{
				reactionMixer2.Destroy();
			}
			reactionMixer2 = new LinearMixerState();
			reactionMixer2.Add(reactionClipB[poseType], 0f);
			reactionMixer2.Add(reactionClipA[poseType], 0.25f);
			reactionMixer2.Add(reactionClip[poseType], 0.5f);
			reactionMixer2.Add(reactionClipAR[poseType], 0.75f);
			reactionMixer2.Add(reactionClipBR[poseType], 1f);
		}
	}

	public void RefreshOrgasm()
	{
		reactionSwitch = !reactionSwitch;
		if (!reactionSwitch)
		{
			if (reactionMixer != null)
			{
				reactionMixer.Destroy();
			}
			reactionMixer = new LinearMixerState();
			reactionMixer.Add(orgasmClipA[poseType], 0f);
			reactionMixer.Add(orgasmClipA[poseType], 1f);
		}
		else if (reactionSwitch)
		{
			if (reactionMixer2 != null)
			{
				reactionMixer2.Destroy();
			}
			reactionMixer2 = new LinearMixerState();
			reactionMixer2.Add(orgasmClipB[poseType], 0f);
			reactionMixer2.Add(orgasmClipB[poseType], 1f);
		}
	}

	public void ReactionSet(string value, float feed)
	{
		feedTime = feed;
		ReactionName = value;
		if (ReactionName == "isIdle" && idleClip[poseType] != null)
		{
			_stateIdle = animancerLayer2.Play(idleClip[poseType], feedTime);
		}
		else if (ReactionName == "isReaction" && reactionClipA[poseType] != null)
		{
			if (!isReactionForce && changeWaitTime < 0f)
			{
				changeWaitTime = changeWaitTimeMax;
				RefreshReaction();
				if (!reactionSwitch && !isReaction)
				{
					isReaction = true;
					maxTime = reactionClipA[poseType].length;
					SetAnimation();
				}
				else if (reactionSwitch && !isReaction2)
				{
					isReaction2 = true;
					maxTime = reactionClipA[poseType].length;
					SetAnimation2();
				}
			}
			else if (isReactionForce)
			{
				changeWaitTime = changeWaitTimeMax;
				RefreshReaction();
				if (!reactionSwitch)
				{
					maxTime = reactionClipA[poseType].length;
					SetAnimation();
				}
				else if (reactionSwitch)
				{
					maxTime = reactionClipA[poseType].length;
					SetAnimation2();
				}
			}
		}
		else if (ReactionName == "isOrgasm" && orgasmClipA[poseType] != null)
		{
			changeWaitTime = changeWaitTimeMax;
			RefreshOrgasm();
			if (!reactionSwitch && !isReaction)
			{
				isReaction = true;
				maxTime = orgasmClipA[poseType].length;
				SetAnimation();
			}
			else if (reactionSwitch && !isReaction2)
			{
				isReaction2 = true;
				maxTime = orgasmClipA[poseType].length;
				SetAnimation2();
			}
		}
		else
		{
			Debug.LogError("Animation Reaction nothing! : " + value, base.gameObject);
		}
	}

	public void SetAnimation()
	{
		reactionData = Random.Range(0f, 1f);
		reactionMixer.Parameter = reactionData;
		currentTime = 0f;
		isReaction2 = false;
		targetWeight += Random.Range(addWeightMin, addWeightMax);
		if (targetWeight > 1f)
		{
			targetWeight = 1f;
		}
		if (poseType < normalPose)
		{
			SetMixer();
			_state = animancerLayer2.Play(poseMixer, feedTime);
		}
		else
		{
			_state = animancerLayer2.Play(reactionMixer, feedTime);
		}
		_state.Events(this).OnEnd = delegate
		{
			animancerLayer2.Play(idleClip[poseType], 0.25f);
			targetWeight = 0f;
			ReactionName = "isIdle";
			isReaction = false;
		};
	}

	public void SetAnimation2()
	{
		reactionData2 = Random.Range(0f, 1f);
		reactionMixer2.Parameter = reactionData2;
		currentTime = 0f;
		isReaction = false;
		targetWeight += Random.Range(addWeightMin, addWeightMax);
		if (targetWeight > 1f)
		{
			targetWeight = 1f;
		}
		if (poseType < normalPose)
		{
			SetMixer();
			_state2 = animancerLayer2.Play(poseMixer2, feedTime);
		}
		else
		{
			_state2 = animancerLayer2.Play(reactionMixer2, feedTime);
		}
		_state2.Events(this).OnEnd = delegate
		{
			animancerLayer2.Play(idleClip[poseType], 0.25f);
			targetWeight = 0f;
			ReactionName = "isIdle";
			isReaction2 = false;
		};
	}
}
