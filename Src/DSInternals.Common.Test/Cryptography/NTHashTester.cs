using DSInternals.Common;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Security;

namespace DSInternals.Common.Cryptography.Test
{
    [TestClass]
    public class NTHashTester
    {
        [TestMethod]
        public void NTHash_EmptyInput()
        {
            SecureString password = string.Empty.ToSecureString();
            string result = NTHash.ComputeHash(password).ToHex(true);
            string expected = "31D6CFE0D16AE931B73C59D7E0C089C0";
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NTHash_NullInput()
        {
            NTHash.ComputeHash(null);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void NTHash_LongInput()
        {
            SecureString password = "012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789".ToSecureString();
            string result = NTHash.ComputeHash(password).ToHex(true);
        }
        [TestMethod]
        public void NTHash_TestVector1()
        {
            SecureString password = "Pa$$w0rd".ToSecureString();
            string result = NTHash.ComputeHash(password).ToHex(true);
            string expected = "92937945B518814341DE3F726500D4FF";
            Assert.AreEqual(expected, result);
        }
    }
}
