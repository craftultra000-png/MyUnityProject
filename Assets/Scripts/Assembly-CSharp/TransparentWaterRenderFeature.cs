using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class TransparentWaterRenderFeature : ScriptableRendererFeature
{
	private class RenderWaterPass : ScriptableRenderPass
	{
		private readonly int _renderTargetId;

		private readonly ProfilingSampler _profilingSampler;

		private RenderTargetIdentifier _renderTargetIdentifier;

		private FilteringSettings _filteringSettings;

		private RenderStateBlock _renderStateBlock;

		private int _waterTexID;

		private int _sceneColorID;

		private int _cameraDepthID;

		public RenderWaterPass(string profilerTag, int renderTargetId, LayerMask layerMask)
		{
			_profilingSampler = new ProfilingSampler(profilerTag);
			_renderTargetId = renderTargetId;
			_filteringSettings = new FilteringSettings(RenderQueueRange.transparent, layerMask);
			_renderStateBlock = new RenderStateBlock(RenderStateMask.Nothing);
			_waterTexID = Shader.PropertyToID("_WaterTexture");
			_sceneColorID = Shader.PropertyToID("_SceneColor");
			_cameraDepthID = Shader.PropertyToID("_CameraDepthTexture");
		}

		public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
		{
			RenderTextureDescriptor cameraTargetDescriptor = renderingData.cameraData.cameraTargetDescriptor;
			cameraTargetDescriptor.colorFormat = RenderTextureFormat.ARGB32;
			cameraTargetDescriptor.depthBufferBits = 0;
			cmd.GetTemporaryRT(_renderTargetId, cameraTargetDescriptor, FilterMode.Bilinear);
			_renderTargetIdentifier = new RenderTargetIdentifier(_renderTargetId);
			RTHandle rTHandle = RTHandles.Alloc(_renderTargetIdentifier);
			ConfigureTarget(rTHandle);
			ConfigureClear(ClearFlag.Color, Color.clear);
		}

		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			CommandBuffer commandBuffer = CommandBufferPool.Get();
			using (new ProfilingScope(commandBuffer, _profilingSampler))
			{
				SortingCriteria sortingCriteria = SortingCriteria.CommonTransparent;
				DrawingSettings drawingSettings = CreateDrawingSettings(new List<ShaderTagId>
				{
					new ShaderTagId("SRPDefaultUnlit")
				}, ref renderingData, sortingCriteria);
				context.DrawRenderers(renderingData.cullResults, ref drawingSettings, ref _filteringSettings, ref _renderStateBlock);
				commandBuffer.SetGlobalTexture(_waterTexID, _renderTargetIdentifier);
				commandBuffer.SetGlobalTexture(_sceneColorID, renderingData.cameraData.renderer.cameraColorTarget);
				RenderTargetIdentifier cameraDepthTarget = renderingData.cameraData.renderer.cameraDepthTarget;
				commandBuffer.SetGlobalTexture(_cameraDepthID, cameraDepthTarget);
			}
			context.ExecuteCommandBuffer(commandBuffer);
			CommandBufferPool.Release(commandBuffer);
		}

		public override void OnCameraCleanup(CommandBuffer cmd)
		{
			cmd.ReleaseTemporaryRT(_renderTargetId);
		}
	}

	private RenderWaterPass _renderWaterPass;

	[SerializeField]
	private string _renderTargetId = "_WaterRT";

	[SerializeField]
	private LayerMask _layerMask;

	[SerializeField]
	private Shader _waterBlendShader;

	public override void Create()
	{
		int renderTargetId = Shader.PropertyToID(_renderTargetId);
		_renderWaterPass = new RenderWaterPass("RenderWater", renderTargetId, _layerMask);
		_renderWaterPass.renderPassEvent = RenderPassEvent.AfterRenderingOpaques;
	}

	public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
	{
		renderer.EnqueuePass(_renderWaterPass);
	}
}
