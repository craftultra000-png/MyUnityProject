using System;
using System.Collections.Generic;
using Animancer;
using UnityEngine;

public class CharacterAnimancer : MonoBehaviour
{
	public AnimancerComponent _animancer;

	[Header("Status")]
	public string StateName = "Idle";

	public bool isEndWait;

	[Header("Anim FeedTime")]
	public float feedTime = 0.5f;

	[Header("Animation Clip")]
	public AnimationClip tPose;

	public List<AnimationClip> listCiip;

	public AnimationClip idleClip;

	public AnimationClip rampageClip;

	public AnimationClip pitonClip;

	public AnimationClip shotClip;

	public AnimationClip shotIdleClip;

	public AnimationClip conceiveClip;

	private void Start()
	{
		_animancer.Play(idleClip);
	}

	public void StateSet(string value, float feed)
	{
		Debug.Log(base.gameObject.name + " StateSet:" + value, base.gameObject);
		feedTime = feed;
		StateName = value;
		if (isEndWait || StateName == "isSkip")
		{
			return;
		}
		if (StateName == "isIdle")
		{
			_animancer.Play(idleClip, feed);
		}
		else if (StateName == "isRampage")
		{
			_animancer.Play(rampageClip, feedTime).Events(this).OnEnd = delegate
			{
				_animancer.Play(idleClip, 0.25f);
			};
		}
		else if (StateName == "isPiston")
		{
			_animancer.Play(pitonClip, feedTime);
		}
		else if (StateName == "isShot")
		{
			isEndWait = true;
			AnimancerState animancerState = _animancer.Play(shotClip, feedTime);
			animancerState.Events(this).OnEnd = delegate
			{
				_animancer.Play(shotIdleClip, 0.25f);
			};
			ref Action onEnd = ref animancerState.Events(this).OnEnd;
			onEnd = (Action)Delegate.Combine(onEnd, (Action)delegate
			{
				isEndWait = false;
			});
		}
		else if (StateName == "isShotIdle")
		{
			_animancer.Play(shotIdleClip, feedTime);
		}
		else if (StateName == "isConceive")
		{
			_animancer.Play(conceiveClip, feedTime);
		}
	}
}
