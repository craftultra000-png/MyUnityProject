using UnityEngine;
using UnityEngine.UI;

public class BriefingAnimancerSpeedManager : MonoBehaviour
{
	public BriefingCaracterAnimancer _briefingCaracterAnimancer;

	[Header("Volume Setting")]
	public float masturbationSpeed;

	public AnimationCurve masturbation;

	public Slider masturbationSlider;

	private void Start()
	{
		masturbationSpeed = 0f;
		masturbationSlider.value = masturbationSpeed;
		ChangeMasturbationSpeed(masturbationSpeed);
	}

	public void ChangeMasturbation()
	{
		masturbationSpeed = masturbationSlider.value;
		ChangeMasturbationSpeed(masturbationSpeed);
	}

	public void ChangeMasturbationSpeed(float value)
	{
		masturbationSpeed = value;
		float value2 = masturbation.Evaluate(masturbationSpeed);
		_briefingCaracterAnimancer.pistonMixParameter = masturbationSpeed;
		_briefingCaracterAnimancer.ChangeAnimationSpeed(value2);
	}
}
