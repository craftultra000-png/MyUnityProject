using UnityEngine;

public class OnomatopoeiaParticleCollider : MonoBehaviour
{
	[Header("Effect Type")]
	public string type = "SpermDrop0";

	[Header("Spawn")]
	[Range(0f, 1f)]
	public float onomatopoeiaChance = 0.1f;

	public int maxOnomatopoeia = 5;

	public int currentCount;

	[Header("Wait Time")]
	public float currentTime;

	public float spawnTime = 0.5f;

	private void Start()
	{
		currentTime = Time.time;
	}

	private void OnParticleCollision(GameObject other)
	{
		if (currentCount < maxOnomatopoeia && !(Time.time - currentTime < spawnTime) && onomatopoeiaChance < Random.value)
		{
			Vector3 position = other.transform.position;
			OnomatopoeiaManager.instance.SpawnOnomatopoeia(position, null, type, Camera.main.transform);
			currentCount++;
		}
	}
}
