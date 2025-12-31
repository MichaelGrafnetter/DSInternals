using System.Security;

namespace DSInternals.Common.Cryptography.Test;

[TestClass]
public class OrgIdHashTester
{
    [TestMethod]
    public void OrgIdHash_TestVector1()
    {
        SecureString password = "Pa$$w0rd".ToSecureString();
        byte[] salt = "181a3024085fcee2f70e".HexToBinary();
        string result = OrgIdHash.ComputeFormattedHash(password, salt);
        string expected = "v1;PPH1_MD4,181a3024085fcee2f70e,1000,b39525c3bc72a1136fcf7c8a338e0c14313d0450d1a4c98ef0a6ddada3bc5b0a;";
        Assert.AreEqual(expected, result);
    }
    [TestMethod]
    public void OrgIdHash_NullInput()
    {
        SecureString password = null;
        byte[] salt = "317ee9d1dec6508fa510".HexToBinary();
        Assert.ThrowsExactly<ArgumentNullException>(() => OrgIdHash.ComputeFormattedHash(password, salt));
    }
    [TestMethod]
    public void OrgIdHash_EmptyInput()
    {
        SecureString password = string.Empty.ToSecureString();
        byte[] salt = "01cda06eceb9d9bc2621".HexToBinary();
        string result = OrgIdHash.ComputeFormattedHash(password, salt);
        string expected = "v1;PPH1_MD4,01cda06eceb9d9bc2621,1000,9d4fc778add44776555d3fa6ccb4f9637f25e34a62dbc5fa0f782ef8c762c902;";
        Assert.AreEqual(expected, result);
    }
    [TestMethod]
    public void OrgIdHash_SaltLength()
    {
        byte[] salt = OrgIdHash.GenerateSalt();
        Assert.AreEqual(OrgIdHash.SaltSize, salt.Length);
    }
    [TestMethod]
    public void OrgIdHash_HashLength()
    {
        SecureString pwd = "Pa$$w0rd".ToSecureString();
        byte[] salt = OrgIdHash.GenerateSalt();
        byte[] hash = OrgIdHash.ComputeHash(pwd, salt);
        Assert.AreEqual(OrgIdHash.HashSize, hash.Length);
    }
}
