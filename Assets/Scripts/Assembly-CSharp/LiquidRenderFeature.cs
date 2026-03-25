using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class LiquidRenderFeature : ScriptableRendererFeature
{
	private class LiquidPass : ScriptableRenderPass
	{
		private Material liquidMaterial;

		private LayerMask layerMask;

		private RenderTexture temporaryRT;

		public LiquidPass(LayerMask layerMask, Material material)
		{
			this.layerMask = layerMask;
			liquidMaterial = material;
		}

		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			Camera camera = renderingData.cameraData.camera;
			CommandBuffer commandBuffer = CommandBufferPool.Get("Liquid Effect");
			RTHandle cameraColorTargetHandle = renderingData.cameraData.renderer.cameraColorTargetHandle;
			int cullingMask = camera.cullingMask;
			camera.cullingMask = layerMask;
			if (temporaryRT == null || temporaryRT.width != renderingData.cameraData.cameraTargetDescriptor.width || temporaryRT.height != renderingData.cameraData.cameraTargetDescriptor.height)
			{
				if (temporaryRT != null)
				{
					temporaryRT.Release();
				}
				RenderTextureDescriptor cameraTargetDescriptor = renderingData.cameraData.cameraTargetDescriptor;
				cameraTargetDescriptor.depthBufferBits = 0;
				temporaryRT = new RenderTexture(cameraTargetDescriptor.width, cameraTargetDescriptor.height, 0, RenderTextureFormat.Default);
				temporaryRT.Create();
			}
			commandBuffer.Blit(cameraColorTargetHandle, temporaryRT);
			commandBuffer.Blit(temporaryRT, cameraColorTargetHandle, liquidMaterial);
			camera.cullingMask = cullingMask;
			context.ExecuteCommandBuffer(commandBuffer);
			CommandBufferPool.Release(commandBuffer);
			temporaryRT.DiscardContents();
		}
	}

	[Header("Liquid Render Feature Settings")]
	public LayerMask liquidLayer = 64;

	public Material liquidMaterial;

	private LiquidPass pass;

	public override void Create()
	{
		pass = new LiquidPass(liquidLayer, liquidMaterial);
		pass.renderPassEvent = RenderPassEvent.AfterRenderingTransparents;
	}

	public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
	{
		renderer.EnqueuePass(pass);
	}
}
