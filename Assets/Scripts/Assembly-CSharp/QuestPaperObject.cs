using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Utage;

public class QuestPaperObject : MonoBehaviour
{
	[Header("Language")]
	public string langBase;

	public LanguageManagerBase langManager;

	[Header("Quest Name")]
	public string headerDefault = "QuestHeader";

	public List<string> bodyDefault;

	public string nameHaderDefault = "QuestNameHeader";

	public string nameDefault = "Vein Lurk";

	public TextMeshPro headerDefaultText;

	public List<TextMeshPro> bodyDefaultText;

	public TextMeshPro nameHaderDefaultText;

	public TextMeshPro nameDefaultText;

	private void Start()
	{
		StartCoroutine(WaitStart());
	}

	private IEnumerator WaitStart()
	{
		yield return null;
		LangaugeReset();
	}

	public void LangaugeReset()
	{
		if (langManager == null)
		{
			langManager = LanguageManagerBase.Instance;
		}
		langBase = langManager.CurrentLanguage;
		headerDefaultText.text = langManager.LocalizeText(headerDefault);
		for (int i = 0; i < bodyDefault.Count; i++)
		{
			bodyDefaultText[i].text = langManager.LocalizeText(bodyDefault[i]);
		}
		nameHaderDefaultText.text = langManager.LocalizeText(nameHaderDefault);
		nameDefaultText.text = langManager.LocalizeText(nameDefault);
	}
}
