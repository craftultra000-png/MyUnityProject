using UnityEngine;

public class StateBoolBehaviour : StateMachineBehaviour
{
	public string boolName = "isDisable";

	public bool status;

	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		animator.SetBool(boolName, status);
	}
}
