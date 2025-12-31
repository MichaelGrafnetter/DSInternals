using System.Security;

namespace DSInternals.Common.Cryptography.Test;

[TestClass]
public class NTHashTester
{
    [TestMethod]
    public void NTHash_SecureString_EmptyInput()
    {
        SecureString password = string.Empty.ToSecureString();
        string result = NTHash.ComputeHash(password).ToHex(true);
        string expected = "31D6CFE0D16AE931B73C59D7E0C089C0";
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void NTHash_String_EmptyInput()
    {
        string result = NTHash.ComputeHash(string.Empty).ToHex(true);
        string expected = "31D6CFE0D16AE931B73C59D7E0C089C0";
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void NTHash_SecureString_NullInput()
    {
        Assert.ThrowsExactly<ArgumentNullException>(() => NTHash.ComputeHash((SecureString)null));
    }

    [TestMethod]
    public void NTHash_String_NullInput()
    {
        Assert.ThrowsExactly<ArgumentNullException>(() => NTHash.ComputeHash((string)null));
    }

    [TestMethod]
    public void NTHash_SecureString_LongInput()
    {
        SecureString password = "012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789".ToSecureString();
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => NTHash.ComputeHash(password).ToHex(true));
    }

    [TestMethod]
    public void NTHash_String_LongInput()
    {
        string password = "012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789";
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => NTHash.ComputeHash(password).ToHex(true));
    }

    [TestMethod]
    public void NTHash_SecureString_TestVector1()
    {
        SecureString password = "Pa$$w0rd".ToSecureString();
        string result = NTHash.ComputeHash(password).ToHex(true);
        string expected = "92937945B518814341DE3F726500D4FF";
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void NTHash_String_TestVector1()
    {
        string result = NTHash.ComputeHash("Pa$$w0rd").ToHex(true);
        string expected = "92937945B518814341DE3F726500D4FF";
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void NTHash_GenerateRandom()
    {
        byte[] randomHash1 = NTHash.GetRandom();
        byte[] randomHash2 = NTHash.GetRandom();

        // Check hash size
        Assert.AreEqual(NTHash.HashSize, randomHash1.Length);

        // Check that the hashes are not the same
        Assert.AreNotEqual(randomHash1.ToHex(), randomHash2.ToHex());
    }
}
