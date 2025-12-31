namespace DSInternals.Common.Cryptography.Test;
[TestClass]
public class WDigestHashTester
{
    [TestMethod]
    public void WDigestHash_NullInput()
    {
        Assert.ThrowsExactly<ArgumentNullException>(() => WDigestHash.ComputeHash(null, null, null, null, null));
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

    [TestMethod]
    public void WDigestHash_ParseProperty()
    {
        byte[] blob = "3100011D00000000000000000000000038723ED34D6DF84FB88972CE00B9D28952E954B1F92368D40FB88C9B54DF2D5AE275AEEBE6D0A0DA7B0572F719BB307B38723ED34D6DF84FB88972CE00B9D28952E954B1F92368D40FB88C9B54DF2D5AFE93C6CC61028BDD93B74042ED778C2338723ED34D6DF84FB88972CE00B9D289429072BFFA5BE07598CA43AE52833732429072BFFA5BE07598CA43AE528337320749C5CCFC3FCD9A23F8EE373FE0DAEC55113516DFD326403029BBC568B2FA4A429072BFFA5BE07598CA43AE52833732D04622762DF13DB90786B72C423017DB55113516DFD326403029BBC568B2FA4AC35F5A94C1F7BD9C0B654DA4526F2DA5C35F5A94C1F7BD9C0B654DA4526F2DA59C200D20FA1D18F814BD491108E3F5440784E0DD92BED5304A4589BC6751DD8C148DC7BE0270C46539CBC9A6C3509E2691F52DC6ECBF3CF2B1D802EBD9E56319D09E4A62CCF638C2B8AD1360BEBD11CED09E4A62CCF638C2B8AD1360BEBD11CEDB565E643D18D2C3BB4F3DA942B27DDC56D0DA9761F9E1BD60332B365A10677256D0DA9761F9E1BD60332B365A1067725F8A9A18636D788F56C432A6E51FE884FBB9BB6939D39D22E17DCCFFBAB6F314FC31AFC0E0C72839C51F5647D1582B83AECD3707827166AE2D55DA8A4F2A8D8B".HexToBinary();
        byte[][] hashes = WDigestHash.Parse(blob);
        Assert.AreEqual(WDigestHash.HashCount, hashes.Length);
    }

    [TestMethod]
    public void WDigestHash_Compute_LDS()
    {
        // Property value taken from AD LDS
        string expectedBlob = "3100011d000000000000000000000000af4156909297baece4e553b731d9d552da58c12e5880cad54a3f909c07b0792eec9d262783c811ad9cc7aeb1ab019fe0af4156909297baece4e553b731d9d552af4156909297baece4e553b731d9d552ec9d262783c811ad9cc7aeb1ab019fe0da58c12e5880cad54a3f909c07b0792e95e11e920c7405ca6639d932888cc11c53d9e953141b322f29eb1a02798c299042eea90067734b2a81abbc79618cc1519d9aac28e63107a495266e755f7a2babac6b5dfb3bfa3b7039058310d5b36ccc7c97824dd59b9a6a053b5fae3d8603f20a5e2bc3111c759efb3a91e3740633e83b12daad09e83cb92477ac59993c5a843b12daad09e83cb92477ac59993c5a843b12daad09e83cb92477ac59993c5a8471e7228faba51df979beef06b34f791820aa1475f40540f0db59f9d2d93fc3da5f88f6d82d185741a82c7fa79e801c6d8eae592a0ce55271a17de0b39cf1e5e9923354fb99887724cf2b10e88108776b427804b8a6d6ed92c11ecb94e40da72c0c997bc1906b1f0092d61bd0efee428b0c997bc1906b1f0092d61bd0efee428b0c997bc1906b1f0092d61bd0efee428bd93e9a9a7452dff036a67a5d5d333d7c4effe21b0afc142382943ef26024649aaa6a6a95c237f2b8efa2f13ecb0cb220";
        string password = @"Pa$$w0rd";
        string userDN = "CN=john,DC=dsinternals,DC=com";
        string namingContext = "DC=dsinternals,DC=com";

        // Compute and compare
        byte[][] hashes = WDigestHash.ComputeHash(password.ToSecureString(), userDN, namingContext);
        byte[] blob = WDigestHash.Encode(hashes);
        Assert.AreEqual(expectedBlob, blob.ToHex());
    }

    [TestMethod]
    public void WDigestHash_EncodeProperty()
    {
        byte[] blob = "3100011D00000000000000000000000038723ED34D6DF84FB88972CE00B9D28952E954B1F92368D40FB88C9B54DF2D5AE275AEEBE6D0A0DA7B0572F719BB307B38723ED34D6DF84FB88972CE00B9D28952E954B1F92368D40FB88C9B54DF2D5AFE93C6CC61028BDD93B74042ED778C2338723ED34D6DF84FB88972CE00B9D289429072BFFA5BE07598CA43AE52833732429072BFFA5BE07598CA43AE528337320749C5CCFC3FCD9A23F8EE373FE0DAEC55113516DFD326403029BBC568B2FA4A429072BFFA5BE07598CA43AE52833732D04622762DF13DB90786B72C423017DB55113516DFD326403029BBC568B2FA4AC35F5A94C1F7BD9C0B654DA4526F2DA5C35F5A94C1F7BD9C0B654DA4526F2DA59C200D20FA1D18F814BD491108E3F5440784E0DD92BED5304A4589BC6751DD8C148DC7BE0270C46539CBC9A6C3509E2691F52DC6ECBF3CF2B1D802EBD9E56319D09E4A62CCF638C2B8AD1360BEBD11CED09E4A62CCF638C2B8AD1360BEBD11CEDB565E643D18D2C3BB4F3DA942B27DDC56D0DA9761F9E1BD60332B365A10677256D0DA9761F9E1BD60332B365A1067725F8A9A18636D788F56C432A6E51FE884FBB9BB6939D39D22E17DCCFFBAB6F314FC31AFC0E0C72839C51F5647D1582B83AECD3707827166AE2D55DA8A4F2A8D8B".HexToBinary();
        byte[][] hashes = WDigestHash.Parse(blob);

        byte[] newBlob = WDigestHash.Encode(hashes);
        Assert.AreEqual(blob.ToHex(), newBlob.ToHex());
    }
}
