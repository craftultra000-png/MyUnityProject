using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utage;

public class OnomatopoeiaManager : MonoBehaviour
{
	public static OnomatopoeiaManager instance;

	public Transform shotStocker;

	[Header("Onomatopoeia")]
	public GameObject onomatopoeia;

	[Header("Language")]
	public string lang;

	public int langType;

	private LanguageManagerBase langManager;

	public List<Text> roundTextList;

	public List<ButtonTriggerGUI> roundTextButtonList;

	public Color enableColor;

	public Color disableColor;

	[Header("Status")]
	public bool useOtomanopoeia = true;

	public int heartCount;

	[Header("Onomatopoeia Player")]
	public string key;

	public List<string> spermHit;

	public List<string> shoot;

	public List<string> bind;

	public List<string> spanking;

	public List<string> touch;

	public List<string> rub;

	[Space]
	public List<string> syringeShot;

	public List<string> syringeHit;

	public List<string> syringeDrop;

	[Space]
	public List<string> bounce;

	public List<string> stick;

	public List<string> bite;

	public List<string> lick;

	[Space]
	public List<string> suck;

	public List<string> suckFit;

	public List<string> suckTits;

	public List<string> suckMilk;

	[Space]
	public List<string> milkDrip;

	public List<string> milkTank;

	[Space]
	[Header("Onomatopoeia Character")]
	public List<string> wetSteam;

	public List<string> dloplets;

	public List<string> splash;

	public List<string> pee;

	public List<string> lostVirsin;

	public List<string> blood;

	public List<string> globs;

	public List<string> shot;

	public List<string> creamPie;

	public List<string> drip;

	public List<string> waterDrop;

	[Header("Onomatopoeia CharacterFacial")]
	public List<string> breath;

	public List<string> breathHard;

	public List<string> dizzy;

	public List<string> heart;

	public List<string> overHeat;

	[Header("Onomatopoeia Costume")]
	public List<string> costumeChange;

	public List<string> costumeWear;

	public List<string> costumeBreak;

	public List<string> costumeMelt;

	[Header("Onomatopoeia Fuck")]
	public List<string> vaginaOpen;

	[Header("Onomatopoeia Child")]
	public List<string> childSpawn;

	public List<string> childDrop;

	public List<string> childDig;

	public List<string> childGlow;

	[Header("Onomatopoeia Gimmick")]
	public List<string> jelly;

	public List<string> jellyLight;

	public List<string> jellyCanon;

	public List<string> jellyShot;

	public List<string> jellyFook;

	[Space]
	public List<string> coralSteam;

	[Space]
	public List<string> eaterOpen;

	public List<string> eaterEating;

	public List<string> horseRide;

	public List<string> wartBed;

	public List<string> wartBedOpen;

	[Header("Color")]
	public List<Color> spermColor;

	public Color bindColor;

	public Color spankingColor;

	public Color touchColor;

	public Color rubColor;

	[Space]
	public Color syringeColor;

	public Color bounceColor;

	public Color stickColor;

	public Color biteColor;

	public Color lickColor;

	public Color peeColor;

	[Space]
	public Color dizzyColor;

	public Color heartColor;

	public Color overHeatColor;

	[Space]
	public Color childColor;

	[Space]
	public Color costumeChangeColor;

	public Color costumeWearColor;

	public Color costumeBreakColor;

	public Color costumeMeltColor;

	[Space]
	public Color pinkLight;

	public Color pinkDeep;

	[Space]
	public Color vaginaColor;

	public Color bloodColor;

	public Color pistonColor;

	public Color shotColor;

	public Color creamPieColor;

	[Space]
	public Color jerryColor;

	public Color coralColor;

	public Color eaterColor;

