using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using DSInternals.Common.Cryptography;

namespace DSInternals.Common.Test;

[TestClass]
public class CngProtectedDataBlobTester
{
    [TestMethod]
    public void CngProtectedDataBlob_DecryptText_Empty()
    {
        var blob = new CngProtectedDataBlob();

        Assert.IsNull(blob.DecryptText(Encoding.Unicode));
    }

    /// <summary>
    /// CMS enveloped DPAPI-NG blob whose single top-level <c>RecipientInfo</c> is an
    /// <c>OtherRecipientInfo</c> ([4]) carrying the <c>AndCombiner</c> protector
    /// (OID 1.3.6.1.4.1.311.74.1.4). Inside the combiner are two embedded
    /// <c>KEKRecipientInfo</c> entries, one bound to the user SID (RID 1000) and one to
    /// the built-in Administrator SID (RID 500), both anchored to KDS root key
    /// <c>1c556b71-ed22-c45f-723c-ddbe199f6824</c>. The parser surfaces the first
    /// SID-based protector while multi-protector enumeration remains a TODO.
    /// </summary>
    [TestMethod]
    public void CngProtectedDataBlob_AndCombiner_TwoSids()
    {
        byte[] blob = Convert.FromBase64String(
            "MIIF6AYJKoZIhvcNAQcDoIIF2TCCBdUCAQIxggWEpIIFgAYKKwYBBAGCN0oBBDCCBXACAQAwggUlooIBGgIBBDCB3QSBhAEA" +
            "AABLRFNLAgAAAGwBAAAEAAAAFQAAAHFrVRwi7V/EcjzdvhmfaCQgAAAAGAAAABgAAAAPrvyac44B6dWc1F5D2OnIMo8bFByh" +
            "sOmktVKT2iP0n2MAbwBuAHQAbwBzAG8ALgBjAG8AbQAAAGMAbwBuAHQAbwBzAG8ALgBjAG8AbQAAADBUBgkrBgEEAYI3SgEw" +
            "RwYKKwYBBAGCN0oBATA5MDcwNQwDU0lEDC5TLTEtNS0yMS0zMjg4ODUwMzkyLTMyOTk1MzY5MzItMjYxNDc5MzA4MS0xMDAw" +
            "MAsGCWCGSAFlAwQBLQQoE651kJ5GGhd6UcIP/XsV6PBT+3LpIws9NEMKJvLu1WFR+voM6xjVaaKCBAMCAQQwggPFBIIDbAEA" +
            "AABLRFNLAwAAAGwBAAAEAAAAFQAAAHFrVRwi7V/EcjzdvhmfaCQIAwAAGAAAABgAAABESFBCAAEAAIeo5h20tmY8/7vRnGUZ" +
            "WZmM7vYIZg3Q8l0s7tRDXjsA4A348dYZV9T6999FYbKqMBbD2RE0CW+qO/QpbYMOmnwgngxkl1F6vVqKnTBrz2ftkfnmcltH" +
            "WMAi4LHvQnW/e2xb/BHUX5CIuUH1TrHlm7i8OaC/EjB/XE/bcMWBsj92tjrK4cqmt5AtUlJnNUiKDvE8bZpRv6SrOtg0d5ZS" +
            "TY72oWe1pBgl2WfhROUUBWQlHMrLg+a0hvazyj95cVBgJsC4V/aJlihW3tQBCr0L5iHDo5YKVOcQw3XyY3XXAUEDpLVDMMGY" +
            "rxJhFtInbhFxX2k4d/rX7wnK2wlK6R4aFZc/syybcxNNCy53UGZg7b1ITKexjyHvIFQH9Hk6GguhJRDbwVB3vkY//0/tSqwL" +
            "tVW+OmwbDGtHsbw3c79+jG9ikBIo+MKMuxilWuMTQQAKZQGW+THHelfy3fRj5ensFEt3feYqqrioYorDdtKC1u04ZOZ5gkKO" +
            "vIMdFDSPby+Rk7UEWvJ2cWTh38lnwfs/LlWkvRv/6DucgNBSuYXRguoK2yo7cxPT/hTISEseBSWIubfSu9LfAWGZ7NBuFVfN" +
            "CRWzNTu7ZODsN3/QKDcN+StSx4kUKM3GfrYYS1I9HbJGwy9jB4SQ8A741kfRSNR5VFFeIyfP75jFgmZLTA9sxBZZQTNarakk" +
            "tbI3Hhmv1ke+ExYoUr02AjJgz7Y9tjx09FVuogZT8OhR0iU6IVF4+fB1+41iWGZXpUnzdTTzDLVLkggYe6/rrsR0eV9HY2nJ" +
            "e/jYgJ704n9blZCR7QAzLvLkyyE41Izz6QUCGYmLDCAUKyZ5Z+dYJvEk6aw+uUPpW5OZItPKu1pGDtNt9KzJQM5VWNM3k6It" +
            "tKYKCUBrZywcTLLwDDESnvtQQakd6WvZPYFZQE1i/dRQ0YyyiXcYKYY5DJE+SGvCPil+jMXni9wcf3hm7w9s8JVnx4gGaMvN" +
            "PCOIpd2osVgxkAcw+7op30oRJ/zXpC+rjOFDVgA+nwV5QWMAbwBuAHQAbwBzAG8ALgBjAG8AbQAAAGMAbwBuAHQAbwBzAG8A" +
            "LgBjAG8AbQAAADBTBgkrBgEEAYI3SgEwRgYKKwYBBAGCN0oBATA4MDYwNAwDU0lEDC1TLTEtNS0yMS0zMjg4ODUwMzkyLTMy" +
            "OTk1MzY5MzItMjYxNDc5MzA4MS01MDAwCwYJYIZIAWUDBAEtBChaBLLTtua4RxZ2Oxfr/CAj5CuaSzdjHXZhDHJBPu+YveiD" +
            "1sVtMgERMAsGCWCGSAFlAwQCATALBglghkgBZQMEAS0EKI0QkR2PE0jktkSHQWStWjBef85f80BT1VyghSIFTCutYa1YVWpJ" +
            "pjUwSAYJKoZIhvcNAQcBMB4GCWCGSAFlAwQBLjARBAzBV4C8M0l4xESKRF4CARCAG9IiTbj3ud4SH5MUjMlV496E5rU7YLK8" +
            "NJzy3A==");

        CngProtectedDataBlob parsed = CngProtectedDataBlob.Decode(blob);

        Assert.AreEqual(2, parsed.SidKeyProtectors.Count);
        Assert.AreEqual(
            new SecurityIdentifier("S-1-5-21-3288850392-3299536932-2614793081-1000"),
            parsed.SidKeyProtectors[0].TargetSid);
        Assert.AreEqual(
            new SecurityIdentifier("S-1-5-21-3288850392-3299536932-2614793081-500"),
            parsed.SidKeyProtectors[1].TargetSid);
        Assert.AreEqual(
            "SID=S-1-5-21-3288850392-3299536932-2614793081-1000 AND SID=S-1-5-21-3288850392-3299536932-2614793081-500",
            parsed.Descriptor);

        Assert.AreEqual(Guid.Parse("1c556b71-ed22-c45f-723c-ddbe199f6824"), parsed.SidKeyProtectors[0].ProtectionKeyIdentifier.RootKeyId);
        Assert.AreEqual("contoso.com", parsed.SidKeyProtectors[0].ProtectionKeyIdentifier.DomainName);
        Assert.AreEqual("contoso.com", parsed.SidKeyProtectors[0].ProtectionKeyIdentifier.ForestName);
        Assert.AreEqual(364, parsed.SidKeyProtectors[0].ProtectionKeyIdentifier.L0KeyId);
        Assert.AreEqual(4, parsed.SidKeyProtectors[0].ProtectionKeyIdentifier.L1KeyId);
        Assert.AreEqual(21, parsed.SidKeyProtectors[0].ProtectionKeyIdentifier.L2KeyId);

        // AES-256-GCM content encryption, AES-256 key wrap for the KEK
        Assert.AreEqual("2.16.840.1.101.3.4.1.46", parsed.ContentEncryptionAlgorithm.Value);
        Assert.AreEqual("2.16.840.1.101.3.4.1.45", parsed.SidKeyProtectors[0].KeyEncryptionAlgorithm.Value);
    }

