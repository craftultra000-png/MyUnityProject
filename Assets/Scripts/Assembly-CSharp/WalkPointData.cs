using UnityEngine;

[CreateAssetMenu(fileName = "0_WalkPointData_", menuName = "WayPoint/WakjPointData")]
public class WalkPointData : ScriptableObject
{
	[Header("Start")]
	public string stellaStart;

	public string vacuaStart;

	public string nuisanceStart;

	[Header("Target")]
	public string stellaTarget;

	public string vacuaTarget;

	public string nuisanceTarget;
}
