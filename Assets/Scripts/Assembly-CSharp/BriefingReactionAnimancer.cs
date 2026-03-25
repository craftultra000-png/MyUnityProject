using System.Collections.Generic;
using System.Linq;
using Animancer;
using UnityEngine;

public class BriefingReactionAnimancer : MonoBehaviour
{
	public AnimancerComponent _animancer;

	public BriefingCaracterAnimancer _briefingCaracterAnimancer;

	[Header("Status")]
	public bool isReaction;

	public bool isReaction2;

	public bool isReactionForce;

	public string ReactionName = "isIdle";

	public bool changePose;

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
	public AnimancerLayer animancerLayer1;

	[Header("Animation")]
	public int poseType;

	[Header("Animation Layer2")]
	public bool reactionSwitch;

	public int reactionCount;

	public float reactionData;

	public float reactionData2;

	private LinearMixerState reactionMixer;

	private LinearMixerState reactionMixer2;

	public List<AnimationClip> idleClip;

	public List<AnimationClip> reactionClipA;

	public List<AnimationClip> reactionClipB;

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
		reactionClipA = SortClipsByPrefix(reactionClipA);
		reactionClipB = SortClipsByPrefix(reactionClipB);
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
			animancerLayer1 = _animancer.Layers[1];
			animancerLayer1.IsAdditive = true;
			animancerLayer1.Weight = 0f;
			animancerLayer1.Play(idleClip[poseType], 0.25f);
			animancerLayer1.ApplyFootIK = true;
		}
		reactionSwitch = false;
		reactionMixer = new LinearMixerState();
		reactionMixer.Add(reactionClipA[0], 0f);
		reactionMixer.Add(reactionClipB[0], 1f);
		reactionMixer2 = new LinearMixerState();
		reactionMixer2.Add(reactionClipB[0], 0f);
		reactionMixer2.Add(reactionClipA[0], 1f);
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
		animancerLayer1.Weight = currentWeight;
		if (changeWaitTime > 0f)
		{
			changeWaitTime -= Time.deltaTime;
		}
	}

	public void PoseChange(int value)
	{
		poseType = value;
		ReactionSet("isIdle", 0.25f);
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
			reactionMixer.Add(reactionClipB[poseType], 1f);
		}
		else if (reactionSwitch)
		{
			if (reactionMixer2 != null)
			{
				reactionMixer2.Destroy();
			}
			reactionMixer2 = new LinearMixerState();
			reactionMixer2.Add(reactionClipB[poseType], 0f);
			reactionMixer2.Add(reactionClipA[poseType], 1f);
		}
	}

	public void ReactionSet(string value, float feed)
	{
		if (changePose)
		{
			return;
		}
		feedTime = feed;
		ReactionName = value;
		if (ReactionName == "isIdle" && idleClip[poseType] != null)
		{
			_stateIdle = animancerLayer1.Play(idleClip[poseType], feedTime);
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
		_state = animancerLayer1.Play(reactionMixer, feedTime);
		_state.Events(this).OnEnd = delegate
		{
			animancerLayer1.Play(idleClip[poseType], 0.25f);
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
		_state2 = animancerLayer1.Play(reactionMixer2, feedTime);
		_state2.Events(this).OnEnd = delegate
		{
			animancerLayer1.Play(idleClip[poseType], 0.25f);
			targetWeight = 0f;
			ReactionName = "isIdle";
			isReaction2 = false;
		};
	}
}
