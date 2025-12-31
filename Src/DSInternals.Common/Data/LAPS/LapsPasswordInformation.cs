namespace DSInternals.Common.Data;

/// <summary>
/// Represents LAPS (Local Administrator Password Solution) password information for a computer.
/// </summary>
public class LapsPasswordInformation
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LapsPasswordInformation"/> class for a legacy LAPS password.
    /// </summary>
    /// <param name="computerName">The name of the computer.</param>
    /// <param name="password">The cleartext password.</param>
    /// <param name="expiration">The expiration date and time of the password.</param>
    public LapsPasswordInformation(string computerName, string password, DateTime? expiration)
    {
        this.Source = LapsPasswordSource.LegacyLapsCleartextPassword;
        this.DecryptionStatus = LapsDecryptionStatus.NotApplicable;
        this.ComputerName = computerName;
        this.ExpirationTimestamp = expiration;
        this.Password = password;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="LapsPasswordInformation"/> class for a cleartext Windows LAPS password.
    /// </summary>
    /// <param name="computerName">The name of the computer.</param>
    /// <param name="password">The parsed cleartext LAPS password.</param>
    /// <param name="expiration">The expiration date and time of the password.</param>
    /// <exception cref="ArgumentNullException">The <paramref name="password"/> parameter is <see langword="null"/>.</exception>
    public LapsPasswordInformation(string computerName, LapsClearTextPassword password, DateTime? expiration)
    {
        ArgumentNullException.ThrowIfNull(password);

        this.ComputerName = computerName;
        this.Account = password.AccountName;
        this.Password = password.Password;
        this.PasswordUpdateTime = password.UpdateTimestamp;
        this.ExpirationTimestamp = expiration;
        this.Source = LapsPasswordSource.CleartextPassword;
        this.DecryptionStatus = LapsDecryptionStatus.NotApplicable;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="LapsPasswordInformation"/> class for an encrypted Windows LAPS password.
    /// </summary>
    /// <param name="computerName">The name of the computer.</param>
    /// <param name="encryptedPassword">The encrypted LAPS password.</param>
    /// <param name="source">One of the enumeration values that specifies the source of the LAPS password.</param>
    /// <param name="expiration">The expiration date and time of the password.</param>
    /// <param name="rootKeyResolver">The KDS root key resolver for decrypting the password. Can be <see langword="null"/> if decryption is not needed.</param>
    /// <exception cref="ArgumentNullException">The <paramref name="encryptedPassword"/> parameter is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException">The <paramref name="source"/> parameter is not a valid encrypted password source.</exception>
    public LapsPasswordInformation(string computerName, LapsEncryptedPassword encryptedPassword, LapsPasswordSource source, DateTime? expiration, IKdsRootKeyResolver? rootKeyResolver = null)
    {
        ArgumentNullException.ThrowIfNull(encryptedPassword);

        // Validate the source type
        this.Source = source switch
        {
            LapsPasswordSource.EncryptedPassword or Data.LapsPasswordSource.EncryptedPasswordHistory or LapsPasswordSource.EncryptedDSRMPassword or LapsPasswordSource.EncryptedDSRMPasswordHistory => source,
            _ => throw new ArgumentOutOfRangeException(nameof(source))
        };

        this.ComputerName = computerName;
        this.PasswordUpdateTime = encryptedPassword.UpdateTimeStamp;
        this.ExpirationTimestamp = expiration;

        // Try to locate the root key and cache the derived group key.
        bool rootKeyFound = false;

        if (rootKeyResolver != null)
        {
            Guid rootKeyId = encryptedPassword.EncryptedBlob.ProtectionKeyIdentifier.RootKeyId;
            KdsRootKey? rootKey = rootKeyResolver.GetKdsRootKey(rootKeyId);

            if (rootKey != null)
            {
                rootKeyFound = true;
                var gke = GroupKeyEnvelope.Create(rootKey, encryptedPassword.EncryptedBlob.ProtectionKeyIdentifier, encryptedPassword.EncryptedBlob.TargetSid);
                gke.WriteToCache();
            }
        }

        // Decrypt the data using the native Win32 API, which uses the pre-cached group keys if available.
        bool isSuccess = encryptedPassword.TryDecrypt(out LapsClearTextPassword decryptedPassword);

        if (isSuccess)
        {
            this.DecryptionStatus = LapsDecryptionStatus.Success;
            this.Account = decryptedPassword.AccountName;
            this.Password = decryptedPassword.Password;
        }
        else
        {
            // Check if offline or online decryption attempt failed.
            this.DecryptionStatus = rootKeyFound ? LapsDecryptionStatus.Error : LapsDecryptionStatus.Unauthorized;
        }
    }

    /// <summary>
    /// Gets the name of the computer.
    /// </summary>
    public string ComputerName { get; private set; }

    /// <summary>
    /// Gets the account name that the password applies to.
    /// </summary>
    public string Account { get; private set; }

    /// <summary>
    /// Gets the cleartext password.
    /// </summary>
    public string Password { get; private set; }

    /// <summary>
    /// Gets the date and time when the password was last updated.
    /// </summary>
    public DateTime? PasswordUpdateTime { get; private set; }

    /// <summary>
    /// Gets the date and time when the password expires.
    /// </summary>
    public DateTime? ExpirationTimestamp { get; private set; }

    /// <summary>
    /// Gets the source of the LAPS password.
    /// </summary>
    public LapsPasswordSource Source { get; private set; }

    /// <summary>
    /// Gets the status of the password decryption.
    /// </summary>
    public LapsDecryptionStatus DecryptionStatus { get; private set; }
}
