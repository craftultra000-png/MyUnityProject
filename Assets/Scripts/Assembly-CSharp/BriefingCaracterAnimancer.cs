using System.Collections.Generic;
using Animancer;
using UnityEngine;

public class BriefingCaracterAnimancer : MonoBehaviour
{
	public AnimancerComponent _animancer;

	public CharacterAnimation _characterAnimation;

	public CharacterFacialManager _characterFacialManager;

	public CharacterMouthManager _characterMouthManager;

	public BriefingReactionAnimancer _briefingReactionAnimancer;

	[Header("Animation Data")]
	public string currentState;

	public string previousState;

	public float feedTime = 0.25f;

	[Header("Pose Data")]
	public int poseType;

	public int previousPoseType;

	public int PoseSortType;

	public bool changePose;

	public List<int> poseSort;

	[Header("Animation Clip")]
	public AnimationClip idleClip;

	public List<AnimationClip> masturbationClip;

	public List<AnimationClip> masturbationHighClip;

	public List<AnimationClip> masturbationFinishClip;

	public List<AnimationClip> masturbationFinishIdleClip;

	public List<AnimationClip> masturbationChangeStartClip;

	public List<AnimationClip> masturbationChangeEndClip;

	private AnimancerState _state;

	private List<LinearMixerState> masturbationSpeedMixer = new List<LinearMixerState>();

	[Header("GUI")]
	public ButtonTriggerGUI poseButton;

	public ButtonTriggerGUI masturbationButton;

	public ButtonTriggerGUI finishButton;

	public Color enableColor;

	public Color disableColor;

	[Header("Anim Speed")]
	public float animationSpeed;

	[Header("Anim FeedTime")]
	public float pistonMixParameter;

	private void Start()
	{
		_animancer.Layers[0].ApplyFootIK = true;
		for (int i = 0; i < masturbationClip.Count; i++)
		{
			LinearMixerState linearMixerState = new LinearMixerState();
			linearMixerState.Add(masturbationClip[i], 0f);
			linearMixerState.Add(masturbationHighClip[i], 1f);
			masturbationSpeedMixer.Add(linearMixerState);
			masturbationSpeedMixer[i].Parameter = pistonMixParameter;
		}
		_characterMouthManager.useBreath = true;
		PoseSortType = Random.Range(0, poseSort.Count);
		previousPoseType = PoseSortType;
		poseType = poseSort[PoseSortType];
		SetAnimationClip("Masturbation");
		SetFacial();
		ChangeButtonColor();
	}

	public void ChangeButtonColor()
	{
		if (currentState == "Masturbation")
		{
			masturbationButton.defaultColor = enableColor;
			finishButton.defaultColor = disableColor;
			finishButton.ColorReset();
		}
		else if (currentState == "Finish")
		{
			masturbationButton.defaultColor = disableColor;
			finishButton.defaultColor = enableColor;
			masturbationButton.ColorReset();
		}
	}

	public void ButtonLock(bool value)
	{
		if (value)
		{
			poseButton.unuse = false;
			poseButton.ColorReset();
			masturbationButton.unuse = false;
			masturbationButton.ColorReset();
			finishButton.unuse = false;
			finishButton.ColorReset();
		}
		else
		{
			poseButton.unuse = true;
			poseButton.Unuse();
			masturbationButton.unuse = true;
			masturbationButton.Unuse();
			finishButton.unuse = true;
			finishButton.Unuse();
		}
	}

	public void ChangeAnimationSpeed(float value)
	{
		animationSpeed = value;
		if (currentState == "Masturbation" || currentState == "Change")
		{
			_state.Speed = animationSpeed;
		}
		for (int i = 0; i < masturbationSpeedMixer.Count; i++)
		{
			masturbationSpeedMixer[i].Parameter = pistonMixParameter;
		}
	}

	public void SetPose(int value)
	{
		if (value != previousPoseType)
		{
			previousPoseType = poseType;
			if (poseType == 0)
			{
				poseType = 1;
			}
			else
			{
				poseType = 0;
			}
			changePose = true;
			_briefingReactionAnimancer.changePose = true;
			SetAnimationClip("Change");
		}
	}

	public void SetPose()
	{
		PoseSortType++;
		if (PoseSortType >= poseSort.Count)
		{
			PoseSortType = 0;
		}
		previousPoseType = poseType;
		poseType = poseSort[PoseSortType];
		changePose = true;
		_briefingReactionAnimancer.changePose = true;
		SetAnimationClip("Change");
	}

	public void SetAnimationClip(string value)
	{
		Debug.LogError("Set Animation: " + value);
		currentState = value;
		if (currentState == "Idle")
		{
			previousState = currentState;
			_state = _animancer.Play(idleClip);
			_state.Speed = 1f;
		}
		else if (currentState == "Masturbation" && (previousState != currentState || changePose))
		{
			previousState = currentState;
			_state = _animancer.Play(masturbationSpeedMixer[poseType], feedTime);
			_state.Speed = animationSpeed;
			ChangeButtonColor();
			ButtonLock(value: true);
		}
		else if (currentState == "Finish" && previousState != currentState)
		{
			previousState = currentState;
			_state = _animancer.Play(masturbationFinishClip[poseType], feedTime);
			_state.Speed = 1f;
			ChangeButtonColor();
			ButtonLock(value: false);
			_state.Events(this).OnEnd = delegate
			{
				previousState = "FinishIdle";
				_state = _animancer.Play(masturbationFinishIdleClip[poseType], 0f);
				_state.Speed = 1f;
				ButtonLock(value: true);
			};
		}
		else if (currentState == "FinishIdle" && (previousState != currentState || changePose))
		{
			previousState = currentState;
			_state = _animancer.Play(masturbationFinishIdleClip[poseType], feedTime);
			_state.Speed = 1f;
			ChangeButtonColor();
			ButtonLock(value: true);
		}
		else if (currentState == "Change")
		{
			_state = _animancer.Play(masturbationChangeStartClip[previousPoseType], feedTime);
			_state.Speed = 1f;
			ChangeButtonColor();
			ButtonLock(value: false);
		}
	}

