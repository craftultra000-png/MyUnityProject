using System.Collections.Generic;
using UnityEngine;

public class FeelerTest : MonoBehaviour
{
	public List<Animator> _animator;

	public List<FeelerCoil> _feelerCoil;

	public List<FeelerCoilAimObject> _feelerCoilAimObject;

	public List<FeelerShotObject> _feelerShotObject;

	public List<FeelerAmebaObject> FeelerAmebaObject;

	public bool isClick;

	public bool isCoil;

	private void Start()
	{
		for (int i = 0; i < _feelerCoil.Count; i++)
		{
		}
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			for (int i = 0; i < _feelerShotObject.Count; i++)
			{
				_feelerShotObject[i].Shot();
			}
			for (int j = 0; j < FeelerAmebaObject.Count; j++)
			{
				FeelerAmebaObject[j].Shot();
			}
		}
		if (!Input.GetKeyDown(KeyCode.Mouse1))
		{
			return;
		}
		if (!isClick)
		{
			isClick = true;
			isCoil = true;
			for (int k = 0; k < _animator.Count; k++)
			{
				_animator[k].SetBool("isCoil", value: true);
			}
			for (int l = 0; l < _feelerCoil.Count; l++)
			{
				_feelerCoil[l].isAim = true;
			}
			for (int m = 0; m < _feelerCoilAimObject.Count; m++)
			{
				_feelerCoilAimObject[m].SetAim(value: true);
			}
		}
		else
		{
			isClick = false;
			isCoil = false;
			for (int n = 0; n < _animator.Count; n++)
			{
				_animator[n].SetBool("isCoil", value: false);
			}
			for (int num = 0; num < _feelerCoil.Count; num++)
			{
				_feelerCoil[num].isAim = false;
			}
			for (int num2 = 0; num2 < _feelerCoilAimObject.Count; num2++)
			{
				_feelerCoilAimObject[num2].SetAim(value: false);
			}
		}
	}
}
