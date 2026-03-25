using UnityEngine;

[CreateAssetMenu(fileName = "NewSkillData", menuName = "Skill Data")]
public class SkillData : ScriptableObject
{
	public int skillID;

	public string skillName;

	public string infomation;

	public bool mouse;

	public bool mouseToggle;

	[Space]
	public Sprite skillIcon;

	public Color skillcolor;
}
