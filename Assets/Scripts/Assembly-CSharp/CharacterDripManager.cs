using UnityEngine;

public class CharacterDripManager : MonoBehaviour
{
	public string onomatopoeiaType = "Blood";

	[Header("Status")]
	public bool isSpawn;

	[Header("Effect")]
	public GameObject onomatopoeiaDrip;

	public Transform shotStocker;

	[Header("Effect Link")]
	public GameObject effectLink;

	[Header("Time")]
	public float spawnTime;

	public float spawnTimeMin = 2f;

	public float spawnTimeMax = 4f;

	private void Start()
	{
		spawnTime = 0.1f;
	}

	private void LateUpdate()
	{
		if (isSpawn && (bool)effectLink)
		{
			if (spawnTime > 0f)
			{
				spawnTime -= Time.deltaTime;
				if (spawnTime < 0f)
				{
					spawnTime = Random.Range(spawnTimeMin, spawnTimeMax);
					SpawnDrip();
				}
			}
		}
		else if (isSpawn && !effectLink)
		{
			isSpawn = false;
		}
	}

	private void SpawnDrip()
	{
		if (OnomatopoeiaManager.instance.useOtomanopoeia)
		{
			Object.Instantiate(onomatopoeiaDrip, base.transform.position, Quaternion.identity, shotStocker).GetComponent<OnomatopoeiaSperm>().shotStocker = shotStocker;
		}
	}
}
