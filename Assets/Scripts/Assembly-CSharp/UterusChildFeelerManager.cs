using System.Collections.Generic;
using UnityEngine;

public class UterusChildFeelerManager : MonoBehaviour
{
	public UterusAnimancer _uterusAnimancer;

	public ChildFeelerManager _childFeelerManager;

	public CharacterConceiveManager _characterConceiveManager;

	public Transform onomatopoeiaLookTarget;

	public CharacterLifeManager _characterLifeManager;

	[Header("Skill Data")]
	public int skillID = 98;

	public bool isVaginaInsert;

	public bool isRide;

	public bool isFuck;

	[Header("Spawn Point")]
	public Transform birthPoint;

	public Transform impregnatePoint;

	public Transform exitPoint;

	public Transform movePoint;

	[Header("Spawn Data")]
	public int maxSpawn = 25;

	[Header("Uterus Param")]
	public float conceiveParameter;

	public AnimationCurve conceiveSize;

	[Header("Prefab")]
	public GameObject uterusChildFeeler;

	public List<UterusChildFeelerObject> childScriptList;

	[Header("Scale")]
	public Vector3 childScale = new Vector3(0.2f, 0.2f, 0.2f);

	private void Start()
	{
	}

	private void LateUpdate()
	{
		if (isVaginaInsert || isRide || isFuck)
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
				_characterConceiveManager.vaginaChildCount++;
				_characterConceiveManager.SetConceive(0.05f);
				_uterusAnimancer.SetConceive(0.05f);
				GameObject obj = Object.Instantiate(uterusChildFeeler, impregnatePoint.position, impregnatePoint.rotation, base.transform);
				obj.transform.localScale = childScale;
				UterusChildFeelerObject component = obj.GetComponent<UterusChildFeelerObject>();
				component.onomatopoeiaLookTarget = onomatopoeiaLookTarget;
				component._uterusChildFeelerManager = this;
				component.birthPosition = birthPoint.position;
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
		_characterLifeManager.vaginaChildCount += value;
		for (int i = 0; i < value; i++)
		{
			childScriptList[0].GetBirthPosition();
			childScriptList.RemoveAt(0);
			_characterConceiveManager.vaginaChildCount--;
			if (childScriptList.Count < 20)
			{
				_characterConceiveManager.SetConceive(-0.05f);
				_uterusAnimancer.SetConceive(-0.05f);
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
		float num = conceiveSize.Evaluate(conceiveParameter);
		return movePoint.position + new Vector3(insideUnitCircle.x * num, insideUnitCircle.y * num, 0.1f);
	}

	public void ClearList(UterusChildFeelerObject script, float size)
	{
		_childFeelerManager.BirthVaginaFeeler(size);
	}
}
