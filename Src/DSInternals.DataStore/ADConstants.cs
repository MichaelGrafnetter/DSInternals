
using System.Globalization;
using Microsoft.Isam.Esent.Interop.Vista;

namespace DSInternals.DataStore
{
    internal static class ADConstants
    {
        public const string DataTableName = "datatable";
        public const string SystemTableName = "hiddentable";
        public const string LinkTableName = "link_table";
        public const string SecurityDescriptorTableName = "sd_table";
        public const string EseBaseName = "edb";
        public const string EseTempDatabaseName = "temp.edb";
        public const int EseLogFileSize = 10240; // 10M
        public const int EseIndexDefaultLocale = 1033; // = DS_DEFAULT_LOCALE = EN-US | SORT_DEFAULT
        public const int EseIndexDefaultCompareOptions = 0x00000001 | 0x00000002 | 0x00010000 | 0x00020000 | 0x00001000; // = DS_DEFAULT_LOCALE_COMPARE_FLAGS | LCMAP_SORTKEY = NORM_IGNORECASE | NORM_IGNOREKANATYPE | NORM_IGNORENONSPACE | NORM_IGNOREWIDTH | SORT_STRINGSORT
        public const LegacyFileNames EseLegacyFileNames = LegacyFileNames.EightDotThreeSoftCompat | LegacyFileNames.ESE98FileNames;
        public const int EseMaxOpenTables = 1000;
        public const int NotAnObjectDNTag = 1;
        public const int RootDNTag = 2;
        public const int RootSecurityDescriptorId = 1;
    }
}
