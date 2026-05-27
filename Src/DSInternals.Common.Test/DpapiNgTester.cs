using System.Text;
using DSInternals.Common.Cryptography;

namespace DSInternals.Common.Test;

[TestClass]
public class DpapiNgTester
{
    [TestMethod]
    public void DpapiNg_RegisterQueryListDeleteDescriptor()
    {
        string name = "DSInternalsTest-" + Guid.NewGuid().ToString("N");
        const string Descriptor = "LOCAL=user";

        try
        {
            DpapiNg.RegisterDescriptor(name, Descriptor);

            Assert.AreEqual(Descriptor, DpapiNg.QueryDescriptor(name));
            Assert.IsTrue(DpapiNg.ListDescriptors().Any(item => item.Key == name && item.Value == Descriptor));
        }
        finally
        {
            DpapiNg.DeleteDescriptor(name);
        }

        Assert.IsFalse(DpapiNg.ListDescriptors().Any(item => item.Key == name));
    }

    [TestMethod]
    public void DpapiNg_ProtectUnprotectSecret_RoundTrip()
    {
        byte[] cleartext = Encoding.UTF8.GetBytes("Secret payload");

        byte[] protectedBlob = DpapiNg.ProtectSecret("LOCAL=user", cleartext);
        byte[]? roundTrip = DpapiNg.UnprotectSecret(protectedBlob);

        CollectionAssert.AreEqual(cleartext, roundTrip);
    }

    [TestMethod]
    public void DpapiNg_ProtectUnprotectSecret_Text_RoundTrip()
    {
        const string Plaintext = "Secret payload";

        byte[] protectedBlob = DpapiNg.ProtectSecret("LOCAL=user", Encoding.Unicode.GetBytes(Plaintext));
        string? roundTrip = DpapiNg.UnprotectSecret(protectedBlob, Encoding.Unicode);

        Assert.AreEqual(Plaintext, roundTrip);
    }

    [TestMethod]
    public void DpapiNg_UnprotectSecret_EmptyBlob()
    {
        Assert.IsNull(DpapiNg.UnprotectSecret(Array.Empty<byte>()));
        Assert.IsNull(DpapiNg.UnprotectSecret(Array.Empty<byte>(), Encoding.Unicode));
    }
}
