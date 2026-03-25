using UnityEngine;

public class CharacterVaginaManager : MonoBehaviour
{
	public GameObject mosaicVagina;

	public GameObject mosaicInsert;

	[Space]
	public GameObject paint3D;

	private void Awake()
	{
		paint3D.SetActive(value: false);
	}

	public void MosaicVaginaOn()
	{
		mosaicVagina.SetActive(value: true);
	}

	public void MosaicVaginaOff()
	{
		mosaicVagina.SetActive(value: false);
	}

	public void MosaicInsertOn()
	{
		mosaicInsert.SetActive(value: true);
	}

	public void MosaicInsertOff()
	{
		mosaicInsert.SetActive(value: false);
	}

	public void SetPaint3D()
	{
		paint3D.SetActive(value: true);
	}
}
