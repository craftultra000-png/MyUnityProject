using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MissionData", menuName = "Mission/MissionData", order = 1)]
public class MissionData : ScriptableObject
{
	[Header("Mission Name")]
	public List<string> missionName;

	[Header("Mission Count")]
	public List<int> missionCount;

	[Header("Mission Type")]
	public List<Sprite> missionSprite;

	public List<int> missionPulsType;
}
