using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
	public static ActionManager instance;

	public ActionDataBase _actionDataBase;

	[Header("Pair Type")]
	public string pairType;

	public bool reverse;

	public string handATK;

	public string handDEF;

	public string characterName;

	public int characterType;

	public int clotheType;

	public List<CharacterAnimancerManager> _characterAnimancerManager;

	public List<CharacterFaceManager> _characterFaceManager;

	public List<CharacterModelManager> _characterModelManager;

	public List<CharacterEffectManager> _characterEffectManager;

	public List<CharacterVagina> _characterVagina;

	[Header("Script")]
	public PlayerBlinkEyes _playerBlinkEyes;

	[Header("Card")]
	[Header("Libido Num")]
	public int setLibido = 5;

	[Header("Paramater")]
	public bool isLibido;

	public bool isLibidoCard;

	public bool isDamage;

	public bool isDamagePlus;

	public int libido;

	public float damagePlayer;

	public float damagePlayerPlus;

	public float damageEnemy;

	public float damageEnemyPlus;

	public float pillow;

	[Header("Card Data")]
	public AnimationClip atkClip;

	public AnimationClip defClip;

	public AnimationClip atkRClip;

	public AnimationClip defRClip;

	[Space]
	public AnimationClip atkIdleClip;

	public AnimationClip defIdleClip;

	public AnimationClip atkRIdleClip;

	public AnimationClip defRIdleClip;

	[Header("Face Animancer")]
	public bool useTongue;

	public bool unLookHeadAtk;

	public bool unLookEyesAtk;

	public bool unLookHeadDef;

	public bool unLookEyesDef;

	public string idleFaceAtk;

	public string clickFaceAtk;

	public string idleFaceDef;

	public string clickFaceDef;

	[Header("Hand IK Data")]
	public HandIKObject handLTarget;

	public HandIKObject handRTarget;

	public int handNum;

	public bool setHandIK;

	public bool useHandR;

	public string atkHand;

	public string defHand;

	public string atkHandR;

	public string defHandR;

	[Header("Clock")]
	public bool timeOver;

	public bool timeOverTalk;

	[Header("Animation Param")]
	public bool animClick;

	public void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
	}

	public void SetHandATK(string type, bool touch, int num)
	{
		if (pairType == "StellaVacua")
		{
			if (!reverse)
			{
				handATK = "Stella";
				handDEF = "Vacua";
			}
			else
			{
				handATK = "Vacua";
				handDEF = "Stella";
			}
		}
		if (pairType == "StellaNuisance")
		{
			if (!reverse)
			{
				handATK = "Stella";
				handDEF = "Nuisance";
			}
			else
			{
				handATK = "Nuisance";
				handDEF = "Stella";
			}
		}
		switch (type)
		{
		case "InTitsBoth":
			InTitsTouch(handATK, handDEF, touch, num);
			break;
		case "InTitsLeft":
			InTitsTouchL(handATK, handDEF, touch, num);
			break;
		case "InTitsRight":
			InTitsTouchR(handATK, handDEF, touch, num);
			break;
		case "OutTitsBoth":
			OutTitsTouch(handATK, handDEF, touch, num);
			break;
		case "OutTitsLeft":
			OutTitsTouchL(handATK, handDEF, touch, num);
			break;
		case "OutTitsRight":
			OutTitsTouchR(handATK, handDEF, touch, num);
			break;
		case "SideTitsBoth":
			SideTitsTouch(handATK, handDEF, touch, num);
			break;
		case "SideTitsLeft":
			SideTitsTouchL(handATK, handDEF, touch, num);
			break;
		case "SideTitsRight":
			SideTitsTouchR(handATK, handDEF, touch, num);
			break;
		case "SelfSideTitsBoth":
			SideTitsTouch(handATK, handATK, touch, num);
			break;
		case "SelfSideTitsLeft":
			SideTitsTouchL(handATK, handATK, touch, num);
			break;
		case "SelfSideTitsRight":
			SideTitsTouchR(handATK, handATK, touch, num);
			break;
		case "SelfTitsBoth":
			InTitsTouch(handATK, handATK, touch, num);
			break;
		case "SelfTitsLeft":
			InTitsTouchL(handATK, handATK, touch, num);
			break;
		case "SelfTitsRight":
			InTitsTouchR(handATK, handATK, touch, num);
			break;
		case "PalmBoth":
			PalmTouch(handATK, handDEF, touch);
			break;
		case "PalmLeft":
			PalmTouchL(handATK, handDEF, touch);
			break;
		case "PalmRight":
			PalmTouchR(handATK, handDEF, touch);
			break;
		}
	}

	public void SetHandDEF(string type, bool touch, int num)
	{
		if (pairType == "StellaVacua")
		{
			if (!reverse)
			{
				handATK = "Stella";
				handDEF = "Vacua";
			}
			else
			{
				handATK = "Vacua";
				handDEF = "Stella";
			}
		}
		if (pairType == "StellaNuisance")
		{
			if (!reverse)
			{
				handATK = "Stella";
				handDEF = "Nuisance";
			}
			else
			{
				handATK = "Nuisance";
				handDEF = "Stella";
			}
		}
		switch (type)
		{
		case "InTitsBoth":
			InTitsTouch(handDEF, handATK, touch, num);
			break;
		case "InTitsLeft":
			InTitsTouchL(handDEF, handATK, touch, num);
			break;
		case "InTitsRight":
			InTitsTouchR(handDEF, handATK, touch, num);
			break;
		case "OutTitsBoth":
			OutTitsTouch(handDEF, handATK, touch, num);
			break;
		case "OutTitsLeft":
			OutTitsTouchL(handDEF, handATK, touch, num);
			break;
		case "OutTitsRight":
			OutTitsTouchR(handDEF, handATK, touch, num);
			break;
		case "SideTitsBoth":
			SideTitsTouch(handDEF, handATK, touch, num);
			break;
		case "SideTitsLeft":
			SideTitsTouchL(handDEF, handATK, touch, num);
			break;
		case "SideTitsRight":
			SideTitsTouchR(handDEF, handATK, touch, num);
			break;
		case "SelfSideTitsBoth":
			SideTitsTouch(handDEF, handDEF, touch, num);
			break;
		case "SelfSideTitsLeft":
			SideTitsTouchL(handDEF, handDEF, touch, num);
			break;
		case "SelfSideTitsRight":
			SideTitsTouchR(handDEF, handDEF, touch, num);
			break;
		case "SelfTitsBoth":
			InTitsTouch(handDEF, handDEF, touch, num);
			break;
		case "SelfTitsLeft":
			InTitsTouchL(handDEF, handDEF, touch, num);
			break;
		case "SelfTitsRight":
			InTitsTouchR(handDEF, handDEF, touch, num);
			break;
		case "PalmBoth":
			PalmTouch(handDEF, handATK, touch);
			break;
		case "PalmLeft":
			PalmTouchL(handDEF, handATK, touch);
			break;
		case "PalmRight":
			PalmTouchR(handDEF, handATK, touch);
			break;
		}
	}

	public void ClearHand()
	{
		if (pairType == "StellaVacua")
		{
			_characterAnimancerManager[0].ClearHand();
			_characterAnimancerManager[1].ClearHand();
		}
		if (pairType == "StellaNuisance")
		{
			_characterAnimancerManager[0].ClearHand();
			_characterAnimancerManager[2].ClearHand();
		}
	}

	public void SetTitsSe(string atk)
	{
		switch (atk)
		{
		case "Stella":
			_characterAnimancerManager[0].SetTitsSe();
			break;
		case "Vacua":
			_characterAnimancerManager[1].SetTitsSe();
			break;
		case "Nuisance":
			_characterAnimancerManager[2].SetTitsSe();
			break;
		}
	}

	public void OutTitsTouch(string atk, string def, bool touch, int num)
	{
		Debug.LogWarning("Tits Touch Both", base.gameObject);
		OutTitsTouchL(atk, def, touch, num);
		OutTitsTouchR(atk, def, touch, num);
	}

	public void OutTitsTouchL(string atk, string def, bool touch, int num)
	{
		Debug.LogWarning("Tits Touch L :" + atk + " to " + def, base.gameObject);
		switch (def)
		{
		case "Stella":
			handLTarget = _characterAnimancerManager[0].handOutTitsL;
			break;
		case "Vacua":
			handLTarget = _characterAnimancerManager[1].handOutTitsL;
			break;
		case "Nuisance":
			handLTarget = _characterAnimancerManager[2].handOutTitsL;
			break;
		}
		switch (atk)
		{
		case "Stella":
			_characterAnimancerManager[0].SetHandL(handLTarget, touch, "OutTits", num);
			break;
		case "Vacua":
			_characterAnimancerManager[1].SetHandL(handLTarget, touch, "OutTits", num);
			break;
		case "Nuisance":
			_characterAnimancerManager[2].SetHandL(handLTarget, touch, "OutTits", num);
			break;
		}
	}

	public void OutTitsTouchR(string atk, string def, bool touch, int num)
	{
		Debug.LogWarning("Tits Touch R :" + atk + " to " + def, base.gameObject);
		switch (def)
		{
		case "Stella":
			handRTarget = _characterAnimancerManager[0].handOutTitsR;
			break;
		case "Vacua":
			handRTarget = _characterAnimancerManager[1].handOutTitsR;
			break;
		case "Nuisance":
			handRTarget = _characterAnimancerManager[2].handOutTitsR;
			break;
		}
		switch (atk)
		{
		case "Stella":
			_characterAnimancerManager[0].SetHandR(handRTarget, touch, "OutTits", num);
			break;
		case "Vacua":
			_characterAnimancerManager[1].SetHandR(handRTarget, touch, "OutTits", num);
			break;
		case "Nuisance":
			_characterAnimancerManager[2].SetHandR(handRTarget, touch, "OutTits", num);
			break;
		}
	}

	public void InTitsTouch(string atk, string def, bool touch, int num)
	{
		Debug.LogWarning("Tits Touch Both :" + atk + " to " + def, base.gameObject);
		InTitsTouchL(atk, def, touch, num);
		InTitsTouchR(atk, def, touch, num);
	}

	public void InTitsTouchL(string atk, string def, bool touch, int num)
	{
		Debug.LogWarning("Tits Touch L :" + atk + " to " + def, base.gameObject);
		switch (def)
		{
		case "Stella":
			handLTarget = _characterAnimancerManager[0].handInTitsL;
			break;
		case "Vacua":
			handLTarget = _characterAnimancerManager[1].handInTitsL;
			break;
		case "Nuisance":
			handLTarget = _characterAnimancerManager[2].handInTitsL;
			break;
		}
		switch (atk)
		{
		case "Stella":
			_characterAnimancerManager[0].SetHandL(handLTarget, touch, "InTits", num);
			break;
		case "Vacua":
			_characterAnimancerManager[1].SetHandL(handLTarget, touch, "InTits", num);
			break;
		case "Nuisance":
			_characterAnimancerManager[2].SetHandL(handLTarget, touch, "InTits", num);
			break;
		}
	}

	public void InTitsTouchR(string atk, string def, bool touch, int num)
	{
		Debug.LogWarning("Tits Touch R :" + atk + " to " + def, base.gameObject);
		switch (def)
		{
		case "Stella":
			handRTarget = _characterAnimancerManager[0].handInTitsR;
			break;
		case "Vacua":
			handRTarget = _characterAnimancerManager[1].handInTitsR;
			break;
		case "Nuisance":
			handRTarget = _characterAnimancerManager[2].handInTitsR;
			break;
		}
		switch (atk)
		{
		case "Stella":
			_characterAnimancerManager[0].SetHandR(handRTarget, touch, "InTits", num);
			break;
		case "Vacua":
			_characterAnimancerManager[1].SetHandR(handRTarget, touch, "InTits", num);
			break;
		case "Nuisance":
			_characterAnimancerManager[2].SetHandR(handRTarget, touch, "InTits", num);
			break;
		}
	}

	public void SideTitsTouch(string atk, string def, bool touch, int num)
	{
		Debug.LogWarning("Tits Touch Both :" + atk + " to " + def, base.gameObject);
		SideTitsTouchL(atk, def, touch, num);
		SideTitsTouchR(atk, def, touch, num);
	}

	public void SideTitsTouchL(string atk, string def, bool touch, int num)
	{
		Debug.LogWarning("Tits Touch L :" + atk + " to " + def, base.gameObject);
		switch (def)
		{
		case "Stella":
			handLTarget = _characterAnimancerManager[0].handSideTitsL;
			break;
		case "Vacua":
			handLTarget = _characterAnimancerManager[1].handSideTitsL;
			break;
		case "Nuisance":
			handLTarget = _characterAnimancerManager[2].handSideTitsL;
			break;
		}
		switch (atk)
		{
		case "Stella":
			_characterAnimancerManager[0].SetHandL(handLTarget, touch, "SideTits", num);
			break;
		case "Vacua":
			_characterAnimancerManager[1].SetHandL(handLTarget, touch, "SideTits", num);
			break;
		case "Nuisance":
			_characterAnimancerManager[2].SetHandL(handLTarget, touch, "SideTits", num);
			break;
		}
	}

	public void SideTitsTouchR(string atk, string def, bool touch, int num)
	{
		Debug.LogWarning("Tits Touch R :" + atk + " to " + def, base.gameObject);
		switch (def)
		{
		case "Stella":
			handRTarget = _characterAnimancerManager[0].handSideTitsR;
			break;
		case "Vacua":
			handRTarget = _characterAnimancerManager[1].handSideTitsR;
			break;
		case "Nuisance":
			handRTarget = _characterAnimancerManager[2].handSideTitsR;
			break;
		}
		switch (atk)
		{
		case "Stella":
			_characterAnimancerManager[0].SetHandR(handRTarget, touch, "SideTits", num);
			break;
		case "Vacua":
			_characterAnimancerManager[1].SetHandR(handRTarget, touch, "SideTits", num);
			break;
		case "Nuisance":
			_characterAnimancerManager[2].SetHandR(handRTarget, touch, "SideTits", num);
			break;
		}
	}

	public void PalmTouch(string atk, string def, bool touch)
	{
		Debug.LogWarning("Hand Position Fixed Both :" + atk + " to " + def, base.gameObject);
		PalmTouchL(atk, def, touch);
		PalmTouchR(atk, def, touch);
	}

	public void PalmTouchL(string atk, string def, bool touch)
	{
		Debug.LogWarning("Hand Position Fixed  L :" + atk + " to " + def, base.gameObject);
		switch (def)
		{
		case "Stella":
			handLTarget = _characterAnimancerManager[0].handPalmL;
			break;
		case "Vacua":
			handLTarget = _characterAnimancerManager[1].handPalmL;
			break;
		case "Nuisance":
			handLTarget = _characterAnimancerManager[2].handPalmL;
			break;
		}
		switch (atk)
		{
		case "Stella":
			_characterAnimancerManager[0].SetHandL(handLTarget, touch, "Palm", 0);
			break;
		case "Vacua":
			_characterAnimancerManager[1].SetHandL(handLTarget, touch, "Palm", 0);
			break;
		case "Nuisance":
			_characterAnimancerManager[2].SetHandL(handLTarget, touch, "Palm", 0);
			break;
		}
	}

	public void PalmTouchR(string atk, string def, bool touch)
	{
		Debug.LogWarning("Hand Position Fixed  R :" + atk + " to " + def, base.gameObject);
		switch (def)
		{
		case "Stella":
			handRTarget = _characterAnimancerManager[0].handPalmR;
			break;
		case "Vacua":
			handRTarget = _characterAnimancerManager[1].handPalmR;
			break;
		case "Nuisance":
			handRTarget = _characterAnimancerManager[2].handPalmR;
			break;
		}
		switch (atk)
		{
		case "Stella":
			_characterAnimancerManager[0].SetHandR(handRTarget, touch, "Palm", 0);
			break;
		case "Vacua":
			_characterAnimancerManager[1].SetHandR(handRTarget, touch, "Palm", 0);
			break;
		case "Nuisance":
			_characterAnimancerManager[2].SetHandR(handRTarget, touch, "Palm", 0);
			break;
		}
	}

	public void SetEffect(string character, string value)
	{
		characterName = character;
		Debug.LogWarning("SetEffect: " + character + "  " + value, base.gameObject);
		if (characterName == "Stella")
		{
			characterType = 0;
		}
		else if (characterName == "Vacua")
		{
			characterType = 1;
		}
		else if (characterName == "Nuisance")
		{
			characterType = 2;
		}
		switch (value)
		{
		case "Orgasm":
			_characterVagina[characterType].OrgasmSplash();
			break;
		case "Splash":
			_characterVagina[characterType].VaginaSplash();
			break;
		case "Pee":
			_characterVagina[characterType].PeeSplash();
			break;
		}
	}
}
