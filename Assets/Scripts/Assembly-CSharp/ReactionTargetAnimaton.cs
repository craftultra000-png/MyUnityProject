using UnityEngine;

public class ReactionTargetAnimaton : MonoBehaviour
{
	public Animator _animator;

	[Header("Paramater")]
	public float currentX;

	public float currentY;

	public float targetX;

	public float targetY;

	public float calcX;

	public float calcY;

	[Range(-1f, 1f)]
	public float holdBody;

	public float speed = 2f;

	private void Start()
	{
		targetX = 0f;
		targetY = 0f;
	}

	private void LateUpdate()
	{
		if (currentX < targetX)
		{
			currentX += Time.deltaTime * speed;
			if (currentX > targetX)
			{
				currentX = targetX;
				targetX = 0f;
			}
		}
		else if (currentX > targetX)
		{
			currentX -= Time.deltaTime * speed;
			if (currentX < targetX)
			{
				currentX = targetX;
				targetX = 0f;
			}
		}
		if (currentY < targetY)
		{
			currentY += Time.deltaTime * speed;
			if (currentY > targetY)
			{
				currentY = targetY;
				targetY = 0f;
			}
		}
		else if (currentY > targetY)
		{
			currentY -= Time.deltaTime * speed;
			if (currentY < targetY)
			{
				currentY = targetY;
				targetY = 0f;
			}
		}
		calcX = currentX;
		if (calcX > 1f)
		{
			calcX = 1f;
		}
		else if (calcX < -1f)
		{
			calcX = -1f;
		}
		calcY = currentY + holdBody;
		if (calcY > 1f)
		{
			calcY = 1f;
		}
		else if (calcY < -1f)
		{
			calcY = -1f;
		}
		_animator.SetFloat("moveX", calcX);
		_animator.SetFloat("moveY", calcY);
	}
}
