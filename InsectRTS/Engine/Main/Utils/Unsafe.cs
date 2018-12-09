/*
* Copyright © 2018-2019 Scott Sewell
* See "Licence.txt" for full licence.
*/
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Engine
{
    /// <summary>
    /// Utility methods useful for making high-performance code and helping with calls to unmanaged code.
    /// </summary>
    internal static class Unsafe
    {
        /// <summary>
        /// Casts one type to another by changing the reference type, leaving the data unmodified. 
        /// This means any changes to the values made to the reinterpereted instance will affect
        /// the values of the original type.
        /// </summary>
        /// <typeparam name="TSrc">The source type. Must be blittable and have the same size as the destination type.</typeparam>
        /// <typeparam name="TDest">The destination type. Must be blittable and have the same size as the source type</typeparam>
        /// <param name="source">The value to cast.</param>
        /// <returns>A reference with the new type pointing to the original value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe TDest ReinterpretCast<TSrc, TDest>(TSrc source)
            where TSrc : struct
            where TDest : struct
        {
#if DEBUG
            // check the types are copyable
            if (!IsBlittable<TSrc>())
            {
                throw new ArgumentException($"\"{typeof(TSrc).FullName}\" must be a blittable type to use a reinterperet cast!");
            }
            else if (!IsBlittable<TDest>())
            {
                throw new ArgumentException($"\"{typeof(TDest).FullName}\" must be a blittable type to use a reinterperet cast!");
            }

            // check the types are the same size
            int srcSize = SizeOf<TSrc>();
            int destSize = SizeOf<TDest>();
            if (srcSize != destSize)
            {
                throw new ArgumentException($"Can't reinterperet cast, \"{typeof(TSrc).FullName}\" has size {srcSize} but \"{typeof(TDest).FullName}\" has size {destSize}!");
            }
#endif

            var sourceRef = __makeref(source);
            var dest = default(TDest);
            var destRef = __makeref(dest);
            *(IntPtr*)&destRef = *(IntPtr*)&sourceRef;
            return __refvalue(destRef, TDest);
        }

        /// <summary>
        /// Gets the number of contiguous bytes an instance of a given type occupies in memory.
        /// </summary>
        /// <typeparam name="T">The type to get the size of.</typeparam>
        /// <returns>The size in bytes.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int SizeOf<T>() where T : struct
        {
            return SizeOfCache<T>.VALUE;
        }

        private static class SizeOfCache<T> where T : struct
        {
            public static readonly int VALUE = SizeOf();

            [StructLayout(LayoutKind.Sequential, Pack = 1)]
            private struct DoubleStruct
            {
                public T value0;
                public T value1;
            }

            public static unsafe int SizeOf()
            {
                var doubleStruct = default(DoubleStruct);

                var tRef0 = __makeref(doubleStruct.value0);
                var tRef1 = __makeref(doubleStruct.value1);

                IntPtr ptrToT0 = *((IntPtr*)&tRef0);
                IntPtr ptrToT1 = *((IntPtr*)&tRef1);

                return (int)(((byte*)ptrToT1) - ((byte*)ptrToT0));
            }
        }

        /// <summary>
        /// Checks if a given type is blittable.
        /// </summary>
        /// <typeparam name="T">The type to check.</typeparam>
        /// <returns>True if the type is blittable.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsBlittable<T>()
        {
            return IsBlittableCache<T>.VALUE;
        }

        private static class IsBlittableCache<T>
        {
            public static readonly bool VALUE = IsBlittable(typeof(T));

            private static bool IsBlittable(Type type)
            {
                if (type.IsArray)
                {
                    Type elem = type.GetElementType();
                    return elem.IsValueType && IsBlittable(elem);
                }
                try
                {
                    object instance = FormatterServices.GetUninitializedObject(type);
                    GCHandle.Alloc(instance, GCHandleType.Pinned).Free();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
