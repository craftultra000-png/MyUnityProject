using System;
using System.Collections.Generic;
using UnityEngine;

public class GuildClockObject : MonoBehaviour
{
	public Transform pendulum;

	[Header("Status")]
	public bool isClock;

	[Header("Rotation")]
	public Vector3 calcRotation;

	public float swayRangeCurrent;

	public float swayRangeMax = 10f;

	public float timer;

	public float sin;

	public float sinOld;

	[Header("SE")]
	public AudioSource _audioSourceSE;

	public List<AudioClip> clockSe;

	private void Start()
	{
	}

	private void LateUpdate()
	{
		if (isClock)
		{
			timer += Time.deltaTime * MathF.PI;
			sin = Mathf.Sin(timer);
			float z = sin * swayRangeMax;
			calcRotation = new Vector3(0f, 0f, z);
			pendulum.localRotation = Quaternion.Euler(calcRotation);
			if (sin <= 0f && sinOld > 0f)
			{
				_audioSourceSE.PlayOneShot(clockSe[UnityEngine.Random.Range(0, clockSe.Count)]);
			}
			sinOld = sin;
		}
	}

	public void StopClock()
	{
		isClock = false;
	}
}
