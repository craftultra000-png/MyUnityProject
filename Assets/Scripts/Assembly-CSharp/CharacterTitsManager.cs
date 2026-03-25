using System.Collections.Generic;
using UnityEngine;

public class CharacterTitsManager : MonoBehaviour
{
	public CharacterLifeManager _characterLifeManager;

	[Header("Status")]
	public bool isPress;

	public bool isRelease;

	[Header("Transform")]
	public Transform titsCenter;

	public Transform boneTitsL;

	public Transform boneTitsR;

	[Header("Data")]
	public float currentTime;

	public float timeSpeed = 2f;

	public float timeReleaseSpeed = 0.5f;

	public float releaseTime;

	public float releaseTimeMin = 5f;

	public float releaseTimeMax = 15f;

	[Header("Rotation")]
	public Vector3 currentRotationTits;

	public Vector3 targetRotationTits;

	[Header("Position")]
	public Vector3 currentPositionTits;

	public Vector3 targetPositionTits;

	[Header("Se")]
	public List<AudioClip> pressSe;

	private void Start()
	{
		releaseTime = Random.Range(releaseTimeMin, releaseTimeMax);
	}

	private void LateUpdate()
	{
		if (isPress)
		{
			if (releaseTime > 0f)
			{
				releaseTime -= Time.deltaTime;
				if (releaseTime <= 0f)
				{
					isRelease = true;
				}
				if (currentTime < 1f)
				{
					currentTime += Time.deltaTime * timeSpeed;
					if (currentTime > 1f)
					{
						currentTime = 1f;
						TitsPress();
					}
				}
			}
			else
			{
				currentTime -= Time.deltaTime * timeReleaseSpeed;
				if (currentTime < 0f)
				{
					currentTime = 0f;
					isRelease = false;
					releaseTime = Random.Range(releaseTimeMin, releaseTimeMax);
				}
			}
		}
		else if (currentTime > 0f)
		{
			currentTime -= Time.deltaTime * timeSpeed;
			if (currentTime < 0f)
			{
				currentTime = 0f;
			}
		}
		currentRotationTits = Vector3.Lerp(Vector3.zero, targetRotationTits, currentTime);
		currentPositionTits = Vector3.Lerp(Vector3.zero, targetPositionTits, currentTime);
		boneTitsL.rotation *= Quaternion.Euler(currentRotationTits);
		boneTitsL.position += currentPositionTits;
		boneTitsR.rotation *= Quaternion.Euler(currentRotationTits);
		boneTitsR.position -= currentPositionTits;
	}

	public void SetPress()
	{
		isPress = true;
		releaseTime = Random.Range(releaseTimeMin, releaseTimeMax);
	}

	public void TitsPress()
	{
		_characterLifeManager.HitData("Tits", "Press");
		EffectSeManager.instance.PlaySe(pressSe[Random.Range(0, pressSe.Count)]);
		OnomatopoeiaManager.instance.SpawnOnomatopoeia(titsCenter.transform.position, null, "Bounce", Camera.main.transform);
	}
}
