using Animancer;
using UnityEngine;

public class GuildTongueObject : MonoBehaviour
{
	public AnimancerComponent _animancer;

	[Header("Animation Data")]
	public string currentState;

	public float feedTime = 0.25f;

	[Header("Animation Clip")]
	public AnimationClip idleClip;

	public AnimationClip eatClip;

	private AnimancerState _state;

	private void Start()
	{
		SetAnimationClip("Idle");
	}

	public void SetAnimationClip(string value)
	{
		currentState = value;
		if (currentState == "Idle")
		{
			_state = _animancer.Play(idleClip);
		}
		else if (currentState == "Eat")
		{
			_state = _animancer.Play(eatClip, feedTime);
		}
	}
}
