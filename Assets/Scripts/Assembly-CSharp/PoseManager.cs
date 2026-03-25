using System.Collections.Generic;
using System.Linq;
using Animancer;
using TMPro;
using UnityEngine;
using Utage;

public class PoseManager : MonoBehaviour
{
	public static PoseManager instance;

	public MotionAnimancer _motionAnimancer;

	public PoseDataGUI _poseDataGUI;

	public GUIManager _GUIManager;

	public BindManager _bindManager;

	public AnimancerComponent _animancer;

	[Header("Anim FeedTime")]
	public float feedTime = 0.5f;

	[Header("Animation Clip")]
	public int poseType;

	public int lockPoseType;

	public List<AnimationClip> poseCiip;

	public List<AnimationClip> poseCiipLock;

	public List<AnimationClip> poseCiipFuck;

	[Header("Animation Mixer")]
	public Vector2 poseParam;

	public List<Vector2> mixParam;

	private DirectionalMixerState poseMixer;

	[Header("Status")]
	public bool isLock;

	public bool isFuck;

	[Header("Text")]
	public List<TextMeshProUGUI> poseSet;

	public List<UguiLocalize> poseText;

	public List<string> poseName;

	[Header("Button")]
	public List<GameObject> button;

	public List<ButtonTriggerGUI> buttonScript;

	[Header("Roll")]
	public Transform torsoObject;

	public float currentRoll;

	public float rollSpeed = 5f;

	public Vector3 calcRoll;

	[Header("Wait")]
	public bool isActionTime;

	public float actionWait;

	public float actionEndWait;

	[Header("Show Hide Frame")]
	public bool hidePoseList;

	public GameObject framePoseList;

	public TextMeshProUGUI markPoseList;

	[Space]
	public bool hideGimmick;

	public GameObject frameGimmick;

	public TextMeshProUGUI markGimmick;

	[Space]
	public bool hideFeelerDoll;

	public GameObject frameFeelerDoll;

	public TextMeshProUGUI markFeelerDoll;

	[ContextMenu("Set Sort AnimationList")]
	public void SortAnimationList()
	{
		poseCiip = SortClipsByPrefix(poseCiip);
		poseCiipLock = SortClipsByPrefix(poseCiipLock);
		poseCiipFuck = SortClipsByPrefix(poseCiipFuck);
		Debug.LogError("Sort Animation Clip List", base.gameObject);
	}

	private List<AnimationClip> SortClipsByPrefix(List<AnimationClip> list)
	{
		return (from clip in list
			orderby ExtractPrefixNumber(clip.name), clip.name
			select clip).ToList();
	}

