using System.Diagnostics.CodeAnalysis;
using System.Security.AccessControl;
using System.Security.Cryptography;
using DSInternals.Common;
using Microsoft.Database.Isam;

using SecurityDescriptorIdentifier = long;

namespace DSInternals.DataStore;
public class SecurityDescriptorResolver : IDisposable
{
    private const string SecurityDescriptorIdentifierColumn = "sd_id";
    private const string SecurityDescriptorValueColumn = "sd_value";
    private const string SecurityDescriptorHashColumn = "sd_hash";
    private const string ReferenceCountColumn = "sd_refcount";
    private const string SecurityDescriptorIdentifierIndex = "sd_id_index";
    private const string SecurityDescriptorHashIndex = "sd_hash_index";
    private const SecurityDescriptorIdentifier RootSecurityDescriptorId = 1;
    private const int RootSecurityDescriptorOffset = sizeof(int);

    private IsamDatabase _database;
    private MD5 _hashFunction;
    private Columnid _securityDescriptorIdentifierColumnId;
    private Columnid _securityDescriptorValueColumnId;

    [SuppressMessage("Security", "CA5351:Do Not Use Broken Cryptographic Algorithms", Justification = "AD security descriptor index still uses MD5.")]
    public SecurityDescriptorResolver(IsamDatabase database)
    {
        _database = database ?? throw new ArgumentNullException(nameof(database));

        var sdTable = database.Tables[ADConstants.SecurityDescriptorTableName];

        // Cache column identifiers
        _securityDescriptorIdentifierColumnId = sdTable.Columns[SecurityDescriptorIdentifierColumn].Columnid;
        _securityDescriptorValueColumnId = sdTable.Columns[SecurityDescriptorValueColumn].Columnid;

        // Cache MD5
        _hashFunction = MD5.Create();
    }

    public RawSecurityDescriptor? GetDescriptor(SecurityDescriptorIdentifier id)
    {
        RawSecurityDescriptor? result = null;

        using (var cursor = _database.OpenCursor(ADConstants.SecurityDescriptorTableName))
        {
            cursor.CurrentIndex = SecurityDescriptorIdentifierIndex;
            bool found = cursor.GotoKey(Key.Compose(id));

            if (found)
            {
                byte[] binaryForm = cursor.RetrieveColumnAsByteArray(_securityDescriptorValueColumnId);

                // Strip the root SD prefix, which is 0x0F000000
                int sdOffset = (id == RootSecurityDescriptorId) ? RootSecurityDescriptorOffset : 0;
                return new RawSecurityDescriptor(binaryForm, sdOffset);
            }
        }

        return result;
    }

    public IEnumerable<SecurityDescriptorIdentifier> FindDescriptor(GenericSecurityDescriptor securityDescriptor)
    {
        byte[] sdHash = ComputeHash(_hashFunction, securityDescriptor);
        // TODO: Handle possible collisions
        return this.FindDescriptorHash(sdHash);
    }

    public IEnumerable<SecurityDescriptorIdentifier> FindDescriptor(string securityDescriptor)
    {
        byte[] sdHash = ComputeHash(_hashFunction, securityDescriptor);
        // TODO: Handle possible collisions
        return this.FindDescriptorHash(sdHash);
    }

    public IEnumerable<SecurityDescriptorIdentifier> FindDescriptorHash(byte[] sdHash)
    {
        ArgumentNullException.ThrowIfNull(sdHash);

        using (var cursor = _database.OpenCursor(ADConstants.SecurityDescriptorTableName))
        {
            cursor.CurrentIndex = SecurityDescriptorHashIndex;
            cursor.FindRecords(MatchCriteria.EqualTo, Key.Compose(sdHash));

            while (cursor.MoveNext())
            {
                yield return cursor.RetrieveColumnAsLong(_securityDescriptorIdentifierColumnId).Value;
            }
        }
    }

    [SuppressMessage("Security", "CA5351:Do Not Use Broken Cryptographic Algorithms", Justification = "AD security descriptor index still uses MD5.")]
    public static byte[] ComputeHash(GenericSecurityDescriptor securityDescriptor)
    {
        ArgumentNullException.ThrowIfNull(securityDescriptor);

        using (MD5 hashFunction = MD5.Create())
        {
            return ComputeHash(hashFunction, securityDescriptor);
        }
    }

    private static byte[] ComputeHash(MD5 hashFunction, GenericSecurityDescriptor securityDescriptor)
    {
        // Convert to binary form. We have to use double conversion, because .NET returns the SD in different order than Win32 API used by AD.
        string stringSecurityDescriptor = securityDescriptor.GetSddlForm(AccessControlSections.All);
        byte[] binaryDescriptor = stringSecurityDescriptor.SddlToBinary();
        return hashFunction.ComputeHash(binaryDescriptor);
    }

    [SuppressMessage("Security", "CA5351:Do Not Use Broken Cryptographic Algorithms", Justification = "AD security descriptor index still uses MD5.")]
    public static byte[] ComputeHash(string securityDescriptor)
    {
        ArgumentNullException.ThrowIfNull(securityDescriptor);

        using (MD5 hashFunction = MD5.Create())
        {
            return ComputeHash(hashFunction, securityDescriptor);
        }
    }

    private static byte[] ComputeHash(MD5 hashFunction, string securityDescriptor)
    {
        byte[] binaryDescriptor = securityDescriptor.SddlToBinary();
        return hashFunction.ComputeHash(binaryDescriptor);
    }

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing && _hashFunction != null)
        {
            _hashFunction.Dispose();
            _hashFunction = null;
        }
    }
}