    [TestMethod]
    public void CngProtectedDataBlob_OrDescriptor_TwoSids()
    {
        byte[] blob = Convert.FromBase64String("""
            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
            """);

        CngProtectedDataBlob parsed = CngProtectedDataBlob.Decode(blob);

        Assert.AreEqual(2, parsed.SidKeyProtectors.Count);
        Assert.AreEqual(
            new SecurityIdentifier("S-1-5-21-3288850392-3299536932-2614793081-1000"),
            parsed.SidKeyProtectors[0].TargetSid);
        Assert.AreEqual(
            new SecurityIdentifier("S-1-5-21-3288850392-3299536932-2614793081-500"),
            parsed.SidKeyProtectors[1].TargetSid);
        Assert.AreEqual(
            "SID=S-1-5-21-3288850392-3299536932-2614793081-1000 OR SID=S-1-5-21-3288850392-3299536932-2614793081-500",
            parsed.Descriptor);

        Assert.AreEqual(Guid.Parse("1c556b71-ed22-c45f-723c-ddbe199f6824"), parsed.SidKeyProtectors[0].ProtectionKeyIdentifier.RootKeyId);
        Assert.AreEqual("contoso.com", parsed.SidKeyProtectors[0].ProtectionKeyIdentifier.DomainName);
        Assert.AreEqual("contoso.com", parsed.SidKeyProtectors[0].ProtectionKeyIdentifier.ForestName);
        Assert.AreEqual(364, parsed.SidKeyProtectors[0].ProtectionKeyIdentifier.L0KeyId);
        Assert.AreEqual(4, parsed.SidKeyProtectors[0].ProtectionKeyIdentifier.L1KeyId);
        Assert.AreEqual(22, parsed.SidKeyProtectors[0].ProtectionKeyIdentifier.L2KeyId);

        // AES-256-GCM content encryption, AES-256 key wrap for the KEK
        Assert.AreEqual("2.16.840.1.101.3.4.1.46", parsed.ContentEncryptionAlgorithm.Value);
        Assert.AreEqual("2.16.840.1.101.3.4.1.45", parsed.SidKeyProtectors[0].KeyEncryptionAlgorithm.Value);
    }

