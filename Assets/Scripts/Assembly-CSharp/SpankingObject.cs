using System.Collections.Generic;
using UnityEngine;

public class SpankingObject : MonoBehaviour
{
	[Header("Attack Type")]
	public string attackType = "Spanking";

	public string onomatopoeiaType = "Spanking";

	[Header("Self")]
	public Rigidbody _rigidbody;

	public Transform shotStocker;

	[Header("Status")]
	public bool isHit;

	[Header("Effect")]
	public GameObject hitEffect;

	[Header("Se")]
	public List<AudioClip> spankSe;

	private void OnTriggerEnter(Collider other)
	{
		if (isHit)
		{
			return;
		}
		if (other.CompareTag("Character"))
		{
			isHit = true;
			EffectSeManager.instance.PlaySe(spankSe[Random.Range(0, spankSe.Count)]);
			Object.Instantiate(hitEffect, base.transform.position, base.transform.rotation, shotStocker);
			base.gameObject.SetActive(value: false);
			if ((bool)other.gameObject.GetComponent<CharacterColliderObject>())
			{
				other.gameObject.GetComponent<CharacterColliderObject>().HitData(attackType);
			}
			if ((bool)other.gameObject.GetComponent<CharacterColliderBelly>())
			{
				Vector3 data = other.ClosestPointOnBounds(base.transform.position);
				other.gameObject.GetComponent<CharacterColliderBelly>().HitPosition(data);
			}
			if (OnomatopoeiaManager.instance.useOtomanopoeia)
			{
				OnomatopoeiaManager.instance.SpawnOnomatopoeia(base.transform.position, null, onomatopoeiaType, Camera.main.transform);
			}
		}
		if (other.CompareTag("StageObject"))
		{
			isHit = true;
			EffectSeManager.instance.PlaySe(spankSe[Random.Range(0, spankSe.Count)]);
			Object.Instantiate(hitEffect, base.transform.position, base.transform.rotation, shotStocker);
			base.gameObject.SetActive(value: false);
			if (OnomatopoeiaManager.instance.useOtomanopoeia)
			{
				OnomatopoeiaManager.instance.SpawnOnomatopoeia(base.transform.position, null, onomatopoeiaType, Camera.main.transform);
			}
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (isHit)
		{
			return;
		}
		if (other.CompareTag("Character"))
		{
			isHit = true;
			EffectSeManager.instance.PlaySe(spankSe[Random.Range(0, spankSe.Count)]);
			Object.Instantiate(hitEffect, base.transform.position, base.transform.rotation, shotStocker);
			base.gameObject.SetActive(value: false);
			if ((bool)other.gameObject.GetComponent<CharacterColliderObject>())
			{
				other.gameObject.GetComponent<CharacterColliderObject>().HitData(attackType);
			}
			if ((bool)other.gameObject.GetComponent<CharacterColliderBelly>())
			{
				Vector3 data = other.ClosestPointOnBounds(base.transform.position);
				other.gameObject.GetComponent<CharacterColliderBelly>().HitPosition(data);
			}
		}
		if (other.CompareTag("StageObject"))
		{
			isHit = true;
			EffectSeManager.instance.PlaySe(spankSe[Random.Range(0, spankSe.Count)]);
			Object.Instantiate(hitEffect, base.transform.position, base.transform.rotation, shotStocker);
			base.gameObject.SetActive(value: false);
		}
	}
}
