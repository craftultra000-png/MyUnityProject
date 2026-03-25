using System.Collections.Generic;
using UnityEngine;

public class FeelerShotMob : MonoBehaviour
{
	[Header("Status")]
	public bool isAuto;

	public bool isVolley;

	public bool isStop;

	public float waitTime;

	public float waitTimeMin = 5f;

	public float waitTimeMax = 20f;

	[Header("Status")]
	public GameObject shotEffect;

	public Transform shotStocker;

	public Transform shotPoint;

	public Vector3 adujustRotation = new Vector3(-90f, 0f, 0f);

	public Vector3 calcRotation;

	[Header("Shot")]
	public int shotCount = 2;

	public float shotWait = 0.1f;

	public int meltPower;

	public List<GameObject> meltEffect;

	[Header("ShotData")]
	public float shotSpeed = 4f;

	public float shotSpeedNoise = 1f;

	public Vector3 shotForward;

	public Vector3 shotUp;

	public Vector3 shotCalc;

	public float shotNoise = 5f;

	[Header("Se")]
	public List<AudioClip> shotSe;

	private void Start()
	{
		waitTime = Random.Range(waitTimeMin, waitTimeMax);
	}

	private void LateUpdate()
	{
		if (isAuto || isVolley)
		{
			waitTime -= Time.deltaTime;
			if (waitTime < 0f)
			{
				isVolley = false;
				Shot(sound: true);
			}
		}
	}

	public void Shot(bool sound)
	{
		if (!isStop)
		{
			waitTime = Random.Range(waitTimeMin, waitTimeMax);
			if (sound)
			{
				EffectSeManager.instance.PlaySe(shotSe[Random.Range(0, shotSe.Count)]);
			}
			for (int i = 0; i < shotCount; i++)
			{
				GameObject obj = Object.Instantiate(meltEffect[meltPower], shotPoint.position, shotPoint.rotation, shotStocker);
				Rigidbody component = obj.GetComponent<Rigidbody>();
				shotForward = shotPoint.forward.normalized;
				shotCalc = shotForward * (shotSpeed + Random.Range(0f - shotSpeedNoise, shotSpeedNoise));
				shotCalc += new Vector3(Random.Range(0f - shotNoise, shotNoise), Random.Range(0f - shotNoise, shotNoise), Random.Range(0f - shotNoise, shotNoise));
				component.AddForce(shotCalc);
				ShotObject component2 = obj.GetComponent<ShotObject>();
				component2.shotStocker = shotStocker;
				component2.attackType = "BukkakeMob";
				obj.SetActive(value: true);
			}
		}
	}
}
