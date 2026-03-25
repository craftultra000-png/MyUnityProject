using UnityEngine;

public class CharacterAnimBehaviour : StateMachineBehaviour
{
	[SerializeField]
	private AvatarTarget targetBodyPart;

	[SerializeField]
	[Range(0f, 1f)]
	private float start;

	[SerializeField]
	[Range(0f, 1f)]
	private float end = 1f;

	[Header("match target")]
	public Vector3 matchPosition;

	public Quaternion matchRotation;

	[Header("Weights")]
	public Vector3 positionWeight = new Vector3(1f, 0f, 1f);

	public float rotationWeight = 1f;

	private MatchTargetWeightMask weightMask;

	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		weightMask = new MatchTargetWeightMask(positionWeight, rotationWeight);
	}

	public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if (!animator.IsInTransition(layerIndex))
		{
			animator.MatchTarget(matchPosition, matchRotation, targetBodyPart, weightMask, start, end);
		}
	}
}
