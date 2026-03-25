using Animancer;
using UnityEngine;

public class ShareAnimancer : MonoBehaviour
{
	public AnimancerComponent _animancer;

	[Header("Status")]
	public string StateName = "Idle";

	public bool isEndWait;

	[Header("Anim Speed")]
	public float animationSpeed;

	[Header("Anim FeedTime")]
	public float feedTime = 0.5f;

	[Header("Animation Clip")]
	public AnimationClip tPose;

	public AnimationClip idleClip;

	public AnimationClip rampageClip;

	public AnimationClip insertClip;

	public AnimationClip pistonClip;

	public AnimationClip shotClip;

	public AnimationClip shotIdleClip;

	private AnimancerState _state;

	private void Start()
	{
		_state = _animancer.Play(idleClip);
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
		Debug.Log(base.gameObject.name + " StateSet:" + value, base.gameObject);
		feedTime = feed;
		StateName = value;
		if (isEndWait || StateName == "isSkip")
		{
			return;
		}
		if (StateName == "isIdle" && idleClip != null)
		{
			if (_state != null)
			{
				_state.Events(this).OnEnd = null;
			}
			_state = _animancer.Play(idleClip, feed);
			_state.Speed = 1f;
		}
		else if (StateName == "isRampage" && rampageClip != null)
		{
			_state = _animancer.Play(rampageClip, feedTime);
			_state.Speed = 1f;
			_state.Events(this).OnEnd = delegate
			{
				_animancer.Play(idleClip, 0.25f);
			};
		}
		else if (StateName == "isInsert" && insertClip != null)
		{
			if (_state != null)
			{
				_state.Events(this).OnEnd = null;
			}
			_state = _animancer.Play(insertClip, feedTime);
			_state.Speed = animationSpeed;
		}
		else if (StateName == "isPiston" && pistonClip != null)
		{
			if (_state != null)
			{
				_state.Events(this).OnEnd = null;
			}
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
				_animancer.Play(shotIdleClip, 0.25f);
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
