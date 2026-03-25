using UnityEngine;

public class TailDemo_TailCutId : MonoBehaviour
{
	public int index;

	public TailDemo_SegmentedTailGenerator owner;

	private void OnMouseDown()
	{
		if ((bool)owner)
		{
			owner.ExmapleCutAt(index);
			Object.Destroy(this);
		}
	}
}
