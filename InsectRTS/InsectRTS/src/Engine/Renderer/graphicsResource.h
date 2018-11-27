#pragma once

#include "../../global.h"

namespace Renderer
{
    /// <summary>
    /// A class that controls a handle to an unmanaged graphics resource.
    /// </summary>
    class GraphicsResource
    {
    protected:
        GLuint m_handle;

    public:
        /// <summary>
        /// Cleans up resources held by this instance.
        /// </summary>
        virtual ~GraphicsResource() = 0;
    };

} // namespace renderer
