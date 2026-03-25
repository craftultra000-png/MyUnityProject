using System.Collections.Generic;
using UnityEngine;

public class CharacterEffectManager : MonoBehaviour
{
	public CharacterSoundManager _characterSoundManager;

	public Transform effectStocker;

	[Header("Effect Link")]
	public CharacterDripManager _characterDripManagervirsin;

	[Header("Status")]
	public bool isLostVirsin;

	public bool isTitsGag;

	public bool isTitsMilk;

	[Header("Count")]
	public float milkCount;

	[Header("Character")]
	public Transform vagina;

	public Transform anal;

	public Transform mouth;

	[Space]
	public Transform titsPiston;

	public Transform titsSteam;

	public Transform nippleL;

	public Transform nippleR;

	[Space]
	public Transform handL;

	public Transform handR;

	public Transform footL;

	public Transform footR;

	[Space]
	public Transform root;

	public Transform belly;

	public Transform chest;

	[Header("Effect Drip")]
	public GameObject vaginaDripEffect;

	public GameObject analDripEffect;

	[Header("Effect Vagina")]
	public GameObject vaginaDropletsEffect;

	public GameObject vaginaSplashEffect;

	public GameObject peeSplashEffect;

	[Header("Effect Piston")]
	public GameObject virsinLostEffect;

	public GameObject pistonVaginaGlobsEffect;

	public GameObject pistonVaginaShotEffect;

	public GameObject pistonAnalGlobsEffect;

	public GameObject pistonAnalShotEffect;

	public GameObject pistonMouthGlobsEffect;

	public GameObject pistonMouthShotEffect;

	public GameObject pistonTitsGlobsEffect;

	public GameObject pistonTitsShotEffect;

	[Header("Effect MIlk")]
	public GameObject milkDischargeEffect;

	public GameObject milkSplashEffect;

	[Header("Effect Steam")]
	public GameObject steamTitsEffect;

	public GameObject steamVaginaEffect;

	public GameObject steamAnalEffect;

	public GameObject steamHandEffect;

	public GameObject steamFootEffect;

	public GameObject steamBellyEffect;

	public GameObject steamNippleEffect;

	[Header("Effect Cahnge")]
	public GameObject costumeChangeEfefct;

	[Header("Effect Shot")]
	public List<GameObject> meltEffect;

	[Header("Effect Body")]
	public GameObject shoveEffect;

	public GameObject spankEffect;

	public GameObject kickEffect;

	[Header("Character Effect")]
	public SkinnedMeshRenderer bodyMesh;

	public GameObject heartStreamEffect;

	[Header("Other Script")]
	public FeelerMilkTankObject _feelerMilkTankObjectL;

	public FeelerMilkTankObject _feelerMilkTankObjectR;

	public void StockerClear()
	{
		foreach (Transform item in effectStocker)
		{
			Object.Destroy(item.gameObject);
		}
	}

	public void VaginaDroplets()
	{
		Object.Instantiate(vaginaDropletsEffect, vagina.position, vagina.rotation, vagina.transform);
		_characterSoundManager.DropletsSe();
		if (OnomatopoeiaManager.instance.useOtomanopoeia)
		{
			OnomatopoeiaManager.instance.SpawnOnomatopoeia(vagina.position, null, "Dloplets", Camera.main.transform);
		}
	}

	public void VaginaSplash()
	{
		GameObject gameObject = Object.Instantiate(vaginaSplashEffect, vagina.position, vagina.rotation, vagina.transform);
		_characterSoundManager.SplashSe();
		if (OnomatopoeiaManager.instance.useOtomanopoeia)
		{
			SpermOnomatopeObject component = gameObject.GetComponent<SpermOnomatopeObject>();
			component.useOnomatopoeia = true;
			component.shotStocker = vagina.transform;
			OnomatopoeiaManager.instance.SpawnOnomatopoeia(vagina.position, null, "Splash", Camera.main.transform);
		}
	}

