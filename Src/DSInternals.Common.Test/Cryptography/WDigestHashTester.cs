namespace DSInternals.Common.Cryptography.Test
{
    using DSInternals.Common;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

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
        public void WDigestHash_ASCIIInput()
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
        [TestMethod]
        public void WDigestHash_UnicodeInput()
        {
            var hashes = WDigestHash.ComputeHash(@"Příliš žluťoučký kůň úpěl ďábelské ódy. В чащах юга жил бы цитрус? Да, но фальшивый экземпляр!".ToSecureString(), "April@adatum.com", "April", "ADATUM", "Adatum.com");
            Assert.AreEqual(WDigestHash.HashCount, hashes.Length);
            Assert.AreEqual("fad613ac9301e7ae6dcc537e2eef5021", hashes[0].ToHex());
            Assert.AreEqual("f3b76b7db452d6be096942f9bcce74ea", hashes[1].ToHex());
            Assert.AreEqual("919952d85318a3d5b67e9df5a78b7141", hashes[2].ToHex());
            Assert.AreEqual("fad613ac9301e7ae6dcc537e2eef5021", hashes[3].ToHex());
            Assert.AreEqual("35dd0ca1e7631567f2ae132e038ccd8f", hashes[4].ToHex());
            Assert.AreEqual("f6d89b7867a2d3d1cbae038091bdb0cc", hashes[5].ToHex());
            Assert.AreEqual("0bfedf219f856e2edcfec5231470c0b3", hashes[6].ToHex());
            Assert.AreEqual("df33d4e6eb9af1406b25e0e16548a2da", hashes[7].ToHex());
            Assert.AreEqual("5e2d43bdbb743c5d2601baafdd32a1af", hashes[8].ToHex());
            Assert.AreEqual("5277f1cf72b11b2ff48721cf9d431893", hashes[9].ToHex());
            Assert.AreEqual("9681f457b0767ed163b8dc8526469af6", hashes[10].ToHex());
            Assert.AreEqual("3e9ce47f72f7e39661199f18ee8bc2aa", hashes[11].ToHex());
            Assert.AreEqual("404243dbcec9a1d73d0b1cb49060c196", hashes[12].ToHex());
            Assert.AreEqual("29d93dc099bd1b5c9f20bbeb042bb4ff", hashes[13].ToHex());
            Assert.AreEqual("0ecdf90500b3189dc3082dd1bce0b5bc", hashes[14].ToHex());
            Assert.AreEqual("4a4135a2ac1daa81fea605adc9188c67", hashes[15].ToHex());
            Assert.AreEqual("0a1f7bd410a8340342045daf0cc78789", hashes[16].ToHex());
            Assert.AreEqual("be97dc768a7683e839438b29e30b6f7b", hashes[17].ToHex());
            Assert.AreEqual("7d6a36f69323172b25ef61a761f29d1b", hashes[18].ToHex());
            Assert.AreEqual("ed2d15fcde27c04892014aaf46201ec8", hashes[19].ToHex());
            Assert.AreEqual("51f43cb00d57226f530d52908a072c43", hashes[20].ToHex());
            Assert.AreEqual("35329ba52771477b8665ef47b89bebbf", hashes[21].ToHex());
            Assert.AreEqual("7515013a7da75b573467e521abb783e1", hashes[22].ToHex());
            Assert.AreEqual("9bab42c45f29cda4f4dadad0a14cc8b6", hashes[23].ToHex());
            Assert.AreEqual("26075b3e4791600800363160d217265b", hashes[24].ToHex());
            Assert.AreEqual("bbba87b75b45d71cf7fd84d4e2a22344", hashes[25].ToHex());
            Assert.AreEqual("18ca7497b2624e7fde7165854788dd49", hashes[26].ToHex());
            Assert.AreEqual("0d9a52cbff395f8df9e285b01c2a78fb", hashes[27].ToHex());
            Assert.AreEqual("c4280744f0ff1181b939b71b1ddc37f5", hashes[28].ToHex());
        }
    }
}