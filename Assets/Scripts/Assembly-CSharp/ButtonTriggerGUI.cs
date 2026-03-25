using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonTriggerGUI : MonoBehaviour, IPointerClickHandler, IEventSystemHandler, IPointerEnterHandler, IPointerExitHandler
{
	[Header("Status")]
	public bool Initialize;

	public bool unuse;

	[Header("Target")]
	public List<GameObject> target;

	public List<GameObject> targetUnUse;

	private List<Image> _image;

	private List<SpriteRenderer> _spriteRenderer;

	private List<TextMeshPro> _textMeshPro;

	private List<TextMeshProUGUI> _textMeshProUgui;

	private List<Text> _text;

	private List<Image> _imageUnUse;

	private List<SpriteRenderer> _spriteRendererUnUse;

	private List<TextMeshPro> _textMeshProUnUse;

	private List<TextMeshProUGUI> _textMeshProUguiUnUse;

	private List<Text> _textUnUse;

	[Header("SE")]
	public AudioSource _audioSource;

	public AudioClip enterSe;

	public AudioClip clickSe;

	[Header("Color")]
	public Color defaultColor;

	public Color pointerEnterColor;

	public Color mouseClickColor;

	public Color unUseColor;

	[Header("Click Event")]
	[SerializeField]
	private UnityEvent m_PointerClick;

	[SerializeField]
	private UnityEvent m_PointerEnter;

	[SerializeField]
	private UnityEvent m_PointerExit;

	private void Start()
	{
		if (!Initialize)
		{
			CheckComponent();
		}
	}

	private void OnEnable()
	{
		if (!Initialize)
		{
			CheckComponent();
		}
		if (!unuse)
		{
			ColorReset();
		}
		else if (unuse)
		{
			Unuse();
		}
	}

	public void OnPointerClick(PointerEventData data)
	{
		if (!unuse)
		{
			MouseClick();
			if (clickSe != null)
			{
				_audioSource.PlayOneShot(clickSe);
			}
			m_PointerClick.Invoke();
		}
	}

	public void OnPointerEnter(PointerEventData data)
	{
		if (!unuse)
		{
			PointerEnter();
			if (enterSe != null)
			{
				_audioSource.PlayOneShot(enterSe);
			}
			m_PointerEnter.Invoke();
		}
	}

	public void OnPointerExit(PointerEventData data)
	{
		if (!unuse)
		{
			ColorReset();
			m_PointerExit.Invoke();
		}
	}

	public void CheckComponent()
	{
		Initialize = true;
		_image = new List<Image>();
		_spriteRenderer = new List<SpriteRenderer>();
		_textMeshPro = new List<TextMeshPro>();
		_textMeshProUgui = new List<TextMeshProUGUI>();
		_text = new List<Text>();
		_imageUnUse = new List<Image>();
		_spriteRendererUnUse = new List<SpriteRenderer>();
		_textMeshProUnUse = new List<TextMeshPro>();
		_textMeshProUguiUnUse = new List<TextMeshProUGUI>();
		_textUnUse = new List<Text>();
		for (int i = 0; i < target.Count; i++)
		{
			if (target[i].GetComponent<Image>() != null)
			{
				_image.Add(target[i].GetComponent<Image>());
			}
			if (target[i].GetComponent<SpriteRenderer>() != null)
			{
				_spriteRenderer.Add(target[i].GetComponent<SpriteRenderer>());
			}
			if (target[i].GetComponent<TextMeshPro>() != null)
			{
				_textMeshPro.Add(target[i].GetComponent<TextMeshPro>());
			}
			if (target[i].GetComponent<TextMeshProUGUI>() != null)
			{
				_textMeshProUgui.Add(target[i].GetComponent<TextMeshProUGUI>());
			}
			if (target[i].GetComponent<Text>() != null)
			{
				_text.Add(target[i].GetComponent<Text>());
			}
		}
		for (int j = 0; j < targetUnUse.Count; j++)
		{
			if (targetUnUse[j].GetComponent<Image>() != null)
			{
				_imageUnUse.Add(targetUnUse[j].GetComponent<Image>());
			}
			if (targetUnUse[j].GetComponent<SpriteRenderer>() != null)
			{
				_spriteRendererUnUse.Add(targetUnUse[j].GetComponent<SpriteRenderer>());
			}
			if (targetUnUse[j].GetComponent<TextMeshPro>() != null)
			{
				_textMeshProUnUse.Add(targetUnUse[j].GetComponent<TextMeshPro>());
			}
			if (targetUnUse[j].GetComponent<TextMeshProUGUI>() != null)
			{
				_textMeshProUguiUnUse.Add(targetUnUse[j].GetComponent<TextMeshProUGUI>());
			}
			if (targetUnUse[j].GetComponent<Text>() != null)
			{
				_textUnUse.Add(targetUnUse[j].GetComponent<Text>());
			}
		}
	}

	public void ColorReset()
	{
		if (!Initialize)
		{
			CheckComponent();
		}
		for (int i = 0; i < _image.Count; i++)
		{
			_image[i].color = defaultColor;
		}
		for (int j = 0; j < _spriteRenderer.Count; j++)
		{
			_spriteRenderer[j].color = defaultColor;
		}
		for (int k = 0; k < _textMeshPro.Count; k++)
		{
			_textMeshPro[k].color = defaultColor;
		}
		for (int l = 0; l < _textMeshProUgui.Count; l++)
		{
			_textMeshProUgui[l].color = defaultColor;
		}
		for (int m = 0; m < _text.Count; m++)
		{
			_text[m].color = defaultColor;
		}
		for (int n = 0; n < _imageUnUse.Count; n++)
		{
			_imageUnUse[n].color = defaultColor;
		}
		for (int num = 0; num < _spriteRendererUnUse.Count; num++)
		{
			_spriteRendererUnUse[num].color = defaultColor;
		}
		for (int num2 = 0; num2 < _textMeshProUnUse.Count; num2++)
		{
			_textMeshProUnUse[num2].color = defaultColor;
		}
		for (int num3 = 0; num3 < _textMeshProUguiUnUse.Count; num3++)
		{
			_textMeshProUguiUnUse[num3].color = defaultColor;
		}
		for (int num4 = 0; num4 < _textUnUse.Count; num4++)
		{
			_textUnUse[num4].color = defaultColor;
		}
	}

	public void PointerEnter()
	{
		if (!Initialize)
		{
			CheckComponent();
		}
		for (int i = 0; i < _image.Count; i++)
		{
			_image[i].color = pointerEnterColor;
		}
		for (int j = 0; j < _spriteRenderer.Count; j++)
		{
			_spriteRenderer[j].color = pointerEnterColor;
		}
		for (int k = 0; k < _textMeshPro.Count; k++)
		{
			_textMeshPro[k].color = pointerEnterColor;
		}
		for (int l = 0; l < _textMeshProUgui.Count; l++)
		{
			_textMeshProUgui[l].color = pointerEnterColor;
		}
		for (int m = 0; m < _text.Count; m++)
		{
			_text[m].color = pointerEnterColor;
		}
	}

	public void MouseClick()
	{
		if (!Initialize)
		{
			CheckComponent();
		}
		for (int i = 0; i < _image.Count; i++)
		{
			_image[i].color = mouseClickColor;
		}
		for (int j = 0; j < _spriteRenderer.Count; j++)
		{
			_spriteRenderer[j].color = mouseClickColor;
		}
		for (int k = 0; k < _textMeshPro.Count; k++)
		{
			_textMeshPro[k].color = mouseClickColor;
		}
		for (int l = 0; l < _textMeshProUgui.Count; l++)
		{
			_textMeshProUgui[l].color = mouseClickColor;
		}
		for (int m = 0; m < _text.Count; m++)
		{
			_text[m].color = mouseClickColor;
		}
	}

	public void Unuse()
	{
		if (!Initialize)
		{
			CheckComponent();
		}
		for (int i = 0; i < _imageUnUse.Count; i++)
		{
			_imageUnUse[i].color = unUseColor;
		}
		for (int j = 0; j < _spriteRendererUnUse.Count; j++)
		{
			_spriteRendererUnUse[j].color = unUseColor;
		}
		for (int k = 0; k < _textMeshProUnUse.Count; k++)
		{
			_textMeshProUnUse[k].color = unUseColor;
		}
		for (int l = 0; l < _textMeshProUguiUnUse.Count; l++)
		{
			_textMeshProUguiUnUse[l].color = unUseColor;
		}
		for (int m = 0; m < _textUnUse.Count; m++)
		{
			_textUnUse[m].color = unUseColor;
		}
	}
}
