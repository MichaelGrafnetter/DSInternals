using System.Security.Principal;
using DSInternals.Common.Data;

namespace DSInternals.Common.Test;

[TestClass]
public class DnsSigningKeyTester
{
    [TestMethod]
    public void DnsSigningKey_KSK_RSA()
    {
        // Arrange
        string dnsZone = "contoso.com";
        byte[] binaryData = "b0bf358f9e2ee541b165feba44d1f206100000005052544b080000009903000052005300410000003082039506092a864886f70d010703a0820386308203820201023182011da28201190201043081dc048184010000004b44534b020000006b010000180000000e000000716b551c22ed5fc4723cddbe199f6824200000001800000018000000072618fb781c33c99a585d06abe816a685a5b04919f32fc03e286b0f41ba1d0e63006f006e0074006f0073006f002e0063006f006d00000063006f006e0074006f0073006f002e0063006f006d000000305306092b0601040182374a013046060a2b0601040182374a01013038303630340c035349440c2d532d312d352d32312d333238383835303339322d333239393533363933322d323631343739333038312d353136300b060960864801650304012d0428ddc5e7476a37f431a094e230ed2ae555490a63c966456c0c046bda4e8f15d580652c2142b4973ef73082025a06092a864886f70d010701301e060960864801650304012e3011040c3bd07cab67c4b56aba2687330201108082022bc3a7ad5d896ff602e13ab3ef9992d644ae7954ea6f5622a8e66078d754f54f5c5fde13488bcf7a7b43ac397717ea6087ebc9b3be7d3bb175520e15a49a32891a83153b965eb33c2882ee713060c47a7d43b085e0a2344c8acfa7f761b15415a6190dd06f5f6d8716bd065bf90b6ba6a33d6a32f50f01d71c7e6466357af799e31e8920eee9ca3212da4cb0058ab90586c45f1aa224f4b7d4a6b3d6d066c0b2f69502382d98756966c800aaea5a7b8c74b5da6d48c9fea70b426e40b6205352e50908e3af36c91b72c25c681d9adf0b930fc9f6d5af491495c6cfc4548cb009df589c0dd2f2d97b8332227a2e23f76325d92099a13405f3fc63e91f71a65e29488bf5e70955b0ca0bae69b5b63e755a09acf65663f10af0dba5c0d0643828c084445ae8012cf947525a06c9e607e6af5964c9759cc777222175a3f38b5bae20ee7b19e1a6eb0fa6061cb699e227854af118307c436436df0635b5474b382b0a58aa5c2b9fc02d21f9cc4fb6eaa0b7f53a38823398c56cafed228a8287a2020ff4dcb975c4916c0e36e05c8394a3c822e37709e42977bd944823b21a9c1711c6f354f7458ff3ac162b627d03b84091acacebadaecaac24c45324394beb07474d93727086a0053a4477005a7b9ff63de9c67b8fd6bfb7a4fc9ff9243e7f548830d6e5123f6e30901ed978ac707936112afdf80b94f6eb2adb5dce97b5455c43bba28088eb59fe188b6eb12449291dca235db558850379040d245bb4469c656029a7e86f2256c69916c50c4851".HexToBinary();
        byte[] binaryKDSRootKey = "ccc82c5ed3c98fdb0c2890b2881c01e3a0dbc0f5594fca5b5aab7286fff60154b0733fab4b0ec5b622c1c2ab9c45cf1567893534f073ab6fb072e8113bbce329".HexToBinary();
        Guid kdsRootKeyId = Guid.Parse("1c556b71-ed22-c45f-723c-ddbe199f6824");
        SecurityIdentifier dcSid = new SecurityIdentifier("S-1-5-21-3288850392-3299536932-2614793081-516");

        // Parse the signing key
        DnsSigningKey signingKey = DnsSigningKey.Decode(dnsZone, binaryData);
        KdsRootKey kdsRootKey = new(kdsRootKeyId, binaryKDSRootKey);

        // Assert parsed values
        Assert.IsNotNull(signingKey);
        Assert.AreEqual("RSA", signingKey.AlgorithmName);
        Assert.AreEqual("contoso.com", signingKey.DnsZone);
        Assert.AreEqual(Guid.Parse("8F35BFB0-2E9E-41E5-B165-FEBA44D1F206"), signingKey.Guid);
        Assert.AreEqual(kdsRootKeyId, signingKey.ProtectedKeyBlob.ProtectionKeyIdentifier.RootKeyId);
        Assert.AreEqual(dcSid, signingKey.ProtectedKeyBlob.TargetSid);

        // Decrypt the key
        var gke = GroupKeyEnvelope.Create(kdsRootKey, signingKey.ProtectedKeyBlob.ProtectionKeyIdentifier, signingKey.ProtectedKeyBlob.TargetSid);
        gke.WriteToCache();
        ReadOnlySpan<byte> decryptedKey = signingKey.ProtectedKeyBlob.Decrypt();

        // Assert decrypted key magic
        Assert.StartsWith("52534132", decryptedKey.ToHex()); // "RSA2" encoded in HEX (RSAPUBKEY structure magic)
    }

