using System.Collections.Generic;
using UnityEngine;

public class FeelerPistonObject : MonoBehaviour
{
	public CharacterLifeManager _characterLifeManager;

	public CharacterEffectManager _characterEffectManager;

	public CharacterSoundManager _characterSoundManager;

	[Space]
	public CharacterVaginaManager _characterVaginaManager;

	public CharacterAnalManager _characterAnalManager;

	[Header("Reaction Target")]
	public ReactionTargetAnimaton _reactionTargetAnimaton;

	[Header("Aim")]
	public bool unUseAim;

	public List<Transform> baseBones;

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

	public Transform pairObject;

	public Transform bendObject;

	[Header("Piston")]
	public Transform pistonTargetObject;

	public Transform pistonBone;

	[Header("Calc")]
	public float calcBend;

	public AnimationCurve bendCurve;

	public List<Vector3> baseBonesPosition;

	public Vector3 oldPositionP0;

	public Vector3 oldPositionP1;

	public Vector3 oldPositionP2;

	public Vector3 oldPositionP3;

	[Header("Status")]
	public bool isAim;

	public bool isAimEnd;

	[Header("Aim Data")]
	public float aimMin = 0.2f;

	public float aimMax = 0.95f;

	[Space]
	public float aimCurrent;

	public float aimSpeed = 3f;

	[Header("Effect Time Check")]
	public float time;

	[Header("Aim Position")]
	public float distance;

	public float calcDistance;

	[Header("Bend Object")]
	public FeelerNoisePosition _feelerNoisePosition;

	public Vector3 bendDefaultPositon;

	public Vector3 bendCalcPositon;

	public Quaternion bone0DefaultRotation;

	[Header("Se")]
	public AudioClip aimSe;

	private void Start()
	{
		if (!unUseAim)
		{
			aimCurrent = aimMin;
			baseBonesPosition.Clear();
			for (int i = 0; i < baseBones.Count; i++)
			{
				baseBonesPosition.Add(Vector3.zero);
			}
			bendDefaultPositon = _feelerNoisePosition.transform.localPosition;
			bone0DefaultRotation = baseBones[0].localRotation;
		}
	}

	private void LateUpdate()
	{
		if (unUseAim)
		{
			return;
		}
		if (isAim)
		{
			if (aimCurrent < aimMax)
			{
				aimCurrent += Time.deltaTime * aimSpeed;
				if (aimCurrent > aimMax)
				{
					isAimEnd = true;
					aimCurrent = aimMax;
					if (vagina)
					{
						_characterLifeManager.vaginaCount++;
					}
					if (anal)
					{
						_characterLifeManager.analCount++;
					}
					if (mouth)
					{
						_characterLifeManager.headCount++;
					}
					if (tits)
					{
						_characterLifeManager.titsCount++;
					}
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
		_feelerNoisePosition.aimCurrent = aimCurrent;
		base.transform.position = Vector3.Lerp(rootObject.position, targetObject.position, aimCurrent);
		bendCalcPositon = bendDefaultPositon * aimCurrent;
		_feelerNoisePosition.defaultPosition = bendCalcPositon;
		Vector3 normalized = (pistonTargetObject.position - pistonBone.position).normalized;
		Quaternion quaternion = Quaternion.FromToRotation(pistonBone.up, normalized);
		pistonBone.rotation = quaternion * pistonBone.rotation;
		float num = Vector3.Distance(pistonBone.position, rootObject.position);
		float num2 = Vector3.Distance(bendObject.position, rootObject.position);
		float num3 = Mathf.Clamp01((num - 0.1f) / num2);
		Vector3 normalized2 = Vector3.Cross(-Camera.main.transform.up, normalized).normalized;
		Vector3 normalized3 = Vector3.Cross(normalized, normalized2).normalized;
		float num4 = num * 0.5f * num3;
		oldPositionP0 = Vector3.Lerp(oldPositionP0, pistonBone.position, 0.2f);
		oldPositionP1 = Vector3.Lerp(oldPositionP1, bendObject.position, 0.2f);
		Vector3 vector = oldPositionP0;
		Vector3 vector2 = oldPositionP1;
		Vector3 vector3 = (vector2 + rootObject.position) * 0.5f;
		oldPositionP2 = Vector3.Lerp(oldPositionP2, vector3 + normalized3 * num4, 0.2f);
		Vector3 p = oldPositionP2;
		Vector3 position = rootObject.position;
		for (int i = 0; i < baseBones.Count; i++)
		{
			float num5 = (float)i / (float)(baseBones.Count - 1);
			float t = bendCurve.Evaluate(num5);
			Vector3 value = BezierCubic(vector, vector2, p, position, t);
			if (value.y < 0f)
			{
				value.y = Mathf.Lerp(value.y, 0f, 0.2f);
			}
			if (i == 0)
			{
				baseBonesPosition[i] = vector;
			}
			else
			{
				baseBonesPosition[i] = value;
			}
		}
		for (int j = 0; j < baseBones.Count; j++)
		{
			normalized = ((j >= baseBones.Count - 1) ? (baseBonesPosition[j] - baseBonesPosition[j - 1]).normalized : (baseBonesPosition[j + 1] - baseBonesPosition[j]).normalized);
			Quaternion rotation = Quaternion.identity;
			if (normalized != Vector3.zero)
			{
				rotation = ((j != 0) ? Quaternion.LookRotation(normalized, -baseBones[j - 1].forward) : Quaternion.LookRotation(normalized, pairObject.forward));
			}
			rotation *= Quaternion.Euler(90f, 0f, 0f);
			baseBones[j].rotation = rotation;
			baseBones[j].position = baseBonesPosition[j];
		}
	}

	private Vector3 BezierCubic(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
	{
		float num = 1f - t;
		float num2 = t * t;
		float num3 = num * num;
		float num4 = num3 * num;
		float num5 = num2 * t;
		return num4 * p0 + 3f * num3 * t * p1 + 3f * num * num2 * p2 + num5 * p3;
	}

	public void ShotSe()
	{
		_characterSoundManager.ShotSe();
	}

	public void PistonSe()
	{
		if (time != Time.time)
		{
			_characterSoundManager.PistonSe();
		}
		time = Time.time;
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
		if (time != Time.time)
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
		time = Time.time;
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
		if (time != Time.time && (mouth || tits))
		{
			_reactionTargetAnimaton.targetX += Random.Range(0.75f, 1f);
			_reactionTargetAnimaton.targetY += Random.Range(-0.25f, 0.25f);
		}
		time = Time.time;
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
		if (time != Time.time && Random.Range(0f, 1f) < 0.3f)
		{
			_characterLifeManager.PlayHitSe();
		}
		time = Time.time;
	}

	public void OrgasmVoice()
	{
		_characterLifeManager.PlayOrgasmSe();
	}

	public void PistonDamage()
	{
		if (time != Time.time)
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
		time = Time.time;
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
