using Animancer;
using UnityEngine;

public class CharacterTongueManager : MonoBehaviour
{
	public AnimancerComponent _animancer;

	public CharacterFacialManager _characterFacialManager;

	[Header("Status")]
	public bool changeTongue;

	public bool isIdle;

	public bool isBreath;

	public bool isDoggy;

	public bool isLipUpperLick;

	public bool isLipLowerLick;

	public bool isSwayX;

	public bool isSwayY;

	[Header("Tongue Layer Number")]
	public int tongueNumberMotion = 5;

	public AnimancerLayer tongueMotionLayer;

	public AnimationClip setAnim;

	public AnimationClip setCloseAnim;

	public AnimationClip setOpenAnim;

	[Range(0f, 1f)]
	public float param;

	private AnimancerState _animancerState;

	[Header("Tongue Position")]
	[Range(0f, 1f)]
	public float tongueOut;

	[Range(-1f, 1f)]
	public float tongueX;

	[Range(-1f, 1f)]
	public float tongueY;

	public float tongueXcalc;

	public float tongueYcalc;

	public AnimationCurve curveX;

	public AnimationCurve curveY;

	public AnimationCurve curveOut;

	[Header("Mouth Open Adjust Tongue")]
	public float currentMouthOpen;

	public float defaultMouthOpen;

	public float paramCalc;

	public float diff;

	public float openDiff;

	public float openCalc;

	public float tongueAdjustCalc;

	public float tongueUnOpenCalc;

	public AnimationCurve curveOpenAnim;

	public AnimationCurve curveAdjustClose;

	public AnimationCurve curveAdjustOpen;

	public AnimationCurve curveUnOpenClose;

	public AnimationCurve curveUnOpenOpen;

	[Header("Mesh ShapeKey")]
	public SkinnedMeshRenderer headMesh;

	public int shapeKeyUp = 25;

	public int shapeKeyDown = 26;

	public int shapeKeyRight = 27;

	public int shapeKeyLeft = 28;

	[Space]
	public int shapeKeyOpen = 14;

	public int shapeKeyOut = 21;

	public int shapeKeyAdjust = 23;

	public int shapeKeyUnOpen = 22;

	[Header("Param Layer Number")]
	public int tongueNumberMove = 9;

	public AnimancerLayer tongueMoveLayer;

	public float feedTime = 0.25f;

	private AnimancerState _state;

	public AnimationClip idleAnim;

	public AnimationClip breathAnim;

	public AnimationClip doggyAnim;

	public AnimationClip lickUpperLipAnim;

	public AnimationClip lickLowerLipAnim;

	public AnimationClip swayXAnim;

	public AnimationClip swayYAnim;

	public void InitalizeMouth()
	{
		tongueMotionLayer = _animancer.Layers[tongueNumberMotion];
		tongueMotionLayer.IsAdditive = true;
		_animancerState = tongueMotionLayer.Play(setAnim);
		_animancerState.IsPlaying = false;
		tongueMoveLayer = _animancer.Layers[tongueNumberMove];
		tongueMoveLayer.IsAdditive = false;
		tongueMoveLayer.Play(idleAnim);
		param = 0f;
		tongueOut = 0f;
		tongueX = 0f;
		tongueY = 0f;
	}

