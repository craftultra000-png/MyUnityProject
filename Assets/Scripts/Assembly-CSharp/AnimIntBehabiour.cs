using UnityEngine;

public class AnimIntBehabiour : StateMachineBehaviour
{
	[SerializeField]
	private string parametersName = "AnimNum";

	[SerializeField]
	private int num = -1;

	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		animator.SetInteger(parametersName, num);
	}
}