    [TestMethod]
    public void CngProtectedDataBlob_OrDescriptor_LocalMachineOrLocalUserAndSid()
    {
        CngProtectedDataBlob parsed = CngProtectedDataBlob.Decode(Convert.FromBase64String("""
            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
            """));

        Assert.AreEqual("LOCAL=machine OR LOCAL=USER AND SID=S-1-5-21-4392301", parsed.Descriptor);

        // LOCAL protectors are not SID or SDDL; only the SID protector inside the AND combiner is collected.
        Assert.AreEqual(1, parsed.SidKeyProtectors.Count);
        Assert.AreEqual(new SecurityIdentifier("S-1-5-21-4392301"), parsed.SidKeyProtectors[0].TargetSid);

        Assert.AreEqual("2.16.840.1.101.3.4.1.46", parsed.ContentEncryptionAlgorithm.Value);
        Assert.AreEqual("2.16.840.1.101.3.4.1.45", parsed.SidKeyProtectors[0].KeyEncryptionAlgorithm.Value);
    }

    [TestMethod]
    public void CngProtectedDataBlob_LocalMachine()
    {
        CngProtectedDataBlob parsed = CngProtectedDataBlob.Decode(Convert.FromBase64String("""
                MIIB4QYJKoZIhvcNAQcDoIIB0jCCAc4CAQIxggF9ooIBeQIBBDCCATsEggEGAQAAANCMnd8BFdERjHoAwE/Cl+sBAAAA57Ft5fy4Tk+B4omF48EblgQAAAACAAAAAAAQZgAAAAEAACAAAADM8iclug5tDpZzF2K+lmJcf845nlwyDSr6UDZWi
                W2F0AAAAAAOgAAAAAIAACAAAAC1dAiOjsyMeye+wZnjILrZ16jLNNjPwawBCD7Un1NG/zAAAABR16ybOT9nehKqLWzKJN6E+cFDFuzV5b1fncicR/Pe83keXmVkJmnOIRol+Ec0Xj9AAAAAyqbY2FYkXlsGWivMpjZwCMm423h1UpF2vOIm26
                moaVVFk3XVPsamFwqPVWk6Qj+4EvVJMxYKKP1iaJ+b1K/E7jAvBgkrBgEEAYI3SgEwIgYKKwYBBAGCN0oBCDAUMBIwEAwFTE9DQUwMB21hY2hpbmUwCwYJYIZIAWUDBAEtBCijpz5TfQw10XCHOD540D2FSTiH3SjAyPjD16cdOYZ9VAnin5H
                RBwjcMEgGCSqGSIb3DQEHATAeBglghkgBZQMEAS4wEQQMA8HBBh2+tOiRY5FqAgEQgBt0tpuSra0I4s1sgYZV3orMeepoV/beCc3AGQU=
                """));

        Assert.AreEqual("LOCAL=machine", parsed.Descriptor);
    }

