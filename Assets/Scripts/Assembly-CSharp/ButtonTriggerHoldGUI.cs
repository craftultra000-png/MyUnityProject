using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonTriggerHoldGUI : MonoBehaviour, IPointerClickHandler, IEventSystemHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
	public bool unuse;

	public bool isHold;

	[Header("Target")]
	public List<GameObject> target;

	public List<GameObject> targetUnUse;

	private Image _image;

	private SpriteRenderer _spriteRenderer;

	private TextMeshPro _textMeshPro;

	private TextMeshProUGUI _textMeshProUgui;

	private Text _text;

	private Image _imageUnUse;

	private SpriteRenderer _spriteRendererUnUse;

	private TextMeshPro _textMeshProUnUse;

	private TextMeshProUGUI _textMeshProUguiUnUse;

	private Text _textUnUse;

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
	private UnityEvent m_PointerDown;

	[SerializeField]
	private UnityEvent m_PointerEnter;

	[SerializeField]
	private UnityEvent m_PointerExit;

	private void Start()
	{
		CheckComponent();
	}

	private void OnEnable()
	{
		CheckComponent();
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

	public void OnPointerDown(PointerEventData data)
	{
		if (!unuse)
		{
			m_PointerDown.Invoke();
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
		for (int i = 0; i < target.Count; i++)
		{
			if (target[i].GetComponent<Image>() != null && _image == null)
			{
				_image = target[i].GetComponent<Image>();
			}
			if (target[i].GetComponent<SpriteRenderer>() != null && _spriteRenderer == null)
			{
				_spriteRenderer = target[i].GetComponent<SpriteRenderer>();
			}
			if (target[i].GetComponent<TextMeshPro>() != null)
			{
				_textMeshPro = target[i].GetComponent<TextMeshPro>();
			}
			if (target[i].GetComponent<TextMeshProUGUI>() != null)
			{
				_textMeshProUgui = target[i].GetComponent<TextMeshProUGUI>();
			}
			if (target[i].GetComponent<Text>() != null)
			{
				_text = target[i].GetComponent<Text>();
			}
		}
		for (int j = 0; j < targetUnUse.Count; j++)
		{
			if (targetUnUse[j].GetComponent<Image>() != null && _imageUnUse == null)
			{
				_imageUnUse = targetUnUse[j].GetComponent<Image>();
			}
			if (targetUnUse[j].GetComponent<SpriteRenderer>() != null && _spriteRendererUnUse == null)
			{
				_spriteRendererUnUse = targetUnUse[j].GetComponent<SpriteRenderer>();
			}
			if (targetUnUse[j].GetComponent<TextMeshPro>() != null)
			{
				_textMeshProUnUse = targetUnUse[j].GetComponent<TextMeshPro>();
			}
			if (targetUnUse[j].GetComponent<TextMeshProUGUI>() != null)
			{
				_textMeshProUguiUnUse = targetUnUse[j].GetComponent<TextMeshProUGUI>();
			}
			if (targetUnUse[j].GetComponent<Text>() != null)
			{
				_textUnUse = targetUnUse[j].GetComponent<Text>();
			}
		}
	}

	public void ColorReset()
	{
		if ((bool)_image)
		{
			_image.color = defaultColor;
		}
		if ((bool)_spriteRenderer)
		{
			_spriteRenderer.color = defaultColor;
		}
		if ((bool)_textMeshPro)
		{
			_textMeshPro.color = defaultColor;
		}
		if ((bool)_textMeshProUgui)
		{
			_textMeshProUgui.color = defaultColor;
		}
		if ((bool)_text)
		{
			_text.color = defaultColor;
		}
		if ((bool)_imageUnUse)
		{
			_imageUnUse.color = defaultColor;
		}
		if ((bool)_spriteRendererUnUse)
		{
			_spriteRendererUnUse.color = defaultColor;
		}
		if ((bool)_textMeshProUnUse)
		{
			_textMeshProUnUse.color = defaultColor;
		}
		if ((bool)_textMeshProUguiUnUse)
		{
			_textMeshProUguiUnUse.color = defaultColor;
		}
		if ((bool)_textUnUse)
		{
			_textUnUse.color = defaultColor;
		}
	}

	public void PointerEnter()
	{
		if ((bool)_image)
		{
			_image.color = pointerEnterColor;
		}
		if ((bool)_spriteRenderer)
		{
			_spriteRenderer.color = pointerEnterColor;
		}
		if ((bool)_textMeshPro)
		{
			_textMeshPro.color = pointerEnterColor;
		}
		if ((bool)_textMeshProUgui)
		{
			_textMeshProUgui.color = pointerEnterColor;
		}
		if ((bool)_text)
		{
			_text.color = pointerEnterColor;
		}
	}

	public void MouseClick()
	{
		if ((bool)_image)
		{
			_image.color = mouseClickColor;
		}
		if ((bool)_spriteRenderer)
		{
			_spriteRenderer.color = mouseClickColor;
		}
		if ((bool)_textMeshPro)
		{
			_textMeshPro.color = mouseClickColor;
		}
		if ((bool)_textMeshProUgui)
		{
			_textMeshProUgui.color = mouseClickColor;
		}
		if ((bool)_text)
		{
			_text.color = mouseClickColor;
		}
	}

	public void Unuse()
	{
		if ((bool)_imageUnUse)
		{
			_imageUnUse.color = unUseColor;
		}
		if ((bool)_spriteRendererUnUse)
		{
			_spriteRendererUnUse.color = unUseColor;
		}
		if ((bool)_textMeshProUnUse)
		{
			_textMeshProUnUse.color = unUseColor;
		}
		if ((bool)_textMeshProUguiUnUse)
		{
			_textMeshProUguiUnUse.color = unUseColor;
		}
		if ((bool)_textUnUse)
		{
			_textUnUse.color = unUseColor;
		}
	}
}
