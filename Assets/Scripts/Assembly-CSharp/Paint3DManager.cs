using PaintCore;
using PaintIn3D;
using UnityEngine;

public class Paint3DManager : MonoBehaviour
{
	[Header("Dammy")]
	public CwPaintableMeshTexture _cwPaintableDammy;

	public CwGraduallyFade _cwGraduallyFade;

	[Header("Delay")]
	public CwPaintableMeshTexture _cwPaintableDelay;

	[Header("Material")]
	public Material clotheMaterial;

	public Texture mainTex;

	public Texture alphaMask;

	private void OnEnable()
	{
		_cwGraduallyFade.enabled = false;
		_cwPaintableDammy.Clear();
		if (_cwPaintableDelay != null)
		{
			_cwPaintableDelay.Clear();
		}
		_cwGraduallyFade.enabled = true;
	}

	private void OnDestroy()
	{
	}

	public void LockPaint(bool value)
	{
		_cwPaintableDammy.enabled = value;
		_cwPaintableDelay.enabled = value;
	}
}
