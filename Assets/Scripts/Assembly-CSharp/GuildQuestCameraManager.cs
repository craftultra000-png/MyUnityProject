using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class GuildQuestCameraManager : MonoBehaviour
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
		currentPosition = cameraPoint[0].position;
		currentRotation = cameraPoint[0].rotation;
		cameraTransform.transform.position = currentPosition;
		cameraTransform.transform.rotation = currentRotation;
		_guildCaracterAnimancer.SetAnimationClip("Idle");
	}

	private void LateUpdate()
	{
		if (isTime)
		{
			currentTime += Time.deltaTime * timeSpeed;
			if (!isPaper && currentTime > 3f)
			{
				currentTime = 3f;
				isTime = false;
				isPaper = true;
				_guildCore.GuildEvent("MoveCamera2");
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
		else if (isPaper && currentPosition != calcPosition)
		{
			currentPosition = Vector3.Lerp(currentPosition, calcPosition, Time.deltaTime * 5f);
			currentRotation = Quaternion.Slerp(currentRotation, calcRotation, Time.deltaTime * 5f);
			cameraTransform.transform.position = currentPosition;
			cameraTransform.transform.rotation = currentRotation;
		}
	}
}