	private void LateUpdate()
	{
		CheckBool();
		paramCalc = Mathf.Clamp(param + tongueOut, 0f, 1f);
		_animancerState.Time = paramCalc;
		tongueXcalc = Mathf.Clamp(tongueX, -1f, 1f) * 100f;
		tongueYcalc = Mathf.Clamp(tongueY, -1f, 1f) * 100f;
		tongueXcalc *= curveX.Evaluate(paramCalc);
		tongueYcalc *= curveY.Evaluate(paramCalc);
		if (tongueXcalc == 0f)
		{
			headMesh.SetBlendShapeWeight(shapeKeyLeft, 0f);
			headMesh.SetBlendShapeWeight(shapeKeyRight, 0f);
		}
		else if (tongueXcalc > 0f)
		{
			headMesh.SetBlendShapeWeight(shapeKeyLeft, 0f);
			headMesh.SetBlendShapeWeight(shapeKeyRight, tongueXcalc);
		}
		else if (tongueXcalc < 0f)
		{
			headMesh.SetBlendShapeWeight(shapeKeyLeft, 0f - tongueXcalc);
			headMesh.SetBlendShapeWeight(shapeKeyRight, 0f);
		}
		if (tongueYcalc == 0f)
		{
			headMesh.SetBlendShapeWeight(shapeKeyUp, 0f);
			headMesh.SetBlendShapeWeight(shapeKeyDown, 0f);
		}
		else if (tongueYcalc > 0f)
		{
			headMesh.SetBlendShapeWeight(shapeKeyUp, tongueYcalc);
			headMesh.SetBlendShapeWeight(shapeKeyDown, 0f);
		}
		else if (tongueYcalc < 0f)
		{
			headMesh.SetBlendShapeWeight(shapeKeyUp, 0f);
			headMesh.SetBlendShapeWeight(shapeKeyAdjust, (0f - tongueYcalc) / 2f);
		}
		if (paramCalc > 0f)
		{
			currentMouthOpen = headMesh.GetBlendShapeWeight(shapeKeyOpen);
			float time = paramCalc * 100f;
			defaultMouthOpen = curveOpenAnim.Evaluate(paramCalc);
			diff = currentMouthOpen - defaultMouthOpen;
			openDiff = 100f - defaultMouthOpen;
			if (openDiff <= 0f)
			{
				openDiff = 1f;
			}
			openCalc = diff / openDiff;
			headMesh.SetBlendShapeWeight(shapeKeyOut, curveOut.Evaluate(time));
			tongueAdjustCalc = Mathf.Lerp(curveAdjustClose.Evaluate(defaultMouthOpen), curveAdjustOpen.Evaluate(defaultMouthOpen), openCalc);
			headMesh.SetBlendShapeWeight(shapeKeyAdjust, tongueAdjustCalc);
			tongueUnOpenCalc = Mathf.Lerp(curveUnOpenClose.Evaluate(defaultMouthOpen), curveUnOpenOpen.Evaluate(defaultMouthOpen), openCalc);
			headMesh.SetBlendShapeWeight(shapeKeyUnOpen, tongueUnOpenCalc);
		}
	}

	public void ResetBool()
	{
		isIdle = false;
		isBreath = false;
		isDoggy = false;
		isLipUpperLick = false;
		isLipLowerLick = false;
		isSwayX = false;
		isSwayY = false;
		_characterFacialManager.ResetTongueBool(idle: false);
	}

	public void CheckBool()
	{
		if (!changeTongue)
		{
			return;
		}
		changeTongue = false;
		if (isIdle)
		{
			if (_state != null)
			{
				_state.Events(this).Clear();
			}
			_state = tongueMoveLayer.Play(idleAnim, feedTime);
			_state.Speed = 1f;
			ResetBool();
		}
		if (isBreath)
		{
			if (_state != null)
			{
				_state.Events(this).Clear();
			}
			_state = tongueMoveLayer.Play(breathAnim, feedTime);
			_state.Speed = 1f;
		}
		if (isDoggy)
		{
			if (_state != null)
			{
				_state.Events(this).Clear();
			}
			_state = tongueMoveLayer.Play(doggyAnim, feedTime);
			_state.Speed = 1f;
		}
		if (isLipUpperLick)
		{
			if (_state != null)
			{
				_state.Events(this).Clear();
			}
			_state = tongueMoveLayer.Play(lickUpperLipAnim, feedTime);
			_state.Speed = 1f;
			_state.Events(this).OnEnd = delegate
			{
				tongueMoveLayer.Play(idleAnim, feedTime);
				ResetBool();
			};
		}
		if (isLipLowerLick)
		{
			if (_state != null)
			{
				_state.Events(this).Clear();
			}
			_state = tongueMoveLayer.Play(lickLowerLipAnim, feedTime);
			_state.Speed = 1f;
			_state.Events(this).OnEnd = delegate
			{
				tongueMoveLayer.Play(idleAnim, feedTime);
				ResetBool();
			};
		}
		if (isSwayX)
		{
			if (_state != null)
			{
				_state.Events(this).Clear();
			}
			_state = tongueMoveLayer.Play(swayXAnim, feedTime);
			_state.Speed = 1f;
		}
		if (isSwayY)
		{
			if (_state != null)
			{
				_state.Events(this).Clear();
			}
			_state = tongueMoveLayer.Play(swayYAnim, feedTime);
			_state.Speed = 1f;
		}
	}

	public void ResetTongue()
	{
	}
}
