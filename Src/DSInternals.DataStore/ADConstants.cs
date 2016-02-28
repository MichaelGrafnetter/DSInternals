
namespace DSInternals.DataStore
{
    internal static class ADConstants
    {
        public const string DataTableName = "datatable";
        public const string SystemTableName = "hiddentable";
        public const string LinkTableName = "link_table";
        public const string SecurityDescriptorTableName = "sd_table";
        public const int GeneralizedTimeCoefficient = 10000000;
        public const string EseBaseName = "edb";
        public const int PageSize = 8192; // 8k
        public const int EseLogFileSize = 10240; // 10M
        public const int NotAnObjectDNTag = 1;
        public const int RootDNTag = 2;
        public const int RootSecurityDescriptorId = 1;
    }
}
