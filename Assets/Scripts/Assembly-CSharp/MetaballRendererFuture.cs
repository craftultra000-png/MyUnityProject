using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class MetaballRendererFuture : ScriptableRendererFeature
{
	private class RenderObjectsPass : ScriptableRenderPass
	{
		private readonly int _renderTargetId;

		private readonly ProfilingSampler _profilingSampler;

		private readonly List<ShaderTagId> _shaderTagIds = new List<ShaderTagId>();

		private RenderTargetIdentifier _renderTargetIdentifier;

		private FilteringSettings _filteringSettings;

		private RenderStateBlock _renderStateBlock;

		public RenderObjectsPass(string profilerTag, int renderTargetId, LayerMask layerMask)
		{
			_profilingSampler = new ProfilingSampler(profilerTag);
			_renderTargetId = renderTargetId;
			_filteringSettings = new FilteringSettings(null, layerMask);
			_shaderTagIds.Add(new ShaderTagId("SRPDefaultUnlit"));
			_shaderTagIds.Add(new ShaderTagId("UniversalForward"));
			_shaderTagIds.Add(new ShaderTagId("UniversalForwardOnly"));
			_shaderTagIds.Add(new ShaderTagId("LightweightForward"));
			_renderStateBlock = new RenderStateBlock(RenderStateMask.Nothing);
		}

		public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
		{
			RenderTextureDescriptor cameraTargetDescriptor = renderingData.cameraData.cameraTargetDescriptor;
			cameraTargetDescriptor.colorFormat = RenderTextureFormat.ARGB32;
			cmd.GetTemporaryRT(_renderTargetId, cameraTargetDescriptor);
			_renderTargetIdentifier = new RenderTargetIdentifier(_renderTargetId);
			RTHandle rTHandle = RTHandles.Alloc(_renderTargetIdentifier);
			ConfigureTarget(rTHandle, renderingData.cameraData.renderer.cameraDepthTargetHandle);
			ConfigureClear(ClearFlag.Color, Color.clear);
		}

		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			SortingCriteria sortingCriteria = SortingCriteria.CommonOpaque;
			DrawingSettings drawingSettings = CreateDrawingSettings(_shaderTagIds, ref renderingData, sortingCriteria);
			CommandBuffer commandBuffer = CommandBufferPool.Get();
			using (new ProfilingScope(commandBuffer, _profilingSampler))
			{
				context.DrawRenderers(renderingData.cullResults, ref drawingSettings, ref _filteringSettings, ref _renderStateBlock);
			}
			context.ExecuteCommandBuffer(commandBuffer);
			commandBuffer.Clear();
			CommandBufferPool.Release(commandBuffer);
		}
	}

	private class KawaseBlurRenderPass : ScriptableRenderPass
	{
		public Material BlurMaterial;

		public Material BlitMaterial;

		public int Passes;

		public int Downsample;

		private int _tmpId1;

		private int _tmpId2;

		private RenderTargetIdentifier _tmpRT1;

		private RenderTargetIdentifier _tmpRT2;

		private readonly int _blurSourceId;

		private RenderTargetIdentifier _blurSourceIdentifier;

		private readonly ProfilingSampler _profilingSampler;

		public KawaseBlurRenderPass(string profilerTag, int blurSourceId)
		{
			_profilingSampler = new ProfilingSampler(profilerTag);
			_blurSourceId = blurSourceId;
		}

		public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
		{
			_blurSourceIdentifier = new RenderTargetIdentifier(_blurSourceId);
		}

		public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
		{
			int width = cameraTextureDescriptor.width / Downsample;
			int height = cameraTextureDescriptor.height / Downsample;
			_tmpId1 = Shader.PropertyToID("tmpBlurRT1");
			_tmpId2 = Shader.PropertyToID("tmpBlurRT2");
			cmd.GetTemporaryRT(_tmpId1, width, height, 0, FilterMode.Bilinear, RenderTextureFormat.ARGB32);
			cmd.GetTemporaryRT(_tmpId2, width, height, 0, FilterMode.Bilinear, RenderTextureFormat.ARGB32);
			_tmpRT1 = new RenderTargetIdentifier(_tmpId1);
			_tmpRT2 = new RenderTargetIdentifier(_tmpId2);
			RTHandle rTHandle = RTHandles.Alloc(_tmpRT1);
			RTHandle rTHandle2 = RTHandles.Alloc(_tmpRT2);
			ConfigureTarget(rTHandle);
			ConfigureTarget(rTHandle2);
		}

		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			RenderTextureDescriptor cameraTargetDescriptor = renderingData.cameraData.cameraTargetDescriptor;
			cameraTargetDescriptor.depthBufferBits = 0;
			CommandBuffer commandBuffer = CommandBufferPool.Get();
			using (new ProfilingScope(commandBuffer, _profilingSampler))
			{
				commandBuffer.SetGlobalFloat("_offset", 1.5f);
				commandBuffer.Blit(_blurSourceIdentifier, _tmpRT1, BlurMaterial);
				for (int i = 1; i < Passes - 1; i++)
				{
					commandBuffer.SetGlobalFloat("_offset", 0.5f + (float)i);
					commandBuffer.Blit(_tmpRT1, _tmpRT2, BlurMaterial);
					RenderTargetIdentifier tmpRT = _tmpRT1;
					_tmpRT1 = _tmpRT2;
					_tmpRT2 = tmpRT;
				}
				commandBuffer.SetGlobalFloat("_offset", 0.5f + (float)Passes - 1f);
				commandBuffer.Blit(_tmpRT1, renderingData.cameraData.renderer.cameraColorTargetHandle, BlitMaterial);
			}
			context.ExecuteCommandBuffer(commandBuffer);
			commandBuffer.Clear();
			CommandBufferPool.Release(commandBuffer);
		}

		public override void OnCameraCleanup(CommandBuffer cmd)
		{
			cmd.ReleaseTemporaryRT(_tmpId1);
			cmd.ReleaseTemporaryRT(_tmpId2);
		}
	}

	private RenderObjectsPass _renderObjectsPass;

	private KawaseBlurRenderPass _blurPass;

	private const string PassTag = "RenderScreenSpaceMetaballs";

	[SerializeField]
	private string _renderTargetId = "_RenderMetaballsRT";

	[SerializeField]
	private LayerMask _layerMask;

	[SerializeField]
	private Material _blurMaterial;

	[SerializeField]
	private Material _blitMaterial;

	[SerializeField]
	[Range(1f, 16f)]
	private int _blurPasses = 1;

	public override void Create()
	{
		int num = Shader.PropertyToID(_renderTargetId);
		_renderObjectsPass = new RenderObjectsPass("RenderScreenSpaceMetaballs", num, _layerMask);
		_blurPass = new KawaseBlurRenderPass("KawaseBlur", num)
		{
			Downsample = 1,
			Passes = _blurPasses,
			BlitMaterial = _blitMaterial,
			BlurMaterial = _blurMaterial
		};
	}

	public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
	{
		renderer.EnqueuePass(_renderObjectsPass);
		renderer.EnqueuePass(_blurPass);
	}
}
