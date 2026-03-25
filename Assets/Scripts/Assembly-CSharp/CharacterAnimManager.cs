using UnityEngine;

public class CharacterAnimManager : MonoBehaviour
{
	public Animator _animator;

	public CharacterFaceManager _characterFaceManager;

	public CharacterMouthManager _characterMouthManager;

	public CharacterEyesManager _characterEyesManager;

	public CharacterAnimancerManager _characterAnimancerManager;

	[Header("Effect and SE")]
	public CharacterEffectManager _characterEffectManager;

	public CharacterSoundManager _characterSoundManager;

	public CharacterSyncManager _characterSyncManager;

	private void Awake()
	{
		_animator = GetComponent<Animator>();
		_animator.SetInteger("AnimNum", -1);
	}

	public void SetCardIdle()
	{
	}

	public void HideCard()
	{
	}

	public void DestroyCard()
	{
	}

	public void Damage()
	{
	}

	public void SyncPosition()
	{
		_characterSyncManager.SyncPosition();
	}

	public void SyncAnimation()
	{
		_characterSyncManager.SyncAnimation();
	}

	public void SetEmote(string value)
	{
		_characterFaceManager.SetEmote(value);
	}

	public void EyesChange()
	{
		_characterFaceManager.EyesChange();
	}

	public void SetFacial(string value)
	{
		_characterFaceManager.SetFacial(value);
	}

	public void TalkATK(string type)
	{
	}

	public void TalkDef(string type)
	{
	}

	public void TalkCountATK(string type)
	{
	}

	public void TalkCountDEF(string type)
	{
	}

	public void TalkCountNoFlinchATK(string type)
	{
	}

	public void TalkCountNoFlinchDEF(string type)
	{
	}

	public void VaginaDamage()
	{
	}

	public void EyeBlinkForceTrue()
	{
	}

	public void HitVoice()
	{
		_characterMouthManager.PlayHitSe();
	}

	public void OrgasmVoice()
	{
		_characterMouthManager.PlayOrgasmSe();
	}

	public void ShoveChest()
	{
		_characterEffectManager.ShoveChest();
	}

	public void ShoveHandL()
	{
		_characterEffectManager.ShoveHandL();
	}

	public void SlapHandL()
	{
		_characterEffectManager.SlapHandL();
	}

	public void SlapHandR()
	{
		_characterEffectManager.SlapHandR();
	}

	public void KickFootL()
	{
		_characterEffectManager.KickFootL();
	}

	public void KickFootR()
	{
		_characterEffectManager.KickFootR();
	}

	public void KissHang()
	{
	}

	public void KissRandomHang()
	{
	}

	public void SpittleHang()
	{
	}

	public void SpittleRandomHang()
	{
	}

	public void TitsAnim(int value)
	{
		_characterAnimancerManager.TitsSet(value);
	}

	public void TitsAnimL(int value)
	{
		_characterAnimancerManager.TitsLSet(value);
	}

	public void TitsAnimR(int value)
	{
		_characterAnimancerManager.TitsRSet(value);
	}

	public void VaginaSplashCount()
	{
	}

	public void VaginaSpermCount()
	{
	}

	public void VaginaAnim(int value)
	{
	}

	public void AnalAnim(int value)
	{
	}

	public void VaginaGlobs()
	{
	}

	public void VaginaSplash()
	{
	}

	public void SlapSe()
	{
		_characterSoundManager.SlapSe();
	}

	public void SpankSe()
	{
		_characterSoundManager.SpankSe();
	}

	public void KickSe()
	{
		_characterSoundManager.KickSe();
	}

	public void ShoveSe()
	{
		_characterSoundManager.ShoveSe();
	}

	public void DownSe()
	{
		_characterSoundManager.DownSe();
	}

	public void KissSe()
	{
		_characterSoundManager.KissSe();
	}

	public void LickSe()
	{
		_characterSoundManager.LickSe();
	}

	public void SuckSe()
	{
		_characterSoundManager.SuckSe();
	}

	public void BiteSe()
	{
		_characterSoundManager.BiteSe();
	}

	public void TitsSe()
	{
		_characterSoundManager.TitsSe();
	}

	public void PistonSe()
	{
		_characterSoundManager.PistonSe();
	}

	public void PistonHipSe()
	{
		_characterSoundManager.PistonHipSe();
	}

	public void ChokeSe()
	{
		_characterSoundManager.ChokeSe();
	}

	public void EnvironmentSe()
	{
	}

	public void Default()
	{
		_characterSoundManager.Default();
	}

	public void Stone()
	{
		_characterSoundManager.Stone();
	}

	public void Clothe()
	{
		_characterSoundManager.Clothe();
	}

	public void Wood()
	{
		_characterSoundManager.Wood();
	}

	public void Grass()
	{
		_characterSoundManager.Grass();
	}

	public void Dirt()
	{
		_characterSoundManager.Dirt();
	}
}
