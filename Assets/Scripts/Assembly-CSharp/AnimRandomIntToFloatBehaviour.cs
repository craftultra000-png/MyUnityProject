using UnityEngine;

public class AnimRandomIntToFloatBehaviour : StateMachineBehaviour
{
	[SerializeField]
	private string parametersName = "Name";

	[SerializeField]
	private float randomNum;

	[Header("Calc Max Auto+1")]
	[SerializeField]
	private int randomMin;

	[SerializeField]
	private int randomMax = 1;

	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		randomNum = Random.Range(randomMin, randomMax + 1);
		animator.SetFloat(parametersName, randomNum);
	}
}
