/*
* Copyright © 2018-2019 Scott Sewell
* See "Licence.txt" for full licence.
*/
namespace Engine.Rendering
{
    /// <summary>
    /// Manages graphics API interactions from the window.
    /// </summary>
    internal interface IContext
    {
        /// <summary>
        /// Gets a string describing the current graphics context.
        /// </summary>
        string GetContextInformation();

        /// <summary>
        /// Checks if the requested context supports the required features.
        /// </summary>
        /// <returns>True if the required features are supported.</returns>
        bool CheckSupport();

        /// <summary>
        /// Enables debug output from the graphics context.
        /// </summary>
        void EnableDebugOutput();

        /// <summary>
        /// Initializes the context.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Resize the context.
        /// </summary>
        void Resize(int width, int height);
    }
}
