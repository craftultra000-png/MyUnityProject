using System.Collections.Generic;
using UnityEngine;

public class SqueezeManager : MonoBehaviour
{
	public BindManager _bindManager;

	public CharacterLifeManager _characterLifeManager;

	public CharacterEyesManager _characterEyesManager;

	public CharacterLookAtEyes _characterLookAtEyes;

	public CharacterMouthManager _characterMouthManager;

	public CharacterTalkManager _characterTalkManager;

	[Header("Status")]
	public bool isSqueeze;

	public bool isExtendUpper;

	public bool isExtendLower;

	[Header("Damage")]
	public int bindBodyCount;

	public int bindHeadCount;

	[Header("Face Time")]
	public bool faceChange;

	public float faceTime;

	public float faceTimeMax = 1.5f;

	[Header("Time")]
	public float currentTime;

	public float minTime = 3f;

	public float maxTime = 8f;

	[Header("Attack Type")]
	public string attackType = "Squeeze";

	[Header("Se")]
	public List<AudioClip> coilSe;

	private void Start()
	{
	}

	private void LateUpdate()
	{
		bindBodyCount = _bindManager.bindBodyCount;
		bindHeadCount = _bindManager.bindHeadCount;
		if (isSqueeze)
		{
			currentTime -= Time.deltaTime;
			if (currentTime < 0f)
			{
				currentTime = Random.Range(minTime, maxTime);
				Squeeze();
			}
		}
		if (faceChange && faceTime > 0f)
		{
			faceTime -= Time.deltaTime;
			if (faceTime < 0f)
			{
				faceChange = false;
				ChangeFace(value: true);
				Squeeze();
			}
		}
	}

	public void SetSqueeze(bool value)
	{
		isSqueeze = value;
		if (isSqueeze)
		{
			currentTime = Random.Range(minTime, maxTime);
		}
		if (isSqueeze)
		{
			faceChange = true;
			faceTime = faceTimeMax;
		}
		else
		{
			faceChange = false;
			ChangeFace(value: false);
		}
	}

	public void ChangeFace(bool value)
	{
		_characterEyesManager.isSqueeze = value;
		_characterLookAtEyes.isSqueeze = value;
		_characterMouthManager.isSqueeze = value;
		_characterTalkManager.squeeze = value;
	}

	public void Squeeze()
	{
		_characterLifeManager.HitData("Squeeze", attackType);
		EffectSeManager.instance.PlaySe(coilSe[Random.Range(0, coilSe.Count)]);
		_bindManager.SqueezeOnomatopoeia();
	}
}
