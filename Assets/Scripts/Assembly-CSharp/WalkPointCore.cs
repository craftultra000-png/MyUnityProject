using System.Collections.Generic;
using UnityEngine;

public class WalkPointCore : MonoBehaviour
{
	public static WalkPointCore instance;

	public WalkPointStageData _walkPointStageData;

	public WalkPointDataBase _walkPointDataBase;

	public List<CharacterWalkManager> _characterWalkManager;

	public List<CharacterFaceManager> _characterFaceManager;

	[Header("Start Data")]
	public WalkPointData setData;

	[Header("Current Data")]
	public List<GameObject> currentPoint;

	public GameObject swapPoint;

	[Header("Set Data")]
	public string characterName;

	public string characterName2;

	public string pointName;

	public GameObject targetPoint;

	[Header("Rotate Point")]
	public GameObject rotatePoint;

	public void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		_walkPointStageData.LoadPoint();
		setData = _walkPointStageData.currentData;
		_walkPointDataBase.SetTargetPoint("Stella", setData.stellaStart);
		_characterWalkManager[0].startPoint = _walkPointDataBase.targetPoint;
		_walkPointDataBase.SetTargetPoint("Stella", setData.stellaTarget);
		_characterWalkManager[0].targetPoint = _walkPointDataBase.targetPoint;
		currentPoint[0] = _walkPointDataBase.targetPoint;
		_walkPointDataBase.SetTargetPoint("Vacua", setData.vacuaStart);
		_characterWalkManager[1].startPoint = _walkPointDataBase.targetPoint;
		_walkPointDataBase.SetTargetPoint("Vacua", setData.vacuaTarget);
		_characterWalkManager[1].targetPoint = _walkPointDataBase.targetPoint;
		currentPoint[1] = _walkPointDataBase.targetPoint;
		_walkPointDataBase.SetTargetPoint("Nuisance", setData.nuisanceStart);
		_characterWalkManager[2].startPoint = _walkPointDataBase.targetPoint;
		_walkPointDataBase.SetTargetPoint("Nuisance", setData.nuisanceTarget);
		_characterWalkManager[2].targetPoint = _walkPointDataBase.targetPoint;
		currentPoint[2] = _walkPointDataBase.targetPoint;
		Debug.LogWarning("Start Point Data: " + setData.name, base.gameObject);
	}

	public void ChangeData()
	{
		_walkPointDataBase.SetTargetPoint("Stella", setData.stellaStart);
		_characterWalkManager[0].startPoint = _walkPointDataBase.targetPoint;
		_walkPointDataBase.SetTargetPoint("Stella", setData.stellaTarget);
		_characterWalkManager[0].targetPoint = _walkPointDataBase.targetPoint;
		currentPoint[0] = _walkPointDataBase.targetPoint;
		_walkPointDataBase.SetTargetPoint("Vacua", setData.vacuaStart);
		_characterWalkManager[1].startPoint = _walkPointDataBase.targetPoint;
		_walkPointDataBase.SetTargetPoint("Vacua", setData.vacuaTarget);
		_characterWalkManager[1].targetPoint = _walkPointDataBase.targetPoint;
		currentPoint[1] = _walkPointDataBase.targetPoint;
		_walkPointDataBase.SetTargetPoint("Nuisance", setData.nuisanceStart);
		_characterWalkManager[2].startPoint = _walkPointDataBase.targetPoint;
		_walkPointDataBase.SetTargetPoint("Nuisance", setData.nuisanceTarget);
		_characterWalkManager[2].targetPoint = _walkPointDataBase.targetPoint;
		currentPoint[2] = _walkPointDataBase.targetPoint;
		_characterWalkManager[0].SetMoveAnim();
		_characterWalkManager[0].SetMovePoint(_characterWalkManager[0].targetPoint);
		_characterWalkManager[1].SetMoveAnim();
		_characterWalkManager[1].SetMovePoint(_characterWalkManager[1].targetPoint);
		_characterWalkManager[2].SetMoveAnim();
		_characterWalkManager[2].SetMovePoint(_characterWalkManager[2].targetPoint);
	}

	public void CharacterMoveSet(string character, string point)
	{
		Debug.Log("MoveSet :" + character + " Point: " + point, base.gameObject);
		characterName = character;
		pointName = point;
		_walkPointDataBase.SetTargetPoint(characterName, pointName);
		targetPoint = _walkPointDataBase.targetPoint;
		if (characterName == "Stella")
		{
			_characterWalkManager[0].SetMoveAnim();
			_characterWalkManager[0].SetMovePoint(targetPoint);
			currentPoint[0] = targetPoint;
		}
		else if (characterName == "Vacua")
		{
			_characterWalkManager[1].SetMoveAnim();
			_characterWalkManager[1].SetMovePoint(targetPoint);
			currentPoint[1] = targetPoint;
		}
		else if (characterName == "Nuisance")
		{
			_characterWalkManager[2].SetMoveAnim();
			_characterWalkManager[2].SetMovePoint(targetPoint);
			currentPoint[2] = targetPoint;
		}
	}

	public void CharacterMoveSkip(string character, string point)
	{
		Debug.Log("MoveSet :" + character + " Point: " + point, base.gameObject);
		Debug.LogError("MoveSkip Check Idle or Move:" + character + " Point: " + point, base.gameObject);
		characterName = character;
		pointName = point;
		_walkPointDataBase.SetTargetPoint(characterName, pointName);
		targetPoint = _walkPointDataBase.targetPoint;
		if (characterName == "Stella")
		{
			_characterWalkManager[0].SetMoveAnim();
			_characterWalkManager[0].SkipMovePoint(targetPoint, value: true);
			currentPoint[0] = targetPoint;
		}
		else if (characterName == "Vacua")
		{
			_characterWalkManager[1].SetMoveAnim();
			_characterWalkManager[1].SkipMovePoint(targetPoint, value: true);
			currentPoint[1] = targetPoint;
		}
		else if (characterName == "Nuisance")
		{
			_characterWalkManager[2].SetMoveAnim();
			_characterWalkManager[2].SkipMovePoint(targetPoint, value: true);
			currentPoint[2] = targetPoint;
		}
	}

	public void CharacterSwap(string character, string character2)
	{
		Debug.LogWarning("Swap Position :" + character + " and " + character2, base.gameObject);
		characterName = character;
		characterName2 = character2;
		if (characterName == "Stella")
		{
			targetPoint = currentPoint[0];
		}
		else if (characterName == "Vacua")
		{
			targetPoint = currentPoint[1];
		}
		else if (characterName == "Nuisance")
		{
			targetPoint = currentPoint[2];
		}
		if (characterName2 == "Stella")
		{
			_characterWalkManager[0].SkipMovePoint(targetPoint, value: false);
		}
		else if (characterName2 == "Vacua")
		{
			_characterWalkManager[1].SkipMovePoint(targetPoint, value: false);
		}
		else if (characterName2 == "Nuisance")
		{
			_characterWalkManager[2].SkipMovePoint(targetPoint, value: false);
		}
		if (characterName2 == "Stella")
		{
			targetPoint = currentPoint[0];
		}
		else if (characterName2 == "Vacua")
		{
			targetPoint = currentPoint[1];
		}
		else if (characterName2 == "Nuisance")
		{
			targetPoint = currentPoint[2];
		}
		if (characterName == "Stella")
		{
			_characterWalkManager[0].SkipMovePoint(targetPoint, value: false);
		}
		else if (characterName == "Vacua")
		{
			_characterWalkManager[1].SkipMovePoint(targetPoint, value: false);
		}
		else if (characterName == "Nuisance")
		{
			_characterWalkManager[2].SkipMovePoint(targetPoint, value: false);
		}
		swapPoint = currentPoint[0];
		currentPoint[0] = currentPoint[1];
		currentPoint[1] = swapPoint;
	}

	public void CharacterDisable(string value)
	{
		Debug.LogError("Disable Character: " + value);
		switch (value)
		{
		case "Stella":
			_characterWalkManager[0].gameObject.SetActive(value: false);
			break;
		case "Vacua":
			_characterWalkManager[1].gameObject.SetActive(value: false);
			break;
		case "Nuisance":
			_characterWalkManager[2].gameObject.SetActive(value: false);
			break;
		}
	}

	public void RotatePosition(string pair, bool spin)
	{
		if (pair == "StellaVacua")
		{
			if (characterName == "Stella")
			{
				_characterWalkManager[0].SkipMovePoint(null, value: true);
			}
			else if (characterName == "Vacua")
			{
				_characterWalkManager[1].SkipMovePoint(null, value: true);
			}
		}
		else if (pair == "StellaNuisance")
		{
			if (characterName == "Stella")
			{
				_characterWalkManager[0].SkipMovePoint(null, value: true);
			}
			else if (characterName == "Nuisance")
			{
				_characterWalkManager[2].SkipMovePoint(null, value: true);
			}
		}
	}
}
