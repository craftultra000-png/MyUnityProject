using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class BriefingCameraManager : MonoBehaviour
{
	public BriefingCore _brieifngCore;

	public BriefingCaracterAnimancer _briefingCaracterAnimancer;

	public Transform cameraTransform;

	public CinemachineVirtualCamera _virtualCamera;

	[Header("Camera Point")]
	public List<Transform> cameraPoint;

	[Header("Move")]
	public bool isMove;

	public int currentPoint;

	public int targetPoint;

	public float currentTime;

	public float timeSpeed = 0.2f;

	[Header("Calc Transform")]
	public Vector3 currentPosition;

	public Quaternion currentRotation;

	public Vector3 targetPosition;

	public Quaternion targetRotation;

	public Vector3 calcPosition;

	public Quaternion calcRotation;

	private void Start()
	{
		currentPosition = cameraPoint[0].position;
		currentRotation = cameraPoint[0].rotation;
		targetPosition = currentPosition;
		targetRotation = currentRotation;
		targetPoint = 0;
		cameraTransform.transform.position = currentPosition;
		cameraTransform.transform.rotation = currentRotation;
	}

	private void LateUpdate()
	{
		if (isMove)
		{
			currentTime += Time.deltaTime * timeSpeed;
			calcPosition = Vector3.Lerp(currentPosition, targetPosition, currentTime);
			calcRotation = Quaternion.Slerp(currentRotation, targetRotation, currentTime);
			cameraTransform.position = calcPosition;
			cameraTransform.rotation = calcRotation;
			if (currentTime >= 1f)
			{
				isMove = false;
				currentTime = 0f;
				currentPosition = targetPosition;
				currentRotation = targetRotation;
				_brieifngCore.SetGUI();
			}
		}
	}

	public void SetTarget(int value)
	{
		targetPoint = value;
		currentPosition = cameraTransform.position;
		currentRotation = cameraTransform.rotation;
		targetPosition = cameraPoint[targetPoint].position;
		targetRotation = cameraPoint[targetPoint].rotation;
		currentTime = 0f;
		isMove = true;
	}
}
