using DSInternals.Common;

namespace DSInternals.DataStore.Test;

[TestClass]
public class BootKeyRetrieverTester
{
    [TestMethod]
    public void BootKeyRetriever_Hive1()
    {
        throw new AssertInconclusiveException("There are no test registry hives available yet.");
    }
    [TestMethod]
    public void BootKeyRetriever_NonExistingFile()
    {
        Assert.ThrowsExactly<FileNotFoundException>(() => BootKeyRetriever.GetBootKey(@"C:\xxxxxx"));
    }
    [TestMethod]
    public void BootKeyRetriever_NullFile()
    {
        Assert.ThrowsExactly<ArgumentNullException>(() => BootKeyRetriever.GetBootKey(null));
    }
    [TestMethod]
    public void BootKeyRetriever_NotRegistryHiveFile()
    {
        throw new AssertInconclusiveException();
    }
    [TestMethod]
    public void BootKeyRetriever_NotSystemHiveFile()
    {
        throw new AssertInconclusiveException();
    }

    [TestMethod]
    public void BootKeyRetriever_Online()
    {
        byte[] bootKey = BootKeyRetriever.GetBootKey();
        // Just test that the key has 16B.
        Assert.AreEqual(BootKeyRetriever.BootKeyLength, bootKey.Length);
    }

    [TestMethod]
    public void BootKeyRetriever_LDS()
    {
        // AD LDS/ADAM
        byte[] rootObjectPekList = "e2b95102f97b7528a7e2477a2406438f97974fabd412be91aca18a2c241d513482a51553d3f28b26".HexToBinary();
        byte[] schemaNCPekList = "cb6ef0da6e2069f735b8211ee6071fb206ba4ade0d048e4b279decdc174747bb55ee46a321796c8a".HexToBinary();

        byte[] bootKey = BootKeyRetriever.GetBootKey(rootObjectPekList, schemaNCPekList);
        string expected = "51f9a1e2282c7b7a79f0ba210d1e8ef7";

        Assert.AreEqual(expected, bootKey.ToHex(false));
    }
}
