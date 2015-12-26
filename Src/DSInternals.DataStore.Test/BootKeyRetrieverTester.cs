using DSInternals.Common;
using DSInternals.DataStore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Security;
using System.Text;

namespace DSInternals.DataStore.Test
{
    [TestClass]
    public class BootKeyRetrieverTester
    {
        [TestMethod]
        public void BootKeyRetriever_Hive1()
        {
            // HACK: Use relative path.
            string path = @"C:\Users\michael\Source\Workspaces\Workspace\DSInternals\TestData\IFM\registry\SYSTEM";
            string bootKey = BootKeyRetriever.GetBootKey(path).ToHex();
            string expected = "41e34661faa0d182182f6ddf0f0ca0d1";
            Assert.AreEqual(expected, bootKey);
        }
        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void BootKeyRetriever_NonExistingFile()
        {
            BootKeyRetriever.GetBootKey(@"C:\xxxxxx");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void BootKeyRetriever_NullFile()
        {
            BootKeyRetriever.GetBootKey(null);
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
            Assert.AreEqual(BootKeyRetriever.BootKeyLength, bootKey.Length);
        }
    }
}
