using System.Collections.Generic;
using MagicaCloth2;
using UnityEngine;

public class CharacterCostumeMagica : MonoBehaviour
{
	[Header("Status")]
	public bool isEat;

	[Header("Magica Cloth2")]
	public List<MagicaCloth> _magicaHead;

	public void SetEatWeight(bool value)
	{
		isEat = value;
		for (int i = 0; i < _magicaHead.Count; i++)
		{
			if (isEat)
			{
				_magicaHead[i].SerializeData.blendWeight = 0f;
			}
			else
			{
				_magicaHead[i].SerializeData.blendWeight = 1f;
			}
			_magicaHead[i].SetParameterChange();
		}
	}
}
