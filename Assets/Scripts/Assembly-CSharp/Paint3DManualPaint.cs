using PaintCore;
using UnityEngine;

public class Paint3DManualPaint : MonoBehaviour
{
	public CwHitNearby _cwHitNearby;

	public float disableTime = 0.5f;

	public float disableTimeMax = 0.5f;

	private void Start()
	{
		base.gameObject.SetActive(value: false);
	}

	private void LateUpdate()
	{
		if (disableTime > 0f)
		{
			disableTime -= Time.deltaTime;
			if (disableTime < 0f)
			{
				base.gameObject.SetActive(value: false);
			}
		}
	}

	private void OnEnable()
	{
		_cwHitNearby.ManuallyHitNow();
		disableTime = disableTimeMax;
	}

	private void OnDisable()
	{
	}
}
