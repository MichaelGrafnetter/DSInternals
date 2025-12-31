using System.Security.Cryptography;

namespace DSInternals.Common.Cryptography.Test;

[TestClass]
public class RSAExtensionsTester
{
    [TestMethod]
    public void RSAExtensions_BCrypt_Import()
    {
        byte[] publicKeyBlob = "525341310008000003000000000100000000000000000000010001bf723df58198223d30d10ef3335b1360453a89c57d4b8f0cce3f958f834f50a01a069e3d92ae0de07c92a43df405ac756ffe2c97801e879ced5b0e25e052cebf352c605c36bf87a2cfc16f830abcb5a14ddc3ee282313abe7049c55f2d37164bd050a20c8e5f6cd4b9eddec523836ea8ddf0e94ece5b87a4b6541811312fed6ba0a118e174cca19352c1a0db704b9e789c086fb58543554746f4dfcddd8e5dfea2a548788dc340fd806a6d6ed6f2003b9e1447af6a4040fbb2802d9093c3eb432bb72b8f033887555f60e70b927cb6c1fec2bf17c03fca03b3baa56fb4f2a1ecccd33b6c6afcbb29cb65304e5894fdd77fd3982d1fb2b2aeac6b5451f14a1a8f".HexToBinary();
        Assert.IsTrue(publicKeyBlob.IsBCryptRSAPublicKeyBlob());

        var publicKey = publicKeyBlob.ImportRSAPublicKeyBCrypt();
        Assert.AreEqual(256, publicKey.Modulus.Length);
    }

    [TestMethod]
    public void RSAExtensions_DER_Import()
    {
        byte[] publicKeyBlob = "3082010a0282010100b851c9219527f52e8a51582243e2cca390b634fe5de16b2bca2e225257f3ff20bfe478c98b36095c49d897d42a67e2545d77003d38b9df18682af6fbff281895ce61dadd5f72e13b40da34e47833d380e58175f7d509dfa5e9971068756626af1425b7ce0393bdb28aff8e25cc601de4542672e723b5bbb4e7d3963c2acfb445171b43c14683df0ed6524bd11f583d5bbeebba1de6de3384df598e0d8badacfbf1667890dc72ce61af746084364bc288d982f23a6cd123e9bb6b701e00b096be899876fe93bdd8b1c56fc107f36f7b2c8ce1afb715fcdeca192634be961b6104f21bfd84c97305123ff69d05d685cc8760ce54d9788457882d9dd39afda1d77d0203010001".HexToBinary();
        Assert.IsFalse(publicKeyBlob.IsBCryptRSAPublicKeyBlob());

        RSAParameters publicKey = publicKeyBlob.ImportRSAPublicKeyDER();
        Assert.AreEqual(256, publicKey.Modulus.Length);
    }

    [TestMethod]
    public void RSAExtensions_IsWeakKey()
    {
        // Sample data
        byte[] modulus1 = "bf723df58198223d30d10ef3335b1360453a89c57d4b8f0cce3f958f834f50a01a069e3d92ae0de07c92a43df405ac756ffe2c97801e879ced5b0e25e052cebf352c605c36bf87a2cfc16f830abcb5a14ddc3ee282313abe7049c55f2d37164bd050a20c8e5f6cd4b9eddec523836ea8ddf0e94ece5b87a4b6541811312fed6ba0a118e174cca19352c1a0db704b9e789c086fb58543554746f4dfcddd8e5dfea2a548788dc340fd806a6d6ed6f2003b9e1447af6a4040fbb2802d9093c3eb432bb72b8f033887555f60e70b927cb6c1fec2bf17c03fca03b3baa56fb4f2a1ecccd33b6c6afcbb29cb65304e5894fdd77fd3982d1fb2b2aeac6b5451f14a1a8f".HexToBinary();
        byte[] modulus2 = "976d21dc9a0c0b84040688f5e7f2bb8147b1305ca01cefdb13e9fab49eb6734fd3c32b5d34b01eb6ace35ddf73e62cb506501a5fd1aaab698fb98aea2f2721393c155d84ddf59ef91d8f6402fd755d246c3e04baf96efa04bbc7dd314c083800b934b192ea587904c938255d781ec0b2fe8fa3135f952a13ff805492579ad6710051525a7a824a8a5cba74ef4d3a2f2e271856ff633a411912a53beaa2805a1b57148acc8404b473fd3580f450de5aab10334feb084b6045a65840898a66bf88ae19db802af7fa4aeed95ecdc8ff286ae0075575f82974396b72730c15c511a961bbd6a5a4b46d395aa85f82acbd585ce57dae05ee7b22cbea9e9e02571ef589".HexToBinary();
        byte[] modulus3 = "b11fdb9cc39bb2c09aa244e8794bf60c7c9a7348de93e9f368d4ffa77e6b96bb81898d53da004ba74ec5e5eae8c67d6dbc126863a78c436357a6c0af5af0557e8b1c71319d98cf6ecabed321e0751eac0fcd2808a02152b0d703afe0b54c10132ce981e4088a28110f5a4743b5d5a7862a5eaed28f53f2413ce763bcd823ec81eb225eb6a9a9989006e36a574d3ffdbf62be4bdc00f7014d2e59bfd4077285be88232babfc3aef85e20d8e97c2a94f64902cc86adc3a2c486cc7ca8d0b163deb41f1f66d202382d1c5f7dac30bf9ca6f26538e5e91f3e1cbd8818b58459676588913babd84e1ae0c2ccf5a76326f81b063581468b55b3e015dda17b30a436ca1".HexToBinary();
        byte[] modulus4 = "b851c9219527f52e8a51582243e2cca390b634fe5de16b2bca2e225257f3ff20bfe478c98b36095c49d897d42a67e2545d77003d38b9df18682af6fbff281895ce61dadd5f72e13b40da34e47833d380e58175f7d509dfa5e9971068756626af1425b7ce0393bdb28aff8e25cc601de4542672e723b5bbb4e7d3963c2acfb445171b43c14683df0ed6524bd11f583d5bbeebba1de6de3384df598e0d8badacfbf1667890dc72ce61af746084364bc288d982f23a6cd123e9bb6b701e00b096be899876fe93bdd8b1c56fc107f36f7b2c8ce1afb715fcdeca192634be961b6104f21bfd84c97305123ff69d05d685cc8760ce54d9788457882d9dd39afda1d77d".HexToBinary();
        byte[] exponent = BitConverter.GetBytes(65537);

        // Tests
        var key1 = new RSAParameters() { Modulus = modulus1, Exponent = exponent };
        var key2 = new RSAParameters() { Modulus = modulus2, Exponent = exponent };
        var key3 = new RSAParameters() { Modulus = modulus3, Exponent = exponent };
        var key4 = new RSAParameters() { Modulus = modulus4, Exponent = exponent };

        Assert.IsFalse(key1.IsWeakKey());
        Assert.IsTrue(key2.IsWeakKey());
        Assert.IsFalse(key3.IsWeakKey());
        Assert.IsFalse(key4.IsWeakKey());
    }
}
