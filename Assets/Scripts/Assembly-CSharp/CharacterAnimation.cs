using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
	public FeelerController _feelerController;

	public CharacterFaceManager _characterFaceManager;

	public CharacterMouthManager _characterMouthManager;

	public CharacterEyesManager _characterEyesManager;

	public CharacterEffectManager _characterEffectManager;

	public CharacterTalkManager _characterTalkManager;

	public CharacterLifeManager _characterLifeManager;

	public float time;

	public void ControlOn()
	{
		_feelerController.ControlOn();
	}

	public void ControlOff()
	{
		_feelerController.ControlOff();
	}

	public void EyesChange()
	{
		if (time != Time.time)
		{
			_characterFaceManager.EyesChange();
		}
		time = Time.time;
	}

	public void VaginaDroplets()
	{
		_characterEffectManager.VaginaDroplets();
	}

	public void VaginaSplash()
	{
		_characterEffectManager.VaginaSplash();
	}

	public void VaginaGlobs()
	{
		_characterEffectManager.VaginaGlobs();
	}

	public void VaginaShot()
	{
		_characterEffectManager.VaginaShot();
	}

	public void VaginaCreamPie()
	{
		_characterEffectManager.VaginaCreamPie();
	}

	public void Talk(string type)
	{
		_characterLifeManager.Talk(type);
	}

	public void TalkCount(string type)
	{
		_characterLifeManager.TalkCount(type);
	}

	public void PistonDamage()
	{
		_characterLifeManager.HitData("Vagina", "Piston");
	}

	public void EjaculationDamage()
	{
		_characterLifeManager.HitData("Vagina", "Ejaculation");
	}

	public void HitVoice()
	{
		_characterMouthManager.PlayHitSe();
	}

	public void OrgasmVoice()
	{
		_characterMouthManager.PlayOrgasmSe();
	}
}
