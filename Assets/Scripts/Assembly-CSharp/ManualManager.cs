using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManualManager : MonoBehaviour
{
	public List<GameObject> pageObject;

	public List<ButtonTriggerGUI> pageButton;

	public List<Text> pageText;

	public int currentPage;

	[Header("Color")]
	public Color enableColor;

	public Color disableColor;

	private void Start()
	{
		Page(0);
	}

	public void OnDisable()
	{
		Page(0);
	}

	public void Page(int value)
	{
		currentPage = value;
		for (int i = 0; i < pageObject.Count; i++)
		{
			pageObject[i].SetActive(value: false);
			pageText[i].color = disableColor;
			pageButton[i].defaultColor = disableColor;
			pageButton[i].ColorReset();
		}
		pageObject[currentPage].SetActive(value: true);
		pageText[currentPage].color = enableColor;
		pageButton[currentPage].defaultColor = enableColor;
		pageButton[currentPage].ColorReset();
	}
}
