using System.Collections.Generic;
using UnityEngine;

public class FilamentTouchManager : MonoBehaviour
{
	public List<FilamentTouchTarget> _filamentTouchTarget;

	public List<FilamentTouchObject> _filamentTouchObject;

	public List<Transform> filamentDefaultPosition;

	public List<FeelerNoisePosition> filamentBendObject;

	[Header("Target")]
	public List<Transform> targetFrontL;

	public List<Transform> targetFrontR;

	public List<Transform> targetBackL;

	public List<Transform> targetBackR;

	[Header("Status")]
	public bool isSearch;

	public bool isSet;

	[Header("Data")]
	public List<float> currentHeight;

	public List<float> currentBend;

	[Space]
	public List<float> targetHeight;

	public List<float> targetBend;

	[Space]
	public float targetHeightMin = 0.5f;

	public float targetHeightMax = 0.75f;

	public float targetBendMin = -0.5f;

	public float targetBendMax = -0.3f;

	[Space]
	public float heightMoveSpeed = 2f;

	public float bendMoveInSpeed = 0.1f;

	public float bendMoveOutSpeed = 1f;

	[Header("Time")]
	public List<float> currentTime;

	public float timeMin = 5f;

	public float timeMax = 15f;

	[Header("Clc")]
	public List<Vector3> calcDefaultPosition;

	public List<Vector3> calcBendPosition;

	public Vector3 calcPosition;

	private void Awake()
	{
		currentHeight.Clear();
		currentBend.Clear();
		targetHeight.Clear();
		targetBend.Clear();
		for (int i = 0; i < filamentDefaultPosition.Count; i++)
		{
			currentHeight.Add(0.01f);
			currentBend.Add(0.01f);
			targetHeight.Add(Random.Range(targetHeightMin, targetHeightMax));
			targetBend.Add(Random.Range(targetBendMin, targetBendMax));
		}
		calcDefaultPosition.Clear();
		for (int j = 0; j < filamentDefaultPosition.Count; j++)
		{
			calcDefaultPosition.Add(filamentDefaultPosition[j].localPosition);
			calcPosition = calcDefaultPosition[j];
			calcPosition.y = currentHeight[j];
			calcDefaultPosition[j] = calcPosition;
			filamentDefaultPosition[j].localPosition = calcDefaultPosition[j];
		}
		calcBendPosition.Clear();
		for (int k = 0; k < filamentBendObject.Count; k++)
		{
			calcBendPosition.Add(filamentBendObject[k].transform.localPosition);
			calcPosition = calcBendPosition[k];
			calcPosition.x = currentBend[k];
			calcBendPosition[k] = calcPosition;
			filamentBendObject[k].transform.localPosition = calcBendPosition[k];
			filamentBendObject[k].defaultPosition = calcBendPosition[k];
		}
		currentTime.Clear();
		for (int l = 0; l < filamentDefaultPosition.Count; l++)
		{
			currentTime.Add(Random.Range(timeMin, timeMax));
		}
	}

	private void LateUpdate()
	{
		if (isSearch)
		{
			for (int i = 0; i < filamentDefaultPosition.Count; i++)
			{
				currentTime[i] -= Time.deltaTime;
				if (currentTime[i] < 0f)
				{
					currentTime[i] = Random.Range(timeMin, timeMax);
					targetHeight[i] = Random.Range(targetHeightMin, targetHeightMax);
					targetBend[i] = Random.Range(targetBendMin, targetBendMax);
				}
				if (currentHeight[i] < targetHeight[i])
				{
					currentHeight[i] += Time.deltaTime * heightMoveSpeed;
					if (currentHeight[i] > targetHeight[i])
					{
						currentHeight[i] = targetHeight[i];
					}
				}
				else if (currentHeight[i] > targetHeight[i])
				{
					currentHeight[i] -= Time.deltaTime * heightMoveSpeed;
					if (currentHeight[i] < targetHeight[i])
					{
						currentHeight[i] = targetHeight[i];
					}
				}
				if (currentBend[i] < targetBend[i])
				{
					currentBend[i] += Time.deltaTime * bendMoveInSpeed;
					if (currentBend[i] > targetBend[i])
					{
						currentBend[i] = targetBend[i];
					}
				}
				else if (currentBend[i] > targetBend[i])
				{
					currentBend[i] -= Time.deltaTime * bendMoveInSpeed;
					if (currentBend[i] < targetBend[i])
					{
						currentBend[i] = targetBend[i];
					}
				}
			}
		}
		else
		{
			for (int j = 0; j < filamentDefaultPosition.Count; j++)
			{
				if (currentHeight[j] > 0f)
				{
					currentHeight[j] -= Time.deltaTime * heightMoveSpeed;
					if (currentHeight[j] < 0f)
					{
						currentHeight[j] = 0f;
					}
				}
				if (currentBend[j] < 0f)
				{
					currentBend[j] += Time.deltaTime * bendMoveOutSpeed;
					if (currentBend[j] > 0f)
					{
						currentBend[j] = 0f;
					}
				}
			}
		}
		for (int k = 0; k < filamentDefaultPosition.Count; k++)
		{
			calcPosition = calcDefaultPosition[k];
			if (calcPosition.y != currentHeight[k])
			{
				calcPosition.y = currentHeight[k];
				filamentDefaultPosition[k].localPosition = calcPosition;
			}
		}
		for (int l = 0; l < filamentBendObject.Count; l++)
		{
			calcPosition = calcBendPosition[l];
			if (calcPosition.x != currentBend[l])
			{
				calcPosition.x = currentBend[l];
				filamentBendObject[l].transform.localPosition = calcPosition;
				filamentBendObject[l].defaultPosition = calcPosition;
			}
		}
	}

	public void SetSearch(bool value)
	{
		isSearch = value;
		if (value)
		{
			for (int i = 0; i < _filamentTouchObject.Count; i++)
			{
				_filamentTouchTarget[i].isSearch = true;
				_filamentTouchObject[i].isSearch = true;
				_filamentTouchTarget[i].ChangeTarget();
			}
		}
		else
		{
			for (int j = 0; j < _filamentTouchObject.Count; j++)
			{
				_filamentTouchTarget[j].isSearch = false;
				_filamentTouchObject[j].isSearch = false;
			}
		}
	}
}
