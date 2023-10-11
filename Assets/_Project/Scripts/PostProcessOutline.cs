using System;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
[PostProcess(typeof(PostProcessEffectRenderer),PostProcessEvent.AfterStack, "Outline")]
public sealed class PostProcessOutline: PostProcessEffectSettings
{
    public FloatParameter Thickness = new FloatParameter { value = 1f };
    public FloatParameter DepthMin = new FloatParameter { value = 1f };
    public FloatParameter DepthMax= new FloatParameter { value = 1f };
}

