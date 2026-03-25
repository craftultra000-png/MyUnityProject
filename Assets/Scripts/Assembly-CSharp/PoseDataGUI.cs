using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoseDataGUI : MonoBehaviour
{
	public CharacterFacialManager _characterFacialManager;

	public PoseDataGUIObject _poseDataGUIObject;

	public BindManager _bindManager;

	[Header("Status")]
	public bool isRandom;

	public bool isLock;

	[Header("Pose")]
	public MotionAnimancer _motionAnimancer;

	public ReactionAnimancer _reactionAnimancer;

	public RectTransform _currentMark;

	public RectTransform _targetMark;

	public Vector2 poseCurrentParam;

	public Vector2 poseTargetParam;

	[Header("Graph")]
	public float maxPosition = 80f;

	public float moveSpeed = 5f;

	public Vector2 calcPosition;

	[Header("Circle")]
	public int clipCircle = 8;

	public float clipAngle = 45f;

	[Space]
	public float randomRangeMin = 0.75f;

	public float randomRangeMax = 1f;

	[Header("Random Move")]
	public float randomTime;

	public float randomTimeMin = 15f;

	public float randomTimeMax = 60f;

	public float randomSpeed = 0.1f;

	public Vector2 randomTarget;

	[Header("Clip Icon")]
	public List<Vector2> mixParam;

	public List<RectTransform> clipPoint;

	public List<Image> clipIcon;

	public Color defaultColor;

	public Color buttonColor;

	private void Start()
	{
		isRandom = false;
		mixParam = _motionAnimancer.mixParam;
		for (int i = 0; i < mixParam.Count; i++)
		{
			calcPosition = new Vector2(mixParam[i].x * maxPosition, mixParam[i].y * maxPosition);
			clipPoint[i].anchoredPosition = calcPosition;
			clipIcon[i].color = defaultColor;
		}
	}

	private void LateUpdate()
	{
		if (isRandom)
		{
			randomTime -= Time.deltaTime;
			if (randomTime < 0f)
			{
				SetNextRandomTarget();
				_bindManager.ChangePose();
			}
			poseTargetParam = Vector2.MoveTowards(poseTargetParam, randomTarget, Time.deltaTime * randomSpeed);
		}
		calcPosition = new Vector2(poseTargetParam.x * maxPosition, poseTargetParam.y * maxPosition);
		_currentMark.anchoredPosition = calcPosition;
		poseCurrentParam = Vector2.MoveTowards(poseCurrentParam, poseTargetParam, Time.deltaTime * moveSpeed);
		calcPosition = new Vector2(poseCurrentParam.x * maxPosition, poseCurrentParam.y * maxPosition);
		_targetMark.anchoredPosition = calcPosition;
		_motionAnimancer.paramX = poseCurrentParam.x;
		_motionAnimancer.paramY = poseCurrentParam.y;
	}

	public void SetIcon(int value)
	{
		for (int i = 0; i < mixParam.Count; i++)
		{
			if (i == value)
			{
				clipIcon[i].color = buttonColor;
			}
			else
			{
				clipIcon[i].color = defaultColor;
			}
		}
	}

	public void ResetIcon()
	{
		for (int i = 0; i < mixParam.Count; i++)
		{
			clipIcon[i].color = defaultColor;
		}
	}

	public void SetTarget(Vector2 value)
	{
		randomTime = UnityEngine.Random.Range(randomTimeMin, randomTimeMax);
		poseTargetParam = value;
		randomTarget = poseTargetParam;
		Vector2 vector = randomTarget;
		Debug.LogError("Set Target: " + vector.ToString());
	}

	public void SetRandomTarget()
	{
		int num = 0;
		if (randomTarget != Vector2.zero)
		{
			float num2 = Mathf.Atan2(randomTarget.y, randomTarget.x) * 57.29578f;
			if (num2 < 0f)
			{
				num2 += 360f;
			}
			num = Mathf.RoundToInt(num2 / clipAngle) % clipCircle;
		}
		int num3 = ((UnityEngine.Random.value < 0.5f) ? 1 : (-1));
		int num4 = UnityEngine.Random.Range(1, 5);
		float f = (float)((num + num3 * num4 + clipCircle) % clipCircle) * clipAngle * (MathF.PI / 180f);
		float num5 = UnityEngine.Random.Range(randomRangeMin, randomRangeMax);
		poseTargetParam = new Vector2(Mathf.Cos(f) * num5, Mathf.Sin(f) * num5);
		randomTarget = poseTargetParam;
		Vector2 vector = randomTarget;
		Debug.LogError("Set RandomTarget: " + vector.ToString());
	}

	public void SetNextRandomTarget()
	{
		randomTime = UnityEngine.Random.Range(randomTimeMin, randomTimeMax);
		float f = UnityEngine.Random.Range(0f, MathF.PI * 2f);
		float num = UnityEngine.Random.Range(0.5f, 1f);
		float x = Mathf.Cos(f) * num;
		float y = Mathf.Sin(f) * num;
		poseTargetParam = new Vector2(x, y);
		randomTarget = poseTargetParam;
	}

	public void Lock()
	{
		isLock = !isLock;
		_poseDataGUIObject.isLock = isLock;
	}
}
