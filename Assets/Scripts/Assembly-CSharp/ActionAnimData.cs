using System.Collections.Generic;
using UnityEngine;

public class ActionAnimData : MonoBehaviour
{
	[Header("Target Data")]
	public string characterName;

	public string actionName;

	public AnimationClip targetClip;

	public AnimationClip targetIdleClip;

	[Header("Idle Data")]
	public AnimationClip stellaIdleClip;

	public AnimationClip vacuaIdleClip;

	public AnimationClip nuisanceIdleClip;

	[Header("Action Data")]
	public List<AnimationClip> firstAction;

	public List<AnimationClip> secondAction;

	public List<AnimationClip> thirdAction;

	public List<AnimationClip> fourthAction;

	public List<AnimationClip> endAction;

	[Header("Bed Action Data")]
	public List<AnimationClip> firstBedL;

	public List<AnimationClip> firstBedR;

	public List<AnimationClip> secondBedC;

	public List<AnimationClip> secondBedBottom;

	public List<AnimationClip> thirdBedF;

	public List<AnimationClip> thirdBedB;

	public List<AnimationClip> fourthBedF;

	public List<AnimationClip> fourthBedB;

	public List<AnimationClip> endBedF;

	public List<AnimationClip> endBedB;

	[Header("Look Data")]
	public int bedLookCurrent;

	public int bedLookBefore = -1;

	public List<AnimationClip> bedLookOut;

	public List<AnimationClip> bedLookIn;

	public List<AnimationClip> bedLookDo;

	[Header("Tits Data")]
	public AnimationClip titsUpperHnadClip;

	public AnimationClip titsUpperHnadForwardThrustClip;

	public AnimationClip titsSwayHorizontalClip;

	public AnimationClip titsSwayVerticalClip;

	[Header("PushDown Data")]
	public List<AnimationClip> pushDownATK;

	public List<AnimationClip> pushDownDef;

	public List<AnimationClip> pushDownIdleATK;

	public List<AnimationClip> pushDownIdleDef;

	private void Start()
	{
		bedLookBefore = -1;
	}

