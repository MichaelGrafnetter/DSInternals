﻿namespace DSInternals.Common.Data
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;

    /// <summary>
    /// The Protection Key Identifier data structure is used to store metadata about keys used to cryptographically wrap DPAPI-NG encryption keys and to derive managed passwords.
    /// </summary>
    /// <see>https://learn.microsoft.com/en-us/openspecs/windows_protocols/ms-dnsp/98a575da-ca48-4afd-ba79-f77a8bed4e4e</see>
    public struct ProtectionKeyIdentifier
    {
        private const string KdsKeyMagic = "KDSK";
        private const int ExpectedVersion = 1;
        private static readonly int StructureHeaderSize = Marshal.SizeOf<ProtectionKeyIdentifierHeader>();

        public int L0KeyId
        {
            get;
            private set;
        }

        public int L1KeyId
        {
            get;
            private set;
        }

        public int L2KeyId
        {
            get;
            private set;
        }

        public Guid RootKeyId
        {
            get;
            private set;
        }

        public string? DomainName
        {
            get;
            private set;
        }

        public string? ForestName
        {
            get;
            private set;
        }

        public ReadOnlyMemory<byte>? PublicKey
        {
            get;
            private set;
        }

        public GroupKeyEnvelopeFlags Flags
        {
            get;
            set;
        }

        /// <summary>
        /// Struct header
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct ProtectionKeyIdentifierHeader
        {
            /// <summary>
            /// The Version.
            /// </summary>
            public int Version;
            /// <summary>
            /// The Magic.
            /// </summary>
            public uint Magic;
            /// <summary>
            /// The Flags.
            /// </summary>
            public GroupKeyEnvelopeFlags Flags;
            /// <summary>
            /// The L0KeyId.
            /// </summary>
            public int L0KeyId;
            /// <summary>
            /// The L1KeyId.
            /// </summary>
            public int L1KeyId;
            /// <summary>
            /// The L2KeyId.
            /// </summary>
            public int L2KeyId;
            /// <summary>
            /// The RootKeyId.
            /// </summary>
            public Guid RootKeyId;
            /// <summary>
            /// The PublicKeyLength.
            /// </summary>
            public int PublicKeyLength;
            /// <summary>
            /// The DomainNameLength.
            /// </summary>
            public int DomainNameLength;
            /// <summary>
            /// The ForestNameLength.
            /// </summary>
            public int ForestNameLength;

            // Variable length strings follow
        }

        public ProtectionKeyIdentifier(ReadOnlyMemory<byte> blob)
        {
            Validator.AssertMinLength(blob, StructureHeaderSize, nameof(blob));

            // Parse the fixed length structure header
            var header = MemoryMarshal.Read<ProtectionKeyIdentifierHeader>(blob.Span);

            // Version must be 0x00000001
            Validator.AssertEquals(ExpectedVersion, header.Version, nameof(header.Version));

            // Magic must be 0x4B53444B
            string magic = ParseMagic(header.Magic);
            Validator.AssertEquals(KdsKeyMagic, magic, nameof(magic));

            // Copy daya fields
            this.Flags = header.Flags;
            this.RootKeyId = header.RootKeyId;
            this.L0KeyId = header.L0KeyId;
            this.L1KeyId = header.L1KeyId;
            this.L2KeyId = header.L2KeyId;

            // Validate variable data length
            int expectedLength = StructureHeaderSize + header.PublicKeyLength + header.DomainNameLength + header.ForestNameLength;
            Validator.AssertMinLength(blob, expectedLength, nameof(blob));

            // Read the variable length data
            var remainingSlice = blob.Slice(StructureHeaderSize);

            if (header.PublicKeyLength > 0)
            {
                // Additional info used in key derivation
                this.PublicKey = remainingSlice.Slice(0, header.PublicKeyLength);

                // Advance the current position.
                remainingSlice = remainingSlice.Slice(header.PublicKeyLength);
            }

            if (header.DomainNameLength > 0)
            {
                // DNS-style name of the Active Directory domain in which this identifier was created.
                var binaryDomainName = remainingSlice.Slice(0, header.DomainNameLength);
                this.DomainName = ParseUnicodeString(binaryDomainName.Span);

                // Advance the current position.
                remainingSlice = remainingSlice.Slice(header.DomainNameLength);
            }

            if (header.ForestNameLength > 0)
            {
                // DNS-style name of the Active Directory forest in which this identifier was created.
                var binaryForestName = remainingSlice.Slice(0, header.ForestNameLength);
                this.ForestName = ParseUnicodeString(binaryForestName.Span);
            }
        }

        public ProtectionKeyIdentifier(Guid rootKeyId, DateTime effectiveTime, string? domain = null, string? forest = null)
        {
            (this.L0KeyId, this.L1KeyId, this.L2KeyId) = KdsRootKey.GetKeyId(effectiveTime);

            this.RootKeyId = rootKeyId;
            this.DomainName = domain;
            this.ForestName = forest;
        }

        public ProtectionKeyIdentifier(Guid rootKeyId, int l0KeyId, int l1KeyId, int l2KeyId, string? domain = null, string? forest = null)
        {
            if (l0KeyId < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(l0KeyId));
            }

            if (l1KeyId < 0 || l1KeyId >= KdsRootKey.L1KeyModulus)
            {
                throw new ArgumentOutOfRangeException(nameof(l1KeyId));
            }

            if (l2KeyId < 0 || l2KeyId >= KdsRootKey.L2KeyModulus)
            {
                throw new ArgumentOutOfRangeException(nameof(l2KeyId));
            }

            this.RootKeyId = rootKeyId;
            this.L0KeyId = l0KeyId;
            this.L1KeyId = l1KeyId;
            this.L2KeyId = l2KeyId;
            this.DomainName = domain;
            this.ForestName = forest;
        }

        /// <summary>
        /// Returns a string representation of the object.
        /// </summary>
        public override string ToString()
        {
            DateTime cycle = KdsRootKey.GetKeyStartTime(this.L0KeyId, this.L1KeyId, this.L2KeyId);

            return string.Format("RootKey={0}, Cycle={1} (L0={2}, L1={3}, L2={4})",
                this.RootKeyId,
                cycle,
                this.L0KeyId,
                this.L1KeyId,
                this.L2KeyId
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        unsafe private static string ParseUnicodeString(ReadOnlySpan<byte> buffer)
        {
            fixed (byte* stringPointer = buffer)
            {
                // Trim \0
                return Encoding.Unicode.GetString(stringPointer, buffer.Length - sizeof(char));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        unsafe private static string ParseMagic(uint magic)
        {
            Span<byte> binaryMagic = stackalloc byte [sizeof(uint)];
            MemoryMarshal.Write(binaryMagic, ref magic);

            fixed (byte* stringPointer = binaryMagic)
            {
                return Encoding.ASCII.GetString(stringPointer, sizeof(uint));
            }
        }
    }
}
