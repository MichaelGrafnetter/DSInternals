using System.Security;

namespace DSInternals.Common.Cryptography.Test;

[TestClass]
public class LMHashTester
{
    [TestMethod]
    public void LMHash_EmptyInput()
    {
        SecureString password = string.Empty.ToSecureString();
        string result = LMHash.ComputeHash(password).ToHex(true);
        string expected = "AAD3B435B51404EEAAD3B435B51404EE";
        Assert.AreEqual(expected, result);
    }
    [TestMethod]
    public void LMHash_NullInput()
    {
        Assert.ThrowsExactly<ArgumentNullException>(() => LMHash.ComputeHash(null));
    }
    [TestMethod]
    public void LMHash_LongInput()
    {
        // LM Hash history contains aad3b435b51404eeaad3b435b51404ee, hash of empty string
        SecureString password = "17CharsLongString".ToSecureString();
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => LMHash.ComputeHash(password).ToHex(true));
    }
    [TestMethod]
    public void LMHash_UnicodeInput()
    {
        // Taken from AD, so the correctness of the expected result is guaranteed
        SecureString password = "♠".ToSecureString();
        string result = LMHash.ComputeHash(password).ToHex(false);
        string expected = "5a5503d0e85f58abaad3b435b51404ee";
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void LMHash_InputWithDiacriticalMarks()
    {
        // Taken from AD, so the correctness of the expected result is guaranteed
        SecureString password = "Č".ToSecureString();
        string result = LMHash.ComputeHash(password).ToHex(false);
        string expected = "f9393d97e7a1873caad3b435b51404ee";
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void LMHash_TestVector1()
    {
        SecureString password = "Pa$$w0rd".ToSecureString();
        string result = LMHash.ComputeHash(password).ToHex(true);
        string expected = "727E3576618FA1754A3B108F3FA6CB6D";
        Assert.AreEqual(expected, result);
    }
}
