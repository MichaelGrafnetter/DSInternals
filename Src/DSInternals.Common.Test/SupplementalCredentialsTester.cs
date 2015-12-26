using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DSInternals.Common.Data;

namespace DSInternals.Common.Test
{
    [TestClass]
    public class SupplementalCredentialsTester
    {
        [TestMethod]
        public void SupplementalCredentials_Null()
        {
            byte[] input = null;
            var result = new SupplementalCredentials(input);
        }
        [TestMethod]
        public void SupplementalCredentials_Empty1()
        {
            byte[] input = "00000000000000000000000000".HexToBinary();
            var result = new SupplementalCredentials(input);
        }
        [TestMethod]
        public void SupplementalCredentials_Empty2()
        {
            // TODO: Test 111 bytes long empty structure
            throw new AssertInconclusiveException();
        }
    }
}
