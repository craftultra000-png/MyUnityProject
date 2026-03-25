using System.Collections.Generic;
using Animancer;
using UnityEngine;

public class BigTongueManager : MonoBehaviour
{
	public AnimancerComponent _animancer;

	public Animator _animator;

	[Space]
	public CharacterPositionManager _characterPositionManager;

	public CharacterHairManager _characterHairManager;

	public CoralMouthManager _coralMouthManager;

	public CharacterLifeManager _characterLifeManager;

	[Header("Status")]
	public bool isEat;

	[Header("Damage")]
	public float damageTime;

	public float damageTimeMin = 5f;

	public float damageTimeMax = 10f;

	[Header("Spawn Drool")]
	public Transform droolSpawnPoint;

	public GameObject onomatopoeiaSperm;

	public Transform shotStocker;

	public float droolTime;

	public float droolTimeMin = 0.5f;

	public float droolTimeMax = 5f;

	[Header("Eatingl")]
	public Transform eatingPoint;

	[Header("Rotation")]
	public float rotationSpeed = 5f;

	public float currentRotation;

	public Vector3 calcRotation;

	[Header("Anim Time")]
	public float currentTime;

	public float minTime = 5f;

	public float maxTime = 15f;

	[Header("Anim")]
	public float feedTime = 0.5f;

	public List<AnimationClip> idleClip;

	public AnimationClip eatClip;

	public AnimationClip eattingClip;

	public AnimationClip releaseClip;

	[Header("Se")]
	public List<AudioClip> eatSe;

	public List<AudioClip> eattingSe;

	public List<AudioClip> releaseSe;

	private void Start()
	{
		calcRotation = base.transform.localRotation.eulerAngles;
		currentRotation = calcRotation.y;
		droolTime = droolTimeMax;
	}

	private void LateUpdate()
	{
		if (!isEat)
		{
			currentTime -= Time.deltaTime;
			if (currentTime < 0f)
			{
				ChangeAnim();
				currentTime = Random.Range(minTime, maxTime);
			}
			currentRotation += rotationSpeed * Time.deltaTime;
			if (currentRotation > 360f)
			{
				currentRotation -= 360f;
			}
			calcRotation.y = currentRotation;
			base.transform.rotation = Quaternion.Euler(calcRotation);
			if (OnomatopoeiaManager.instance.useOtomanopoeia)
			{
				droolTime -= Time.deltaTime;
				if (droolTime <= 0f)
				{
					droolTime = Random.Range(droolTimeMin, droolTimeMax);
					Object.Instantiate(onomatopoeiaSperm, droolSpawnPoint.position, Quaternion.identity, shotStocker);
				}
			}
		}
		else
		{
			damageTime -= Time.deltaTime;
			if (damageTime <= 0f)
			{
				damageTime = Random.Range(damageTimeMin, damageTimeMax);
				_characterLifeManager.HitData("Reaction", "Reaction");
			}
		}
	}

	public void ChangeAnim()
	{
		_animancer.Play(idleClip[Random.Range(0, idleClip.Count)], feedTime);
	}

	public void EatAnim()
	{
		isEat = true;
		_coralMouthManager.SetEat(value: true);
		_animancer.Play(eatClip, feedTime).Events(this).OnEnd = delegate
		{
			_animancer.Play(eattingClip, feedTime);
		};
	}

	public void EatingSe()
	{
		EffectSeManager.instance.PlaySe(eattingSe[Random.Range(0, eattingSe.Count)]);
		if (OnomatopoeiaManager.instance.useOtomanopoeia)
		{
			OnomatopoeiaManager.instance.SpawnOnomatopoeia(eatingPoint.position, null, "EaterEating", Camera.main.transform);
		}
	}

	public void ReleaseAnim()
	{
		_animancer.Play(releaseClip, feedTime).Events(this).OnEnd = delegate
		{
			_animancer.Play(idleClip[Random.Range(0, idleClip.Count)], feedTime);
			isEat = false;
			_coralMouthManager.SetEat(value: false);
			currentTime = Random.Range(minTime, maxTime);
		};
	}

	public void Eat()
	{
		_characterPositionManager.SetEatBody();
		_characterHairManager.isEat = true;
		EffectSeManager.instance.PlaySe(eatSe[Random.Range(0, eatSe.Count)]);
		if (OnomatopoeiaManager.instance.useOtomanopoeia)
		{
			OnomatopoeiaManager.instance.SpawnOnomatopoeia(eatingPoint.position, null, "EaterEating", Camera.main.transform);
		}
	}

	public void Release()
	{
		_characterPositionManager.SetDefaultBody();
		_characterHairManager.isEat = false;
		EffectSeManager.instance.PlaySe(releaseSe[Random.Range(0, releaseSe.Count)]);
		damageTime = 0f;
		if (OnomatopoeiaManager.instance.useOtomanopoeia)
		{
			OnomatopoeiaManager.instance.SpawnOnomatopoeia(eatingPoint.position, null, "EaterEating", Camera.main.transform);
		}
	}

	public void ReleaseEnd()
	{
		_characterPositionManager.SetDefaultEnd();
	}
}
