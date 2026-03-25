using UnityEngine;

public class AdjustScaleToCollider : MonoBehaviour
{
	public Transform target;

	public SphereCollider _collider;

	[Header("Collider Size")]
	public float currentScale;

	public float targetScale;

	public AnimationCurve _curve;

	private void LateUpdate()
	{
		targetScale = target.localScale.x;
		if (currentScale != targetScale)
		{
			currentScale = targetScale;
			_collider.radius = _curve.Evaluate(currentScale);
		}
	}
}
