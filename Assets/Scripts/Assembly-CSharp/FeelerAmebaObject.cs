using UnityEngine;

public class FeelerAmebaObject : MonoBehaviour
{
	[Header("Status")]
	public bool isShot;

	public float coolTime;

	public float coolTimeMax = 0.25f;

	public float shootForceMin = 20f;

	public float shootForceMax = 40f;

	[Header("Status")]
	public GameObject shotEffect;

	public Transform shotPoint;

	public Vector3 adujustRotation = new Vector3(90f, -90f, 0f);

	public Vector3 calcRotation;

	[Header("Se")]
	public AudioClip shotSe;

	private void Start()
	{
	}

	private void LateUpdate()
	{
		if (isShot)
		{
			coolTime -= Time.deltaTime;
			if (coolTime < 0f)
			{
				isShot = false;
			}
		}
	}

	public void Shot()
	{
		if (!isShot)
		{
			isShot = true;
			coolTime = coolTimeMax;
			EffectSeManager.instance.PlaySe(shotSe);
			calcRotation = shotPoint.rotation.eulerAngles + adujustRotation;
			GameObject obj = Object.Instantiate(shotEffect, shotPoint.position, shotPoint.rotation, shotPoint.parent);
			Vector3 eulerAngles = obj.transform.rotation.eulerAngles;
			eulerAngles.y = Random.Range(0f, 360f);
			obj.transform.rotation = Quaternion.Euler(eulerAngles);
			obj.SetActive(value: true);
			obj.GetComponent<Rigidbody>().velocity = shotPoint.forward * Random.Range(shootForceMin, shootForceMax);
		}
	}
}
