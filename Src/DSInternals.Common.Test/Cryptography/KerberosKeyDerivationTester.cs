namespace DSInternals.Common.Cryptography.Test
{
    using DSInternals.Common.Data;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.ComponentModel;
    using System.Security;

    [TestClass]
    public class KerberosKeyDerivationTester
    {
        [TestMethod]
        public void KerberosKeyDerivation_DES_CBC_MD5_User()
        {
            var password = "Pa$$w0rd".ToSecureString();
            string salt = "ADATUM.COMApril";
            int iterations = 4096;
            string expected = "76fe3b5bda911a40";

            byte[] result = KerberosKeyDerivation.DeriveKey(KerberosKeyType.DES_CBC_MD5, password, salt, iterations);
            Assert.AreEqual(expected, result.ToHex(false));
        }

        [TestMethod]
        public void KerberosKeyDerivation_DES_CBC_MD5_Service()
        {
            SecureString password = "f81377aacff9cafe039d91a8f758de148200332b062dc1ac59d8cfcb4f14d9fe0def16e33e4b1a7d90645407860797097ac424570c0664f50d3f3433cea5c3e8594eada2797ef1e27cda6d92fe72d3425206e3ca173f01ac04325d0eaab2eac06b3ff7b4668f3a62a1696e27c1c32e7f06e09adb7784290a2704dc02416bb46c19e91bc4b5a842ce0879459439f685b20225134ed4562cb5bcd944d0acb07986308466385a455e65fd0ee325cae97709a33bc0f413b66ef40bbc59ce7a2a20f500cc1f80a13849b86efbfd59f037277c017ac4bfda3596a75cf06d84a5e118a948653f1aef02dec76501d26ea3cc3b63bb587824a727d02373ea8a5a9e7a71f5".HexToBinary().ReadSecureWString(0);
            string salt = "CONTOSO.COMhostsvc_adfs.contoso.com";
            int iterations = 4096;
            string expected = "16bab507d3dad66d";

            byte[] result = KerberosKeyDerivation.DeriveKey(KerberosKeyType.DES_CBC_MD5, password, salt, iterations);

            // TODO: Properly implement DES key derivation for service accounts.
            Assert.Inconclusive("DES key derivation does not yield proper results for service accounts yet.");
            Assert.AreEqual(expected, result.ToHex(false));
        }

        [TestMethod]
        public void KerberosKeyDerivation_AES256_CTS_HMAC_SHA1_96_User()
        {
            var password = "Pa$$w0rd".ToSecureString();
            string salt = "ADATUM.COMApril";
            int iterations = 4096;
            string expected = "3a3b6a89bb82d112db5ef68f6db5d1afc2b806df61dcd85e3eacf3b85ee382d8";

            byte[] result = KerberosKeyDerivation.DeriveKey(KerberosKeyType.AES256_CTS_HMAC_SHA1_96, password, salt, iterations);
            Assert.AreEqual(expected, result.ToHex(false));
        }

        [TestMethod]
        public void KerberosKeyDerivation_AES256_CTS_HMAC_SHA1_96_Service()
        {
            SecureString password = "f81377aacff9cafe039d91a8f758de148200332b062dc1ac59d8cfcb4f14d9fe0def16e33e4b1a7d90645407860797097ac424570c0664f50d3f3433cea5c3e8594eada2797ef1e27cda6d92fe72d3425206e3ca173f01ac04325d0eaab2eac06b3ff7b4668f3a62a1696e27c1c32e7f06e09adb7784290a2704dc02416bb46c19e91bc4b5a842ce0879459439f685b20225134ed4562cb5bcd944d0acb07986308466385a455e65fd0ee325cae97709a33bc0f413b66ef40bbc59ce7a2a20f500cc1f80a13849b86efbfd59f037277c017ac4bfda3596a75cf06d84a5e118a948653f1aef02dec76501d26ea3cc3b63bb587824a727d02373ea8a5a9e7a71f5".HexToBinary().ReadSecureWString(0);
            string salt = "CONTOSO.COMhostsvc_adfs.contoso.com";
            int iterations = 4096;
            string expected = "5dcc418cd0a30453b267e6e5b158be4b4d80d23fd72a6ae4d5bd07f023517117";

            byte[] result = KerberosKeyDerivation.DeriveKey(KerberosKeyType.AES256_CTS_HMAC_SHA1_96, password, salt, iterations);
            Assert.AreEqual(expected, result.ToHex(false));
        }

        [TestMethod]
        public void KerberosKeyDerivation_AES128_CTS_HMAC_SHA1_96()
        {
            var password = "Pa$$w0rd".ToSecureString();
            string salt = "ADATUM.COMApril";
            int iterations = 4096;
            string expected = "a72c8bc96c4a6f03244f0b0067a1e440";

            byte[] result = KerberosKeyDerivation.DeriveKey(KerberosKeyType.AES128_CTS_HMAC_SHA1_96, password, salt, iterations);
            Assert.AreEqual(expected, result.ToHex(false));
        }

        [TestMethod]
        [ExpectedException(typeof(Win32Exception))]
        public void KerberosKeyDerivation_NULL()
        {
            var password = "Pa$$w0rd".ToSecureString();
            string salt = "ADATUM.COMApril";
            int iterations = 4096;

            byte[] result = KerberosKeyDerivation.DeriveKey(KerberosKeyType.NULL, password, salt, iterations);
        }

        [TestMethod]
        public void KerberosKeyDerivation_UserSalt()
        {
            Assert.AreEqual("ADATUM.COMApril", KerberosKeyDerivation.DeriveSalt("April", "Adatum.com"));
        }

        [TestMethod]
        public void KerberosKeyDerivation_ComputerSalt()
        {
            Assert.AreEqual("CONTOSO.COMhostdc01.contoso.com", KerberosKeyDerivation.DeriveSalt("DC01$", "Contoso.com"));
        }
    }
}
