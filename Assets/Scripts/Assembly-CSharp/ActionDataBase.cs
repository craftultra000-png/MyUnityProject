using UnityEngine;

public class ActionDataBase : MonoBehaviour
{
	public static ActionDataBase instance;

	public ActionManager _actionManager;

	[Header("Event")]
	public bool isEvent;

	public string eventType;

	[Space]
	public bool isLibido;

	public int libido;

	public void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
	}

	public void SetTimeOver()
	{
		Debug.LogWarning("Set TimeOver Check", base.gameObject);
		Debug.LogWarning("<i><color=#009900>ActionDataBase isEvent: " + isEvent + " eventType: " + eventType + " </color></i>", base.gameObject);
		Debug.LogWarning("<i><color=#009900>ActionDataBase SetTimeOver isLibido: " + isLibido + " libido: " + libido + " </color></i>", base.gameObject);
		if (isEvent)
		{
			Debug.LogWarning("<i><color=#009900>ActionDataBase isEvent: " + isEvent + "</color></i> Talk: " + eventType, base.gameObject);
			TalkGUI.instance.SpownTalk(eventType, force: true);
		}
		else if (libido == 0 && isLibido)
		{
			TalkGUI.instance.SpownTalk("*ChoiceDeckEnemy", force: true);
		}
		else
		{
			TalkGUI.instance.SpownTalk("*ChoiceDeckPlayer", force: true);
		}
	}

	public void EventReset()
	{
		Debug.LogWarning("<i><color=#009900>ActionDataBase EventReset</color></i>", base.gameObject);
		Debug.LogError("<i><color=#009900>ActionDataBase EventReset</color></i>", base.gameObject);
		isEvent = false;
	}
}
