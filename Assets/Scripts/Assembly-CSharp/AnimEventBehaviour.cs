using UnityEngine;

public class AnimEventBehaviour : StateMachineBehaviour
{
	[Header("StateEnter and Exit")]
	public bool fireEvents;

	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		animator.fireEvents = fireEvents;
		Debug.LogWarning(animator.name + " OnStateEnter FireEvents :" + animator.fireEvents);
	}
}
