using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utage;

public class LangManager : MonoBehaviour
{
	public static LangManager instance;

	public string lang;

	[Header("Script")]
	public QuestPaperObject _questPaperObject;

	public GuildCardObject _guildCardObject;

	[Header("Lang")]
	public List<Text> roundTextList;

	public List<ButtonTriggerGUI> roundTextButtonList;

	public List<Text> enTextList;

	public List<Text> jaTextList;

	public Color enableColor;

	public Color disableColor;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		if (ES3.KeyExists("Language"))
		{
			lang = ES3.Load<string>("Language");
			if (lang == "en")
			{
				SetEn();
			}
			else if (lang == "ja")
			{
				SetJa();
			}
		}
		else if (Application.systemLanguage == SystemLanguage.Japanese)
		{
			SetJa();
		}
		else
		{
			SetEn();
		}
	}

	private void Start()
	{
	}

	public void SetEn()
	{
		lang = "en";
		ES3.Save("Language", lang);
		roundTextButtonList[0].defaultColor = enableColor;
		roundTextButtonList[1].defaultColor = disableColor;
		roundTextButtonList[0].ColorReset();
		roundTextButtonList[1].ColorReset();
		for (int i = 0; i > enTextList.Count; i++)
		{
			enTextList[0].color = enableColor;
		}
		for (int j = 0; j > jaTextList.Count; j++)
		{
			jaTextList[0].color = disableColor;
		}
		LanguageManagerBase languageManagerBase = LanguageManagerBase.Instance;
		if (languageManagerBase == null)
		{
			StartCoroutine(WaitForLanguageManager());
		}
		languageManagerBase.CurrentLanguage = "English";
		Debug.LogWarning("Set Language: en " + languageManagerBase.CurrentLanguage, base.gameObject);
		if (_guildCardObject != null)
		{
			_guildCardObject.LangaugeReset();
		}
		if (_questPaperObject != null)
		{
			_questPaperObject.LangaugeReset();
		}
	}

	public void SetJa()
	{
		lang = "ja";
		ES3.Save("Language", lang);
		roundTextButtonList[0].defaultColor = disableColor;
		roundTextButtonList[1].defaultColor = enableColor;
		roundTextButtonList[0].ColorReset();
		roundTextButtonList[1].ColorReset();
		for (int i = 0; i > enTextList.Count; i++)
		{
			enTextList[0].color = disableColor;
		}
		for (int j = 0; j > jaTextList.Count; j++)
		{
			jaTextList[0].color = enableColor;
		}
		LanguageManagerBase languageManagerBase = LanguageManagerBase.Instance;
		if (languageManagerBase == null)
		{
			StartCoroutine(WaitForLanguageManager());
		}
		languageManagerBase.CurrentLanguage = "Japanese";
		Debug.LogWarning("Set Language: ja " + languageManagerBase.CurrentLanguage, base.gameObject);
		if (_guildCardObject != null)
		{
			_guildCardObject.LangaugeReset();
		}
		if (_questPaperObject != null)
		{
			_questPaperObject.LangaugeReset();
		}
	}

	private IEnumerator WaitForLanguageManager()
	{
		LanguageManagerBase langManager = null;
		while (langManager == null)
		{
			langManager = LanguageManagerBase.Instance;
			yield return null;
		}
	}
}
