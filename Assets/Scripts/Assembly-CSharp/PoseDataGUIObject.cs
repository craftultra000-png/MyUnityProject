using UnityEngine;
using UnityEngine.EventSystems;

public class PoseDataGUIObject : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerClickHandler, IDragHandler
{
	public PoseDataGUI _poseDataGUI;

	[Header("Status")]
	public bool isLock;

	[Header("Data")]
	public Vector2 currentParam;

	public Vector2 clickParam;

	[Header("Object")]
	public RectTransform frame;

	public Vector2 padding = new Vector2(10f, 10f);

	[Header("Calc")]
	public Vector2 safeMin;

	public Vector2 safeMax;

	public Vector2 limitHalf;

	private void Start()
	{
		limitHalf = frame.rect.size * 0.5f;
		safeMin = -limitHalf + padding;
		safeMax = limitHalf - padding;
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (RectTransformUtility.ScreenPointToLocalPointInRectangle(frame, eventData.position, eventData.pressEventCamera, out var localPoint))
		{
			ClickPosition(localPoint, click: true);
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (RectTransformUtility.ScreenPointToLocalPointInRectangle(frame, eventData.position, eventData.pressEventCamera, out var localPoint))
		{
			ClickPosition(localPoint, click: true);
		}
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (RectTransformUtility.ScreenPointToLocalPointInRectangle(frame, eventData.position, eventData.pressEventCamera, out var localPoint))
		{
			ClickPosition(localPoint, click: false);
		}
	}

	private void ClickPosition(Vector2 localPos, bool click)
	{
		clickParam = localPos;
		Vector2 vector = limitHalf - padding;
		float num = Mathf.Min(vector.x, vector.y);
		if (!(clickParam.magnitude > num && click))
		{
			if (clickParam.magnitude > num && !click)
			{
				clickParam = clickParam.normalized * num;
			}
			Vector2 vector2 = clickParam / num;
			currentParam = vector2;
			_poseDataGUI.SetTarget(currentParam);
		}
	}
}