    [TestMethod]
    public void DnsSigningKey_ZSK_RSA()
    {
        // Arrange
        string dnsZone = "contoso.com";
        byte[] binaryData = "393195f709027e48931efd217763b314100000005052544b080000009902000052005300410000003082029506092a864886f70d010703a0820286308202820201023182011da28201190201043081dc048184010000004b44534b020000006b010000180000000e000000716b551c22ed5fc4723cddbe199f6824200000001800000018000000daeec9d1492528f39ab3940f45db271206bd6a1a95b0de912c3a7d801dd9a34a63006f006e0074006f0073006f002e0063006f006d00000063006f006e0074006f0073006f002e0063006f006d000000305306092b0601040182374a013046060a2b0601040182374a01013038303630340c035349440c2d532d312d352d32312d333238383835303339322d333239393533363933322d323631343739333038312d353136300b060960864801650304012d04289dbc38d4a2e266ff186138cbc3c2f283a05c5d75fc8e02ef6778c01fd223cd8559e09d0af869241f3082015a06092a864886f70d010701301e060960864801650304012e3011040c0c94432165ddad4c261fcbb30201108082012b5b976b236f58a7cf92566e3203ba595557fd3f11070d7ab3c60ae5c863d22d2627badf2ed764f3189ba8a4653a73e14ffa780236a1325821918ee27e9cf9a6e160725fbe79db63feeb9bedee18011f6ec04cead1e50e7063c862f05594e7869ab209589ee16a4c08c869663e9551e2bf4eda1e38817d38b53710469c35e079ded950cd8b7709dc406b6bf6335a24823d63d652e523ffac6862868fd26fad4d0764eb3020181a54c0a4baef64dbe490a261f86b5c392a7b2d48705805823b6d519202b2174d87d1edec5bc0cecf11aec5543c7c9b4b004b6fb3a80d70e11f104a439e0874121afffa0b95035566c500cafedef22f885c3c469a0b2f47488d50c43fd71de8679df2ab88911e4c88e561520845ef285eb8c6e834ca8d5ff40467fb1a960ded85882f23d62659".HexToBinary();
        byte[] binaryKDSRootKey = "ccc82c5ed3c98fdb0c2890b2881c01e3a0dbc0f5594fca5b5aab7286fff60154b0733fab4b0ec5b622c1c2ab9c45cf1567893534f073ab6fb072e8113bbce329".HexToBinary();

        // Parse the signing key
        DnsSigningKey signingKey = DnsSigningKey.Decode(dnsZone, binaryData);
        KdsRootKey kdsRootKey = new(signingKey.ProtectedKeyBlob.ProtectionKeyIdentifier.RootKeyId, binaryKDSRootKey);

        // Assert parsed values
        Assert.IsNotNull(signingKey);
        Assert.AreEqual("RSA", signingKey.AlgorithmName);
        Assert.AreEqual("contoso.com", signingKey.DnsZone);
        Assert.AreEqual(Guid.Parse("f7953139-0209-487e-931e-fd217763b314"), signingKey.Guid);

        // Decrypt the key
        var gke = GroupKeyEnvelope.Create(kdsRootKey, signingKey.ProtectedKeyBlob.ProtectionKeyIdentifier, signingKey.ProtectedKeyBlob.TargetSid);
        gke.WriteToCache();
        ReadOnlySpan<byte> decryptedKey = signingKey.ProtectedKeyBlob.Decrypt();

        // Assert decrypted key magic
        Assert.StartsWith("52534132", decryptedKey.ToHex()); // "RSA2" encoded in HEX (RSAPUBKEY structure magic)
    }

