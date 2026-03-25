using System.Collections.Generic;
using UnityEngine;

public class AmebaObject : MonoBehaviour
{
	public SkinnedMeshRenderer ameba;

	public Material copyMaterial;

	public float lifeTimeMax = 10f;

	public float lifeTimeDissolve = 10f;

	public float lifeTime;

	public float currentDissolve;

	public Rigidbody parentBone;

	public List<Rigidbody> childBoneA;

	public List<Rigidbody> childBoneB;

	public List<Rigidbody> childBoneC;

	public Vector2 dragParent = new Vector2(20f, 19f);

	public Vector2 dragchild0 = new Vector2(19f, 18f);

	public Vector2 dragchild1 = new Vector2(18f, 16f);

	public float calcDrag;

	private void Start()
	{
		base.transform.parent = null;
		copyMaterial = new Material(ameba.GetComponent<Renderer>().material);
		ameba.GetComponent<Renderer>().material = copyMaterial;
		lifeTimeDissolve = lifeTimeMax - 2f;
		SetRandomDrag();
	}

	private void LateUpdate()
	{
		lifeTime += Time.deltaTime;
		if (lifeTime > lifeTimeDissolve)
		{
			currentDissolve = Mathf.InverseLerp(lifeTimeDissolve, lifeTimeMax, lifeTime);
			copyMaterial.SetFloat("_Dissolve", currentDissolve);
		}
		if (lifeTime > lifeTimeMax)
		{
			Object.Destroy(base.gameObject);
		}
	}

	private void OnDestroy()
	{
		if (copyMaterial != null)
		{
			Object.Destroy(copyMaterial);
		}
	}

	public void SetRandomDrag()
	{
		calcDrag = Random.Range(dragParent.x, dragParent.y);
		parentBone.angularDrag = calcDrag;
		parentBone.drag = calcDrag;
		for (int i = 0; i < childBoneA.Count; i++)
		{
			if (i == 0)
			{
				calcDrag = Random.Range(dragchild0.x, dragchild0.y);
			}
			else
			{
				calcDrag = Random.Range(dragchild1.x, dragchild1.y);
			}
			childBoneA[i].angularDrag = calcDrag;
			childBoneA[i].drag = calcDrag;
			if (i == 0)
			{
				calcDrag = Random.Range(dragchild0.x, dragchild0.y);
			}
			else
			{
				calcDrag = Random.Range(dragchild1.x, dragchild1.y);
			}
			childBoneB[i].angularDrag = calcDrag;
			childBoneB[i].drag = calcDrag;
			if (i == 0)
			{
				calcDrag = Random.Range(dragchild0.x, dragchild0.y);
			}
			else
			{
				calcDrag = Random.Range(dragchild1.x, dragchild1.y);
			}
			childBoneC[i].angularDrag = calcDrag;
			childBoneC[i].drag = calcDrag;
		}
	}
}
