using UnityEngine;

public class SelectionManager : MonoBehaviour
{
	public static SelectionManager instance;

	public RectTransform selectionVaginaGUI;

	public RectTransform selectionAnalGUI;

	public RectTransform selectionVaginaButton;

	public RectTransform selectionAnalButton;

	[Header("Camera Manger")]
	public SelectionGUIManager _uterusCameraManager;

	public SelectionGUIManager _analCameraManager;

	[Header("Status")]
	public bool isSelectionVagina;

	public bool isSelectionAnal;

	public bool isSelectionHideGUI;

	[Header("Lock")]
	public bool isLockVagina;

	public bool isLockAnal;

	[Header("Selection ClacVagina")]
	public Vector2 selectionVaginaHide;

	public Vector2 selectionVaginaShow;

	public Vector2 selectionVaginaCurrent;

	public Vector2 selectionVaginaCalc;

	[Header("Selection ClacAnal")]
	public Vector2 selectionAnalHide;

	public Vector2 selectionAnalShow;

	public Vector2 selectionAnalCurrent;

	public Vector2 selectionAnalCalc;

	[Header("Position Clac")]
	public float selectionVaginaLerp = 1f;

	public float selectionVaginaHideLerp = 1f;

	[Space]
	public float selectionAnalLerp = 1f;

	public float selectionAnalHideLerp = 1f;

	[Space]
	public AnimationCurve slideCurve;

