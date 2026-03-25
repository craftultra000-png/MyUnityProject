using Animancer;
using UnityEngine;

public class GuildCaracterAnimancer : MonoBehaviour
{
	public AnimancerComponent _animancer;

	[Header("Animation Data")]
	public string currentState;

	public float feedTime = 0.25f;

	[Header("Animation Clip")]
	public AnimationClip idleClip;

	public AnimationClip lookAwayClip;

	public AnimationClip masturbationStartClip;

	public AnimationClip masturbationIdleClip;

	public AnimationClip lookFrontClip;

	public AnimationClip handDownClip;

	public AnimationClip eatClip;

	private AnimancerState _state;

	private void Start()
	{
	}

	public void SetAnimationClip(string value)
	{
		currentState = value;
		if (currentState == "Idle")
		{
			_state = _animancer.Play(idleClip);
		}
		else if (currentState == "LookAway")
		{
			_state = _animancer.Play(lookAwayClip);
			_state.Events(this).OnEnd = delegate
			{
				_state = _animancer.Play(idleClip, 0f);
			};
		}
		else if (currentState == "Masturbation")
		{
			_state = _animancer.Play(masturbationStartClip, feedTime);
			_state.Events(this).OnEnd = delegate
			{
				_state = _animancer.Play(masturbationIdleClip, 0f);
			};
		}
		else if (currentState == "LookFront")
		{
			_state = _animancer.Play(lookFrontClip, 0.5f);
		}
		else if (currentState == "HandDown")
		{
			_state = _animancer.Play(handDownClip, 0.5f);
		}
		else if (currentState == "Eat")
		{
			_state = _animancer.Play(eatClip, 1f);
		}
	}

	public void SetMasturbation()
	{
		_state = _animancer.Play(masturbationIdleClip, 0.25f);
	}
}
