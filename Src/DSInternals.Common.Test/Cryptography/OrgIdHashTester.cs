using DSInternals.Common;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Security;

namespace DSInternals.Common.Cryptography.Test
{
    [TestClass]
    public class OrgIdHashTester
    {
        [TestMethod]
        public void OrgIdHash_TestVector1()
        {
            SecureString password = "Pa$$w0rd".ToSecureString();
            byte[] salt = "317ee9d1dec6508fa510".HexToBinary();
            string result = OrgIdHash.ComputeFormattedHash(password, salt);
            string expected = "v1;PPH1_MD4,317ee9d1dec6508fa510,100,f4a257ffec53809081a605ce8ddedfbc9df9777b80256763bc0a6dd895ef404f;";
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void OrgIdHash_NullInput()
        {
            SecureString password = null;
            byte[] salt = "317ee9d1dec6508fa510".HexToBinary();
            string result = OrgIdHash.ComputeFormattedHash(password, salt);
        }
        [TestMethod]
        public void OrgIdHash_EmptyInput()
        {
            SecureString password = string.Empty.ToSecureString();
            byte[] salt = "317ee9d1dec6508fa510".HexToBinary();
            string result = OrgIdHash.ComputeFormattedHash(password, salt);
            string expected = "v1;PPH1_MD4,317ee9d1dec6508fa510,100,f56b3637eb57e927438c6d0ebbd69d1d95f8e849912915a6733a33921c8e4806;";
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
}
