using FIMSpace.FTail;
using UnityEngine;

public class TailDemo_CompensateMovingPlatform : MonoBehaviour
{
	public TailAnimator2 tailAnimator;

	public Transform movingPlatform;

	[Range(0f, 1f)]
	public float LimitBlend = 1f;

	private Vector3 prePos;

	private void Start()
	{
		prePos = movingPlatform.position;
	}

	private void Update()
	{
		if (tailAnimator.IsInitialized)
		{
			Vector3 vector = (movingPlatform.position - prePos) * LimitBlend * tailAnimator.MotionInfluence;
			for (int i = 0; i < tailAnimator.TailSegments.Count; i++)
			{
				tailAnimator.TailSegments[i].ProceduralPosition += vector;
				tailAnimator.TailSegments[i].PreviousPosition += vector;
			}
			tailAnimator.GetGhostChild().ProceduralPosition += vector;
			tailAnimator.GetGhostChild().PreviousPosition += vector;
			prePos = movingPlatform.position;
		}
	}
}
