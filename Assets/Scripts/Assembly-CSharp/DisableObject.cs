using UnityEngine;

public class DisableObject : MonoBehaviour
{
	public bool unDisable;

	private void Awake()
	{
		if (!unDisable)
		{
			base.gameObject.SetActive(value: false);
		}
	}
}
