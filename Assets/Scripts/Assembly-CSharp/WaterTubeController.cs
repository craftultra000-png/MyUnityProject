using UnityEngine;

public class WaterTubeController : MonoBehaviour
{
	[SerializeField]
	private bool _update;

	[SerializeField]
	private Transform _CreationPoint;

	[SerializeField]
	private GameObject _WaterTubePrefab;

	private void Update()
	{
		if (_update && Input.GetMouseButtonDown(0) && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out var hitInfo))
		{
			InstantiateWaterTube(hitInfo.point);
		}
	}

	public void InstantiateWaterTube(Vector3 hitPoint)
	{
		Vector3 vector = hitPoint - _CreationPoint.position;
		vector.y = 0f;
		vector = vector.normalized;
		Object.Instantiate(_WaterTubePrefab, _CreationPoint.transform.position, Quaternion.identity).transform.forward = vector;
	}
}
