using UnityEngine;

public class CharacterColliderBelly : MonoBehaviour
{
	public CharacterBellySwayManager _characterBellySwayManager;

	public void HitPosition(Vector3 data)
	{
		_characterBellySwayManager.HitSway(data);
	}
}