	public void AnimationSet(string character, string action)
	{
		Debug.Log("Animation Set :" + character + " Action: " + action);
		characterName = character;
		actionName = action;
		targetClip = null;
		targetIdleClip = null;
		if (characterName == "Stella" && actionName == "Idle")
		{
			targetClip = stellaIdleClip;
		}
		else if (characterName == "Vacua" && actionName == "Idle")
		{
			targetClip = vacuaIdleClip;
		}
		else if (characterName == "Nuisance" && actionName == "Idle")
		{
			targetClip = nuisanceIdleClip;
		}
		else if (characterName == "Nuisance" && actionName == "BedLook Out")
		{
			if (bedLookOut.Count != 1)
			{
				while (bedLookCurrent == bedLookBefore)
				{
					bedLookCurrent = Random.Range(0, bedLookOut.Count);
				}
				bedLookBefore = bedLookCurrent;
			}
			else
			{
				bedLookCurrent = 0;
			}
			targetClip = bedLookOut[bedLookCurrent];
		}
		else if (characterName == "Nuisance" && actionName == "BedLook In")
		{
			if (bedLookIn.Count != 1)
			{
				while (bedLookCurrent == bedLookBefore)
				{
					bedLookCurrent = Random.Range(0, bedLookIn.Count);
				}
				bedLookBefore = bedLookCurrent;
			}
			else
			{
				bedLookCurrent = 0;
			}
			targetClip = bedLookIn[bedLookCurrent];
		}
		else if (characterName == "Nuisance" && actionName == "BedLook Do")
		{
			if (bedLookDo.Count != 1)
			{
				while (bedLookCurrent == bedLookBefore)
				{
					bedLookCurrent = Random.Range(0, bedLookDo.Count);
				}
				bedLookBefore = bedLookCurrent;
			}
			else
			{
				bedLookCurrent = 0;
			}
			targetClip = bedLookDo[bedLookCurrent];
		}
		else if (characterName == "Stella" && actionName == "PushDown")
		{
			targetClip = pushDownATK[2];
			targetIdleClip = pushDownIdleATK[2];
		}
		else if (characterName == "Vacua" && actionName == "PushDown")
		{
			targetClip = pushDownDef[0];
			targetIdleClip = pushDownIdleDef[0];
		}
		else if (characterName == "Stella" && actionName == "PushDownIdle")
		{
			targetClip = pushDownIdleATK[0];
		}
		else if (characterName == "Vacua" && actionName == "PushDownIdle")
		{
			targetClip = pushDownIdleDef[0];
		}
		else if (characterName == "Stella" && actionName == "PushDown1")
		{
			targetClip = pushDownATK[0];
			targetIdleClip = pushDownIdleATK[0];
		}
		else if (characterName == "Vacua" && actionName == "PushDown1")
		{
			targetClip = pushDownDef[0];
			targetIdleClip = pushDownIdleDef[0];
		}
		else if (characterName == "Stella" && actionName == "PushDownIdle1")
		{
			targetClip = pushDownIdleATK[0];
		}
		else if (characterName == "Vacua" && actionName == "PushDownIdle1")
		{
			targetClip = pushDownIdleDef[0];
		}
		else if (characterName == "Stella" && actionName == "PushDown2")
		{
			targetClip = pushDownATK[1];
			targetIdleClip = pushDownIdleATK[1];
		}
		else if (characterName == "Vacua" && actionName == "PushDown2")
		{
			targetClip = pushDownDef[1];
			targetIdleClip = pushDownIdleDef[1];
		}
		else if (characterName == "Stella" && actionName == "PushDownIdle2")
		{
			targetClip = pushDownIdleATK[1];
		}
		else if (characterName == "Vacua" && actionName == "PushDownIdle2")
		{
			targetClip = pushDownIdleDef[1];
		}
		else if (characterName == "Stella" && actionName == "PushDown3")
		{
			targetClip = pushDownATK[2];
			targetIdleClip = pushDownIdleATK[2];
		}
		else if (characterName == "Vacua" && actionName == "PushDown3")
		{
			targetClip = pushDownDef[2];
			targetIdleClip = pushDownIdleDef[2];
		}
		else if (characterName == "Stella" && actionName == "PushDownIdle3")
		{
			targetClip = pushDownIdleATK[2];
		}
		else if (characterName == "Vacua" && actionName == "PushDownIdle3")
		{
			targetClip = pushDownIdleDef[2];
		}
		else if (characterName == "Nuisance" && actionName == "SwayHorizontal")
		{
			targetClip = titsSwayHorizontalClip;
		}
		else if (characterName == "Nuisance" && actionName == "SwayVertical")
		{
			targetClip = titsSwayVerticalClip;
		}
		else if (characterName == "Nuisance" && actionName == "TitsUpperHnad")
		{
			targetClip = titsUpperHnadClip;
		}
		else if (characterName == "Nuisance" && actionName == "TitsUpperHnad2")
		{
			targetClip = titsUpperHnadForwardThrustClip;
		}
		else if (actionName == "FirstAction0")
		{
			targetClip = firstAction[0];
			targetIdleClip = stellaIdleClip;
		}
		else if (actionName == "FirstAction1")
		{
			targetClip = firstAction[1];
			targetIdleClip = vacuaIdleClip;
		}
		else if (actionName == "FirstAction2")
		{
			targetClip = firstAction[2];
			targetIdleClip = vacuaIdleClip;
		}
		else if (actionName == "FirstAction3")
		{
			targetClip = firstAction[3];
			targetIdleClip = stellaIdleClip;
		}
		else if (actionName == "SecondAction0")
		{
			targetClip = secondAction[0];
			targetIdleClip = stellaIdleClip;
		}
		else if (actionName == "SecondAction1")
		{
			targetClip = secondAction[1];
			targetIdleClip = nuisanceIdleClip;
		}
		else if (actionName == "SecondAction2")
		{
			targetClip = secondAction[2];
			targetIdleClip = stellaIdleClip;
		}
		else if (actionName == "SecondAction3")
		{
			targetClip = secondAction[3];
			targetIdleClip = nuisanceIdleClip;
		}
		else if (actionName == "ThirdAction0")
		{
			targetClip = thirdAction[0];
			targetIdleClip = vacuaIdleClip;
		}
		else if (actionName == "ThirdAction1")
		{
			targetClip = thirdAction[1];
			targetIdleClip = vacuaIdleClip;
		}
		else if (actionName == "ThirdAction2")
		{
			targetClip = thirdAction[2];
			targetIdleClip = stellaIdleClip;
		}
		else if (actionName == "ThirdAction3")
		{
			targetClip = thirdAction[3];
			targetIdleClip = nuisanceIdleClip;
		}
		else if (actionName == "FourthAction0")
		{
			targetClip = fourthAction[0];
			targetIdleClip = stellaIdleClip;
		}
		else if (actionName == "FourthAction1")
		{
			targetClip = fourthAction[1];
			targetIdleClip = nuisanceIdleClip;
		}
		else if (actionName == "FourthAction2")
		{
			targetClip = fourthAction[2];
			targetIdleClip = stellaIdleClip;
		}
		else if (actionName == "FourthAction3")
		{
			targetClip = fourthAction[3];
			targetIdleClip = nuisanceIdleClip;
		}
		else if (actionName == "FourthAction4")
		{
			targetClip = fourthAction[4];
			targetIdleClip = nuisanceIdleClip;
		}
		else if (actionName == "EndAction0")
		{
			targetClip = endAction[0];
			targetIdleClip = stellaIdleClip;
		}
		else if (actionName == "EndAction1")
		{
			targetClip = endAction[1];
			targetIdleClip = nuisanceIdleClip;
		}
		else if (actionName == "EndAction2")
		{
			targetClip = endAction[2];
			targetIdleClip = stellaIdleClip;
		}
		else if (actionName == "EndAction3")
		{
			targetClip = endAction[3];
			targetIdleClip = nuisanceIdleClip;
		}
		else if (actionName == "EndAction4")
		{
			targetClip = endAction[4];
			targetIdleClip = stellaIdleClip;
		}
		else if (actionName == "EndAction5")
		{
			targetClip = endAction[5];
			targetIdleClip = nuisanceIdleClip;
		}
		else if (actionName == "FirstBedL0")
		{
			targetClip = firstBedL[0];
		}
		else if (actionName == "FirstBedL1")
		{
			targetClip = firstBedL[1];
			targetIdleClip = firstBedL[2];
		}
		else if (actionName == "FirstBedL2")
		{
			targetClip = firstBedL[2];
		}
		else if (actionName == "FirstBedL3")
		{
			targetClip = firstBedL[3];
			targetIdleClip = firstBedL[0];
		}
		else if (actionName == "FirstBedL4")
		{
			targetClip = firstBedL[4];
			targetIdleClip = firstBedL[0];
		}
		else if (actionName == "FirstBedL5")
		{
			targetClip = firstBedL[5];
			targetIdleClip = firstBedL[0];
		}
		else if (actionName == "FirstBedR0")
		{
			targetClip = firstBedR[0];
		}
		else if (actionName == "FirstBedR1")
		{
			targetClip = firstBedR[1];
			targetIdleClip = firstBedR[2];
		}
		else if (actionName == "FirstBedR2")
		{
			targetClip = firstBedR[2];
		}
		else if (actionName == "FirstBedR3")
		{
			targetClip = firstBedR[3];
			targetIdleClip = firstBedR[0];
		}
		else if (actionName == "FirstBedR4")
		{
			targetClip = firstBedR[4];
			targetIdleClip = firstBedR[0];
		}
		else if (actionName == "SecondBedC0")
		{
			targetClip = secondBedC[0];
		}
		else if (actionName == "SecondBedC1")
		{
			targetClip = secondBedC[1];
			targetIdleClip = secondBedC[2];
		}
		else if (actionName == "SecondBedC2")
		{
			targetClip = secondBedC[2];
		}
		else if (actionName == "SecondBedC3")
		{
			targetClip = secondBedC[3];
			targetIdleClip = secondBedC[0];
		}
		else if (actionName == "SecondBedC4")
		{
			targetClip = secondBedC[4];
			targetIdleClip = secondBedC[0];
		}
		else if (actionName == "SecondBedBottom0")
		{
			targetClip = secondBedBottom[0];
		}
		else if (actionName == "SecondBedBottom1")
		{
			targetClip = secondBedBottom[1];
			targetIdleClip = secondBedBottom[2];
		}
		else if (actionName == "SecondBedBottom2")
		{
			targetClip = secondBedBottom[2];
		}
		else if (actionName == "SecondBedBottom3")
		{
			targetClip = secondBedBottom[3];
			targetIdleClip = secondBedBottom[0];
		}
		else if (actionName == "SecondBedBottom4")
		{
			targetClip = secondBedBottom[4];
			targetIdleClip = secondBedBottom[0];
		}
		else if (actionName == "ThirdBedF0")
		{
			targetClip = thirdBedF[0];
		}
		else if (actionName == "ThirdBedF1")
		{
			targetClip = thirdBedF[1];
			targetIdleClip = thirdBedF[2];
		}
		else if (actionName == "ThirdBedF2")
		{
			targetClip = thirdBedF[2];
		}
		else if (actionName == "ThirdBedF3")
		{
			targetClip = thirdBedF[3];
			targetIdleClip = thirdBedF[0];
		}
		else if (actionName == "ThirdBedF4")
		{
			targetClip = thirdBedF[4];
			targetIdleClip = thirdBedF[0];
		}
		else if (actionName == "ThirdBedB0")
		{
			targetClip = thirdBedB[0];
		}
		else if (actionName == "ThirdBedB1")
		{
			targetClip = thirdBedB[1];
			targetIdleClip = thirdBedB[2];
		}
		else if (actionName == "ThirdBedB2")
		{
			targetClip = thirdBedB[2];
		}
		else if (actionName == "ThirdBedB3")
		{
			targetClip = thirdBedB[3];
			targetIdleClip = thirdBedB[0];
		}
		else if (actionName == "ThirdBedB4")
		{
			targetClip = thirdBedB[4];
			targetIdleClip = thirdBedB[0];
		}
		else if (actionName == "FourthBedF0")
		{
			targetClip = fourthBedF[0];
		}
		else if (actionName == "FourthBedF1")
		{
			targetClip = fourthBedF[1];
			targetIdleClip = fourthBedF[2];
		}
		else if (actionName == "FourthBedF2")
		{
			targetClip = fourthBedF[2];
		}
		else if (actionName == "FourthBedF3")
		{
			targetClip = fourthBedF[3];
		}
		else if (actionName == "FourthBedF4")
		{
			targetClip = fourthBedF[4];
			targetIdleClip = fourthBedF[0];
		}
		else if (actionName == "FourthBedB0")
		{
			targetClip = fourthBedB[0];
		}
		else if (actionName == "FourthBedB1")
		{
			targetClip = fourthBedB[1];
			targetIdleClip = fourthBedB[2];
		}
		else if (actionName == "FourthBedB2")
		{
			targetClip = fourthBedB[2];
		}
		else if (actionName == "FourthBedB3")
		{
			targetClip = fourthBedB[3];
		}
		else if (actionName == "FourthBedB4")
		{
			targetClip = fourthBedB[4];
			targetIdleClip = fourthBedB[0];
		}
		else if (actionName == "EndBedF0")
		{
			targetClip = endBedF[0];
		}
		else if (actionName == "EndBedF1")
		{
			targetClip = endBedF[1];
			targetIdleClip = endBedF[2];
		}
		else if (actionName == "EndBedF2")
		{
			targetClip = endBedF[2];
		}
		else if (actionName == "EndBedF3")
		{
			targetClip = endBedF[3];
			targetIdleClip = endBedF[0];
		}
		else if (actionName == "EndBedF4")
		{
			targetClip = endBedF[4];
			targetIdleClip = endBedF[0];
		}
		else if (actionName == "EndBedB0")
		{
			targetClip = endBedB[0];
		}
		else if (actionName == "EndBedB1")
		{
			targetClip = endBedB[1];
			targetIdleClip = endBedB[2];
		}
		else if (actionName == "EndBedB2")
		{
			targetClip = endBedB[2];
		}
		else if (actionName == "EndBedB3")
		{
			targetClip = endBedB[3];
			targetIdleClip = endBedB[0];
		}
		else if (actionName == "EndBedB4")
		{
			targetClip = endBedB[4];
			targetIdleClip = endBedB[0];
		}
	}
}
