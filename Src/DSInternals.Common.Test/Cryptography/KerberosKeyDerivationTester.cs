namespace DSInternals.Common.Cryptography.Test
{
    using DSInternals.Common.Data;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.ComponentModel;

    [TestClass]
    public class KerberosKeyDerivationTester
    {
        [TestMethod]
        public void KerberosKeyDerivation_DES_CBC_MD5()
        {
            var password = "Pa$$w0rd".ToSecureString();
            string salt = "ADATUM.COMApril";
            int iterations = 4096;
            string expected = "76fe3b5bda911a40";

            byte[] result = KerberosKeyDerivation.DeriveKey(KerberosKeyType.DES_CBC_MD5, password, salt, iterations);
            Assert.AreEqual(expected, result.ToHex(false));
        }

        [TestMethod]
        public void KerberosKeyDerivation_AES256_CTS_HMAC_SHA1_96()
        {
            var password = "Pa$$w0rd".ToSecureString();
            string salt = "ADATUM.COMApril";
            int iterations = 4096;
            string expected = "3a3b6a89bb82d112db5ef68f6db5d1afc2b806df61dcd85e3eacf3b85ee382d8";

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
        public void KerberosKeyDerivation_Salt()
        {
            Assert.AreEqual("ADATUM.COMApril", KerberosKeyDerivation.DeriveSalt("April", "Adatum.com"));
        }
    }
}
