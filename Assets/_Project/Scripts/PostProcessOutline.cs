using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Assets.BlockPuzzle.Visuals
{
    [Serializable]
    [PostProcess(typeof(PostProcessEffectRenderer),PostProcessEvent.AfterStack, "Outline")]
    public class PostProcessOutline: PostProcessEffectSettings
    {
        public FloatParameter Thickness = new FloatParameter { value = 1f };
        public FloatParameter DepthMin = new FloatParameter { value = 1f };
        public FloatParameter DepthMax= new FloatParameter { value = 1f };
    }

    public class PostProcessOutlineRenderer : PostProcessEffectRenderer<PostProcessOutline>
    {
        public override void Render(PostProcessRenderContext context)
        {
            var sheet = context.propertySheets.Get(Shader.Find("Hidden/Outline"));

            sheet.properties.SetFloat("_Thickness", settings.Thickness);
            sheet.properties.SetFloat("_DepthMin", settings.DepthMin);
            sheet.properties.SetFloat("_DepthMax", settings.DepthMax);

            context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
        }
    }
}

