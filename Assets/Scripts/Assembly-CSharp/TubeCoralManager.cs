using System.Collections.Generic;
using UnityEngine;

public class TubeCoralManager : MonoBehaviour
{
	public List<GameObject> steamEffect;

	[Header("Timer")]
	public float currentTime;

	public float minTime = 15f;

	public float maxTime = 60f;

	[Header("Se")]
	public List<AudioClip> steamSe;

	private void Start()
	{
		for (int i = 0; i < steamEffect.Count; i++)
		{
			steamEffect[i].SetActive(value: false);
		}
		currentTime = Random.Range(minTime, maxTime);
	}

	private void LateUpdate()
	{
		currentTime -= Time.deltaTime;
		if (currentTime < 0f)
		{
			SteamOn();
			currentTime = Random.Range(minTime, maxTime);
		}
	}

	public void SteamOn()
	{
		for (int i = 0; i < steamEffect.Count; i++)
		{
			steamEffect[i].SetActive(value: true);
			if (OnomatopoeiaManager.instance.useOtomanopoeia)
			{
				OnomatopoeiaManager.instance.SpawnOnomatopoeia(steamEffect[i].transform.position, null, "CoralSteam", Camera.main.transform);
			}
		}
		EffectSeManager.instance.PlaySe(steamSe[Random.Range(0, steamSe.Count)]);
	}
}