	public void PoseChangeEnd()
	{
		_state = _animancer.Play(masturbationChangeEndClip[poseType], feedTime);
		_state.Speed = 1f;
		_state.Events(this).OnEnd = delegate
		{
			previousState = "Masturbation";
			SetAnimationClip(previousState);
			ButtonLock(value: true);
			changePose = false;
			_briefingReactionAnimancer.changePose = false;
		};
	}

	public void SetMasturbation()
	{
		_animancer.Play(masturbationSpeedMixer[poseType], feedTime);
	}

	public void SetFacial()
	{
		_characterFacialManager.isBottom = true;
		_characterFacialManager.isEyesClose02 = true;
	}

	public void RandomSetFacial()
	{
		if (Random.Range(0, 4) == 0)
		{
			_characterFacialManager.isBottom = false;
			_characterFacialManager.isAhe = false;
			_characterFacialManager.isWinkL = false;
			_characterFacialManager.isWinkR = false;
			_characterFacialManager.isEyesClose01 = false;
			_characterFacialManager.isEyesClose02 = false;
			_characterFacialManager.isEyesClose03 = false;
			switch (Random.Range(0, 10))
			{
			case 0:
				_characterFacialManager.isBottom = true;
				_characterFacialManager.isEyesClose01 = true;
				break;
			case 1:
				_characterFacialManager.isBottom = true;
				_characterFacialManager.isEyesClose01 = true;
				break;
			case 2:
				_characterFacialManager.isBottom = true;
				_characterFacialManager.isEyesClose01 = true;
				break;
			case 3:
				_characterFacialManager.isBottom = true;
				_characterFacialManager.isEyesClose01 = true;
				break;
			case 4:
				_characterFacialManager.isBottom = true;
				_characterFacialManager.isEyesClose02 = true;
				break;
			case 5:
				_characterFacialManager.isBottom = true;
				_characterFacialManager.isEyesClose02 = true;
				break;
			case 6:
				_characterFacialManager.isAhe = true;
				_characterFacialManager.isEyesClose03 = true;
				break;
			case 7:
				_characterFacialManager.isBottom = true;
				_characterFacialManager.isWinkL = true;
				_characterFacialManager.isEyesClose02 = true;
				break;
			case 8:
				_characterFacialManager.isBottom = true;
				_characterFacialManager.isWinkR = true;
				_characterFacialManager.isEyesClose03 = true;
				break;
			case 9:
				_characterFacialManager.isBottom = true;
				_characterFacialManager.isWinkL = true;
				_characterFacialManager.isWinkR = true;
				break;
			}
		}
	}

	public void RandomOrgasmSetFacial()
	{
		_characterFacialManager.isBottom = false;
		_characterFacialManager.isAhe = false;
		_characterFacialManager.isWinkL = false;
		_characterFacialManager.isWinkR = false;
		_characterFacialManager.isEyesClose01 = false;
		_characterFacialManager.isEyesClose02 = false;
		_characterFacialManager.isEyesClose03 = false;
		switch (Random.Range(0, 5))
		{
		case 0:
			_characterFacialManager.isAhe = true;
			_characterFacialManager.isEyesClose01 = true;
			break;
		case 1:
			_characterFacialManager.isAhe = true;
			_characterFacialManager.isEyesClose02 = true;
			break;
		case 2:
			_characterFacialManager.isAhe = true;
			_characterFacialManager.isEyesClose03 = true;
			break;
		case 3:
			_characterFacialManager.isWinkL = true;
			_characterFacialManager.isEyesClose02 = true;
			break;
		case 4:
			_characterFacialManager.isWinkR = true;
			_characterFacialManager.isEyesClose03 = true;
			break;
		}
	}

	public void RandomOrgasmIdleSetFacial()
	{
		if (Random.Range(0, 4) == 0)
		{
			_characterFacialManager.isBottom = false;
			_characterFacialManager.isAhe = false;
			_characterFacialManager.isWinkL = false;
			_characterFacialManager.isWinkR = false;
			_characterFacialManager.isEyesClose01 = false;
			_characterFacialManager.isEyesClose02 = false;
			_characterFacialManager.isEyesClose03 = false;
			switch (Random.Range(0, 5))
			{
			case 0:
				_characterFacialManager.isBottom = true;
				_characterFacialManager.isEyesClose02 = true;
				break;
			case 1:
				_characterFacialManager.isBottom = true;
				_characterFacialManager.isEyesClose03 = true;
				break;
			case 2:
				_characterFacialManager.isWinkL = true;
				_characterFacialManager.isEyesClose02 = true;
				break;
			case 3:
				_characterFacialManager.isWinkR = true;
				_characterFacialManager.isEyesClose03 = true;
				break;
			case 4:
				_characterFacialManager.isWinkL = true;
				_characterFacialManager.isWinkR = true;
				break;
			}
		}
	}
}
