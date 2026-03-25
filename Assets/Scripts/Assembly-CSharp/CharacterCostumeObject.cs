using PaintCore;
using PaintIn3D;
using UnityEngine;

public class CharacterCostumeObject : MonoBehaviour
{
	[Header("GUI Data")]
	public string clotheName;

	public int clotheNum;

	public int clotheLevel;

	public float breakRatio = 0.6f;

	[Header("Script Data")]
	public Paint3DManager _paint3DManager;

	[Header("Mesh Data")]
	public MeshRenderer _mesh;

	public SkinnedMeshRenderer _skinnedMesh;

	public bool mosaic;

	public bool unUseBodyBone;

	[Header("Break Steam Data")]
	public bool tits;

	public bool vagina;

	public bool anal;

	public bool handL;

	public bool handR;

	public bool footL;

	public bool footR;

	[Header("Texture Data")]
	public Texture costumeTex;

	public Texture costumeNormal;

	public Texture alphaMask;

	[Header("Channel Data")]
	public CwChannelCounter counter;

	public CwPaintableMeshTexture painter;
}
