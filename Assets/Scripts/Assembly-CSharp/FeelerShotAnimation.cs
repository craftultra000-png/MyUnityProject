using System.Collections.Generic;
using UnityEngine;

public class FeelerShotAnimation : MonoBehaviour
{
	public FeelerShotObject _feelerShotObject;

	public Animator _animator;

	[Header("Shot NearPaint")]
	public int meltPower;

	public List<GameObject> nearPaintObject;

	private void Start()
	{
		ShotEnd();
	}

	public void ShotReady()
	{
		_animator.SetBool("isShot", value: true);
		nearPaintObject[meltPower].SetActive(value: true);
	}

	public void Shot()
	{
		_feelerShotObject.Shot();
	}

	public void ShotEnd()
	{
		for (int i = 0; i < nearPaintObject.Count; i++)
		{
			nearPaintObject[i].SetActive(value: false);
		}
	}

	public void SetMeltPower(int value)
	{
		meltPower = value;
	}
}
