using UnityEngine;

public class EffectStocker : MonoBehaviour
{
	public static EffectStocker instance;

	public Transform stocker;

	private void Awake()
	{
		instance = this;
	}
}