	public void PeeSplash()
	{
		GameObject gameObject = Object.Instantiate(peeSplashEffect, vagina.position, vagina.rotation, vagina.transform);
		_characterSoundManager.PeeSe();
		if (OnomatopoeiaManager.instance.useOtomanopoeia)
		{
			SpermOnomatopeObject component = gameObject.GetComponent<SpermOnomatopeObject>();
			component.useOnomatopoeia = true;
			component.shotStocker = vagina.transform;
			OnomatopoeiaManager.instance.SpawnOnomatopoeia(vagina.position, null, "Pee", Camera.main.transform);
		}
	}

	public void LostVirsin()
	{
		if (!isLostVirsin)
		{
			isLostVirsin = true;
			GameObject effectLink = Object.Instantiate(virsinLostEffect, vagina.position, vagina.rotation, vagina.transform);
			_characterSoundManager.LostVirsinSe();
			_characterDripManagervirsin.isSpawn = true;
			_characterDripManagervirsin.effectLink = effectLink;
			if (OnomatopoeiaManager.instance.useOtomanopoeia)
			{
				OnomatopoeiaManager.instance.SpawnOnomatopoeia(vagina.position, null, "LostVirsin", Camera.main.transform);
			}
		}
	}

	public void VaginaGlobs()
	{
		Object.Instantiate(pistonVaginaGlobsEffect, vagina.position, vagina.rotation, vagina.transform);
		_characterSoundManager.PistonSe();
		if (OnomatopoeiaManager.instance.useOtomanopoeia)
		{
			OnomatopoeiaManager.instance.SpawnOnomatopoeia(vagina.position, null, "Globs", Camera.main.transform);
		}
	}

	public void VaginaShot()
	{
		Object.Instantiate(pistonVaginaShotEffect, vagina.position, vagina.rotation, vagina.transform);
		_characterSoundManager.ShotSe();
		if (OnomatopoeiaManager.instance.useOtomanopoeia)
		{
			OnomatopoeiaManager.instance.SpawnOnomatopoeia(vagina.position, null, "Shot", Camera.main.transform);
		}
	}

	public void VaginaCreamPie()
	{
		Object.Instantiate(vaginaDripEffect, vagina.position, vagina.rotation, vagina.transform);
		_characterSoundManager.DripSe();
		if (OnomatopoeiaManager.instance.useOtomanopoeia)
		{
			OnomatopoeiaManager.instance.SpawnOnomatopoeia(vagina.position, null, "CreamPie", Camera.main.transform);
		}
	}

	public void AnalGlobs()
	{
		Object.Instantiate(pistonAnalGlobsEffect, anal.position, anal.rotation, anal.transform);
		_characterSoundManager.PistonSe();
		if (OnomatopoeiaManager.instance.useOtomanopoeia)
		{
			OnomatopoeiaManager.instance.SpawnOnomatopoeia(anal.position, null, "Globs", Camera.main.transform);
		}
	}

	public void AnalShot()
	{
		Object.Instantiate(pistonAnalShotEffect, anal.position, anal.rotation, anal.transform);
		_characterSoundManager.ShotSe();
		if (OnomatopoeiaManager.instance.useOtomanopoeia)
		{
			OnomatopoeiaManager.instance.SpawnOnomatopoeia(anal.position, null, "Shot", Camera.main.transform);
		}
	}

	public void AnalCreamPie()
	{
		Object.Instantiate(analDripEffect, anal.position, anal.rotation, anal.transform);
		_characterSoundManager.DripSe();
		if (OnomatopoeiaManager.instance.useOtomanopoeia)
		{
			OnomatopoeiaManager.instance.SpawnOnomatopoeia(anal.position, null, "CreamPie", Camera.main.transform);
		}
	}

	public void MouthGlobs()
	{
		Object.Instantiate(pistonMouthGlobsEffect, mouth.position, mouth.rotation, mouth.transform);
		_characterSoundManager.PistonSe();
		if (OnomatopoeiaManager.instance.useOtomanopoeia)
		{
			OnomatopoeiaManager.instance.SpawnOnomatopoeia(mouth.position, null, "Globs", Camera.main.transform);
		}
	}

	public void MouthShot()
	{
		Object.Instantiate(pistonMouthShotEffect, mouth.position, mouth.rotation, mouth.transform);
		_characterSoundManager.ShotSe();
		if (OnomatopoeiaManager.instance.useOtomanopoeia)
		{
			OnomatopoeiaManager.instance.SpawnOnomatopoeia(mouth.position, null, "Shot", Camera.main.transform);
		}
	}

