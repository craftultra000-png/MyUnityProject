using System.Collections.Generic;
using UnityEngine;

public class FeelerShotObject : MonoBehaviour
{
	public FeelerShotAnimation _feelerShotAnimation;

	[Header("Status")]
	public bool isShot;

	public float coolTime;

	public float coolTimeMax = 0.25f;

	public int shotCount = 3;

	[Header("ShotData")]
	public float shotSpeed = 4f;

	public Vector3 shotForward;

	public Vector3 shotUp;

	public Vector3 shotCalc;

	public float shotNoise = 5f;

	[Header("Shot")]
	public int meltPower;

	public GameObject shotEffect;

	public List<GameObject> meltEffect;

	public Transform shotPoint;

	public Transform shotStocker;

	public Vector3 adujustRotation = new Vector3(90f, -90f, 0f);

	public Vector3 calcRotation;

	[Header("Se")]
	public List<AudioClip> shotSe;

	private void Start()
	{
	}

	private void LateUpdate()
	{
		if (isShot)
		{
			coolTime -= Time.deltaTime;
			if (coolTime < 0f)
			{
				isShot = false;
			}
		}
	}

	public void SetMeltPower(int value)
	{
		meltPower = value;
		if (meltPower >= meltEffect.Count)
		{
			meltPower = meltEffect.Count - 1;
		}
		_feelerShotAnimation.SetMeltPower(meltPower);
	}

	public void ShotReady()
	{
		if (!isShot)
		{
			isShot = true;
			coolTime = coolTimeMax;
			_feelerShotAnimation.ShotReady();
			for (int i = 1; i <= 3; i++)
			{
				SkillGUIDataBase.instance.SetCoolTime(i, coolTimeMax);
			}
		}
	}

	public void Shot()
	{
		EffectSeManager.instance.PlaySe(shotSe[Random.Range(0, shotSe.Count)]);
		GameObject obj = Object.Instantiate(meltEffect[meltPower], shotPoint.position, shotPoint.rotation, shotStocker);
		Rigidbody component = obj.GetComponent<Rigidbody>();
		shotForward = shotPoint.forward.normalized;
		shotCalc = shotForward * shotSpeed;
		shotCalc += new Vector3(Random.Range(0f - shotNoise, shotNoise), Random.Range(0f - shotNoise, shotNoise), Random.Range(0f - shotNoise, shotNoise));
		component.AddForce(shotCalc);
		obj.GetComponent<ShotObject>().shotStocker = shotStocker;
		obj.SetActive(value: true);
	}
}
