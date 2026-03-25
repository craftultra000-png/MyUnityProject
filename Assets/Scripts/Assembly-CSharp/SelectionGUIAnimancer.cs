using System.Collections.Generic;
using Animancer;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectionGUIAnimancer : MonoBehaviour
{
	public AnimancerComponent _animancer;

	[Header("Status")]
	public int currentCamera;

	public bool isLock;

	[Header("Anim FeedTime")]
	public float feedTime = 0.5f;

	[Header("Animation Clip")]
	public List<AnimationClip> listCiip;

	private AnimancerState _state;

	[Header("Zoom Image")]
	public TextMeshProUGUI zoomInText;

	public TextMeshProUGUI zoomOutText;

	public ButtonTriggerGUI zoomInButton;

	public ButtonTriggerGUI zoomOutButton;

	[Header("Lock Image")]
	public Image lockImage;

	public ButtonTriggerGUI lockButton;

	public Sprite lockIcon;

	public Sprite unlockIcon;

	[Header("Color")]
	public Color enableColor;

	public Color disableColor;

	private void Start()
	{
		_state = _animancer.Play(listCiip[currentCamera]);
		ChangeButtonColor();
		LockButton(value: false);
	}

	public void CameraZoomIn()
	{
		if (currentCamera < listCiip.Count - 1)
		{
			currentCamera++;
			_state = _animancer.Play(listCiip[currentCamera], feedTime);
		}
		ChangeButtonColor();
	}

	public void CameraZoomOut()
	{
		if (currentCamera > 0)
		{
			currentCamera--;
			_state = _animancer.Play(listCiip[currentCamera], feedTime);
		}
		ChangeButtonColor();
	}

	public void CameraSet(int value)
	{
		if (!isLock)
		{
			currentCamera = value;
			_state = _animancer.Play(listCiip[currentCamera], feedTime);
			ChangeButtonColor();
		}
	}

	public void ChangeButtonColor()
	{
		if (currentCamera == listCiip.Count - 1)
		{
			zoomInText.color = disableColor;
			zoomInButton.defaultColor = disableColor;
		}
		else
		{
			zoomInText.color = enableColor;
			zoomInButton.defaultColor = enableColor;
		}
		if (currentCamera == 0)
		{
			zoomOutText.color = disableColor;
			zoomOutButton.defaultColor = disableColor;
		}
		else
		{
			zoomOutText.color = enableColor;
			zoomOutButton.defaultColor = enableColor;
		}
	}

	public void LockButton(bool value)
	{
		isLock = value;
		if (isLock)
		{
			lockImage.sprite = lockIcon;
			lockImage.color = enableColor;
			lockButton.defaultColor = enableColor;
		}
		else
		{
			lockImage.sprite = unlockIcon;
			lockImage.color = disableColor;
			lockButton.defaultColor = disableColor;
		}
	}
}
