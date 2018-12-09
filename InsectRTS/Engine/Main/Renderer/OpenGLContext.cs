/*
* Copyright © 2018-2019 Scott Sewell
* See "Licence.txt" for full licence.
*/
using System;
using System.Text;
using OpenTK.Graphics.OpenGL4;

namespace Engine.Rendering
{
    internal class OpenGLContext : IContext
    {
        public static readonly int OPENGL_VERSION_MAJOR = 4;
        public static readonly int OPENGL_VERSION_MINOR = 6;

        /// <summary>
        /// Gets a string describing the current graphics context.
        /// </summary>
        public string GetContextInformation()
        {
            const int padding = 20;
            StringBuilder sb = new StringBuilder();

            sb.Append("Vendor:".PadRight(padding));
            sb.AppendLine(GL.GetString(StringName.Vendor));

            sb.Append("Renderer:".PadRight(padding));
            sb.AppendLine(GL.GetString(StringName.Renderer));

            sb.Append("OpenGL Version:".PadRight(padding));
            sb.AppendLine(GL.GetString(StringName.Version));

            sb.Append("GLSL Version:".PadRight(padding));
            sb.Append(GL.GetString(StringName.ShadingLanguageVersion));

            return sb.ToString();
        }

        /// <summary>
        /// Checks if the requested context supports the required features.
        /// </summary>
        /// <returns>True if the required features are supported.</returns>
        public bool CheckSupport()
        {
            bool supported = true;

            int major = GL.GetInteger(GetPName.MajorVersion);
            int minor = GL.GetInteger(GetPName.MinorVersion);

            // confirm the graphics api version
            if (major < OPENGL_VERSION_MAJOR || (major == OPENGL_VERSION_MAJOR && minor < OPENGL_VERSION_MINOR))
            {
                Logger.Warning($"OpenGL context is version {major}.{minor} but {OPENGL_VERSION_MAJOR}.{OPENGL_VERSION_MINOR} was requested.");
                supported = false;
            }

            return supported;
        }

        /// <summary>
        /// Enables debug output from the graphics context.
        /// </summary>
        public void EnableDebugOutput()
        {
            GL.Enable(EnableCap.DebugOutput);
            GL.DebugMessageCallback(DebugCallback, IntPtr.Zero);
        }

        /// <summary>
        /// Initializes the context.
        /// </summary>
        public void Initialize()
        {
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.ScissorTest);
        }

        /// <summary>
        /// Resize the context.
        /// </summary>
        public void Resize(int width, int height)
        {
            GL.Viewport(0, 0, width, height);
            GL.Scissor(0, 0, width, height);
        }

        private static void DebugCallback(DebugSource source, DebugType type, int id, DebugSeverity severity, int length, IntPtr message, IntPtr userParam)
        {
            StringBuilder sb = new StringBuilder();

            // get the message source
            sb.Append("OpenGL ");
            switch (source)
            {
                case DebugSource.DebugSourceApplication:
                    sb.Append("Application");
                    break;
                case DebugSource.DebugSourceApi:
                    sb.Append("API");
                    break;
                case DebugSource.DebugSourceThirdParty:
                    sb.Append("Third Party");
                    break;
                case DebugSource.DebugSourceShaderCompiler:
                    sb.Append("Shader Compiler");
                    break;
                case DebugSource.DebugSourceWindowSystem:
                    sb.Append("Window System");
                    break;
                default:
                    sb.Append("Other");
                    break;
            }
            sb.Append(": ");

            // add the message
            unsafe
            {
                byte* str = (byte*)message.ToPointer();
                for (int i = 0; i < length; i++)
                {
                    sb.Append((char)str[i]);
                }
            }

            // forward the message to an appropriate logger method
            string line = sb.ToString();

            switch (severity)
            {
                case DebugSeverity.DebugSeverityHigh:
                    Logger.Error(line, false);
                    break;
                case DebugSeverity.DebugSeverityMedium:
                case DebugSeverity.DebugSeverityLow:
                    Logger.Warning(line);
                    break;
                default:
                    Logger.Info(line);
                    break;
            }
        }
    }
}
