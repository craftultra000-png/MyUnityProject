using System.Collections.Generic;
using UnityEngine;

public class ParticleHitStop : MonoBehaviour
{
	public GameObject hitEffect;

	public float spawnRate = 0.5f;

	private ParticleSystem _particle;

	private List<ParticleCollisionEvent> collisionEventList;

	private void Start()
	{
		_particle = GetComponent<ParticleSystem>();
		collisionEventList = new List<ParticleCollisionEvent>();
	}

	private void OnParticleCollision(GameObject other)
	{
		if (!(other.tag != "Effect") || !(Random.Range(0f, 1f) <= spawnRate))
		{
			return;
		}
		collisionEventList.Clear();
		_particle.GetCollisionEvents(other, collisionEventList);
		foreach (ParticleCollisionEvent collisionEvent in collisionEventList)
		{
			Vector3 intersection = collisionEvent.intersection;
			Object.Instantiate(hitEffect, intersection, Quaternion.identity, other.transform);
		}
	}
}
