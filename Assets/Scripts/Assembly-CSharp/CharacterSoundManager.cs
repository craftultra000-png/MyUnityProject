using System.Collections.Generic;
using UnityEngine;

public class CharacterSoundManager : MonoBehaviour
{
	public AudioSource _audioSource;

	[Header("SE")]
	public List<AudioClip> slapSe;

	public List<AudioClip> spankSe;

	public List<AudioClip> kickSe;

	public List<AudioClip> shoveSe;

	public List<AudioClip> downSe;

	public List<AudioClip> kissSe;

	public List<AudioClip> lickSe;

	public List<AudioClip> suckSe;

	public List<AudioClip> biteSe;

	public List<AudioClip> titsSe;

	public List<AudioClip> pistonHipSe;

	public List<AudioClip> chokeSe;

	[Header("Vagina")]
	public List<AudioClip> lostVirsinSe;

	public List<AudioClip> dropletsSe;

	public List<AudioClip> splashSe;

	public List<AudioClip> peeSe;

	public List<AudioClip> pistonSe;

	public List<AudioClip> shotSe;

	public List<AudioClip> dripSe;

	[Header("Tits")]
	public List<AudioClip> milkSe;

	[Header("Clothe")]
	public List<AudioClip> takeoffSe;

	public List<AudioClip> breakClotheSe;

	[Header("Effect")]
	public AudioClip heartStreamSe;

	public AudioClip heartCircleSe;

	[Header("Foot IK")]
	public List<AudioClip> defaultSe;

	public List<AudioClip> stoneSe;

	public List<AudioClip> clotheSe;

	public List<AudioClip> grassSe;

	public List<AudioClip> woodSe;

	public List<AudioClip> dirtSe;

	[Header("Change Se")]
	public List<AudioClip> changeSe;

	public void SlapSe()
	{
		_audioSource.PlayOneShot(slapSe[Random.Range(0, slapSe.Count)]);
	}

	public void SpankSe()
	{
		_audioSource.PlayOneShot(spankSe[Random.Range(0, spankSe.Count)]);
	}

	public void KickSe()
	{
		_audioSource.PlayOneShot(kickSe[Random.Range(0, kickSe.Count)]);
	}

	public void ShoveSe()
	{
		_audioSource.PlayOneShot(shoveSe[Random.Range(0, shoveSe.Count)]);
	}

	public void DownSe()
	{
		_audioSource.PlayOneShot(shoveSe[Random.Range(0, downSe.Count)]);
	}

	public void KissSe()
	{
		_audioSource.PlayOneShot(kissSe[Random.Range(0, kissSe.Count)]);
	}

	public void LickSe()
	{
		_audioSource.PlayOneShot(lickSe[Random.Range(0, lickSe.Count)]);
	}

	public void SuckSe()
	{
		_audioSource.PlayOneShot(suckSe[Random.Range(0, suckSe.Count)]);
	}

	public void BiteSe()
	{
		_audioSource.PlayOneShot(biteSe[Random.Range(0, biteSe.Count)]);
	}

	public void TitsSe()
	{
		_audioSource.PlayOneShot(titsSe[Random.Range(0, titsSe.Count)]);
	}

	public void LostVirsinSe()
	{
		_audioSource.PlayOneShot(lostVirsinSe[Random.Range(0, lostVirsinSe.Count)]);
	}

	public void DropletsSe()
	{
		_audioSource.PlayOneShot(dropletsSe[Random.Range(0, dropletsSe.Count)]);
	}

	public void SplashSe()
	{
		_audioSource.PlayOneShot(splashSe[Random.Range(0, splashSe.Count)]);
	}

	public void PeeSe()
	{
		_audioSource.PlayOneShot(peeSe[Random.Range(0, peeSe.Count)]);
	}

	public void PistonSe()
	{
		_audioSource.PlayOneShot(pistonSe[Random.Range(0, pistonSe.Count)]);
	}

	public void ShotSe()
	{
		_audioSource.PlayOneShot(shotSe[Random.Range(0, shotSe.Count)]);
	}

	public void DripSe()
	{
		_audioSource.PlayOneShot(dripSe[Random.Range(0, dripSe.Count)]);
	}

	public void MilkSe()
	{
		_audioSource.PlayOneShot(milkSe[Random.Range(0, milkSe.Count)]);
	}

	public void PistonHipSe()
	{
		_audioSource.PlayOneShot(pistonHipSe[Random.Range(0, pistonHipSe.Count)]);
	}

	public void ChokeSe()
	{
		_audioSource.PlayOneShot(chokeSe[Random.Range(0, chokeSe.Count)]);
	}

	public void TakeOffSe()
	{
		_audioSource.PlayOneShot(takeoffSe[Random.Range(0, takeoffSe.Count)]);
	}

	public void BreakClotheSe()
	{
		_audioSource.PlayOneShot(breakClotheSe[Random.Range(0, breakClotheSe.Count)]);
	}

	public void HeartStreamSe()
	{
		_audioSource.PlayOneShot(heartStreamSe);
	}

	public void HeartCircleSe()
	{
		_audioSource.PlayOneShot(heartCircleSe);
	}

	public void Default()
	{
		_audioSource.PlayOneShot(defaultSe[Random.Range(0, defaultSe.Count)]);
	}

	public void Stone()
	{
		_audioSource.PlayOneShot(stoneSe[Random.Range(0, stoneSe.Count)]);
	}

	public void Clothe()
	{
		_audioSource.PlayOneShot(clotheSe[Random.Range(0, clotheSe.Count)]);
	}

	public void Wood()
	{
		_audioSource.PlayOneShot(woodSe[Random.Range(0, woodSe.Count)]);
	}

	public void Grass()
	{
		_audioSource.PlayOneShot(grassSe[Random.Range(0, grassSe.Count)]);
	}

	public void Dirt()
	{
		_audioSource.PlayOneShot(dirtSe[Random.Range(0, dirtSe.Count)]);
	}

	public void Change()
	{
		Debug.LogError("Change Se");
		_audioSource.PlayOneShot(changeSe[Random.Range(0, changeSe.Count)]);
	}
}
