using UnityEngine;

public class GroundObject : MonoBehaviour
{
	public enum GroundType
	{
		Default = 0,
		Stone = 1,
		Clothe = 2,
		Grass = 3,
		Wood = 4,
		Dirt = 5
	}

	public GroundType type;
}
