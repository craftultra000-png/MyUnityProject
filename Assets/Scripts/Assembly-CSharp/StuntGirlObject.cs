using UnityEngine;

public class StuntGirlObject : MonoBehaviour
{
	public CharacterLimbIK _characterLimbIK;

	[Header("Hold Object")]
	public Transform holdLeftHand;

	public Transform holdRightHand;

	public Transform holdLeftFoot;

	public Transform holdRightFoot;

	[Header("FrontFuck Object")]
	public Transform frontFuckLeftHand;

	public Transform frontFuckRightHand;

	[Header("RideFuck Object")]
	public Transform rideFuckLeftHand;

	public Transform rideFuckRightHand;

	[Header("LiftFuck Object")]
	public Transform liftFuckLeftHand;

	public Transform liftFuckRightHand;

	[Header("DoggyFuck Object")]
	public Transform doggyFuckLeftHand;

	public Transform doggyFuckRightHand;

	public void SetFrontFuck()
	{
		Debug.LogError("Set FrontFuck HandIK", base.gameObject);
		_characterLimbIK.leftHandTarget = frontFuckLeftHand;
		_characterLimbIK.rightHandTarget = frontFuckRightHand;
		_characterLimbIK.isLeftHand = true;
		_characterLimbIK.isRightHand = true;
	}

	public void SetRideFuck()
	{
		Debug.LogError("Set RideFuck HandIK", base.gameObject);
		_characterLimbIK.leftHandTarget = rideFuckLeftHand;
		_characterLimbIK.rightHandTarget = rideFuckRightHand;
		_characterLimbIK.isLeftHand = true;
		_characterLimbIK.isRightHand = true;
	}

	public void SetLiftFuck()
	{
		Debug.LogError("Set RideFuck HandIK", base.gameObject);
		_characterLimbIK.leftHandTarget = liftFuckLeftHand;
		_characterLimbIK.rightHandTarget = liftFuckRightHand;
		_characterLimbIK.isLeftHand = true;
		_characterLimbIK.isRightHand = true;
	}

	public void SetDoggyFuck()
	{
		Debug.LogError("Set DoggyFuck HandIK", base.gameObject);
		_characterLimbIK.leftHandTarget = doggyFuckLeftHand;
		_characterLimbIK.rightHandTarget = doggyFuckRightHand;
		_characterLimbIK.isLeftHand = true;
		_characterLimbIK.isRightHand = true;
	}

	public void EndFuck()
	{
		_characterLimbIK.leftHandTarget = null;
		_characterLimbIK.rightHandTarget = null;
		_characterLimbIK.leftFootTarget = null;
		_characterLimbIK.rightFootTarget = null;
		_characterLimbIK.isLeftHand = false;
		_characterLimbIK.isRightHand = false;
		_characterLimbIK.isLeftFoot = false;
		_characterLimbIK.isRightFoot = false;
	}

	public void SetIK(bool value)
	{
		if (value)
		{
			_characterLimbIK.leftHandTarget = holdLeftHand;
			_characterLimbIK.rightHandTarget = holdRightHand;
			_characterLimbIK.leftFootTarget = holdLeftFoot;
			_characterLimbIK.rightFootTarget = holdRightFoot;
			_characterLimbIK.isLeftHand = true;
			_characterLimbIK.isRightHand = true;
			_characterLimbIK.isLeftFoot = true;
			_characterLimbIK.isRightFoot = true;
		}
		else
		{
			_characterLimbIK.leftHandTarget = null;
			_characterLimbIK.rightHandTarget = null;
			_characterLimbIK.leftFootTarget = null;
			_characterLimbIK.rightFootTarget = null;
			_characterLimbIK.isLeftHand = false;
			_characterLimbIK.isRightHand = false;
			_characterLimbIK.isLeftFoot = false;
			_characterLimbIK.isRightFoot = false;
		}
	}
}
