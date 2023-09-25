using DSInternals.Common.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DSInternals.Common.Test
{
    [TestClass]
    public class ProtectionKeyIdentifierTester
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ProtectionKeyIdentifier_Null()
        {
            var obj = new ProtectionKeyIdentifier(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ProtectionKeyIdentifier_Empty()
        {
            var obj = new ProtectionKeyIdentifier(new byte[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ProtectionKeyIdentifier_Truncated1()
        {
            // Only contains version
            byte[] blob = "01000000".HexToBinary();
            var parsed = new ProtectionKeyIdentifier(blob);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ProtectionKeyIdentifier_Truncated2()
        {
            // The last byte has been trimmed
            byte[] blob = "010000004b44534b02000000690100001a00000018000000965cc97d85fa3a18dff5f70696bf0b1100000000180000001800000063006f006e0074006f0073006f002e0063006f006d00000063006f006e0074006f0073006f002e0063006f006d0000".HexToBinary();
            var parsed = new ProtectionKeyIdentifier(blob);
        }

        [TestMethod]
        public void ProtectionKeyIdentifier_Parse()
        {
            byte[] blob = "010000004b44534b02000000690100001a00000018000000965cc97d85fa3a18dff5f70696bf0b1100000000180000001800000063006f006e0074006f0073006f002e0063006f006d00000063006f006e0074006f0073006f002e0063006f006d000000".HexToBinary();
            var parsed = new ProtectionKeyIdentifier(blob);
            Assert.AreEqual("contoso.com", parsed.DomainName);
            Assert.AreEqual("contoso.com", parsed.ForestName);
            Assert.AreEqual("7dc95c96-fa85-183a-dff5-f70696bf0b11", parsed.RootKeyId.ToString());
            Assert.AreEqual(361, parsed.L0KeyId);
            Assert.AreEqual(26, parsed.L1KeyId);
            Assert.AreEqual(24, parsed.L2KeyId);
        }
    }
}
