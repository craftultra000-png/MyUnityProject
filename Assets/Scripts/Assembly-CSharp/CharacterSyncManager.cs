using UnityEngine;

public class CharacterSyncManager : MonoBehaviour
{
	[Header("Body")]
	public Transform body;

	public Animator bodyAnimator;

	[Header("Sync")]
	public CharacterSyncManager targetSync;

	[Header("Position Status")]
	public bool backPosition;

	[Header("Animation Param")]
	public Vector3 animTargetPositon;

	public Vector3 animTargetRotation;

	public Vector3 calcRotation;

	public Vector3 calcTargetRotation;

	private CharacterAnimBehaviour[] _animBehaviour;

	private void Start()
	{
		_animBehaviour = bodyAnimator.GetBehaviours<CharacterAnimBehaviour>();
	}

	public void CurrentPosition(float angle)
	{
		animTargetPositon = body.position + body.forward * 0.5f;
		calcRotation = body.rotation.eulerAngles;
		calcTargetRotation = body.rotation.eulerAngles;
		animTargetRotation = calcRotation;
		animTargetRotation.y += angle;
		if (animTargetRotation.y > 360f)
		{
			animTargetRotation.y -= 360f;
		}
		else if (animTargetRotation.y < 360f)
		{
			animTargetRotation.y += 360f;
		}
		if (!backPosition)
		{
			animTargetPositon = body.position + body.forward * 0.5f;
		}
		else
		{
			animTargetPositon = body.position - body.forward * 0.5f;
		}
		calcRotation = body.rotation.eulerAngles;
		animTargetRotation = calcRotation;
		if (!backPosition)
		{
			if (animTargetRotation.y < 0f)
			{
				animTargetRotation.y += 180f;
			}
			else
			{
				animTargetRotation.y -= 180f;
			}
		}
	}

	public void SyncPosition()
	{
		CurrentPosition(180f);
		for (int i = 0; i < _animBehaviour.Length; i++)
		{
			_animBehaviour[i].matchPosition = animTargetPositon;
			_animBehaviour[i].matchRotation = Quaternion.Euler(animTargetRotation);
		}
	}

	public void SyncAnimation()
	{
		int value = Random.Range(0, 5);
		bodyAnimator.SetInteger("RandomAnimInt", value);
		targetSync.bodyAnimator.SetInteger("RandomAnimInt", value);
	}
}