	public void TitsGlobs()
	{
		Object.Instantiate(pistonTitsGlobsEffect, titsPiston.position, titsPiston.rotation, titsPiston.transform);
		_characterSoundManager.PistonSe();
		if (OnomatopoeiaManager.instance.useOtomanopoeia)
		{
			OnomatopoeiaManager.instance.SpawnOnomatopoeia(titsPiston.position, null, "Globs", Camera.main.transform);
		}
	}

	public void TitsShot()
	{
		Shot(0, titsPiston, 2f);
		Object.Instantiate(pistonTitsShotEffect, titsPiston.position, titsPiston.rotation, titsPiston.transform);
		_characterSoundManager.ShotSe();
		if (OnomatopoeiaManager.instance.useOtomanopoeia)
		{
			OnomatopoeiaManager.instance.SpawnOnomatopoeia(titsPiston.position, null, "Shot", Camera.main.transform);
		}
	}

	public void TitsSuck(float value)
	{
		milkCount += value;
	}

	public void TitsSplashL(float value)
	{
		if (isTitsMilk && !isTitsGag)
		{
			milkCount += value;
			Object.Instantiate(milkSplashEffect, nippleL.position, nippleL.rotation, nippleL.transform);
			Object.Instantiate(milkDischargeEffect, nippleL.position, nippleL.rotation, nippleL.transform);
			_characterSoundManager.MilkSe();
			if (OnomatopoeiaManager.instance.useOtomanopoeia)
			{
				OnomatopoeiaManager.instance.SpawnOnomatopoeia(nippleL.position, null, "SuckMilk", Camera.main.transform);
			}
		}
		else if (isTitsMilk && isTitsGag)
		{
			milkCount += value;
			_feelerMilkTankObjectL.AddMilk(0.02f);
		}
	}

	public void TitsSplashR(float value)
	{
		if (isTitsMilk && !isTitsGag)
		{
			milkCount += value;
			Object.Instantiate(milkSplashEffect, nippleR.position, nippleR.rotation, nippleR.transform);
			Object.Instantiate(milkDischargeEffect, nippleR.position, nippleR.rotation, nippleR.transform);
			_characterSoundManager.MilkSe();
			if (OnomatopoeiaManager.instance.useOtomanopoeia)
			{
				OnomatopoeiaManager.instance.SpawnOnomatopoeia(nippleR.position, null, "SuckMilk", Camera.main.transform);
			}
		}
		else if (isTitsMilk && isTitsGag)
		{
			milkCount += value;
			_feelerMilkTankObjectR.AddMilk(0.02f);
		}
	}

	public void TitsSteam()
	{
		Object.Instantiate(steamTitsEffect, titsSteam.position, titsSteam.rotation, titsSteam.transform);
		if (OnomatopoeiaManager.instance.useOtomanopoeia)
		{
			OnomatopoeiaManager.instance.SpawnOnomatopoeia(titsSteam.position, null, "WetSteam", Camera.main.transform);
		}
	}

	public void VaginaSteam()
	{
		Object.Instantiate(steamVaginaEffect, vagina.position, vagina.rotation, vagina.transform);
		if (OnomatopoeiaManager.instance.useOtomanopoeia)
		{
			OnomatopoeiaManager.instance.SpawnOnomatopoeia(vagina.position, null, "WetSteam", Camera.main.transform);
		}
	}

	public void AnalSteam()
	{
		Object.Instantiate(steamAnalEffect, anal.position, anal.rotation, anal.transform);
		if (OnomatopoeiaManager.instance.useOtomanopoeia)
		{
			OnomatopoeiaManager.instance.SpawnOnomatopoeia(anal.position, null, "WetSteam", Camera.main.transform);
		}
	}

	public void HandLSteam()
	{
		Object.Instantiate(steamHandEffect, handL.position, handL.rotation, handL.transform);
		if (OnomatopoeiaManager.instance.useOtomanopoeia)
		{
			OnomatopoeiaManager.instance.SpawnOnomatopoeia(handL.position, null, "WetSteam", Camera.main.transform);
		}
	}

