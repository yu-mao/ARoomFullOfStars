Shader "Custom/Stencil"
{
    Properties
    {
        [IntRange] _StencilID("Stencil ID", Range(0, 255)) = 0
    }
    
    SubShader
    {
        Tags
        {
            "RenderType" = "Opaque"
            "Queue" = "Geometry"
            "RenderPipeline" = "UniversalPipeline"
        }
        pass
        {
            Blend Zero One
            Zwrite Off
            
            Stencil
            {
                Ref[_StencilID]
                Comp Always
                Pass Replace
            }
        }
    }
}
