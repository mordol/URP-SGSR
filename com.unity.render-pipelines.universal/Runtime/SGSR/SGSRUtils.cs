using UnityEngine;
using UnityEngine.Rendering;

public static class SGSRUtils
{
    private static class ShaderConstants
    {
        public static readonly int _SgsrViewportInfo = Shader.PropertyToID("ViewportInfo");
        public static readonly int _SgsrEdgeSharpness = Shader.PropertyToID("EdgeSharpness");
        public static readonly int _SgsrScaleFactor = Shader.PropertyToID("ScaleFactor");
    }

    public static void SetConstants(CommandBuffer cmd, Vector4 viewportInfo, float edgeSharpness, float scaleFactor)
    {
        cmd.SetGlobalVector(ShaderConstants._SgsrViewportInfo, viewportInfo);
        cmd.SetGlobalFloat(ShaderConstants._SgsrEdgeSharpness, edgeSharpness);
        cmd.SetGlobalFloat(ShaderConstants._SgsrScaleFactor, scaleFactor);
    }

    public static bool IsSupported()
    {
        return SystemInfo.graphicsShaderLevel >= 50;
    }
}