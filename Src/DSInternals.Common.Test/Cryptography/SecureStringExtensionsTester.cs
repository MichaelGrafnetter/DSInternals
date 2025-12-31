using System.Security;
using System.Text;

namespace DSInternals.Common.Cryptography.Test;

[TestClass]
public class SecureStringExtensionsTester
{
    [TestMethod]
    public void SecureStringExtensions_Conversion_TestVector1()
    {
        string pwd = "Pa$$w0rd";
        string result = pwd.ToSecureString().ToUnicodeString();
        Assert.AreEqual(pwd, result);
    }
    [TestMethod]
    public void SecureStringExtensions_ToSecureString_EmptyInput()
    {
        string pwd = string.Empty;
        SecureString result = pwd.ToSecureString();
        Assert.AreEqual(0, result.Length);
    }
    [TestMethod]
    public void SecureStringExtensions_ToSecureString_NullInput()
    {
        string pwd = null;
        SecureString result = pwd.ToSecureString();
        Assert.IsNull(result);
    }
    [TestMethod]
    public void SecureStringExtensions_ToSecureString_ReadOnlyTest()
    {
        SecureString pwd = "Pa$$w0rd".ToSecureString();
        Assert.ThrowsExactly<InvalidOperationException>(() => pwd.AppendChar('c'));
    }
    [TestMethod]
    public void SecureStringExtensions_Append_Test1()
    {
        SecureString pwd = "Pa$$w0rd".ToSecureString().Copy();
        pwd.Append("Suffix");
        Assert.AreEqual("Pa$$w0rdSuffix", pwd.ToUnicodeString());
    }
    [TestMethod]
    public void SecureStringExtensions_Append_NullInput()
    {
        SecureString pwd = "Pa$$w0rd".ToSecureString().Copy();
        pwd.Append(null);
        Assert.AreEqual("Pa$$w0rd", pwd.ToUnicodeString());
    }
    [TestMethod]
    public void SecureStringExtensions_Append_EmptyInput()
    {
        SecureString pwd = "Pa$$w0rd".ToSecureString().Copy();
        pwd.Append(string.Empty);
        Assert.AreEqual("Pa$$w0rd", pwd.ToUnicodeString());
    }

    [TestMethod]
    public void SecureStringExtensions_ToByteArray_TestInput1()
    {
        string input = "Pa$$w0rd";
        byte[] result = input.ToSecureString().ToByteArray();
        string resultString = UnicodeEncoding.Unicode.GetString(result);
        Assert.AreEqual(input, resultString);
    }
    [TestMethod]
    public void SecureStringExtensions_ToByteArray_EmptyInput()
    {
        SecureString input = new SecureString();
        byte[] output = input.ToByteArray();
        byte[] expected = new byte[0];
        CollectionAssert.AreEqual(expected, output);
    }
}