    [TestMethod]
    public void CngProtectedDataBlob_Sddl()
    {
        CngProtectedDataBlob parsed = CngProtectedDataBlob.Decode(Convert.FromBase64String("""
                MIIEfwYJKoZIhvcNAQcDoIIEcDCCBGwCAQIxggQbooIEFwIBBDCCA9kEggNsAQAAAEtEU0sDAAAAbAEAAAQAAAAWAAAAcWtVHCLtX8RyPN2+GZ9oJAgDAAAYAAAAGAAAAERIUEIAAQAAh6jmHbS2Zjz/u9GcZRlZmYzu9ghmDdDyXSzu1ENeO
                wDgDfjx1hlX1Pr330VhsqowFsPZETQJb6o79Cltgw6afCCeDGSXUXq9WoqdMGvPZ+2R+eZyW0dYwCLgse9Cdb97bFv8EdRfkIi5QfVOseWbuLw5oL8SMH9cT9twxYGyP3a2Osrhyqa3kC1SUmc1SIoO8TxtmlG/pKs62DR3llJNjvahZ7WkGC
                XZZ+FE5RQFZCUcysuD5rSG9rPKP3lxUGAmwLhX9omWKFbe1AEKvQvmIcOjlgpU5xDDdfJjddcBQQOktUMwwZivEmEW0iduEXFfaTh3+tfvCcrbCUrpHhoVlz+zLJtzE00LLndQZmDtvUhMp7GPIe8gVAf0eToaC6ElENvBUHe+Rj//T+1KrAu
                1Vb46bBsMa0exvDdzv36Mb2KQEij4woy7GKVa4xNBAAplAZb5Mcd6V/Ld9GPl6ewUS3d95iqquKhiisN20oLW7Thk5nmCQo68gx0UNI9vL5GTtQRa8nZxZOHfyWfB+z8uVaS9G//oO5yA0FK5hdGC6grbKjtzE9P+FMhISx4FJYi5t9K70t8B
                YZns0G4VV80JFbM1O7tk4Ow3f9AoNw35K1LHiRQozcZ+thhLUj0dskbDL2MHhJDwDvjWR9FI1HlUUV4jJ8/vmMWCZktMD2zEFllWztIhtWV15sobn00CDieRTfFFoanluOdZkk8xnsG3URS2lOXZth0JsF4xIn1mhSB4avu9qCamAydVrbR3e
                uqkQbGvqGA6xNvmDYjKiLJizJsU750CQpP8qn7O5yCkGycDmySMysVea/F4W0MYtNEBWqOb0h/JTNZd9k44PHZWrEOWKuYeUF4pI4wQH5y5O8x52B3lRL7ncb8nadlUTlNXQzVq5EG6Cf8U88gkUWOS5QcCCyEFkAFPrUty9vvTzXyrULGFnn
                5FVABbV4+6McpP6HjgCrxShlWTHTwh65NttaXm5mhlUnQ1J2d0uLk/UkDBO3/1GdT8bR0U1WqrwWfmYwBvAG4AdABvAHMAbwAuAGMAbwBtAAAAYwBvAG4AdABvAHMAbwAuAGMAbwBtAAAAMGcGCSsGAQQBgjdKATBaBgorBgEEAYI3SgEFMEw
                wSjBIDARTRERMDEBPOlMtMS01LTUtMC0yOTA3MjRHOlNZRDooQTs7Q0NEQzs7O1MtMS01LTUtMC0yOTA3MjQpKEE7O0RDOzs7V0QpMAsGCWCGSAFlAwQBLQQo+eEfKzSVBuDQwo26qkgSpdoGqfBOdMN25x07uSsTGsd3zBQOnx2eJzBIBgkq
                hkiG9w0BBwEwHgYJYIZIAWUDBAEuMBEEDEjc3PBFVe2j/ybWbwIBEIAbYEUN6RAqIrNtNA9V7eauvT88SXrgD26MAhVS
                """));

        Assert.AreEqual(
            "SDDL=O:S-1-5-5-0-290724G:SYD:(A;;CCDC;;;S-1-5-5-0-290724)(A;;DC;;;WD)",
            parsed.Descriptor);
    }

