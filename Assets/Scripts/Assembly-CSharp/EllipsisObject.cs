using UnityEngine;
using UnityEngine.UI;

public class EllipsisObject : MonoBehaviour
{
	[Header("WaitTime")]
	public float waitTime;

	public float waitTimeMax = 3f;

	[Header("... Ellipsis Calc")]
	public float interval;

	public float calc1 = 2.5f;

	public float calc2 = 1.5f;

	public float calc3 = 0.5f;

	[Header("... Ellipsis")]
	public Text ellipsis;

	public int ellipsisCount;

	private void FixedUpdate()
	{
		if (waitTime > 0f)
		{
			waitTime -= Time.deltaTime;
			if (waitTime < calc1 && ellipsisCount < 1)
			{
				ellipsisCount++;
				ellipsis.text = ".";
			}
			else if (waitTime < calc2 && ellipsisCount < 2)
			{
				ellipsisCount++;
				ellipsis.text = "..";
			}
			else if (waitTime < calc3 && ellipsisCount < 3)
			{
				ellipsisCount++;
				ellipsis.text = "...";
			}
		}
	}

	private void OnEnable()
	{
		waitTime = waitTimeMax;
		ellipsisCount = 0;
		ellipsis.text = "";
	}
}
