/*
* Copyright © 2018-2019 Scott Sewell
* See "Licence.txt" for full licence.
*/
using System;
using OpenTK;
using OpenTK.Graphics;
using Engine.Rendering;

namespace Engine
{
    /// <summary>
    /// Manages the main game window.
    /// </summary>
    internal class Window : GameWindow
    {
        private readonly IContext m_context;
        private bool m_viewportDirty = true;

        public Window(IContext context, int width, int height, string title) : base(
            width, height,
            RendererConfig.GRAPHICS_MODE,
            title,
            GameWindowFlags.Default,
            DisplayDevice.Default,
            OpenGLContext.OPENGL_VERSION_MAJOR, OpenGLContext.OPENGL_VERSION_MINOR,
#if DEBUG
            GraphicsContextFlags.Debug
#else
            GraphicsContextFlags.Default
#endif
        )
        {
            m_context = context;
            
#if DEBUG
            m_context.EnableDebugOutput();
#endif

            Logger.Info($"Graphics context information: {Environment.NewLine}{m_context.GetContextInformation()}");
            m_context.CheckSupport();
            
            VSync = VSyncMode.Adaptive;
            TargetRenderFrequency = 60.0;
        }

        protected override void OnResize(EventArgs e)
        {
            m_viewportDirty = true;
            base.OnResize(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            Title = $"(Vsync: {VSync}) FPS: {1f / e.Time:0}";

            if (m_viewportDirty)
            {
                m_context.Resize(Width, Height);
                m_viewportDirty = false;
            }

            base.OnRenderFrame(e);
        }
    }
}
