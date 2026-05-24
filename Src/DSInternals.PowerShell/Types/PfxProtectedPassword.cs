using System.Text;
using DSInternals.Common.Cryptography;
using DSInternals.Common.Data;

namespace DSInternals.PowerShell;

/// <summary>
/// Represents a PKCS #12 password protected by a DPAPI-NG certificate password protector.
/// </summary>
public sealed class PfxProtectedPassword
{
    internal PfxProtectedPassword(string filePath, CngProtectedDataBlob? encryptedPassword)
    {
        ArgumentException.ThrowIfNullOrEmpty(filePath);

        this.FilePath = filePath;
        this.EncryptedPassword = encryptedPassword;
    }

    /// <summary>
    /// Loads a PKCS #12 PFX file and decodes its SID-based certificate password protector.
    /// </summary>
    /// <param name="filePath">The path to the DER or BER encoded PFX file.</param>
    /// <returns>A protected PFX password object that contains the source path and encrypted certificate password.</returns>
    /// <exception cref="ArgumentException">The <paramref name="filePath" /> parameter is <see langword="null" /> or empty.</exception>
    /// <exception cref="UnauthorizedAccessException">Access to the PFX file is denied.</exception>
    /// <exception cref="IOException">The PFX file cannot be read.</exception>
    /// <exception cref="System.Security.Cryptography.CryptographicException">The PFX content is malformed or does not contain a SID-based certificate protector.</exception>
    public static PfxProtectedPassword Load(string filePath)
    {
        ArgumentException.ThrowIfNullOrEmpty(filePath);

        CngProtectedDataBlob protector = CngProtectedDataBlob.DecodeFromPfx(filePath);
        return new PfxProtectedPassword(filePath, protector);
    }

    /// <summary>
    /// Gets the path to the PFX file.
    /// </summary>
    /// <value>The path to the PFX file.</value>
    public string FilePath
    {
        get;
        private set;
    }

    /// <summary>
    /// Gets the DPAPI-NG protected certificate password.
    /// </summary>
    /// <value>The encrypted certificate password, or <see langword="null" /> if the PFX file does not contain one.</value>
    public CngProtectedDataBlob? EncryptedPassword
    {
        get;
        private set;
    }

    /// <summary>
    /// Gets the decrypted certificate password.
    /// </summary>
    /// <value>The decrypted certificate password, or <see langword="null" /> until <see cref="Decrypt" /> succeeds.</value>
    public string? Password
    {
        get;
        private set;
    }

    /// <summary>
    /// Decrypts the certificate password.
    /// </summary>
    /// <param name="kdsRootKeys">The optional KDS root keys used to derive and cache a SID group key before decrypting.</param>
    /// <exception cref="ArgumentException">None of the supplied KDS root keys matches the encrypted password protector.</exception>
    /// <exception cref="FormatException">The decrypted password is not a valid UTF-16 string.</exception>
    /// <exception cref="InvalidOperationException">The encrypted password is not protected by a SID recipient.</exception>
    /// <exception cref="System.Security.Cryptography.CryptographicException">The encrypted password cannot be decrypted.</exception>
    public void Decrypt(KdsRootKey[]? kdsRootKeys = null)
    {
        if (this.EncryptedPassword == null)
        {
            this.Password = null;
            return;
        }

        if (kdsRootKeys?.Length > 0)
        {
            bool groupKeyCached = this.EncryptedPassword.CacheGroupKey(kdsRootKeys);

            if (!groupKeyCached)
            {
                throw new ArgumentException("None of the supplied KDS root keys matches the encrypted password protector.", nameof(kdsRootKeys));
            }
        }

        this.Password = this.EncryptedPassword.DecryptText(Encoding.Unicode);
    }
}