	private int ExtractPrefixNumber(string name)
	{
		int num = name.IndexOf('_');
		if (num > 0 && int.TryParse(name.Substring(0, num), out var result))
		{
			return result;
		}
		return int.MaxValue;
	}

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		mixParam = _motionAnimancer.mixParam;
	}

	private void Start()
	{
		calcRoll = torsoObject.transform.localRotation.eulerAngles;
		OnEnable();
		ExcludeButton(poseSet.Count);
		SetPoseName();
	}

	private void LateUpdate()
	{
		currentRoll += Time.deltaTime * rollSpeed;
		if (currentRoll > 360f)
		{
			currentRoll -= 360f;
		}
		calcRoll.y = currentRoll;
		torsoObject.localRotation = Quaternion.Euler(calcRoll);
		if (isActionTime && Time.time >= actionEndWait)
		{
			isActionTime = false;
			GimmickCoolTimeEnd();
		}
		if (_animancer.States.Current == poseMixer)
		{
			poseMixer.Parameter = poseParam;
		}
	}

	public DirectionalMixerState SetMixer(List<AnimationClip> clips)
	{
		if (poseMixer == null)
		{
			poseMixer = new DirectionalMixerState();
			for (int i = 0; i < mixParam.Count; i++)
			{
				poseMixer.Add(clips[i], mixParam[i]);
			}
		}
		poseMixer.Parameter = poseParam;
		return poseMixer;
	}

	public void OnEnable()
	{
		hidePoseList = true;
		HidePoseList();
		hideGimmick = true;
		HideGimmick();
		hideFeelerDoll = true;
		HideFeelerDoll();
		if (isFuck)
		{
			_animancer.Play(poseCiipFuck[lockPoseType], 0f);
		}
		else if (isLock)
		{
			_animancer.Play(poseCiipLock[lockPoseType], 0f);
		}
		else
		{
			_animancer.Play(SetMixer(poseCiip), 0f);
		}
	}

	private void OnDisable()
	{
		currentRoll = 0f;
		calcRoll.y = currentRoll;
		torsoObject.localRotation = Quaternion.Euler(calcRoll);
	}

	public void SetPoseName()
	{
		for (int i = 0; i < poseText.Count; i++)
		{
			poseText[i].Key = poseName[i];
		}
	}

	public void ExcludeButton(int value)
	{
		for (int i = 0; i < button.Count; i++)
		{
			if (i < poseCiip.Count)
			{
				button[i].SetActive(value: true);
			}
			else
			{
				button[i].SetActive(value: false);
			}
		}
	}

	public void ViewPose(int value)
	{
		_animancer.Play(poseCiip[value], feedTime);
		_poseDataGUI.SetIcon(value);
	}

	public void ResetPose()
	{
		_animancer.Play(SetMixer(poseCiip), feedTime);
		_poseDataGUI.ResetIcon();
	}

	public void SetEndPose(int value)
	{
		if (!isLock && !isFuck)
		{
			poseType = value;
			_poseDataGUI.SetTarget(_poseDataGUI.poseTargetParam);
			if (base.gameObject.activeSelf)
			{
				_animancer.Play(SetMixer(poseCiip), feedTime);
			}
			_GUIManager.SetPose();
			_bindManager.ChangePose();
		}
	}

	public void SetPose(int value)
	{
		if (!isLock && !isFuck)
		{
			poseType = value;
			_poseDataGUI.SetTarget(mixParam[poseType]);
			if (base.gameObject.activeSelf)
			{
				_animancer.Play(SetMixer(poseCiip), feedTime);
			}
			_GUIManager.SetPose();
			_bindManager.ChangePose();
		}
	}

	public void GimmickEnd()
	{
		isLock = false;
		isFuck = false;
		lockPoseType = -1;
		SetEndPose(poseType);
	}

	public void GimmickCoolTimeEnd()
	{
		for (int i = 0; i < button.Count; i++)
		{
			buttonScript[i].unuse = isLock;
			buttonScript[i].ColorReset();
		}
	}

	public void ButtonLock(bool value, int pose, bool stunt)
	{
		isLock = value;
		isFuck = stunt;
		lockPoseType = pose;
		for (int i = 0; i < button.Count; i++)
		{
			buttonScript[i].unuse = isLock;
			if (isLock)
			{
				buttonScript[i].Unuse();
			}
			else
			{
				buttonScript[i].ColorReset();
			}
		}
		if (isFuck)
		{
			if (base.gameObject.activeSelf)
			{
				_animancer.Play(poseCiipFuck[pose], feedTime);
			}
		}
		else if (isLock)
		{
			if (base.gameObject.activeSelf)
			{
				_animancer.Play(poseCiipLock[pose], feedTime);
			}
		}
		else
		{
			SetEndPose(poseType);
		}
	}

	public void ButtonUnLock(float value)
	{
		actionWait = value;
		isActionTime = true;
		actionEndWait = Time.time + value;
	}

	public void HidePoseList()
	{
		hidePoseList = !hidePoseList;
		if (hidePoseList)
		{
			markPoseList.text = "-";
		}
		else
		{
			markPoseList.text = "+";
			if (!framePoseList.activeSelf)
			{
				OnDisable();
			}
		}
		framePoseList.SetActive(!hidePoseList);
	}

	public void HideGimmick()
	{
		hideGimmick = !hideGimmick;
		frameGimmick.SetActive(!hideGimmick);
		if (hideGimmick)
		{
			markGimmick.text = "-";
		}
		else
		{
			markGimmick.text = "+";
		}
	}

	public void HideFeelerDoll()
	{
		hideFeelerDoll = !hideFeelerDoll;
		frameFeelerDoll.SetActive(!hideFeelerDoll);
		if (hideFeelerDoll)
		{
			markFeelerDoll.text = "-";
		}
		else
		{
			markFeelerDoll.text = "+";
		}
	}

	public void HideOtherPoseList(bool value)
	{
		hidePoseList = true;
		HidePoseList();
		hideGimmick = !value;
		HideGimmick();
		hideFeelerDoll = !value;
		HideFeelerDoll();
	}

	public void HideOtherGimmick(bool value)
	{
		hidePoseList = !value;
		HidePoseList();
		hideGimmick = true;
		HideGimmick();
		hideFeelerDoll = !value;
		HideFeelerDoll();
	}

	public void HideOtherFeelerDoll(bool value)
	{
		hidePoseList = !value;
		HidePoseList();
		hideGimmick = !value;
		HideGimmick();
		hideFeelerDoll = true;
		HideFeelerDoll();
	}
}
