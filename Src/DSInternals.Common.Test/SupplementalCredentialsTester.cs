using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DSInternals.Common.Data;

namespace DSInternals.Common.Test
{
    [TestClass]
    public class SupplementalCredentialsTester
    {
        [TestMethod]
        public void SupplementalCredentials_ADAM()
        {
            // AD LDS / ADAM has a slightly different structure of supplemental credentials
            byte[] input = "0100000001000000e80100000600000001000000e00100003100011d000000000000000000000000af4156909297baece4e553b731d9d552da58c12e5880cad54a3f909c07b0792eec9d262783c811ad9cc7aeb1ab019fe0af4156909297baece4e553b731d9d552af4156909297baece4e553b731d9d552ec9d262783c811ad9cc7aeb1ab019fe0da58c12e5880cad54a3f909c07b0792e95e11e920c7405ca6639d932888cc11c53d9e953141b322f29eb1a02798c299042eea90067734b2a81abbc79618cc1519d9aac28e63107a495266e755f7a2babac6b5dfb3bfa3b7039058310d5b36ccc7c97824dd59b9a6a053b5fae3d8603f20a5e2bc3111c759efb3a91e3740633e83b12daad09e83cb92477ac59993c5a843b12daad09e83cb92477ac59993c5a843b12daad09e83cb92477ac59993c5a8471e7228faba51df979beef06b34f791820aa1475f40540f0db59f9d2d93fc3da5f88f6d82d185741a82c7fa79e801c6d8eae592a0ce55271a17de0b39cf1e5e9923354fb99887724cf2b10e88108776b427804b8a6d6ed92c11ecb94e40da72c0c997bc1906b1f0092d61bd0efee428b0c997bc1906b1f0092d61bd0efee428b0c997bc1906b1f0092d61bd0efee428bd93e9a9a7452dff036a67a5d5d333d7c4effe21b0afc142382943ef26024649aaa6a6a95c237f2b8efa2f13ecb0cb220".HexToBinary();
            var result = new SupplementalCredentials(input);
        }

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
