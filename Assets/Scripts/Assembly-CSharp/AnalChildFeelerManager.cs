using System.Collections.Generic;
using UnityEngine;

public class AnalChildFeelerManager : MonoBehaviour
{
	public AnalAnimancer _analAnimancer;

	public ChildFeelerManager _childFeelerManager;

	public CharacterConceiveManager _characterConceiveManager;

	public Transform onomatopoeiaLookTarget;

	public CharacterLifeManager _characterLifeManager;

	[Header("Skill Data")]
	public int skillID = 88;

	public bool isAnalInsert;

	public bool isRide;

	public bool isFuck;

	[Header("Spawn Point")]
	public List<Transform> birthPoint;

	public Transform impregnatePoint;

	public Transform exitPoint;

	public Transform movePoint;

	[Header("Spawn Data")]
	public int maxSpawn = 25;

	[Header("Anal Param")]
	public float conceiveParameter;

	public AnimationCurve conceiveSizeX;

	public AnimationCurve conceiveSizeY;

	[Header("Prefab")]
	public GameObject analChildFeeler;

	public List<AnalChildFeelerObject> childScriptList;

	[Header("Scale")]
	public Vector3 childScale = new Vector3(0.2f, 0.2f, 0.2f);

	private void Start()
	{
	}

	private void LateUpdate()
	{
		if (isAnalInsert || isRide || isFuck)
		{
			SkillGUIDataBase.instance.SetEnable(skillID, value: false);
			SkillGUIDataBase.instance.SetLock(skillID, value: true);
		}
		else if (childScriptList.Count > 0)
		{
			SkillGUIDataBase.instance.SetEnable(skillID, value: true);
			SkillGUIDataBase.instance.SetLock(skillID, value: false);
		}
		else
		{
			SkillGUIDataBase.instance.SetEnable(skillID, value: false);
			SkillGUIDataBase.instance.SetLock(skillID, value: true);
		}
	}

	public void SpawnFeeler(int value)
	{
		for (int i = 0; i < value; i++)
		{
			if (childScriptList.Count < maxSpawn)
			{
				_characterConceiveManager.analChildCount++;
				_characterConceiveManager.SetConceive(0.05f);
				_analAnimancer.SetConceive(0.05f);
				GameObject obj = Object.Instantiate(analChildFeeler, impregnatePoint.position, impregnatePoint.rotation, base.transform);
				obj.transform.localScale = childScale;
				AnalChildFeelerObject component = obj.GetComponent<AnalChildFeelerObject>();
				component.onomatopoeiaLookTarget = onomatopoeiaLookTarget;
				component._analChildFeelerManager = this;
				component.birthPoint = birthPoint;
				component.impregnatePosition = impregnatePoint.position;
				component.exitPosition = exitPoint.position;
				component.movePosition = GetMovePoint();
				childScriptList.Add(component);
				ResetMovePoint();
			}
		}
	}

	public void BirthCoolTime(float coolTime)
	{
		if (childScriptList.Count > 0)
		{
			SkillGUIDataBase.instance.SetCoolTime(skillID, coolTime);
		}
	}

	public void BirthFeeler(int value)
	{
		if (childScriptList.Count < value)
		{
			value = childScriptList.Count;
		}
		bool flag = false;
		if (childScriptList.Count > 0)
		{
			flag = true;
		}
		_characterLifeManager.analChildCount += value;
		for (int i = 0; i < value; i++)
		{
			childScriptList[0].GetBirthPosition();
			childScriptList.RemoveAt(0);
			_characterConceiveManager.analChildCount--;
			if (childScriptList.Count < 20)
			{
				_characterConceiveManager.SetConceive(-0.05f);
				_analAnimancer.SetConceive(-0.05f);
			}
		}
		if (flag)
		{
			ResetMovePoint();
		}
	}

	public void SetBirthPoint()
	{
		for (int i = 0; i < childScriptList.Count; i++)
		{
			childScriptList[i].GetBirthPosition();
		}
	}

	public void ResetMovePoint()
	{
		for (int i = 0; i < childScriptList.Count; i++)
		{
			childScriptList[i].isSizeChange = true;
		}
	}

	public Vector3 GetMovePoint()
	{
		Vector2 insideUnitCircle = Random.insideUnitCircle;
		float num = conceiveSizeX.Evaluate(conceiveParameter);
		float num2 = conceiveSizeY.Evaluate(conceiveParameter);
		return movePoint.position + new Vector3(insideUnitCircle.x * num, insideUnitCircle.y * num2, 0.1f);
	}

	public void ClearList(AnalChildFeelerObject script, float size)
	{
		_childFeelerManager.BirthAnalFeeler(size);
	}
}
