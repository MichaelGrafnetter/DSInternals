//-----------------------------------------------------------------------
// <copyright file="jet_unicodeindex2.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.
// </copyright>
//-----------------------------------------------------------------------

namespace Microsoft.Isam.Esent.Interop
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.InteropServices;

    /// <summary>
    /// The native version of the JET_UNICODEINDEX2 structure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    [SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules",
        "SA1305:FieldNamesMustNotUseHungarianNotation",
        Justification = "This should match the unmanaged API, which isn't capitalized.")]
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.NamingRules",
        "SA1307:AccessibleFieldsMustBeginWithUpperCaseLetter",
        Justification = "This should match the unmanaged API, which isn't capitalized.")]
    internal struct NATIVE_UNICODEINDEX2
    {
        /// <summary>
        /// The locale to be used when normalizing unicode data.
        /// </summary>
        public IntPtr szLocaleName;

        /// <summary>
        /// The flags for LCMapString.
        /// </summary>
        public uint dwMapFlags;
    }

    /// <summary>
    /// Customizes how Unicode data gets normalized when an index is created over a Unicode column.
    /// </summary>
    public sealed partial class JET_UNICODEINDEX : IContentEquatable<JET_UNICODEINDEX>, IDeepCloneable<JET_UNICODEINDEX>
    {
        /// <summary>
        /// Contains a limited mapping of LCIDs to locale names.
        /// </summary>
        private static readonly Dictionary<int, string> LcidToLocales = new Dictionary<int, string>(10);

        /// <summary>
        /// Initializes static members of the JET_UNICODEINDEX class.
        /// </summary>
        static JET_UNICODEINDEX()
        {
            // Some common LCIDs are listed at http://msdn.microsoft.com/en-us/goglobal/bb964664.aspx.
            LcidToLocales.Add(127, string.Empty);
            LcidToLocales.Add(1033, "en-us");
            LcidToLocales.Add(1046, "pt-br");
            LcidToLocales.Add(3084, "fr-ca");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JET_UNICODEINDEX"/> class.
        /// </summary>
        public JET_UNICODEINDEX()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JET_UNICODEINDEX"/> class.
        /// </summary>
        /// <param name="native">The native object from which to read values.</param>
        internal JET_UNICODEINDEX(ref NATIVE_UNICODEINDEX2 native)
        {
            this.szLocaleName = Marshal.PtrToStringUni(native.szLocaleName);
            this.dwMapFlags = native.dwMapFlags;
        }

        /// <summary>
        /// As a workaround for systems that do not have LCID support, we will convert a VERY limited number of
        /// LCIDs to locale names.
        /// </summary>
        /// <returns>A BCP-47 style locale name.</returns>
        public string GetEffectiveLocaleName()
        {
            if (this.szLocaleName != null)
            {
                return this.szLocaleName;
            }

            return LimitedLcidToLocaleNameMapping(this.lcid);
        }

        /// <summary>
        /// As a workaround for systems that do not have LCID support, we will convert a VERY limited number of
        /// LCIDs to locale names.
        /// </summary>
        /// <param name="lcid">A locale identifier.</param>
        /// <returns>A BCP-47 style locale name.</returns>
        internal static string LimitedLcidToLocaleNameMapping(int lcid)
        {
            string locale;
            LcidToLocales.TryGetValue(lcid, out locale);
            return locale;
        }

        /// <summary>
        /// Gets the native version of this object.
        /// </summary>
        /// <returns>The native version of this object.</returns>
        internal NATIVE_UNICODEINDEX2 GetNativeUnicodeIndex2()
        {
            if (this.lcid != 0 && !LcidToLocales.ContainsKey(this.lcid))
            {
                throw new ArgumentException("lcid was specified, but this version of the API does not accept LCIDs. Use a locale name or a different API.");
            }

            ////szLocaleName is converted at pinvoke time.
            var native = new NATIVE_UNICODEINDEX2
            {
                dwMapFlags = this.dwMapFlags,
            };
            return native;
        }
    }
}