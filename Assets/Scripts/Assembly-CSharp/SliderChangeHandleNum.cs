using UnityEngine;
using UnityEngine.UI;

public class SliderChangeHandleNum : MonoBehaviour
{
	public Slider _slider;

	public string setNum;

	public Text handleText;

	private Text _text;

	private void Start()
	{
		_text = handleText.GetComponent<Text>();
		setNum = Mathf.RoundToInt(_slider.value).ToString();
		_text.text = setNum;
	}

	public void ChangeHandleNum()
	{
		if (_text == null)
		{
			_text = handleText.GetComponent<Text>();
		}
		setNum = Mathf.RoundToInt(_slider.value).ToString();
		_text.text = setNum;
	}
}
