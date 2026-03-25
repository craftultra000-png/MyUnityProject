using System.Collections.Generic;
using UnityEngine;

public class FilamentObject : MonoBehaviour
{
	public List<Transform> bone1;

	public List<Transform> bone2;

	public float currentAngle;

	public float targetAngle;

	public float limitAngle = 30f;

	public float angleSpeed = 5f;

	private void LateUpdate()
	{
		currentAngle = Mathf.PingPong(Time.time * angleSpeed, limitAngle);
		for (int i = 0; i < bone1.Count; i++)
		{
			bone1[i].localRotation = Quaternion.Euler(0f, 0f, currentAngle);
		}
		for (int j = 0; j < bone2.Count; j++)
		{
			bone2[j].localRotation = Quaternion.Euler(0f, 0f, currentAngle);
		}
	}
}
