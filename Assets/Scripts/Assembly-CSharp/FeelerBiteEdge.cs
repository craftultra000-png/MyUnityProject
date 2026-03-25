using System.Collections.Generic;
using UnityEngine;

public class FeelerBiteEdge : MonoBehaviour
{
	public CharacterAnimatorIK _characterAnimatorIK;

	public FeelerBiteObject _feelerBiteObject;

	[Header("Bone")]
	public List<Transform> biteBone;

	public List<float> openAngle;

	public List<float> closeAngle;

	[Header("Link")]
	public bool master;

	[Header("Status")]
	public bool isBite;

	public bool isBiteEnd;

	public bool isHit;

	public bool isOpen;

	[Header("Time")]
	public float currentTime;

	public float openSpeed;

	public float closeSpeed;

	[Header("Curve")]
	public int biteType;

	public float biteTime;

	public List<AnimationCurve> biteCurve;

	[Header("Calc")]
	public List<float> currentAngle;

	public List<Quaternion> currentRotation;

	public Quaternion calcRotation;

	private void Start()
	{
		isOpen = true;
		currentTime = 0.1f;
		for (int i = 0; i < biteBone.Count; i++)
		{
			currentRotation[i] = biteBone[i].localRotation;
			closeAngle[i] += biteBone[i].localRotation.x;
			currentAngle[i] = closeAngle[i];
			openAngle[i] += closeAngle[i];
			if (i == 4)
			{
				closeAngle[i] += -10f;
			}
		}
		_characterAnimatorIK._multiBiteEdge.Add(this);
	}

	private void FixedUpdate()
	{
		if (isBite && !isBiteEnd)
		{
			currentTime += Time.deltaTime * closeSpeed;
			if (currentTime > 1f)
			{
				currentTime = 1f;
				isBite = false;
				isBiteEnd = true;
				if (master)
				{
					_feelerBiteObject.MultiBite();
				}
			}
		}
		else if (isOpen && currentTime > 0f)
		{
			currentTime -= Time.deltaTime * openSpeed;
			if (currentTime <= 0f)
			{
				isOpen = false;
				currentTime = 0f;
			}
		}
	}

	public void MagicaAnimator()
	{
		if (!isHit)
		{
			for (int i = 0; i < biteBone.Count; i++)
			{
				biteTime = biteCurve[biteType].Evaluate(currentTime);
				currentAngle[i] = Mathf.Lerp(openAngle[i], closeAngle[i], biteTime);
				Rotation(i);
			}
		}
	}

	private void Rotation(int value)
	{
		calcRotation = currentRotation[value] * Quaternion.Euler(currentAngle[value], 0f, 0f);
		biteBone[value].localRotation = calcRotation;
	}

	public void SetBite(int value)
	{
		isBite = true;
		isBiteEnd = false;
		isHit = false;
		isOpen = false;
		biteType = value;
	}

	public void ResetBite()
	{
		isBite = false;
		isBiteEnd = false;
		isHit = false;
		isOpen = true;
	}
}
