using System;
using System.Security.Principal;
using DSInternals.Common.Cryptography;
using DSInternals.Common.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DSInternals.Common.Test
{
    [TestClass]
    public class KdsRootKeyTester
    {
        [TestMethod]
        public void ParseKdfParameters_Vector1()
        {
            byte[] blob = "00000000010000000e000000000000005300480041003500310032000000".HexToBinary();
            var result = KdsRootKey.ParseKdfParameters(blob);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("SHA512", result[0]);
        }

        [TestMethod]
        public void ParseKdfParameters_NullInput()
        {
            var result = KdsRootKey.ParseKdfParameters(null);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void ParseKdfParameters_EmptyInput()
        {
            var result = KdsRootKey.ParseKdfParameters(new byte[0]{});
            Assert.IsNull(result);
        }

        [TestMethod]
        public void ParseSecretAgreementParameters_Vector1()
        {
            byte[] blob = "0c0200004448504d0001000087a8e61db4b6663cffbbd19c651959998ceef608660dd0f25d2ceed4435e3b00e00df8f1d61957d4faf7df4561b2aa3016c3d91134096faa3bf4296d830e9a7c209e0c6497517abd5a8a9d306bcf67ed91f9e6725b4758c022e0b1ef4275bf7b6c5bfc11d45f9088b941f54eb1e59bb8bc39a0bf12307f5c4fdb70c581b23f76b63acae1caa6b7902d52526735488a0ef13c6d9a51bfa4ab3ad8347796524d8ef6a167b5a41825d967e144e5140564251ccacb83e6b486f6b3ca3f7971506026c0b857f689962856ded4010abd0be621c3a3960a54e710c375f26375d7014103a4b54330c198af126116d2276e11715f693877fad7ef09cadb094ae91e1a15973fb32c9b73134d0b2e77506660edbd484ca7b18f21ef205407f4793a1a0ba12510dbc15077be463fff4fed4aac0bb555be3a6c1b0c6b47b1bc3773bf7e8c6f62901228f8c28cbb18a55ae31341000a650196f931c77a57f2ddf463e5e9ec144b777de62aaab8a8628ac376d282d6ed3864e67982428ebc831d14348f6f2f9193b5045af2767164e1dfc967c1fb3f2e55a4bd1bffe83b9c80d052b985d182ea0adb2a3b7313d3fe14c8484b1e052588b9b7d2bbd2df016199ecd06e1557cd0915b3353bbb64e0ec377fd028370df92b52c7891428cdc67eb6184b523d1db246c32f63078490f00ef8d647d148d47954515e2327cfef98c582664b4c0f6cc41659".HexToBinary();
            (byte[] p, byte[] g) = KdsRootKey.ParseSecretAgreementParameters(blob);
            Assert.IsNotNull(p);
            Assert.IsNotNull(g);
            Assert.AreEqual(p.Length, g.Length);
        }

        [TestMethod]
        public void ParseSecretAgreementParameters_NullInput()
        {
            (byte[] p, byte[] g) = KdsRootKey.ParseSecretAgreementParameters(null);
            Assert.IsNull(p);
            Assert.IsNull(g);
        }

        [TestMethod]
        public void ParseSecretAgreementParameters_EmptyInput()
        {
            (byte[] p, byte[] g) = KdsRootKey.ParseSecretAgreementParameters(new byte[0] { });
            Assert.IsNull(p);
            Assert.IsNull(g);
        }

        [TestMethod]
        public void ComputeL0Key_Vector1()
        {
            byte[] l0Key = KdsRootKey.ComputeL0Key(
                Guid.Parse("7dc95c96-fa85-183a-dff5-f70696bf0b11"),
                "814ad2f3928ff96d3650487967392feab3924f3d0dff8629d46a723640101cff8ca2cbd6aba40805cf03b380803b27837d80663eb4d18fd4cec414ebb2271fe2".HexToBinary(),
                "SP800_108_CTR_HMAC",
                "00000000010000000e000000000000005300480041003500310032000000".HexToBinary(),
                361
            );

            Assert.AreEqual(
                "76d7341bbf6f85f439a14d3f68c6de31a83d2c55b1371c9c122f5b6f0eccff282973da43349da2b21a0a89b050b49e9ace951323f27638ccbfce8b6a0ead782b",
                l0Key.ToHex());
        }

        [TestMethod]
        public void GetGmsaPassword_Vector1()
        {
            byte[] binaryPassword = KdsRootKey.GetPassword(
                new SecurityIdentifier("S-1-5-21-2468531440-3719951020-3687476655-1109"),
                Guid.Parse("7dc95c96-fa85-183a-dff5-f70696bf0b11"),
                "814ad2f3928ff96d3650487967392feab3924f3d0dff8629d46a723640101cff8ca2cbd6aba40805cf03b380803b27837d80663eb4d18fd4cec414ebb2271fe2".HexToBinary(),
                "SP800_108_CTR_HMAC",
                "00000000010000000e000000000000005300480041003500310032000000".HexToBinary(),
                DateTime.FromFileTimeUtc(133387453261266352));

            Assert.AreEqual("0b5fbfb646dd7bce4f160ad69edb86ba", NTHash.ComputeHash(binaryPassword).ToHex());
        }

        [TestMethod]
        public void GetGmsaPassword_Vector2()
        {
            byte[] binaryPassword = KdsRootKey.GetPassword(
                new SecurityIdentifier("S-1-5-21-2468531440-3719951020-3687476655-1109"),
                Guid.Parse("7dc95c96-fa85-183a-dff5-f70696bf0b11"),
                "814ad2f3928ff96d3650487967392feab3924f3d0dff8629d46a723640101cff8ca2cbd6aba40805cf03b380803b27837d80663eb4d18fd4cec414ebb2271fe2".HexToBinary(),
                "SP800_108_CTR_HMAC",
                "00000000010000000e000000000000005300480041003500310032000000".HexToBinary(),
                DateTime.FromFileTimeUtc(133387453261266352),
                DateTime.FromFileTimeUtc(133403352475182719),
                30
                );

            Assert.AreEqual("0b5fbfb646dd7bce4f160ad69edb86ba", NTHash.ComputeHash(binaryPassword).ToHex());
        }

        [TestMethod]
        public void GetGmsaPassword_Vector3()
        {
            var managedPasswordId = new ProtectionKeyIdentifier("010000004b44534b02000000690100001a00000018000000965cc97d85fa3a18dff5f70696bf0b1100000000180000001800000063006f006e0074006f0073006f002e0063006f006d00000063006f006e0074006f0073006f002e0063006f006d000000".HexToBinary());
            byte[] binaryPassword = KdsRootKey.GetPassword(
                new SecurityIdentifier("S-1-5-21-2468531440-3719951020-3687476655-1109"),
                Guid.Parse("7dc95c96-fa85-183a-dff5-f70696bf0b11"),
                "814ad2f3928ff96d3650487967392feab3924f3d0dff8629d46a723640101cff8ca2cbd6aba40805cf03b380803b27837d80663eb4d18fd4cec414ebb2271fe2".HexToBinary(),
                "SP800_108_CTR_HMAC",
                "00000000010000000e000000000000005300480041003500310032000000".HexToBinary(),
                managedPasswordId.L0KeyId,
                managedPasswordId.L1KeyId,
                managedPasswordId.L2KeyId
                );

            Assert.AreEqual("0b5fbfb646dd7bce4f160ad69edb86ba", NTHash.ComputeHash(binaryPassword).ToHex());
        }
        [TestMethod]
        public void GetGmsaPassword_Vector4()
        {
            byte[] binaryPassword = KdsRootKey.GetPassword(
                new SecurityIdentifier("S-1-5-21-1040335485-253814736-2627409954-1145"),
                Guid.Parse("0670b5ed-f2aa-9a86-dd0e-49cfc2130533"),
                "902bc244751f7cfb1bbafff7586585d467496953da553fd3decae08421b6c0ab5f60637541655b8be90fa319e24041875eccd465e253ceba238e1d475c80f64b".HexToBinary(),
                "SP800_108_CTR_HMAC",
                "00000000010000000e000000000000005300480041003500310032000000".HexToBinary(),
                DateTime.FromFileTimeUtc(133211195280000000), // whenCreated ( 6 months diff)
                DateTime.FromFileTimeUtc(133404554396754922)
                );

            Assert.AreEqual("e510057c721830f0b27482833cff4986", NTHash.ComputeHash(binaryPassword).ToHex());
        }

        [TestMethod]
        public void IntervalCalculation_Reverse()
        {
            DateTime effectiveTime = KdsRootKey.GetRootIntervalStart(361, 28, 4);

            Assert.AreEqual(new DateTime(2023, 9, 27).Date, effectiveTime.Date);
        }
    }
}