	public float lerpSpeed = 5f;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
	}

	private void Start()
	{
		selectionVaginaCalc.y = selectionVaginaHide.y;
		selectionVaginaCalc.x = selectionVaginaShow.x;
		selectionVaginaCurrent = selectionVaginaCalc;
		selectionVaginaGUI.anchoredPosition = selectionVaginaCurrent;
		selectionVaginaButton.anchoredPosition = selectionVaginaCurrent;
		selectionAnalCalc.y = selectionAnalHide.y;
		selectionAnalCalc.x = selectionAnalShow.x;
		selectionAnalCurrent = selectionAnalCalc;
		selectionAnalCurrent.y += selectionVaginaCalc.y;
		selectionAnalGUI.anchoredPosition = selectionAnalCurrent;
		selectionAnalButton.anchoredPosition = selectionAnalCurrent;
		selectionVaginaLerp = 1f;
		selectionVaginaHideLerp = 1f;
		selectionAnalLerp = 1f;
		selectionAnalHideLerp = 1f;
	}

	private void LateUpdate()
	{
		if (!isLockVagina && selectionVaginaLerp < 1f)
		{
			selectionVaginaLerp += Time.deltaTime * lerpSpeed;
			if (selectionVaginaLerp > 1f)
			{
				selectionVaginaLerp = 1f;
			}
			if (isSelectionVagina)
			{
				selectionVaginaCalc.y = Mathf.Lerp(selectionVaginaHide.y, selectionVaginaShow.y, slideCurve.Evaluate(selectionVaginaLerp));
			}
			else if (!isSelectionVagina)
			{
				selectionVaginaCalc.y = Mathf.Lerp(selectionVaginaShow.y, selectionVaginaHide.y, slideCurve.Evaluate(selectionVaginaLerp));
			}
		}
		if (selectionVaginaHideLerp < 1f)
		{
			selectionVaginaHideLerp += Time.deltaTime * lerpSpeed;
			if (selectionVaginaHideLerp > 1f)
			{
				selectionVaginaHideLerp = 1f;
			}
			if (isSelectionHideGUI)
			{
				selectionVaginaCalc.x = Mathf.Lerp(selectionVaginaShow.x, selectionVaginaHide.x, slideCurve.Evaluate(selectionVaginaHideLerp));
			}
			else if (!isSelectionHideGUI)
			{
				selectionVaginaCalc.x = Mathf.Lerp(selectionVaginaHide.x, selectionVaginaShow.x, slideCurve.Evaluate(selectionVaginaHideLerp));
			}
		}
		if (selectionVaginaCurrent != selectionVaginaCalc)
		{
			selectionVaginaCurrent = selectionVaginaCalc;
			selectionVaginaGUI.anchoredPosition = selectionVaginaCurrent;
			selectionVaginaButton.anchoredPosition = selectionVaginaCurrent;
		}
		if (!isLockAnal && selectionAnalLerp < 1f)
		{
			selectionAnalLerp += Time.deltaTime * lerpSpeed;
			if (selectionAnalLerp > 1f)
			{
				selectionAnalLerp = 1f;
			}
			if (isSelectionAnal)
			{
				selectionAnalCalc.y = Mathf.Lerp(selectionAnalHide.y, selectionAnalShow.y, slideCurve.Evaluate(selectionAnalLerp));
			}
			else if (!isSelectionAnal)
			{
				selectionAnalCalc.y = Mathf.Lerp(selectionAnalShow.y, selectionAnalHide.y, slideCurve.Evaluate(selectionAnalLerp));
			}
		}
		if (selectionAnalHideLerp < 1f)
		{
			selectionAnalHideLerp += Time.deltaTime * lerpSpeed;
			if (selectionAnalHideLerp > 1f)
			{
				selectionAnalHideLerp = 1f;
			}
			if (isSelectionHideGUI)
			{
				selectionAnalCalc.x = Mathf.Lerp(selectionAnalShow.x, selectionAnalHide.x, slideCurve.Evaluate(selectionAnalHideLerp));
			}
			else if (!isSelectionHideGUI)
			{
				selectionAnalCalc.x = Mathf.Lerp(selectionAnalHide.x, selectionAnalShow.x, slideCurve.Evaluate(selectionAnalHideLerp));
			}
		}
		if (selectionAnalCurrent.y != selectionAnalCalc.y + selectionVaginaCalc.y)
		{
			selectionAnalCurrent = selectionAnalCalc;
			selectionAnalCurrent.y += selectionVaginaCalc.y;
			selectionAnalGUI.anchoredPosition = selectionAnalCurrent;
			selectionAnalButton.anchoredPosition = selectionAnalCurrent;
		}
		if (selectionAnalCurrent.x != selectionAnalCalc.x)
		{
			selectionAnalCurrent = selectionAnalCalc;
			selectionAnalCurrent.y += selectionVaginaCalc.y;
			selectionAnalGUI.anchoredPosition = selectionAnalCurrent;
			selectionAnalButton.anchoredPosition = selectionAnalCurrent;
		}
	}

	public void SelectionVaginaButton()
	{
		if (!isLockVagina)
		{
			SelectionVaginaSlide(!isSelectionVagina);
		}
	}

	public void SelectionVaginaSlide(bool value)
	{
		if (isLockVagina && value)
		{
			selectionVaginaLerp = 1f;
		}
		else if (isSelectionVagina != value)
		{
			selectionVaginaLerp = 0f;
		}
		isSelectionVagina = value;
	}

	public void SelectionAnalButton()
	{
		if (!isLockAnal)
		{
			SelectionAnalSlide(!isSelectionAnal);
		}
	}

	public void SelectionAnalSlide(bool value)
	{
		if (isLockAnal && value)
		{
			selectionAnalLerp = 1f;
		}
		else if (isSelectionAnal != value)
		{
			selectionAnalLerp = 0f;
		}
		isSelectionAnal = value;
	}

	public void SelectionHideGUI(bool value)
	{
		if (isSelectionHideGUI != value)
		{
			selectionVaginaHideLerp = 0f;
			selectionAnalHideLerp = 0f;
		}
		isSelectionHideGUI = value;
	}

	public void LockVagina()
	{
		isLockVagina = !isLockVagina;
		_uterusCameraManager.LockButton(isLockVagina);
	}

	public void LockAnal()
	{
		isLockAnal = !isLockAnal;
		_analCameraManager.LockButton(isLockAnal);
	}
}
