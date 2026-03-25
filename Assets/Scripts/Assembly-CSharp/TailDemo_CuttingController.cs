using UnityEngine;
using UnityEngine.UI;

public class TailDemo_CuttingController : MonoBehaviour
{
	public Text CountText;

	public Slider slider;

	public TailDemo_SegmentedTailGenerator generator;

	private void Start()
	{
		if ((bool)slider)
		{
			slider.onValueChanged.AddListener(delegate
			{
				ValueChangeCheck();
			});
		}
	}

	private void Update()
	{
		if ((bool)CountText && (bool)generator)
		{
			CountText.text = "Segments Count: " + generator.SegmentsCount;
		}
		if ((bool)generator && (bool)slider && slider.value != (float)generator.SegmentsCount)
		{
			slider.value = generator.SegmentsCount;
		}
	}

	public void ValueChangeCheck()
	{
		generator.SegmentsCount = (int)slider.value;
		generator.OnValidate();
	}
}
