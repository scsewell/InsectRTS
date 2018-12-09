/*
* Copyright © 2018-2019 Scott Sewell
* See "Licence.txt" for full licence.
*/
using OpenTK.Graphics;

namespace Engine.Rendering
{
    /// <summary>
    /// Constants used for the renderer configuration.
    /// </summary>
    public static class RendererConfig
    {
        private static readonly ColorFormat MAIN_BUFFER_FORMAT  = new ColorFormat(8, 8, 8, 8);
        private static readonly ColorFormat ACCUM_BUFFER_FORMAT = new ColorFormat(0);
        private static readonly int DEPTH_BUFFER_SIZE           = 24;
        private static readonly int STENCIL_BUFFER_SIZE         = 8;
        private static readonly int AA_SAMPLES                  = 0;
        private static readonly int BUFFER_COUNT                = 2;

        public static readonly GraphicsMode GRAPHICS_MODE = new GraphicsMode(MAIN_BUFFER_FORMAT, DEPTH_BUFFER_SIZE, STENCIL_BUFFER_SIZE, AA_SAMPLES, ACCUM_BUFFER_FORMAT, BUFFER_COUNT, false);
    }
}
