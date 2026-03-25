using System.Collections.Generic;
using UnityEngine;

public class FacialDataGUI : MonoBehaviour
{
	public GameObject graphs;

	[Header("Faical")]
	public CharacterFaceManager _characterFaceManager;

	public List<RectTransform> _rectTransform;

	public List<RectTransform> _rectTransformFacial;

	public List<Vector2> facialData;

	[Header("Hot and Pleasure")]
	public CharacterFacialManager _characterFacialManager;

	public RectTransform _rectTransformHot;

	public RectTransform _rectTransformPleasure;

	public float currentHot;

	public float currentPleasure;

	[Header("Graph")]
	public float maxPosition = 40f;

	public float maxPositionVertical = 0.8f;

	private void Start()
	{
		graphs.SetActive(value: false);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.X))
		{
			graphs.SetActive(!graphs.activeSelf);
		}
	}

	private void LateUpdate()
	{
		facialData[0] = _characterFaceManager.facialData;
		facialData[1] = _characterFaceManager.facialDataEmote;
		for (int i = 0; i < _rectTransform.Count; i++)
		{
			Vector2 anchoredPosition = new Vector2(facialData[i].x * maxPosition, facialData[i].y * maxPosition);
			_rectTransform[i].anchoredPosition = anchoredPosition;
		}
		currentHot = _characterFacialManager.currentHot;
		currentPleasure = _characterFacialManager.currentPleasure;
		Vector2 anchoredPosition2 = new Vector2(0f, currentHot * maxPositionVertical);
		_rectTransformHot.anchoredPosition = anchoredPosition2;
		anchoredPosition2 = new Vector2(0f, currentPleasure * maxPositionVertical);
		_rectTransformPleasure.anchoredPosition = anchoredPosition2;
	}
}
