/*
* Copyright © 2018-2019 Scott Sewell
* See "Licence.txt" for full licence.
*/
using System;
using Engine.Rendering;

namespace Engine
{
    public abstract class Main
    {
        /// <summary>
        /// The main singleton instance.
        /// </summary>
        public static Main Instance { get; private set; }

        /// <summary>
        /// The name of the application.
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// The game window.
        /// </summary>
        internal Window Window { get; private set; }

        public Main()
        {
            // check that this no instance already exists.
            if (Instance != null)
            {
                throw new InvalidOperationException("Attempted to create another instance of Main class!");
            }

            // configure the main thread
            Threading.SetMainThread();

            RectInt rect = new RectInt(30, 40, 20, 35);
            foreach (var corner in rect.Corners)
            {
                Logger.Info($"{corner}");
            }
            
            Quaternion q1 = Quaternion.FromEuler(15.0f * Mathf.DegToRad, 25.0f * Mathf.DegToRad, 90.0f * Mathf.DegToRad);
            //Quaternion q1 = Quaternion.FromAxisAngle(Vector3.Up, 0f * Mathf.DegToRad);
            Quaternion q2 = Quaternion.FromAxisAngle(Vector3.Up, 0f * Mathf.DegToRad);

            q1 = new Quaternion(0.2418448f, 0.06162842f, 0.664463f, 0.704416f);

            Logger.Info($"{q1}");
            Logger.Info($"{q1.Inverse}");
            Logger.Info($"{q1 * q1.Inverse}");
            
            Vector3 v1 = Vector3.Rotate(Vector3.Up, q1);
            Logger.Info($"{v1.x} {v1.y} {v1.z}");

            // print the system information
            string os = Environment.OSVersion.VersionString;
            string platform = Environment.Is64BitOperatingSystem ? "x64" : "x86";
            Logger.Info($"Running on {os} {platform}");

            // create the game window
            Window window = new Window(new OpenGLContext(), 1280, 720, Name);
            window.UpdateFrame += UpdateFrame;

            window.Run();
        }

        private void UpdateFrame(object sender, OpenTK.FrameEventArgs e)
        {
            try
            {
                Update();
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
            }
        }

        /// <summary>
        /// The main update loop.
        /// </summary>
        protected virtual void Update() {}
    }
}
