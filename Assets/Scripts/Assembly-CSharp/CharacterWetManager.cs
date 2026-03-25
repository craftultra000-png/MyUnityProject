using UnityEngine;

public class CharacterWetManager : MonoBehaviour
{
	public CharacterTypeManager _characterTypeManager;

	public CharacterCostumeSwitch _characterCostumeSwitch;

	[Header("Effect")]
	public GameObject wetEffect;

	[Header("Data")]
	public bool isWet;

	private void Start()
	{
	}

	public void SetWet()
	{
		Debug.LogError("Set Wet: " + isWet);
		MaterialWet(!isWet);
	}

	public void MaterialWet(bool wet)
	{
		isWet = wet;
		_characterTypeManager.MaterialWet(isWet);
		_characterCostumeSwitch.MaterialWet(isWet);
		wetEffect.SetActive(isWet);
	}
}