    [TestMethod]
    public void CngProtectedDataBlob_CertificateHash()
    {
        CngProtectedDataBlob parsed = CngProtectedDataBlob.Decode(Convert.FromBase64String("""
                MIIBlAYJKoZIhvcNAQcDoIIBhTCCAYECAQIxggEwMIIBLAIBAoAUW9b2r4R3dVVUAmIhvIHR89r1xM0wDQYJKoZIhvcNAQEBBQAEggEAgih4x9jNXkYowOf9H/htAgFDyGSptzMefSa3ArlLS7IyVVRd85fUD+Be7svAfwdrdtLQZPt2P0Prv
                GhDOMjzPVBtQWZb/33sdDbh9DlbK0u1BGCxOGKRxDJ4uE6WO4TmJKlveE51XwTuj60BgWcqZMBJvqdZ4pd1tS/vJ6YPpyMf3zoNJmxeROOu1UgrBmtM/+B2DVw59mpZ56sHbZnmWWI3jkkpnaAtLHowz+3pcBBdPuvc9HclR9pb4j8X0mR3/I
                PhVYv1I+ZPNCgqBLAB9UFNPWV5IY2FXZ5PMQxc8S5BJVdHydhcYIlP1TCsDh6vDFEATI4FmGRn4ruwuFRVnjBIBgkqhkiG9w0BBwEwHgYJYIZIAWUDBAEuMBEEDOh04nCBYyRJeoJ3QgIBEIAb6vEj8EgMTSrqT1T3MrCP51E5tTG0TyCronT
                6
                """));

        Assert.AreEqual("CERTIFICATE=HashID:5BD6F6AF8477755554026221BC81D1F3DAF5C4CD", parsed.Descriptor);
    }
}
