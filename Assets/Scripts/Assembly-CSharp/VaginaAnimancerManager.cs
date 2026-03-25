using Animancer;
using UnityEngine;

public class VaginaAnimancerManager : MonoBehaviour
{
	public AnimancerComponent _animancer;

	public AnimancerLayer vaginaLayer;

	public AnimancerLayer analLayer;

	[Header("Animation Clip Vagina")]
	public AnimationClip vaginaIdleClip;

	public AnimationClip vaginaOpenClip;

	public AnimationClip vaginaHoleOpenClip;

	[Header("Animation Clip Anal")]
	public AnimationClip analIdleClip;

	public AnimationClip analOpenClip;

	private void Start()
	{
		vaginaLayer = _animancer.Layers[1];
		vaginaLayer.Play(vaginaIdleClip, 0.25f);
		analLayer = _animancer.Layers[2];
		analLayer.Play(analIdleClip, 0.25f);
	}

	public void Vagina(int value)
	{
		switch (value)
		{
		case 0:
			vaginaLayer.Play(vaginaIdleClip, 0.5f);
			break;
		case 1:
			vaginaLayer.Play(vaginaOpenClip, 0.5f);
			break;
		case 2:
			vaginaLayer.Play(vaginaHoleOpenClip, 0.5f);
			break;
		}
	}

	public void Anal(int value)
	{
		switch (value)
		{
		case 0:
			vaginaLayer.Play(analIdleClip, 0.5f);
			break;
		case 1:
			vaginaLayer.Play(analOpenClip, 0.5f);
			break;
		}
	}
}
