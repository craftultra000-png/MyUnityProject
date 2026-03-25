using System.Collections.Generic;
using UnityEngine;

public class CharacterVagina : MonoBehaviour
{
	public CharacterFaceManager _characterFaceManager;

	public CharacterMouthManager _characterMouthManager;

	public CharacterModelManager _characterModelManager;

	public VaginaAnimancerManager _vaginaAnimancerManager;

	[Header("Mosaic")]
	public bool mosaic;

	public GameObject mosaicObject;

	[Header("Vagina Drip")]
	public bool drip;

	public Transform vaginaHole;

	[Header("Vagina Splash")]
	public int splashCurrent;

	public int splashMin = 3;

	public int splashMax = 6;

	[Header("Vagina Hang")]
	public int hangCurrent;

	public int hangMin = 5;

	public int hangMax = 10;

	[Header("Calc Rotation")]
	public Quaternion effectRotation;

	[Header("Effect Stocker")]
	public GameObject effectStocker;

	[Header("Effect")]
	public GameObject vaginaGlobs;

	public GameObject vaginaDrip;

	public GameObject vaginaSplash;

	public GameObject orgasmBurst;

	public GameObject peeSplash;

	[Header("SE")]
	public AudioSource audioSe;

	private AudioClip playClip;

	public List<AudioClip> seSplash = new List<AudioClip>();

	public List<AudioClip> seBurst = new List<AudioClip>();

	public List<AudioClip> sePee = new List<AudioClip>();

	public List<AudioClip> seCreamPie = new List<AudioClip>();

	public List<AudioClip> sePiston = new List<AudioClip>();

	public List<AudioClip> seAnal = new List<AudioClip>();

	public List<AudioClip> seWet = new List<AudioClip>();

	private void Start()
	{
		splashCurrent = splashMin;
		hangCurrent = hangMin;
	}

	public void MosaicSet(bool value)
	{
		mosaic = value;
		mosaicObject.SetActive(mosaic);
	}

	public void VaginaSplashCount()
	{
		splashCurrent--;
		if (Random.Range(0, 3) == 0)
		{
			VaginaGlobs();
		}
		if (splashCurrent <= 0)
		{
			splashCurrent = Random.Range(splashMax, splashMin);
			VaginaSplash();
			_characterMouthManager.PlayHitSe();
		}
	}

	public void VaginaAnim(int value)
	{
		_vaginaAnimancerManager.Vagina(value);
	}

	public void AnalAnim(int value)
	{
		_vaginaAnimancerManager.Anal(value);
	}

	public void VaginaGlobs()
	{
		Object.Instantiate(vaginaGlobs, vaginaHole.position, vaginaHole.rotation, effectStocker.transform);
	}

	public void VaginaDrip()
	{
		Object.Instantiate(vaginaDrip, vaginaHole.position, vaginaHole.rotation, effectStocker.transform);
	}

	public void VaginaSplash()
	{
		Object.Instantiate(vaginaSplash, vaginaHole.position, vaginaHole.rotation, effectStocker.transform);
		SplashSe();
	}

	public void OrgasmSplash()
	{
		Object.Instantiate(orgasmBurst, vaginaHole.position, vaginaHole.rotation, effectStocker.transform);
		PeeSe();
	}

	public void PeeSplash()
	{
		Object.Instantiate(peeSplash, vaginaHole.position, vaginaHole.rotation, effectStocker.transform);
		BurstSe();
	}

	public void SplashSe()
	{
		playClip = seSplash[Random.Range(0, seSplash.Count)];
		audioSe.PlayOneShot(playClip);
	}

	public void BurstSe()
	{
		playClip = seBurst[Random.Range(0, seBurst.Count)];
		audioSe.PlayOneShot(playClip);
	}

	public void PeeSe()
	{
		playClip = sePee[Random.Range(0, sePee.Count)];
		audioSe.PlayOneShot(playClip);
	}

	public void CreamPieSe()
	{
		playClip = seCreamPie[Random.Range(0, seCreamPie.Count)];
		audioSe.PlayOneShot(playClip);
	}

	public void WetSe()
	{
		playClip = seWet[Random.Range(0, seWet.Count)];
		audioSe.PlayOneShot(playClip);
	}
}
