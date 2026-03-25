using UnityEngine;

public class AnalAnimation : MonoBehaviour
{
	public AnalController _analController;

	[Header("Paramater")]
	public float param;

	public void Shot()
	{
		_analController.ShotEffect();
	}

	public void InsertSe()
	{
		_analController.InsertSe();
	}

	public void PistonSe()
	{
		_analController.PistonSe();
	}

	public void ConceiveSe()
	{
		_analController.ConceiveSe();
	}
}
