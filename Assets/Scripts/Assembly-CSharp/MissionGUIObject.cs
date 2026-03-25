using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utage;

public class MissionGUIObject : MonoBehaviour
{
	public MissionGUIManager _missionGUIManager;

	public RectTransform mssionTransform;

	public UguiLocalize missionTextLocalize;

	public TextMeshProUGUI missionText;

	public TextMeshProUGUI countText;

	public TextMeshProUGUI clearText;

	public Image missionIcon;

	[Header("Status")]
	public bool isStart;

	public bool isSync;

	public bool isClear;

	public bool isEnd;

	[Header("Mission")]
	public int missionNumber;

	public int missionType;

	public string missionName;

	public int countCurrent;

	public int countCurrentOld;

	public int countStart;

	public int countEnd;

	[Header("Effect")]
	public GameObject spawnEffect;

	public GameObject clearEffect;

	[Header("Icon")]
	public Sprite missionTypeIcon;

	public Sprite questionMark;

	public Sprite exclamationMark;

	[Header("Position")]
	public float missionStartPositon;

	public float missionTargetPositon;

	public float missionCurrentPositon;

	public Vector3 missionCalcPositon;

	public float moveSpeed = 20f;

	[Header("Color")]
	public Color defaultColor;

	public Color clearColor;

	private void Start()
	{
		missionTextLocalize.Key = "????";
		countText.text = countCurrent + "?";
		clearText.gameObject.SetActive(value: false);
		missionIcon.sprite = missionTypeIcon;
		missionText.color = defaultColor;
		countText.color = defaultColor;
		missionIcon.color = defaultColor;
	}

	private void LateUpdate()
	{
		if (isStart)
		{
			missionCurrentPositon += Time.deltaTime * moveSpeed;
			if (missionCurrentPositon > missionTargetPositon)
			{
				isStart = false;
				isSync = true;
				missionCurrentPositon = missionTargetPositon;
				missionTextLocalize.Key = missionName;
				countText.text = countCurrent - countStart + " / " + (countEnd - countStart);
				spawnEffect.SetActive(value: true);
			}
			missionCalcPositon.y = missionCurrentPositon;
			mssionTransform.localPosition = missionCalcPositon;
		}
		else if (isSync)
		{
			if (_missionGUIManager != null)
			{
				countCurrent = _missionGUIManager.missionCount[missionNumber];
			}
			if (countCurrent != countCurrentOld)
			{
				countText.text = countCurrent - countStart + " / " + (countEnd - countStart);
				countCurrentOld = countCurrent;
			}
			if (countCurrent >= countEnd)
			{
				isSync = false;
				isClear = true;
			}
		}
		else if (isClear)
		{
			isClear = false;
			countText.gameObject.SetActive(value: false);
			clearText.gameObject.SetActive(value: true);
			missionText.color = clearColor;
			countText.color = clearColor;
			clearText.color = clearColor;
			missionIcon.color = clearColor;
			_missionGUIManager.ClearMission(missionNumber);
			clearEffect.SetActive(value: true);
		}
		else if (isEnd)
		{
			missionCurrentPositon -= Time.deltaTime * moveSpeed;
			if (missionCurrentPositon < missionStartPositon)
			{
				isEnd = false;
				missionCurrentPositon = missionStartPositon;
			}
			missionCalcPositon.y = missionCurrentPositon;
			mssionTransform.localPosition = missionCalcPositon;
			_missionGUIManager.DestroyMission(this);
			Object.Destroy(base.gameObject);
		}
	}
}
