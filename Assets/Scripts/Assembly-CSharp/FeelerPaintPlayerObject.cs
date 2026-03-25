using UnityEngine;
using UnityEngine.UI;

public class FeelerPaintPlayerObject : MonoBehaviour
{
	public CameraController _cameraController;

	[Header("Status")]
	public bool isMelt;

	public bool isRestore;

	[Header("Camera")]
	public GameObject meltObject;

	public GameObject restoreObject;

	[Header("Time")]
	public int meltSkill;

	public int restoreSkill;

	[Header("GUI")]
	public GameObject meltMask;

	public GameObject restoreMask;

	public Image meltImage;

	public Image restoreImage;

	public Color meltColor;

	public Color restoreColor;

	[Header("Time")]
	public float currentMelt;

	public float currentRestore;

	[Header("Effect")]
	public Transform effectStocker;

	public GameObject meltOnEffect;

	public GameObject meltOffEffect;

	public GameObject restoreOnEffect;

	public GameObject restoreOffEffect;

	private void Start()
	{
		Debug.LogError("Set Paint Player Object");
		currentMelt = 0f;
		meltColor.a = currentMelt;
		meltImage.color = meltColor;
		meltObject.SetActive(value: false);
		meltMask.SetActive(value: false);
		currentRestore = 0f;
		restoreColor.a = currentRestore;
		restoreImage.color = restoreColor;
		restoreObject.SetActive(value: false);
		restoreMask.SetActive(value: false);
	}

	private void FixedUpdate()
	{
		if (isMelt && currentMelt < 1f)
		{
			currentMelt += Time.deltaTime;
			if (currentMelt > 1f)
			{
				currentMelt = 1f;
			}
			meltColor.a = currentMelt;
			meltImage.color = meltColor;
		}
		else if (!isMelt && currentMelt > 0f)
		{
			currentMelt -= Time.deltaTime;
			if (currentMelt < 0f)
			{
				currentMelt = 0f;
				meltMask.SetActive(value: false);
			}
			meltColor.a = currentMelt;
			meltImage.color = meltColor;
		}
		if (isRestore && currentRestore < 1f)
		{
			currentRestore += Time.deltaTime;
			if (currentRestore > 1f)
			{
				currentRestore = 1f;
			}
			restoreColor.a = currentRestore;
			restoreImage.color = restoreColor;
		}
		else if (!isRestore && currentRestore > 0f)
		{
			currentRestore -= Time.deltaTime;
			if (currentRestore < 0f)
			{
				currentRestore = 0f;
				restoreMask.SetActive(value: false);
			}
			restoreColor.a = currentRestore;
			restoreImage.color = restoreColor;
		}
	}

	public void SetSkillID(int melt, int restore)
	{
		meltSkill = melt;
		restoreSkill = restore;
	}

	public void Melt(int skillID)
	{
		Debug.LogError("Set Paint Player Melt Object: " + skillID);
		isMelt = !isMelt;
		_cameraController.isMelt = isMelt;
		if (isMelt)
		{
			_cameraController.MeltSe();
		}
		meltObject.SetActive(isMelt);
		if (isMelt)
		{
			meltMask.SetActive(isMelt);
		}
		if (isMelt && isRestore)
		{
			Restore(restoreSkill);
		}
		if (!isRestore)
		{
			if (isMelt)
			{
				Object.Instantiate(meltOnEffect, effectStocker.position, effectStocker.rotation, effectStocker);
			}
			else if (!isMelt)
			{
				Object.Instantiate(meltOffEffect, effectStocker.position, effectStocker.rotation, effectStocker);
			}
		}
		SkillGUIDataBase.instance.SetEnable(meltSkill, isMelt);
	}

	public void Restore(int skillID)
	{
		Debug.LogError("Set Paint Player Restore Object: " + skillID);
		isRestore = !isRestore;
		_cameraController.isRestore = isRestore;
		if (isRestore)
		{
			_cameraController.RestoreSe();
		}
		restoreObject.SetActive(isRestore);
		if (isRestore)
		{
			restoreMask.SetActive(isRestore);
		}
		if (isMelt && isRestore)
		{
			Melt(meltSkill);
		}
		if (!isMelt)
		{
			if (isRestore)
			{
				Object.Instantiate(restoreOnEffect, effectStocker.position, effectStocker.rotation, effectStocker);
			}
			else if (!isRestore)
			{
				Object.Instantiate(restoreOffEffect, effectStocker.position, effectStocker.rotation, effectStocker);
			}
		}
		SkillGUIDataBase.instance.SetEnable(restoreSkill, isRestore);
	}
}
