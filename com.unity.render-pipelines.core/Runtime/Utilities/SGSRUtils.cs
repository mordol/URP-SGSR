using UnityEngine;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering
{
    public static class SGSRUtils
    {
        private static class ShaderConstants
        {
            public static readonly int _SgsrViewportInfo = Shader.PropertyToID("ViewportInfo");
            public static readonly int _SgsrEdgeSharpness = Shader.PropertyToID("EdgeSharpness");
            public static readonly int _SgsrScaleFactor = Shader.PropertyToID("ScaleFactor");
        }

        public static void SetViewportInfo(CommandBuffer cmd, Vector4 viewportInfo)
        {
            cmd.SetGlobalVector(ShaderConstants._SgsrViewportInfo, viewportInfo);
        }
        
        public static void SetViewportInfo(RasterCommandBuffer cmd, Vector4 viewportInfo)
        {
            SetViewportInfo(cmd.m_WrappedCommandBuffer, viewportInfo);
        }

        public static void SetConstants(CommandBuffer cmd, float edgeSharpness, float scaleFactor)
        {
            cmd.SetGlobalFloat(ShaderConstants._SgsrEdgeSharpness, edgeSharpness);
            cmd.SetGlobalFloat(ShaderConstants._SgsrScaleFactor, scaleFactor);
        }

        public static void SetConstants(RasterCommandBuffer cmd, float edgeSharpness, float scaleFactor)
        {
            SetConstants(cmd.m_WrappedCommandBuffer, edgeSharpness, scaleFactor);
        }

        public static bool IsSupported()
        {
            return SystemInfo.graphicsShaderLevel >= 50;
        }
    }
}