using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SideCostumeManager : MonoBehaviour
{
	[Header("Costume Button")]
	public int costumeType = -1;

	public List<Image> costumeEquip;

	public List<ButtonTriggerGUI> costumeButtonGUI;

	[Header("Color")]
	public Color enableColor;

	public Color disableColor;

	private void Start()
	{
	}

	public void SetCostume(int value)
	{
		costumeType = value;
		for (int i = 0; i < costumeEquip.Count; i++)
		{
			if (i == costumeType)
			{
				costumeEquip[i].color = enableColor;
				costumeButtonGUI[i].defaultColor = enableColor;
			}
			else
			{
				costumeEquip[i].color = disableColor;
				costumeButtonGUI[i].defaultColor = disableColor;
			}
		}
	}
}
