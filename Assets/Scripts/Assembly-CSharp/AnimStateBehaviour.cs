using UnityEngine;

public class AnimStateBehaviour : StateMachineBehaviour
{
	public string state = "isChangeState";

	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		animator.SetBool(state, value: false);
	}
}