    [TestMethod]
    public void DnsSigningKey_KSK_P256()
    {
        // Arrange
        string dnsZone = "contoso.com";
        byte[] binaryData = "7e4ef9ddd80ebb4b944703fe66202874100000005052544b16000000e3010000450043004400530041005f0050003200350036000000308201df06092a864886f70d010703a08201d0308201cc0201023182011da28201190201043081dc048184010000004b44534b020000006b010000180000000e000000716b551c22ed5fc4723cddbe199f6824200000001800000018000000dbed0d2ad7b6c566a873977f046159dd455264ebc56195621f1f1f17723276a563006f006e0074006f0073006f002e0063006f006d00000063006f006e0074006f0073006f002e0063006f006d000000305306092b0601040182374a013046060a2b0601040182374a01013038303630340c035349440c2d532d312d352d32312d333238383835303339322d333239393533363933322d323631343739333038312d353136300b060960864801650304012d0428c23870d9f8da7c17fde47a1bfedb59a066d5805739b1a0ee2554fbf35bb21edf73f1e89206fc93703081a506092a864886f70d010701301e060960864801650304012e3011040ce5eda9788c9e1e9ed605cea202011080787b3f467f4369e521a55910adef08a5cfec8b96aea1cd32c9c547606ba59294d52cd684fd85d421c95fd92175c6c7d83ad66f44a1551f990c74617b279aac8583a0941fdb30b0bab4eef9d9cb5429992e0989a250479195b4ae6abe68e3224f7cb4d8563b0cea7d8003c5f2db350fb30132c98facba5765a0".HexToBinary();
        byte[] binaryKDSRootKey = "ccc82c5ed3c98fdb0c2890b2881c01e3a0dbc0f5594fca5b5aab7286fff60154b0733fab4b0ec5b622c1c2ab9c45cf1567893534f073ab6fb072e8113bbce329".HexToBinary();

        // Parse the signing key
        DnsSigningKey signingKey = DnsSigningKey.Decode(dnsZone, binaryData);
        KdsRootKey kdsRootKey = new(signingKey.ProtectedKeyBlob.ProtectionKeyIdentifier.RootKeyId, binaryKDSRootKey);

        // Assert parsed values
        Assert.IsNotNull(signingKey);
        Assert.AreEqual("ECDSA_P256", signingKey.AlgorithmName);
        Assert.AreEqual("contoso.com", signingKey.DnsZone);
        Assert.AreEqual(Guid.Parse("ddf94e7e-0ed8-4bbb-9447-03fe66202874"), signingKey.Guid);

        // Decrypt the key
        var gke = GroupKeyEnvelope.Create(kdsRootKey, signingKey.ProtectedKeyBlob.ProtectionKeyIdentifier, signingKey.ProtectedKeyBlob.TargetSid);
        gke.WriteToCache();
        ReadOnlySpan<byte> decryptedKey = signingKey.ProtectedKeyBlob.Decrypt();

        // Assert decrypted key magic
        Assert.StartsWith("45435332", decryptedKey.ToHex()); // "ECK2" encoded in HEX
    }
}
