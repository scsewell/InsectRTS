#pragma once

#include "../graphicsResource.h"

namespace Renderer
{
    enum struct BufferTarget : GLenum
    {
        ArrayBuffer             = GL_ARRAY_BUFFER,
        ElementArrayBuffer      = GL_ELEMENT_ARRAY_BUFFER,
        PixelPackBuffer         = GL_PIXEL_PACK_BUFFER,
        PixelUnpackBuffer       = GL_PIXEL_UNPACK_BUFFER,
        UniformBuffer           = GL_UNIFORM_BUFFER,
        TextureBuffer           = GL_TEXTURE_BUFFER,
        TransformFeedbackBuffer = GL_TRANSFORM_FEEDBACK_BUFFER,
        CopyReadBuffer          = GL_COPY_READ_BUFFER,
        CopyWriteBuffer         = GL_COPY_WRITE_BUFFER,
        DrawIndirectBuffer      = GL_DRAW_INDIRECT_BUFFER,
        ShaderStorageBuffer     = GL_SHADER_STORAGE_BUFFER,
        DispatchIndirectBuffer  = GL_DISPATCH_INDIRECT_BUFFER,
        QueryBuffer             = GL_QUERY_BUFFER,
        AtomicCounterBuffer     = GL_ATOMIC_COUNTER_BUFFER,
    };

    enum struct BufferUsageHint : GLenum
    {
        StreamDraw  = GL_STREAM_DRAW,
        StreamRead  = GL_STREAM_READ,
        StreamCopy  = GL_STREAM_COPY,
        StaticDraw  = GL_STATIC_DRAW,
        StaticRead  = GL_STATIC_READ,
        StaticCopy  = GL_STATIC_COPY,
        DynamicRead = GL_DYNAMIC_READ,
        DynamicCopy = GL_DYNAMIC_COPY,
    };

    /// <summary>
    /// The base for all buffer objects. Manages an array that contains all elements
    /// to be buffered on the GPU. A count is used to keep track of how many elements 
    /// to use, much like an array list, mimimizing allocations.
    /// </summary>
    template <class TData> class Buffer: public GraphicsResource
    {
    private:
        static const uint m_elementSize;
        const GLenum m_target;
        uint m_capacity;

    protected:
        std::unique_ptr<std::vector<TData>> m_buffer;
        bool m_dirty;

    public:
        /// <summary>
        /// Initialises a new buffer instance.
        /// </summary>
        /// <param name="target">The buffer type.</param>
        /// <param name="capacity">The initial capacity of the buffer.</param>
        Buffer<TData>(BufferTarget target, int capacity = 1);

        /// <summary>
        /// Cleanup resources.
        /// </summary>
        ~Buffer<TData>();

        /// <summary>
        /// The number of elements in the buffer.
        /// </summary>
        uint GetCount() const;

        /// <summary>
        /// Binds the buffer object.
        /// </summary>
        void Bind();

        /// <summary>
        /// Unbinds the buffer object.
        /// </summary>
        void Unbind();

        /// <summary>
        /// Uploads the buffer to the GPU if it has changed since last buffered.
        /// </summary>
        /// <param name="usageHint">The usage hint.</param>
        void BufferData(BufferUsageHint usageHint = BufferUsageHint.DynamicDraw);

        /// <summary>
        /// Gets a string describing this buffer.
        /// </summary>
        String ToString();
    };

} // namespace renderer
