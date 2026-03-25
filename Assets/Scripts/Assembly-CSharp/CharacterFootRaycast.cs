using UnityEngine;

public class CharacterFootRaycast : MonoBehaviour
{
	public CharacterSoundManager _characterSoundManager;

	[Header("Foot Status")]
	public bool footL;

	public bool footR;

	[Header("Foot bone")]
	public Transform footBoneL;

	public Transform footBoneR;

	public GameObject hitGround;

	public void FootL()
	{
		if (!footL)
		{
			footL = true;
			if (Physics.Raycast(footBoneL.position, -footBoneL.forward, out var hitInfo, 0.5f))
			{
				hitGround = hitInfo.collider.gameObject;
			}
			Debug.DrawRay(footBoneL.position, -footBoneL.forward * 1f, Color.red, 5f);
			GroundTypeCheck();
		}
	}

	public void FootR()
	{
		if (!footR)
		{
			footR = true;
			if (Physics.Raycast(footBoneR.position, -footBoneR.forward, out var hitInfo, 0.5f))
			{
				hitGround = hitInfo.collider.gameObject;
			}
			Debug.DrawRay(footBoneR.position, -footBoneR.forward * 1f, Color.red, 5f);
			GroundTypeCheck();
		}
	}

	public void FootReset()
	{
		footL = false;
		footR = false;
		hitGround = null;
	}

	public void GroundTypeCheck()
	{
		if (hitGround != null && hitGround.tag == "StageObject" && hitGround.GetComponent<GroundObject>() != null)
		{
			if (hitGround.GetComponent<GroundObject>().type.ToString() == "Stone")
			{
				Default();
			}
			if (hitGround.GetComponent<GroundObject>().type.ToString() == "Stone")
			{
				Stone();
			}
			if (hitGround.GetComponent<GroundObject>().type.ToString() == "Clothe")
			{
				Clothe();
			}
			if (hitGround.GetComponent<GroundObject>().type.ToString() == "Wood")
			{
				Wood();
			}
			if (hitGround.GetComponent<GroundObject>().type.ToString() == "Grass")
			{
				Grass();
			}
			if (hitGround.GetComponent<GroundObject>().type.ToString() == "Dirt")
			{
				Dirt();
			}
		}
	}

	public void Default()
	{
		_characterSoundManager.Default();
	}

	public void Stone()
	{
		_characterSoundManager.Stone();
	}

	public void Clothe()
	{
		_characterSoundManager.Clothe();
	}

	public void Wood()
	{
		_characterSoundManager.Wood();
	}

	public void Grass()
	{
		_characterSoundManager.Grass();
	}

	public void Dirt()
	{
		_characterSoundManager.Dirt();
	}
}
