using UnityEngine;

public class FeelerPistonShortOpbject : MonoBehaviour
{
	public CharacterLifeManager _characterLifeManager;

	public CharacterEffectManager _characterEffectManager;

	public CharacterSoundManager _characterSoundManager;

	[Space]
	public CharacterVaginaManager _characterVaginaManager;

	public CharacterAnalManager _characterAnalManager;

	[Header("Reaction Target")]
	public ReactionTargetAnimaton _reactionTargetAnimaton;

	[Header("Type")]
	public bool vagina;

	public bool anal;

	public bool mouth;

	public bool tits;

	[Header("Status")]
	public bool isFirstInsert;

	[Header("Target")]
	public Transform targetObject;

	public Transform rootObject;

	[Header("Status")]
	public bool isAim;

	public bool isAimEnd;

	[Header("Aim Data")]
	public float aimMin;

	public float aimMax = 1f;

	[Space]
	public float aimCurrent;

	public float aimSpeed = 3f;

	[Header("Se")]
	public AudioClip aimSe;

	private void Start()
	{
		aimCurrent = aimMin;
	}

	private void LateUpdate()
	{
		if (isAim)
		{
			if (aimCurrent < aimMax)
			{
				aimCurrent += Time.deltaTime * aimSpeed;
				if (aimCurrent > aimMax)
				{
					isAimEnd = true;
					aimCurrent = aimMax;
					_characterLifeManager.HitData("Reaction", "Reaction");
				}
			}
		}
		else if (aimCurrent > aimMin)
		{
			aimCurrent -= Time.deltaTime * aimSpeed;
			if (aimCurrent < aimMin)
			{
				isAimEnd = false;
				aimCurrent = aimMin;
			}
		}
		base.transform.position = Vector3.Lerp(rootObject.position, targetObject.position, aimCurrent);
	}

	public void ShotSe()
	{
		_characterSoundManager.ShotSe();
	}

	public void PistonSe()
	{
		_characterSoundManager.PistonSe();
	}

	public void FirstInsert()
	{
		if (!isFirstInsert)
		{
			isFirstInsert = true;
			PistonGlobs();
			if (vagina && !_characterLifeManager.isLostVirsin)
			{
				_characterLifeManager.HitData("Vagina", "LostVirsin");
				return;
			}
			PistonDamage();
			_characterLifeManager.PlayHitSe();
		}
	}

	public void PistonGlobs()
	{
		if (vagina)
		{
			_characterEffectManager.VaginaGlobs();
		}
		if (anal)
		{
			_characterEffectManager.AnalGlobs();
		}
		if (mouth)
		{
			_characterEffectManager.MouthGlobs();
		}
		if (tits)
		{
			_characterEffectManager.TitsGlobs();
		}
	}

	public void PistonShot()
	{
		if (vagina)
		{
			_characterEffectManager.VaginaShot();
			_characterLifeManager.SpawnVaginaChild(3);
		}
		if (anal)
		{
			_characterEffectManager.AnalShot();
			_characterLifeManager.SpawnAnalChild(3);
		}
		if (mouth)
		{
			_characterEffectManager.MouthShot();
			_characterLifeManager.BirthChild(2);
		}
		if (tits)
		{
			_characterEffectManager.TitsShot();
			_characterLifeManager.BirthChild(2);
		}
	}

	public void PistonCreamPie()
	{
		if (vagina)
		{
			_characterEffectManager.VaginaCreamPie();
		}
		if (anal)
		{
			_characterEffectManager.AnalCreamPie();
		}
	}

	public void ReactionPiston()
	{
		if (mouth || tits)
		{
			_reactionTargetAnimaton.targetX += Random.Range(0.75f, 1f);
			_reactionTargetAnimaton.targetY += Random.Range(-0.25f, 0.25f);
		}
	}

	public void ReactionShot()
	{
		if (mouth || tits)
		{
			_reactionTargetAnimaton.targetX += Random.Range(0.5f, 1f);
		}
	}

	public void PistonVoice()
	{
		if (Random.Range(0f, 1f) < 0.3f)
		{
			_characterLifeManager.PlayHitSe();
		}
	}

	public void OrgasmVoice()
	{
		_characterLifeManager.PlayOrgasmSe();
	}

	public void PistonDamage()
	{
		if (vagina)
		{
			_characterLifeManager.HitData("Vagina", "Piston");
		}
		if (anal)
		{
			_characterLifeManager.HitData("Anal", "Piston");
		}
		if (mouth)
		{
			_characterLifeManager.HitData("Mouth", "Piston");
		}
		if (tits)
		{
			_characterLifeManager.HitData("Tits", "Piston");
		}
	}

	public void EjaculationDamage()
	{
		if (vagina)
		{
			_characterLifeManager.HitData("Vagina", "Ejaculation");
		}
		if (anal)
		{
			_characterLifeManager.HitData("Anal", "Ejaculation");
		}
		if (mouth)
		{
			_characterLifeManager.HitData("Mouth", "Ejaculation");
		}
		if (tits)
		{
			_characterLifeManager.HitData("Tits", "Ejaculation");
		}
	}

	public void MosaicVaginaOn()
	{
		if (_characterVaginaManager != null)
		{
			_characterVaginaManager.MosaicVaginaOn();
		}
	}

	public void MosaicVaginaOff()
	{
		if (_characterVaginaManager != null)
		{
			_characterVaginaManager.MosaicVaginaOff();
		}
	}

	public void MosaicInsertOn()
	{
		if (_characterVaginaManager != null)
		{
			_characterVaginaManager.MosaicInsertOn();
		}
		if (_characterAnalManager != null)
		{
			_characterAnalManager.MosaicInsertOn();
		}
	}

	public void MosaicInsertOff()
	{
		if (_characterVaginaManager != null)
		{
			_characterVaginaManager.MosaicInsertOff();
		}
		if (_characterAnalManager != null)
		{
			_characterAnalManager.MosaicInsertOff();
		}
	}
}
