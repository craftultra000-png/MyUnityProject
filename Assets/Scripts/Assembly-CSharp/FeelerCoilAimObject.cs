using System.Collections.Generic;
using UnityEngine;

public class FeelerCoilAimObject : MonoBehaviour
{
	public Animator _animator;

	public FeelerCoilObject _feelerCoilObject;

	public FeelerCoilPoint _feelerCoilPoint;

	public List<Transform> baseBones;

	[Header("Target")]
	public Transform targetObject;

	public Transform rootObject;

	[Header("Self")]
	public Transform origin;

	public Transform rootBase;

	public Transform rootEndBase;

	public Transform coilBone;

	[Header("Self")]
	public Transform coilBase;

	[Header("Target")]
	public bool isArm;

	public bool isForearm;

	public bool isLeg;

	public bool isFoot;

	[Header("Skip")]
	public bool isSkip;

	[Header("Status")]
	public bool isAim;

	public bool isAimEnd;

	public bool isCoil;

	[Header("Aim Data")]
	public float aimCurrent;

	public float aimMin = 0.2f;

	public float aimSpeed = 5f;

	[Header("Aim Position")]
	public int boneEndcount;

	[Header("Aim Rotation")]
	public Vector3 defaultRotation;

	public Vector3 calcRotation;

	[Header("Root Feeler")]
	public Vector3 boneCoilRotation;

	public Vector3 boneCoilDefaultRotation;

	[Header("Se")]
	public AudioClip aimSe;

	public AudioClip coilSe;

	public AudioClip uncoilSe;

	private void Start()
	{
		boneEndcount = baseBones.Count - 1;
		aimCurrent = aimMin;
		boneCoilDefaultRotation = coilBase.localRotation.eulerAngles;
		if (isSkip)
		{
			aimCurrent = 1f;
			isAimEnd = true;
			CoilSkip();
		}
	}

	private void FixedUpdate()
	{
		if (!isSkip)
		{
			if (isAim)
			{
				if (!isAimEnd && aimCurrent < 1f)
				{
					if (aimCurrent < 1f)
					{
						aimCurrent += Time.deltaTime * aimSpeed;
						if (aimCurrent > 1f)
						{
							aimCurrent = 1f;
						}
					}
				}
				else if (!isAimEnd && aimCurrent >= 1f)
				{
					isAimEnd = true;
				}
			}
			else
			{
				isAimEnd = false;
			}
		}
		base.transform.position = Vector3.Lerp(rootObject.position, targetObject.position, aimCurrent);
		Vector3 normalized = (rootObject.position - origin.position).normalized;
		Quaternion quaternion = Quaternion.FromToRotation(rootBase.up, normalized);
		rootBase.rotation = quaternion * rootBase.rotation;
		for (int i = 0; i < boneEndcount; i++)
		{
			float num = (float)i / (float)boneEndcount;
			if (num == 0f)
			{
				num = 0.15f;
			}
			baseBones[i].position = Vector3.Lerp(origin.position, rootEndBase.position, num);
		}
		rootEndBase.position = rootObject.position;
	}

	public void SetAim(bool value)
	{
		isAim = value;
		if (isAim)
		{
			EffectSeManager.instance.PlaySe(aimSe);
		}
	}

	public void CoilSkip()
	{
		isSkip = true;
		Coil(value: true);
	}

	public void Coil(bool value)
	{
		isCoil = value;
		isArm = value;
		_animator.SetBool("isSkip", isSkip);
		_animator.SetBool("isCoil", isCoil);
		_animator.SetBool("isArm", isArm);
		_animator.SetBool("isForearm", isForearm);
		_animator.SetBool("isLeg", isLeg);
		_animator.SetBool("isFoot", isFoot);
	}

	public void SetCoil(bool value)
	{
		if (value)
		{
			Coil(value: true);
			EffectSeManager.instance.PlaySe(coilSe);
		}
		else
		{
			Coil(value: false);
			EffectSeManager.instance.PlaySe(uncoilSe);
		}
	}
}
