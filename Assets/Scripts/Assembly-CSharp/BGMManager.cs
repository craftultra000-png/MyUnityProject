using UnityEngine;

public class BGMManager : MonoBehaviour
{
	public static BGMManager instance;

	public AudioSource _audioSource1;

	public AudioSource _audioSource2;

	[Header("Setting")]
	public bool unAwake;

	[Header("Status")]
	public bool isCrossFade;

	public bool isMute;

	[Space]
	public bool source1;

	public bool source2;

	[Header("Cross Feed Calc")]
	public float currentTime;

	public float feedTime = 0.5f;

	public float defaultVolume;

	public float volume1;

	public float volume2;

	[Range(0f, 1f)]
	public float currentFeed;

	[Header("BGM Name")]
	public AudioClip currentBGM;

	public AudioClip targetBGM;

	[Header("BGM Data")]
	public string bgmName;

	public AudioClip titleBGM;

	public AudioClip guildBGM;

	public AudioClip gameoverBGM;

	public AudioClip sleepBGM;

	public void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
	}

	private void Start()
	{
		if (VolumeManager.instance != null)
		{
			defaultVolume = (float)VolumeManager.instance.bgm / 10f;
		}
		if (!unAwake)
		{
			targetBGM = currentBGM;
			currentBGM = null;
			CheckAudioSource();
		}
		else
		{
			currentBGM = null;
		}
	}

	private void LateUpdate()
	{
		if (isCrossFade && !isMute)
		{
			currentTime += Time.unscaledDeltaTime;
			currentFeed = currentTime / feedTime;
			if (currentFeed > 1f)
			{
				currentFeed = 1f;
				isCrossFade = false;
			}
			if (source1)
			{
				volume1 = currentFeed * defaultVolume;
				_audioSource1.volume = volume1;
				if (currentFeed == 1f)
				{
					_audioSource2.Stop();
					return;
				}
				volume2 = defaultVolume - currentFeed * defaultVolume;
				_audioSource2.volume = volume2;
			}
			else if (source2)
			{
				volume2 = currentFeed * defaultVolume;
				_audioSource2.volume = volume2;
				if (currentFeed == 1f)
				{
					_audioSource1.Stop();
					return;
				}
				volume1 = defaultVolume - currentFeed * defaultVolume;
				_audioSource1.volume = volume1;
			}
		}
		else
		{
			if (!isMute)
			{
				return;
			}
			currentTime += Time.unscaledDeltaTime;
			currentFeed = feedTime - currentTime / feedTime;
			if (currentFeed < 0f)
			{
				currentFeed = 0f;
				isMute = false;
			}
			if (source1)
			{
				volume1 = currentFeed * defaultVolume;
				_audioSource1.volume = volume1;
				if (currentFeed == 0f)
				{
					source1 = false;
					_audioSource1.Stop();
				}
			}
			if (source2)
			{
				volume2 = currentFeed * defaultVolume;
				_audioSource2.volume = volume2;
				if (currentFeed == 0f)
				{
					source2 = false;
					_audioSource2.Stop();
				}
			}
		}
	}

	public void SetBGM(string type)
	{
		Debug.LogWarning("BGM Name: " + type, base.gameObject);
		bgmName = type;
		currentBGM = targetBGM;
		if (bgmName == "Title")
		{
			targetBGM = titleBGM;
		}
		else if (bgmName == "Guild")
		{
			targetBGM = guildBGM;
		}
		else if (bgmName == "GameOver")
		{
			targetBGM = gameoverBGM;
		}
		else if (bgmName == "Sleep")
		{
			targetBGM = sleepBGM;
		}
		else
		{
			Debug.LogError("BGM Name Nothing!", base.gameObject);
		}
		CheckAudioSource();
	}

	public void StopBGM()
	{
		Debug.LogWarning("Stop BGM", base.gameObject);
		isMute = true;
		currentTime = 0f;
	}

	public void CheckAudioSource()
	{
		Debug.Log("BGM Name: " + targetBGM.name, base.gameObject);
		if (currentBGM != targetBGM)
		{
			isCrossFade = true;
			isMute = false;
			currentTime = 0f;
			if (!source1 && !source2)
			{
				source1 = true;
				source2 = false;
				_audioSource1.clip = targetBGM;
				_audioSource1.Play();
			}
			else if (source1 && !source2)
			{
				source1 = false;
				source2 = true;
				_audioSource2.clip = targetBGM;
				_audioSource2.Play();
			}
			else if (!source1 && source2)
			{
				source1 = true;
				source2 = false;
				_audioSource1.clip = targetBGM;
				_audioSource1.Play();
			}
		}
	}
}
