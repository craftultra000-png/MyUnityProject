using UnityEngine;

public class CharacterColliderObject : MonoBehaviour
{
	public enum BodyType
	{
		Body = 0,
		Head = 1,
		TitsL = 2,
		TitsR = 3,
		HipL = 4,
		HipR = 5,
		Vagina = 6,
		Anal = 7,
		Belly = 8
	}

	public CharacterLifeManager _characterLifeManager;

	public BodyType bodyType;

	public void HitData(string attackType)
	{
		_characterLifeManager.HitData(bodyType.ToString(), attackType);
	}
}
