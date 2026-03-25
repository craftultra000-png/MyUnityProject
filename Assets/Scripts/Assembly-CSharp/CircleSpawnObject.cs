using UnityEngine;

public class CircleSpawnObject : MonoBehaviour
{
	public GameObject onomatopoeiaSperm;

	public Transform shotStocker;

	[Header("Spawn Position")]
	public bool isGizmos;

	public float radius = 1.5f;

	public float radiusThickness = 0.5f;

	private Vector2 pos;

	private float dist;

	private Vector3 spawnPos;

	[Header("Spawn Time")]
	public int spawnCount = 10;

	public float spawnTime;

	public float spawnTimeMin = 2f;

	public float spawnTimeMax = 10f;

	private void Start()
	{
		spawnTime = spawnTimeMax;
	}

	private void OnDrawGizmosSelected()
	{
		if (isGizmos)
		{
			Gizmos.color = Color.green;
			Gizmos.DrawWireSphere(base.transform.position, radius);
			Gizmos.color = Color.green;
			Gizmos.DrawWireSphere(base.transform.position, radiusThickness);
		}
	}

	private void LateUpdate()
	{
		spawnTime -= Time.deltaTime;
		if (spawnTime < 0f)
		{
			spawnTime = Random.Range(spawnTimeMin, spawnTimeMax);
			SpawnObject();
		}
	}

	public void SpawnObject()
	{
		if (base.transform.childCount < spawnCount)
		{
			pos = Random.insideUnitCircle.normalized;
			dist = Random.Range(radiusThickness, radius);
			spawnPos = base.transform.position + new Vector3(pos.x, 0f, pos.y) * dist;
			Object.Instantiate(onomatopoeiaSperm, spawnPos, Quaternion.identity, shotStocker).GetComponent<OnomatopoeiaSperm>().shotStocker = shotStocker;
		}
	}
}