	public void HandRSteam()
	{
		Object.Instantiate(steamHandEffect, handR.position, handR.rotation, handR.transform);
		if (OnomatopoeiaManager.instance.useOtomanopoeia)
		{
			OnomatopoeiaManager.instance.SpawnOnomatopoeia(handR.position, null, "WetSteam", Camera.main.transform);
		}
	}

	public void FootLSteam()
	{
		Object.Instantiate(steamFootEffect, footL.position, footL.rotation, footL.transform);
		if (OnomatopoeiaManager.instance.useOtomanopoeia)
		{
			OnomatopoeiaManager.instance.SpawnOnomatopoeia(footL.position, null, "WetSteam", Camera.main.transform);
		}
	}

	public void FootRSteam()
	{
		Object.Instantiate(steamFootEffect, footR.position, footR.rotation, footR.transform);
		if (OnomatopoeiaManager.instance.useOtomanopoeia)
		{
			OnomatopoeiaManager.instance.SpawnOnomatopoeia(footR.position, null, "WetSteam", Camera.main.transform);
		}
	}

	public void CostumeChange()
	{
		Object.Instantiate(costumeChangeEfefct, root.position, root.rotation, root.transform);
	}

	public void BellyChange()
	{
		Object.Instantiate(steamBellyEffect, belly.position, belly.rotation, belly.transform);
	}

	public void NippleChange()
	{
		Object.Instantiate(steamNippleEffect, nippleL.position, nippleL.rotation, nippleL.transform);
		Object.Instantiate(steamNippleEffect, nippleR.position, nippleR.rotation, nippleR.transform);
	}

	public void Shot(int power, Transform point, float speed)
	{
		GameObject obj = Object.Instantiate(meltEffect[power], point.position, point.rotation, effectStocker);
		Rigidbody component = obj.GetComponent<Rigidbody>();
		Vector3 force = point.forward.normalized * speed;
		component.AddForce(force);
		obj.GetComponent<ShotObject>().shotStocker = effectStocker;
		obj.SetActive(value: true);
	}

	public void ShoveChest()
	{
		if (Application.isPlaying)
		{
			Object.Instantiate(shoveEffect, chest.position, chest.rotation, effectStocker);
			_characterSoundManager.ShoveSe();
		}
	}

	public void ShoveHandL()
	{
		if (Application.isPlaying)
		{
			Object.Instantiate(shoveEffect, handL.position, handL.rotation, effectStocker);
			_characterSoundManager.ShoveSe();
		}
	}

	public void SlapHandL()
	{
		if (Application.isPlaying)
		{
			Object.Instantiate(spankEffect, handL.position, handL.rotation, effectStocker);
			_characterSoundManager.SlapSe();
		}
	}

	public void SlapHandR()
	{
		if (Application.isPlaying)
		{
			Object.Instantiate(spankEffect, handR.position, handR.rotation, effectStocker);
			_characterSoundManager.SlapSe();
		}
	}

	public void KickFootL()
	{
		if (Application.isPlaying)
		{
			Object.Instantiate(kickEffect, footL.position, footL.rotation, effectStocker);
			_characterSoundManager.KickSe();
		}
	}

	public void KickFootR()
	{
		if (Application.isPlaying)
		{
			Object.Instantiate(kickEffect, footR.position, footR.rotation, effectStocker);
			_characterSoundManager.KickSe();
		}
	}

	public void HeartStream()
	{
		if (!Application.isPlaying)
		{
			return;
		}
		GameObject gameObject = Object.Instantiate(heartStreamEffect, base.transform.position, base.transform.rotation, effectStocker);
		SetParticleSystemMesh(gameObject);
		foreach (Transform item in gameObject.transform)
		{
			SetParticleSystemMesh(item.gameObject);
		}
	}

	private void SetParticleSystemMesh(GameObject obj)
	{
		ParticleSystem component = obj.GetComponent<ParticleSystem>();
		if (!(component == null))
		{
			ParticleSystem.ShapeModule shape = component.shape;
			shape.shapeType = ParticleSystemShapeType.SkinnedMeshRenderer;
			shape.skinnedMeshRenderer = bodyMesh;
		}
	}
}
