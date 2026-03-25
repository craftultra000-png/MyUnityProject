using System.Collections.Generic;
using UnityEngine;

public class AnimMakeSupporter : MonoBehaviour
{
	public Animator _animator;

	[Header("Anim Data")]
	public float animTime;

	public float currentTime;

	public float endTime;

	[Header("Anim Button Filter")]
	public string filterName = "";

	public bool atk;

	public bool def;

	public int layerIndex;

	[Header("Anim Name")]
	public string clipName = "T-Pose";

	public AnimationClip currentClip;

	public AnimationClip resetClip;

	public int layer;

	public string resetClipName = "T-Pose";

	[Header("Anim Status")]
	public bool isPlay;

	public bool isLoop;

	[Header("Anim Status")]
	public int animationCount;

	public List<string> animationNameList;

	public List<AnimationClip> animationList;

	public void SetUpdate()
	{
		if (!Application.isPlaying && isPlay)
		{
			Vector3 localPosition = base.transform.localPosition;
			Quaternion localRotation = base.transform.localRotation;
			currentClip.SampleAnimation(base.gameObject, currentTime);
			base.transform.localPosition = localPosition;
			base.transform.localRotation = localRotation;
			isPlay = false;
			_animator.enabled = true;
		}
	}

	public void SetPose()
	{
		isPlay = true;
		_animator.enabled = false;
		endTime = currentClip.length;
		currentTime = animTime;
		Vector3 localPosition = base.transform.localPosition;
		Quaternion localRotation = base.transform.localRotation;
		currentClip.SampleAnimation(base.gameObject, currentTime);
		base.transform.localPosition = localPosition;
		base.transform.localRotation = localRotation;
	}

	public void ResetPose()
	{
		isPlay = false;
		_animator.enabled = true;
		currentTime = 0f;
		Vector3 localPosition = base.transform.localPosition;
		Quaternion localRotation = base.transform.localRotation;
		resetClip = animationList.Find((AnimationClip c) => c.name == resetClipName);
		if (resetClip != null)
		{
			resetClip.SampleAnimation(base.gameObject, currentTime);
		}
		else
		{
			Debug.LogError("Reset Clip not found: " + resetClipName);
		}
		base.transform.localPosition = localPosition;
		base.transform.localRotation = localRotation;
	}

	public void SetAnimation(AnimationClip anim)
	{
		currentClip = anim;
		clipName = anim.name;
		SetPose();
	}

	public void GetAnimationList()
	{
		animationList.Clear();
		animationNameList.Clear();
	}

	private AnimationClip FindAnimationClipByName(string name)
	{
		return animationList.Find((AnimationClip clip) => clip.name == name);
	}
}
