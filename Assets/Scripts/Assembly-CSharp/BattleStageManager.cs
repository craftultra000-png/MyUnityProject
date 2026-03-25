using System.Collections.Generic;
using UnityEngine;

public class BattleStageManager : MonoBehaviour
{
	public static BattleStageManager instance;

	public BGMManager _BGMManager;

	public string stageName;

	[Header("Dream Catcher")]
	public ActionManager _actionManager;

	[Header("Stage")]
	public GameObject startStage;

	public GameObject roadStage;

	public GameObject bedStage;

	public GameObject bathStage;

	public GameObject churchStage;

	public GameObject endStage;

	[Header("SE")]
	public AudioSource _audioSource;

	public List<AudioClip> bedSe;

	public List<AudioClip> bathSe;

	public void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
	}

	public void SetStage(string value)
	{
		Debug.LogWarning("Set Stage :" + value);
		stageName = value;
		if (stageName == "Start")
		{
			startStage.SetActive(value: true);
			roadStage.SetActive(value: false);
			bedStage.SetActive(value: false);
			bathStage.SetActive(value: false);
			churchStage.SetActive(value: false);
			endStage.SetActive(value: false);
		}
		else if (stageName == "Road")
		{
			startStage.SetActive(value: false);
			roadStage.SetActive(value: true);
			bedStage.SetActive(value: false);
			bathStage.SetActive(value: false);
			churchStage.SetActive(value: false);
			endStage.SetActive(value: false);
		}
		else if (stageName == "Bed")
		{
			startStage.SetActive(value: false);
			roadStage.SetActive(value: false);
			bedStage.SetActive(value: true);
			bathStage.SetActive(value: false);
			churchStage.SetActive(value: false);
			endStage.SetActive(value: false);
		}
		else if (stageName == "Bath")
		{
			startStage.SetActive(value: false);
			roadStage.SetActive(value: false);
			bedStage.SetActive(value: false);
			bathStage.SetActive(value: true);
			churchStage.SetActive(value: false);
			endStage.SetActive(value: false);
		}
		else if (stageName == "Church")
		{
			startStage.SetActive(value: false);
			roadStage.SetActive(value: false);
			bedStage.SetActive(value: false);
			bathStage.SetActive(value: false);
			churchStage.SetActive(value: true);
			endStage.SetActive(value: false);
		}
		else if (stageName == "End")
		{
			startStage.SetActive(value: false);
			roadStage.SetActive(value: false);
			bedStage.SetActive(value: false);
			bathStage.SetActive(value: false);
			churchStage.SetActive(value: false);
			endStage.SetActive(value: true);
			_BGMManager.StopBGM();
		}
	}

	public void EnvironmentSe()
	{
		if (stageName == "Bed" || stageName == "Road" || stageName == "Church")
		{
			_audioSource.PlayOneShot(bedSe[Random.Range(0, bedSe.Count)]);
		}
		else if (stageName == "Bath")
		{
			_audioSource.PlayOneShot(bedSe[Random.Range(0, bedSe.Count)]);
		}
	}
}
