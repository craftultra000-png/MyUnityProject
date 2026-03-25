using System.Collections.Generic;
using Animancer;
using UnityEngine;

public class PistonAnimancer : MonoBehaviour
{
	public AnimancerComponent _animancer;

	public CharacterLifeManager _characterLifeManager;

	[Header("Type")]
	public bool vagina;

	public bool anal;

	public bool mouth;

	public bool tits;

	[Header("Status")]
	public string StateName = "Idle";

	public bool isEndWait;

	[Header("Anim Speed")]
	public float animationSpeed;

	[Header("Anim FeedTime")]
	public float feedTime = 0.5f;

	public float idleMixParameter;

	[Header("Animation Clip")]
	public AnimationClip tPose;

	public List<AnimationClip> idleClip;

	public AnimationClip rampageClip;

	public AnimationClip insertClip;

	public AnimationClip pistonClip;

	public AnimationClip shotClip;

	public AnimationClip shotIdleClip;

	public AnimationClip conceiveClip;

	private LinearMixerState idleMixer;

	private AnimancerState _state;

	private void Start()
	{
		idleMixer = new LinearMixerState();
		idleMixer.Add(idleClip[0], 0f);
		idleMixer.Add(idleClip[1], 0.5f);
		idleMixer.Add(idleClip[2], 1f);
		idleMixParameter = Random.Range(0f, 1f);
		idleMixer.Parameter = idleMixParameter;
		_state = _animancer.Play(idleMixer);
		_state.Speed = 1f;
	}

	public void ChangeAnimationSpeed(float value)
	{
		animationSpeed = value;
		if (StateName == "isPiston")
		{
			_state.Speed = animationSpeed;
		}
	}

	public void StateSet(string value, float feed)
	{
		feedTime = feed;
		StateName = value;
		if (isEndWait || StateName == "isSkip")
		{
			return;
		}
		if (StateName == "isIdle" && idleClip != null)
		{
			idleMixParameter = Random.Range(0f, 1f);
			idleMixer.Parameter = idleMixParameter;
			_state = _animancer.Play(idleMixer, 0.25f);
			_state.Speed = 1f;
		}
		else if (StateName == "isRampage" && rampageClip != null)
		{
			_state = _animancer.Play(rampageClip, feedTime);
			_state.Speed = 1f;
			_state.Events(this).OnEnd = delegate
			{
				idleMixParameter = Random.Range(0f, 1f);
				idleMixer.Parameter = idleMixParameter;
				_state = _animancer.Play(idleMixer, 0.25f);
				_state.Speed = 1f;
			};
		}
		else if (StateName == "isInsert" && insertClip != null)
		{
			_state = _animancer.Play(insertClip, feedTime);
			_state.Speed = animationSpeed;
		}
		else if (StateName == "isPiston" && pistonClip != null)
		{
			_state = _animancer.Play(pistonClip, feedTime);
			_state.Speed = animationSpeed;
		}
		else if (StateName == "isShot" && shotClip != null)
		{
			isEndWait = true;
			_state = _animancer.Play(shotClip, feedTime);
			_state.Speed = 1f;
			_state.Events(this).OnEnd = delegate
			{
				_state = _animancer.Play(shotIdleClip, 0.25f);
				_state.Speed = 1f;
				isEndWait = false;
			};
		}
		else if (StateName == "isShotIdle" && shotIdleClip != null)
		{
			_state = _animancer.Play(shotIdleClip, feedTime);
			_state.Speed = 1f;
		}
	}
}
