using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
	public static VolumeManager instance;

	[Header("Volume Setting")]
	public int bgm;

	public int se;

	public int voice;

	[Header("Volume Default")]
	public int bgmDefault = 3;

	public int seDefault = 6;

	public int voiceDefault = 9;

	[Header("UI Slider")]
	public bool useSliderSe;

	public AudioClip slideSe;

	public Slider bgmSlider;

	public Slider seSlider;

	public Slider voiceSlider;

	[Header("Main AudioSource")]
	public List<AudioSource> mainBgm = new List<AudioSource>();

	public AudioSource mainSe;

	[Header("Change AudioSource")]
	public List<AudioSource> listBgm = new List<AudioSource>();

	public List<AudioSource> listSe = new List<AudioSource>();

	public List<AudioSource> listVoice = new List<AudioSource>();

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		if (!ES3.KeyExists("VolumeBgm"))
		{
			ES3.Save("VolumeBgm", bgmDefault);
			ES3.Save("VolumeSe", seDefault);
			ES3.Save("VolumeVoice", voiceDefault);
		}
		bgm = ES3.Load<int>("VolumeBgm");
		se = ES3.Load<int>("VolumeSe");
		voice = ES3.Load<int>("VolumeVoice");
		SetBgm();
		SetSe();
		SetVoice();
		if (bgmSlider != null)
		{
			bgmSlider.value = bgm;
		}
		if (seSlider != null)
		{
			seSlider.value = se;
		}
		if (voiceSlider != null)
		{
			voiceSlider.value = voice;
		}
	}

	private void Start()
	{
		UseSlide(value: true);
	}

	public void UseSlide(bool value)
	{
		useSliderSe = value;
	}

	public void SliderSe()
	{
		if (useSliderSe)
		{
			mainSe.PlayOneShot(slideSe);
		}
	}

	public void SetAll()
	{
		SetBgm();
		SetSe();
		SetVoice();
	}

	public void SetBgm()
	{
		bgm = ES3.Load<int>("VolumeBgm");
		for (int i = 0; i < mainBgm.Count; i++)
		{
			mainBgm[i].volume = (float)bgm / 10f;
		}
		if (listBgm.Count > 0)
		{
			for (int j = 0; j < listBgm.Count; j++)
			{
				listBgm[j].volume = (float)bgm / 10f;
			}
		}
		if (BGMManager.instance != null)
		{
			BGMManager.instance.defaultVolume = (float)bgm / 10f;
		}
	}

	public void SetSe()
	{
		se = ES3.Load<int>("VolumeSe");
		mainSe.volume = (float)se / 10f;
		if (listSe.Count > 0)
		{
			for (int i = 0; i < listSe.Count; i++)
			{
				listSe[i].volume = (float)se / 10f;
			}
		}
	}

	public void SetVoice()
	{
		voice = ES3.Load<int>("VolumeVoice");
		if (listVoice.Count > 0)
		{
			for (int i = 0; i < listVoice.Count; i++)
			{
				listVoice[i].volume = (float)voice / 10f;
			}
		}
	}

	public void SaveAll()
	{
		SaveBgm();
		SaveSe();
		SaveVoice();
	}

	public void SaveBgm()
	{
		ES3.Save("VolumeBgm", Mathf.RoundToInt(bgmSlider.value));
		SetBgm();
	}

	public void SaveSe()
	{
		ES3.Save("VolumeSe", Mathf.RoundToInt(seSlider.value));
		SetSe();
	}

	public void SaveVoice()
	{
		ES3.Save("VolumeVoice", Mathf.RoundToInt(voiceSlider.value));
		SetVoice();
	}
}
