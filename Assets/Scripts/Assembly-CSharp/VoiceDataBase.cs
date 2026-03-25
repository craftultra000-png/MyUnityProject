using System.Collections.Generic;
using UnityEngine;

public class VoiceDataBase : MonoBehaviour
{
	[Header("Action")]
	public List<AudioClip> voiceEat;

	public List<AudioClip> voiceDrink;

	public List<AudioClip> voicePiston;

	public List<AudioClip> voiceVomit;

	public List<AudioClip> voiceKiss;

	[Header("Data")]
	public string charactorName;

	public List<AudioClip> voiceHit;

	public List<AudioClip> voiceOrgasm;

	public List<AudioClip> breathSoft;

	public List<AudioClip> breathHard;

	[Header("Stella")]
	public List<AudioClip> voiceHitStella;

	public List<AudioClip> voiceOrgasmStella;

	public List<AudioClip> breathSoftStella;

	public List<AudioClip> breathHardStella;

	public List<AudioClip> kissSoftStella;

	public List<AudioClip> kissHardStella;

	[Header("Vacua")]
	public List<AudioClip> voiceHitVacua;

	public List<AudioClip> voiceOrgasmVacua;

	public List<AudioClip> breathSoftVacua;

	public List<AudioClip> breathHardVacua;

	public List<AudioClip> kissSoftVacua;

	public List<AudioClip> kissHardVacua;

	[Header("Nuisance")]
	public List<AudioClip> voiceHitNuisance;

	public List<AudioClip> voiceOrgasmNuisance;

	public List<AudioClip> breathSoftNuisance;

	public List<AudioClip> breathHardNuisance;

	public List<AudioClip> kissSoftNuisance;

	public List<AudioClip> kissHardNuisance;

	public void SetVoiceData(string value)
	{
		charactorName = value;
		voiceHit.Clear();
		voiceOrgasm.Clear();
		breathSoft.Clear();
		breathHard.Clear();
		switch (value)
		{
		case "Stella":
			voiceHit.AddRange(voiceHitStella);
			voiceOrgasm.AddRange(voiceOrgasmStella);
			breathSoft.AddRange(breathSoftStella);
			breathHard.AddRange(breathHardStella);
			break;
		case "StellaKiss":
			voiceHit.AddRange(kissSoftStella);
			voiceOrgasm.AddRange(kissHardVacua);
			breathSoft.AddRange(breathSoftStella);
			breathHard.AddRange(breathHardStella);
			break;
		case "Vacua":
			voiceHit.AddRange(voiceHitVacua);
			voiceOrgasm.AddRange(voiceOrgasmVacua);
			breathSoft.AddRange(breathSoftVacua);
			breathHard.AddRange(breathHardVacua);
			break;
		case "VacuaKiss":
			voiceHit.AddRange(kissSoftVacua);
			voiceOrgasm.AddRange(kissHardVacua);
			breathSoft.AddRange(breathSoftVacua);
			breathHard.AddRange(breathHardVacua);
			break;
		case "Nuisance":
			voiceHit.AddRange(voiceHitNuisance);
			voiceOrgasm.AddRange(voiceOrgasmNuisance);
			breathSoft.AddRange(kissSoftNuisance);
			breathHard.AddRange(kissHardNuisance);
			break;
		case "NuisanceKiss":
			voiceHit.AddRange(kissSoftNuisance);
			voiceOrgasm.AddRange(breathHardNuisance);
			breathSoft.AddRange(kissSoftNuisance);
			breathHard.AddRange(kissHardNuisance);
			break;
		}
	}
}
