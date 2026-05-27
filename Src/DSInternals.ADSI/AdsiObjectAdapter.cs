using System.DirectoryServices;
using System.Security.Principal;
using DSInternals.Common.Data;
using DSInternals.Common.Schema;

namespace DSInternals.ADSI;

public class AdsiObjectAdapter : DirectoryObject
{
    protected SearchResult directoryEntry;

    public AdsiObjectAdapter(SearchResult directoryEntry)
    {
        ArgumentNullException.ThrowIfNull(directoryEntry);
        this.directoryEntry = directoryEntry;
    }

    /// <summary>
    /// Gets the distinguished name of the object.
    /// </summary>
    /// <remarks>All AD objects should have a distinguished name, so this property is expected to always return a non-null value.</remarks>
    public override string DistinguishedName => this.ReadAttributeSingle<string>(CommonDirectoryAttributes.DistinguishedName) ?? throw new InvalidOperationException("Could not read the distinguished name.");

    /// <summary>
    /// Gets the GUID of the object.
    /// </summary>
    /// <remarks>All AD objects should have a GUID, so this property is expected to always return a non-empty value.</remarks>
    public override Guid Guid
    {
        get
        {
            this.ReadAttribute(CommonDirectoryAttributes.ObjectGuid, out Guid? guid);
            return guid ?? throw new InvalidOperationException("Could not read the object GUID.");
        }
    }

    public override SecurityIdentifier? Sid
    {
        get
        {
            this.ReadAttribute(CommonDirectoryAttributes.ObjectSid, out SecurityIdentifier? sid);
            return sid;
        }
    }

    protected override bool HasBigEndianRid => false;

    public override bool HasAttribute(string name) => this.directoryEntry.Properties.Contains(name);

    public override void ReadAttribute(string name, out byte[]? value) => value = this.ReadAttributeSingle<byte[]>(name);

    public override void ReadAttribute(string name, out byte[][]? value) => value = this.ReadAttributeMulti<byte[]>(name);

    public override void ReadAttribute(string name, out int? value) => value = this.ReadAttributeSingle<int?>(name);

    // ADSI marshals Boolean-syntax attributes (e.g. isDeleted, dnsIsSigned) as System.Boolean,
    // but some callers also read Integer-syntax attributes (e.g. adminCount) through this overload,
    // so accept either underlying type rather than forcing a single cast.
    public override void ReadAttribute(string name, out bool value)
    {
        object? raw = this.directoryEntry.Properties[name].Cast<object>().FirstOrDefault();
        value = raw switch
        {
            null => false,
            bool b => b,
            int i => i != 0,
            long l => l != 0L,
            _ => Convert.ToBoolean(raw, System.Globalization.CultureInfo.InvariantCulture)
        };
    }

    public override void ReadAttribute(string name, out long? value) => value = this.ReadAttributeSingle<long?>(name);

    // ADSI has already converted attribute values to their native CLR types, so the asGeneralizedTime
    // hint (used by the datastore reader to interpret raw numeric columns) does not apply here:
    // GeneralizedTime/UTC syntax attributes (e.g. whenCreated) arrive as System.DateTime, while
    // Interval/LargeInteger syntax attributes (e.g. pwdLastSet) arrive as a FILETIME Int64.
    public override void ReadAttribute(string name, out DateTime? value, bool asGeneralizedTime)
    {
        object? raw = this.directoryEntry.Properties[name].Cast<object>().FirstOrDefault();
        value = raw switch
        {
            null => null,
            DateTime dt => dt,
            long fileTime => fileTime > 0 ? DateTime.FromFileTime(fileTime) : null,
            int fileTime => fileTime > 0 ? DateTime.FromFileTime(fileTime) : null,
            _ => Convert.ToDateTime(raw, System.Globalization.CultureInfo.InvariantCulture)
        };
    }

    // Unicode vs. IA5 strings are handled by ADSI itself.
    public override void ReadAttribute(string name, out string? value, bool unicode = true) => value = this.ReadAttributeSingle<string>(name);

    // Unicode vs. IA5 strings are handled by ADSI itself.
    public override void ReadAttribute(string name, out string[]? values, bool unicode = true) => values = this.ReadAttributeMulti<string>(name);

    public override void ReadAttribute(string name, out DistinguishedName value)
    {
        string? dnString = this.ReadAttributeSingle<string>(name);
        value = new DistinguishedName(dnString);
    }

    public override void ReadLinkedValues(string attributeName, out byte[][]? values)
    {
        // Parse the DN with binary value
        string[]? textValues = this.ReadAttributeMulti<string>(attributeName);
        values = textValues?.Select(textValue => DNWithBinary.Parse(textValue).Binary)?.ToArray();
    }

    protected TResult? ReadAttributeSingle<TResult>(string name) => this.directoryEntry.Properties[name].Cast<TResult>().FirstOrDefault();

    protected TResult[]? ReadAttributeMulti<TResult>(string name)
    {
        var result = this.directoryEntry.Properties[name].Cast<TResult>().ToArray();
        if (result.Length == 0)
        {
            // We do not want to return an empty array.
            return null;
        }
        return result;
    }
}
