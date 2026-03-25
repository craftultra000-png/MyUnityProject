using UnityEngine;

public class EffectSeManager : MonoBehaviour
{
	public static EffectSeManager instance;

	public AudioSource _audioSource;

	private void Awake()
	{
		instance = this;
	}

	public void PlaySe(AudioClip clip)
	{
		_audioSource.PlayOneShot(clip);
	}
}
