using System.Runtime.InteropServices;

namespace DSInternals.SAM.Interop;

/// <summary>
/// Used by various Local Security Authority (LSA) functions to specify a Unicode string.
/// </summary>
/// <remarks>
/// Corresponds to the LSA_UNICODE_STRING structure.
/// See https://learn.microsoft.com/en-us/windows/win32/api/lsalookup/ns-lsalookup-lsa_unicode_string
/// </remarks>
[StructLayout(LayoutKind.Sequential)]
internal struct LsaBuffer
{
    /// <summary>
    /// Specifies the length, in bytes, of the string pointed to by the Buffer member, not including the terminating null character, if any.
    /// </summary>
    public ushort Length;

    /// <summary>
    /// Specifies the total size, in bytes, of the memory allocated for Buffer. Up to MaximumLength bytes can be written into the buffer without trampling memory.
    /// </summary>
    public ushort MaximumLength;

    /// <summary>
    /// Pointer to a buffer used to contain a string of wide characters.
    /// </summary>
    public IntPtr Buffer;

    /// <summary>
    /// Gets the content of the buffer as a byte array.
    /// </summary>
    /// <returns>Content of the buffer as a byte array or null if empty.</returns>
    public byte[]? GetBytes()
    {
        if (this.Buffer == IntPtr.Zero || this.Length == 0)
        {
            // Nothing to marshal
            return null;
        }

        byte[] binaryBuffer = new byte[this.Length];
        Marshal.Copy(this.Buffer, binaryBuffer, 0, (int)this.Length);
        return binaryBuffer;
    }
}
