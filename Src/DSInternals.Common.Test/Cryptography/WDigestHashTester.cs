namespace DSInternals.Common.Cryptography.Test
{
    using DSInternals.Common;
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Security;

    [TestClass]
    public class WDigestHashTester
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WDigestHash_NullInput()
        {
            var hashes = WDigestHash.ComputeHash(null, null, null, null, null);
        }
        
        [TestMethod]
        public void WDigestHash_TestVector1()
        {
            var hashes = WDigestHash.ComputeHash(@"Pa$$w0rd".ToSecureString(), null, "Administrator", "ADATUM", "Adatum.com");
            Assert.AreEqual(WDigestHash.HashCount, hashes.Length);
            Assert.AreEqual("3f602ffdac6b06655481a06cce08adf3", hashes[0].ToHex());
            Assert.AreEqual("971256a81b883974fcbcf369f67ecf61", hashes[1].ToHex());
            Assert.AreEqual("ec3d57e1caf31b9cd0fc1e3bf913bbf6", hashes[2].ToHex());
            Assert.AreEqual("3f602ffdac6b06655481a06cce08adf3", hashes[3].ToHex());
            Assert.AreEqual("30c8584ec6a52de03fead6e66443b896", hashes[4].ToHex());
            Assert.AreEqual("da9aab75d2ad8152daedc92aea4e49ac", hashes[5].ToHex());
            Assert.AreEqual("eb781ca6bf2d4f3a30bc8f8ba360b1a0", hashes[6].ToHex());
            Assert.AreEqual("b70f192a3ee11e47bc1408e18835210e", hashes[7].ToHex());
            Assert.AreEqual("afebaf56c0b704ab12276306c3524d9f", hashes[8].ToHex());
            Assert.AreEqual("a4d5b0be4785b495b4b53e70c35d4990", hashes[9].ToHex());
            Assert.AreEqual("1167280ac9a9bc1ec1b49d171a8e9b62", hashes[10].ToHex());
            Assert.AreEqual("e17ff031d74e32e67ec1f1b6e0b2c26b", hashes[11].ToHex());
            Assert.AreEqual("379c029a72f4d0e649e7aa6ad659dbe0", hashes[12].ToHex());
            Assert.AreEqual("f7fcfe4c5ec50f9db38577cea6b99f28", hashes[13].ToHex());
            Assert.AreEqual("ffde532cbd677b5562ce4455099cdafd", hashes[14].ToHex());
            Assert.AreEqual("478561ada3cecab0094ea704b90b1c84", hashes[15].ToHex());
            Assert.AreEqual("51ca360a2a2d35b184a0c78fd0432137", hashes[16].ToHex());
            Assert.AreEqual("f7841c8b57a9a65875ee40a39ba270cb", hashes[17].ToHex());
            Assert.AreEqual("3b8dc2b699160e41ce1c8f1cdacd0c2c", hashes[18].ToHex());
            Assert.AreEqual("132d33c113262bd560b40c39d1cac291", hashes[19].ToHex());
            Assert.AreEqual("8b8c4f339d3736ede2c4c0bc4e53f3ff", hashes[20].ToHex());
            Assert.AreEqual("209ae3b1dc6789f90f352e2ff273111b", hashes[21].ToHex());
            Assert.AreEqual("9b22dae245b82c3e55061265b288607c", hashes[22].ToHex());
            Assert.AreEqual("bb1edeb4c9b818d829800192a1528a88", hashes[23].ToHex());
            Assert.AreEqual("f78b1d41fe8372506e31461625743eb4", hashes[24].ToHex());
            Assert.AreEqual("dd829e0cda63ba68f95f98b1750885f6", hashes[25].ToHex());
            Assert.AreEqual("177eeed7b3cf4df4e114c4ca91beafcd", hashes[26].ToHex());
            Assert.AreEqual("9c0d49ddf1ac7cff9d1e6f96cc8a55d5", hashes[27].ToHex());
            Assert.AreEqual("8da34a0b94604b06cd58f9bf69142383", hashes[28].ToHex());
        }
    }
}