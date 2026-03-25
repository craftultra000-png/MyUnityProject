using UnityEngine;

public class UterusAnimation : MonoBehaviour
{
	public UterusController _uterusController;

	public void Shot()
	{
		_uterusController.ShotEffect();
	}

	public void InsertSe()
	{
		_uterusController.InsertSe();
	}

	public void PistonSe()
	{
		_uterusController.PistonSe();
	}

	public void ConceiveSe()
	{
		_uterusController.ConceiveSe();
	}
}
