using System.Collections.Generic;
using Animancer;
using UnityEngine;

public class CoralMouthManager : MonoBehaviour
{
	public AnimancerComponent _animancer;

	[Header("Paramater")]
	public AnimationClip openAnim;

	[Range(0f, 1f)]
	public float param;

	private AnimancerState _animancerState;

	[Header("Status")]
	public bool isOpen;

	public bool isDrool;

	public bool isEat;

	[Header("Random Open")]
	public float paramTarget;

	public float paramMin = 0.1f;

	public float paramMax = 1f;

	public float paramSpeed = 0.5f;

	public float paramEatSpeed = 1f;

	[Header("WaitTime")]
	public float waitCurrent;

	public float waitMin = 5f;

	public float waitMax = 10f;

	[Header("Effect")]
	public Transform spawnPoint;

	public GameObject droorEffect;

	[Header("Audio")]
	public List<AudioClip> droolSe;

	private void Start()
	{
		waitCurrent = Random.Range(waitMin, waitMax);
		paramTarget = paramMax;
		_animancerState = _animancer.Play(openAnim);
		_animancerState.IsPlaying = false;
	}

	private void LateUpdate()
	{
		if (!isEat && waitCurrent >= 0f && !isOpen)
		{
			waitCurrent -= Time.deltaTime;
			if (waitCurrent <= 0f)
			{
				paramTarget = paramMax;
				isOpen = true;
			}
		}
		if (isEat)
		{
			if (param < 1f)
			{
				param += Time.deltaTime * paramEatSpeed;
				if (param > 1f)
				{
					param = 1f;
					if (OnomatopoeiaManager.instance.useOtomanopoeia)
					{
						OnomatopoeiaManager.instance.SpawnOnomatopoeia(base.transform.position, null, "EaterOpen", Camera.main.transform);
					}
				}
			}
		}
		else if (isOpen)
		{
			if (param < paramTarget)
			{
				param += Time.deltaTime * paramSpeed;
				if (param > paramTarget)
				{
					isDrool = true;
					paramTarget = 0f;
					SpawnDrool();
					if (OnomatopoeiaManager.instance.useOtomanopoeia)
					{
						OnomatopoeiaManager.instance.SpawnOnomatopoeia(base.transform.position, null, "EaterOpen", Camera.main.transform);
					}
				}
			}
			else if (param > paramTarget)
			{
				param -= Time.deltaTime * paramSpeed;
				if (param < paramTarget)
				{
					isDrool = false;
					isOpen = false;
					param = 0f;
					waitCurrent = Random.Range(waitMin, waitMax);
				}
			}
		}
		float length = openAnim.length;
		float num = Mathf.Clamp(param, 0f, 1f);
		_animancerState.Time = num * length;
	}

	public void SetEat(bool value)
	{
		isEat = value;
		if (!isEat)
		{
			isDrool = false;
			isOpen = true;
			paramTarget = 0f;
			waitCurrent = Random.Range(waitMin, waitMax);
		}
	}

	public void SpawnDrool()
	{
		Object.Instantiate(droorEffect, spawnPoint.position, spawnPoint.rotation, spawnPoint.transform);
		EffectSeManager.instance.PlaySe(droolSe[Random.Range(0, droolSe.Count)]);
	}
}
