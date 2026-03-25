using System.Collections.Generic;
using UnityEngine;

public class FilamentTouchPlayerManager : MonoBehaviour
{
	public List<FilamentTouchPlayer> _filamentTouchPlayer;

	[Header("Attack Type")]
	public string attackType = "Touch";

	public Transform currentTarget;

	[Header("Status")]
	public bool isSearch;

	public bool isTouch;

	[Header("Touch Time")]
	public List<float> currentTime;

	public float minTime = 1.5f;

	public float maxTime = 3f;

	[Header("Se")]
	public List<AudioClip> touchSe;

	private void Start()
	{
		currentTime.Clear();
		for (int i = 0; i < _filamentTouchPlayer.Count; i++)
		{
			currentTime.Add(Random.Range(minTime, maxTime));
		}
	}

	private void LateUpdate()
	{
		if (!isSearch)
		{
			return;
		}
		isTouch = false;
		for (int i = 0; i < _filamentTouchPlayer.Count; i++)
		{
			if (!_filamentTouchPlayer[i].isHit)
			{
				continue;
			}
			isTouch = true;
			if (isTouch)
			{
				currentTime[i] -= Time.deltaTime;
				if (currentTime[i] < 0f)
				{
					currentTime[i] = Random.Range(minTime, maxTime);
					if (Random.Range(0, 3) < 1)
					{
						EffectSeManager.instance.PlaySe(touchSe[Random.Range(0, touchSe.Count)]);
					}
					currentTarget = _filamentTouchPlayer[i].currentTarget;
					if ((bool)currentTarget.gameObject.GetComponent<CharacterColliderObject>())
					{
						currentTarget.gameObject.GetComponent<CharacterColliderObject>().HitData(attackType);
					}
				}
			}
			else
			{
				currentTime[i] = Random.Range(minTime, maxTime);
			}
		}
	}

	public void SetSearch(bool value)
	{
		isSearch = value;
		for (int i = 0; i < _filamentTouchPlayer.Count; i++)
		{
			_filamentTouchPlayer[i].isSearch = isSearch;
			currentTime.Add(Random.Range(minTime, maxTime));
		}
	}
}
