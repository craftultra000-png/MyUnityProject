using System.Collections.Generic;
using UnityEngine;

public class SlimeUnionManager : MonoBehaviour
{
	[Header("Transform")]
	public Transform centerTransform;

	public List<Transform> baseTransform;

	public List<Transform> targetTransform;

	public List<Transform> tmpTransform;

	public List<Vector3> adjustPosition;

	public List<SlimeHoldObject> slimeScript;

	[Header("Setting")]
	public float radius = 1f;

	public float followSpeed = 5f;

	public float heightRange = 0.75f;

	[Header("Status")]
	public bool isStage;

	public Vector3 currentStagePosition;

	public float heightStageCurrent;

	public float heightStageMin = -0.2f;

	public float heightStageMax;

	public float stageLerpSpeed = 5f;

	private void Start()
	{
		isStage = false;
		heightStageCurrent = heightStageMin;
		currentStagePosition.y = heightStageCurrent;
		base.transform.position = currentStagePosition;
	}

	private void LateUpdate()
	{
		if (isStage && heightStageCurrent < heightStageMax)
		{
			heightStageCurrent += Time.deltaTime * stageLerpSpeed;
			if (heightStageCurrent > heightStageMax)
			{
				heightStageCurrent = heightStageMax;
			}
			currentStagePosition.y = heightStageCurrent;
			base.transform.position = currentStagePosition;
		}
		else if (!isStage && heightStageCurrent > heightStageMin)
		{
			heightStageCurrent -= Time.deltaTime * stageLerpSpeed;
			if (heightStageCurrent < heightStageMin)
			{
				heightStageCurrent = heightStageMin;
			}
			currentStagePosition.y = heightStageCurrent;
			base.transform.position = currentStagePosition;
		}
		Vector3 position = centerTransform.position;
		_ = Vector3.ProjectOnPlane(centerTransform.forward, Vector3.up).normalized;
		for (int i = 0; i < baseTransform.Count; i++)
		{
			Transform obj = baseTransform[i];
			Transform transform = targetTransform[i];
			Vector3 position2 = transform.position;
			if (i < adjustPosition.Count)
			{
				position2 += adjustPosition[i];
			}
			Vector3 normalized = Vector3.ProjectOnPlane(position2 - position, Vector3.up).normalized;
			Vector3 vector = obj.position - position;
			Vector3 current = ((!(vector.sqrMagnitude > 0.0001f)) ? normalized : Vector3.ProjectOnPlane(vector, Vector3.up).normalized);
			Vector3 vector2 = Vector3.RotateTowards(current, normalized, followSpeed * Time.deltaTime, 0f);
			obj.position = position + vector2 * radius;
			Quaternion quaternion = Quaternion.LookRotation(vector2, Vector3.up);
			float x = transform.localEulerAngles.x;
			obj.rotation = quaternion * Quaternion.Euler(x, 0f, 0f);
			float y = (obj.position.y + transform.position.y) * heightRange;
			slimeScript[i].bendDefaultPositon.y = y;
		}
	}
}
