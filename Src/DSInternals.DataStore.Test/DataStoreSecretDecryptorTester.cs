using DSInternals.Common;
using DSInternals.Common.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;

namespace DSInternals.DataStore.Test
{
    [TestClass]
    public class DataStoreSecretDecryptorTester
    {
        [TestMethod]
        public void PasswordEncryptionKey_DataStoreDecryptPEK()
        {
            byte[] encryptedPEK = "020000000100000042b1f49dbb723edff3b865a4d28e3afbf215961695225991e991d429a02ad382bd89214319f61e7eb4620e89b42ddba3d0de84c0603d6e34ae2fccf79eb9374a9a08d3b1".HexToBinary();
            byte[] bootKey = "41e34661faa0d182182f6ddf0f0ca0d1".HexToBinary();
            var pek = new DataStoreSecretDecryptor(encryptedPEK, bootKey);
            string expected = "04b7b3fd6df689af9d6837e840abdc8c";
            Assert.AreEqual(expected, pek.CurrentKey.ToHex());
        }
        [TestMethod]
        public void PasswordEncryptionKey_DataStoreNTHash()
        {
            byte[] blob = "1100000000000000133c2e574dfc2df435671649180617cfb3cc9ef487c99b1d6cda3fb410a021f5".HexToBinary();
            byte[] binaryPek = "56d98148ec91d111905a00c04fc2d4cfb0b0f777efcece0100000000010000000000000004b7b3fd6df689af9d6837e840abdc8c".HexToBinary();
            var pek = new DataStoreSecretDecryptor(binaryPek);
            int rid = 500;

            string result = pek.DecryptHash(blob, rid).ToHex(true);
            string expected = "92937945B518814341DE3F726500D4FF";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void PasswordEncryptionKey_DataStoreNTHashHistory()
        {
            byte[] binaryPek = "56d98148ec91d111905a00c04fc2d4cfb0b0f777efcece0100000000010000000000000004b7b3fd6df689af9d6837e840abdc8c".HexToBinary();
            var pek = new DataStoreSecretDecryptor(binaryPek);
            byte[] blob = "1100000000000000b9cc6e0358210d60e5f9233a47d4053ccf16b320eec132d7de81b13dace6f4e7".HexToBinary();
            int rid = 1375;
            
            var result = pek.DecryptHashHistory(blob, rid);
            Assert.AreEqual(1, result.Length);
            Assert.AreEqual("92937945B518814341DE3F726500D4FF", result[0].ToHex(true));
        }
    }
}
