using System.Security.Principal;
using DSInternals.Common.Cryptography;

namespace DSInternals.Common.Test;

[TestClass]
public class PfxCertificateSidProtectorTester
{
    private const string Sid3DesChainPropertiesPfx = """
        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
        """;

    private const string SidAes256EndCertificateNoPropertiesPfx = """
        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
        AdUwggHRBgkqhkiG9w0BBwOgggHCMIIBvgIBAjGCAR2iggEZAgEEMIHcBIGEAQAAAEtEU0sCAAAAbAEAAAQAAAAWAAAAcWtVHCLtX8RyPN2+GZ9oJCAAAAAY
        AAAAGAAAAPcLXRhEo18uYxWCjtWd6Pv2wZb4Obcbjxgji8KQ1/BVYwBvAG4AdABvAHMAbwAuAGMAbwBtAAAAYwBvAG4AdABvAHMAbwAuAGMAbwBtAAAAMFMG
        CSsGAQQBgjdKATBGBgorBgEEAYI3SgEBMDgwNjA0DANTSUQMLVMtMS01LTIxLTMyODg4NTAzOTItMzI5OTUzNjkzMi0yNjE0NzkzMDgxLTUxMjALBglghkgB
        ZQMEAS0EKEb1tkoGQzV1MEwsuAR9ROh2TgnAb7TEFDwdmOT8SUzq4SeX1qmtUgIwgZcGCSqGSIb3DQEHATAeBglghkgBZQMEAS4wEQQM9Hoy9x6sU2lvwdjE
        AgEQgGrQcQEkDt/4TlyZTJMDCCvO/vWulhA06CEN2r+xe4a0rrQAcKTMjrKxp5uWPA3KAPGYaJNW7rfQ8avqCY8B+annOru4hNHFoFCECCzffu4S1ushWzez
        O4l+le/+d6xW6vlvYnbBlhW3TNsiMQAwSzAvMAsGCWCGSAFlAwQCAQQgLe8CqfmwAWitDWkEgZFMWD6KWtP3G24SBh/whFYd3qcEFGRpbYIIiAmkSFaNf6Cj
        8GcjTD/dAgIH0A==
        """;

    [TestMethod]
    public void PfxSidProtector_3DesChainProperties()
    {
        CngProtectedDataBlob protector = CngProtectedDataBlob.DecodeFromPfx(Convert.FromBase64String(Sid3DesChainPropertiesPfx));

        Assert.AreEqual(2, protector.SidKeyProtectors.Count);
        Assert.AreEqual(new SecurityIdentifier("S-1-5-21-3288850392-3299536932-2614793081-519"), protector.SidKeyProtectors[0].TargetSid);
        Assert.AreEqual(new SecurityIdentifier("S-1-5-21-3288850392-3299536932-2614793081-512"), protector.SidKeyProtectors[1].TargetSid);
        Assert.AreEqual(
            "SID=S-1-5-21-3288850392-3299536932-2614793081-519 OR SID=S-1-5-21-3288850392-3299536932-2614793081-512",
            protector.Descriptor);
        Assert.AreEqual(Guid.Parse("1c556b71-ed22-c45f-723c-ddbe199f6824"), protector.SidKeyProtectors[0].ProtectionKeyIdentifier.RootKeyId);
        Assert.AreEqual("contoso.com", protector.SidKeyProtectors[0].ProtectionKeyIdentifier.DomainName);
        Assert.AreEqual("contoso.com", protector.SidKeyProtectors[0].ProtectionKeyIdentifier.ForestName);
        Assert.AreEqual(364, protector.SidKeyProtectors[0].ProtectionKeyIdentifier.L0KeyId);
        Assert.AreEqual(4, protector.SidKeyProtectors[0].ProtectionKeyIdentifier.L1KeyId);
        Assert.AreEqual(22, protector.SidKeyProtectors[0].ProtectionKeyIdentifier.L2KeyId);
        Assert.AreEqual("2.16.840.1.101.3.4.1.46", protector.ContentEncryptionAlgorithm.Value);
        Assert.AreEqual("2.16.840.1.101.3.4.1.45", protector.SidKeyProtectors[0].KeyEncryptionAlgorithm.Value);
        Assert.AreEqual(40, protector.SidKeyProtectors[0].EncryptedKey.Length);
        Assert.AreEqual(106, protector.EncryptedData.Length);
        Assert.AreEqual(12, protector.Nonce.Length);
    }

    [TestMethod]
    public void PfxSidProtector_Aes256EndCertificateNoProperties()
    {
        CngProtectedDataBlob protector = CngProtectedDataBlob.DecodeFromPfx(Convert.FromBase64String(SidAes256EndCertificateNoPropertiesPfx));

        Assert.AreEqual(1, protector.SidKeyProtectors.Count);
        Assert.AreEqual(new SecurityIdentifier("S-1-5-21-3288850392-3299536932-2614793081-512"), protector.SidKeyProtectors[0].TargetSid);
        Assert.AreEqual("SID=S-1-5-21-3288850392-3299536932-2614793081-512", protector.Descriptor);
        Assert.AreEqual(Guid.Parse("1c556b71-ed22-c45f-723c-ddbe199f6824"), protector.SidKeyProtectors[0].ProtectionKeyIdentifier.RootKeyId);
        Assert.AreEqual("contoso.com", protector.SidKeyProtectors[0].ProtectionKeyIdentifier.DomainName);
        Assert.AreEqual("contoso.com", protector.SidKeyProtectors[0].ProtectionKeyIdentifier.ForestName);
        Assert.AreEqual(364, protector.SidKeyProtectors[0].ProtectionKeyIdentifier.L0KeyId);
        Assert.AreEqual(4, protector.SidKeyProtectors[0].ProtectionKeyIdentifier.L1KeyId);
        Assert.AreEqual(22, protector.SidKeyProtectors[0].ProtectionKeyIdentifier.L2KeyId);
        Assert.AreEqual("2.16.840.1.101.3.4.1.46", protector.ContentEncryptionAlgorithm.Value);
        Assert.AreEqual("2.16.840.1.101.3.4.1.45", protector.SidKeyProtectors[0].KeyEncryptionAlgorithm.Value);
        Assert.AreEqual(40, protector.SidKeyProtectors[0].EncryptedKey.Length);
        Assert.AreEqual(106, protector.EncryptedData.Length);
        Assert.AreEqual(12, protector.Nonce.Length);
    }

    [TestMethod]
    public void PfxSidProtector_FilePath()
    {
        string filePath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid():N}.pfx");

        try
        {
            File.WriteAllBytes(filePath, Convert.FromBase64String(SidAes256EndCertificateNoPropertiesPfx));
            CngProtectedDataBlob protector = CngProtectedDataBlob.DecodeFromPfx(filePath);

            Assert.IsNotNull(protector);
            Assert.AreEqual(1, protector.SidKeyProtectors.Count);
            Assert.AreEqual(new SecurityIdentifier("S-1-5-21-3288850392-3299536932-2614793081-512"), protector.SidKeyProtectors[0].TargetSid);
            Assert.AreEqual("SID=S-1-5-21-3288850392-3299536932-2614793081-512", protector.Descriptor);
        }
        finally
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }

    [TestMethod]
    public void PfxSidProtector_FilePathRequired()
    {
        Assert.ThrowsExactly<ArgumentNullException>(() => CngProtectedDataBlob.DecodeFromPfx((string)null));
        Assert.ThrowsExactly<ArgumentException>(() => CngProtectedDataBlob.DecodeFromPfx(string.Empty));
    }
}
