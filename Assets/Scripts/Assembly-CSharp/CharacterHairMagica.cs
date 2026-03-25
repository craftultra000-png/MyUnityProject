using System.Collections.Generic;
using MagicaCloth2;
using UnityEngine;

public class CharacterHairMagica : MonoBehaviour
{
	[Header("Status")]
	public bool isEat;

	[Header("Magica Cloth2")]
	public List<MagicaCloth> _magicaHair;

	public void SetEatWeight(bool value)
	{
		isEat = value;
		for (int i = 0; i < _magicaHair.Count; i++)
		{
			if (isEat)
			{
				_magicaHair[i].SerializeData.blendWeight = 0f;
			}
			else
			{
				_magicaHair[i].SerializeData.blendWeight = 1f;
			}
			_magicaHair[i].SetParameterChange();
		}
	}
}
