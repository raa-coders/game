using System.Collections;
using System.Collections.Generic;
using Rendering.Scripts;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.LWRP;

public class RenderPipelineController : MonoBehaviour
{
    public LightweightRenderPipelineAsset pipelineAsset;

    public ForwardRendererData forwardRenderer;

    public BlitFeature BlitPixelEffect;
    public BlitFeature BlitOutline;
    public BlitFeature BlitHorizonFade;


    public void Awake()
    {
#if !UNITY_EDITOR
        DontDestroyOnLoad(this);
#else
        if (EditorApplication.isPlaying)
            DontDestroyOnLoad(this);
#endif
    }


    public void OnEnable()
    {
        UpdatePipeline();
    }


    public void UpdatePipeline()
    {
        Debug.Log("RenderPipelineController Updating Render Pipeline");

        if (!this.pipelineAsset)
        {
            Debug.LogError("Please, set the RenderPipelineController pipeline asset.");
            return;
        }

        if (!this.forwardRenderer)
        {
            Debug.LogError("Please, set the RenderPipelineController forward renderer.");
            return;
        }

        this.forwardRenderer.rendererFeatures.Clear();

        Add(this.BlitPixelEffect);
        Add(this.BlitOutline);
        Add(this.BlitHorizonFade);

        GraphicsSettings.renderPipelineAsset = this.pipelineAsset;
    }

    private void Add(BlitFeature feature)
    {
        if (feature == null)
            return;

        if (!feature.Enabled)
            return;

        this.forwardRenderer.rendererFeatures.Add(feature);
    }

}
