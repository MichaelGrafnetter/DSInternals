using System.IO;
using System.Management.Automation;
using System.Text;
using DSInternals.Common.DNS;

namespace DSInternals.PowerShell.Commands;

[Cmdlet(VerbsData.Save, "DnsResourceRecord")]
[Alias("Save-DnsRecord")]
[OutputType("None")]
public class SaveDnsResourceRecordCommand : PSCmdletEx
{
    private const string ZoneFileExtension = ".dns";
    private const string ZoneApexName = "@";
    private static readonly Encoding ZoneFileEncoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false);
    private readonly List<DnsResourceRecord> inputRecords = [];

    [Parameter(
        Mandatory = true,
        ValueFromPipeline = true
    )]
    [ValidateNotNull]
    [Alias("Record", "DnsRecord")]
    public DnsResourceRecord InputObject
    {
        get;
        set;
    }

    [Parameter(
        Mandatory = true,
        Position = 0
    )]
    [ValidateNotNullOrEmpty]
    [Alias("Path", "OutputPath", "OutputDirectory", "OutDir")]
    public string DirectoryPath
    {
        get;
        set;
    }

    [Parameter(Mandatory = false)]
    public SwitchParameter Force
    {
        get;
        set;
    }

    protected override void ProcessRecord()
    {
        this.inputRecords.Add(this.InputObject);
    }

    protected override void EndProcessing()
    {
        if (this.inputRecords.Count == 0)
        {
            this.WriteVerbose("No DNS resource records were received.");
            return;
        }

        string absoluteDirectoryPath = this.ResolveDirectoryPath(this.DirectoryPath);

        IEnumerable<ZoneFile> zoneFiles = this.inputRecords
            .GroupBy(record => record.Zone, StringComparer.OrdinalIgnoreCase)
            .OrderBy(group => group.Key, StringComparer.OrdinalIgnoreCase)
            .Select(group => new ZoneFile(
                group.Key,
                GetZoneFilePath(absoluteDirectoryPath, group.Key),
                [.. group
                    .OrderBy(GetZoneFileRecordPriority)
                    .ThenBy(record => record.Name == ZoneApexName ? 0 : 1)
                    .ThenBy(record => record.Name, StringComparer.OrdinalIgnoreCase)
                    .ThenBy(record => record.Type)
                    .ThenBy(record => record.Data, StringComparer.Ordinal)]));

        var zoneByFilePath = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        foreach (var zoneFile in zoneFiles)
        {
            if (zoneByFilePath.TryGetValue(zoneFile.FilePath, out string existingZone))
            {
                var exception = new InvalidOperationException($"DNS zones '{existingZone}' and '{zoneFile.Zone}' resolve to the same zone file path '{zoneFile.FilePath}'.");
                var error = new ErrorRecord(exception, "SaveDnsResourceRecord_DuplicateFilePath", ErrorCategory.InvalidData, zoneFile.FilePath);
                this.WriteError(error);
                continue;
            }

            if (!this.Force.IsPresent && File.Exists(zoneFile.FilePath))
            {
                var exception = new IOException($"The DNS zone file '{zoneFile.FilePath}' already exists. Use the Force parameter to overwrite it.");
                var error = new ErrorRecord(exception, "SaveDnsResourceRecord_FileExists", ErrorCategory.ResourceExists, zoneFile.FilePath);
                this.WriteError(error);
                continue;
            }

            try
            {
                this.WriteVerbose($"Saving {zoneFile.Records.Length} DNS resource record(s) from zone '{zoneFile.Zone}' to file '{zoneFile.FilePath}'.");
                zoneByFilePath.Add(zoneFile.FilePath, zoneFile.Zone);
                File.WriteAllLines(zoneFile.FilePath, zoneFile.Records.Select(record => record.ToString()), ZoneFileEncoding);
            }
            catch (IOException exception)
            {
                var error = new ErrorRecord(exception, "SaveDnsResourceRecord_WriteFailed", ErrorCategory.WriteError, zoneFile.FilePath);
                this.WriteError(error);
                continue;
            }
        }
    }

    /// <summary>
    /// Combines the output directory with the sanitized zone file name to produce the absolute path of the zone file.
    /// </summary>
    /// <param name="directoryPath">Absolute path to the directory that will contain the zone file.</param>
    /// <param name="zone">DNS zone name used to derive the file name.</param>
    /// <returns>The absolute path of the zone file inside <paramref name="directoryPath"/>.</returns>
    private static string GetZoneFilePath(string directoryPath, string zone)
    {
        string fileName = GetZoneFileName(zone);
        return Path.Combine(directoryPath, fileName);
    }

    /// <summary>
    /// Builds a safe zone-file name by replacing characters that are invalid in a Windows file name with underscores and appending the .dns extension. Falls back to "root.dns" when the zone name is empty or consists only of stripped characters.
    /// </summary>
    /// <param name="zone">DNS zone name to sanitize.</param>
    /// <returns>A file name (with the .dns extension) that is safe to use on a Windows file system.</returns>
    private static string GetZoneFileName(string zone)
    {
        var fileName = new StringBuilder(zone.Length + ZoneFileExtension.Length);
        char[] invalidFileNameChars = Path.GetInvalidFileNameChars();

        foreach (char currentChar in zone)
        {
            bool isInvalid = Array.IndexOf(invalidFileNameChars, currentChar) >= 0 || char.IsControl(currentChar);
            fileName.Append(isInvalid ? '_' : currentChar);
        }

        string sanitizedFileName = fileName.ToString().TrimEnd('.', ' ');

        if (string.IsNullOrEmpty(sanitizedFileName))
        {
            sanitizedFileName = "root";
        }

        return sanitizedFileName + ZoneFileExtension;
    }

    /// <summary>
    /// Returns a sort key that places the SOA record first in a zone file, followed by NS records, then WINS records, then everything else.
    /// </summary>
    /// <param name="record">DNS resource record to evaluate.</param>
    /// <returns>0 for SOA, 1 for NS, 2 for WINS, 3 for any other record type.</returns>
    private static int GetZoneFileRecordPriority(DnsResourceRecord record) => record.Type switch
    {
        ResourceRecordType.SOA => 0,
        ResourceRecordType.NS => 1,
        ResourceRecordType.WINS => 2,
        _ => 3
    };

    private sealed record ZoneFile(string Zone, string FilePath, DnsResourceRecord[] Records);
}
