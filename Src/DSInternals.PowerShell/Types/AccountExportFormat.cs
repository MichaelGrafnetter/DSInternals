using DSInternals.Common.Data;

namespace DSInternals.PowerShell
{
    /// <summary>
    /// Specifies the format for exporting account password hashes and credentials for password cracking tools.
    /// </summary>
    public enum AccountExportFormat : byte
    {
        JohnNT = 1,
        JohnNTHistory,
        JohnLM,
        JohnLMHistory,
        HashcatNT,
        HashcatNTHistory,
        HashcatLM,
        HashcatLMHistory,
        NTHash,
        NTHashHistory,
        LMHash,
        LMHashHistory,
        Ophcrack,
        PWDump,
        PWDumpHistory
    }

    /// <summary>
    /// Provides extension methods for AccountExportFormat to determine required property sets for different export formats.
    /// </summary>
    public static class AccountExportFormatExtensions
    {
        /// <summary>
        /// Determines the required account property sets for the specified export format.
        /// </summary>
        /// <param name="format">The account export format.</param>
        /// <returns>The property sets required for the export format.</returns>
        public static AccountPropertySets GetRequiredProperties(this AccountExportFormat? format)
        {
            switch (format)
            {
                case AccountExportFormat.JohnNT:
                case AccountExportFormat.HashcatNT:
                case AccountExportFormat.NTHash:
                    return AccountPropertySets.NTHash;
                case AccountExportFormat.JohnLM:
                case AccountExportFormat.HashcatLM:
                case AccountExportFormat.LMHash:
                    return AccountPropertySets.LMHash;
                case AccountExportFormat.JohnNTHistory:
                case AccountExportFormat.HashcatNTHistory:
                    return AccountPropertySets.NTHash | AccountPropertySets.NTHashHistory;
                case AccountExportFormat.JohnLMHistory:
                case AccountExportFormat.HashcatLMHistory:
                    return AccountPropertySets.LMHash | AccountPropertySets.LMHashHistory;
                case AccountExportFormat.PWDump:
                case AccountExportFormat.Ophcrack:
                    return AccountPropertySets.PasswordHashes;
                case AccountExportFormat.PWDumpHistory:
                    return AccountPropertySets.PasswordHashes | AccountPropertySets.PasswordHashHistory;
                default:
                    return AccountPropertySets.All;
            }
        }
    }
}
