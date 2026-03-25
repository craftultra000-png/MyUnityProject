using UnityEngine;

public class AnimRandomIntBehaviour : StateMachineBehaviour
{
	[SerializeField]
	private string parametersName = "Name";

	[SerializeField]
	private int randomNum;

	[Header("Calc")]
	[SerializeField]
	private int randomMin;

	[SerializeField]
	private int randomMax = 1;

	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		randomNum = Random.Range(randomMin, randomMax + 1);
		animator.SetInteger(parametersName, randomNum);
	}
}
