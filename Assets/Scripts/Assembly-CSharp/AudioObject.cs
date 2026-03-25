using UnityEngine;

public class AudioObject : MonoBehaviour
{
	public AudioSource _audioSource;

	public float volume;

	[Header("Type")]
	public bool bgm;

	public bool se;

	public bool voice;

	[Header("Manager")]
	public bool setList;

	private void Start()
	{
		if (_audioSource == null)
		{
			_audioSource = GetComponent<AudioSource>();
		}
		if (bgm)
		{
			volume = (float)VolumeManager.instance.se / 10f;
			_audioSource.volume = volume;
			if (setList)
			{
				VolumeManager.instance.listBgm.Add(_audioSource);
			}
		}
		if (se)
		{
			volume = (float)VolumeManager.instance.se / 10f;
			_audioSource.volume = volume;
			if (setList)
			{
				VolumeManager.instance.listSe.Add(_audioSource);
			}
		}
		if (voice)
		{
			volume = (float)VolumeManager.instance.voice / 10f;
			_audioSource.volume = volume;
			if (setList)
			{
				VolumeManager.instance.listVoice.Add(_audioSource);
			}
		}
	}

	private void Reset()
	{
		if (GetComponent<AudioSource>() != null)
		{
			_audioSource = GetComponent<AudioSource>();
		}
	}
}
