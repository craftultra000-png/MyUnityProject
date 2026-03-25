using UnityEngine;

public class SpermOnomatopeObject : MonoBehaviour
{
	public GameObject onomatopoeiaSperm;

	public Transform shotStocker;

	[Header("Status")]
	public bool useOnomatopoeia;

	[Header("Bend Onomatopoeia")]
	public float onomatopoeiaTime;

	public float onomatopoeiaTimeMin = 2f;

	public float onomatopoeiaTimeMax = 5f;

	[Header("Time")]
	public float spawnTime = 5f;

	[Header("Adjust Position")]
	public Vector3 adjustPosition;

	public Vector3 calcPosition;

	[Header("Adjust Force")]
	public Vector3 adjustForce;

	public Vector3 calcForce;

	private void Start()
	{
		onomatopoeiaTime = Random.Range(onomatopoeiaTimeMin, onomatopoeiaTimeMax);
	}

	private void LateUpdate()
	{
		if (!useOnomatopoeia)
		{
			return;
		}
		spawnTime -= Time.deltaTime;
		if (!(spawnTime > 0f))
		{
			return;
		}
		onomatopoeiaTime -= Time.deltaTime;
		if (onomatopoeiaTime < 0f)
		{
			onomatopoeiaTime = Random.Range(onomatopoeiaTimeMin, onomatopoeiaTimeMax);
			calcPosition = base.transform.position + base.transform.rotation * adjustPosition;
			Rigidbody component = Object.Instantiate(onomatopoeiaSperm, calcPosition, Quaternion.identity, shotStocker).GetComponent<Rigidbody>();
			if (adjustForce == Vector3.zero)
			{
				Vector3 onUnitSphere = Random.onUnitSphere;
				calcForce = onUnitSphere * Random.Range(0.5f, 1f);
			}
			else
			{
				Vector3 normalized = adjustForce.normalized;
				Vector3 onUnitSphere2 = Random.onUnitSphere;
				float t = 0.05f;
				Vector3 normalized2 = Vector3.Slerp(normalized, onUnitSphere2, t).normalized;
				Vector3 vector = base.transform.TransformDirection(normalized2);
				float num = Random.Range(0.8f, 1.2f);
				float num2 = adjustForce.magnitude * num;
				calcForce = vector * num2;
			}
			component.velocity = calcForce;
		}
	}
}
