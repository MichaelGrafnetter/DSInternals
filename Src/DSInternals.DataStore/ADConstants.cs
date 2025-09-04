using Microsoft.Isam.Esent.Interop.Vista;
using Windows.Win32;
using Windows.Win32.Globalization;

namespace DSInternals.DataStore
{
    internal static class ADConstants
    {
        /// <summary>
        /// Name of the main Active Directory database table containing object data.
        /// </summary>
        public const string DataTableName = "datatable";
        /// <summary>
        /// Name of the hidden system table containing internal Active Directory metadata.
        /// </summary>
        public const string SystemTableName = "hiddentable";
        /// <summary>
        /// Name of the table containing linked attribute references between Active Directory objects.
        /// </summary>
        public const string LinkTableName = "link_table";
        /// <summary>
        /// Name of the table containing security descriptors for Active Directory objects.
        /// </summary>
        public const string SecurityDescriptorTableName = "sd_table";
        /// <summary>
        /// Base filename prefix for ESE database files.
        /// </summary>
        public const string EseBaseName = "edb";
        /// <summary>
        /// Filename for the ESE temporary database used during operations.
        /// </summary>
        public const string EseTempDatabaseName = "temp.edb";
        /// <summary>
        /// Size of ESE log files in kilobytes (10MB).
        /// </summary>
        public const int EseLogFileSize = 10240; // 10MB
        /// <summary>
        /// Corresponds to DS_DEFAULT_LOCALE
        /// </summary>
        public const uint EseIndexDefaultLocale = PInvoke.LANG_ENGLISH | PInvoke.SUBLANG_ENGLISH_US << 10 | PInvoke.SORT_DEFAULT << 16;
        /// <summary>
        /// Corresponds to DS_DEFAULT_LOCALE_COMPARE_FLAGS | LCMAP_SORTKEY
        /// </summary>
        public const uint EseIndexDefaultCompareOptions = (uint)(
            COMPARE_STRING_FLAGS.NORM_IGNORECASE |
            COMPARE_STRING_FLAGS.NORM_IGNOREKANATYPE |
            COMPARE_STRING_FLAGS.NORM_IGNORENONSPACE |
            COMPARE_STRING_FLAGS.NORM_IGNOREWIDTH |
            COMPARE_STRING_FLAGS.SORT_STRINGSORT) |
            PInvoke.LCMAP_SORTKEY;
        /// <summary>
        /// The EseLegacyFileNames.
        /// </summary>
        public const LegacyFileNames EseLegacyFileNames = LegacyFileNames.EightDotThreeSoftCompat | LegacyFileNames.ESE98FileNames;
        /// <summary>
        /// The 1000.
        /// </summary>
        public const int EseMaxOpenTables = 1000;
    }
}
