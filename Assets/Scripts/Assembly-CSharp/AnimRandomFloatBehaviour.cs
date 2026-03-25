using UnityEngine;

public class AnimRandomFloatBehaviour : StateMachineBehaviour
{
	[SerializeField]
	private string parametersName = "Name";

	[SerializeField]
	private float randomNum;

	[Header("Calc Max Auto+1")]
	[SerializeField]
	private float randomMin;

	[SerializeField]
	private float randomMax = 1f;

	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		randomNum = Random.Range(randomMin, randomMax + 1f);
		animator.SetFloat(parametersName, randomNum);
	}
}
