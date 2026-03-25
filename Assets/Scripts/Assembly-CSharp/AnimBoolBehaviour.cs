using UnityEngine;

public class AnimBoolBehaviour : StateMachineBehaviour
{
	public string value = "is";

	public bool check;

	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		animator.SetBool(value, check);
	}
}
