#include "buffer.h"

namespace Renderer
{
    template <typename TData> 
    const uint Buffer<TData>::m_elementSize = sizeof(TData);

    template <typename TData> 
    Buffer<TData>::Buffer(BufferTarget target, int capacity)
    {
        m_target = static_cast<GLenum>(target);

        glGenBuffers(1, &m_handle);
        m_capacity = 0;

        m_buffer = std::make_unique<std::vector<TData>>(capacity);
        m_dirty = true;
    }

    template<class TData>
    Buffer<TData>::~Buffer()
    {
        glDeleteBuffers(1, &m_handle);
    }

    template<class TData> 
    inline uint Buffer<TData>::GetCount() const
    {
        return m_buffer->size();
    }

    template<class TData>
    inline void Buffer<TData>::Bind()
    {
        glBindBuffer(m_target, m_handle);
    }

    template<class TData>
    inline void Buffer<TData>::Unbind()
    {
        glBindBuffer(m_target, 0);
    }

    template<class TData>
    void Buffer<TData>::BufferData(BufferUsageHint usageHint)
    {
        if (m_dirty)
        {
            Bind();

            // If the allocated buffer on the GPU is large enough, don't reallocate
            int requiredSize = m_elementSize * GetCount();
            if (m_capacity >= requiredSize)
            {
                glBufferSubData(m_target, 0, requiredSize, m_buffer->data());
            }
            else
            {
                glBufferData(m_target, requiredSize, m_buffer->data(), usageHint);
                m_capacity = requiredSize;
            }
            m_dirty = false;

            Unbind();
        }
    }

    template<class TData>
    String Buffer<TData>::ToString()
    {
        return "Buffer<" + String(typeid(TData).name()) + "> Handle:" + std::to_string(m_handle) + " ElementSize:" + std::to_string(m_elementSize) + " Count:" + std::to_string(GetCount());
    }
}