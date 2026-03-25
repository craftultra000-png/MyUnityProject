using System.Collections;
using UnityEngine;

public class WaterBall : MonoBehaviour
{
	[SerializeField]
	private ParticleSystem _WaterBallParticleSystem;

	[SerializeField]
	private AnimationCurve _SpeedCurve;

	[SerializeField]
	private float _Speed;

	[SerializeField]
	private ParticleSystem _SplashPrefab;

	[SerializeField]
	private ParticleSystem _SpillPrefab;

	public void Throw(Vector3 target)
	{
		StopAllCoroutines();
		StartCoroutine(Coroutine_Throw(target));
	}

	private IEnumerator Coroutine_Throw(Vector3 target)
	{
		float lerp = 0f;
		Vector3 startPos = base.transform.position;
		while (lerp < 1f)
		{
			base.transform.position = Vector3.Lerp(startPos, target, _SpeedCurve.Evaluate(lerp));
			if ((base.transform.position - target).magnitude < 0.4f)
			{
				break;
			}
			lerp += Time.deltaTime * _Speed;
			yield return null;
		}
		_WaterBallParticleSystem.Stop(withChildren: false, ParticleSystemStopBehavior.StopEmittingAndClear);
		ParticleSystem particleSystem = Object.Instantiate(_SplashPrefab, target, Quaternion.identity);
		Vector3 forward = target - startPos;
		forward.y = 0f;
		particleSystem.transform.forward = forward;
		if (Vector3.Angle(startPos - target, Vector3.up) > 30f)
		{
			Object.Instantiate(_SpillPrefab, target, Quaternion.identity).transform.forward = forward;
		}
		Object.Destroy(base.gameObject, 0.5f);
	}
}
