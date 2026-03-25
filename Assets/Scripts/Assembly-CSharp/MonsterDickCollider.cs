using UnityEngine;

public class MonsterDickCollider : MonoBehaviour
{
	public MonsterDickManager _monsterDickManager;

	[Header("Status")]
	public bool isEnter;

	[Header("Radius")]
	public SphereCollider _collider;

	public float currentSize;

	public float enterSize = 0.2f;

	public float exitSize = 0.4f;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("MainCamera"))
		{
			isEnter = true;
			_monsterDickManager.isErect = isEnter;
			_collider.radius = exitSize;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("MainCamera"))
		{
			isEnter = false;
			_monsterDickManager.isErect = isEnter;
			_collider.radius = enterSize;
		}
	}
}
