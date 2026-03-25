using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class GuildCameraManager : MonoBehaviour
{
	public GuildCore _guildCore;

	public GuildCaracterAnimancer _guildCaracterAnimancer;

	public Transform cameraTransform;

	public CinemachineVirtualCamera _virtualCamera;

	[Header("Camera Point")]
	[Range(0f, 10f)]
	public float cameraPath;

	public List<Transform> cameraPoint;

	[Header("Time")]
	public bool isTime;

	public bool isPaper;

	public bool isLookAway;

	public bool isMasturbation;

	public float currentTime;

	public float timeSpeed = 0.2f;

	[Header("Calc Transform")]
	public int path;

	public int nextPath;

	public Vector3 currentPosition;

	public Quaternion currentRotation;

	public Vector3 calcPosition;

	public Quaternion calcRotation;

	private void Start()
	{
	}

	public void StartSet()
	{
		currentPosition = cameraPoint[0].position;
		currentRotation = cameraPoint[0].rotation;
		cameraTransform.transform.position = currentPosition;
		cameraTransform.transform.rotation = currentRotation;
	}

	private void LateUpdate()
	{
		if (isTime)
		{
			currentTime += Time.deltaTime * timeSpeed;
			if (!isPaper && currentTime > 2f)
			{
				currentTime = 2f;
				isTime = false;
				isPaper = true;
			}
			if (!isLookAway && currentTime > 3f)
			{
				isLookAway = true;
				_guildCaracterAnimancer.SetAnimationClip("LookAway");
			}
			if (!isMasturbation && currentTime > (float)(cameraPoint.Count - 1))
			{
				currentTime = cameraPoint.Count;
				isTime = false;
				isMasturbation = true;
			}
			cameraPath = currentTime;
			path = Mathf.FloorToInt(cameraPath);
			nextPath = path + 1;
			if (cameraPath < (float)(cameraPoint.Count - 1))
			{
				float t = cameraPath - (float)path;
				calcPosition = Vector3.Lerp(cameraPoint[path].position, cameraPoint[nextPath].position, t);
				calcRotation = Quaternion.Slerp(cameraPoint[path].rotation, cameraPoint[nextPath].rotation, t);
				currentPosition = Vector3.Lerp(currentPosition, calcPosition, Time.deltaTime * 5f);
				currentRotation = Quaternion.Slerp(currentRotation, calcRotation, Time.deltaTime * 5f);
				cameraTransform.transform.position = currentPosition;
				cameraTransform.transform.rotation = currentRotation;
			}
		}
		else if (isMasturbation)
		{
			if (currentPosition != calcPosition)
			{
				currentPosition = Vector3.Lerp(currentPosition, calcPosition, Time.deltaTime * 5f);
				currentRotation = Quaternion.Slerp(currentRotation, calcRotation, Time.deltaTime * 5f);
				cameraTransform.transform.position = currentPosition;
				cameraTransform.transform.rotation = currentRotation;
			}
		}
		else if (isPaper && currentPosition != calcPosition)
		{
			currentPosition = Vector3.Lerp(currentPosition, calcPosition, Time.deltaTime * 5f);
			currentRotation = Quaternion.Slerp(currentRotation, calcRotation, Time.deltaTime * 5f);
			cameraTransform.transform.position = currentPosition;
			cameraTransform.transform.rotation = currentRotation;
		}
	}
}
