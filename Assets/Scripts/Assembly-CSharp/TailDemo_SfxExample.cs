using FIMSpace.FTail;
using UnityEngine;

public class TailDemo_SfxExample : MonoBehaviour
{
	public TailAnimator2[] toDetectCollisionsForSFX;

	public AudioClip clipToPlay;

	[Range(0f, 1f)]
	public float volume = 0.8f;

	public float toPlayAgainDelay = 0.1f;

	public int playUpToSegment;

	private bool[] wasCollision;

	private float toPlayTimer;

	private void Start()
	{
		wasCollision = new bool[toDetectCollisionsForSFX.Length];
		for (int i = 0; i < wasCollision.Length; i++)
		{
			wasCollision[i] = false;
		}
	}

	private void Update()
	{
		toPlayTimer += Time.deltaTime;
		if (toPlayTimer < toPlayAgainDelay)
		{
			return;
		}
		for (int i = 0; i < toDetectCollisionsForSFX.Length; i++)
		{
			TailAnimator2 tailAnimator = toDetectCollisionsForSFX[i];
			bool flag = false;
			for (int j = 0; j < tailAnimator.TailSegments.Count; j++)
			{
				if (!wasCollision[i])
				{
					if (tailAnimator.TailSegments[j].CollisionContactFlag)
					{
						flag = true;
						wasCollision[i] = true;
						OnTailCollisionEnterFirst(tailAnimator, j);
					}
				}
				else if (tailAnimator.TailSegments[j].CollisionContactFlag)
				{
					OnTailCollisionStay(tailAnimator, j);
					flag = true;
				}
			}
			if (wasCollision[i] && !flag)
			{
				wasCollision[i] = false;
				OnTailCollisionExitAll(tailAnimator);
			}
		}
	}

	private void OnTailCollisionEnterFirst(TailAnimator2 tail, int segment)
	{
		if (clipToPlay != null && (playUpToSegment <= 0 || segment <= playUpToSegment))
		{
			AudioSource.PlayClipAtPoint(clipToPlay, tail.TailSegments[segment].ProceduralPosition, volume);
			toPlayTimer = 0f;
		}
	}

	private void OnTailCollisionStay(TailAnimator2 tail, int segment)
	{
	}

	private void OnTailCollisionExitAll(TailAnimator2 tail)
	{
	}
}