	public Color gimmickColor;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		if (ES3.KeyExists("LanguageOnomatopoeia"))
		{
			langType = ES3.Load<int>("LanguageOnomatopoeia");
			SetLanguage(langType);
		}
		else if (Application.systemLanguage == SystemLanguage.Japanese)
		{
			SetLanguage(1);
		}
		else
		{
			SetLanguage(0);
		}
	}

	public void SetLanguage(int value)
	{
		langType = value;
		if (langType == -1)
		{
			useOtomanopoeia = false;
			ES3.Save("LanguageOnomatopoeia", langType);
			roundTextButtonList[0].defaultColor = enableColor;
			roundTextButtonList[1].defaultColor = disableColor;
			roundTextButtonList[2].defaultColor = disableColor;
		}
		else if (langType == 0)
		{
			lang = "English";
			useOtomanopoeia = true;
			ES3.Save("LanguageOnomatopoeia", langType);
			roundTextButtonList[0].defaultColor = disableColor;
			roundTextButtonList[1].defaultColor = enableColor;
			roundTextButtonList[2].defaultColor = disableColor;
		}
		else if (langType == 1)
		{
			lang = "Japanese";
			useOtomanopoeia = true;
			ES3.Save("LanguageOnomatopoeia", langType);
			roundTextButtonList[0].defaultColor = disableColor;
			roundTextButtonList[1].defaultColor = disableColor;
			roundTextButtonList[2].defaultColor = enableColor;
		}
		roundTextButtonList[0].ColorReset();
		roundTextButtonList[1].ColorReset();
		roundTextButtonList[2].ColorReset();
	}

	public void SpawnOnomatopoeia(Vector3 position, Transform parent, string type, Transform lookTarget)
	{
		if (!useOtomanopoeia)
		{
			return;
		}
		GameObject gameObject = Object.Instantiate(onomatopoeia, position, Quaternion.identity, shotStocker.transform);
		OnomatopoeiaObject component = gameObject.GetComponent<OnomatopoeiaObject>();
		component.lookTarget = lookTarget;
		component.lang = lang;
		component.heartCount = heartCount;
		if (parent == null)
		{
			gameObject.transform.parent = shotStocker;
		}
		else
		{
			gameObject.transform.parent = parent;
		}
		component.floatSpeed = Random.Range(0.05f, 0.075f);
		switch (type)
		{
		case "SpermHit0":
		case "SpermHit1":
		case "SpermHit2":
			key = spermHit[Random.Range(0, spermHit.Count)];
			component.currentTime = 1f;
			component.text.fontSize = Random.Range(0.25f, 0.3f);
			component.isFloat = true;
			if (type == "SpermHit0")
			{
				component.text.color = spermColor[0];
			}
			if (type == "SpermHit1")
			{
				component.text.color = spermColor[1];
			}
			if (type == "SpermHit2")
			{
				component.text.color = spermColor[2];
			}
			break;
		case "Shoot":
			component.currentTime = 2f;
			component.isFloat = true;
			key = shoot[Random.Range(0, shoot.Count)];
			component.text.fontSize = Random.Range(0.45f, 0.55f);
			break;
		case "Bind":
			component.currentTime = 3f;
			component.isSway = true;
			key = bind[Random.Range(0, bind.Count)];
			component.text.fontSize = Random.Range(0.25f, 0.3f);
			component.text.color = bindColor;
			break;
		case "Spanking":
			component.currentTime = 1f;
			component.isBomb = true;
			key = spanking[Random.Range(0, spanking.Count)];
			component.text.fontSize = Random.Range(0.4f, 0.45f);
			component.text.color = spankingColor;
			break;
		case "Touch":
			component.currentTime = 1f;
			component.isSway = true;
			key = touch[Random.Range(0, touch.Count)];
			component.text.fontSize = Random.Range(0.25f, 0.3f);
			component.text.color = touchColor;
			break;
		case "Rub":
			component.currentTime = 1f;
			component.isSway = true;
			key = rub[Random.Range(0, rub.Count)];
			component.text.fontSize = Random.Range(0.25f, 0.3f);
			component.text.color = rubColor;
			break;
		case "SyringeShot":
			component.currentTime = 1f;
			component.isBomb = true;
			key = syringeShot[Random.Range(0, syringeShot.Count)];
			component.text.fontSize = Random.Range(0.25f, 0.3f);
			component.text.color = syringeColor;
			break;
		case "SyringeHit":
			component.currentTime = 2f;
			component.isSway = true;
			key = syringeHit[Random.Range(0, syringeHit.Count)];
			component.text.fontSize = Random.Range(0.25f, 0.3f);
			component.text.color = syringeColor;
			break;
		case "SyringeDrop":
			component.currentTime = 1f;
			component.isFloat = true;
			key = syringeDrop[Random.Range(0, syringeDrop.Count)];
			component.text.fontSize = Random.Range(0.25f, 0.3f);
			component.text.color = syringeColor;
			break;
		case "Bounce":
			component.currentTime = 1f;
			component.isBomb = true;
			key = bounce[Random.Range(0, bounce.Count)];
			component.text.fontSize = Random.Range(0.3f, 0.35f);
			component.text.color = bounceColor;
			break;
		case "Stick":
			component.currentTime = 1f;
			component.isSway = true;
			key = stick[Random.Range(0, stick.Count)];
			component.text.fontSize = Random.Range(0.25f, 0.3f);
			component.text.color = stickColor;
			break;
		case "Bite":
			component.currentTime = 1f;
			component.isBomb = true;
			key = bite[Random.Range(0, bite.Count)];
			component.text.fontSize = Random.Range(0.25f, 0.3f);
			component.text.color = biteColor;
			break;
		case "Lick":
			component.currentTime = 1f;
			component.isSway = true;
			key = lick[Random.Range(0, lick.Count)];
			component.text.fontSize = Random.Range(0.25f, 0.3f);
			component.text.color = lickColor;
			break;
		case "CostumeChange":
			component.currentTime = 2f;
			component.isBomb = true;
			key = costumeChange[Random.Range(0, costumeChange.Count)];
			component.text.fontSize = Random.Range(0.4f, 0.45f);
			component.text.color = costumeChangeColor;
			break;
		case "CostumeWear":
			component.currentTime = 2f;
			component.isBomb = true;
			key = costumeWear[Random.Range(0, costumeWear.Count)];
			component.text.fontSize = Random.Range(0.4f, 0.45f);
			component.text.color = costumeWearColor;
			break;
		case "CostumeBreak":
			component.currentTime = 2f;
			component.isBomb = true;
			key = costumeBreak[Random.Range(0, costumeBreak.Count)];
			component.text.fontSize = Random.Range(0.4f, 0.45f);
			component.text.color = costumeBreakColor;
			break;
		case "CostumeMelt":
			component.currentTime = 2f;
			component.isBomb = true;
			key = costumeMelt[Random.Range(0, costumeMelt.Count)];
			component.text.fontSize = Random.Range(0.4f, 0.45f);
			component.text.color = costumeMeltColor;
			break;
		case "Jelly":
			component.currentTime = 2f;
			component.isSway = true;
			key = jelly[Random.Range(0, jelly.Count)];
			component.text.fontSize = Random.Range(0.25f, 0.3f);
			break;
		case "JellyLight":
			component.currentTime = 1f;
			component.isBomb = true;
			key = jellyLight[Random.Range(0, jellyLight.Count)];
			component.text.fontSize = Random.Range(0.15f, 0.2f);
			component.text.color = jerryColor;
			break;
		case "JellyCanon":
			component.currentTime = 1f;
			component.isSway = true;
			key = jellyCanon[Random.Range(0, jellyCanon.Count)];
			component.text.fontSize = Random.Range(0.15f, 0.2f);
			component.text.color = jerryColor;
			break;
		case "JellyShot":
			component.currentTime = 0.5f;
			component.isBomb = true;
			key = jellyShot[Random.Range(0, jellyShot.Count)];
			component.text.fontSize = Random.Range(0.15f, 0.2f);
			component.text.color = jerryColor;
			break;
		case "JellyFook":
			component.currentTime = 0.5f;
			component.isSway = true;
			key = jellyFook[Random.Range(0, jellyFook.Count)];
			component.text.fontSize = Random.Range(0.15f, 0.2f);
			component.text.color = jerryColor;
			break;
		case "CoralSteam":
			component.currentTime = 2f;
			component.isSway = true;
			key = coralSteam[Random.Range(0, coralSteam.Count)];
			component.text.fontSize = Random.Range(0.3f, 0.35f);
			component.text.color = coralColor;
			break;
		case "SuckFit":
			component.currentTime = 1f;
			component.isBomb = true;
			key = suckFit[Random.Range(0, suckFit.Count)];
			component.text.fontSize = Random.Range(0.25f, 0.3f);
			break;
		case "SuckTits":
			component.currentTime = 1f;
			component.isSway = true;
			key = suckTits[Random.Range(0, suckTits.Count)];
			component.text.fontSize = Random.Range(0.25f, 0.3f);
			break;
		case "SuckMilk":
			component.currentTime = 1f;
			component.isBomb = true;
			key = suckMilk[Random.Range(0, suckMilk.Count)];
			component.text.fontSize = Random.Range(0.25f, 0.3f);
			break;
		case "MilkDrip":
			component.currentTime = 1f;
			component.isFloat = true;
			key = milkDrip[Random.Range(0, milkDrip.Count)];
			component.text.fontSize = Random.Range(0.25f, 0.3f);
			break;
		case "MilkTank":
			component.currentTime = 1f;
			component.isFloat = true;
			key = milkTank[Random.Range(0, milkTank.Count)];
			component.text.fontSize = Random.Range(0.25f, 0.3f);
			break;
		case "WetSteam":
			component.currentTime = 2f;
			component.isFloat = true;
			key = wetSteam[Random.Range(0, wetSteam.Count)];
			component.text.fontSize = Random.Range(0.25f, 0.3f);
			component.floatSpeed = Random.Range(0.025f, 0.05f);
			break;
		case "Dloplets":
			component.currentTime = 1f;
			component.isFloat = true;
			key = dloplets[Random.Range(0, dloplets.Count)];
			component.text.fontSize = Random.Range(0.25f, 0.3f);
			break;
		case "Splash":
			component.currentTime = 1f;
			component.isSway = true;
			key = splash[Random.Range(0, splash.Count)];
			component.text.fontSize = Random.Range(0.3f, 0.35f);
			break;
		case "Pee":
			component.currentTime = 3f;
			component.isSway = true;
			key = pee[Random.Range(0, pee.Count)];
			component.text.fontSize = Random.Range(0.3f, 0.35f);
			component.text.color = peeColor;
			break;
		case "LostVirsin":
			component.currentTime = 2f;
			component.isBomb = true;
			key = lostVirsin[Random.Range(0, lostVirsin.Count)];
			component.text.fontSize = Random.Range(0.4f, 0.45f);
			component.text.color = bloodColor;
			break;
		case "Blood":
			component.currentTime = 1f;
			component.isFloat = true;
			key = blood[Random.Range(0, blood.Count)];
			component.text.fontSize = Random.Range(0.25f, 0.3f);
			component.text.color = bloodColor;
			break;
		case "Globs":
			component.currentTime = 1f;
			component.isFloat = true;
			key = globs[Random.Range(0, globs.Count)];
			component.text.fontSize = Random.Range(0.25f, 0.3f);
			component.text.color = pistonColor;
			break;
		case "Shot":
			component.currentTime = 2f;
			component.isBomb = true;
			key = shot[Random.Range(0, shot.Count)];
			component.text.fontSize = Random.Range(0.4f, 0.45f);
			component.text.color = shotColor;
			break;
		case "CreamPie":
			component.currentTime = 2f;
			component.isSway = true;
			key = creamPie[Random.Range(0, creamPie.Count)];
			component.text.fontSize = Random.Range(0.3f, 0.35f);
			component.text.color = creamPieColor;
			break;
		case "Drip":
			component.currentTime = 1f;
			component.isFloat = true;
			key = creamPie[Random.Range(0, creamPie.Count)];
			component.text.fontSize = Random.Range(0.25f, 0.3f);
			component.text.color = vaginaColor;
			break;
		case "TitleDrip":
			component.currentTime = 1f;
			component.isFloat = true;
			key = drip[Random.Range(0, drip.Count)];
			component.text.fontSize = Random.Range(0.3f, 0.35f);
			break;
		case "WaterDrop":
			component.currentTime = 1f;
			component.isFloat = true;
			key = waterDrop[Random.Range(0, waterDrop.Count)];
			component.text.fontSize = Random.Range(0.25f, 0.3f);
			break;
		case "PeeDrop":
			component.currentTime = 1f;
			component.isFloat = true;
			key = waterDrop[Random.Range(0, waterDrop.Count)];
			component.text.fontSize = Random.Range(0.25f, 0.3f);
			component.text.color = peeColor;
			break;
		case "Breath":
			component.currentTime = 1f;
			component.isFloat = true;
			key = breath[Random.Range(0, breath.Count)];
			component.text.fontSize = Random.Range(0.25f, 0.3f);
			break;
		case "BreathHard":
			component.currentTime = 1f;
			component.isFloat = true;
			key = breathHard[Random.Range(0, breathHard.Count)];
			component.text.fontSize = Random.Range(0.25f, 0.3f);
			break;
		case "Dizzy":
			component.currentTime = 1f;
			component.isSway = true;
			key = dizzy[Random.Range(0, dizzy.Count)];
			component.text.fontSize = Random.Range(0.25f, 0.3f);
			component.text.color = dizzyColor;
			break;
		case "Heart":
			component.currentTime = 1f;
			component.isSway = true;
			key = heart[Random.Range(0, heart.Count)];
			component.text.fontSize = Random.Range(0.25f, 0.3f);
			component.text.color = heartColor;
			break;
		case "OverHeat":
			component.currentTime = 1f;
			component.isSway = true;
			key = overHeat[Random.Range(0, overHeat.Count)];
			component.text.fontSize = Random.Range(0.25f, 0.3f);
			component.text.color = overHeatColor;
			break;
		case "VaginaOpen":
			component.currentTime = 1f;
			component.isBomb = true;
			key = vaginaOpen[Random.Range(0, vaginaOpen.Count)];
			component.text.fontSize = Random.Range(0.25f, 0.3f);
			break;
		case "ChildSpawn":
			component.currentTime = 1f;
			component.isBomb = true;
			key = childSpawn[Random.Range(0, childSpawn.Count)];
			component.text.fontSize = Random.Range(0.3f, 0.35f);
			component.text.color = childColor;
			break;
		case "ChildDrop":
			component.currentTime = 1f;
			component.isFloat = true;
			key = childDrop[Random.Range(0, childDrop.Count)];
			component.text.fontSize = Random.Range(0.25f, 0.3f);
			component.text.color = childColor;
			break;
		case "ChildDig":
			component.currentTime = 1f;
			component.isSway = true;
			key = childDig[Random.Range(0, childDig.Count)];
			component.text.fontSize = Random.Range(0.25f, 0.3f);
			break;
		case "ChildGlow":
			component.currentTime = 1f;
			component.isSway = true;
			key = childGlow[Random.Range(0, childGlow.Count)];
			component.text.fontSize = Random.Range(1f, 1.5f);
			break;
		case "SelectionGlobs":
			component.currentTime = 1f;
			component.isFloat = true;
			key = globs[Random.Range(0, globs.Count)];
			component.text.fontSize = Random.Range(1f, 1.5f);
			component.text.color = pistonColor;
			break;
		case "SelectionShot":
			component.currentTime = 2f;
			component.isBomb = true;
			key = shot[Random.Range(0, shot.Count)];
			component.text.fontSize = Random.Range(2f, 2.5f);
			component.text.color = shotColor;
			break;
		case "SelectionDrip":
			component.currentTime = 1f;
			component.isFloat = true;
			key = drip[Random.Range(0, drip.Count)];
			component.text.fontSize = Random.Range(1.5f, 2f);
			break;
		case "SelectionCreamPie":
			component.currentTime = 2f;
			component.isFloat = true;
			key = creamPie[Random.Range(0, creamPie.Count)];
			component.text.fontSize = Random.Range(2f, 2.5f);
			break;
		case "EaterOpen":
			component.currentTime = 1f;
			component.isBomb = true;
			key = eaterOpen[Random.Range(0, eaterOpen.Count)];
			component.text.fontSize = Random.Range(0.4f, 0.45f);
			component.text.color = eaterColor;
			break;
		case "EaterEating":
			component.currentTime = 1f;
			component.isBomb = true;
			key = eaterEating[Random.Range(0, eaterEating.Count)];
			component.text.fontSize = Random.Range(0.3f, 0.35f);
			component.text.color = eaterColor;
			break;
		case "HorseRide":
			component.currentTime = 1.5f;
			component.isSway = true;
			key = horseRide[Random.Range(0, horseRide.Count)];
			component.text.fontSize = Random.Range(0.3f, 0.35f);
			component.text.color = gimmickColor;
			break;
		case "WartBed":
			component.currentTime = 1.5f;
			component.isSway = true;
			key = wartBed[Random.Range(0, wartBed.Count)];
			component.text.fontSize = Random.Range(0.3f, 0.35f);
			component.text.color = gimmickColor;
			break;
		case "WartBedOpen":
			component.currentTime = 1.5f;
			component.isSway = true;
			key = wartBedOpen[Random.Range(0, wartBedOpen.Count)];
			component.text.fontSize = Random.Range(0.3f, 0.35f);
			component.text.color = gimmickColor;
			break;
		default:
			Debug.LogError("Onomatopoeia Type Error: " + type);
			break;
		}
		component.key = key;
	}
}
