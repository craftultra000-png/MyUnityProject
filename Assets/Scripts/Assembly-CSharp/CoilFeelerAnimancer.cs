using Animancer;
using UnityEngine;

public class CoilFeelerAnimancer : MonoBehaviour
{
	public AnimancerComponent _animancer;

	[Header("Status")]
	public bool isCoil;

	public bool isSqueeze;

	public bool isSqueezeEnd;

	[Header("Paramater")]
	public AnimationClip coilAnim;

	[Range(0f, 1f)]
	public float coilParam;

	[Range(0f, 1f)]
	public float squeezeParam;

	[Range(0f, 2f)]
	public float totalParam;

	private AnimancerState _animancerState;

	[Header("Noise")]
	public float seed;

	[Range(0f, 1f)]
	public float noizeParam;

	[Range(0f, 1f)]
	public float noizeCalcParam;

	private void Start()
	{
		_animancerState = _animancer.Play(coilAnim);
		_animancerState.IsPlaying = false;
		seed = Random.Range(0f, 100f);
	}

	private void FixedUpdate()
	{
		_ = coilAnim.length;
		float num = Mathf.Clamp(coilParam, 0f, 1f);
		if (isCoil && isSqueeze)
		{
			if (squeezeParam < 1f)
			{
				squeezeParam += Time.deltaTime;
				if (squeezeParam > 1f)
				{
					isSqueezeEnd = true;
					squeezeParam = 1f;
				}
			}
		}
		else if (isCoil && !isSqueeze)
		{
			if (squeezeParam > 0f)
			{
				squeezeParam -= Time.deltaTime;
				if (squeezeParam < 0f)
				{
					squeezeParam = 0f;
				}
			}
		}
		else if (!isCoil && !isSqueeze)
		{
			if (squeezeParam > 0f)
			{
				squeezeParam -= Time.deltaTime * 5f;
				if (squeezeParam < 0f)
				{
					squeezeParam = 0f;
				}
			}
		}
		else if (!isCoil && isSqueeze && squeezeParam > 0f)
		{
			squeezeParam -= Time.deltaTime * 5f;
			if (squeezeParam < 0f)
			{
				squeezeParam = 0f;
			}
		}
		totalParam = num + squeezeParam;
		if (isSqueeze && isSqueezeEnd)
		{
			noizeParam = 0f - Mathf.PerlinNoise(Time.time, seed * 0.5f) * 0.4f;
		}
		else if (isCoil && !isSqueeze)
		{
			noizeParam = Mathf.PerlinNoise(Time.time, seed * 0.5f) * 0.2f;
		}
		else
		{
			noizeParam = 0f;
		}
		if (noizeCalcParam < noizeParam)
		{
			noizeCalcParam += Time.deltaTime;
			if (noizeCalcParam > noizeParam)
			{
				noizeCalcParam = noizeParam;
			}
		}
		else if (noizeCalcParam > noizeParam)
		{
			noizeCalcParam -= Time.deltaTime;
			if (noizeCalcParam < noizeParam)
			{
				noizeCalcParam = noizeParam;
			}
		}
		totalParam += noizeCalcParam;
		_animancerState.Time = totalParam;
	}
}
