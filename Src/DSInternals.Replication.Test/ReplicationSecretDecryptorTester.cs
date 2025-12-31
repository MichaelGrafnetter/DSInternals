using System.Text;
using DSInternals.Common;
using DSInternals.Common.Data;

namespace DSInternals.Replication.Test;

[TestClass]
public class ReplicationSecretDecryptorTester
{
    [TestMethod]
    public void PasswordEncryptionKey_ReplicationNTHash()
    {
        byte[] sessionKey = "b0133bfc9ce59c805dd15d5872e247c5".HexToBinary();
        var pek = new ReplicationSecretDecryptor(sessionKey);

        byte[] blob = "e650e0179becf540e1e6dbe37deb38b379618228d74d9ff7e08a588c2a6fbd511ad78f61".HexToBinary();
        int rid = 500;
        string result = pek.DecryptHash(blob, rid).ToHex(true);
        string expected = "92937945B518814341DE3F726500D4FF";
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void PasswordEncryptionKey_ReplicationLMHash()
    {
        byte[] sessionKey = "7dfee67fc2476e6bc1b68563e5ee7614".HexToBinary();
        var pek = new ReplicationSecretDecryptor(sessionKey);
        byte[] blob = "fd2db3a4edba8fb08cf372b701449c1c28ddb6fcd70226fa571d490f9798f30efe6454fc".HexToBinary();
        int rid = 35093;
        string result = pek.DecryptHash(blob, rid).ToHex(false);
        string expected = "727e3576618fa1754a3b108f3fa6cb6d";
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void PasswordEncryptionKey_ReplicationNTHashHistory()
    {
        byte[] sessionKey = "f029c008decee208f36450cc1f767892".HexToBinary();
        var pek = new ReplicationSecretDecryptor(sessionKey);
        byte[] blob = "491e1b5e8ff95982ccedc4ff2c50ad5fe154650df83e8430ede739271da2e03d9aa99aa8d4b598ef56c9650eb71a233a9fd580f4d9c2dbc33f92e0002e1d2901050fb5d7f66ca3555e5befbf322cd23c1eeaaf7c".HexToBinary();
        int rid = 1375;
        var result = pek.DecryptHashHistory(blob, rid);
        Assert.AreEqual(4, result.Length);
        Assert.AreEqual("a4ff9743bdda4849cb2108d2ceb5c5b9", result[0].ToHex(false));
        Assert.AreEqual("185418d4b03fb6cfe90e71402220e807", result[1].ToHex(false));
        Assert.AreEqual("4fed6755410545c28b519f30e02d54a2", result[2].ToHex(false));
        Assert.AreEqual("92937945b518814341de3f726500d4ff", result[3].ToHex(false));
    }

    [TestMethod]
    public void PasswordEncryptionKey_ReplicationSupplementalCredDecrypt()
    {
        byte[] sessionKey = "62a65b7b549b2bf5f5426feec9cf9536".HexToBinary();
        var pek = new ReplicationSecretDecryptor(sessionKey);
        var sb = new StringBuilder();
        sb.Append("ba85a50b74cd4bf5dc44b0f475d0529b8a2f3952ad159b4cadfcd7708b0dd4d192afcac2d09f99608c65609efc7007c190f029d31583b821e7f36c94203d1");
        sb.Append("ce5a9e975a16efed71e17886028ea898e9299a18a010394b080742ff520f7f160ed71898b1b4a2f21b9579645c66fe0c14a689");
        sb.Append("c05651fe81c01c3fcd86a7669ff2552fc94598ae53d2011c372054f1b30b4a48fbdf466fb51c7e0a96db5d025144470d2a79fb20dd86dbaad6745929d4a1335e0920b4b4fa200cad");
        sb.Append("866390adf148e7e0c13d586b35478650dce0c6297842f4c81c8cad3446600c4d8be60a657a17b6191f5894e04f695faee579acdc0e7adc40bf1c7efadaffd3eaf1ccaf98b862d3a25e1cd0670cec56a3e46b88b606e949cf2f96e442cb9268f038a4795");
        sb.Append("1d261a86a9c4298af7c5b7cf9905961e52ac1796dc48c54bc3c454b952738c581b92a6a1c14d56c3f1bbe167d2783cf944ae10fb6e59ba58022259e750daeb0deec376670790d35b4db851e025c898950df33f16b9abfb9b15804d54a4e7d7c9a63193dba2327341abdbae78a8cb6614b9bc3f48968153b1e10ad493b8855001d95a25eda8beee35b972249a1cfb6f07485959175a72d79756b6413a4e6e0b5129b909c57fb75f828571ea692f1cb213db0e60fcc728da1a91ea8f9415fdfcc6348a60156ef785cbea8d87b36869305bb381db661009b287ae8371448606b2aa874882d3534a454933f4c806dc3f059c12736a9c176feea59c8e45bc7c0a1e7f1b3e608625feede0f6f6b0e89ad7419bb27718f276486068c7aeaa78c1b3d9cdf81330a970e7e3d2e41c88d1abeb53315a676d051a25e17480de55b2eb65e8ec6bf521d4471a55ff0efbb515568f57ec60afe283f1a6b7cc852a5aac0e9d9d94190d437c948b3feba26841cfe0b1b8d694bfc1b06a920e1cb8fa45fba4fa15c11b56dd6d6119b43553f9dd51b5cce619699824381b5c22b90a71a0dc712e5850e52bbab94cd1c6ab5eb496ac92a36deed84bf5a95c36ab31e752e60a3f239255bff07aa5c4a05210fac255808fc8e723e6f8492c286198b55a1ac7438e17f39129f883c63868b9b57a74ce8d208819bc2990c2ab9d98f580429814d0b66b0e11a04e7ef179397dc3af2d5bb1f072f11d4053e32bc91cc48049553e6c6e03ee8d6d619875943ec5aa9a1c079d50f09db3ae8b61ad945e811dcd23462152e2f21753a2c24435332ca87be7e2378270fe48e5a5dd7460f9b01c16a9e20278f5affa4ac16a082cfb8f25350e5f774465e615a125b5818f2cf19474736662ad81248a8132711105bb8e87c35fc04847a4f9b2ee59151f1482f0864a06e233579d5e53cc331e702d51de656af4826a39f2386110a09ce0c497d8c8bb7c430d3a77894511fa5d41bb2073efa016adb121cb1d95bc62b3e0654c5ab7ce8dce14ed8242739d6c3a15473b3fe5766d76e658556debb86c8981c520a725bbe3655ba4bf482e327983bc0f116fbe53fcf29ec7e6c5f5b23a11a459fe55c7b9a0f51808e4396624c56a8435d95b73cf13f9cfc35229b1750975c654fc61a779d634e4e3da67544a319be55028e4934a38e5df5ab4b554a0ac37969da37045f8884862ac1a16e54b6662cb30dcc25bb891cdc1d62c173532c5be6b9597cb45c32dc3b67f355b4c01ad272176af5f047149e0f9d1e29747a63de553e0bb7a70a198087e331e1e18323e50f761b28662a7a7b9a0a12e6789e10895d86764d8f3c5bf62f91434fce62ecca03f7969801d776aa9ae8fda2bf9ae0bd120f186e5f0fb0a60dc304c4038f065f9b742788dbe8d2a88d5054e6ce1d79e95c2ae65fb4d9b7045d2a4e69819eb55c9cc7bb46735ef7a06d00948f8682ef845523c4d30bb5c3e29ff5db529fbd6accc288e7880df6cc422ba00a56fe7ec5094547d0cf12b042aec506f6a1fa353e260c85942000ea847720f4c64870ed3c6defbdbb860bbd13d9f990f6cce99adc89f151d7693e4ed544512d5eb2cc6cebfe82525f9266c055dd73c989e7a959af3587da4699282b5b035a0464e73c79b3df27a6dbf34de2b507a471be9e606b7815e259e3cde3e80e3db3ed9203720cefe9435d05bf72a04d76dd321047617ccf66e538885688f94a5247830f69a653788a209be12631c7bcece66375cfa2775af919d266d00650d080b47165cba5748347d748282a1aa332cdc6571444e00f1a4aa747e922812d09e883b5ad2c4272ba4b5773bb98b44c2b35b4c924827002251a8f0b7917da3272b9cb5b8ed424e22a6a3c914b512bc95888672a5fbfedc068f462db928aec7ede46520ce22337486f2a0b292228a4deff106b665302da8b78d0b30c019a5e5a0084fce3058738d1fd758c03d9a460133e0a12fe1909c7945");
        sb.Append("3ded595ae19a329e2dba6e8e528fac60a5f1bb121ede721c26856e31aa9e90897f4931c8a701b676280bc41e9524819238b7be4f01c270af7b4ab96259d189fddfba3454c342a1790157f8f18535c475c9d52ac79464dbf3a520d06db58177868fa11f003bd55d5b0ca1b621d12a61ce053a8e96447f78b72d742567b7f0009ffeacc609728d1fc9211923f0d54c3022cb96f6bd494bfc72a2c9651e46963c992c9ea785dfcdd21b7fc5bdc54494857d7aa17f2af08276caef60df05ffedd86694e3ff3f6c8838317a9780f70b7f5874d6fea8a82761188b757fd8f7e722ddaa490a0420f6dcb4473dcce341f04236913fe87192b5bb448e1a0adcdc56b5553dce1e193e4e8ca16ba3b17d436f5222e6c20f445a7d45f5085c2a0862e6adab1fea0434f8898bdb50c59d08bbec85b4c1973b1a2ab0411c752a5c6585302489c17a801231b479f9856eeca150ade6b137775a8bb43e194f749a7f85169ec1e60b348c42ff13feba7cd3a0b8faac2aee8055e843458c1f7ef35a1a4c38cd599e6b72df2e78791f5929866d53a728d216548e38e14a5caf9f960827d90f0317bdc9f131cef726faa1664aa6f2972bbf59a55a9106a487fb155fed111269fd5652c6dcb465c020546eec4bccae4515b668e047029b7c1bc44d7b681f77e7e391a2f8f76dcc111a5b25943c82c8c07922390d0370f119e9d681461225dba0e67271844ab79d81f4bac4d4d0c991b61db5034c00259d8309cd1330f75bdc5e9fd3dfc2de708c8a96efc0afa7427ed093f779d344fee9d53489d1ccc9e61124646f9520ed614f8d5f5d7b22417d5f37d4531cbf2ef7e62ad57232edab974d7065c64c752ee184f0037e0dc86fce1a406f5de7972afc27716b9fd655a5c03e5b79c5d8b4bb91aa43ccc1093a6057e136edfa0bc4cab1f6216191a730b223693149d041879d1fedc19dc9e92111f939ebc294e6c2c685dc0633fd48ea0c5c09a1c32afee5c3d7f334ac498592eb8f41fc5c701718cfe58a63b10cb7030d021afe9a7a925baf449825a7279e0530df5737fddc484ed2f5b76e4bc7a5dd238e4bf6f43229cea7823e2aede3382b2e4b11abce8486fd419323e8186f0cd3455c2a201723b1507fe0a0f5c0e94ec96de79a85dc8f1df7bfe1e7da90a86b5c35fad66e7f48ce0ed3610454b7ef57cd8e8c323b452d311ee96617866d9c7ea51401f10137ccd9a64d0ef77c7da4a4171701a6ab93377ba5f8583ce6deaae91f5bdb5cdbe9e74e3806efc812a64e3437402f2ac83cab47");
        sb.Append("4c5376e8e03acf07ba84b73a1c454201a6436e79cf039617c4ccbc4fe46286c6a9ce21991a378bb27952e10f6be13fc4478ce6f9110b88");
        sb.Append("3cfe615b72c70466a2d79d8d3b66dbd421");
        byte[] blob = sb.ToString().HexToBinary();
        var decryptedBlob = pek.DecryptSecret(blob);
        var cred = new SupplementalCredentials(decryptedBlob);

        // Check properties
        Assert.AreEqual(@"Pa$$w0rd3", cred.ClearText);
        Assert.AreEqual(29, cred.WDigest.Length);
        Assert.AreEqual(1, cred.Kerberos.Credentials.Length);
        Assert.AreEqual(3, cred.KerberosNew.Credentials.Length);
        Assert.AreEqual(4096, cred.KerberosNew.DefaultIterationCount);
    }


    [TestMethod]
    public void PasswordEncryptionKey_NullInput()
    {
        throw new AssertInconclusiveException();
    }
}
