using UnityEngine;

public class StuntAnimation : MonoBehaviour
{
	public CharacterPositionManager _characterPositionManager;

	public void EyesChange()
	{
	}

	public void Release()
	{
		_characterPositionManager.SetDefaultBody();
	}

	public void ReleaseEnd()
	{
		_characterPositionManager.SetDefaultEnd();
	}
}
