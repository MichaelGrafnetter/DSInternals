using System;
using System.Security.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DSInternals.Common.Cryptography.Test
{
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
    }
}
