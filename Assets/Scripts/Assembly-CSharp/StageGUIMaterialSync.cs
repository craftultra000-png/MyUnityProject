using UnityEngine;

public class StageGUIMaterialSync : MonoBehaviour
{
	public SpriteRenderer _spriteRenderer;

	public Sprite _sprite;

	public Texture _tex;

	public Color _color;

	private Material _material;

	private MaterialPropertyBlock _block;

	private void Start()
	{
		_spriteRenderer.sprite = _sprite;
		_block = new MaterialPropertyBlock();
		_spriteRenderer.GetPropertyBlock(_block);
		_block.SetColor("_MainColor", _spriteRenderer.color);
		_block.SetTexture("_MainTex", _tex);
		_spriteRenderer.SetPropertyBlock(_block);
	}

	private void LateUpdate()
	{
		_block.SetColor("_MainColor", _color);
		_spriteRenderer.SetPropertyBlock(_block);
	}
}
