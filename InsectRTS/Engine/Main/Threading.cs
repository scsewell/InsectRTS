﻿/*
* Copyright © 2018-2019 Scott Sewell
* See "Licence.txt" for full licence.
*/
using System;
using System.Threading;

namespace Engine
{
    /// <summary>
    /// Contains code for helping manage threads.
    /// </summary>
    internal static class Threading
    {
        private static int m_mainThreadId;
        
        /// <summary>
        /// Records the currently executing thead as the main thread.
        /// </summary>
        public static void SetMainThread()
        {
            m_mainThreadId = Thread.CurrentThread.ManagedThreadId;
        }

        /// <summary>
        /// Checks if the code is currently running on the main thread.
        /// </summary>
        /// <returns>true if the code is currently running on the main thread.</returns>
        public static bool IsOnMainThread()
        {
            return m_mainThreadId == Thread.CurrentThread.ManagedThreadId;
        }

        /// <summary>
        /// Throws an exception if the code is not currently running on the main thread.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if the code is not currently running on the main thread.</exception>
        public static void EnsureMainThread()
        {
            if (!IsOnMainThread())
            {
                throw new InvalidOperationException("Operation not called on main thread.");
            }
        }
    }
}