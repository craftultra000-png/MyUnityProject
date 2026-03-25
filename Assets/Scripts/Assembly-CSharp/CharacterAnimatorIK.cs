using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimatorIK : MonoBehaviour
{
	public CharacterLookAtEyes _characterLookAtEyes;

	public CharacterLookAtHead _characterLookAtHead;

	public List<LookCoilTarget> _lookCoilTargetsFeeler;

	public LookCoilTarget _lookCoilTargetsFeelerHeadEyes;

	public List<FeelerRubObject> _titsRubObject;

	public List<FeelerTitsSuckObject> _titsSuckObject;

	public FeelerRubObject _vaginaRubObject;

	public List<FeelerRubObject> _multiRubObject;

	public List<FeelerBiteObject> _multiBiteObject;

	public List<FeelerBiteEdge> _multiBiteEdge;

	private void OnAnimatorIK(int layerIndex)
	{
		_characterLookAtHead.LinkOnAnimatorIK(layerIndex);
		for (int i = 0; i < _lookCoilTargetsFeeler.Count; i++)
		{
			_lookCoilTargetsFeeler[i].LinkOnAnimatorIK(layerIndex);
		}
		_lookCoilTargetsFeelerHeadEyes.LinkOnAnimatorIK(layerIndex);
	}

	private IEnumerator Start()
	{
		while (true)
		{
			for (int i = 0; i < _titsSuckObject.Count; i++)
			{
				_titsRubObject[i].MagicaAnimator();
				_titsSuckObject[i].MagicaAnimator();
			}
			_vaginaRubObject.MagicaAnimator();
			for (int j = 0; j < _multiRubObject.Count; j++)
			{
				_multiRubObject[j].MagicaAnimator();
			}
			for (int k = 0; k < _multiBiteObject.Count; k++)
			{
				_multiBiteObject[k].MagicaAnimator();
			}
			for (int l = 0; l < _multiBiteEdge.Count; l++)
			{
				_multiBiteEdge[l].MagicaAnimator();
			}
			yield return new WaitForEndOfFrame();
		}
	}
}
