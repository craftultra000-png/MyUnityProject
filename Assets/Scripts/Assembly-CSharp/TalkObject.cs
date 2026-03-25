using UnityEngine;
using UnityEngine.UI;

public class TalkObject : MonoBehaviour
{
	[Header("Animator")]
	public Animator _animator;

	[Header("Status")]
	public bool isIdle;

	public bool isEnd;

	[Header("Text")]
	public string character;

	public string talk;

	[Space]
	public Text characterText;

	public Text talkText;

	[Header("Detroy")]
	public float destroyWait = 2f;

	private void FixedUpdate()
	{
		if (!isEnd)
		{
			destroyWait -= Time.deltaTime;
			if (destroyWait < 0f)
			{
				TextEnd();
			}
		}
	}

	private void OnEnable()
	{
		_animator.SetBool("isIdle", isIdle);
		_animator.SetBool("isEnd", isEnd);
	}

	public void TextEnd()
	{
		isIdle = false;
		isEnd = true;
		_animator.SetBool("isEnd", value: true);
	}

	public void SetText(string characterValue, string talkValue)
	{
		isIdle = true;
		isEnd = false;
		Debug.Log("Talk: " + characterValue + " : " + talkValue);
		character = characterValue;
		talk = talkValue;
		characterText.text = character;
		talkText.text = talk;
	}
}
