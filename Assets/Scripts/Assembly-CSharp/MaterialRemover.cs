using UnityEngine;

public class MaterialRemover : MonoBehaviour
{
	private void OnDestroy()
	{
		ParticleSystemRenderer component = GetComponent<ParticleSystemRenderer>();
		if (component != null && component.material != null && component.material != component.sharedMaterial)
		{
			Object.Destroy(component.material);
		}
	}
}
