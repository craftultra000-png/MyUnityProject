using UnityEngine;

public class BriefingCharacterAnimator : MonoBehaviour
{
	public CharacterAnimation _characterAnimation;

	public CharacterFacialManager _characterFacialManager;

	public CharacterMouthManager _characterMouthManager;

	public CharacterSoundManager _characterSoundManager;

	public BriefingCaracterAnimancer _briefingCaracterAnimancer;

	public BriefingReactionAnimancer _briefingReactionAnimancer;

	public BriefingCharacterManager _briefingCharacterManager;

	[Header("Animation Data")]
	public float feedTime = 0.25f;

	public void PoseChangeEnd()
	{
		_briefingCaracterAnimancer.PoseChangeEnd();
	}

	public void SetFacial()
	{
		_characterFacialManager.isBottom = true;
		_characterFacialManager.isEyesClose02 = true;
	}

	public void RandomSetFacial()
	{
		if (Random.Range(0, 4) == 0)
		{
			_characterFacialManager.isBottom = false;
			_characterFacialManager.isAhe = false;
			_characterFacialManager.isWinkL = false;
			_characterFacialManager.isWinkR = false;
			_characterFacialManager.isEyesClose01 = false;
			_characterFacialManager.isEyesClose02 = false;
			_characterFacialManager.isEyesClose03 = false;
			switch (Random.Range(0, 10))
			{
			case 0:
				_characterFacialManager.isBottom = true;
				_characterFacialManager.isEyesClose01 = true;
				break;
			case 1:
				_characterFacialManager.isBottom = true;
				_characterFacialManager.isEyesClose01 = true;
				break;
			case 2:
				_characterFacialManager.isBottom = true;
				_characterFacialManager.isEyesClose01 = true;
				break;
			case 3:
				_characterFacialManager.isBottom = true;
				_characterFacialManager.isEyesClose01 = true;
				break;
			case 4:
				_characterFacialManager.isBottom = true;
				_characterFacialManager.isEyesClose02 = true;
				break;
			case 5:
				_characterFacialManager.isBottom = true;
				_characterFacialManager.isEyesClose02 = true;
				break;
			case 6:
				_characterFacialManager.isAhe = true;
				_characterFacialManager.isEyesClose03 = true;
				break;
			case 7:
				_characterFacialManager.isBottom = true;
				_characterFacialManager.isWinkL = true;
				_characterFacialManager.isEyesClose02 = true;
				break;
			case 8:
				_characterFacialManager.isBottom = true;
				_characterFacialManager.isWinkR = true;
				_characterFacialManager.isEyesClose03 = true;
				break;
			case 9:
				_characterFacialManager.isBottom = true;
				_characterFacialManager.isWinkL = true;
				_characterFacialManager.isWinkR = true;
				break;
			}
		}
	}

	public void RandomOrgasmSetFacial()
	{
		_characterFacialManager.isBottom = false;
		_characterFacialManager.isAhe = false;
		_characterFacialManager.isWinkL = false;
		_characterFacialManager.isWinkR = false;
		_characterFacialManager.isEyesClose01 = false;
		_characterFacialManager.isEyesClose02 = false;
		_characterFacialManager.isEyesClose03 = false;
		switch (Random.Range(0, 5))
		{
		case 0:
			_characterFacialManager.isAhe = true;
			_characterFacialManager.isEyesClose01 = true;
			break;
		case 1:
			_characterFacialManager.isAhe = true;
			_characterFacialManager.isEyesClose02 = true;
			break;
		case 2:
			_characterFacialManager.isAhe = true;
			_characterFacialManager.isEyesClose03 = true;
			break;
		case 3:
			_characterFacialManager.isWinkL = true;
			_characterFacialManager.isEyesClose02 = true;
			break;
		case 4:
			_characterFacialManager.isWinkR = true;
			_characterFacialManager.isEyesClose03 = true;
			break;
		}
	}

	public void RandomOrgasmIdleSetFacial()
	{
		if (Random.Range(0, 4) == 0)
		{
			_characterFacialManager.isBottom = false;
			_characterFacialManager.isAhe = false;
			_characterFacialManager.isWinkL = false;
			_characterFacialManager.isWinkR = false;
			_characterFacialManager.isEyesClose01 = false;
			_characterFacialManager.isEyesClose02 = false;
			_characterFacialManager.isEyesClose03 = false;
			switch (Random.Range(0, 5))
			{
			case 0:
				_characterFacialManager.isBottom = true;
				_characterFacialManager.isEyesClose02 = true;
				break;
			case 1:
				_characterFacialManager.isBottom = true;
				_characterFacialManager.isEyesClose03 = true;
				break;
			case 2:
				_characterFacialManager.isWinkL = true;
				_characterFacialManager.isEyesClose02 = true;
				break;
			case 3:
				_characterFacialManager.isWinkR = true;
				_characterFacialManager.isEyesClose03 = true;
				break;
			case 4:
				_characterFacialManager.isWinkL = true;
				_characterFacialManager.isWinkR = true;
				break;
			}
		}
	}

	public void RandomLickSe()
	{
		_characterSoundManager.LickSe();
	}

	public void LickSe()
	{
		if (Random.Range(0, 3) == 0)
		{
			_characterSoundManager.LickSe();
		}
	}

	public void RandomVaginaDroplets()
	{
		if (Random.Range(0, 9) == 0)
		{
			_characterAnimation.VaginaDroplets();
		}
	}

	public void VaginaDroplets()
	{
		if (Random.Range(0, 4) == 0)
		{
			_characterAnimation.VaginaDroplets();
		}
	}

	public void VaginaSplash()
	{
		if (Random.Range(0, 4) == 0)
		{
			_characterAnimation.VaginaSplash();
		}
	}

	public void RandomVaginaGlobs()
	{
		if (Random.Range(0, 4) == 0)
		{
			_characterAnimation.VaginaGlobs();
		}
	}

	public void RandomVaginaShot()
	{
		if (Random.Range(0, 4) == 0)
		{
			_characterAnimation.VaginaShot();
		}
	}

	public void RandomVaginaCreamPie()
	{
		if (Random.Range(0, 4) == 0)
		{
			_characterAnimation.VaginaCreamPie();
		}
	}

	public void Talk(string type)
	{
		_briefingCharacterManager.Talk(type);
	}

	public void BarkTalk(string type)
	{
		_briefingCharacterManager.BarkTalk(type);
	}

	public void TalkCount(string type)
	{
		_briefingCharacterManager.TalkCount(type);
	}

	public void RandomHitVoice()
	{
		if (Random.Range(0, 4) == 0)
		{
			_briefingReactionAnimancer.ReactionSet("isReaction", feedTime);
			BarkTalk("Coo");
		}
	}

	public void HitVoice()
	{
		_characterAnimation.HitVoice();
	}

	public void OrgasmVoice()
	{
		_characterAnimation.OrgasmVoice();
		BarkTalk("Coo");
	}
}
