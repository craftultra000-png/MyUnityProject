using UnityEngine;

public class DestroyAnimCallback : MonoBehaviour
{
	private void AnimationCallback_Destroy()
	{
		Object.Destroy(base.gameObject);
	}
}
