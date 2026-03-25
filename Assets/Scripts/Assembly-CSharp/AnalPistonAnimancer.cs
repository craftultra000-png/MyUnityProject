using Animancer;
using UnityEngine;

public class AnalPistonAnimancer : MonoBehaviour
{
	public AnimancerComponent _animancer;

	public AnalAnimation _analAnimation;

	[Header("Paramater")]
	public AnimationClip coreAnim;

	[Range(0f, 1f)]
	public float param;

	private AnimancerState _animancerState;

	private void Start()
	{
		_animancerState = _animancer.Play(coreAnim);
		_animancerState.IsPlaying = false;
	}

	private void FixedUpdate()
	{
		param = _analAnimation.param;
		float length = coreAnim.length;
		float num = Mathf.Clamp(param, 0f, 1f);
		_animancerState.Time = num * length;
	}
}
