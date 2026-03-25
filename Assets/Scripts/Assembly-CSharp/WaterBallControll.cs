using UnityEngine;

public class WaterBallControll : MonoBehaviour
{
	[SerializeField]
	private bool _update;

	[SerializeField]
	private Transform _CreationPoint;

	[SerializeField]
	private WaterBall WaterBallPrefab;

	private WaterBall waterBall;

	private void Update()
	{
		if (_update && Input.GetMouseButtonDown(0))
		{
			RaycastHit hitInfo;
			if (WaterBallCreated())
			{
				CreateWaterBall();
			}
			else if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo) && waterBall != null)
			{
				ThrowWaterBall(hitInfo.point);
			}
		}
	}

	public bool WaterBallCreated()
	{
		return waterBall != null;
	}

	public void CreateWaterBall()
	{
		waterBall = Object.Instantiate(WaterBallPrefab, _CreationPoint.position, Quaternion.identity);
	}

	public void ThrowWaterBall(Vector3 pos)
	{
		waterBall.Throw(pos);
	}
}
